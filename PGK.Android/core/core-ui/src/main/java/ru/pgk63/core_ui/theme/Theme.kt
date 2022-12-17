package ru.pgk63.core_ui.theme

import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.runtime.Composable
import androidx.compose.runtime.CompositionLocalProvider
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp


@Composable
fun MainTheme(
    style: PgkStyle = PgkStyle.Green,
    textSize: PgkSize = PgkSize.Medium,
    paddingSize: PgkSize = PgkSize.Medium,
    corners: PgkCorners = PgkCorners.Rounded,
    fontFamily: PgkFont = PgkFont.Default,
    darkTheme: Boolean = isSystemInDarkTheme(),
    content: @Composable () -> Unit
) {
    val colors = when (darkTheme) {
        true -> {
            when (style) {
                PgkStyle.Purple -> purpleDarkPalette
                PgkStyle.Blue -> blueDarkPalette
                PgkStyle.Orange -> orangeDarkPalette
                PgkStyle.Red -> redDarkPalette
                PgkStyle.Green -> greenDarkPalette
                PgkStyle.Yellow -> yellowDarkPalette
            }
        }
        false -> {
            when (style) {
                PgkStyle.Purple -> purpleLightPalette
                PgkStyle.Blue -> blueLightPalette
                PgkStyle.Orange -> orangeLightPalette
                PgkStyle.Red -> redLightPalette
                PgkStyle.Green -> greenLightPalette
                PgkStyle.Yellow -> yellowLightPalette
            }
        }
    }

    val typography = PgkTypography(
        heading = TextStyle(
            fontSize = when (textSize) {
                PgkSize.Small -> 20.sp
                PgkSize.Medium -> 24.sp
                PgkSize.Big -> 28.sp
            },
            fontWeight = FontWeight.Bold
        ),
        body = TextStyle(
            fontSize = when (textSize) {
                PgkSize.Small -> 14.sp
                PgkSize.Medium -> 16.sp
                PgkSize.Big -> 18.sp
            },
            fontWeight = FontWeight.Normal
        ),
        toolbar = TextStyle(
            fontSize = when (textSize) {
                PgkSize.Small -> 14.sp
                PgkSize.Medium -> 16.sp
                PgkSize.Big -> 18.sp
            },
            fontWeight = FontWeight.Medium
        ),
        caption = TextStyle(
            fontSize = when (textSize) {
                PgkSize.Small -> 10.sp
                PgkSize.Medium -> 12.sp
                PgkSize.Big -> 14.sp
            }
        )
    )

    val shapes = PgkShape(
        padding = when (paddingSize) {
            PgkSize.Small -> 12.dp
            PgkSize.Medium -> 16.dp
            PgkSize.Big -> 20.dp
        },
        cornersStyle = when (corners) {
            PgkCorners.Flat -> RoundedCornerShape(0.dp)
            PgkCorners.Rounded -> RoundedCornerShape(8.dp)
        }
    )

    val font = PgkFontFamily(
        fontFamily = when(fontFamily){
            PgkFont.Cursive -> FontFamily.Cursive
            PgkFont.Serif -> FontFamily.Serif
            PgkFont.Default -> FontFamily.Default
            PgkFont.Monospace -> FontFamily.Monospace
            PgkFont.SansSerif -> FontFamily.SansSerif
        }
    )

    CompositionLocalProvider(
        LocalPgkHabitColors provides colors,
        LocalPgkHabitTypography provides typography,
        LocalPgkHabitShape provides shapes,
        LocalPgkFontFamily provides font,
        content = content
    )
}