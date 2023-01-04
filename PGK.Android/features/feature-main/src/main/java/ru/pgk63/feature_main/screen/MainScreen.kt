package ru.pgk63.feature_main.screen

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.Notifications
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_ui.icon.ResIcons
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingTitle
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingToolbar
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingToolbarScrollBehavior
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_main.screen.enums.DrawerContent
import ru.pgk63.feature_main.viewModel.MainViewModel

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun MainRoute(
    viewModel: MainViewModel = hiltViewModel(),
    onGroupScreen: () -> Unit,
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit,
    onDepartmentListScreen: () -> Unit,
) {
    var userResult by remember { mutableStateOf<Result<User>>(Result.Loading()) }
    var userRole by remember { mutableStateOf<UserRole?>(null) }

    viewModel.responseUserNetwork.onEach { result ->
        userResult = result
    }.launchWhenStarted()

    viewModel.userLocal.onEach { user ->
        userRole = user?.userRole
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getUserNetwork()
    })

    MainScreen(
        userResult = userResult,
        userRole = userRole,
        onGroupScreen = onGroupScreen,
        onTechSupportChatScreen = onTechSupportChatScreen,
        onSettingsScreen = onSettingsScreen,
        onSpecializationListScreen = onSpecializationListScreen,
        onSubjectListScreen = onSubjectListScreen,
        onStudentListScreen = onStudentListScreen,
        onProfileScreen = onProfileScreen,
        onDepartmentListScreen = onDepartmentListScreen,
        updateDarkMode = {
            viewModel.updateDarkMode()
        }
    )
}


@Composable
private fun MainScreen(
    userResult: Result<User>,
    userRole: UserRole?,
    updateDarkMode: () -> Unit = {},
    onGroupScreen: () -> Unit = {},
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit,
    onDepartmentListScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()
    val scope = rememberCoroutineScope()
    val scaffoldState = rememberScaffoldState()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        scaffoldState = scaffoldState,
        topBar = {
            TopBar(
                scrollBehavior = scrollBehavior,
                onClickIconMenu = {
                    scope.launch {
                        scaffoldState.drawerState.open()
                    }
                }
            )
        },
        drawerShape = PgkTheme.shapes.cornersStyle,
        drawerBackgroundColor = PgkTheme.colors.drawerBackground,
        drawerScrimColor = PgkTheme.colors.secondaryBackground,
        drawerContent = {
            DrawerContentUi(
                userResult = userResult,
                userRole = userRole,
                updateDarkMode = updateDarkMode,
                onGroupScreen = onGroupScreen,
                onTechSupportChatScreen = onTechSupportChatScreen,
                onSettingsScreen = onSettingsScreen,
                onSpecializationListScreen = onSpecializationListScreen,
                onSubjectListScreen = onSubjectListScreen,
                onStudentListScreen = onStudentListScreen,
                onProfileScreen = onProfileScreen,
                onDepartmentListScreen = onDepartmentListScreen
            )
        },
        content = { paddingValues ->
            LazyColumn(
                modifier = Modifier.fillMaxSize()
            ) {

                item {
                    Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                }
            }
        }
    )
}

@Composable
private fun TopBar(
    scrollBehavior: CollapsingToolbarScrollBehavior,
    onClickIconMenu:() -> Unit
) {
    CollapsingToolbar(
        collapsingTitle = CollapsingTitle.large(titleText = "Доброе утро"),
        scrollBehavior = scrollBehavior,
        navigationIcon = {
            IconButton(onClick = onClickIconMenu) {
                Icon(
                    imageVector = Icons.Default.Menu,
                    contentDescription = "menu",
                    tint = PgkTheme.colors.primaryText
                )
            }
        },
        actions = {
            IconButton(
                modifier = Modifier.padding(5.dp),
                onClick = {

                }
            ) {
                Icon(
                    imageVector = Icons.Default.Search,
                    contentDescription = "search",
                    tint = PgkTheme.colors.primaryText
                )
            }

            IconButton(
                modifier = Modifier.padding(5.dp),
                onClick = {

                }
            ) {
                Icon(
                    imageVector = Icons.Default.Notifications,
                    contentDescription = "notifications",
                    tint = PgkTheme.colors.primaryText
                )
            }
        }
    )
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun DrawerContentUi(
    userResult: Result<User>,
    userRole: UserRole?,
    updateDarkMode: () -> Unit = {},
    onGroupScreen: () -> Unit = {},
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit,
    onDepartmentListScreen: () -> Unit
) {
    LazyColumn {
        item {
            TopAppBar(
                backgroundColor = PgkTheme.colors.primaryBackground,
                title = {
                    Column {
                        userResult.data?.let { user ->
                            Text(
                                text = "${user.firstName} ${user.lastName}",
                                color = PgkTheme.colors.primaryText,
                                fontFamily = PgkTheme.fontFamily.fontFamily,
                                style = PgkTheme.typography.toolbar
                            )
                        }

                        userRole?.let {
                            Text(
                                text = stringResource(id = userRole.nameId),
                                color = PgkTheme.colors.primaryText,
                                fontFamily = PgkTheme.fontFamily.fontFamily,
                                style = PgkTheme.typography.caption
                            )
                        }
                    }
                },
                actions = {
                    IconButton(
                        modifier = Modifier.padding(5.dp),
                        onClick = updateDarkMode
                    ) {
                        Icon(
                            modifier = Modifier.size(24.dp),
                            painter = painterResource(id = ResIcons.nightMode),
                            contentDescription = "dark mode",
                            tint = PgkTheme.colors.primaryText
                        )
                    }
                }
            )
            Divider(color = PgkTheme.colors.secondaryBackground)
        }

        item {
            DrawerContent.values().forEach { drawerContent ->
                Card(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(5.dp),
                    backgroundColor = Color.Transparent,
                    onClick = {
                        when (drawerContent) {
                            DrawerContent.PROFILE -> onProfileScreen()
                            DrawerContent.STUDENTS -> onStudentListScreen()
                            DrawerContent.GUIDE -> Unit
                            DrawerContent.SPECIALTIES -> onSpecializationListScreen()
                            DrawerContent.DEPARTMENS -> onDepartmentListScreen()
                            DrawerContent.SUBJECTS -> onSubjectListScreen()
                            DrawerContent.GROUPS -> onGroupScreen()
                            DrawerContent.JOURNAL -> Unit
                            DrawerContent.RAPORTICHKA -> Unit
                            DrawerContent.SETTINGS -> onSettingsScreen()
                            DrawerContent.HELP -> userRole?.let { onTechSupportChatScreen(it) }
                        }
                    }
                ) {
                    Row(
                        verticalAlignment = Alignment.CenterVertically
                    ) {
                        Spacer(modifier = Modifier.width(30.dp))

                        Icon(
                            painter = painterResource(id = drawerContent.iconId),
                            contentDescription = null,
                            modifier = Modifier.size(24.dp),
                            tint = PgkTheme.colors.secondaryText
                        )

                        Spacer(modifier = Modifier.width(20.dp))

                        Text(
                            text = stringResource(id = drawerContent.nameId),
                            color = PgkTheme.colors.primaryText,
                            fontFamily = PgkTheme.fontFamily.fontFamily,
                            style = PgkTheme.typography.body
                        )
                    }
                }
            }
        }
    }
}
















