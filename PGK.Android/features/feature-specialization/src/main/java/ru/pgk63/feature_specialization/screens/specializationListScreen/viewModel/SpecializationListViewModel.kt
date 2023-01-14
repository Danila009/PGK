package ru.pgk63.feature_specialization.screens.specializationListScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.repository.SpecializationRepository
import javax.inject.Inject

@HiltViewModel
internal class SpecializationListViewModel @Inject constructor(
    private val specializationRepository: SpecializationRepository
): ViewModel() {

    private val _responseSpecializationList = MutableStateFlow<PagingData<Specialization>>(PagingData.empty())
    val responseSpecializationList = _responseSpecializationList.asStateFlow()

    fun getSpecialization(search: String? = null) {
        viewModelScope.launch {
            specializationRepository.getAll(search = search).cachedIn(viewModelScope).collect {
                _responseSpecializationList.value = it
            }
        }
    }
}