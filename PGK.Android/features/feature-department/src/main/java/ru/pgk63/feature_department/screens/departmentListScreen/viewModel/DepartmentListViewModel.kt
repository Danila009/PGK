package ru.pgk63.feature_department.screens.departmentListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.paging.DepartmentPagingSource
import ru.pgk63.core_common.api.department.repository.DepartmentRepository
import javax.inject.Inject

@HiltViewModel
internal class DepartmentListViewModel @Inject constructor(
    private val departmentRepository: DepartmentRepository
): ViewModel() {

    fun getDepartments(search:String? = null): Flow<PagingData<Department>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            DepartmentPagingSource(
                departmentRepository = departmentRepository,
                search = search
            )
        }.flow.cachedIn(viewModelScope)
    }
}