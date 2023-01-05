package ru.pgk63.feature_subject.screens.subjectDetailsScreen

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.material.Scaffold
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.LoadingUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_subject.screens.subjectDetailsScreen.viewModel.SubjectDetailsViewModel

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun SubjectDetailsRoute(
    viewModel: SubjectDetailsViewModel = hiltViewModel(),
    subjectId: Int,
    onBackScreen: () -> Unit,
) {
    var subjectResult by remember { mutableStateOf<Result<Subject>>(Result.Loading()) }

    viewModel.responseSubject.onEach { result ->
        subjectResult = result
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getSubjectById(id = subjectId)
    })

    SubjectDetailsScreen(
        subjectResult = subjectResult,
        onBackScreen = onBackScreen
    )
}

@Composable
private fun SubjectDetailsScreen(
    subjectResult: Result<Subject>,
    onBackScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = subjectResult.data?.subjectTitle ?: "",
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        when(subjectResult){
            is Result.Error -> ErrorUi(message = subjectResult.message)
            is Result.Loading -> LoadingUi()
            is Result.Success -> SubjectSuccess(
                paddingValues = paddingValues,
            )
        }
    }
}

@Composable
private fun SubjectSuccess(
    paddingValues: PaddingValues,
) {
    LazyVerticalGrid(
        columns = GridCells.Fixed(2),
        modifier = Modifier.padding(paddingValues)
    ){
        item {

        }
    }
}
