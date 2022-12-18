package ru.pgk63.core_common.api.user.repository

import ru.pgk63.core_common.api.user.UserApi
import ru.pgk63.core_common.api.user.model.ResponseUpdateDarkMode
import ru.pgk63.core_common.common.response.ApiResponse
import ru.pgk63.core_database.user.UserDataSource
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

class UserRepository @Inject constructor(
    private val userApi: UserApi,
    private val userDataSource: UserDataSource
): ApiResponse() {
    suspend fun updateDarkMode(): Result<ResponseUpdateDarkMode> {

        val result = safeApiCall { userApi.updateDarkMode() }

        if(result is Result.Success){
            val darkMode = result.data?.darkMode

            darkMode?.let { userDataSource.saveDarkModel(darkMode) }
        }

        return result
    }
}