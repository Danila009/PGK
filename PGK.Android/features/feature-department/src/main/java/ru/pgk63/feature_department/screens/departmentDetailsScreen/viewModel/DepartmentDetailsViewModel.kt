package ru.pgk63.feature_department.screens.departmentDetailsScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.repository.DepartmentRepository
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.group.paging.GroupPagingSource
import ru.pgk63.core_common.api.group.repository.GroupRepository
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.paging.SpecializationPagingSource
import ru.pgk63.core_common.api.speciality.repository.SpecializationRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
internal class DepartmentDetailsViewModel @Inject constructor(
    private val departmentRepository: DepartmentRepository,
    private val specializationRepository: SpecializationRepository,
    private val groupRepository: GroupRepository
): ViewModel() {

    private val _responseDepartment = MutableStateFlow<Result<Department>>(Result.Loading())
    val responseDepartment = _responseDepartment.asStateFlow()

    fun getDepartmentById(id:Int) {
        viewModelScope.launch {
            _responseDepartment.value = departmentRepository.getById(id = id)
        }
    }

    fun getSpecialization(departmentId: Int): Flow<PagingData<Specialization>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            SpecializationPagingSource(
                specializationRepository = specializationRepository
            )
        }.flow.cachedIn(viewModelScope)
    }

    fun getGroups(departmentId: Int): Flow<PagingData<Group>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            GroupPagingSource(
                groupRepository = groupRepository,
                departmentIds = listOf(departmentId)
            )
        }.flow.cachedIn(viewModelScope)
    }
}