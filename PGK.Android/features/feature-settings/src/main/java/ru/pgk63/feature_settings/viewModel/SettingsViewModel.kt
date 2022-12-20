package ru.pgk63.feature_settings.viewModel

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import ru.pgk63.core_database.user.UserDataSource
import javax.inject.Inject

@HiltViewModel
class SettingsViewModel @Inject constructor(
    private val userDataSource: UserDataSource
): ViewModel() {

}