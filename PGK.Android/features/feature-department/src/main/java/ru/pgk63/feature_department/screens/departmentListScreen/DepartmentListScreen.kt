package ru.pgk63.feature_department.screens.departmentListScreen

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.Search
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.input.nestedscroll.nestedScroll
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
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.*
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.core_ui.view.shimmer.VerticalListItemShimmer
import ru.pgk63.feature_department.screens.departmentListScreen.viewModel.DepartmentListViewModel

@Composable
internal fun DepartmentListRoute(
    viewModel: DepartmentListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onDepartmentDetailsScreen: (departmentId: Int) -> Unit,
    onCreateDepartmentScreen: (departmentHeadId: Int?) -> Unit,
) {
    var searchText by remember { mutableStateOf("") }

    val departments = viewModel.responseDepartmentList.collectAsLazyPagingItems()

    LaunchedEffect(key1 = searchText, block = {
        viewModel.getDepartments(search = searchText.ifEmpty { null })
    })

    DepartmentListScreen(
        departments = departments,
        searchText = searchText,
        onSearchTextChange = { searchText = it },
        onBackScreen = onBackScreen,
        onDepartmentDetailsScreen = onDepartmentDetailsScreen,
        onCreateDepartmentScreen = onCreateDepartmentScreen
    )
}

@Composable
private fun DepartmentListScreen(
    departments: LazyPagingItems<Department>,
    searchText: String,
    onSearchTextChange: (String) -> Unit,
    onBackScreen: () -> Unit,
    onDepartmentDetailsScreen: (departmentId: Int) -> Unit,
    onCreateDepartmentScreen: (departmentHeadId: Int?) -> Unit
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
                title = stringResource(id = R.string.departmens),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen,
                actions = {
                    AnimatedVisibility(visible = !searchTextFieldVisible) {
                        Row(verticalAlignment = Alignment.CenterVertically) {
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

                            Spacer(modifier = Modifier.width(5.dp))

                            IconButton(onClick = { onCreateDepartmentScreen(null) }) {
                                Icon(
                                    imageVector = Icons.Default.Add,
                                    contentDescription = null,
                                    tint = PgkTheme.colors.tintColor
                                )
                            }
                        }
                    }

                    AnimatedVisibility(visible = searchTextFieldVisible) {
                        TextFieldSearch(
                            text = searchText,
                            onTextChanged = onSearchTextChange,
                            modifier = Modifier.focusRequester(searchTextFieldFocusRequester),
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
            departments.itemCount <= 0 && departments.loadState.refresh !is LoadState.Loading
        ){
            EmptyUi()
        }else if(departments.loadState.refresh is LoadState.Error) {
            ErrorUi()
        }else{
            LazyColumn(
                modifier = Modifier.fillMaxSize()
            ) {

                items(departments) { department ->
                    department?.let {
                        DepartmentItemUi(
                            department = department,
                            onClick = { onDepartmentDetailsScreen(department.id) }
                        )
                    }
                }

                if (departments.loadState.append is LoadState.Loading){
                    item {
                        VerticalListItemShimmer()
                    }
                }

                if (
                    departments.loadState.refresh is LoadState.Loading
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
private fun DepartmentItemUi(
    department: Department,
    onClick: () -> Unit
) {
    Card(
        modifier = Modifier
            .padding(5.dp)
            .fillMaxWidth(),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        onClick = onClick
    ) {
        Text(
            text = department.name,
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.body,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            modifier = Modifier
                .padding(15.dp)
                .fillMaxWidth(),
            textAlign = TextAlign.Center
        )
    }
}