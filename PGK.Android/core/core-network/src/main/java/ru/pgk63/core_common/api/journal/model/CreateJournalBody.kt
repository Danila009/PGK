package ru.pgk63.core_common.api.journal.model

data class CreateJournalBody(
    val course:Int,
    val semester:Int,
    val groupId:Int
)