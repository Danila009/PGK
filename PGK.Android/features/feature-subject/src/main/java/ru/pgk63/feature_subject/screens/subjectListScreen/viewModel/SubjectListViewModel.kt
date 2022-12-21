package ru.pgk63.feature_subject.screens.subjectListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.paging.SubjectPagingSource
import ru.pgk63.core_common.api.subject.repository.SubjectRepository
import javax.inject.Inject

@HiltViewModel
internal class SubjectListViewModel @Inject constructor(
    private val subjectRepository: SubjectRepository
): ViewModel() {

    fun getSubjectAll(search: String? = null): Flow<PagingData<Subject>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            SubjectPagingSource(
                search = search,
                subjectRepository = subjectRepository
            )
        }.flow.cachedIn(viewModelScope)
    }
}