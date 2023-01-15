package ru.pgk63.core_common.api.search.model

import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.deputyHeadma.model.DeputyHeadma
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.headman.model.Headman
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_common.api.user.model.User

data class SearchResponse(
    val students:List<Student>,
    val headmens:List<Headman>,
    val departmentHead:List<DepartmentHead>,
    val teachers:List<Teacher>,
    val educationalSectors:List<User>,
    val deputyHeadman:List<DeputyHeadma>,
    val admins:List<User>,
    val departments:List<Department>,
    val groups:List<Group>,
    val specialities:List<Specialization>,
    val subjects:List<Subject>
)