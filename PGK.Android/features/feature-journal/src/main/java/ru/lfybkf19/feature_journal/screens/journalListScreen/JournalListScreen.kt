package ru.lfybkf19.feature_journal.screens.journalListScreen

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.material.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import ru.lfybkf19.feature_journal.screens.journalListScreen.viewModel.JournalListViewModel
import ru.lfybkf19.feature_journal.view.JournalUi
import ru.pgk63.core_common.api.journal.model.Journal
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior

@Composable
internal fun JournalListRoute(
    viewModel: JournalListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onJournalDetailsScreen: (journalId: Int) -> Unit
) {
    val journals = viewModel.responseJournalList.collectAsLazyPagingItems()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getJournalList()
    })

    JournalListScreen(
        journals = journals,
        onBackScreen = onBackScreen,
        onJournalDetailsScreen = onJournalDetailsScreen
    )
}

@Composable
private fun JournalListScreen(
    journals: LazyPagingItems<Journal>,
    onBackScreen: () -> Unit,
    onJournalDetailsScreen: (journalId: Int) -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.journals),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        },
        content = { paddingValues ->
            if (
                journals.itemCount <= 0 && journals.loadState.refresh !is LoadState.Loading
            ){
                EmptyUi()
            }else if(journals.loadState.refresh is LoadState.Error) {
                ErrorUi()
            }else{
                JournalList(
                    journals = journals,
                    paddingValues = paddingValues,
                    onJournalDetailsScreen = onJournalDetailsScreen
                )
            }
        }
    )
}

@Composable
private fun JournalList(
    journals: LazyPagingItems<Journal>,
    paddingValues: PaddingValues,
    onJournalDetailsScreen: (journalId: Int) -> Unit
) {
   LazyVerticalGrid(
       columns = GridCells.Fixed(2),
       contentPadding = paddingValues
   ){
       items(journals){ journal ->
           if(journal != null) {
               Box {
                   JournalUi(
                       modifier = Modifier
                           .padding(10.dp)
                           .align(Alignment.Center),
                       journal = journal,
                       onClick = {
                           onJournalDetailsScreen(journal.id)
                       }
                   )
               }
           }
       }
   }
}