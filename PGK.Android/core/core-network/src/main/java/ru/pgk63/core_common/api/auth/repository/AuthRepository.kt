package ru.pgk63.core_common.api.auth.repository

import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.api.auth.AuthApi
import ru.pgk63.core_common.api.auth.model.SignIn
import ru.pgk63.core_common.api.auth.model.SignInResponse
import ru.pgk63.core_common.common.response.ApiResponse
import ru.pgk63.core_database.user.UserDataSource
import ru.pgk63.core_database.user.model.UserLocalDatabase
import javax.inject.Inject

class AuthRepository @Inject constructor(
    private val authApi: AuthApi,
    private val userDataSource: UserDataSource
): ApiResponse() {

    suspend fun signIn(body: SignIn): Result<SignInResponse> {
        val response = safeApiCall { authApi.signIn(body) }

        if(response is Result.Success){
            val userLocalDatabase = UserLocalDatabase(
                statusRegistration = true,
                userId = response.data?.userId,
                userRole = response.data?.userRole,
                darkMode = response.data?.darkMode,
                secondaryBackground = response.data?.secondaryBackground
            )

            userDataSource.save(userLocalDatabase)
            userDataSource.saveAccessToken(response.data?.accessToken)
            userDataSource.saveRefreshToken(response.data?.refreshToken)
        }

        return response
    }
}