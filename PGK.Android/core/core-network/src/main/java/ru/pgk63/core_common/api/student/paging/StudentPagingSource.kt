package ru.pgk63.core_common.api.student.paging

import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.api.student.repository.StudentRepository

class StudentPagingSource(
    private val studentRepository: StudentRepository,
    private val search:String? = null
): PagingSource<Int, Student>() {
    override fun getRefreshKey(state: PagingState<Int, Student>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Student> {
        return try {

            val nextPage = params.key ?: 1

            val data = studentRepository.getAll(
                pageNumber = nextPage,
                search = search
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