package ru.pgk63.core_common.api.raportichka.model

import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.deputyHeadma.model.DeputyHeadma
import ru.pgk63.core_common.api.deputyHeadma.model.RaportichkaRow
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.headman.model.Headman
import ru.pgk63.core_common.api.teacher.model.Teacher
import java.util.Date

data class RaportichkaResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results:List<Raportichka>
)

data class Raportichka(
    val id:Int,
    val date:Date,
    val rowsCount:Int,
    val group:Group,
    val department:Department,
    val classroomTeacher: Teacher,
    val headman: Headman,
    val deputyHeadma: DeputyHeadma,
    val rows: List<RaportichkaRow>
)