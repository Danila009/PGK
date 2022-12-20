package ru.pgk63.feature_settings.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_settings.SettingsRoute

object SettingsDestination : NavigationDestination {
    override val route = "settings_screen"
}
@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.settingsNavigation(
    onBackScreen: () -> Unit
) {
    composable(
        route = SettingsDestination.route
    ){
        SettingsRoute(
            onBackScreen = onBackScreen
        )
    }
}