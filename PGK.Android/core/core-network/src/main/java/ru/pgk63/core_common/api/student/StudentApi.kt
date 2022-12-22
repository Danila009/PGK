package ru.pgk63.core_common.api.student

import retrofit2.http.GET
import retrofit2.http.Query
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.student.model.StudentResponse

interface StudentApi {

    @GET("/pgk63/api/Student")
    suspend fun getAll(
        @Query("search") search:String? = null,
        @Query("pageNumber") pageNumber: Int = 1,
        @Query("pageSize") pageSize: Int = Constants.PAGE_SIZE
    ): StudentResponse
}