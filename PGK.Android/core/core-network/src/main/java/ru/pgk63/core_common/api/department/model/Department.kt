package ru.pgk63.core_common.api.department.model

import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead

data class Department(
    val id:Int,
    val name:String,
    val departmentHead: DepartmentHead
)