package ru.pgk63.core_common.api.director.repository

import ru.pgk63.core_common.api.director.DirectorApi
import ru.pgk63.core_common.api.user.model.UserRegistrationBody
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class DirectorRepository @Inject constructor(
    private val directorApi: DirectorApi
): ApiResponse() {

    suspend fun registration(body: UserRegistrationBody) = safeApiCall {
        directorApi.registration(body)
    }
}