package ru.lfybkf19.feature_journal.screens.journalDetailsScreen

import android.annotation.SuppressLint
import android.widget.Toast
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.BorderStroke
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.items
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.KeyboardArrowLeft
import androidx.compose.material.icons.filled.KeyboardArrowRight
import androidx.compose.material.icons.filled.Sort
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import androidx.paging.compose.itemsIndexed
import com.google.accompanist.pager.ExperimentalPagerApi
import com.google.accompanist.pager.PagerState
import com.google.accompanist.pager.VerticalPager
import com.google.accompanist.pager.rememberPagerState
import kotlinx.coroutines.flow.onEach
import kotlinx.coroutines.launch
import ru.lfybkf19.feature_journal.screens.journalDetailsScreen.model.JournalDetailsBottomDrawerType
import ru.lfybkf19.feature_journal.screens.journalDetailsScreen.viewModel.JournalDetailsViewModel
import ru.lfybkf19.feature_journal.view.JournalTableUi
import ru.pgk63.core_common.api.journal.model.CreateJournalColumnBody
import ru.pgk63.core_common.api.journal.model.JournalEvaluation
import ru.pgk63.core_common.api.journal.model.JournalSubject
import ru.pgk63.core_common.api.journal.model.JournalTopic
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_common.extension.parseToBaseDateFormat
import ru.pgk63.core_common.extension.parseToNetworkFormat
import ru.pgk63.core_common.extension.toDate
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.dialog.calendar.CalendarDialog
import ru.pgk63.core_ui.view.dialog.calendar.models.CalendarSelection
import ru.pgk63.core_ui.view.dialog.rememberSheetState
import java.util.*

@SuppressLint("FlowOperatorInvokedInComposition")
@OptIn(ExperimentalPagerApi::class, ExperimentalMaterialApi::class)
@Composable
internal fun JournalDetailsRoute(
    viewModel: JournalDetailsViewModel = hiltViewModel(),
    journalId: Int?,
    onJournalTopicTableScreen: (journalSubjectId: Int, maxSubjectHours: Int) -> Unit,
    onBackScreen: () -> Unit,
    onCreateJournalSubjectScreen: (journalId: Int) -> Unit,
    onStudentDetailsScreen: (studentId: Int) -> Unit
) {
    val context = LocalContext.current
    val scaffoldState = rememberScaffoldState()
    val pagerState = rememberPagerState()

    val journalSubjects = viewModel.responseJournalSubjectsList.collectAsLazyPagingItems()
    var userRole by remember { mutableStateOf<UserRole?>(null) }
    var journalColumnResponse by remember { mutableStateOf<Result<Unit?>?>(null) }

    val bottomDrawerState = rememberBottomDrawerState(initialValue = BottomDrawerValue.Closed)
    var journalDetailsBottomDrawerType by remember { mutableStateOf<JournalDetailsBottomDrawerType?>(null) }

    viewModel.userLocal.onEach { user ->
        userRole = user.userRole
    }

    viewModel.responseJournalColumn.onEach {
        journalColumnResponse = it
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getJournalSubjects(journalId = journalId)
    })

    LaunchedEffect(key1 = journalDetailsBottomDrawerType, block = {
        if(journalDetailsBottomDrawerType != null){
            bottomDrawerState.open()
        }else {
            bottomDrawerState.close()
        }
    })

    LaunchedEffect(key1 = bottomDrawerState.isOpen, block = {
        if(!bottomDrawerState.isOpen){
            journalDetailsBottomDrawerType = null
        }
    })

    LaunchedEffect(key1 = journalColumnResponse, block = {
        when(journalColumnResponse) {
            is Result.Error -> {
                scaffoldState.snackbarHostState.showSnackbar(
                    message = context.getString(R.string.error)
                )
            }
            is Result.Loading -> {
                scaffoldState.snackbarHostState.showSnackbar(
                    message = context.getString(R.string.loading)
                )
            }
            is Result.Success -> {
                journalSubjects.refresh()
                journalDetailsBottomDrawerType = null

                scaffoldState.snackbarHostState.showSnackbar(
                    message = context.getString(R.string.success)
                )

                scaffoldState.snackbarHostState.showSnackbar(
                    message = context.getString(R.string.updated)
                )
            }
            null -> Unit
        }
    })

    JournalDetailsScreen(
        scaffoldState = scaffoldState,
        pagerState = pagerState,
        bottomDrawerState = bottomDrawerState,
        userRole = userRole,
        journalDetailsBottomDrawerType = journalDetailsBottomDrawerType,
        journalSubjects = journalSubjects,
        onJournalTopicTableScreen = onJournalTopicTableScreen,
        onBackScreen = onBackScreen,
        onStudentDetailsScreen = onStudentDetailsScreen,
        onCreateJournalSubjectScreen = { journalId?.let { onCreateJournalSubjectScreen(it) } },
        getStudentsByGroupId = { groupId ->

            viewModel.getStudents(groupIds = listOf(groupId))

            viewModel.responseStudentList.collectAsLazyPagingItems()
        },
        onAddColumn = {
            viewModel.createColumn(it)
        },
        onUpdateColumn = { columnId, evaluation ->
            viewModel.updateEvaluation(columnId, evaluation)
        },
        onDeleteColumn = {
            viewModel.deleteColumn(it)
        },
        onJournalDetailsBottomDrawerTypeChange = {
            journalDetailsBottomDrawerType = it
        }
    )
}

