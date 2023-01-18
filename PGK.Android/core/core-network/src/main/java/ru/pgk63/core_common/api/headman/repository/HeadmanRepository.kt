package ru.pgk63.core_common.api.headman.repository

import ru.pgk63.core_common.api.headman.HeadmanApi
import ru.pgk63.core_common.api.headman.model.HeadmanRegistrationBody
import ru.pgk63.core_common.api.headman.model.HeadmanUpdateRaportichkaRowBody
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class HeadmanRepository @Inject constructor(
    private val headmanApi: HeadmanApi
): ApiResponse() {

    suspend fun registration(body: HeadmanRegistrationBody) = safeApiCall {
        headmanApi.registration(body)
    }

    suspend fun registrationDeputy(body: HeadmanRegistrationBody) = safeApiCall {
        headmanApi.registrationDeputy(body)
    }

    suspend fun updateRaportichkaRow(
        rowId:Int,
        body: HeadmanUpdateRaportichkaRowBody
    ) = safeApiCall { headmanApi.updateRaportichkaRow(rowId, body) }

    suspend fun createRaportichka() = safeApiCall { headmanApi.createRaportichka() }
}