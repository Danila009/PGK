package ru.pgk63.core_common.api.department

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.department.model.CreateDepartmentBody
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.model.DepartmentResponse

interface DepartmentApi {

    @GET("/pgk63/api/Department")
    suspend fun getAll(
        @Query("search") search:String? = null,
        @Query("pageNumber") pageNumber: Int = 1,
        @Query("pageSize") pageSize: Int = Constants.PAGE_SIZE
    ): DepartmentResponse

    @GET("/pgk63/api/Department/{id}")
    suspend fun getById(@Path("id") id:Int): Response<Department>

    @POST("/pgk63/api/Department")
    suspend fun create(@Body body: CreateDepartmentBody): Response<Department>
}