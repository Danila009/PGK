package com.example.feature_guide.screens.guideListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.departmentHead.repository.DepartmentHeadRepository
import ru.pgk63.core_common.api.director.model.Director
import ru.pgk63.core_common.api.director.repository.DirectorRepository
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_common.api.teacher.repository.TeacherRepository
import javax.inject.Inject

@HiltViewModel
internal class GuideListViewModel @Inject constructor(
    private val departmentHeadRepository: DepartmentHeadRepository,
    private val directorRepository: DirectorRepository,
    private val teacherRepository: TeacherRepository
): ViewModel() {

    private val _responseDepartmentHeadList = MutableStateFlow<PagingData<DepartmentHead>>(PagingData.empty())
    val responseDepartmentHeadList = _responseDepartmentHeadList.asStateFlow()

    private val _responseDirectorList = MutableStateFlow<PagingData<Director>>(PagingData.empty())
    val responseDirectorList = _responseDirectorList.asStateFlow()

    private val _responseTeacherList = MutableStateFlow<PagingData<Teacher>>(PagingData.empty())
    val responseTeacherList = _responseTeacherList.asStateFlow()

    fun getDepartmentHeadList() {
        viewModelScope.launch {
            departmentHeadRepository.getAll().cachedIn(viewModelScope).collect {
                _responseDepartmentHeadList.value = it
            }
        }
    }

    fun getDirectorsList() {
        viewModelScope.launch {
            directorRepository.getAll().cachedIn(viewModelScope).collect {
                _responseDirectorList.value = it
            }
        }
    }

    fun getTeacherList() {
        viewModelScope.launch {
            teacherRepository.getAll().cachedIn(viewModelScope).collect {
                _responseTeacherList.value = it
            }
        }
    }
}