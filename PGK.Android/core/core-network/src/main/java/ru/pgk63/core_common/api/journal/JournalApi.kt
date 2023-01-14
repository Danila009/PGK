package ru.pgk63.core_common.api.journal

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.PATCH
import retrofit2.http.POST
import retrofit2.http.Path
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

    @POST("/pgk63/api/Journal")
    suspend fun create(
        @Body body: CreateJournalBody
    ): Response<Unit?>

    @DELETE("/pgk63/api/Journal/{id}")
    suspend fun delete(
        @Path("id") id:Int
    ): Response<Unit?>

    @GET("/pgk63/api/Journal/Subject")
    suspend fun getJournalSubjects(
        @Query("journalId") journalId:Int?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalSubjectResponse

    @POST("/pgk63/api/Journal/{id}/Subject")
    suspend fun createJournalSubject(
        @Path("id") journalId: Int,
        @Body body: CreateJournalSubjectBody
    ): Response<Unit?>

    @DELETE("/pgk63/api/Journal/Subject/{id}")
    suspend fun deleteJournalSubject(
        @Path("id") journalSubjectId: Int,
    ): Response<Unit?>

    @GET("/pgk63/api/Journal/Subject/Topic")
    suspend fun getJournalTopic(
        @Query("journalSubjectId") journalSubjectId:Int?,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): JournalTopicResponse

    @POST("/pgk63/api/Journal/Subject/{id}/Topic")
    suspend fun createJournalTopic(
        @Path("id") journalSubjectId: Int,
        @Body body: CreateJournalTopicBody
    ): Response<Unit?>

    @DELETE("/pgk63/api/Journal/Subject/Topic/{id}")
    suspend fun deleteJournalTopic(
        @Path("id") journalTopicId: Int,
    ): Response<Unit?>

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

    @PATCH("/pgk63/api/Journal/Subject/Row/Column/{id}/Evaluation")
    suspend fun updateEvaluation(
        @Path("id") columnId: Int,
        @Query("evaluation") evaluation: JournalEvaluation
    ): Response<Unit?>

    @DELETE("/pgk63/api/Journal/Subject/Row/Column/{id}")
    suspend fun deleteColumn(
        @Path("id") columnId: Int,
    ): Response<Unit?>
}