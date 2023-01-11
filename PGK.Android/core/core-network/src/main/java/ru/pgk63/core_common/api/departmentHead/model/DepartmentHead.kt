package ru.pgk63.core_common.api.departmentHead.model

data class DepartmentHead(
    val id:Int,
    val firstName:String,
    val lastName:String,
    val middleName:String?,
    val photoUrl:String?
){
    fun fioAbbreviated():String{

        val middleName = if(middleName?.getOrNull(0) == null)
            ""
        else
            "${middleName[0]}."

        return lastName + " ${firstName[0]}." + " $middleName."
    }

    fun fio():String = lastName + " $firstName " + (middleName ?: "")

}