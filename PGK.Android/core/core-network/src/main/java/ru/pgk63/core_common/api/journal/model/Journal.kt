package ru.pgk63.core_common.api.journal.model

import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.deputyHeadma.model.DeputyHeadma
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.headman.model.Headman
import ru.pgk63.core_common.api.teacher.model.Teacher

data class JournalResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results: List<Journal>
)

data class Journal(
    val id:Int,
    val course:Int,
    val semester:Int,
    val group:Group,
    val department:Department,
    val classroomTeacher:Teacher,
    val headman:Headman? = null,
    val deputyHeadma:DeputyHeadma? = null
)