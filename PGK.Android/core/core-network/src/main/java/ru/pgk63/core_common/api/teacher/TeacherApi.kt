package ru.pgk63.core_common.api.teacher

import retrofit2.http.GET
import retrofit2.http.Query
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.teacher.model.ResponseTeacher

interface TeacherApi {

    @GET("/pgk63/api/Teacher")
    suspend fun getAll(
        @Query("search") search:String? = null,
        @Query("subjectIds") subjectIds:List<Int>? = null,
        @Query("pageNumber") pageNumber: Int = 1,
        @Query("pageSize") pageSize: Int = Constants.PAGE_SIZE
    ): ResponseTeacher
}