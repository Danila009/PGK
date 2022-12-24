package ru.pgk63.core_common.api.auth

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST
import ru.pgk63.core_common.api.auth.model.SignIn
import ru.pgk63.core_common.api.auth.model.SignInResponse

interface AuthApi {

    @POST("/pgk63/api/Auth/SignIn")
    suspend fun signIn(@Body body: SignIn) : Response<SignInResponse>

    @POST("/pgk63/api/Auth/Revoke")
    suspend fun revokeRefreshToken(@Header("refreshToken") refreshToken:String):Response<Unit?>
}