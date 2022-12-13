package ru.pgk63.pgk.navigation

import androidx.navigation.NavController
import androidx.navigation.NavGraphBuilder
import ru.pgk63.feature_main.navigation.mainNavigation

fun NavGraphBuilder.mainNavGraphBuilder(
    navController: NavController
){
    mainNavigation()
}