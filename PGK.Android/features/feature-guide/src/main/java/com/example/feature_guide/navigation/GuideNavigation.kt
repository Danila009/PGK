package com.example.feature_guide.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import com.example.feature_guide.screens.guideListScreen.GuideListRoute
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination

object GuideListDestination : NavigationDestination {
    override val route: String = "guide_list_screen"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.guideNavigation(
    onBackScreen: () -> Unit
) {
    composable(
        route = GuideListDestination.route
    ){
        GuideListRoute(
            onBackScreen = onBackScreen
        )
    }
}