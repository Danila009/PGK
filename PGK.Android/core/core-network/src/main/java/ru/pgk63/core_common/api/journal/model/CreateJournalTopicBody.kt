package ru.pgk63.core_common.api.journal.model

import java.util.Date

data class CreateJournalTopicBody(
    val title:String,
    val homeWork:String?,
    val hours:Int,
    val date: Date = Date()
)