package ru.lfybkf19.feature_journal.screens.createJournalSubject.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.CreateJournalSubjectBody
import ru.pgk63.core_common.api.journal.repository.JournalRepository
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.repository.SubjectRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
internal class CreateJournalSubjectViewModel @Inject constructor(
    private val journalRepository: JournalRepository,
    private val subjectRepository: SubjectRepository
): ViewModel() {

    private val _responseCreateJournalSubjectResult = MutableStateFlow<Result<Unit?>?>(null)
    val responseCreateJournalSubjectResult = _responseCreateJournalSubjectResult.asStateFlow()

    private val _responseSubjectList = MutableStateFlow<PagingData<Subject>>(PagingData.empty())
    val responseSubjectList = _responseSubjectList.asStateFlow()

    fun createJournalSubject(journalId: Int, body: CreateJournalSubjectBody) {
        viewModelScope.launch {
            val response = journalRepository.createJournalSubject(journalId, body)
            _responseCreateJournalSubjectResult.value = response
        }
    }

    fun getSubjectList(search: String? = null) {
        viewModelScope.launch {
            subjectRepository.getAll(
                search = search
            ).cachedIn(viewModelScope).collect {
                _responseSubjectList.value = it
            }
        }
    }
}