package ru.pgk63.core_common.api.teacher.model

data class Teacher(
    val id:Int,
    val firstName:String,
    val lastName:String,
    val middleName:String?,
    val email:String,
    val photoUrl:String?
)