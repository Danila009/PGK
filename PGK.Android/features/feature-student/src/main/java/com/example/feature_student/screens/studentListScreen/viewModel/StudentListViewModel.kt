package com.example.feature_student.screens.studentListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.api.student.paging.StudentPagingSource
import ru.pgk63.core_common.api.student.repository.StudentRepository
import javax.inject.Inject

@HiltViewModel
internal class StudentListViewModel @Inject constructor(
    private val studentRepository: StudentRepository
): ViewModel() {

    fun getStudents(): Flow<PagingData<Student>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            StudentPagingSource(
                studentRepository = studentRepository
            )
        }.flow.cachedIn(viewModelScope)
    }

}