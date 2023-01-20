package ru.pgk63.core_common.common.response

import retrofit2.Response
import java.net.ProtocolException

abstract class ApiResponse {
    suspend fun <T> safeApiCall(apiCall: suspend () -> Response<T>): Result<T> {
        try {
            val response = apiCall()
            if (response.isSuccessful) {
                val body = response.body()
                return Result.Success(body)
            }

            return Result.Error("${response.code()} ${response.message()}")
        } catch (e: ProtocolException) {
            return Result.Error("Ошибка, возможно вам нужно заново авторезироваться это можно сделать в настройках приложения")
        } catch (e: Exception) {
            return Result.Error(e.message.toString())
        }
    }
}