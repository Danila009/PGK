package ru.pgk63.feature_tech_support.screen.chatScreen

import android.annotation.SuppressLint
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.techSupport.model.*
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.extension.copyToClipboard
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_database.user.model.UserLocalDatabase
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.icon.ResIcons
import ru.pgk63.core_ui.view.*
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_tech_support.screen.chatScreen.enums.AttachMenu
import ru.pgk63.feature_tech_support.screen.chatScreen.enums.ChatMenu
import ru.pgk63.feature_tech_support.screen.chatScreen.enums.MessageMenu
import ru.pgk63.feature_tech_support.screen.chatScreen.viewModel.ChatViewModel
import java.util.*

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun ChatRoute(
    viewModel: ChatViewModel = hiltViewModel(),
    chatId: Int? = null,
    onBackScreen: () -> Unit
) {
    var user by remember { mutableStateOf(UserLocalDatabase()) }

    var messageText by remember { mutableStateOf("") }
    var messagesResult by remember { mutableStateOf<Result<MessageResponse>>(Result.Loading()) }
    val messagesParameter by remember { mutableStateOf(MessageListParameters(chatId = chatId)) }

    var searchMode by remember { mutableStateOf(false) }
    var searchText by remember { mutableStateOf("") }

    var pinMode by remember { mutableStateOf<Boolean>(false) }

    viewModel.user.onEach {
        user = it
    }.launchWhenStarted()

    viewModel.responseMessages.onEach {
        messagesResult = it
    }.launchWhenStarted()

    LaunchedEffect(key1 = user, block = {
        user.userId?.let {

        }
        viewModel.webSocketConnect()
    })

    LaunchedEffect(searchText, searchMode, block = {
        messagesParameter.search = if(searchMode && searchText.isNotEmpty()) searchText else null

        viewModel.messagesParameters(messagesParameter)
    })

    LaunchedEffect(key1 = pinMode, block = {
        messagesParameter.pin = if(!pinMode) null else true
        viewModel.messagesParameters(messagesParameter)
    })

    ChatScreen(
        user = user,
        messagesResult = messagesResult,
        pinMode = pinMode,
        searchMode = searchMode,
        searchText = searchText,
        messageText = messageText,
        onMessageChange = { messageText = it },
        onSearchModeChange = { searchMode = it },
        onSearchTextChange = { searchText = it },
        onPunModeChange = { pinMode = it },
        onBackScreen = onBackScreen,
        sendMessage = {
            viewModel.sendMessage(SendMessageBody(text = messageText))

            messageText = ""
        },
        pinMessage = { messageId ->
            viewModel.pinMessage(messageId)
        },
        deleteMessage = { messageId ->
            viewModel.deleteMessage(messageId)
        },
        editMessage = { message ->
            viewModel.updateMessage(
                messageId = message.id,
                body = UpdateMessageBody(
                    text = messageText
                )
            )

            messageText = ""
        }
    )
}

