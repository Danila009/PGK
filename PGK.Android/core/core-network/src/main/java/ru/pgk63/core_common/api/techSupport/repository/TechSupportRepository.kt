package ru.pgk63.core_common.api.techSupport.repository

import ru.pgk63.core_common.api.techSupport.TechSupportApi
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody
import javax.inject.Inject

class TechSupportRepository @Inject constructor(
    private val techSupportApi: TechSupportApi
) {

    suspend fun sendMessage(body: SendMessageBody) {
        techSupportApi.sendMessage(body)
    }
}