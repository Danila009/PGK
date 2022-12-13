package ru.pgk63.core_common.api.group

import retrofit2.http.GET
import retrofit2.http.Query
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.group.model.GroupResponse

interface GroupApi {

    @GET("/Group")
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
    ): List<GroupResponse>
}