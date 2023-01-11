package ru.pgk63.core_common.api.raportichka.repository

import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.raportichka.RaportichkaApi
import ru.pgk63.core_common.api.raportichka.model.RaportichkaAddRowBody
import ru.pgk63.core_common.api.raportichka.model.RaportichkaResponse
import ru.pgk63.core_common.api.raportichka.model.RaportichkaUpdateRowBody
import ru.pgk63.core_common.common.response.ApiResponse
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.extension.isNull
import javax.inject.Inject

class RaportichkaRepository @Inject constructor(
    private val raportichkaApi: RaportichkaApi
): ApiResponse() {

    suspend fun getRaportichkaAll(
        confirmation:Boolean? = null ,
        onlyDate:String? = null,
        startDate:String? = null,
        endDate:String? = null,
        groupIds:List<Int>? = null,
        subjectIds:List<Int>? = null,
        classroomTeacherIds:List<Int>? = null,
        numberLessons:List<Int>? = null,
        teacherIds:List<Int>? = null,
        studentIds:List<Int>? = null,
        pageNumber:Int? = null,
        pageSize:Int = Constants.PAGE_SIZE
    ): RaportichkaResponse {
        return raportichkaApi.getRaportichkaAll(
            confirmation = confirmation,
            onlyDate = onlyDate.isNull(),
            startDate = startDate.isNull(),
            endDate = endDate.isNull(),
            groupIds = groupIds,
            subjectIds = subjectIds,
            classroomTeacherIds = classroomTeacherIds,
            numberLessons = numberLessons,
            teacherIds = teacherIds,
            studentIds = studentIds,
            pageNumber = pageNumber,
            pageSize = pageSize
        )
    }

    suspend fun raportichkaAddRow(
        raportichkaId:Int,
        body: RaportichkaAddRowBody
    ): Result<Unit?> = safeApiCall { raportichkaApi.raportichkaAddRow(raportichkaId, body) }

    suspend fun updateRow(
        rowId:Int,
        body: RaportichkaUpdateRowBody
    ) = safeApiCall { raportichkaApi.updateRow(rowId, body) }

    suspend fun deleteRow(rowId:Int) = safeApiCall { raportichkaApi.deleteRow(rowId) }
}