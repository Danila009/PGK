package ru.pgk63.feature_specialization.screens.specializationListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.paging.SpecializationPagingSource
import ru.pgk63.core_common.api.speciality.repository.SpecializationRepository
import javax.inject.Inject

@HiltViewModel
internal class SpecializationListViewModel @Inject constructor(
    private val specializationRepository: SpecializationRepository
): ViewModel() {

    fun getSpecialization(search: String? = null): Flow<PagingData<Specialization>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            SpecializationPagingSource(
                search = search,
                specializationRepository = specializationRepository
            )
        }.flow.cachedIn(viewModelScope)
    }
}