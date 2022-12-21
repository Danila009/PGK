package ru.pgk63.feature_specialization.screens.specializationDetailsScreen

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Card
import androidx.compose.material.Scaffold
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.LoadingUi
import ru.pgk63.core_ui.R
import ru.pgk63.feature_specialization.screens.specializationDetailsScreen.viewModel.SpecializationDetailsViewModel

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun SpecializationDetailsRoute(
    viewModel: SpecializationDetailsViewModel = hiltViewModel(),
    specializationId: Int,
    onBackScreen: () -> Unit,
) {

    var resultSpecialization by remember { mutableStateOf<Result<Specialization>>(Result.Loading()) }

    viewModel.responseSpecialization.onEach { result ->
        resultSpecialization = result
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getById(specializationId)
    })

    SpecializationDetailsScreen(
        resultSpecialization = resultSpecialization,
        onBackScreen = onBackScreen
    )
}

@Composable
private fun SpecializationDetailsScreen(
    resultSpecialization: Result<Specialization>,
    onBackScreen: () -> Unit
) {
    Scaffold(
        topBar = {
            TopBarBack(
                title = resultSpecialization.data?.name ?: "",
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        Surface(
            modifier = Modifier.fillMaxSize(),
            color = PgkTheme.colors.primaryBackground
        ) {
            when(resultSpecialization){
                is Result.Error -> ErrorUi(message = resultSpecialization.message)
                is Result.Loading -> LoadingUi()
                is Result.Success -> LazyColumn {

                    item {
                        SpecializationDetailsUi(
                            specialization = resultSpecialization.data!!
                        )
                    }

                    item {
                        Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                    }
                }
            }
        }
    }
}

@Composable
private fun SpecializationDetailsUi(specialization: Specialization) {
    Card(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        modifier = Modifier.padding(PgkTheme.shapes.padding)
    ) {
        Column {
            Text(
                text = "${stringResource(id = R.string.number)} ${specialization.number}",
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(10.dp)
            )

            Text(
                text = "${stringResource(id = R.string.qualification)} ${specialization.qualification}",
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.caption,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp)
            )
        }
    }
}





