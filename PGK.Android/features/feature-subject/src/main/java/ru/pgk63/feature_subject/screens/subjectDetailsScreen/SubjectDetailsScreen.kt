package ru.pgk63.feature_subject.screens.subjectDetailsScreen

import android.annotation.SuppressLint
import androidx.compose.runtime.*
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.extension.launchWhenStarted
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

}