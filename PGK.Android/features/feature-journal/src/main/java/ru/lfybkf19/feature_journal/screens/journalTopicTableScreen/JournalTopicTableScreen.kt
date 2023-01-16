package ru.lfybkf19.feature_journal.screens.journalTopicTableScreen

import android.annotation.SuppressLint
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import kotlinx.coroutines.flow.onEach
import ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.model.JournalTopicTableBottomDrawerType
import ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.viewModel.JournalTopicTableViewModel
import ru.lfybkf19.feature_journal.view.JournalTopicTable
import ru.pgk63.core_common.api.journal.model.CreateJournalTopicBody
import ru.pgk63.core_common.api.journal.model.JournalTopic
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_common.extension.toDate
import ru.pgk63.core_common.validation.nameValidation
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.TextFieldBase
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.dialog.calendar.CalendarDialog
import ru.pgk63.core_ui.view.dialog.calendar.models.CalendarSelection
import ru.pgk63.core_ui.view.dialog.rememberSheetState
import java.util.*

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun JournalTopicTableRoute(
    viewModel: JournalTopicTableViewModel = hiltViewModel(),
    journalSubjectId: Int,
    maxSubjectHours: Int,
    onBackScreen: () -> Unit
) {
    val topics = viewModel.responseJournalTopicList.collectAsLazyPagingItems()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getJournalTopics(journalSubjectId = journalSubjectId)
    })

    viewModel.responseCreateJournalTopic.onEach { result ->
        when(result){
            is Result.Error -> Unit
            is Result.Loading -> Unit
            is Result.Success -> topics.refresh()
            null -> Unit
        }
    }.launchWhenStarted()

    JournalTopicTableScreen(
        topics = topics,
        maxSubjectHours = maxSubjectHours,
        onBackScreen = onBackScreen,
        createJournalTopic = {
            viewModel.createJournalTopic(journalSubjectId,it)
        }
    )
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun JournalTopicTableScreen(
    topics: LazyPagingItems<JournalTopic>,
    maxSubjectHours: Int,
    onBackScreen: () -> Unit,
    createJournalTopic: (CreateJournalTopicBody) -> Unit
) {
    val bottomDrawerState = rememberBottomDrawerState(initialValue = BottomDrawerValue.Closed)
    var journalTopicTableBottomDrawerType by remember { mutableStateOf<JournalTopicTableBottomDrawerType?>(null) }

    LaunchedEffect(key1 = journalTopicTableBottomDrawerType, block = {
        if(journalTopicTableBottomDrawerType != null){
            bottomDrawerState.open()
        }else {
            bottomDrawerState.close()
        }
    })

    LaunchedEffect(key1 = bottomDrawerState.isOpen, block = {
        if(!bottomDrawerState.isOpen){
            journalTopicTableBottomDrawerType = null
        }
    })

    Scaffold(
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.topics),
                onBackClick = onBackScreen,
                actions = {
                    IconButton(onClick = {
                        journalTopicTableBottomDrawerType = JournalTopicTableBottomDrawerType.CreateTopic
                    }) {
                        Icon(
                            imageVector = Icons.Default.Add,
                            contentDescription = null,
                            tint = PgkTheme.colors.tintColor
                        )
                    }
                }
            )
        },
        content = { paddingValues ->
            if (
                topics.itemCount <= 0 && topics.loadState.refresh !is LoadState.Loading
            ){
                EmptyUi()
            }else if(topics.loadState.refresh is LoadState.Error) {
                ErrorUi()
            }else{
                BottomDrawer(
                    drawerState = bottomDrawerState,
                    drawerBackgroundColor = PgkTheme.colors.secondaryBackground,
                    drawerShape = PgkTheme.shapes.cornersStyle,
                    gesturesEnabled = bottomDrawerState.isOpen,
                    drawerContent = {
                        if(topics.itemCount > 0) {
                            BottomDrawerContent(
                                type = journalTopicTableBottomDrawerType,
                                createJournalTopic = createJournalTopic
                            )
                        }else {
                            EmptyUi()
                        }
                    }
                ){
                    TopicList(
                        topics = topics,
                        maxSubjectHours = maxSubjectHours,
                        paddingValues = paddingValues
                    )
                }
            }
        }
    )
}

@Composable
private fun BottomDrawerContent(
    type: JournalTopicTableBottomDrawerType?,
    createJournalTopic: (CreateJournalTopicBody) -> Unit
) {
    when(type){
        JournalTopicTableBottomDrawerType.CreateTopic -> CreateTopicUi(
            createJournalTopic = createJournalTopic
        )
        null -> EmptyUi()
    }
}

@Composable
private fun CreateTopicUi(
    createJournalTopic: (CreateJournalTopicBody) -> Unit
) {
    var title by remember { mutableStateOf("") }
    var homeWork by remember { mutableStateOf("") }
    var hours by remember { mutableStateOf("") }
    var date by remember { mutableStateOf<Date?>(null) }

    val titleValidation = nameValidation(title)
    val hoursValidation = nameValidation(title)

    val calendarState = rememberSheetState()

    CalendarDialog(
        state = calendarState,
        selection = CalendarSelection.Date {
            date = it.toDate()
        }
    )

    LazyColumn(
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        item {
            Spacer(modifier = Modifier.height(15.dp))

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceAround
            ) {
                Text(
                    text = stringResource(id = R.string.topic),
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.heading,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    textAlign = TextAlign.Center
                )

                AnimatedVisibility(
                    visible = title.isNotEmpty() && hours.isNotEmpty() && date != null
                ) {
                    TextButton(onClick = {
                        createJournalTopic(
                            CreateJournalTopicBody(
                                title = title,
                                homeWork = homeWork.ifEmpty { null },
                                hours = hours.toInt()
                            ))
                    }) {
                        Text(
                            text = stringResource(id = R.string.add),
                            color = PgkTheme.colors.tintColor,
                            style = PgkTheme.typography.body,
                            fontFamily = PgkTheme.fontFamily.fontFamily,
                            textAlign = TextAlign.Center
                        )
                    }
                }
            }

            Spacer(modifier = Modifier.height(30.dp))

            TextFieldBase(
                text = title,
                onTextChanged = { title = it },
                errorText = if(titleValidation.second != null)
                    stringResource(id = titleValidation.second!!) else null,
                hasError = !titleValidation.first,
            )

            Spacer(modifier = Modifier.height(5.dp))

            TextFieldBase(
                text = homeWork,
                onTextChanged = { homeWork = it }
            )

            Spacer(modifier = Modifier.height(5.dp))

            TextFieldBase(
                text = hours,
                onTextChanged = { hours = it },
                keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                errorText = if(hoursValidation.second != null)
                    stringResource(id = hoursValidation.second!!) else null,
                hasError = !hoursValidation.first,
            )

            Spacer(modifier = Modifier.height(5.dp))

            TextButton(onClick = { calendarState.show() }) {
                Text(
                    text = stringResource(id = R.string.select_date),
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.body,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier.fillMaxWidth(),
                    textAlign = TextAlign.Center
                )
            }
        }
    }
}

@Composable
private fun TopicList(
    topics: LazyPagingItems<JournalTopic>,
    maxSubjectHours: Int,
    paddingValues: PaddingValues
) {
    Box(
        modifier = Modifier
            .padding(paddingValues)
            .fillMaxSize()
    ) {
        JournalTopicTable(
            topics = topics,
            maxSubjectHours = maxSubjectHours,
            onClickRow = {}
        )
    }
}
