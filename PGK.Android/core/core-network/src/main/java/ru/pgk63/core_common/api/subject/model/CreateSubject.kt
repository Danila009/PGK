package ru.pgk63.core_common.api.subject.model

data class CreateSubjectBody(
    val subjectTitle:String
)

data class CreateSubjectResponse(
    val id: Int
)