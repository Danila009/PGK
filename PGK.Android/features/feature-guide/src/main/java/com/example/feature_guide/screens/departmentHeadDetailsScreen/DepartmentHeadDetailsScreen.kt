package com.example.feature_guide.screens.departmentHeadDetailsScreen

import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.hilt.navigation.compose.hiltViewModel
import com.example.feature_guide.screens.departmentHeadDetailsScreen.viewModel.DepartmentHeadDetailsViewModel
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior

@Composable
internal fun DepartmentHeadDetailsRoute(
    viewModel: DepartmentHeadDetailsViewModel = hiltViewModel(),
    departmentHeadId: Int,
    onBackScreen: () -> Unit
) {
    DepartmentHeadDetailsScreen(
        onBackScreen = onBackScreen
    )
}

@Composable
private fun DepartmentHeadDetailsScreen(
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