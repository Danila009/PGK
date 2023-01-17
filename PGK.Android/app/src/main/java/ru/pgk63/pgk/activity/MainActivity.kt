package ru.pgk63.pgk.activity

import android.Manifest
import android.annotation.SuppressLint
import android.os.Build
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.ExperimentalAnimationApi
import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.runtime.*
import androidx.hilt.navigation.compose.hiltViewModel
import com.google.accompanist.navigation.animation.rememberAnimatedNavController
import com.google.accompanist.permissions.ExperimentalPermissionsApi
import com.google.accompanist.permissions.rememberMultiplePermissionsState
import com.google.firebase.messaging.FirebaseMessaging
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_common.language.setLocale
import ru.pgk63.core_database.user.model.UserLocalDatabase
import ru.pgk63.core_navigation.LocalNavController
import ru.pgk63.core_navigation.LocalNavHostController
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.feature_auth.navigation.AuthDestination
import ru.pgk63.feature_main.navigation.MainDestination
import ru.pgk63.pgk.navigation.MainNavHost
import ru.pgk63.pgk.services.fcm.FCM.Companion.subscribeToTopics

@AndroidEntryPoint
class MainActivity : ComponentActivity() {

    @SuppressLint("FlowOperatorInvokedInComposition")
    @OptIn(ExperimentalAnimationApi::class)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContent {

            val mainViewModel = hiltViewModel<MainViewModel>()

            var userLocalDatabase by remember { mutableStateOf<UserLocalDatabase?>(UserLocalDatabase()) }

            val firebaseMessaging = remember { FirebaseMessaging.getInstance() }

            val navHostController = rememberAnimatedNavController()

            val isSystemInDarkTheme = isSystemInDarkTheme()

            mainViewModel.user.onEach {
                userLocalDatabase = it
            }.launchWhenStarted()

            LaunchedEffect(key1 = userLocalDatabase, block = {
                if(userLocalDatabase?.languageCode != null){
                    this@MainActivity.setLocale(userLocalDatabase!!.languageCode!!)
                }
            })

            Permissions()

            userLocalDatabase?.let { user ->

                subscribeToTopics(
                    firebaseMessaging = firebaseMessaging,
                    user = user
                )

                CompositionLocalProvider(
                    LocalNavHostController provides navHostController,
                    LocalNavController provides navHostController
                ) {
                    MainTheme(
                        darkTheme = user.darkMode ?: isSystemInDarkTheme,
                        style = user.themeStyle,
                        textSize = user.themeFontSize,
                        corners = user.themeCorners,
                        fontFamily = user.themeFontStyle
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

    @OptIn(ExperimentalPermissionsApi::class)
    @Composable
    private fun Permissions() {
        val permissions = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
            rememberMultiplePermissionsState(permissions = listOf(
                Manifest.permission.POST_NOTIFICATIONS
            ))
        } else {
            rememberMultiplePermissionsState(permissions = listOf())
        }

        LaunchedEffect(key1 = Unit, block = {
            permissions.launchMultiplePermissionRequest()
        })
    }
}