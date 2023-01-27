package ru.pgk63.core_common.api.journal.model

data class CreateJournalColumnBody(
    val journalSubjectRowId: Int?,
    val studentId:Int?,
    val journalSubjectId:Int?,
    val evaluation: JournalEvaluation,
    val date:String
)