@OptIn(ExperimentalPagerApi::class, ExperimentalMaterialApi::class)
@Composable
private fun JournalDetailsScreen(
    scaffoldState: ScaffoldState,
    bottomDrawerState: BottomDrawerState,
    pagerState: PagerState,
    journalDetailsBottomDrawerType: JournalDetailsBottomDrawerType?,
    userRole: UserRole?,
    journalSubjects: LazyPagingItems<JournalSubject>,
    getStudentsByGroupId: @Composable (groupId: Int) -> LazyPagingItems<Student>,
    onJournalTopicTableScreen: (journalSubjectId: Int, maxSubjectHours: Int) -> Unit,
    onBackScreen: () -> Unit,
    onCreateJournalSubjectScreen: () -> Unit,
    onStudentDetailsScreen: (studentId: Int) -> Unit,
    onAddColumn: (body: CreateJournalColumnBody) -> Unit,
    onUpdateColumn: (columnId: Int, evaluation: JournalEvaluation) -> Unit,
    onDeleteColumn: (columnId: Int) -> Unit,
    onJournalDetailsBottomDrawerTypeChange: (JournalDetailsBottomDrawerType?) -> Unit
) {
    val scope = rememberCoroutineScope()


    Scaffold(
        scaffoldState = scaffoldState,
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {

            val title = if(journalSubjects.itemCount > 0)
                journalSubjects[pagerState.currentPage]?.subject?.subjectTitle
                    ?: stringResource(id = R.string.journal)
            else
                stringResource(id = R.string.journal)

            TopBarBack(
                title = title,
                onBackClick = onBackScreen,
                modifier = Modifier.clickable {
                    onJournalDetailsBottomDrawerTypeChange(JournalDetailsBottomDrawerType.JournalSubjectDetails)
                },
                actions = {
                    AnimatedVisibility(visible = pagerState.currentPage != 0) {
                        IconButton(onClick = {
                            if(pagerState.currentPage != 0){
                                scope.launch {
                                    pagerState.animateScrollToPage(pagerState.currentPage-1)
                                }
                            }
                        }) {
                            Icon(
                                imageVector = Icons.Default.KeyboardArrowLeft,
                                contentDescription = null,
                                tint = PgkTheme.colors.tintColor
                            )
                        }
                    }

                    AnimatedVisibility(visible = pagerState.currentPage != (journalSubjects.itemCount-1)) {
                        IconButton(onClick = {
                            if(pagerState.currentPage != (journalSubjects.itemCount-1)){
                                scope.launch {
                                    pagerState.animateScrollToPage(pagerState.currentPage+1)
                                }
                            }
                        }) {
                            Icon(
                                imageVector = Icons.Default.KeyboardArrowRight,
                                contentDescription = null,
                                tint = PgkTheme.colors.tintColor
                            )
                        }
                    }

                    AnimatedVisibility(visible = journalSubjects.itemCount > 0) {
                        IconButton(onClick = {
                            onJournalDetailsBottomDrawerTypeChange(JournalDetailsBottomDrawerType.JournalSubjectList)
                        }) {
                            Icon(
                                imageVector = Icons.Default.Sort,
                                contentDescription = null,
                                tint = PgkTheme.colors.tintColor
                            )
                        }
                    }

                    if(userRole == UserRole.TEACHER) {
                        IconButton(onClick = { onCreateJournalSubjectScreen() }) {
                            Icon(
                                imageVector = Icons.Default.Add,
                                contentDescription = null,
                                tint = PgkTheme.colors.tintColor
                            )
                        }
                    }
                }
            )
        },
        content = { paddingValues ->
            if (
                journalSubjects.itemCount <= 0 && journalSubjects.loadState.refresh !is LoadState.Loading
            ){
                EmptyUi()
            }else if(journalSubjects.loadState.refresh is LoadState.Error) {
                ErrorUi()
            }else{
                BottomDrawer(
                    drawerState = bottomDrawerState,
                    drawerBackgroundColor = PgkTheme.colors.secondaryBackground,
                    drawerShape = PgkTheme.shapes.cornersStyle,
                    gesturesEnabled = bottomDrawerState.isOpen,
                    drawerContent = {
                        if(journalSubjects.itemCount > 0) {
                            BottomDrawerContent(
                                pagerState = pagerState,
                                journalSubject = journalSubjects[pagerState.currentPage]!!,
                                journalDetailsBottomDrawerType = journalDetailsBottomDrawerType,
                                journalSubjects = journalSubjects,
                                onJournalTopicTableScreen = onJournalTopicTableScreen,
                                onAddColumn = onAddColumn,
                                onUpdateColumn = onUpdateColumn,
                                onDeleteColumn = onDeleteColumn
                            )
                        }else {
                            EmptyUi()
                        }
                    }
                ){
                    JournalSubjectsUi(
                        journalSubjects = journalSubjects,
                        pagerState = pagerState,
                        paddingValues = paddingValues,
                        getStudentsByGroupId = getStudentsByGroupId,
                        onClickStudent = { onStudentDetailsScreen(it.id) },
                        onClickEvaluation = { evaluation, columnId, rowId, student, date ->
                            onJournalDetailsBottomDrawerTypeChange(JournalDetailsBottomDrawerType.JournalColumn(
                                columnId = columnId,
                                rowId = rowId,
                                student = student,
                                date = date,
                                evaluation = evaluation
                            ))
                       }
                    )
                }
            }
        }
    )
}