@Composable
private fun ChatScreen(
    user: UserLocalDatabase,
    messagesResult: Result<MessageResponse>,
    searchMode: Boolean = false,
    pinMode: Boolean = false,
    searchText: String = "",
    messageText:String = "",
    onMessageChange: (String) -> Unit = {},
    onSearchModeChange: (Boolean) -> Unit = {},
    onPunModeChange: (Boolean) -> Unit = {},
    onSearchTextChange: (String) -> Unit = {},
    onBackScreen: () -> Unit = {},
    sendMessage: () -> Unit = {},
    pinMessage: (messageId: Int) -> Unit = {},
    deleteMessage: (messageId: Int) -> Unit = {},
    editMessage: (message: Message) -> Unit = {}
) {
    var chatMenu by remember { mutableStateOf(false) }
    var editMessageAlertDialog by remember { mutableStateOf(false) }
    var clickMessageEdit by remember { mutableStateOf<Message?>(null) }

    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = if(!searchMode) stringResource(id = R.string.help) else "",
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                actions = {
                    Column {
                        AnimatedVisibility(visible = searchMode) {
                            TextFieldSearch(
                                text = searchText,
                                onTextChanged = onSearchTextChange,
                                onClose = {
                                    onSearchTextChange("")
                                    onSearchModeChange(false)
                                }
                            )
                        }

                        AnimatedVisibility(visible = !searchMode) {
                            IconButton(onClick = { chatMenu = !chatMenu }) {
                                Icon(
                                    imageVector = Icons.Default.MoreVert,
                                    contentDescription = null,
                                    tint = PgkTheme.colors.primaryText
                                )
                            }

                            ChatMenuUi(
                                visible = chatMenu,
                                pinMode = pinMode,
                                onDismissRequest = { chatMenu = false },
                                onClick = { chatMenu ->
                                    when(chatMenu){
                                        ChatMenu.SEARCH_MESSAGES -> onSearchModeChange(true)
                                        ChatMenu.PIN_MESSAGES -> onPunModeChange(!pinMode)
                                        ChatMenu.CLEAR_CHAT -> Unit
                                    }
                                }
                            )
                        }
                    }
                }
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
            when(messagesResult){
                is Result.Error -> ErrorUi(messagesResult.message)
                is Result.Loading -> LoadingUi()
                is Result.Success -> {
                    if(editMessageAlertDialog){
                        EditMessageAlertDialog(
                            messageText = messageText,
                            onMessageChange = onMessageChange,
                            editMessage = {
                                clickMessageEdit?.let { editMessage(it) }
                                editMessageAlertDialog = false
                            },
                            onDismissRequest = {
                                editMessageAlertDialog = false
                            }
                        )
                    }

                    Messages(
                        messages = messagesResult.data!!.results,
                        user = user,
                        bottomBarPadding = paddingValues.calculateBottomPadding(),
                        pinMessage = pinMessage,
                        deleteMessage = deleteMessage,
                        editMessage = { message ->
                            message.text?.let { onMessageChange(it) }
                            editMessageAlertDialog = true
                            clickMessageEdit = message
                        }
                    )
                }
            }
        }
    )
}

@Composable
private fun ChatMenuUi(
    visible: Boolean,
    pinMode: Boolean = false,
    onDismissRequest: () -> Unit,
    onClick: (ChatMenu) -> Unit
) {
    DropdownMenu(
        expanded = visible,
        modifier = Modifier.background(PgkTheme.colors.secondaryBackground),
        onDismissRequest = onDismissRequest
    ) {
        ChatMenu.values().forEach { chatMenu ->
            DropdownMenuItem(
                onClick = {
                    onClick(chatMenu)
                    onDismissRequest()
                }
            ) {
                Icon(
                    imageVector = chatMenu.icon,
                    contentDescription = null,
                    tint = if(chatMenu == ChatMenu.CLEAR_CHAT)
                        PgkTheme.colors.errorColor
                    else if(pinMode && chatMenu == ChatMenu.PIN_MESSAGES)
                        PgkTheme.colors.tintColor
                    else
                        PgkTheme.colors.primaryText
                )

                Spacer(modifier = Modifier.width(5.dp))

                Text(
                    text = stringResource(id = chatMenu.nameId),
                    color = if(chatMenu == ChatMenu.CLEAR_CHAT)
                        PgkTheme.colors.errorColor
                    else if(pinMode && chatMenu == ChatMenu.PIN_MESSAGES)
                        PgkTheme.colors.tintColor
                    else
                        PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.caption,
                    fontFamily = PgkTheme.fontFamily.fontFamily
                )
            }
        }
    }
}

