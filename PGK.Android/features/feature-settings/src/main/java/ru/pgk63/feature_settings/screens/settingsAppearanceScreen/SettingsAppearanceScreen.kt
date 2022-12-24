package ru.pgk63.feature_settings.screens.settingsAppearanceScreen

import androidx.compose.foundation.BorderStroke
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import ru.pgk63.core_common.enums.theme.ThemeCorners
import ru.pgk63.core_common.enums.theme.ThemeFontSize
import ru.pgk63.core_common.enums.theme.ThemeFontStyle
import ru.pgk63.core_common.enums.theme.ThemeStyle
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.*
import ru.pgk63.core_ui.view.TopBarBack

@Composable
internal fun SettingsAppearanceRoute(
    onBackScreen: () -> Unit
) {
    val isDarkMode by remember { mutableStateOf(true) }
    var fontStyle by remember { mutableStateOf(ThemeFontStyle.Default) }
    var themeCorners by remember { mutableStateOf(ThemeCorners.Rounded) }
    var themeFontSize by remember { mutableStateOf(ThemeFontSize.Medium) }
    var themeStyle by remember { mutableStateOf(ThemeStyle.Green) }

    SettingsAppearanceScreen(
        isDarkMode = isDarkMode,
        fontStyle = fontStyle,
        themeCorners = themeCorners,
        themeFontSize = themeFontSize,
        themeStyle = themeStyle,
        onBackScreen = onBackScreen,
        onFontStyleChange = {
            fontStyle = it
        },
        onThemeCornersChange = {
            themeCorners = it
        },
        onThemeFontSizeChange = {
            themeFontSize = it
        },
        onThemeStyle = {
            themeStyle = it
        }
    )
}

@Composable
private fun SettingsAppearanceScreen(
    isDarkMode:Boolean,
    fontStyle: ThemeFontStyle,
    themeCorners: ThemeCorners,
    themeFontSize: ThemeFontSize,
    themeStyle: ThemeStyle,
    onFontStyleChange: (ThemeFontStyle) -> Unit,
    onThemeCornersChange: (ThemeCorners) -> Unit,
    onThemeFontSizeChange: (ThemeFontSize) -> Unit,
    onThemeStyle: (ThemeStyle) -> Unit,
    onBackScreen: () -> Unit
) {
    Scaffold(
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.appearance),
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        LazyColumn {

            item {
                MenuThemeFontStyle(
                    fontStyle = fontStyle,
                    onFontStyleChange = onFontStyleChange
                )

                MenuThemeCorners(
                    themeCorners = themeCorners,
                    onThemeCornersChange = onThemeCornersChange
                )

                MenuThemeFontSize(
                    themeFontSize = themeFontSize,
                    onThemeFontSizeChange = onThemeFontSizeChange
                )

                ThemeStyleCards(
                    isDarkMode = isDarkMode,
                    onThemeStyle = onThemeStyle,
                    themeStyle = themeStyle
                )
            }

            item {
                Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
            }
        }
    }
}

@Composable
private fun MenuThemeFontStyle(
    fontStyle: ThemeFontStyle,
    onFontStyleChange: (ThemeFontStyle) -> Unit
) {

    var dropdownMenu by remember { mutableStateOf(false) }

    Column {
        StyleButton(
            text = stringResource(id = R.string.font_style),
            style = stringResource(id = fontStyle.textId),
            fontFamily = fontStyle.fontFamily
        ) {
            dropdownMenu = !dropdownMenu
        }

        DropdownMenu(
            modifier = Modifier
                .fillMaxWidth()
                .background(PgkTheme.colors.secondaryBackground),
            expanded = dropdownMenu,
            onDismissRequest = { dropdownMenu = false }
        ) {
            ThemeFontStyle.values().forEach { style ->
                StyleDropdownMenuItem(
                    text = stringResource(id = style.textId),
                    fontFamily = style.fontFamily,
                    enable = fontStyle == style,
                    onClick = {
                        onFontStyleChange(style)
                        dropdownMenu = false
                    }
                )
            }
        }
    }
}

@Composable
private fun MenuThemeCorners(
    themeCorners: ThemeCorners,
    onThemeCornersChange: (ThemeCorners) -> Unit
) {

    var dropdownMenu by remember { mutableStateOf(false) }

    Column {
        StyleButton(
            text = stringResource(id = R.string.corners),
            style = stringResource(id = themeCorners.textId)
        ) {
            dropdownMenu = !dropdownMenu
        }

        DropdownMenu(
            modifier = Modifier
                .fillMaxWidth()
                .background(PgkTheme.colors.secondaryBackground),
            expanded = dropdownMenu,
            onDismissRequest = { dropdownMenu = false }
        ) {
            ThemeCorners.values().forEach { style ->
                StyleDropdownMenuItem(
                    text = stringResource(id = style.textId),
                    enable = themeCorners == style,
                    onClick = {
                        onThemeCornersChange(style)
                        dropdownMenu = false
                    }
                )
            }
        }
    }
}

