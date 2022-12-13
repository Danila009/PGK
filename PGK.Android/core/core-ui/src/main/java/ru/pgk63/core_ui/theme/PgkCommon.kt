package ru.pgk63.core_ui.theme

import androidx.compose.runtime.Composable
import androidx.compose.runtime.staticCompositionLocalOf
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.unit.Dp

data class JetHabitColors(
    val primaryText: Color,
    val primaryBackground: Color,
    val secondaryText: Color,
    val secondaryBackground: Color,
    val tintColor: Color,
    val drawerBackground: Color,
    val controlColor: Color,
    val errorColor: Color
)

data class JetHabitTypography(
    val heading: TextStyle,
    val body: TextStyle,
    val toolbar: TextStyle,
    val caption: TextStyle
)

data class JetHabitShape(
    val padding: Dp,
    val cornersStyle: Shape
)

data class JetHabitFontFamily(
    val fontFamily: FontFamily
)

object PgkTheme {
    val colors: JetHabitColors
        @Composable
        get() = LocalJetHabitColors.current
    val typography: JetHabitTypography
        @Composable
        get() = LocalJetHabitTypography.current
    val shapes: JetHabitShape
        @Composable
        get() = LocalJetHabitShape.current
    val fontFamily:JetHabitFontFamily
        @Composable
        get() = LocalJetFontFamily.current
}

enum class JetHabitStyle {
    Purple, Orange, Blue, Red, Green, Yellow
}

enum class JetHabitSize {
    Small, Medium, Big
}

enum class JetHabitCorners {
    Flat, Rounded
}

enum class JetHabitFont {
    Cursive, Serif, Default, Monospace, SansSerif
}

val LocalJetHabitColors = staticCompositionLocalOf<JetHabitColors> {
    error("No colors provided")
}

val LocalJetHabitTypography = staticCompositionLocalOf<JetHabitTypography> {
    error("No font provided")
}

val LocalJetHabitShape = staticCompositionLocalOf<JetHabitShape> {
    error("No shapes provided")
}

val LocalJetFontFamily = staticCompositionLocalOf<JetHabitFontFamily> {
    error("No font family provided")
}