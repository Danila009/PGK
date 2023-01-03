package ru.pgk63.core_common.api.techSupport.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.techSupport.model.Chat
import ru.pgk63.core_common.api.techSupport.repository.TechSupportRepository

class ChatPagingSource(
    private val techSupportRepository: TechSupportRepository
): PagingSource<Int, Chat>() {
    override fun getRefreshKey(state: PagingState<Int, Chat>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Chat> {
        return try {

            val nextPage = params.key ?: 1

            val data = techSupportRepository.getChatAll(pageNumber = nextPage).results

            LoadResult.Page(
                data = data,
                prevKey = if (nextPage == 1) null else nextPage - 1,
                nextKey = nextPage.plus(1)
            )
        }catch (e: Exception){
            LoadResult.Error(e)
        }
    }
}