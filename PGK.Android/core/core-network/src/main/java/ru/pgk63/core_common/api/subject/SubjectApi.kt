package ru.pgk63.core_common.api.subject

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.model.SubjectResponse

interface SubjectApi {

    @GET("/pgk63/api/Subject")
    suspend fun getAll(
        @Query("search") search:String? = null,
        @Query("pageNumber") pageNumber: Int = 1,
        @Query("pageSize") pageSize: Int = Constants.PAGE_SIZE
    ): SubjectResponse

    @GET("/pgk63/api/Subject/{id}")
    suspend fun getById(@Path("id") id:Int): Response<Subject>
}