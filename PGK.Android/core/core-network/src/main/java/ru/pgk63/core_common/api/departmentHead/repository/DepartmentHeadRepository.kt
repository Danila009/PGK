package ru.pgk63.core_common.api.departmentHead.repository

import ru.pgk63.core_common.api.departmentHead.DepartmentHeadApi
import ru.pgk63.core_common.api.user.model.UserRegistrationBody
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class DepartmentHeadRepository @Inject constructor(
    private val departmentHeadApi: DepartmentHeadApi
): ApiResponse() {

    suspend fun registration(body: UserRegistrationBody) = safeApiCall {
        departmentHeadApi.registration(body)
    }
}