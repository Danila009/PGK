package ru.pgk63.feature_tech_support.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_tech_support.screen.chatScreen.ChatRoute

object TechSupportChatDestination : NavigationDestination {
    override val route = "tech_support_chat_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.techSupportNavigation(
    onBackScreen: () -> Unit
) {
    composable(
        route = TechSupportChatDestination.route
    ){
        ChatRoute(onBackScreen = onBackScreen)
    }
}