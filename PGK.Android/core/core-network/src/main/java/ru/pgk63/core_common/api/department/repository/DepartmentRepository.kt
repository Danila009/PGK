package ru.pgk63.core_common.api.department.repository

import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.department.DepartmentApi
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.model.DepartmentResponse
import ru.pgk63.core_common.common.response.ApiResponse
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

class DepartmentRepository @Inject constructor(
    private val departmentApi: DepartmentApi
): ApiResponse() {

    suspend fun getAll(
        search:String? = null,
        pageNumber: Int = 1,
        pageSize: Int = Constants.PAGE_SIZE
    ): DepartmentResponse = departmentApi.getAll(search, pageNumber, pageSize)

    suspend fun getById(id:Int): Result<Department> = safeApiCall { departmentApi.getById(id) }
}