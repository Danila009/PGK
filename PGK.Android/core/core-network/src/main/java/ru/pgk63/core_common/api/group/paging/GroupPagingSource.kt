package ru.pgk63.core_common.api.group.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.group.repository.GroupRepository

class GroupPagingSource (
    private val groupRepository: GroupRepository,
    private val search: String? = null,
    private val course: List<Int>? = null,
    private val number: List<Int>? = null,
    private val specialityIds: List<Int>? = null,
    private val departmentIds: List<Int>? = null,
    private val classroomTeacherIds: List<Int>? = null,
    private val deputyHeadmaIds: List<Int>? = null,
    private val headmanIds: List<Int>? = null,
) : PagingSource<Int, Group>() {

    override fun getRefreshKey(state: PagingState<Int, Group>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Group> {
        return try {
            val nextPage = params.key ?: 1

            val groups = groupRepository.getAll(
                search = search,
                course = course,
                number = number,
                specialityIds = specialityIds,
                departmentIds = departmentIds,
                classroomTeacherIds = classroomTeacherIds,
                deputyHeadmaIds = deputyHeadmaIds,
                headmanIds = headmanIds,
                pageNumber = nextPage
            )

            LoadResult.Page(
                data = groups.results,
                prevKey = if (nextPage == 1) null else nextPage - 1,
                nextKey = nextPage.plus(1)
            )
        }catch (e:Exception){
            LoadResult.Error(e)
        }
    }
}