package ru.lfybkf19.feature_journal.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import androidx.navigation.NavType
import androidx.navigation.navArgument
import com.google.accompanist.navigation.animation.composable
import ru.lfybkf19.feature_journal.screens.journalDetailsScreen.JournalDetailsRoute
import ru.lfybkf19.feature_journal.screens.journalListScreen.JournalListRoute
import ru.pgk63.core_navigation.NavigationDestination

object JournalListDestination : NavigationDestination {
    override val route = "journal_list_screen"
}

object JournalDetailsDestination : NavigationDestination {
    override val route = "journal_details_screen"
    const val journalId = "journalId"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.journalNavigation(
    onJournalDetailsScreen: (journalId: Int) -> Unit,
    onBackScreen: () -> Unit
) {
    composable(
        route = JournalListDestination.route
    ){
        JournalListRoute(
            onBackScreen = onBackScreen,
            onJournalDetailsScreen = onJournalDetailsScreen
        )
    }

    composable(
        route = "${JournalDetailsDestination.route}/{${JournalDetailsDestination.journalId}}",
        arguments = listOf(
            navArgument(JournalDetailsDestination.journalId){
                type = NavType.IntType
            }
        )
    ){
        JournalDetailsRoute(
            journalId = it.arguments?.getInt(JournalDetailsDestination.journalId),
            onBackScreen = onBackScreen
        )
    }
}