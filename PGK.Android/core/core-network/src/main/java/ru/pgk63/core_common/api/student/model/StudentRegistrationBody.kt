package ru.pgk63.core_common.api.student.model

data class StudentRegistrationBody(
    val firstName:String,
    val lastName:String,
    val middleName:String?,
    val groupId:Int
)