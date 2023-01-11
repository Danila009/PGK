package ru.pgk63.feature_specialization.screens.specializationListScreen

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.LoadState
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import androidx.paging.compose.items
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.feature_specialization.screens.specializationListScreen.viewModel.SpecializationListViewModel
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.view.*
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.core_ui.view.shimmer.VerticalListItemShimmer

@Composable
internal fun SpecializationListRoute(
    viewModel: SpecializationListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {
    var searchText by remember { mutableStateOf("") }

    val specializations = viewModel.getSpecialization(
        search = searchText.ifEmpty { null }
    ).collectAsLazyPagingItems()

    SpecializationListScreen(
        specializations = specializations,
        searchText = searchText,
        onSearchTextChange = { searchText = it },
        onBackScreen = onBackScreen,
        onSpecializationDetailsScreen = onSpecializationDetailsScreen
    )
}

@Composable
private fun SpecializationListScreen(
    specializations: LazyPagingItems<Specialization>,
    searchText: String,
    onSearchTextChange: (String) -> Unit,
    onBackScreen: () -> Unit,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {
    val scope = rememberCoroutineScope()

    val searchTextFieldFocusRequester = remember { FocusRequester() }

    val scrollBehavior = rememberToolbarScrollBehavior()
    var searchTextFieldVisible by remember { mutableStateOf(false) }

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.specialties),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                actions = {
                    AnimatedVisibility(visible = !searchTextFieldVisible) {
                        IconButton(onClick = {
                            scope.launch {
                                searchTextFieldVisible = true
                                delay(100)
                                searchTextFieldFocusRequester.requestFocus()
                            }
                        }) {
                            Icon(
                                imageVector = Icons.Default.Search,
                                contentDescription = null,
                                tint = PgkTheme.colors.primaryText
                            )
                        }
                    }

                    AnimatedVisibility(visible = searchTextFieldVisible) {
                        TextFieldSearch(
                            text = searchText,
                            onTextChanged = onSearchTextChange,
                            modifier = Modifier
                                .focusRequester(searchTextFieldFocusRequester),
                            onClose = {
                                searchTextFieldVisible = false
                                onSearchTextChange("")
                            }
                        )
                    }
                }
            )
        }
    ) { paddingValues ->

        if (
            specializations.itemCount <= 0 && specializations.loadState.refresh !is LoadState.Loading
        ){
            EmptyUi()
        }else if(specializations.loadState.refresh is LoadState.Error) {
            ErrorUi()
        }else{
            LazyColumn(
                modifier = Modifier.fillMaxSize()
            ) {

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

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun SpecializationCardUi(
    specialization: Specialization,
    onSpecializationDetailsScreen: (specializationId: Int) -> Unit
) {
    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Card(
        modifier = Modifier
            .padding(5.dp)
            .fillMaxWidth(),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        onClick = { onSpecializationDetailsScreen(specialization.id) }
    ) {
        Column {

            Row(verticalAlignment = Alignment.CenterVertically) {

                if(specialization.department.departmentHead.photoUrl != null) {
                    ImageCoil(
                        url = specialization.department.departmentHead.photoUrl,
                        modifier = Modifier
                            .width((screenWidthDp / 2).dp)
                            .height((screenHeightDp / 4.3).dp)
                    )
                }else {
                    Image(
                        painter = painterResource(id = R.drawable.profile_photo),
                        contentDescription = null,
                        modifier = Modifier
                            .width((screenWidthDp / 2).dp)
                            .height((screenHeightDp / 4.3).dp)
                    )
                }

                Text(
                    text = specialization.department.departmentHead.fio(),
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.body,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier.padding(5.dp),
                    textAlign = TextAlign.Center
                )
            }

            Divider(color = PgkTheme.colors.secondaryBackground)

            Text(
                text = specialization.name,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier
                    .padding(5.dp)
                    .fillMaxWidth(),
                textAlign = TextAlign.Center
            )
        }
    }
}
