package ru.pgk63.core_common.api.headman.model

data class HeadmanUpdateRaportichkaRowBody(
    val numberLesson:Int,
    val hours:Int,
    val subjectId:Int,
    val teacherId:Int,
    val studentId:Int,
    val raportichkaId:Int
)