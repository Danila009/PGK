package ru.pgk63.core_common.kotlinxSerialization

import android.annotation.SuppressLint
import kotlinx.serialization.ExperimentalSerializationApi
import kotlinx.serialization.KSerializer
import kotlinx.serialization.Serializer
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder
import java.text.SimpleDateFormat
import java.util.*

@OptIn(ExperimentalSerializationApi::class)
@Serializer(forClass = String::class)
object DateSerialization : KSerializer<String> {

    @SuppressLint("SimpleDateFormat")
    override fun deserialize(decoder: Decoder): String {
        val df = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss")
        val parse = df.parse(decoder.decodeString().replace(" UTC", ""))
        val format = SimpleDateFormat("HH:mm dd.MM.yyyy")
        return format.format(parse!!)
    }

    override fun serialize(encoder: Encoder, value: String) {
        encoder.encodeString(value)
    }
}