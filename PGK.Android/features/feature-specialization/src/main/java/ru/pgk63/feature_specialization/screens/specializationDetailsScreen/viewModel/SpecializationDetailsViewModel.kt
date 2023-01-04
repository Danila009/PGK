package ru.pgk63.feature_specialization.screens.specializationDetailsScreen.viewModel

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
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.group.paging.GroupPagingSource
import ru.pgk63.core_common.api.group.repository.GroupRepository
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.repository.SpecializationRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
class SpecializationDetailsViewModel @Inject constructor(
    private val specializationRepository: SpecializationRepository,
    private val groupRepository: GroupRepository
): ViewModel() {

    private val _responseSpecialization = MutableStateFlow<Result<Specialization>>(Result.Loading())
    val responseSpecialization = _responseSpecialization.asStateFlow()

    fun getById(id:Int){
        viewModelScope.launch {
            _responseSpecialization.value = specializationRepository.getById(id)
        }
    }

    fun getGroups(specialityId: Int): Flow<PagingData<Group>> {
        return Pager(PagingConfig(pageSize = 20)){
            GroupPagingSource(
                groupRepository,
                specialityIds = listOf(specialityId)
            )
        }.flow.cachedIn(viewModelScope)
    }
}