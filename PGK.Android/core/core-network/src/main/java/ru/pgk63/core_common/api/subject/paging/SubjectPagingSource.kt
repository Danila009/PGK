package ru.pgk63.core_common.api.subject.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.subject.repository.SubjectRepository

class SubjectPagingSource(
    private val search:String? = null,
    private val subjectRepository: SubjectRepository
): PagingSource<Int, Subject>() {

    override fun getRefreshKey(state: PagingState<Int, Subject>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Subject> {
        return try {

            val nextPage = params.key ?: 1

            val data = subjectRepository.getAll(search = search, pageNumber = nextPage)

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