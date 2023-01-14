package ru.pgk63.core_common.api.journal.paging

import android.util.Log
import androidx.paging.PagingSource
import androidx.paging.PagingState
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.journal.JournalApi
import ru.pgk63.core_common.api.journal.model.Journal

class JournalPagingSource(
    private val journalApi: JournalApi,
    private val course:List<Int>? = null,
    private val semesters:List<Int>? = null,
    private val groupIds:List<Int>? = null,
    private val specialityIds:List<Int>? = null,
    private val departmentIds:List<Int>? = null,
): PagingSource<Int, Journal>() {

    override fun getRefreshKey(state: PagingState<Int, Journal>): Int? {
        val anchorPosition = state.anchorPosition ?: return null
        val page = state.closestPageToPosition(anchorPosition) ?: return null
        return page.prevKey?.plus(1) ?: page.nextKey?.minus(1)
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Journal> {
        return try {

            val page = params.key ?: 1

            Log.e("JournalPagingSource",course.toString())
            Log.e("JournalPagingSource",semesters.toString())
            Log.e("JournalPagingSource",groupIds.toString())
            Log.e("JournalPagingSource",specialityIds.toString())
            Log.e("JournalPagingSource",departmentIds.toString())

            val data = journalApi.getAll(
                course = course,
                semesters = semesters,
                groupIds = groupIds,
                specialityIds = specialityIds,
                departmentIds = departmentIds,
                pageNumber = page
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