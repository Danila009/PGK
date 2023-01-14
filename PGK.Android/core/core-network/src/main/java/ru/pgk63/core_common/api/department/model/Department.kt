package ru.pgk63.core_common.api.department.model

import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead

data class DepartmentResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val results: List<Department>
)

data class Department(
    val id:Int,
    val name:String,
    val departmentHead: DepartmentHead
){
    override fun toString(): String {
        return name
    }
}