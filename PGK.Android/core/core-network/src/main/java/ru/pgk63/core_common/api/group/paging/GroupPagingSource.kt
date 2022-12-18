package ru.pgk63.core_common.api.group.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.group.repository.GroupRepository

class GroupPagingSource (
    private val groupRepository: GroupRepository
) : PagingSource<Int, Group>() {

    override fun getRefreshKey(state: PagingState<Int, Group>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Group> {
        return try {
            val nextPage = params.key ?: 1

            val groups = groupRepository.getAll(pageNumber = nextPage)

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