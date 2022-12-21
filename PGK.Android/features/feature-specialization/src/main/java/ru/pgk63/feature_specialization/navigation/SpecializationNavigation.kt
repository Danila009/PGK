package ru.pgk63.feature_specialization.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import androidx.navigation.NavType
import androidx.navigation.navArgument
import com.google.accompanist.navigation.animation.composable
import ru.pgk63.core_navigation.NavigationDestination
import ru.pgk63.feature_specialization.screens.specializationDetailsScreen.SpecializationDetailsRoute
import ru.pgk63.feature_specialization.screens.specializationListScreen.SpecializationListRoute

object SpecializationListDestination : NavigationDestination {
    override val route = "specialization_list_screen"
}

object SpecializationDetailsDestination : NavigationDestination {
    override val route: String = "specialization_details_screen"
    const val id_argument = "id"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.specializationNavigation(
    onBackScreen: () -> Unit,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {
    composable(
        route = SpecializationListDestination.route
    ){
        SpecializationListRoute(
            onBackScreen = onBackScreen,
            onSpecializationDetailsScreen = onSpecializationDetailsScreen
        )
    }

    composable(
        route = "${SpecializationDetailsDestination.route}/{${SpecializationDetailsDestination.id_argument}}",
        arguments = listOf(
            navArgument(SpecializationDetailsDestination.id_argument){
                type = NavType.IntType
            }
        )
    ) {
       SpecializationDetailsRoute(
           specializationId = it.arguments!!.getInt(SpecializationDetailsDestination.id_argument),
           onBackScreen = onBackScreen
       )
    }
}