package ru.pgk63.feature_group.screens.groupDetailsScreen

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Scaffold
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.Error
import ru.pgk63.core_ui.view.LoadingList
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.feature_group.screens.groupDetailsScreen.viewModel.GroupDetailsViewModel

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun GroupDetailsRoute(
    viewModel: GroupDetailsViewModel = hiltViewModel(),
    groupId: Int,
    onBackScreen: () -> Unit
) {
    var groupResult by remember { mutableStateOf<Result<Group>>(Result.Loading()) }

    viewModel.responseGroup.onEach { result ->
        groupResult = result
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getGroupById(id = groupId)
    })

    GroupDetailsScreen(
        groupResult = groupResult,
        onBackScreen = onBackScreen
    )
}

@Composable
private fun GroupDetailsScreen(
    groupResult: Result<Group>,
    onBackScreen: () -> Unit
) {
    Scaffold(
        topBar = {

            val title = if(groupResult is Result.Success){
                val group = groupResult.data!!
                "${group.speciality.nameAbbreviation}-${group.course}${group.number}"
            }else { "" }

            TopBarBack(
                title = title,
                onBackClick = onBackScreen
            )
        },
        content = { paddingValues ->
            Surface(
                modifier = Modifier.fillMaxSize(),
                color = PgkTheme.colors.primaryBackground
            ) {
                when(groupResult){
                    is Result.Error -> Error(message = groupResult.message)
                    is Result.Loading -> LoadingList()
                    is Result.Success -> LazyColumn {

                        item {
                            GroupDetails(group = groupResult.data!!)
                        }

                        item {
                            Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                        }
                    }
                }
            }
        }
    )
}

@Composable
private fun GroupDetails(group: Group) {
    Text(
        text = group.toString(),
        color = PgkTheme.colors.primaryText
    )
}