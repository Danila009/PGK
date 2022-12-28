package ru.pgk63.feature_specialization.screens.specializationListScreen

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import androidx.paging.compose.items
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.feature_specialization.screens.specializationListScreen.viewModel.SpecializationListViewModel
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.shimmer.VerticalListItemShimmer

@Composable
internal fun SpecializationListRoute(
    viewModel: SpecializationListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {

    val specializations = viewModel.getSpecialization().collectAsLazyPagingItems()

    SpecializationListScreen(
        onBackScreen = onBackScreen,
        specializations = specializations,
        onSpecializationDetailsScreen = onSpecializationDetailsScreen
    )
}

@Composable
private fun SpecializationListScreen(
    specializations: LazyPagingItems<Specialization>,
    onBackScreen: () -> Unit,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {
    Scaffold(
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.specialties),
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->

        Surface(
            modifier = Modifier.fillMaxSize(),
            color = PgkTheme.colors.primaryBackground
        ) {
            if (
                specializations.itemCount <= 0 && specializations.loadState.refresh !is LoadState.Loading
            ){
                EmptyUi()
            }else if(specializations.loadState.refresh is LoadState.Error) {
                ErrorUi()
            }else{
                LazyColumn {

                    items(specializations) { specialization ->
                        specialization?.let { SpecializationCardUi(it,onSpecializationDetailsScreen) }
                    }

                    if (specializations.loadState.append is LoadState.Loading){
                        item {
                            VerticalListItemShimmer()
                        }
                    }

                    if (
                        specializations.loadState.refresh is LoadState.Loading
                    ){
                        items(10) {
                            VerticalListItemShimmer()
                        }
                    }

                    item {
                        Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                    }
                }
            }
        }
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun SpecializationCardUi(
    specialization: Specialization,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {
    Card(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        modifier = Modifier.padding(5.dp),
        onClick = { onSpecializationDetailsScreen(specialization.id) }
    ) {
        Column {
            Text(
                text = specialization.name,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(10.dp)
            )

            Text(
                text = specialization.qualification,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.caption,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier
                    .padding(5.dp)
                    .align(Alignment.End)
            )
        }
    }
}
