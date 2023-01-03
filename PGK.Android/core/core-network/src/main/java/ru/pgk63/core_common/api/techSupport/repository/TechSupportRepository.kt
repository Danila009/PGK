package ru.pgk63.core_common.api.techSupport.repository

import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.techSupport.TechSupportApi
import ru.pgk63.core_common.api.techSupport.model.ChatResponse
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody
import ru.pgk63.core_common.api.techSupport.model.UpdateMessageBody
import javax.inject.Inject

class TechSupportRepository @Inject constructor(
    private val techSupportApi: TechSupportApi
) {

    suspend fun getChatAll(pageNumber: Int, pageSize: Int = PAGE_SIZE): ChatResponse {
        return techSupportApi.getChatAll(pageNumber, pageSize)
    }

    suspend fun sendMessage(body: SendMessageBody) {
        techSupportApi.sendMessage(body)
    }

    suspend fun pinMessage(messageId: Int) = techSupportApi.pinMessage(messageId)

    suspend fun deleteMessage(messageId: Int) = techSupportApi.deleteMessage(messageId)

    suspend fun updateMessage(messageId: Int, body: UpdateMessageBody) {
        techSupportApi.updateMessage(messageId, body)
    }
}