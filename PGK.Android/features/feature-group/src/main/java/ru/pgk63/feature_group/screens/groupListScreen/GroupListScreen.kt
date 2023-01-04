
package ru.pgk63.feature_group.screens.groupListScreen

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.GridItemSpan
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.core_ui.view.shimmer.VerticalListItemShimmer
import ru.pgk63.feature_group.screens.groupListScreen.viewModel.GroupListViewModel

@Composable
internal fun GroupListRoute(
    viewModel: GroupListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onGroupDetailsScreen: (groupId: Int) -> Unit
) {
    val groups = viewModel.groups.collectAsLazyPagingItems()

    GroupListScreen(
        groups = groups,
        onBackScreen = onBackScreen,
        onGroupDetailsScreen = onGroupDetailsScreen
    )
}

@Composable
private fun GroupListScreen(
    groups: LazyPagingItems<Group>,
    onBackScreen: () -> Unit,
    onGroupDetailsScreen: (groupId: Int) -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.groups),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        },
        content = { paddingValues ->
            if (
                groups.itemCount <= 0 && groups.loadState.refresh !is LoadState.Loading
            ){
                EmptyUi()
            }else if(groups.loadState.refresh is LoadState.Error) {
                ErrorUi()
            }else{
                LazyVerticalGrid(
                    columns = GridCells.Fixed(2),
                    modifier = Modifier.fillMaxSize()
                ){
                    items(groups){ group ->
                        GroupListItem(
                            group = group ?: return@items,
                            onGroupDetailsScreen = onGroupDetailsScreen
                        )
                    }

                    if (groups.loadState.append is LoadState.Loading){
                        item {
                            VerticalListItemShimmer()
                        }
                    }

                    if (
                        groups.loadState.refresh is LoadState.Loading
                    ){
                        items(20) {
                            VerticalListItemShimmer()
                        }
                    }

                    item(
                        span = { GridItemSpan(maxCurrentLineSpan) }
                    ) {
                        Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                    }
                }
            }
        }
    )
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun GroupListItem(group: Group, onGroupDetailsScreen: (groupId: Int) -> Unit) {
    Card(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        modifier = Modifier.padding(5.dp),
        onClick = { onGroupDetailsScreen(group.id) }
    ) {
        Column(
            modifier = Modifier.padding(5.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            Text(
                text = "${group.speciality.nameAbbreviation}-${group.course}${group.number}",
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp)
            )

            Text(
                text = "${group.classroomTeacher.lastName} " +
                        "${group.classroomTeacher.firstName[0]}" +
                        ".${group.classroomTeacher.middleName?.getOrNull(0) ?: ""}",
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.caption,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp)
            )
        }
    }
}