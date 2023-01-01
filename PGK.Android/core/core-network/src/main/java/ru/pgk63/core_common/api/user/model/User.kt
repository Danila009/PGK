package ru.pgk63.core_common.api.user.model

@kotlinx.serialization.Serializable
data class User(
    val id:Int = 0,
    val firstName:String = "",
    val lastName:String = "",
    val middleName:String? = null,
    val photoUrl:String? = null
)