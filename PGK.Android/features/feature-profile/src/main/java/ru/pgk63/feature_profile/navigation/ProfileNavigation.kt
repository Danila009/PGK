package ru.pgk63.feature_profile.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_profile.screens.profileScreen.ProfileRoute

object ProfileDestination : NavigationDestination {
    override val route = "profile_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.profileNavigation(
    onBackScreen: () -> Unit
) {
    composable(
        route = ProfileDestination.route
    ){
        ProfileRoute(
            onBackScreen = onBackScreen
        )
    }
}