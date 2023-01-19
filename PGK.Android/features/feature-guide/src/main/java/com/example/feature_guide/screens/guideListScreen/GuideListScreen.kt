package com.example.feature_guide.screens.guideListScreen

import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyGridScope
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
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import com.example.feature_guide.screens.guideListScreen.viewModel.GuideListViewModel
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.director.model.Director
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.ImageCoil
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.core_ui.view.shimmer.VerticalListItemShimmer

@Composable
internal fun GuideListRoute(
    viewModel: GuideListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onDirectorDetailsScreen: (directorId: Int) -> Unit,
    onDepartmentHeadDetailsScreen: (departmentHeadId: Int) -> Unit,
    onTeacherDetailsScreen: (teacherId: Int) -> Unit
) {
    val departmentHeadList = viewModel.responseDepartmentHeadList.collectAsLazyPagingItems()
    val directorList = viewModel.responseDirectorList.collectAsLazyPagingItems()
    val teacherList = viewModel.responseTeacherList.collectAsLazyPagingItems()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getDirectorsList()
        viewModel.getDepartmentHeadList()
        viewModel.getTeacherList()
    })

    GuideListScreen(
        directorList = directorList,
        departmentHeadList = departmentHeadList,
        teacherList = teacherList,
        onBackScreen = onBackScreen,
        onDirectorDetailsScreen = onDirectorDetailsScreen,
        onDepartmentHeadDetailsScreen = onDepartmentHeadDetailsScreen,
        onTeacherDetailsScreen = onTeacherDetailsScreen
    )
}

@Composable
private fun GuideListScreen(
    directorList: LazyPagingItems<Director>,
    departmentHeadList: LazyPagingItems<DepartmentHead>,
    teacherList: LazyPagingItems<Teacher>,
    onBackScreen: () -> Unit,
    onDirectorDetailsScreen: (directorId: Int) -> Unit,
    onDepartmentHeadDetailsScreen: (departmentHeadId: Int) -> Unit,
    onTeacherDetailsScreen: (teacherId: Int) -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.guide),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->

        LazyVerticalGrid(
            columns = GridCells.Fixed(2),
            contentPadding = paddingValues
        ){
            guideListUi(
                content = directorList,
                getName = { it.fio() },
                getPost = { stringResource(id = ru.pgk63.core_common.R.string.director) },
                getPhotoUrl = { it.photoUrl },
                onClickItem = { onDirectorDetailsScreen(it.id) }
            )

            guideListUi(
                content = departmentHeadList,
                getName = { it.fio() },
                getPost = { stringResource(id = ru.pgk63.core_common.R.string.department_head) },
                getPhotoUrl = { it.photoUrl },
                onClickItem = { onDepartmentHeadDetailsScreen(it.id) }
            )

            guideListUi(
                content = teacherList,
                getName = { it.fio() },
                getPost = { stringResource(id = ru.pgk63.core_common.R.string.teacher) },
                getPhotoUrl = { it.photoUrl },
                onClickItem = { onTeacherDetailsScreen(it.id) }
            )
        }
    }
}

private fun<T : Any> LazyGridScope.guideListUi(
    content: LazyPagingItems<T>,
    getName: (T) -> String,
    getPost: @Composable () -> String,
    getPhotoUrl: (T) -> String?,
    onClickItem: (T) -> Unit
) {
    if (content.loadState.append is LoadState.Loading){
        item {
            VerticalListItemShimmer()
        }
    }

    if (
        content.loadState.refresh is LoadState.Loading
    ){
        items(20) {
            VerticalListItemShimmer()
        }
    }

    items(content){ contentItem ->
        if(contentItem != null){
            GuideItemUi(
                name = getName.invoke(contentItem),
                post = getPost.invoke(),
                photoUrl = getPhotoUrl(contentItem),
                onClick = { onClickItem(contentItem) }
            )
        }
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun GuideItemUi(
    name:String,
    post:String,
    photoUrl: String?,
    onClick: () -> Unit
) {
    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Card(
        modifier = Modifier.padding(5.dp),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        shape = PgkTheme.shapes.cornersStyle,
        onClick = onClick
    ) {
        Column(
            modifier = Modifier.padding(10.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            if(photoUrl != null) {
                ImageCoil(
                    url = photoUrl,
                    modifier = Modifier
                        .width((screenWidthDp / 2).dp)
                        .height((screenHeightDp / 4.3).dp)
                )
            }else {
                Image(
                    painter = painterResource(id = R.drawable.profile_photo),
                    contentDescription = null,
                    modifier = Modifier
                        .width((screenWidthDp / 2).dp)
                        .height((screenHeightDp / 4.3).dp)
                )
            }

            Text(
                text = name,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp),
                textAlign = TextAlign.Center
            )

            Text(
                text = post,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.caption,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp),
                textAlign = TextAlign.Center
            )
        }
    }
}