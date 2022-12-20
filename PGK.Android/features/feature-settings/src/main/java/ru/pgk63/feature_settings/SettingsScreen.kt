package ru.pgk63.feature_settings

import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Scaffold
import androidx.compose.material.Surface
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.feature_settings.viewModel.SettingsViewModel
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme

@Composable
internal fun SettingsRoute(
    viewModel: SettingsViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
) {
    SettingsScreen(
        onBackScreen = onBackScreen
    )
}

@Composable
private fun SettingsScreen(
    onBackScreen: () -> Unit
) {

    Scaffold(
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.settings),
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        Surface(
            modifier = Modifier.fillMaxSize(),
            color = PgkTheme.colors.primaryBackground
        ) {
            LazyColumn {

                item {
                    Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                }
            }
        }
    }
}