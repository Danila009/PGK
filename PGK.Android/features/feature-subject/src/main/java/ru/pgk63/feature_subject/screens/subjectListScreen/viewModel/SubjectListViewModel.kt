package ru.pgk63.feature_subject.screens.subjectListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.subject.model.CreateSubjectBody
import ru.pgk63.core_common.api.subject.model.CreateSubjectResponse
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.repository.SubjectRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
internal class SubjectListViewModel @Inject constructor(
    private val subjectRepository: SubjectRepository
): ViewModel() {

    private val _responseSubject = MutableStateFlow<PagingData<Subject>>(PagingData.empty())
    val responseSubject = _responseSubject.asStateFlow()

    private val _responseCreateSubjectResult = MutableStateFlow<Result<CreateSubjectResponse>?>(null)
    val responseCreateSubjectResult = _responseCreateSubjectResult.asStateFlow()

    fun getSubjectAll(search: String? = null) {
        viewModelScope.launch {
            subjectRepository.getAll(search).cachedIn(viewModelScope).collect {
                _responseSubject.value = it
            }
        }
    }

    fun createSubject(body: CreateSubjectBody) {
        viewModelScope.launch {
            _responseCreateSubjectResult.value = subjectRepository.create(body)
        }
    }

    fun createSubjectResultTNull() {
        viewModelScope.launch {
            _responseCreateSubjectResult.value = null
        }
    }
}