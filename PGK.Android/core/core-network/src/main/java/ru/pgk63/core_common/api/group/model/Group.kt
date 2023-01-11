package ru.pgk63.core_common.api.group.model

import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.student.model.Student
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
    val speciality: Specialization,
    val department: Department,
    val classroomTeacher: Teacher,
    val headman: Student? = null,
    val deputyHeadma: Student? = null
){
    override fun toString(): String {
        return speciality.nameAbbreviation +
                "-${course}" + "$number"
    }
}