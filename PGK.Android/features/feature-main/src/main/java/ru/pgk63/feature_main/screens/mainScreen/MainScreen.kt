package ru.pgk63.feature_main.screens.mainScreen

import android.annotation.SuppressLint
import androidx.compose.foundation.isSystemInDarkTheme
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
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import com.google.accompanist.pager.ExperimentalPagerApi
import com.google.accompanist.pager.HorizontalPager
import kotlinx.coroutines.flow.onEach
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.JournalColumn
import ru.pgk63.core_common.api.raportichka.model.Raportichka
import ru.pgk63.core_common.api.user.model.UserDetails
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_common.extension.parseToBaseDateFormat
import ru.pgk63.core_ui.icon.ResIcons
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingTitle
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingToolbar
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingToolbarScrollBehavior
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_main.screens.mainScreen.enums.DrawerContent
import ru.pgk63.feature_main.screens.mainScreen.viewModel.MainViewModel
import ru.pgk63.core_ui.R

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun MainRoute(
    viewModel: MainViewModel = hiltViewModel(),
    onNotificationListScreen: () -> Unit,
    onGroupScreen: () -> Unit,
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit,
    onDepartmentListScreen: () -> Unit,
    onRaportichkaScreen: (userRole: UserRole, userId: Int) -> Unit,
    onJournalScreen: (userRole: UserRole, userId: Int) -> Unit,
    onGuideListScreen: () -> Unit,
    onSearchScreen: () -> Unit,
) {
    var userResult by remember { mutableStateOf<Result<UserDetails>>(Result.Loading()) }
    var userRole by remember { mutableStateOf<UserRole?>(null) }
    var darkMode by remember { mutableStateOf<Boolean?>(null) }

    val raportichkaList = viewModel.responseRaportichkaList.collectAsLazyPagingItems()
    val journalColumnList = viewModel.responseJournalColumnList.collectAsLazyPagingItems()

    viewModel.responseUserNetwork.onEach { result ->
        userResult = result
    }.launchWhenStarted()

    viewModel.userLocal.onEach { user ->
        userRole = user?.userRole
        darkMode = user?.darkMode
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getUserNetwork()
    })

    MainScreen(
        userResult = userResult,
        userRole = userRole,
        darkMode = darkMode ?: isSystemInDarkTheme(),
        onNotificationListScreen = onNotificationListScreen,
        onGroupScreen = onGroupScreen,
        onTechSupportChatScreen = onTechSupportChatScreen,
        onSettingsScreen = onSettingsScreen,
        onSpecializationListScreen = onSpecializationListScreen,
        onSubjectListScreen = onSubjectListScreen,
        onStudentListScreen = onStudentListScreen,
        onProfileScreen = onProfileScreen,
        onDepartmentListScreen = onDepartmentListScreen,
        onRaportichkaScreen = onRaportichkaScreen,
        onJournalScreen = onJournalScreen,
        onGuideListScreen = onGuideListScreen,
        onSearchScreen = onSearchScreen,
        updateDarkMode = {
            viewModel.updateDarkMode()
        },
        getRaportichkaList = {
            LaunchedEffect(key1 = Unit) {
                viewModel.getRaportichkaList(
                    studentIds = if(
                        (userRole == UserRole.STUDENT || userRole == UserRole.HEADMAN
                                || userRole == UserRole.DEPUTY_HEADMAN) && userResult.data?.id != null)
                        listOf(userResult.data!!.id)
                    else
                        null
                )
            }

            raportichkaList
        },
        getJournalColumnList = {
            LaunchedEffect(key1 = Unit){
                viewModel.getJournalColumnList(
                    studentIds = if(
                        (userRole == UserRole.STUDENT || userRole == UserRole.HEADMAN
                                || userRole == UserRole.DEPUTY_HEADMAN) && userResult.data?.id != null)
                        listOf(userResult.data!!.id)
                    else
                        null
                )
            }

            journalColumnList
        }
    )
}


