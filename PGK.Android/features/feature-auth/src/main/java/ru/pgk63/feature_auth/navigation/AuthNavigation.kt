package ru.pgk63.feature_auth.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_auth.screens.auth.AuthRoute
import ru.pgk63.feature_auth.screens.forgotPassword.ForgotPasswordRoute

object AuthDestination : NavigationDestination {
    override val route = "auth_screen"
}

object ForgotPasswordDestination : NavigationDestination {
    override val route = "forgot_password_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.authNavigation(
    onBackScreen: () -> Unit,
    onForgotPasswordScreen: () -> Unit
) {
    composable(
        route = AuthDestination.route
    ){
        AuthRoute(
            onForgotPasswordScreen = onForgotPasswordScreen
        )
    }

    composable(ForgotPasswordDestination.route){
        ForgotPasswordRoute(
            onBackScreen = onBackScreen
        )
    }
}