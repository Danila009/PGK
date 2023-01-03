package ru.pgk63.feature_main.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_main.screen.MainRoute

object MainDestination : NavigationDestination {
    override val route = "main_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.mainNavigation(
    onGroupScreen: () -> Unit,
    onTechSupportChatScreen: (userRole: UserRole) -> Unit,
    onSettingsScreen: () -> Unit,
    onSpecializationListScreen: () -> Unit,
    onSubjectListScreen: () -> Unit,
    onStudentListScreen: () -> Unit,
    onProfileScreen: () -> Unit
) {
    composable(
        route = MainDestination.route
    ){
        MainRoute(
            onGroupScreen = onGroupScreen,
            onTechSupportChatScreen = onTechSupportChatScreen,
            onSettingsScreen = onSettingsScreen,
            onSpecializationListScreen = onSpecializationListScreen,
            onSubjectListScreen = onSubjectListScreen,
            onStudentListScreen = onStudentListScreen,
            onProfileScreen = onProfileScreen
        )
    }
}