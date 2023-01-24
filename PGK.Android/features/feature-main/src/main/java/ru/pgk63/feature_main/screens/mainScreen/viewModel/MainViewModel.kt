package ru.pgk63.feature_main.screens.mainScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.JournalColumn
import ru.pgk63.core_common.api.journal.repository.JournalRepository
import ru.pgk63.core_common.api.raportichka.model.Raportichka
import ru.pgk63.core_common.api.raportichka.repository.RaportichkaRepository
import ru.pgk63.core_common.api.user.model.UserDetails
import ru.pgk63.core_common.api.user.repository.UserRepository
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.extension.getCurrentDateTime
import ru.pgk63.core_common.extension.parseToNetworkFormat
import ru.pgk63.core_database.user.UserDataSource
import javax.inject.Inject

@HiltViewModel
internal class MainViewModel @Inject constructor(
    userDataSource: UserDataSource,
    private val userRepository: UserRepository,
    private val raportichkaRepository: RaportichkaRepository,
    private val journalRepository: JournalRepository
): ViewModel() {

    private val _responseUserNetwork = MutableStateFlow<Result<UserDetails>>(Result.Loading())
    val responseUserNetwork = _responseUserNetwork.asStateFlow()

    private val _responseRaportichkaList = MutableStateFlow<PagingData<Raportichka>>(PagingData.empty())
    val responseRaportichkaList = _responseRaportichkaList.asStateFlow()

    private val _responseJournalColumnList = MutableStateFlow<PagingData<JournalColumn>>(PagingData.empty())
    val responseJournalColumnList = _responseJournalColumnList.asStateFlow()

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

    fun getRaportichkaList(studentIds:List<Int>? = listOf()) {
        viewModelScope.launch {
            raportichkaRepository.getRaportichkaAll(
                studentIds = studentIds,
                onlyDate = getCurrentDateTime().parseToNetworkFormat()
            )
                .cachedIn(viewModelScope).collect {
                    _responseRaportichkaList.value = it
                }
        }
    }

    fun getJournalColumnList(studentIds:List<Int>? = listOf()) {
        viewModelScope.launch {
            journalRepository.getJournalColumn(
                studentIds = studentIds
            ).cachedIn(viewModelScope).collect {
                _responseJournalColumnList.value = it
            }
        }
    }
}