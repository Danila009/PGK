package ru.pgk63.core_common.api.journal.model

import ru.pgk63.core_common.api.student.model.Student
import java.util.Date

data class JournalColumnResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results:List<JournalColumn>
)

data class JournalColumn(
    val id:Int,
    val evaluation:JournalEvaluation,
    val date:Date,
    val row: JournalRow
)