package ru.pgk63.core_common.api.departmentHead.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.departmentHead.DepartmentHeadApi
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.departmentHead.paging.DepartmentHeadPageSourse
import ru.pgk63.core_common.api.user.model.UserRegistrationBody
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class DepartmentHeadRepository @Inject constructor(
    private val departmentHeadApi: DepartmentHeadApi
): ApiResponse() {

    suspend fun registration(body: UserRegistrationBody) = safeApiCall {
        departmentHeadApi.registration(body)
    }

    fun getAll(search:String? = null): Flow<PagingData<DepartmentHead>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            DepartmentHeadPageSourse(
                departmentHeadApi = departmentHeadApi,
                search = search
            )
        }.flow
    }
}