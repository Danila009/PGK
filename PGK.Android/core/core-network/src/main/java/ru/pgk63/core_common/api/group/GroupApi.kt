package ru.pgk63.core_common.api.group

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.group.model.GroupResponse
import ru.pgk63.core_common.api.student.model.StudentResponse

interface GroupApi {

    @GET("/pgk63/api/Group")
    suspend fun getAll(
        @Query("search") search:String? = null,
        @Query("course") course: List<Int>? = null,
        @Query("number") number:List<Int>? = null,
        @Query("specialityIds") specialityIds: List<Int>? = null,
        @Query("departmentIds") departmentIds: List<Int>? = null,
        @Query("classroomTeacherIds") classroomTeacherIds: List<Int>? = null,
        @Query("deputyHeadmaIds") deputyHeadmaIds: List<Int>? = null,
        @Query("headmanIds") headmanIds: List<Int>? = null,
        @Query("pageNumber") pageNumber: Int = 1,
        @Query("pageSize") pageSize: Int = PAGE_SIZE
    ): GroupResponse

    @GET("/pgk63/api/Group/{id}")
    suspend fun getById(@Path("id") id: Int): Response<Group>

    @GET("/pgk63/api/Group/{id}/Students")
    suspend fun getStudentByGroupId(
        @Path("id") id: Int,
        @Query("pageNumber") pageNumber: Int = 1,
        @Query("pageSize") pageSize: Int = PAGE_SIZE
    ): StudentResponse
}