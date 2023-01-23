package ru.lfybkf19.feature_journal.screens.journalDetailsScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.JournalEvaluation
import ru.pgk63.core_common.api.journal.model.JournalRow
import ru.pgk63.core_common.api.journal.model.JournalSubject
import ru.pgk63.core_common.api.journal.repository.JournalRepository
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.api.student.repository.StudentRepository
import ru.pgk63.core_database.user.UserDataSource
import ru.pgk63.core_database.user.model.UserLocalDatabase
import javax.inject.Inject

@HiltViewModel
internal class JournalDetailsViewModel @Inject constructor(
    private val journalRepository: JournalRepository,
    private val studentRepository: StudentRepository,
    userDataSource: UserDataSource
): ViewModel() {

    val userLocal = userDataSource.get()
        .stateIn(viewModelScope, SharingStarted.Eagerly, UserLocalDatabase())

    private val _responseJournalSubjectsList = MutableStateFlow<PagingData<JournalSubject>>(PagingData.empty())
    val responseJournalSubjectsList = _responseJournalSubjectsList.asStateFlow()

    private val _responseJournalRowsList = MutableStateFlow<PagingData<JournalRow>>(PagingData.empty())
    val responseJournalRowsList = _responseJournalRowsList.asStateFlow()

    private val _responseStudentList = MutableStateFlow<PagingData<Student>>(PagingData.empty())
    val responseStudentList = _responseStudentList.asStateFlow()

    fun getJournalRow(
        journalSubjectId:Int? = null,
        studentIds:List<Int>? = null,
        evaluation: JournalEvaluation? = null
    ) {
        viewModelScope.launch {
            journalRepository.getJournalRow(
                journalSubjectId = journalSubjectId,
                studentIds = studentIds,
                evaluation = evaluation
            ).cachedIn(viewModelScope).collect {
                _responseJournalRowsList.value = it
            }
        }
    }

    fun getJournalSubjects(journalId:Int? = null) {
        viewModelScope.launch {
            journalRepository.getJournalSubjects(
                journalId = journalId
            ).cachedIn(viewModelScope).collect {
                _responseJournalSubjectsList.value = it
            }
        }
    }

    fun getStudents(groupIds: List<Int>) {
        viewModelScope.launch {
            studentRepository.getAll(
                groupIds = groupIds
            ).cachedIn(viewModelScope).collect {
                _responseStudentList.value = it
            }
        }
    }
}