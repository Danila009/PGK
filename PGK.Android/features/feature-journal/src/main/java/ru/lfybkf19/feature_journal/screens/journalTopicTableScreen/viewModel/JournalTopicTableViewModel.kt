package ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.CreateJournalTopicBody
import ru.pgk63.core_common.api.journal.model.JournalTopic
import ru.pgk63.core_common.api.journal.repository.JournalRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
internal class JournalTopicTableViewModel @Inject constructor(
    private val journalRepository: JournalRepository
): ViewModel() {

    private val _responseJournalTopicList = MutableStateFlow<PagingData<JournalTopic>>(PagingData.empty())
    val responseJournalTopicList = _responseJournalTopicList.asStateFlow()

    private val _responseCreateJournalTopic = MutableStateFlow<Result<Unit?>?>(null)
    val responseCreateJournalTopic = _responseCreateJournalTopic.asStateFlow()

    private val _responseDeleteJournalTopic = MutableStateFlow<Result<Unit?>?>(null)
    val responseDeleteJournalTopic = _responseDeleteJournalTopic.asStateFlow()

    fun getJournalTopics(journalSubjectId: Int) {
        viewModelScope.launch {
            journalRepository.getJournalTopics(
                journalSubjectId = journalSubjectId
            ).cachedIn(viewModelScope).collect {
                _responseJournalTopicList.value = it
            }
        }
    }

    fun createJournalTopic(
        journalSubjectId: Int,
        body: CreateJournalTopicBody
    ) {
        viewModelScope.launch {
            _responseCreateJournalTopic.value = journalRepository.createJournalTopic(
                journalSubjectId = journalSubjectId,
                body = body
            )
        }
    }

    fun deleteJournalTopic(id: Int) {
        viewModelScope.launch {
            _responseDeleteJournalTopic.value = journalRepository.deleteJournalTopic(id)
        }
    }
}