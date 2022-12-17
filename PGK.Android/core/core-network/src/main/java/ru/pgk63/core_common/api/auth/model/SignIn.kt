package ru.pgk63.core_common.api.auth.model

import com.google.gson.annotations.SerializedName

data class SignIn(
    val firstName:String,
    val lastName:String,
    val password:String
)

data class SignInResponse(
    val accessToken:String,
    val refreshToken:String,
    val userId:Int,
    val userRole:String,
    @SerializedName("drarkMode")
    val darkMode: Boolean,
    val secondaryBackground:String,
    @SerializedName("Message")
    val errorMessage:String? = null,
    @SerializedName("Code")
    val errorCode: Int? = null
)