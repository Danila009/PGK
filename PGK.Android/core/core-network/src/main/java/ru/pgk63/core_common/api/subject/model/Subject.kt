package ru.pgk63.core_common.api.subject.model

data class SubjectResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results: List<Subject>
)

data class Subject(
    val id:Int,
    val subjectTitle:String
){
    override fun toString(): String {
        return subjectTitle
    }
}