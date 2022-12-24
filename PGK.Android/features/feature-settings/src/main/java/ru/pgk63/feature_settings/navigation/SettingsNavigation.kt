package ru.pgk63.feature_settings.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_settings.screens.settingsAppearanceScreen.SettingsAppearanceRoute
import ru.pgk63.feature_settings.screens.settingsLanguageScreen.SettingsLanguageScreenRoute
import ru.pgk63.feature_settings.screens.settingsNotificationsScreen.SettingsNotificationsRoute
import ru.pgk63.feature_settings.screens.settingsScreen.SettingsRoute
import ru.pgk63.feature_settings.screens.settingsSecurityScreen.SettingsSecurityRoute

object SettingsDestination : NavigationDestination {
    override val route = "settings_screen"
}

object SettingsSecurityDestination : NavigationDestination {
    override val route = "settings_security_screen"
}

object SettingsNotificationsDestination : NavigationDestination {
    override val route = "settings_notifications_screen"
}

object SettingsLanguageDestination : NavigationDestination {
    override val route = "settings_language_screen"
}

object SettingsAppearanceDestination : NavigationDestination {
    override val route = "settings_appearance_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.settingsNavigation(
    onSettingsSecurityScreen:() -> Unit,
    onSettingsNotificationsScreen:() -> Unit,
    onSettingsLanguageScreen:() -> Unit,
    onSettingsAppearanceScreen:() -> Unit,
    onBackScreen: () -> Unit
) {
    composable(
        route = SettingsDestination.route
    ){
        SettingsRoute(
            onBackScreen = onBackScreen,
            onSettingsSecurityScreen = onSettingsSecurityScreen,
            onSettingsNotificationsScreen = onSettingsNotificationsScreen,
            onSettingsLanguageScreen = onSettingsLanguageScreen,
            onSettingsAppearanceScreen = onSettingsAppearanceScreen
        )
    }

    composable(
        route = SettingsSecurityDestination.route
    ){
        SettingsSecurityRoute(
            onBackScreen = onBackScreen
        )
    }

    composable(
        route = SettingsNotificationsDestination.route
    ){
        SettingsNotificationsRoute(
            onBackScreen = onBackScreen
        )
    }

    composable(
        route = SettingsLanguageDestination.route
    ){
        SettingsLanguageScreenRoute(
            onBackScreen = onBackScreen
        )
    }

    composable(
        route = SettingsAppearanceDestination.route
    ){
        SettingsAppearanceRoute(
            onBackScreen = onBackScreen
        )
    }
}