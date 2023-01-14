package ru.pgk63.feature_department.screens.departmentListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.repository.DepartmentRepository
import javax.inject.Inject

@HiltViewModel
internal class DepartmentListViewModel @Inject constructor(
    private val departmentRepository: DepartmentRepository
): ViewModel() {

    private val _responseDepartmentList = MutableStateFlow<PagingData<Department>>(PagingData.empty())
    val responseDepartmentList = _responseDepartmentList.asStateFlow()

    fun getDepartments(search:String? = null) {
        viewModelScope.launch {
            departmentRepository.getAll(search = search).cachedIn(viewModelScope).collect {
                _responseDepartmentList.value = it
            }
        }
    }
}