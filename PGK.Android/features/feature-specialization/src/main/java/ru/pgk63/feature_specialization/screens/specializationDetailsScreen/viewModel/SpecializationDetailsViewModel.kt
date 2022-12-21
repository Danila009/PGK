package ru.pgk63.feature_specialization.screens.specializationDetailsScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.speciality.repository.SpecializationRepository
import ru.pgk63.core_common.common.response.Result
import javax.inject.Inject

@HiltViewModel
class SpecializationDetailsViewModel @Inject constructor(
    private val specializationRepository: SpecializationRepository
): ViewModel() {

    private val _responseSpecialization = MutableStateFlow<Result<Specialization>>(Result.Loading())
    val responseSpecialization = _responseSpecialization.asStateFlow()

    fun getById(id:Int){
        viewModelScope.launch {
            _responseSpecialization.value = specializationRepository.getById(id)
        }
    }
}