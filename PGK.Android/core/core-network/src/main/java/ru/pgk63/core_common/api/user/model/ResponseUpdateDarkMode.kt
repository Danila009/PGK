package ru.pgk63.core_common.api.user.model

import com.google.gson.annotations.SerializedName

data class ResponseUpdateDarkMode(
    @SerializedName("drarkMode")
    val darkMode:Boolean
)