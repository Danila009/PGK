package ru.pgk63.feature_settings.screens.settingsNotificationsScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.user.model.NotificationSetting
import ru.pgk63.core_common.api.user.repository.UserRepository
import javax.inject.Inject

@HiltViewModel
class SettingsNotificationsViewModel @Inject constructor(
    private val userRepository: UserRepository
): ViewModel() {

    fun updateNotificationSettings(body: NotificationSetting) {
        viewModelScope.launch {
            userRepository.updateNotificationSettings(body)
        }
    }
}