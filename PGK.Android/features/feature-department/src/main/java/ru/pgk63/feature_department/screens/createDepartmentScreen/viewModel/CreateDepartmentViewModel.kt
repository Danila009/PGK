package ru.pgk63.feature_department.screens.createDepartmentScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.department.model.CreateDepartmentBody
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.repository.DepartmentRepository
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.departmentHead.repository.DepartmentHeadRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
class CreateDepartmentViewModel @Inject constructor(
    private val departmentRepository: DepartmentRepository,
    private val departmentHeadRepository: DepartmentHeadRepository
): ViewModel() {

    private val _responseDepartmentCreateResult = MutableStateFlow<Result<Department>?>(null)
    val responseDepartmentCreateResult = _responseDepartmentCreateResult.asStateFlow()

    private val _responseDepartmentHeadList = MutableStateFlow<PagingData<DepartmentHead>>(PagingData.empty())
    val responseDepartmentHeadList = _responseDepartmentHeadList.asStateFlow()

    fun createDepartment(body: CreateDepartmentBody){
        viewModelScope.launch {
            _responseDepartmentCreateResult.value = departmentRepository.create(body)
        }
    }

    fun getDepartmentHeadList(search: String? = null){
        viewModelScope.launch {
            departmentHeadRepository.getAll(search = search).cachedIn(viewModelScope).collect {
                _responseDepartmentHeadList.value = it
            }
        }
    }

    fun responseDepartmentCreateResultToNull() {
        viewModelScope.launch {
            _responseDepartmentCreateResult.value = null
        }
    }
}