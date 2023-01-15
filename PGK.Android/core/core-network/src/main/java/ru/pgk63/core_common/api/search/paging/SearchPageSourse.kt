package ru.pgk63.core_common.api.search.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.search.SearchApi
import ru.pgk63.core_common.api.search.model.SearchResponse
import ru.pgk63.core_common.api.search.model.SearchType

class SearchPageSourse(
    private val searchApi: SearchApi,
    private val searchText:String,
    private val type: SearchType? = null
): PagingSource<Int, SearchResponse>() {
    override fun getRefreshKey(state: PagingState<Int, SearchResponse>): Int? {

        val anchorPosition = state.anchorPosition ?: return null
        val page = state.closestPageToPosition(anchorPosition) ?: return null
        return page.prevKey?.plus(1) ?: page.nextKey?.minus(1)
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, SearchResponse> {
        return try {
            val page = params.key ?: 1

            val data = searchApi.search(
                searchText = searchText,
                type = type,
                pageNumber = page
            )

//            LoadResult.Page(
//                data = data,
//                prevKey = if (page == 1) null else page - 1,
//                nextKey = if(data.results.size < Constants.PAGE_SIZE) null else page + 1
//            )

            LoadResult.Invalid()
        }catch (e:Exception){
            LoadResult.Error(e)
        }
    }
}