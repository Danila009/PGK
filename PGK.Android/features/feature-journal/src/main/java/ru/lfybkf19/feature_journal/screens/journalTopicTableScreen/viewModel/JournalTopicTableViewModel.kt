package ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.JournalTopic
import ru.pgk63.core_common.api.journal.repository.JournalRepository
import javax.inject.Inject

@HiltViewModel
internal class JournalTopicTableViewModel @Inject constructor(
    private val journalRepository: JournalRepository
): ViewModel() {

    private val _responseJournalTopicList = MutableStateFlow<PagingData<JournalTopic>>(PagingData.empty())
    val responseJournalTopicList = _responseJournalTopicList.asStateFlow()

    fun getJournalTopics(journalSubjectId: Int) {
        viewModelScope.launch {
            journalRepository.getJournalTopics(
                journalSubjectId = journalSubjectId
            ).cachedIn(viewModelScope).collect {
                _responseJournalTopicList.value = it
            }
        }
    }
}