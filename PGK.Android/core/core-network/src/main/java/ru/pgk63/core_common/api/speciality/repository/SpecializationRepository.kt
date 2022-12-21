package ru.pgk63.core_common.api.speciality.repository

import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.speciality.SpecializationApi
import ru.pgk63.core_common.api.speciality.model.SpecializationResponse
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class SpecializationRepository @Inject constructor(
    private val specializationApi: SpecializationApi
): ApiResponse() {

    suspend fun getAll(
        search:String? = null,
        pageNumber: Int = 1,
        pageSize: Int = Constants.PAGE_SIZE
    ): SpecializationResponse = specializationApi.getAll(search,pageNumber, pageSize)

    suspend fun getById(id:Int) = safeApiCall { specializationApi.getById(id) }
}