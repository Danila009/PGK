package ru.pgk63.core_common.api.subject.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.subject.SubjectApi
import ru.pgk63.core_common.api.subject.model.CreateSubjectBody
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.model.UpdateSubjectBody
import ru.pgk63.core_common.api.subject.paging.SubjectPagingSource
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class SubjectRepository @Inject constructor(
    private val subjectApi: SubjectApi
): ApiResponse() {

    fun getAll(
        search:String? = null,
        teacherIds:List<Int>? = null
    ): Flow<PagingData<Subject>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            SubjectPagingSource(
                subjectApi = subjectApi,
                search = search,
                teacherIds = teacherIds
            )
        }.flow
    }

    suspend fun getById(id: Int) = safeApiCall { subjectApi.getById(id) }

    suspend fun create(body: CreateSubjectBody) = safeApiCall { subjectApi.create(body) }

    suspend fun delete(id: Int) = safeApiCall { subjectApi.delete(id) }

    suspend fun update(id: Int,body: UpdateSubjectBody) = safeApiCall { subjectApi.update(id, body) }
}