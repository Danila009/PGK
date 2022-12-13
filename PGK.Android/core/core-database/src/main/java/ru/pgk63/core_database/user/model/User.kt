package ru.pgk63.core_database.user.model

@kotlinx.serialization.Serializable
data class User(
    val statusRegistration: Boolean = false,
    val accessToken:String? = null,
    val refreshToken:String? = null,
    val userId: Int? = null,
    val userRole: String? = null,
    val darkMode: Boolean = false,
    val secondaryBackground: String? = null
)