@Composable
private fun MainScreen(
    userResult: Result<UserDetails>,
    userRole: UserRole?,
    darkMode: Boolean,
    updateDarkMode: () -> Unit = {},
    onNotificationListScreen: () -> Unit,
    onGroupScreen: () -> Unit = {},
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit,
    onDepartmentListScreen: () -> Unit,
    onRaportichkaScreen: (userRole: UserRole, userId: Int) -> Unit,
    onJournalScreen: (userRole: UserRole, userId: Int) -> Unit,
    onGuideListScreen: () -> Unit,
    onSearchScreen: () -> Unit,
    getRaportichkaList: @Composable () -> LazyPagingItems<Raportichka>,
    getJournalColumnList: @Composable () -> LazyPagingItems<JournalColumn>
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
                onNotificationListScreen = onNotificationListScreen,
                onClickIconMenu = {
                    scope.launch {
                        scaffoldState.drawerState.open()
                    }
                },
                onSearchScreen = onSearchScreen
            )
        },
        drawerShape = PgkTheme.shapes.cornersStyle,
        drawerBackgroundColor = PgkTheme.colors.drawerBackground,
        drawerScrimColor = PgkTheme.colors.secondaryBackground,
        drawerContent = {
            DrawerContentUi(
                userResult = userResult,
                userRole = userRole,
                darkMode = darkMode,
                updateDarkMode = updateDarkMode,
                onGroupScreen = onGroupScreen,
                onTechSupportChatScreen = onTechSupportChatScreen,
                onSettingsScreen = onSettingsScreen,
                onSpecializationListScreen = onSpecializationListScreen,
                onSubjectListScreen = onSubjectListScreen,
                onStudentListScreen = onStudentListScreen,
                onProfileScreen = onProfileScreen,
                onDepartmentListScreen = onDepartmentListScreen,
                onRaportichkaScreen = onRaportichkaScreen,
                onJournalScreen = onJournalScreen,
                onGuideListScreen = onGuideListScreen
            )
        },
        content = { paddingValues ->
            when(userResult){
                is Result.Error -> ErrorUi()
                is Result.Loading -> EmptyUi()
                is Result.Success -> MainScreenSuccess(
                    user = userResult.data!!,
                    userRole = userRole,
                    contentPadding = paddingValues,
                    getRaportichkaList = getRaportichkaList,
                    getJournalColumnList = getJournalColumnList
                )
            }
        }
    )
}

