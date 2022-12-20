package ru.pgk63.feature_tech_support.screen.chatScreen

import android.annotation.SuppressLint
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Send
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import androidx.paging.compose.items
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.techSupport.model.Message
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_database.user.model.UserLocalDatabase
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.rememberTextFieldColors
import ru.pgk63.feature_tech_support.screen.chatScreen.viewModel.ChatViewModel
import java.util.*

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun ChatRoute(
    viewModel: ChatViewModel = hiltViewModel(),
    onBackScreen: () -> Unit
) {
    var user by remember { mutableStateOf(UserLocalDatabase()) }

    var messageText by remember { mutableStateOf("") }
    val messages = viewModel.messages.collectAsLazyPagingItems()

    viewModel.user.onEach {
        user = it
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.connectMessagesWebSocket()
    })

    ChatScreen(
        user = user,
        messages = messages,
        messageText = messageText,
        onMessageChange = { messageText = it },
        onBackScreen = onBackScreen
    ) {
        viewModel.sendMessage(SendMessageBody(text = messageText))

        messageText = ""
    }
}

@Composable
private fun ChatScreen(
    user: UserLocalDatabase,
    messages: LazyPagingItems<Message>,
    messageText:String = "",
    onMessageChange: (String) -> Unit = {},
    onBackScreen: () -> Unit = {},
    sendMessage: () -> Unit = {}
) {
    Scaffold(
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.help),
                onBackClick = onBackScreen
            )
        },
        bottomBar = {
            BottomBarUi(
                sendMessage = sendMessage,
                messageText = messageText,
                onMessageChange = onMessageChange
            )
        },
        content = { paddingValues ->
            Surface(
                modifier = Modifier.fillMaxSize(),
                color = PgkTheme.colors.primaryBackground
            ) {
                LazyColumn(
                    reverseLayout = true
                ) {
                    item {
                        Spacer(modifier = Modifier
                            .height(paddingValues.calculateBottomPadding()))
                    }

                    items(messages){ message ->
                        message?.let {
                            MessageUi(message = message, user = user)
                        }
                    }
                }
            }
        }
    )
}

@Composable
private fun BottomBarUi(
    messageText:String,
    onMessageChange: (String) -> Unit = {},
    sendMessage: () -> Unit = {}
) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .background(PgkTheme.colors.primaryBackground),
        horizontalArrangement = Arrangement.SpaceBetween,
        verticalAlignment = Alignment.CenterVertically
    ) {
        TextField(
            value = messageText,
            onValueChange = onMessageChange,
            modifier = Modifier.fillMaxWidth(0.8f),
            placeholder = {
                Text(
                    text = stringResource(id = R.string.message),
                    color = PgkTheme.colors.primaryText
                )
            },
            colors = rememberTextFieldColors()
        )

        AnimatedVisibility(
            visible = messageText.isNotEmpty()
        ) {
            IconButton(onClick = { sendMessage() }) {
                Icon(
                    imageVector = Icons.Default.Send,
                    contentDescription = null,
                    modifier = Modifier.padding(5.dp),
                    tint = PgkTheme.colors.primaryText
                )
            }
        }
    }
}

@Composable
private fun MessageUi(
    message: Message,
    user: UserLocalDatabase,
    screenWidthDp: Dp = LocalConfiguration.current.screenWidthDp.dp
) {
    Row(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = if(user.userId == message.user.id)
            Arrangement.Start
        else
            Arrangement.End
    ) {
        Card(
            modifier = Modifier
                .widthIn(max = (screenWidthDp / 2))
                .padding(5.dp),
            backgroundColor = if(user.userId == message.user.id)
                PgkTheme.colors.tintColor
            else
                PgkTheme.colors.secondaryBackground,
            shape = PgkTheme.shapes.cornersStyle
        ) {
            Column {

                if(message.text != null && message.text!!.isNotEmpty()){
                    Text(
                        text = message.text!!,
                        color = PgkTheme.colors.primaryText,
                        modifier = Modifier.padding(
                            start = 15.dp,
                            top = 15.dp,
                            end = 15.dp
                        ),
                        style = PgkTheme.typography.body,
                        fontFamily = PgkTheme.fontFamily.fontFamily
                    )
                }

                Text(
                    text = message.date,
                    color = PgkTheme.colors.primaryText,
                    modifier = Modifier.padding(15.dp),
                    style = PgkTheme.typography.caption,
                    fontFamily = PgkTheme.fontFamily.fontFamily
                )
            }
        }
    }
}

@Preview
@Composable
private fun BottomBarUiPreview(){
    Column {
        MainTheme(darkTheme = false) {
            BottomBarUi(messageText = "Привет")
        }

        MainTheme(darkTheme = true) {
            BottomBarUi(messageText = "Привет")
        }
    }
}

@Preview
@Composable
private fun MessagePreview(){
    Column {
        MainTheme(darkTheme = false) {
            val message = Message(
                id = 1,
                text = "Привет",
                userVisible = false,
                pin = false,
                edited = false,
                date = "12:32 22.01.2003",
                user = User()
            )

            Surface(
                color = PgkTheme.colors.primaryBackground
            ) {
                MessageUi(
                    message = message,
                    screenWidthDp = 300.dp,
                    user = UserLocalDatabase()
                )
            }
        }

        MainTheme(darkTheme = true) {
            val message = Message(
                id = 1,
                text = "Привет",
                userVisible = false,
                pin = false,
                edited = false,
                date = "12:32 22.01.2003",
                user = User()
            )

            Surface(
                color = PgkTheme.colors.primaryBackground
            ) {
                MessageUi(
                    message = message,
                    screenWidthDp = 300.dp,
                    user = UserLocalDatabase()
                )
            }
        }
    }
}