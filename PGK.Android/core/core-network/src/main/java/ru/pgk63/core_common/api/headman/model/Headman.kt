package ru.pgk63.core_common.api.headman.model

import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.group.model.Group

data class Headman(
    val id:Int,
    val firstName:String,
    val lastName:String,
    val middleName:String? = null,
    val photoUrl:String? = null,
    val group: Group,
    val department: Department
)