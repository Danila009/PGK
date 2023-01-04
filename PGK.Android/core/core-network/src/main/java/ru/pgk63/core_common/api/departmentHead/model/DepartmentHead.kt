package ru.pgk63.core_common.api.departmentHead.model

data class DepartmentHead(
    val id:Int,
    val firstName:String,
    val lastName:String,
    val middleName:String?,
    val photoUrl:String?
)