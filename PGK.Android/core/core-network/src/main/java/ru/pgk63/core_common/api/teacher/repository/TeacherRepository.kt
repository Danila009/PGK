package ru.pgk63.core_common.api.teacher.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.teacher.TeacherApi
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_common.api.teacher.paging.TeacherPageSource
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class TeacherRepository @Inject constructor(
    private val teacherApi: TeacherApi
): ApiResponse() {

    fun getAll(
        search:String? = null,
        subjectIds:List<Int>? = null
    ): Flow<PagingData<Teacher>> {
        return Pager(PagingConfig(pageSize = Constants.PAGE_SIZE)){
            TeacherPageSource(
                teacherApi = teacherApi,
                search = search,
                subjectIds = subjectIds
            )
        }.flow
    }
}