package ru.pgk63.core_common.api.techSupport.model

import ru.pgk63.core_common.kotlinxSerialization.DateSerialization
import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable
import ru.pgk63.core_common.api.user.model.User
import java.util.Date

@Serializable
data class MessageResponse(
    @SerialName("Results")
    val results:List<Message>
)

@Serializable
data class Message(
    @SerialName("Id")
    val id:Int,
    @SerialName("Text")
    val text:String? = null,
    @SerialName("UserVisible")
    val userVisible:Boolean,
    @SerialName("Pin")
    val pin:Boolean,
    @SerialName("Edited")
    val edited:Boolean,
    @[SerialName("EditedDate") Serializable(with = DateSerialization::class)]
    val editedDate:String? = null,
    @[SerialName("Date") Serializable(with = DateSerialization::class)]
    val date:String,
    @SerialName("User")
    val user: User
)