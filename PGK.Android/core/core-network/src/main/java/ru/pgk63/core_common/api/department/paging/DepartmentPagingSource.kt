package ru.pgk63.core_common.api.department.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.department.repository.DepartmentRepository

class DepartmentPagingSource(
    private val departmentRepository: DepartmentRepository,
    private val search: String? = null
): PagingSource<Int, Department>() {

    override fun getRefreshKey(state: PagingState<Int, Department>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Department> {
        return try {

            val nextPage = params.key ?: 1

            val data = departmentRepository.getAll(
                search = search,
                pageNumber = nextPage
            )

            LoadResult.Page(
                data = data.results,
                prevKey = if (nextPage == 1) null else nextPage - 1,
                nextKey = nextPage.plus(1)
            )
        }catch (e:Exception){
            LoadResult.Error(e)
        }
    }
}