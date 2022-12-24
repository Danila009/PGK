package ru.pgk63.pgk.activity

import android.annotation.SuppressLint
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.ExperimentalAnimationApi
import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.runtime.*
import androidx.hilt.navigation.compose.hiltViewModel
import com.google.accompanist.navigation.animation.rememberAnimatedNavController
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_database.user.model.UserLocalDatabase
import ru.pgk63.core_navigation.LocalNavController
import ru.pgk63.core_navigation.LocalNavHostController
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.feature_auth.navigation.AuthDestination
import ru.pgk63.feature_main.navigation.MainDestination
import ru.pgk63.pgk.navigation.MainNavHost

@AndroidEntryPoint
class MainActivity : ComponentActivity() {

    @SuppressLint("FlowOperatorInvokedInComposition")
    @OptIn(ExperimentalAnimationApi::class)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {

            val mainViewModel = hiltViewModel<MainViewModel>()

            var userLocalDatabase by remember { mutableStateOf<UserLocalDatabase?>(UserLocalDatabase()) }

            mainViewModel.user.onEach {
                userLocalDatabase = it
            }.launchWhenStarted()

            val navHostController = rememberAnimatedNavController()

            val isSystemInDarkTheme = isSystemInDarkTheme()

            userLocalDatabase?.let { user ->
                CompositionLocalProvider(
                    LocalNavHostController provides navHostController,
                    LocalNavController provides navHostController
                ) {
                    MainTheme(
                        darkTheme = user.darkMode ?: isSystemInDarkTheme
                    ) {
                        MainNavHost(
                            startDestination = if(user.statusRegistration)
                                MainDestination.route else AuthDestination.route
                        )
                    }
                }
            }
        }
    }
}