package ru.pgk63.core_common.api.subject.repository

import ru.pgk63.core_common.api.subject.SubjectApi
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class SubjectRepository @Inject constructor(
    private val subjectApi: SubjectApi
): ApiResponse() {

    suspend fun getAll(
        search:String? = null,
        pageNumber: Int = 1,
    ) = subjectApi.getAll(pageNumber = pageNumber, search = search)

    suspend fun getById(id: Int) = safeApiCall { subjectApi.getById(id) }
}