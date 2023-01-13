package ru.lfybkf19.feature_journal.screens.journalDetailsScreen

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowLeft
import androidx.compose.material.icons.filled.KeyboardArrowRight
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import com.google.accompanist.pager.ExperimentalPagerApi
import com.google.accompanist.pager.PagerState
import com.google.accompanist.pager.VerticalPager
import com.google.accompanist.pager.rememberPagerState
import kotlinx.coroutines.launch
import ru.lfybkf19.feature_journal.screens.journalDetailsScreen.model.JournalDetailsSheetType
import ru.lfybkf19.feature_journal.screens.journalDetailsScreen.viewModel.JournalDetailsViewModel
import ru.lfybkf19.feature_journal.view.JournalTableUi
import ru.pgk63.core_common.api.journal.model.JournalSubject
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior

@OptIn(ExperimentalPagerApi::class)
@Composable
internal fun JournalDetailsRoute(
    viewModel: JournalDetailsViewModel = hiltViewModel(),
    journalId: Int?,
    onBackScreen: () -> Unit
) {
    val pagerState = rememberPagerState()

    val journalSubjects = viewModel.responseJournalSubjectsList.collectAsLazyPagingItems()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getJournalSubjects(journalId = journalId)
    })

    JournalDetailsScreen(
        pagerState = pagerState,
        journalSubjects = journalSubjects,
        onBackScreen = onBackScreen,
        getStudentsByGroupId = { groupId ->

            viewModel.getStudents(groupIds = listOf(groupId))

            viewModel.responseStudentList.collectAsLazyPagingItems()
        }
    )
}

@OptIn(ExperimentalPagerApi::class, ExperimentalMaterialApi::class)
@Composable
private fun JournalDetailsScreen(
    pagerState: PagerState,
    journalSubjects: LazyPagingItems<JournalSubject>,
    getStudentsByGroupId: @Composable (groupId:Int) -> LazyPagingItems<Student>,
    onBackScreen: () -> Unit
) {
    val scope = rememberCoroutineScope()
    val scrollBehavior = rememberToolbarScrollBehavior()
    val modalBottomSheetState = rememberModalBottomSheetState(
        initialValue = ModalBottomSheetValue.Hidden
    )
    var journalDetailsSheetType by remember { mutableStateOf<JournalDetailsSheetType?>(null) }

    LaunchedEffect(key1 = journalDetailsSheetType, block = {
        if(journalDetailsSheetType != null){
            modalBottomSheetState.show()
        }else {
            modalBottomSheetState.hide()
        }
    })

    LaunchedEffect(key1 = modalBottomSheetState.isVisible, block = {
        if(!modalBottomSheetState.isVisible){
            journalDetailsSheetType = null
        }
    })

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {

            val title = if(journalSubjects.itemCount > 0)
                journalSubjects[pagerState.currentPage]?.subject?.subjectTitle
                    ?: stringResource(id = R.string.journal)
            else
                stringResource(id = R.string.journal)

            TopBarBack(
                title = title,
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                modifier = Modifier.clickable {
                    journalDetailsSheetType = JournalDetailsSheetType.JournalSubjectDetails
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
                ModalBottomSheetLayout(
                    sheetState = modalBottomSheetState,
                    sheetBackgroundColor = PgkTheme.colors.secondaryBackground,
                    sheetShape = PgkTheme.shapes.cornersStyle,
                    sheetContent = {
                        if(journalSubjects.itemCount > 0) {
                            SheetContent(
                                journalSubject = journalSubjects[pagerState.currentPage]!!,
                                journalDetailsSheetType = journalDetailsSheetType,
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
                        getStudentsByGroupId = getStudentsByGroupId
                    )
                }
            }
        }
    )
}

@Composable
private fun SheetContent(
    journalDetailsSheetType: JournalDetailsSheetType?,
    journalSubject: JournalSubject
) {
    when(journalDetailsSheetType) {
        JournalDetailsSheetType.JournalSubjectDetails -> JournalSubjectDetails(
            journalSubject = journalSubject
        )
        JournalDetailsSheetType.JournalSubjectList -> Unit
        else -> EmptyUi()
    }
}

@Composable
private fun JournalSubjectDetails(
    journalSubject: JournalSubject
) {
    Column {
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

        Spacer(modifier = Modifier.height(10.dp))

        Text(
            text = "${journalSubject.hours} (${stringResource(id = R.string.count_hours)})",
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier.padding(5.dp)
        )

        Text(
            text = "${journalSubject.topics.size} (${stringResource(id = R.string.count_topics)})",
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier.padding(5.dp)
        )
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
private fun JournalSubjectsUi(
    journalSubjects: LazyPagingItems<JournalSubject>,
    pagerState: PagerState,
    paddingValues: PaddingValues,
    getStudentsByGroupId: @Composable (groupId:Int) -> LazyPagingItems<Student>
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
                    students = getStudentsByGroupId.invoke(journalSubject.journal.group.id)
                )
            }else{
                EmptyUi()
            }
        }
    }
}
