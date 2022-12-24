package ru.pgk63.feature_settings.screens.settingsLanguageScreen

import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Icon
import androidx.compose.material.IconButton
import androidx.compose.material.Scaffold
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack

@Composable
internal fun SettingsLanguageScreenRoute(
    onBackScreen: () -> Unit
) {

    SettingsLanguageScreen(
        onBackScreen = onBackScreen
    )
}

@Composable
private fun SettingsLanguageScreen(
    onBackScreen: () -> Unit
) {
    Scaffold(
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.language),
                onBackClick = onBackScreen,
                actions = {
                    IconButton(onClick = { /*TODO*/ }) {
                        Icon(
                            imageVector = Icons.Default.Search,
                            contentDescription = null,
                            tint = PgkTheme.colors.primaryText
                        )
                    }
                }
            )
        }
    ) { paddingValues ->
        LazyColumn {

            items(10){
                LanguageItemUi()
            }

            item {
                Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
            }
        }
    }
}

@Composable
private fun LanguageItemUi() {

}