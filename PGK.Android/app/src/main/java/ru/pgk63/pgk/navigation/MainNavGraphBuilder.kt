package ru.pgk63.pgk.navigation

import androidx.navigation.NavController
import androidx.navigation.NavGraphBuilder
import ru.pgk63.feature_auth.navigation.authNavigation
import ru.pgk63.feature_group.navigation.GroupDestination
import ru.pgk63.feature_group.navigation.GroupDetailsDestination
import ru.pgk63.feature_group.navigation.groupNavigation
import ru.pgk63.feature_main.navigation.mainNavigation
import ru.pgk63.feature_settings.navigation.SettingsDestination
import ru.pgk63.feature_settings.navigation.settingsNavigation
import ru.pgk63.feature_tech_support.navigation.TechSupportChatDestination
import ru.pgk63.feature_tech_support.navigation.techSupportNavigation

fun NavGraphBuilder.mainNavGraphBuilder(
    navController: NavController
){
    mainNavigation(
        onTechSupportChatScreen = {
            navController.navigate(TechSupportChatDestination.route)
        },
        onGroupScreen = {
            navController.navigate(GroupDestination.route)
        },
        onSettingsScreen = {
            navController.navigate(SettingsDestination.route)
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

    techSupportNavigation(
        onBackScreen = {
            navController.navigateUp()
        }
    )

    settingsNavigation(
        onBackScreen = {
            navController.navigateUp()
        }
    )
}