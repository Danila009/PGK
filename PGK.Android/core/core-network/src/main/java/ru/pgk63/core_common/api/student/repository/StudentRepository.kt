package ru.pgk63.core_common.api.student.repository

import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.student.StudentApi
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class StudentRepository @Inject constructor(
    private val studentApi: StudentApi
): ApiResponse() {

    suspend fun getAll(
        search:String? = null,
        pageNumber: Int = 1,
        pageSize: Int = Constants.PAGE_SIZE
    ) = studentApi.getAll(search, pageNumber, pageSize)
}