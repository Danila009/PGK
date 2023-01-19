package com.example.feature_guide.screens.directorDetailsScreen

import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.hilt.navigation.compose.hiltViewModel
import com.example.feature_guide.screens.directorDetailsScreen.viewModel.DirectorViewModel
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior

@Composable
internal fun DirectorDetailsRoute(
    viewModel: DirectorViewModel = hiltViewModel(),
    directorId: Int,
    onBackScreen: () -> Unit
) {
    DirectorDetailsScreen(
        onBackScreen = onBackScreen
    )
}

@Composable
private fun DirectorDetailsScreen(
    onBackScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = "",
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        LazyColumn(
            contentPadding = paddingValues
        ){

        }
    }
}