@Composable
private fun BottomBarUi(
    messageText:String,
    onMessageChange: (String) -> Unit = {},
    sendMessage: () -> Unit = {}
) {
    var attachMenuUi by remember { mutableStateOf(false) }

    AnimatedVisibility(visible = !attachMenuUi) {
        SentMessageTextField(
            messageText = messageText,
            onMessageChange = onMessageChange,
            sendMessage = sendMessage,
            openAttachMenu = {
                attachMenuUi = true
            }
        )
    }

    AnimatedVisibility(visible = attachMenuUi) {
        AttachMenuUi(
            onClick = { attachMenu ->
                when(attachMenu){
                    AttachMenu.BACK -> attachMenuUi = false
                    AttachMenu.CAMERA -> Unit
                    AttachMenu.GALLERY -> Unit
                    AttachMenu.FILE -> Unit
                    AttachMenu.VIDEO -> Unit
                }
            }
        )
    }
}

@Composable
private fun SentMessageTextField(
    messageText:String,
    onMessageChange: (String) -> Unit = {},
    sendMessage: () -> Unit = {},
    openAttachMenu: (() -> Unit)? = null
) {
    TextField(
        value = messageText,
        onValueChange = onMessageChange,
        modifier = Modifier.fillMaxWidth(),
        placeholder = {
            Text(
                text = stringResource(id = R.string.message),
                color = PgkTheme.colors.primaryText
            )
        },
        colors = rememberTextFieldColors(
            focusedIndicatorColor = PgkTheme.colors.primaryBackground
        ),
        trailingIcon = {
            Row {
                AnimatedVisibility(
                    visible = messageText.isNotEmpty()
                ) {
                    IconButton(onClick = sendMessage) {
                        Icon(
                            imageVector = Icons.Default.Send,
                            contentDescription = null,
                            modifier = Modifier.padding(5.dp),
                            tint = PgkTheme.colors.primaryText
                        )
                    }
                }

                if(openAttachMenu != null){
                    IconButton(onClick = openAttachMenu) {
                        Icon(
                            painterResource(id = ResIcons.attachFile),
                            contentDescription = null,
                            modifier = Modifier
                                .padding(5.dp)
                                .size(30.dp),
                            tint = PgkTheme.colors.primaryText
                        )
                    }
                }
            }
        }
    )
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun AttachMenuUi(
    onClick:(AttachMenu) -> Unit
) {
    LazyRow(verticalAlignment = Alignment.CenterVertically) {
        item {
            AttachMenu.values().forEach { attachMenu ->
                Card(
                    backgroundColor = PgkTheme.colors.primaryBackground,
                    onClick = { onClick(attachMenu) }
                ) {
                    Column(
                        modifier = Modifier.padding(10.dp),
                        horizontalAlignment = Alignment.CenterHorizontally
                    ) {
                        Card(
                            shape = CircleShape,
                            backgroundColor = PgkTheme.colors.tintColor,
                            modifier = Modifier.padding(5.dp),
                        ) {
                            Icon(
                                painter = painterResource(id = attachMenu.iconId),
                                contentDescription = null,
                                tint = PgkTheme.colors.primaryText,
                                modifier = Modifier
                                    .size(30.dp)
                                    .padding(5.dp)
                            )
                        }

                        Text(
                            text = stringResource(id = attachMenu.nameId),
                            color = PgkTheme.colors.primaryText,
                            modifier = Modifier.padding(5.dp),
                            style = PgkTheme.typography.caption,
                            fontFamily = PgkTheme.fontFamily.fontFamily
                        )
                    }
                }
            }
        }
    }
}

@Composable
private fun Messages(
    messages: List<Message>,
    user: UserLocalDatabase,
    bottomBarPadding: Dp,
    pinMessage: (messageId: Int) -> Unit,
    deleteMessage: (messageId: Int) -> Unit,
    editMessage: (message: Message) -> Unit
) {
    val context = LocalContext.current

    LazyColumn(
        reverseLayout = true,
        modifier = Modifier.fillMaxSize()
    ) {
        item {
            Spacer(modifier = Modifier.height(bottomBarPadding))
        }

        items(messages){ message ->

            var messageMenuVisible by remember { mutableStateOf(false) }

            MessageUi(
                message = message,
                user = user,
                onClick = { messageMenuVisible = true }
            )

            MessageMenuUi(
                visible = messageMenuVisible,
                onVisibleChange = { messageMenuVisible = false },
                onClick = { messageMenu ->
                    when(messageMenu){
                        MessageMenu.COPY -> message.text?.let { context.copyToClipboard(it) }
                        MessageMenu.PIN -> pinMessage(message.id)
                        MessageMenu.EDIT -> editMessage(message)
                        MessageMenu.DELETE -> deleteMessage(message.id)
                    }
                }
            )
        }
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun MessageUi(
    message: Message,
    user: UserLocalDatabase,
    screenWidthDp: Int = LocalConfiguration.current.screenWidthDp,
    onClick: () -> Unit
) {
    Row(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = if(user.userId == message.user.id)
            Arrangement.End
        else
            Arrangement.Start
    ) {
        Card(
            modifier = Modifier
                .widthIn(max = (screenWidthDp / 1.5).dp)
                .padding(5.dp),
            backgroundColor = if(user.userId == message.user.id)
                PgkTheme.colors.tintColor
            else
                PgkTheme.colors.secondaryBackground,
            shape = PgkTheme.shapes.cornersStyle,
            onClick = onClick
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

                Row(
                    verticalAlignment = Alignment.CenterVertically,
                    modifier = Modifier.padding(15.dp)
                ) {

                    AnimatedVisibility(visible = message.pin){
                        Icon(
                            imageVector = Icons.Default.PushPin,
                            contentDescription = null,
                            tint = PgkTheme.colors.primaryText,
                            modifier = Modifier.size(18.dp)
                        )

                        Spacer(modifier = Modifier.width(5.dp))
                    }

                    AnimatedVisibility(visible = message.edited){
                        Icon(
                            imageVector = Icons.Default.Edit,
                            contentDescription = null,
                            tint = PgkTheme.colors.primaryText,
                            modifier = Modifier.size(18.dp)
                        )

                        Spacer(modifier = Modifier.width(5.dp))
                    }

                    Text(
                        text = message.date,
                        color = PgkTheme.colors.primaryText,
                        style = PgkTheme.typography.caption,
                        fontFamily = PgkTheme.fontFamily.fontFamily
                    )
                }
            }
        }
    }
}

@Composable
private fun MessageMenuUi(
    visible: Boolean,
    onVisibleChange: () -> Unit,
    onClick: (MessageMenu) -> Unit
) {
    if(visible){
        AlertDialog(
            backgroundColor = Color.Transparent,
            onDismissRequest = onVisibleChange,
            buttons = {
                MessageMenu.values().forEach { messageMenu ->
                    DropdownMenuItem(
                        modifier = Modifier.background(PgkTheme.colors.secondaryBackground),
                        onClick = {
                            onClick(messageMenu)
                            onVisibleChange()
                        }
                    ) {
                        Icon(
                            imageVector = messageMenu.icon,
                            contentDescription = null,
                            tint = if(messageMenu == MessageMenu.DELETE)
                                PgkTheme.colors.errorColor
                            else
                                PgkTheme.colors.primaryText,
                        )

                        Spacer(modifier = Modifier.width(10.dp))

                        Text(
                            text = stringResource(id = messageMenu.nameId),
                            color = if(messageMenu == MessageMenu.DELETE)
                                PgkTheme.colors.errorColor
                            else
                                PgkTheme.colors.primaryText,
                            style = PgkTheme.typography.caption,
                            fontFamily = PgkTheme.fontFamily.fontFamily
                        )
                    }
                }
            }
        )
    }
}

@Composable
private fun EditMessageAlertDialog(
    messageText:String,
    onMessageChange: (String) -> Unit = {},
    editMessage: () -> Unit = {},
    onDismissRequest: () -> Unit
) {
    AlertDialog(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        shape = PgkTheme.shapes.cornersStyle,
        onDismissRequest = onDismissRequest,
        buttons = {
            SentMessageTextField(
                messageText = messageText,
                onMessageChange = onMessageChange,
                sendMessage = editMessage
            )
        }
    )
}