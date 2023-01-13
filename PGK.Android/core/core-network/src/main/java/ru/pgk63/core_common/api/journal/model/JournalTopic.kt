package ru.pgk63.core_common.api.journal.model

import java.util.Date

data class JournalTopicResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results:List<JournalTopic>
)

data class JournalTopic(
    val id:Int,
    val title:String,
    val homeWork:String,
    val hours:Int,
    val date:Date
)