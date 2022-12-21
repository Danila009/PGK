package ru.pgk63.core_common.api.speciality.model

data class SpecializationResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val results: List<Specialization>
)

data class Specialization(
    val id:Int,
    val number:String,
    val name:String,
    val nameAbbreviation:String,
    val qualification:String
)