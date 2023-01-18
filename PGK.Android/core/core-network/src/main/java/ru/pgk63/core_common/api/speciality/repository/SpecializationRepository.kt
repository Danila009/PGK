package ru.pgk63.core_common.api.speciality.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.department.paging.DepartmentPagingSource
import ru.pgk63.core_common.api.speciality.SpecializationApi
import ru.pgk63.core_common.api.speciality.model.CreateSpecializationBody
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.model.SpecializationResponse
import ru.pgk63.core_common.api.speciality.paging.SpecializationPagingSource
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class SpecializationRepository @Inject constructor(
    private val specializationApi: SpecializationApi
): ApiResponse() {

    fun getAll(
        search:String? = null,
        departmentIds:List<Int>? = null
    ): Flow<PagingData<Specialization>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            SpecializationPagingSource(
                specializationApi = specializationApi,
                search = search,
                departmentIds = departmentIds
            )
        }.flow
    }

    suspend fun getById(id:Int) = safeApiCall { specializationApi.getById(id) }

    suspend fun create(body: CreateSpecializationBody) = safeApiCall { specializationApi.create(body) }
}