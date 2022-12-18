package ru.pgk63.feature_group.screens.groupListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import ru.pgk63.core_common.api.group.repository.GroupRepository
import ru.pgk63.core_common.api.group.paging.GroupPagingSource
import javax.inject.Inject

@HiltViewModel
internal class GroupListViewModel @Inject constructor(
    private val groupRepository: GroupRepository
): ViewModel() {

    val groups = Pager(PagingConfig(pageSize = 20)){
        GroupPagingSource(groupRepository)
    }.flow.cachedIn(viewModelScope)
}