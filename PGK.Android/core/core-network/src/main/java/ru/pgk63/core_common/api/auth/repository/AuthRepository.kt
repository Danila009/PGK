package ru.pgk63.core_common.api.auth.repository

import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.api.auth.AuthApi
import ru.pgk63.core_common.api.auth.model.SignIn
import ru.pgk63.core_common.api.auth.model.SignInResponse
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class AuthRepository @Inject constructor(
    private val authApi: AuthApi
): ApiResponse() {

    suspend fun signId(signIn: SignIn): Result<SignInResponse> {
        return safeApiCall { authApi.signId(signIn) }
    }
}