package ru.pgk63.core_common.api.student.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.student.StudentApi
import ru.pgk63.core_common.api.student.model.Student

class StudentPagingSource(
    private val studentApi: StudentApi,
    private val search:String? = null,
    private val groupIds:List<Int>? = null
): PagingSource<Int, Student>() {
    override fun getRefreshKey(state: PagingState<Int, Student>): Int? {
        val anchorPosition = state.anchorPosition ?: return null
        val page = state.closestPageToPosition(anchorPosition) ?: return null
        return page.prevKey?.plus(1) ?: page.nextKey?.minus(1)
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Student> {
        return try {

            val page = params.key ?: 1

            val data = studentApi.getAll(
                pageNumber = page,
                search = search,
                groupIds = groupIds
            )

            LoadResult.Page(
                data = data.results,
                prevKey = if (page == 1) null else page - 1,
                nextKey = if(data.results.size < Constants.PAGE_SIZE) null else page + 1
            )
        }catch (e:Exception){
            LoadResult.Error(e)
        }
    }
}