package ru.pgk63.core_common.api.techSupport.model

import ru.pgk63.core_common.Constants

@kotlinx.serialization.Serializable
data class MessageListParameters(
    var search:String? = null,
    var pin: Boolean? = null,
    val userVisible:String? = null,
    val onlyDate:String? = null,
    val startDate:String? = null,
    val endDate:String? = null,
    val userId:Int? = null,
    val chatId:Int? = null,
    val pageNumber:Int = 1,
    val pageSize:Int = Constants.PAGE_SIZE
)