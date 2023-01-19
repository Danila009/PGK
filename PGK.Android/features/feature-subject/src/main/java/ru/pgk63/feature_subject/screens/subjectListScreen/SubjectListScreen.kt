package ru.pgk63.feature_subject.screens.subjectListScreen

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TextFieldSearch
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_subject.screens.subjectListScreen.viewModel.SubjectListViewModel

@Composable
internal fun SubjectListRoute(
    viewModel: SubjectListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onSubjectDetailsScreen: (subjectId: Int) -> Unit,
) {
    var searchText by remember { mutableStateOf("") }

    val subjects = viewModel.responseSubject.collectAsLazyPagingItems()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getSubjectAll(
            search = searchText.ifEmpty { null }
        )
    })

    SubjectListScreen(
        subjects = subjects,
        searchText = searchText,
        onBackScreen = onBackScreen,
        onSubjectDetailsScreen = onSubjectDetailsScreen,
        onSearchTextChange = { searchText = it }
    )
}

@Composable
private fun SubjectListScreen(
    subjects: LazyPagingItems<Subject>,
    searchText: String,
    onSearchTextChange: (String) -> Unit,
    onBackScreen: () -> Unit,
    onSubjectDetailsScreen: (subjectId: Int) -> Unit
) {
    val scope = rememberCoroutineScope()
    val scrollBehavior = rememberToolbarScrollBehavior()

    val searchTextFieldFocusRequester = remember { FocusRequester() }
    var searchTextFieldVisible by remember { mutableStateOf(false) }

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.subjects),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                actions = {
                    AnimatedVisibility(visible = !searchTextFieldVisible) {
                        Row(verticalAlignment = Alignment.CenterVertically) {

                            IconButton(onClick = {
                                scope.launch {
                                    searchTextFieldVisible = true
                                    delay(100)
                                    searchTextFieldFocusRequester.requestFocus()
                                }
                            }) {
                                Icon(
                                    imageVector = Icons.Default.Search,
                                    contentDescription = null,
                                    tint = PgkTheme.colors.primaryText
                                )
                            }

                            Spacer(modifier = Modifier.height(5.dp))
                        }
                    }

                    AnimatedVisibility(visible = searchTextFieldVisible) {
                        IconButton(onClick = { /*TODO*/ }) {
                            Icon(
                                imageVector = Icons.Default.Add,
                                contentDescription = null,
                                tint = PgkTheme.colors.primaryText
                            )
                        }
                    }


                    AnimatedVisibility(visible = searchTextFieldVisible) {
                        TextFieldSearch(
                            text = searchText,
                            onTextChanged = onSearchTextChange,
                            modifier = Modifier.focusRequester(searchTextFieldFocusRequester),
                            onClose = {
                                searchTextFieldVisible = false
                                onSearchTextChange("")
                            }
                        )
                    }
                }
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