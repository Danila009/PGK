package ru.pgk63.core_common.api.user

import retrofit2.Response
import retrofit2.http.PATCH
import ru.pgk63.core_common.api.user.model.ResponseUpdateDarkMode

interface UserApi {

    @PATCH("/pgk63/api/User/Settings/DrarkMode")
    suspend fun updateDarkMode(): Response<ResponseUpdateDarkMode>
}