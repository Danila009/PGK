package ru.pgk63.core_common.api.departmentHead

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Query
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.departmentHead.model.DepartmentResponse
import ru.pgk63.core_common.api.user.model.UserRegistrationBody
import ru.pgk63.core_common.api.user.model.UserRegistrationResponse

interface DepartmentHeadApi {

    @POST("/pgk63/api/DepartmentHead/Registration")
    suspend fun registration(
        @Body body: UserRegistrationBody
    ): Response<UserRegistrationResponse>

    @GET("/pgk63/api/DepartmentHead")
    suspend fun getAll(
        @Query("search") search: String?,
        @Query("pageNumber") pageNumber: Int,
        @Query("pageSize") pageSize: Int = PAGE_SIZE
    ): DepartmentResponse
}