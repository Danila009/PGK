package ru.pgk63.core_common.api.speciality.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.repository.SpecializationRepository

class SpecializationPagingSource(
    private val specializationRepository: SpecializationRepository,
    private val search:String? = null,
):PagingSource<Int, Specialization>() {

    override fun getRefreshKey(state: PagingState<Int, Specialization>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Specialization> {
        return try {

            val nextPage = params.key ?: 1

            val data = specializationRepository.getAll(pageNumber = nextPage, search = search)

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