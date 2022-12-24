package ru.pgk63.pgk.activity

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.stateIn
import ru.pgk63.core_database.user.UserDataSource
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    userDataSource: UserDataSource
): ViewModel() {

    val user = userDataSource.get().stateIn(viewModelScope, SharingStarted.Eagerly,null)
}