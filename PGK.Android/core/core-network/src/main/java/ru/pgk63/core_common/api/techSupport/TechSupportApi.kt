package ru.pgk63.core_common.api.techSupport

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.PATCH
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import retrofit2.http.Query
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.techSupport.model.ChatResponse
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody
import ru.pgk63.core_common.api.techSupport.model.UpdateMessageBody

interface TechSupportApi {

    @GET("/pgk63/api/TechnicalSupport/Chat")
    suspend fun getChatAll(
        @Query("pageNumber") pageNumber: Int,
        @Query("pageSize") pageSize: Int = PAGE_SIZE
    ): ChatResponse

    @POST("/pgk63/api/TechnicalSupport/Chat/Message")
    suspend fun sendMessage(@Body body: SendMessageBody): Response<Unit>

    @PATCH("/pgk63/api/TechnicalSupport/Chat/Message/{id}/Pin")
    suspend fun pinMessage(@Path("id") messageId: Int)

    @DELETE("/pgk63/api/TechnicalSupport/Chat/Message/{id}")
    suspend fun deleteMessage(@Path("id") messageId: Int)

    @PUT("/pgk63/api/TechnicalSupport/Chat/Message/{id}")
    suspend fun updateMessage(@Path("id") messageId: Int, @Body body: UpdateMessageBody)
}