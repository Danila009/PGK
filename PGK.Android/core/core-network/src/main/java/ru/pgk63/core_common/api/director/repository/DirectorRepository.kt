package ru.pgk63.core_common.api.director.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.director.DirectorApi
import ru.pgk63.core_common.api.director.model.Director
import ru.pgk63.core_common.api.director.paging.DirectorPageSourse
import ru.pgk63.core_common.api.user.model.UserRegistrationBody
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class DirectorRepository @Inject constructor(
    private val directorApi: DirectorApi
): ApiResponse() {

    fun getAll(
        search:String? = null,
        current:Boolean? = null
    ): Flow<PagingData<Director>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            DirectorPageSourse(
                directorApi = directorApi,
                search = search,
                current = current
            )
        }.flow
    }

    suspend fun registration(body: UserRegistrationBody) = safeApiCall {
        directorApi.registration(body)
    }
}