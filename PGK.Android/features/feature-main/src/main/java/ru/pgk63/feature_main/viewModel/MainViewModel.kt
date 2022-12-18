package ru.pgk63.feature_main.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.user.repository.UserRepository
import javax.inject.Inject

@HiltViewModel
internal class MainViewModel @Inject constructor(
    private val userRepository: UserRepository
): ViewModel() {

    fun updateDarkMode() {
        viewModelScope.launch {
            userRepository.updateDarkMode()
        }
    }
}