package ru.pgk63.feature_settings.screens.settingsNotificationsScreen

import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Scaffold
import androidx.compose.material.Switch
import androidx.compose.material.SwitchDefaults
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.Lifecycle
import ru.pgk63.core_common.api.user.model.NotificationSetting
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.OnLifecycleEvent
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_settings.screens.settingsNotificationsScreen.viewModel.SettingsNotificationsViewModel
import ru.pgk63.feature_settings.view.SettingsButton

@Composable
internal fun SettingsNotificationsRoute(
    viewModel: SettingsNotificationsViewModel = hiltViewModel(),
    onBackScreen: () -> Unit
) {
    var notifications by remember { mutableStateOf(true) }
    var soundNotifications by remember { mutableStateOf(true) }
    var vibrationNotifications by remember { mutableStateOf(true) }

    var scheduleNotifications by remember { mutableStateOf(true) }
    var journalNotifications by remember { mutableStateOf(true) }
    var raportichkaNotifications by remember { mutableStateOf(true) }
    var technicalSupportNotifications by remember { mutableStateOf(true) }

    if(!notifications) {
        scheduleNotifications = false
        journalNotifications = false
        raportichkaNotifications = false
        technicalSupportNotifications = false
    }

    if(!scheduleNotifications && !journalNotifications
        && !raportichkaNotifications && !technicalSupportNotifications
    ){
        notifications = false
    }

    OnLifecycleEvent { owner, event ->
        if(event == Lifecycle.Event.ON_STOP) {
            viewModel.updateNotificationSettings(
                NotificationSetting(
                    includedNotifications = notifications,
                    soundNotifications = soundNotifications,
                    vibrationNotifications = vibrationNotifications,
                    includedSchedulesNotifications = scheduleNotifications,
                    includedJournalNotifications = journalNotifications,
                    includedRaportichkaNotifications = raportichkaNotifications,
                    includedTechnicalSupportNotifications = technicalSupportNotifications
                )
            )
        }
    }

    SettingsNotificationsScreen(
        notifications = notifications,
        vibrationNotifications = vibrationNotifications,
        soundNotifications = soundNotifications,
        scheduleNotifications = scheduleNotifications,
        journalNotifications = journalNotifications,
        raportichkaNotifications = raportichkaNotifications,
        technicalSupportNotifications = technicalSupportNotifications,
        onBackScreen = onBackScreen,
        onNotificationsChange = {
            notifications = it
            scheduleNotifications = it
            journalNotifications = it
            raportichkaNotifications = it
            technicalSupportNotifications = it
        },
        onSoundNotificationsChange = { soundNotifications = it },
        onVibrationNotificationsChange = { vibrationNotifications = it },
        onScheduleNotificationsChange = {
            scheduleNotifications = it
            if(it) notifications = true
        },
        onJournalNotificationsChange = {
            journalNotifications = it
            if(it) notifications = true
        },
        onRaportichkaNotificationsChange = {
            raportichkaNotifications = it
            if(it) notifications = true
        },
        onTechnicalSupportNotificationsChange = {
            technicalSupportNotifications = it
            if(it) notifications = true
        }
    )
}

@Composable
private fun SettingsNotificationsScreen(
    notifications: Boolean,
    onNotificationsChange: (Boolean) -> Unit,
    soundNotifications: Boolean,
    onSoundNotificationsChange:(Boolean) -> Unit,
    vibrationNotifications: Boolean,
    onVibrationNotificationsChange:(Boolean) -> Unit,
    scheduleNotifications:Boolean,
    onScheduleNotificationsChange: (Boolean) -> Unit,
    journalNotifications:Boolean,
    onJournalNotificationsChange: (Boolean) -> Unit,
    raportichkaNotifications:Boolean,
    onRaportichkaNotificationsChange: (Boolean) -> Unit,
    technicalSupportNotifications:Boolean,
    onTechnicalSupportNotificationsChange: (Boolean) -> Unit,
    onBackScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.notifications),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                actions = {
                    Switch(
                        checked = notifications,
                        onCheckedChange = onNotificationsChange,
                        colors = SwitchDefaults.colors(
                            checkedThumbColor = PgkTheme.colors.tintColor,
                            checkedTrackColor = PgkTheme.colors.tintColor,
                            uncheckedThumbColor = PgkTheme.colors.primaryBackground,
                            uncheckedTrackColor = PgkTheme.colors.errorColor
                        )
                    )
                }
            )
        }
    ) { paddingValues ->
        LazyColumn(
            modifier = Modifier.fillMaxSize()
        ) {

            item {

                SettingsButton(
                    title = stringResource(id = R.string.sound),
                    switchCheck = soundNotifications,
                    onSwitchCheckedChange = onSoundNotificationsChange,
                    onClick = { onSoundNotificationsChange(!soundNotifications) }
                )

                SettingsButton(
                    title = stringResource(id = R.string.vibration),
                    switchCheck = vibrationNotifications,
                    onSwitchCheckedChange = onVibrationNotificationsChange,
                    onClick = { onVibrationNotificationsChange(!vibrationNotifications) }
                )

                SettingsButton(
                    title = stringResource(id = R.string.schedule),
                    body = stringResource(id = R.string.schedule_notifications_settings_body),
                    switchCheck = scheduleNotifications,
                    onSwitchCheckedChange = onScheduleNotificationsChange,
                    onClick = { onScheduleNotificationsChange(!scheduleNotifications) }
                )

                SettingsButton(
                    title = stringResource(id = R.string.journal),
                    body = stringResource(id = R.string.journal_notifications_settings_body),
                    switchCheck = journalNotifications,
                    onSwitchCheckedChange = onJournalNotificationsChange,
                    onClick = { onJournalNotificationsChange(!journalNotifications) }
                )

                SettingsButton(
                    title = stringResource(id = R.string.raportichka),
                    body = stringResource(id = R.string.raportichka_notifications_settings_body),
                    switchCheck = raportichkaNotifications,
                    onSwitchCheckedChange = onRaportichkaNotificationsChange,
                    onClick = { onRaportichkaNotificationsChange(!raportichkaNotifications) }
                )

                SettingsButton(
                    title = stringResource(id = R.string.technical_support),
                    body = stringResource(id = R.string.technical_support_notifications_settings_body),
                    switchCheck = technicalSupportNotifications,
                    onSwitchCheckedChange = onTechnicalSupportNotificationsChange,
                    onClick = { onTechnicalSupportNotificationsChange(!technicalSupportNotifications) }
                )
            }

            item { Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding())) }
        }
    }
}