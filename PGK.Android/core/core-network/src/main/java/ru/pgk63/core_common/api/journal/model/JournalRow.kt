package ru.pgk63.core_common.api.journal.model

import ru.pgk63.core_common.api.student.model.Student

data class JournalRowResponse(
    val results:List<JournalRow>
)

data class JournalRow(
    val id:Int,
    val student:Student,
    val journalSubject:JournalSubject,
    val columns: List<JournalColumn>
)