package ru.pgk63.core_common.api.auth

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST
import ru.pgk63.core_common.api.auth.model.SignIn
import ru.pgk63.core_common.api.auth.model.SignInResponse

interface AuthApi {

    @POST("/Auth/SignIn")
    suspend fun signId(@Body signIn: SignIn) : Response<SignInResponse>
}