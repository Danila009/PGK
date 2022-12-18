package ru.pgk63.core_common.api.group.model

import ru.pgk63.core_common.api.speciality.model.Speciality
import ru.pgk63.core_common.api.teacher.model.Teacher

data class GroupResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results:List<Group>
)

data class Group(
    val id:Int,
    val course:Int,
    val number:Int,
    val speciality: Speciality,
    val classroomTeacher: Teacher
)