package ru.lfybkf19.feature_journal.screens.journalListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.journal.model.Journal
import ru.pgk63.core_common.api.journal.repository.JournalRepository
import javax.inject.Inject

@HiltViewModel
internal class JournalListViewModel @Inject constructor(
    private val journalRepository: JournalRepository
): ViewModel() {

    private val _responseJournalList = MutableStateFlow<PagingData<Journal>>(PagingData.empty())
    val responseJournalList = _responseJournalList.asStateFlow()

    fun getJournalList(
        course:List<Int>? = null,
        semesters:List<Int>? = null,
        groupIds:List<Int>? = null,
        specialityIds:List<Int>? = null,
        departmentIds:List<Int>? = null
    ){
        viewModelScope.launch {
            journalRepository.getAll(
                course = course,
                semesters = semesters,
                groupIds = groupIds,
                specialityIds = specialityIds,
                departmentIds = departmentIds
            ).cachedIn(viewModelScope).collect {
                _responseJournalList.value = it
            }
        }
    }
}