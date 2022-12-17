package ru.pgk63.core_database.user.model

@kotlinx.serialization.Serializable
data class UserLocalDatabase(
    val statusRegistration: Boolean = false,
    val userId: Int? = null,
    val userRole: String? = null,
    val darkMode: Boolean? = null,
    val secondaryBackground: String? = null
)