package ru.pgk63.core_common.api.techSupport

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody

interface TechSupportApi {

    @POST("/pgk63/api/TechnicalSupport/Chat/Message")
    suspend fun sendMessage(@Body body: SendMessageBody): Response<Unit>
}