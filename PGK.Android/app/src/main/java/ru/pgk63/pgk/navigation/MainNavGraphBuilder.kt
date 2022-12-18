package ru.pgk63.pgk.navigation

import androidx.navigation.NavController
import androidx.navigation.NavGraphBuilder
import ru.pgk63.feature_auth.navigation.authNavigation
import ru.pgk63.feature_group.navigation.GroupDestination
import ru.pgk63.feature_group.navigation.GroupDetailsDestination
import ru.pgk63.feature_group.navigation.groupNavigation
import ru.pgk63.feature_main.navigation.mainNavigation

fun NavGraphBuilder.mainNavGraphBuilder(
    navController: NavController
){
    mainNavigation(
        onGroupScreen = {
            navController.navigate(GroupDestination.route)
        }
    )

    authNavigation()

    groupNavigation(
        onGroupDetailsScreen = { id ->
            navController.navigate("${GroupDetailsDestination.route}/$id")
        },
        onBackScreen = {
            navController.navigateUp()
        }
    )
}