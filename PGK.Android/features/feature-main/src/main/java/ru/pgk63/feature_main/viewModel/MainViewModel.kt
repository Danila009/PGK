package ru.pgk63.feature_main.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.api.user.repository.UserRepository
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_database.user.UserDataSource
import javax.inject.Inject

@HiltViewModel
internal class MainViewModel @Inject constructor(
    userDataSource: UserDataSource,
    private val userRepository: UserRepository
): ViewModel() {

    private val _responseUserNetwork = MutableStateFlow<Result<User>>(Result.Loading())
    val responseUserNetwork = _responseUserNetwork.asStateFlow()

    val userLocal = userDataSource.get()
        .stateIn(viewModelScope, SharingStarted.Eagerly, null)

    fun getUserNetwork(){
        viewModelScope.launch {
            _responseUserNetwork.value = userRepository.get()
        }
    }

    fun updateDarkMode() {
        viewModelScope.launch {
            userRepository.updateDarkMode()
        }
    }
}