@OptIn(ExperimentalPagerApi::class)
@Composable
private fun BottomDrawerContent(
    pagerState: PagerState,
    journalDetailsBottomDrawerType: JournalDetailsBottomDrawerType?,
    journalSubjects: LazyPagingItems<JournalSubject>,
    journalSubject: JournalSubject,
    onJournalTopicTableScreen: (journalSubjectId: Int, maxSubjectHours: Int) -> Unit,
    onAddColumn: (body: CreateJournalColumnBody) -> Unit,
    onUpdateColumn: (columnId: Int, evaluation: JournalEvaluation) -> Unit,
    onDeleteColumn: (columnId: Int) -> Unit
) {
    val scope = rememberCoroutineScope()

    when(journalDetailsBottomDrawerType) {
        JournalDetailsBottomDrawerType.JournalSubjectDetails -> JournalSubjectDetails(
            journalSubject = journalSubject,
            onJournalTopicTableScreen = onJournalTopicTableScreen
        )
        JournalDetailsBottomDrawerType.JournalSubjectList -> JournalSubjectList(
            journalSubjects = journalSubjects,
            onClickItem = { _, index ->
                scope.launch {
                    pagerState.animateScrollToPage(index)
                }
            }
        )
        is JournalDetailsBottomDrawerType.JournalColumn -> {
            if(journalSubjects.itemCount > 0){
                JournalColumn(
                    columnId = journalDetailsBottomDrawerType.columnId,
                    rowId = journalDetailsBottomDrawerType.rowId,
                    student = journalDetailsBottomDrawerType.student,
                    date = journalDetailsBottomDrawerType.date,
                    evaluation = journalDetailsBottomDrawerType.evaluation,
                    onAddColumn = onAddColumn,
                    onUpdateColumn = onUpdateColumn,
                    onDeleteColumn = onDeleteColumn,
                    journalSubjectId = journalSubjects[pagerState.currentPage]!!.id
                )
            }else {
                EmptyUi()
            }
        }
        else -> EmptyUi()
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun JournalColumn(
    journalSubjectId: Int,
    columnId: Int?,
    rowId: Int?,
    student: Student,
    date: Date?,
    evaluation: JournalEvaluation?,
    onAddColumn: (body: CreateJournalColumnBody) -> Unit,
    onUpdateColumn: (columnId: Int, evaluation: JournalEvaluation) -> Unit,
    onDeleteColumn: (columnId: Int) -> Unit
) {
    val context = LocalContext.current
    val calendarState = rememberSheetState()

    var selectEvaluation by remember { mutableStateOf(evaluation) }
    var selectDate by remember { mutableStateOf(date) }

    val dateText = if(selectDate != null)
        "(${selectDate!!.parseToBaseDateFormat()})"
    else
        ""

    CalendarDialog(
        state = calendarState,
        selection = CalendarSelection.Date {
            selectDate = it.toDate()
        }
    )

    LazyColumn {
        item {
            Text(
                text = "${stringResource(id = R.string.create_journal_column)}\n" +
                        "${student.fio()}\n$dateText",
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.heading,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier
                    .padding(5.dp)
                    .fillMaxWidth(),
                textAlign = TextAlign.Center
            )

            Spacer(modifier = Modifier.height(15.dp))

            LazyRow {
                item {
                    JournalEvaluation.values().forEach { item ->
                        Card(
                            modifier = Modifier.padding(5.dp),
                            shape = PgkTheme.shapes.cornersStyle,
                            backgroundColor = PgkTheme.colors.primaryBackground,
                            onClick = { selectEvaluation = item },
                            border = if(selectEvaluation == item)
                                BorderStroke(1.dp, PgkTheme.colors.tintColor)
                            else
                                null
                        ) {
                            Text(
                                text = item.text,
                                color = PgkTheme.colors.primaryText,
                                style = PgkTheme.typography.body,
                                fontFamily = PgkTheme.fontFamily.fontFamily,
                                modifier = Modifier.padding(10.dp),
                                textAlign = TextAlign.Center
                            )
                        }
                    }
                }
            }

            if (date == null) {
                TextButton(onClick = {
                    calendarState.show()
                }){
                    Text(
                        text = stringResource(id = R.string.select_date),
                        color = PgkTheme.colors.tintColor,
                        style = PgkTheme.typography.body,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        modifier = Modifier
                            .padding(5.dp)
                            .fillMaxWidth(),
                        textAlign = TextAlign.Center
                    )
                }
            }

            TextButton(onClick = {
                if(columnId == null && selectEvaluation != null && selectDate != null){
                    onAddColumn(CreateJournalColumnBody(
                        journalSubjectId = journalSubjectId,
                        journalSubjectRowId = rowId,
                        evaluation = selectEvaluation!!,
                        studentId = student.id,
                        date = selectDate!!.parseToNetworkFormat()
                    ))
                }else if(columnId != null && selectEvaluation != null){
                    onUpdateColumn(columnId, selectEvaluation!!)
                }else {
                    Toast.makeText(
                        context,
                        context.getString(R.string.error),
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }) {
                Text(
                    text = stringResource(id = R.string.add),
                    color = PgkTheme.colors.tintColor,
                    style = PgkTheme.typography.body,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier
                        .padding(5.dp)
                        .fillMaxWidth(),
                    textAlign = TextAlign.Center
                )
            }

            if (evaluation != null && columnId != null) {
                TextButton(onClick = {
                    onDeleteColumn(columnId)
                }){
                    Text(
                        text = stringResource(id = R.string.delete),
                        color = PgkTheme.colors.tintColor,
                        style = PgkTheme.typography.body,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        modifier = Modifier
                            .padding(5.dp)
                            .fillMaxWidth(),
                        textAlign = TextAlign.Center
                    )
                }
            }
        }
    }
}

@Composable
private fun JournalSubjectDetails(
    journalSubject: JournalSubject,
    onJournalTopicTableScreen: (journalSubjectId: Int, maxSubjectHours: Int) -> Unit
) {
    LazyColumn {
        item {
            Text(
                text = "${journalSubject.subject}\n(${journalSubject.teacher})",
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.heading,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier
                    .padding(5.dp)
                    .fillMaxWidth(),
                textAlign = TextAlign.Center
            )

            Spacer(modifier = Modifier.height(15.dp))

            Row(
                modifier = Modifier
                    .fillMaxWidth()
                    .clickable {
                        onJournalTopicTableScreen(journalSubject.id, journalSubject.hours)
                    },
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Text(
                    text = stringResource(id = R.string.topics),
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.heading,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier.padding(5.dp)
                )

                Icon(
                    imageVector = Icons.Default.KeyboardArrowRight,
                    contentDescription = null,
                    tint = PgkTheme.colors.tintColor
                )
            }
        }

        items(journalSubject.topics){ topic ->
            JournalTopicItem(
                topic = topic,
                maxSubjectHours = journalSubject.hours
            )
        }
    }
}

@Composable
private fun JournalTopicItem(
    topic: JournalTopic,
    maxSubjectHours: Int
) {
    Divider(color = PgkTheme.colors.primaryBackground)

    Row(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = Arrangement.SpaceBetween
    ) {
        Text(
            text = topic.date.parseToBaseDateFormat(),
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier.padding(5.dp)
        )

        Text(
            text = "${topic.hours}/$maxSubjectHours",
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier.padding(5.dp)
        )
    }

    Text(
        text = topic.title,
        color = PgkTheme.colors.primaryText,
        style = PgkTheme.typography.body,
        fontFamily = PgkTheme.fontFamily.fontFamily,
        modifier = Modifier.padding(5.dp),
        textAlign = TextAlign.Center
    )

    if(topic.homeWork != null){
        Text(
            text = topic.homeWork.toString(),
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier.padding(5.dp),
            textAlign = TextAlign.Center
        )
    }

    Divider(color = PgkTheme.colors.primaryBackground)
}

@Composable
private fun JournalSubjectList(
    journalSubjects: LazyPagingItems<JournalSubject>,
    onClickItem: (JournalSubject, index: Int) -> Unit
) {
    LazyColumn {
        itemsIndexed(journalSubjects) {index, journalSubject ->
            if(journalSubject != null) {
                JournalSubjectItem(
                    journalSubject = journalSubject,
                    index = index,
                    onClick = { onClickItem(journalSubject,index) }
                )
            }
        }
    }
}

@Composable
private fun JournalSubjectItem(
    journalSubject: JournalSubject,
    index: Int,
    onClick: () -> Unit
) {
    Column(
        modifier = Modifier.clickable { onClick() }
    ) {
        Divider(color = PgkTheme.colors.primaryBackground)

        Text(
            text = "${index + 1}. ${journalSubject.subject} (${journalSubject.teacher})",
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier.padding(5.dp)
        )

        Divider(color = PgkTheme.colors.primaryBackground)
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
private fun JournalSubjectsUi(
    journalSubjects: LazyPagingItems<JournalSubject>,
    pagerState: PagerState,
    paddingValues: PaddingValues,
    getStudentsByGroupId: @Composable (groupId:Int) -> LazyPagingItems<Student>,
    onClickStudent: (Student) -> Unit,
    onClickEvaluation: (
        JournalEvaluation?,
        columnId: Int?,
        rowId: Int?,
        student: Student,
        date: Date?
    ) -> Unit
) {
    VerticalPager(
        count = journalSubjects.itemCount,
        state = pagerState,
        userScrollEnabled = false,
        contentPadding = paddingValues
    ) { pageIndex ->
        Box(modifier = Modifier.fillMaxSize()) {
            val journalSubject = journalSubjects[pageIndex]

            if(journalSubject != null){
                JournalTableUi(
                    rows = journalSubject.rows,
                    students = getStudentsByGroupId.invoke(journalSubject.journal.group.id),
                    onClickStudent = onClickStudent,
                    onClickEvaluation = onClickEvaluation
                )
            }else{
                EmptyUi()
            }
        }
    }
}
