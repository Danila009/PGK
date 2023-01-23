package com.example.feature_guide.screens.guideListScreen

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.staggeredgrid.LazyStaggeredGridScope
import androidx.compose.foundation.lazy.staggeredgrid.LazyVerticalStaggeredGrid
import androidx.compose.foundation.lazy.staggeredgrid.StaggeredGridCells
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
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
import com.example.feature_guide.screens.guideListScreen.model.GuideListMainMenu
import com.example.feature_guide.screens.guideListScreen.viewModel.GuideListViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.director.model.Director
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.ImageCoil
import ru.pgk63.core_ui.view.TextFieldSearch
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.core_ui.view.shimmer.VerticalListItemShimmer

@Composable
internal fun GuideListRoute(
    viewModel: GuideListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onDirectorDetailsScreen: (directorId: Int) -> Unit,
    onDepartmentHeadDetailsScreen: (departmentHeadId: Int) -> Unit,
    onTeacherDetailsScreen: (teacherId: Int) -> Unit,
    onRegistrationScreen: (userRole: UserRole) -> Unit
) {
    val departmentHeadList = viewModel.responseDepartmentHeadList.collectAsLazyPagingItems()
    val directorList = viewModel.responseDirectorList.collectAsLazyPagingItems()
    val teacherList = viewModel.responseTeacherList.collectAsLazyPagingItems()

    var searchText by remember { mutableStateOf("") }

    LaunchedEffect(key1 = searchText, block = {
        viewModel.getDirectorsList(search = searchText.ifEmpty { null })
        viewModel.getDepartmentHeadList(search = searchText.ifEmpty { null })
        viewModel.getTeacherList(search = searchText.ifEmpty { null })
    })

    GuideListScreen(
        directorList = directorList,
        departmentHeadList = departmentHeadList,
        teacherList = teacherList,
        searchText = searchText,
        onBackScreen = onBackScreen,
        onDirectorDetailsScreen = onDirectorDetailsScreen,
        onDepartmentHeadDetailsScreen = onDepartmentHeadDetailsScreen,
        onTeacherDetailsScreen = onTeacherDetailsScreen,
        onRegistrationScreen = onRegistrationScreen,
        onSearchTextChange = { searchText = it }
    )
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
private fun GuideListScreen(
    directorList: LazyPagingItems<Director>,
    departmentHeadList: LazyPagingItems<DepartmentHead>,
    teacherList: LazyPagingItems<Teacher>,
    searchText: String,
    onSearchTextChange: (String) -> Unit,
    onBackScreen: () -> Unit,
    onDirectorDetailsScreen: (directorId: Int) -> Unit,
    onDepartmentHeadDetailsScreen: (departmentHeadId: Int) -> Unit,
    onTeacherDetailsScreen: (teacherId: Int) -> Unit,
    onRegistrationScreen: (userRole: UserRole) -> Unit
) {
    val scope = rememberCoroutineScope()
    val scrollBehavior = rememberToolbarScrollBehavior()

    var mainMenuShow by remember { mutableStateOf(false) }

    val searchTextFieldFocusRequester = remember { FocusRequester() }
    var searchTextFieldVisible by remember { mutableStateOf(false) }

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.guide),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                actions = {
                    AnimatedVisibility(visible = !searchTextFieldVisible) {
                        Row {
                            IconButton(onClick = {
                                scope.launch {
                                    mainMenuShow = false
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

                            Spacer(modifier = Modifier.width(5.dp))

                            Column {
                                IconButton(onClick = { mainMenuShow = true }) {
                                    Icon(
                                        imageVector = Icons.Default.Menu,
                                        contentDescription = null,
                                        tint = PgkTheme.colors.primaryText
                                    )
                                }

                                MainMenu(
                                    show = mainMenuShow,
                                    onDismissRequest = { mainMenuShow = false },
                                    onClick = { menu ->
                                        when(menu){
                                            GuideListMainMenu.REGISTRATION_DIRECTOR ->
                                                onRegistrationScreen(UserRole.DIRECTOR)
                                            GuideListMainMenu.REGISTRATION_DEPARTMENT_HEAD ->
                                                onRegistrationScreen(UserRole.DEPARTMENT_HEAD)
                                            GuideListMainMenu.REGISTRATION_TEACHER ->
                                                onRegistrationScreen(UserRole.TEACHER)
                                        }
                                    }
                                )
                            }
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

        LazyVerticalStaggeredGrid(
            columns = StaggeredGridCells.Fixed(2),
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

@Composable
private fun MainMenu(
    show: Boolean,
    onDismissRequest: () -> Unit,
    onClick: (GuideListMainMenu) -> Unit
) {
    DropdownMenu(
        expanded = show,
        onDismissRequest = onDismissRequest,
        modifier = Modifier.background(PgkTheme.colors.secondaryBackground)
    ) {
        GuideListMainMenu.values().forEach { menu ->
            MainMenuItem(
                menu = menu
            ) { onClick(menu) }
        }
    }
}

@Composable
private fun MainMenuItem(
    menu: GuideListMainMenu,
    onClick: () -> Unit
) {
    DropdownMenuItem(
        modifier = Modifier.background(PgkTheme.colors.secondaryBackground),
        onClick = onClick
    ) {
        Icon(
            imageVector = menu.icon,
            contentDescription = null,
            tint = PgkTheme.colors.primaryText
        )

        Spacer(modifier = Modifier.width(5.dp))

        Text(
            text = stringResource(id = menu.textId),
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.caption,
            fontFamily = PgkTheme.fontFamily.fontFamily
        )
    }
}

@OptIn(ExperimentalFoundationApi::class)
private fun<T : Any> LazyStaggeredGridScope.guideListUi(
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