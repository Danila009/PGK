package ru.pgk63.core_ui.view

import androidx.compose.foundation.layout.RowScope
import androidx.compose.material.Icon
import androidx.compose.material.IconButton
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.runtime.Composable
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingTitle
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingToolbar
import ru.pgk63.core_ui.view.collapsingToolbar.CollapsingToolbarScrollBehavior

@Composable
fun TopBarBack(
    title: String? = null,
    collapsingTitle: CollapsingTitle? = if(title == null) null else CollapsingTitle.large(titleText = title),
    scrollBehavior: CollapsingToolbarScrollBehavior? = null,
    onBackClick: () -> Unit,
    navigationIcon: @Composable () -> Unit = {},
    actions: @Composable RowScope.() -> Unit = {},
    additionalContent: (@Composable () -> Unit)? = null,
    centralContent: (@Composable () -> Unit)? = null
) {
    CollapsingToolbar(
        collapsingTitle = collapsingTitle,
        scrollBehavior = scrollBehavior,
        additionalContent = additionalContent,
        centralContent = centralContent,
        navigationIcon = {

            IconButton(onClick = onBackClick) {
                Icon(
                    imageVector = Icons.Default.ArrowBack,
                    contentDescription = null,
                    tint = PgkTheme.colors.primaryText
                )
            }

            navigationIcon()
        },
        actions = actions
    )
}