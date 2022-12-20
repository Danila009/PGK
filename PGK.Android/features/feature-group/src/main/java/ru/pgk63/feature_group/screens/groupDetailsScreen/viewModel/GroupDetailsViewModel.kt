package ru.pgk63.feature_group.screens.groupDetailsScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.Constants
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.group.paging.StudentByGroupIdPagingSource
import ru.pgk63.core_common.api.group.repository.GroupRepository
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
class GroupDetailsViewModel @Inject constructor(
    private val groupRepository: GroupRepository
): ViewModel() {

    private val _responseGroup = MutableStateFlow<Result<Group>>(Result.Loading())
    val responseGroup = _responseGroup.asStateFlow()

    fun getGroupById(id: Int){
        viewModelScope.launch {
            val response = groupRepository.getById(id)
            _responseGroup.value = response
        }
    }

    fun getStudentsByGroupId(id: Int): Flow<PagingData<Student>> {
        return Pager(PagingConfig(pageSize = Constants.PAGE_SIZE)){
            StudentByGroupIdPagingSource(
                groupRepository = groupRepository,
                groupId = id
            )
        }.flow.cachedIn(viewModelScope)
    }
}