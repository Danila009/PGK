package ru.pgk63.core_common.api.department.model

data class CreateDepartmentBody(
    val name:String,
    val departmentHeadId:Int
)