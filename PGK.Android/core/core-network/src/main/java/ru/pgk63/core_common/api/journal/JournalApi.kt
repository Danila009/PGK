package ru.pgk63.core_common.api.journal

import retrofit2.http.GET
import retrofit2.http.Query
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.journal.model.*

interface JournalApi {

    @GET("/pgk63/api/Journal")
    suspend fun getAll(
        @Query("course") course:List<Int>?,
        @Query("semesters") semesters:List<Int>?,
        @Query("groupIds") groupIds:List<Int>?,
        @Query("specialityIds") specialityIds:List<Int>?,
        @Query("departmentIds") departmentIds:List<Int>?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalResponse

    @GET("/pgk63/api/Journal/Subject")
    suspend fun getJournalSubjects(
        @Query("journalId") journalId:Int?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalSubjectResponse

    @GET("/pgk63/api/Journal/Subject/Topic")
    suspend fun getJournalTopic(
        @Query("journalSubjectId") journalSubjectId:Int?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalTopicResponse

    @GET("/pgk63/api/Journal/Subject/Row/Column")
    suspend fun getJournalColumn(
        @Query("journalRowId") journalRowId:Int?,
        @Query("studentIds") studentIds:List<Int>?,
        @Query("evaluation") evaluation:JournalEvaluation?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalColumnResponse

    @GET("/pgk63/api/Journal/Subject/Row")
    suspend fun getJournalRow(
        @Query("journalSubjectId") journalSubjectId:Int?,
        @Query("studentIds") studentIds:List<Int>?,
        @Query("evaluation") evaluation:JournalEvaluation?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalRowResponse
}