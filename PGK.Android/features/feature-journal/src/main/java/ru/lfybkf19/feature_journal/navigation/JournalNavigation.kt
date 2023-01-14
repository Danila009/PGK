package ru.lfybkf19.feature_journal.navigation

import androidx.compose.animation.ExperimentalAnimationApi
import androidx.navigation.NavGraphBuilder
import androidx.navigation.NavType
import androidx.navigation.navArgument
import com.google.accompanist.navigation.animation.composable
import ru.lfybkf19.feature_journal.screens.journalDetailsScreen.JournalDetailsRoute
import ru.lfybkf19.feature_journal.screens.journalListScreen.JournalListRoute
import ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.JournalTopicTableRoute
import ru.pgk63.core_navigation.NavigationDestination

object JournalListDestination : NavigationDestination {
    override val route = "journal_list_screen"
}

object JournalDetailsDestination : NavigationDestination {
    override val route = "journal_details_screen"
    const val journalId = "journalId"
}

object JournalTopicTableDestination : NavigationDestination {
    override val route = "journal_topics_table_screen"
    const val journalSubjectId = "journalSubjectId"
    const val maxSubjectHours = "maxSubjectHours"
}

@OptIn(ExperimentalAnimationApi::class)
fun NavGraphBuilder.journalNavigation(
    onJournalDetailsScreen: (journalId: Int) -> Unit,
    onJournalTopicTableScreen: (journalSubjectId:Int, maxSubjectHours:Int) -> Unit,
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
            onJournalTopicTableScreen = onJournalTopicTableScreen,
            onBackScreen = onBackScreen
        )
    }

    composable(
        route = "${JournalTopicTableDestination.route}/{${JournalTopicTableDestination.journalSubjectId}}?" +
                "${JournalTopicTableDestination.maxSubjectHours}={${JournalTopicTableDestination.maxSubjectHours}}",
        arguments = listOf(
            navArgument(JournalTopicTableDestination.journalSubjectId){
                type = NavType.IntType
            },
            navArgument(JournalTopicTableDestination.maxSubjectHours){
                type = NavType.IntType
            }
        )
    ){
        JournalTopicTableRoute(
            journalSubjectId = it.arguments?.getInt(JournalTopicTableDestination.journalSubjectId)!!,
            maxSubjectHours = it.arguments?.getInt(JournalTopicTableDestination.maxSubjectHours)!!,
            onBackScreen = onBackScreen
        )
    }
}