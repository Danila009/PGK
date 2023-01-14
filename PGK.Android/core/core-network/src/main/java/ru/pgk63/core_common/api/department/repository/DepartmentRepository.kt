package ru.pgk63.core_common.api.department.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.department.DepartmentApi
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.paging.DepartmentPagingSource
import ru.pgk63.core_common.common.response.ApiResponse
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

class DepartmentRepository @Inject constructor(
    private val departmentApi: DepartmentApi
): ApiResponse() {

    fun getAll(
        search:String? = null
    ): Flow<PagingData<Department>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            DepartmentPagingSource(
                departmentApi = departmentApi,
                search = search
            )
        }.flow
    }

    suspend fun getById(id:Int): Result<Department> = safeApiCall { departmentApi.getById(id) }
}