package ru.lfybkf19.feature_journal.screens.journalTopicTableScreen

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.viewModel.JournalTopicTableViewModel
import ru.lfybkf19.feature_journal.view.JournalTopicTable
import ru.pgk63.core_common.api.journal.model.JournalTopic
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior

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

    JournalTopicTableScreen(
        topics = topics,
        maxSubjectHours = maxSubjectHours,
        onBackScreen = onBackScreen
    )
}

@Composable
private fun JournalTopicTableScreen(
    topics: LazyPagingItems<JournalTopic>,
    maxSubjectHours: Int,
    onBackScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.journal_topics),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
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
                TopicList(
                    topics = topics,
                    maxSubjectHours = maxSubjectHours,
                    paddingValues = paddingValues
                )
            }
        }
    )
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
