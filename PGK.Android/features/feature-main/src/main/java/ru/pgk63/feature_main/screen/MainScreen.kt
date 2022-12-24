package ru.pgk63.feature_main.screen

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.Notifications
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.Composable
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.launch
import ru.pgk63.core_ui.icon.ResIcons
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.feature_main.screen.enums.DrawerContent
import ru.pgk63.feature_main.viewModel.MainViewModel

@Composable
internal fun MainRoute(
    viewModel: MainViewModel = hiltViewModel(),
    onGroupScreen: () -> Unit,
    onTechSupportChatScreen: () -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
) {
    MainScreen(
        onGroupScreen = onGroupScreen,
        onTechSupportChatScreen = onTechSupportChatScreen,
        onSettingsScreen = onSettingsScreen,
        onSpecializationListScreen = onSpecializationListScreen,
        onSubjectListScreen = onSubjectListScreen,
        onStudentListScreen = onStudentListScreen,
        updateDarkMode = {
            viewModel.updateDarkMode()
        }
    )
}


@Composable
private fun MainScreen(
    updateDarkMode: () -> Unit = {},
    onGroupScreen: () -> Unit = {},
    onTechSupportChatScreen: () -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit
) {
    val scope = rememberCoroutineScope()
    val scaffoldState = rememberScaffoldState()

    Scaffold(
        scaffoldState = scaffoldState,
        topBar = {
            TopBar(
                onClickMenu = {
                    scope.launch {
                        scaffoldState.drawerState.open()
                    }
                }
            )
        },
        drawerShape = PgkTheme.shapes.cornersStyle,
        drawerBackgroundColor = PgkTheme.colors.drawerBackground,
        drawerContent = {
            DrawerContentUi(
                updateDarkMode = updateDarkMode,
                onGroupScreen = onGroupScreen,
                onTechSupportChatScreen = onTechSupportChatScreen,
                onSettingsScreen = onSettingsScreen,
                onSpecializationListScreen = onSpecializationListScreen,
                onSubjectListScreen = onSubjectListScreen,
                onStudentListScreen = onStudentListScreen
            )
        },
        content = { paddingValues ->
            Surface(
                modifier = Modifier.fillMaxSize(),
                color = PgkTheme.colors.primaryBackground
            ) {
                LazyColumn(
                    modifier = Modifier.fillMaxSize()
                ) {

                    item {
                        Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                    }
                }
            }
        }
    )
}

@Composable
private fun TopBar(
    onClickMenu:() -> Unit
) {
    TopAppBar(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        title = {
            Text(
                text = "Доброе утро",
                color = PgkTheme.colors.primaryText,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                style = PgkTheme.typography.toolbar
            )
        },
        navigationIcon = {
            IconButton(onClick = onClickMenu) {
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

@Composable
private fun DrawerContentUi(
    updateDarkMode: () -> Unit = {},
    onGroupScreen: () -> Unit = {},
    onTechSupportChatScreen: () -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit
) {
    LazyColumn {
        item {
            TopAppBar(
                backgroundColor = PgkTheme.colors.secondaryBackground,
                title = {
                    Column {
                        Text(
                            text = "Данила ИСП-239",
                            color = PgkTheme.colors.primaryText,
                            fontFamily = PgkTheme.fontFamily.fontFamily,
                            style = PgkTheme.typography.toolbar
                        )

                        Text(
                            text = "Студент",
                            color = PgkTheme.colors.primaryText,
                            fontFamily = PgkTheme.fontFamily.fontFamily,
                            style = PgkTheme.typography.caption
                        )
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
        }

        item {
            DrawerContent.values().forEach { drawerContent ->
                Row(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(15.dp)
                        .clickable {
                            when(drawerContent){
                                DrawerContent.PROFILE -> Unit
                                DrawerContent.SCHEDULE -> Unit
                                DrawerContent.STUDENTS -> onStudentListScreen()
                                DrawerContent.GUIDE -> Unit
                                DrawerContent.SPECIALTIES -> onSpecializationListScreen()
                                DrawerContent.DEPARTMENS -> Unit
                                DrawerContent.SUBJECTS -> onSubjectListScreen()
                                DrawerContent.GROUPS -> onGroupScreen()
                                DrawerContent.JOURNAL -> Unit
                                DrawerContent.RAPORTICHKA -> Unit
                                DrawerContent.SETTINGS -> onSettingsScreen()
                                DrawerContent.HELP -> onTechSupportChatScreen()
                            }
                        },
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
















