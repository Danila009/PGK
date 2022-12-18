package ru.pgk63.core_ui.view

import androidx.compose.foundation.layout.RowScope
import androidx.compose.material.Icon
import androidx.compose.material.IconButton
import androidx.compose.material.Text
import androidx.compose.material.TopAppBar
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowLeft
import androidx.compose.runtime.Composable
import ru.pgk63.core_ui.theme.PgkTheme

@Composable
fun TopBarBack(
    title: String,
    onBackClick: () -> Unit,
    navigationIcon: @Composable () -> Unit = {},
    actions: @Composable RowScope.() -> Unit = {},
) {
    TopAppBar(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        title = {
            Text(
                text = title,
                color = PgkTheme.colors.primaryText,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                style = PgkTheme.typography.toolbar
            )
        },
        navigationIcon = {
            IconButton(onClick = onBackClick) {
                Icon(
                    imageVector = Icons.Default.KeyboardArrowLeft,
                    contentDescription = "back",
                    tint = PgkTheme.colors.primaryText
                )
            }

            navigationIcon()
        },
        actions = actions
    )
}