package ru.pgk63.feature_subject.screens.subjectListScreen

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.material.Card
import androidx.compose.material.ExperimentalMaterialApi
import androidx.compose.material.Scaffold
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_subject.screens.subjectListScreen.viewModel.SubjectListViewModel

@Composable
internal fun SubjectListRoute(
    viewModel: SubjectListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onSubjectDetailsScreen: (subjectId: Int) -> Unit,
) {

    val subjects = viewModel.responseSubject.collectAsLazyPagingItems()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getSubjectAll()
    })

    SubjectListScreen(
        subjects = subjects,
        onBackScreen = onBackScreen,
        onSubjectDetailsScreen = onSubjectDetailsScreen
    )
}

@Composable
private fun SubjectListScreen(
    subjects: LazyPagingItems<Subject>,
    onBackScreen: () -> Unit,
    onSubjectDetailsScreen: (subjectId: Int) -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.subjects),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        LazyVerticalGrid(
            columns = GridCells.Fixed(2),
            modifier = Modifier.fillMaxSize()
        ) {

            items(subjects){ subject ->
                subject?.let {
                    SubjectCard(
                        subject = subject,
                        onSubjectDetailsScreen = onSubjectDetailsScreen
                    )
                }
            }

            item {
                Spacer(modifier = Modifier
                    .height(paddingValues.calculateBottomPadding()))
            }
        }
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun SubjectCard(subject: Subject, onSubjectDetailsScreen: (subjectId: Int) -> Unit) {
    Card(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        modifier = Modifier.padding(5.dp),
        onClick = { onSubjectDetailsScreen(subject.id) }
    ) {
        Box {
            Text(
                text = subject.subjectTitle,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                textAlign = TextAlign.Center,
                modifier = Modifier
                    .padding(10.dp)
                    .align(Alignment.Center)
            )
        }
    }
}