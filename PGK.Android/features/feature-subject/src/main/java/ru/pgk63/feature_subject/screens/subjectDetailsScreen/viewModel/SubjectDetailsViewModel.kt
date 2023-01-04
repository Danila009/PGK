package ru.pgk63.feature_subject.screens.subjectDetailsScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.repository.SubjectRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
class SubjectDetailsViewModel @Inject constructor(
    private val subjectRepository: SubjectRepository
): ViewModel() {

    private val _responseSubject = MutableStateFlow<Result<Subject>>(Result.Loading())
    val responseSubject = _responseSubject.asStateFlow()

    fun getSubjectById(id:Int){
        viewModelScope.launch {
            _responseSubject.value = subjectRepository.getById(id)
        }
    }
}