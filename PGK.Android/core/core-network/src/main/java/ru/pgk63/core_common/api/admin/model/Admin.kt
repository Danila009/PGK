package ru.pgk63.core_common.api.admin.model

data class Admin(
    val id:Int = 0,
    val firstName:String = "",
    val lastName:String = "",
    val middleName:String? = null,
    val photoUrl:String? = null
)