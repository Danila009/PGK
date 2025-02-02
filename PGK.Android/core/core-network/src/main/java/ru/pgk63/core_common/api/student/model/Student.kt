package ru.pgk63.core_common.api.student.model

import ru.pgk63.core_common.api.group.model.Group

data class StudentResponse(
    val currentPage:Int,
    val totalPages:Int,
    val pageSize:Int,
    val totalCount:Int,
    val hasPrevious:Boolean,
    val hasNext:Boolean,
    val results: List<Student>
)

data class Student(
    val id:Int,
    val firstName:String,
    val lastName:String,
    val middleName:String? = null,
    val photoUrl:String? = null,
    val group:Group
){
    override fun toString(): String {
        return fioAbbreviated()
    }

    fun fioAbbreviated():String{

        val correctMiddleName = if(middleName == null || middleName.isEmpty())
            ""
        else
            " ${middleName[0]}."

        return lastName + " ${firstName[0]}." + correctMiddleName
    }

    fun fio():String = lastName + " $firstName " + (middleName ?: "")

}