package ru.pgk63.pgk.activity

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.ExperimentalAnimationApi
import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.runtime.*
import com.google.accompanist.navigation.animation.rememberAnimatedNavController
import dagger.hilt.android.AndroidEntryPoint
import ru.pgk63.core_navigation.LocalNavController
import ru.pgk63.core_navigation.LocalNavHostController
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.pgk.navigation.MainNavHost

@AndroidEntryPoint
class MainActivity : ComponentActivity() {
    @OptIn(ExperimentalAnimationApi::class)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {

            val navHostController = rememberAnimatedNavController()

            val isSystemInDarkTheme = isSystemInDarkTheme()

            val isDarkMode by remember { mutableStateOf(isSystemInDarkTheme) }

            CompositionLocalProvider(
                LocalNavHostController provides navHostController,
                LocalNavController provides navHostController
            ) {
                MainTheme(
                    darkTheme = isDarkMode
                ) {
                    MainNavHost()
                }
            }
        }
    }
}