@Composable
private fun MenuThemeFontSize(
    themeFontSize: ThemeFontSize,
    onThemeFontSizeChange: (ThemeFontSize) -> Unit
) {

    var dropdownMenu by remember { mutableStateOf(false) }

    Column {
        StyleButton(
            text = stringResource(id = R.string.font_size),
            style = stringResource(id = themeFontSize.textId)
        ) {
            dropdownMenu = !dropdownMenu
        }

        DropdownMenu(
            modifier = Modifier
                .fillMaxWidth()
                .background(PgkTheme.colors.secondaryBackground),
            expanded = dropdownMenu,
            onDismissRequest = { dropdownMenu = false }
        ) {
            ThemeFontSize.values().forEach { style ->
                StyleDropdownMenuItem(
                    text = stringResource(id = style.textId),
                    enable = themeFontSize == style,
                    onClick = {
                        onThemeFontSizeChange(style)
                        dropdownMenu = false
                    }
                )
            }
        }
    }
}

@Composable
private fun StyleDropdownMenuItem(
    text: String,
    enable:Boolean,
    fontFamily:FontFamily? = null,
    onClick: () -> Unit
) {
    DropdownMenuItem(onClick = onClick) {
        Text(
            text = text,
            color = if(enable) PgkTheme.colors.tintColor else PgkTheme.colors.primaryText,
            fontFamily = fontFamily ?: PgkTheme.fontFamily.fontFamily,
            style = PgkTheme.typography.body
        )
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun StyleButton(
    text: String,
    style: String,
    fontFamily: FontFamily? = null,
    onClick: () -> Unit,
) {
    Card(backgroundColor = PgkTheme.colors.primaryBackground,onClick = onClick) {
        Column {
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Text(
                    text = text,
                    color = PgkTheme.colors.primaryText,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    style = PgkTheme.typography.body,
                    modifier = Modifier.padding(5.dp)
                )

                Text(
                    text = style,
                    color = PgkTheme.colors.primaryText,
                    fontFamily = fontFamily ?: PgkTheme.fontFamily.fontFamily,
                    style = PgkTheme.typography.caption,
                    modifier = Modifier.padding(5.dp),
                    fontWeight = FontWeight.W100
                )
            }

            Divider(color = PgkTheme.colors.secondaryBackground)
        }
    }
}

@Composable
private fun ThemeStyleCards(
    isDarkMode:Boolean,
    themeStyle: ThemeStyle,
    onThemeStyle: (ThemeStyle) -> Unit
) {

    Spacer(modifier = Modifier.height(30.dp))

    Row(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = Arrangement.SpaceEvenly
    ) {
        ThemeStyleCard(
            color = if (isDarkMode) purpleDarkPalette.tintColor else purpleLightPalette.tintColor,
            enable = themeStyle == ThemeStyle.Purple,
            onClick = {
                onThemeStyle(ThemeStyle.Purple)
            }
        )
        ThemeStyleCard(
            color = if (isDarkMode) orangeDarkPalette.tintColor else orangeLightPalette.tintColor,
            enable = themeStyle == ThemeStyle.Orange,
            onClick = {
                onThemeStyle(ThemeStyle.Orange)
            }
        )
        ThemeStyleCard(
            color = if (isDarkMode) blueDarkPalette.tintColor else blueLightPalette.tintColor,
            enable = themeStyle == ThemeStyle.Blue,
            onClick = {
                onThemeStyle(ThemeStyle.Blue)
            }
        )
    }

    Spacer(modifier = Modifier.height(20.dp))

    Row(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = Arrangement.SpaceEvenly
    ) {
        ThemeStyleCard(
            color = if (isDarkMode) redDarkPalette.tintColor else redLightPalette.tintColor,
            enable = themeStyle == ThemeStyle.Red,
            onClick = {
                onThemeStyle(ThemeStyle.Red)
            }
        )
        ThemeStyleCard(
            color = if (isDarkMode) greenDarkPalette.tintColor else greenLightPalette.tintColor,
            enable = themeStyle == ThemeStyle.Green,
            onClick = {
                onThemeStyle(ThemeStyle.Green)
            }
        )
        ThemeStyleCard(
            color = if (isDarkMode) yellowDarkPalette.tintColor else yellowLightPalette.tintColor,
            enable = themeStyle == ThemeStyle.Yellow,
            onClick = {
                onThemeStyle(ThemeStyle.Yellow)
            }
        )
    }
}

@Composable
private fun ThemeStyleCard(
    color: Color,
    enable:Boolean,
    onClick: () -> Unit
) {
    Card(
        modifier = Modifier
            .size(60.dp)
            .clickable { onClick() },
        backgroundColor = color,
        elevation = 8.dp,
        shape = PgkTheme.shapes.cornersStyle,
        border = if(enable) BorderStroke(3.dp,PgkTheme.colors.primaryText) else null
    ) { }
}