package ru.pgk63.core_common.api.techSupport.model

@kotlinx.serialization.Serializable
data class GetMessageListParameters(
    val search:String? = null,
    val pin: Boolean? = null,
    val userVisible:String? = null,
    val onlyDate:String? = null,
    val startDate:String? = null,
    val endDate:String? = null,
    val userId:Int? = null,
    val chatId:Int? = null,
    val pageNumber:Int = 1,
    val pageSize:Int = 20
)