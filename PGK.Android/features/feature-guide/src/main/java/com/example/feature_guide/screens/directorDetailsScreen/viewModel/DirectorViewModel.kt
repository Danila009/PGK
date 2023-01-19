package com.example.feature_guide.screens.directorDetailsScreen.viewModel

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import ru.pgk63.core_common.api.director.repository.DirectorRepository
import javax.inject.Inject

@HiltViewModel
internal class DirectorViewModel @Inject constructor(
    private val directorRepository: DirectorRepository
): ViewModel() {

}