@Composable
private fun TopBar(
    scrollBehavior: CollapsingToolbarScrollBehavior,
    onClickIconMenu:() -> Unit,
    onNotificationListScreen: () -> Unit,
    onSearchScreen: () -> Unit
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
                onClick = onSearchScreen
            ) {
                Icon(
                    imageVector = Icons.Default.Search,
                    contentDescription = "search",
                    tint = PgkTheme.colors.primaryText
                )
            }

            IconButton(
                modifier = Modifier.padding(5.dp),
                onClick = onNotificationListScreen
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
    userResult: Result<UserDetails>,
    userRole: UserRole?,
    darkMode: Boolean,
    updateDarkMode: () -> Unit = {},
    onGroupScreen: () -> Unit = {},
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit,
    onDepartmentListScreen: () -> Unit,
    onRaportichkaScreen: (userRole: UserRole, userId: Int) -> Unit,
    onJournalScreen: (userRole: UserRole, userId: Int) -> Unit,
    onGuideListScreen: () -> Unit,
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
                            painter = if(darkMode)
                                painterResource(id = ResIcons.sun)
                            else
                                painterResource(id = ResIcons.nightMode),
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
                    elevation = 0.dp,
                    onClick = {
                        when (drawerContent) {
                            DrawerContent.PROFILE -> onProfileScreen()
                            DrawerContent.STUDENTS -> onStudentListScreen()
                            DrawerContent.GUIDE -> onGuideListScreen()
                            DrawerContent.SPECIALTIES -> onSpecializationListScreen()
                            DrawerContent.DEPARTMENS -> onDepartmentListScreen()
                            DrawerContent.SUBJECTS -> onSubjectListScreen()
                            DrawerContent.GROUPS -> onGroupScreen()
                            DrawerContent.JOURNAL -> {
                                if(userRole != null && userResult.data != null){
                                    onJournalScreen(userRole, userResult.data!!.id)
                                }
                            }
                            DrawerContent.RAPORTICHKA -> {
                                if(userRole != null && userResult.data != null){
                                    onRaportichkaScreen(userRole, userResult.data!!.id)
                                }
                            }
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

@Composable
private fun MainScreenSuccess(
    user: UserDetails,
    userRole: UserRole?,
    contentPadding: PaddingValues,
    getRaportichkaList: @Composable () -> LazyPagingItems<Raportichka>,
    getJournalColumnList: @Composable () -> LazyPagingItems<JournalColumn>
) {
    if(userRole == UserRole.STUDENT || userRole == UserRole.HEADMAN || userRole == UserRole.DEPUTY_HEADMAN){
        LazyColumn(contentPadding = contentPadding) {
            item {
                MainScreenStudent(
                    user = user,
                    getRaportichkaList = getRaportichkaList,
                    getJournalColumnList = getJournalColumnList
                )
            }
        }
    }else {
        EmptyUi()
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
private fun MainScreenStudent(
    user: UserDetails,
    getRaportichkaList: @Composable () -> LazyPagingItems<Raportichka>,
    getJournalColumnList: @Composable () -> LazyPagingItems<JournalColumn>
) {
    val raportichkaList = getRaportichkaList()
    val journalColumnList = getJournalColumnList()

    if(journalColumnList.itemCount > 0){

        Text(
            text = stringResource(id = R.string.journal),
            color = PgkTheme.colors.primaryText,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            style = PgkTheme.typography.heading,
            modifier = Modifier.padding(15.dp)
        )

        HorizontalPager(count = journalColumnList.itemCount) {

            val journalColumn = journalColumnList[it]

            if(journalColumn != null){
                JournalColumnItem(
                    journalColumn = journalColumn
                )
            }
        }
    }

    if(raportichkaList.itemCount > 0){

        Text(
            text = stringResource(id = R.string.raportichka),
            color = PgkTheme.colors.primaryText,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            style = PgkTheme.typography.heading,
            modifier = Modifier.padding(15.dp)
        )

        HorizontalPager(count = raportichkaList.itemCount) { page ->

            val raportichka = raportichkaList[page]

            if(raportichka != null && raportichka.rows.any { it.student.id == user.id }){
                RaportichkaItem(
                    raportichka = raportichka,
                    user = user
                )
            }
        }
    }
}

@Composable
private fun JournalColumnItem(
    modifier: Modifier = Modifier,
    journalColumn: JournalColumn
) {
    val journalSubject = journalColumn.row.journalSubject

    Card(
        modifier = modifier.padding(6.dp),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle
    ) {
        Column {
            Text(
                text = "${journalSubject.subject} (${journalSubject.teacher})",
                color = PgkTheme.colors.primaryText,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                style = PgkTheme.typography.body,
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(5.dp),
                textAlign = TextAlign.Center
            )

            Text(
                text = journalColumn.evaluation.text,
                color = PgkTheme.colors.primaryText,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                style = PgkTheme.typography.body,
                modifier = Modifier
                    .padding(5.dp)
                    .align(Alignment.CenterHorizontally)
            )

            Text(
                text = journalColumn.date.parseToBaseDateFormat(),
                color = PgkTheme.colors.primaryText,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                style = PgkTheme.typography.caption,
                modifier = Modifier
                    .padding(5.dp)
                    .align(Alignment.End)
            )
        }
    }
}

@Composable
private fun RaportichkaItem(
    modifier: Modifier = Modifier,
    raportichka: Raportichka,
    user: UserDetails,
) {
    Card(
        modifier = modifier.padding(6.dp),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle
    ) {
        Column {
            Text(
                text = raportichka.date.parseToBaseDateFormat(),
                color = PgkTheme.colors.primaryText,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                style = PgkTheme.typography.body,
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(5.dp),
                textAlign = TextAlign.Center
            )

            repeat(raportichka.rows.size){ index ->

                val row = raportichka.rows[index]

                if(user.id == row.student.id) {
                    Text(
                        text = "${row.numberLesson}. ${row.subject.subjectTitle} (${row.teacher.fioAbbreviated()})",
                        color = PgkTheme.colors.primaryText,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        style = PgkTheme.typography.caption,
                        modifier = Modifier.padding(5.dp)
                    )
                }
            }
        }
    }
}












