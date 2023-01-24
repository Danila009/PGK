package ru.pgk63.feature_main.screens.searchScreen

import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.director.model.Director
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.search.model.SearchType
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.api.subject.model.Subject
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_main.screens.searchScreen.viewModel.SearchViewModel
import ru.pgk63.core_ui.view.TextFieldSearch

@Composable
internal fun SearchRoute(
    viewModel: SearchViewModel = hiltViewModel(),
    onBackScreen: () -> Unit
) {
    var searchText by remember { mutableStateOf("") }

    var searchType by remember { mutableStateOf(SearchType.STUDENT) }

    val studentList = viewModel.responseStudentList.collectAsLazyPagingItems()
    val departmentList = viewModel.responseDepartmentList.collectAsLazyPagingItems()
    val departmentHeadList = viewModel.responseDepartmentHeadList.collectAsLazyPagingItems()
    val directorList = viewModel.responseDirectorList.collectAsLazyPagingItems()
    val groupList = viewModel.responseGroupList.collectAsLazyPagingItems()
    val specializationList = viewModel.responseSpecializationList.collectAsLazyPagingItems()
    val subjectList = viewModel.responseSubjectList.collectAsLazyPagingItems()
    val teacherList = viewModel.responseTeacherList.collectAsLazyPagingItems()

    LaunchedEffect(searchText,searchType, block = {
        when(searchType){
            SearchType.STUDENT -> viewModel.getStudentList(search = searchText.ifEmpty { null })
            SearchType.HEADMAN -> TODO()
            SearchType.DEPUTY_HEADMAN -> TODO()
            SearchType.TEACHER ->  viewModel.getTeacherList(search = searchText.ifEmpty { null })
            SearchType.DEPARTMENT_HEAD -> viewModel.getDepartmentHeadList(search = searchText.ifEmpty { null })
            SearchType.DEPARTMENT -> viewModel.getDepartmentList(search = searchText.ifEmpty { null })
            SearchType.GROUP ->  viewModel.getGroupList(search = searchText.ifEmpty { null })
            SearchType.SPECIALITY -> viewModel.getSpecializationList(search = searchText.ifEmpty { null })
            SearchType.SUBJECT ->  viewModel.getSubjectList(search = searchText.ifEmpty { null })
        }

//        viewModel.getDirectorList(search = searchText.ifEmpty { null })
    })

    SearchScreen(
        searchType = searchType,
        searchText = searchText,
        departmentList = departmentList,
        departmentHeadList = departmentHeadList,
        directorList = directorList,
        groupList = groupList,
        specializationList = specializationList,
        subjectList = subjectList,
        teacherList = teacherList,
        studentList = studentList,
        onBackScreen = onBackScreen,
        onSearchTextChange = { searchText = it },
        onSearchTypeChange = { searchType = it }
    )
}

@Composable
private fun SearchScreen(
    searchText: String,
    searchType: SearchType,
    departmentList: LazyPagingItems<Department>,
    departmentHeadList: LazyPagingItems<DepartmentHead>,
    directorList: LazyPagingItems<Director>,
    groupList: LazyPagingItems<Group>,
    specializationList: LazyPagingItems<Specialization>,
    subjectList: LazyPagingItems<Subject>,
    teacherList: LazyPagingItems<Teacher>,
    studentList: LazyPagingItems<Student>,
    onSearchTypeChange: (SearchType) -> Unit,
    onSearchTextChange: (String) -> Unit,
    onBackScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = null,
                scrollBehavior = scrollBehavior,
                backgroundColor = PgkTheme.colors.secondaryBackground,
                onBackClick = onBackScreen,
                centralContent = {
                    TextFieldSearch(
                        text = searchText,
                        onTextChanged = onSearchTextChange,
                        onClose = { onSearchTextChange("") }
                    )
                },
                additionalContent = {
                    ScrollableTabRow(
                        selectedTabIndex = searchType.ordinal,
                        backgroundColor = PgkTheme.colors.secondaryBackground,
                        contentColor = PgkTheme.colors.tintColor
                    ) {
                        SearchType.values().forEach {
                            Tab(
                                selected = searchType.ordinal == it.ordinal,
                                onClick = { onSearchTypeChange(it) },
                                text = {
                                    Text(
                                        text = stringResource(id = it.nameId),
                                        color = PgkTheme.colors.primaryText,
                                        fontFamily = PgkTheme.fontFamily.fontFamily,
                                        style = PgkTheme.typography.body
                                    )
                                }
                            )
                        }
                    }
                }
            )
        }
    ){ paddingValues ->
        LazyColumn(contentPadding = paddingValues) {
            item {

            }
        }
    }
}