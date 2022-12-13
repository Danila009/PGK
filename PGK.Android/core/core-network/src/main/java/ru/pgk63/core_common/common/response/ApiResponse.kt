package ru.pgk63.core_common.common.response

import retrofit2.Response

abstract class ApiResponse {
    suspend fun <T> safeApiCall(apiCall: suspend () -> Response<T>): Result<T> {
        try {
            val response = apiCall()
            if (response.isSuccessful) {
                val body = response.body()
                return Result.Success(body)
            }
            return Result.Error("${response.code()} ${response.message()}")
        } catch (e: Exception) {
            return Result.Error(e.message.toString())
        }
    }
}