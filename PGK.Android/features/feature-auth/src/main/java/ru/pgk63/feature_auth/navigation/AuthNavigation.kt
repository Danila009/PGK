package ru.pgk63.feature_auth.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_auth.screens.auth.AuthRoute

object AuthDestination : NavigationDestination {
    override val route = "auth_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.authNavigation() {
    composable(
        route = AuthDestination.route
    ){
        AuthRoute()
    }
}