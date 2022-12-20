package ru.pgk63.core_common.api.group.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.group.repository.GroupRepository
import ru.pgk63.core_common.api.student.model.Student

class StudentByGroupIdPagingSource(
    private val groupRepository: GroupRepository,
    private val groupId:Int
): PagingSource<Int, Student>() {

    override fun getRefreshKey(state: PagingState<Int, Student>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Student> {
        return try {
            val nextPage = params.key ?: 1

            val students = groupRepository.getStudentByGroupId(
                id = groupId,
                pageNumber = nextPage
            ).results

            LoadResult.Page(
                data = students,
                prevKey = if (nextPage == 1) null else nextPage - 1,
                nextKey = nextPage.plus(1)
            )
        }catch (e:Exception){
            LoadResult.Error(e)
        }
    }
}