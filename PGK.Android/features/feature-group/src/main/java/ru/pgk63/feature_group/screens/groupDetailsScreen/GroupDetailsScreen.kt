package ru.pgk63.feature_group.screens.groupDetailsScreen

import android.annotation.SuppressLint
import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.GridItemSpan
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowRight
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.paging.compose.LazyPagingItems
import androidx.paging.compose.collectAsLazyPagingItems
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.paging.items
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.ImageCoil
import ru.pgk63.core_ui.view.LoadingUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.feature_group.screens.groupDetailsScreen.viewModel.GroupDetailsViewModel

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun GroupDetailsRoute(
    viewModel: GroupDetailsViewModel = hiltViewModel(),
    groupId: Int,
    onBackScreen: () -> Unit,
    onStudentDetailsScreen: (studentId: Int) -> Unit
) {
    var groupResult by remember { mutableStateOf<Result<Group>>(Result.Loading()) }

    val students = viewModel.getStudentsByGroupId(id = groupId).collectAsLazyPagingItems()

    viewModel.responseGroup.onEach { result ->
        groupResult = result
    }.launchWhenStarted()

    LaunchedEffect(key1 = Unit, block = {
        viewModel.getGroupById(id = groupId)
    })

    GroupDetailsScreen(
        groupResult = groupResult,
        onBackScreen = onBackScreen,
        students = students,
        onStudentDetailsScreen = onStudentDetailsScreen
    )
}

@Composable
private fun GroupDetailsScreen(
    groupResult: Result<Group>,
    students: LazyPagingItems<Student>,
    onBackScreen: () -> Unit,
    onStudentDetailsScreen: (studentId: Int) -> Unit
) {
    Scaffold(
        topBar = {

            val title = if(groupResult is Result.Success){
                val group = groupResult.data!!
                "${group.speciality.nameAbbreviation}-${group.course}${group.number}"
            }else { "" }

            TopBarBack(
                title = title,
                onBackClick = onBackScreen
            )
        },
        content = { paddingValues ->
            Surface(
                modifier = Modifier.fillMaxSize(),
                color = PgkTheme.colors.primaryBackground
            ) {
                when(groupResult){
                    is Result.Error -> ErrorUi(message = groupResult.message)
                    is Result.Loading -> LoadingUi()
                    is Result.Success -> {
                        LazyVerticalGrid(GridCells.Fixed(2)) {

                            item(span = { GridItemSpan(maxCurrentLineSpan) }) {
                                Spacer(modifier = Modifier.height(10.dp))

                                ClassroomTeacherUi(classroomTeacher = groupResult.data!!.classroomTeacher)
                            }

                            item(span = { GridItemSpan(maxCurrentLineSpan) }) {
                                Spacer(modifier = Modifier.height(10.dp))

                                DepartmentAndSpecialityUi(
                                    department = groupResult.data!!.department,
                                    speciality = groupResult.data!!.speciality
                                )
                            }

                            item(span = { GridItemSpan(maxCurrentLineSpan) }) {
                                if(students.itemCount > 0 ){
                                    Spacer(modifier = Modifier.height(40.dp))

                                    Text(
                                        text = stringResource(id = R.string.students),
                                        color = PgkTheme.colors.primaryText,
                                        style = PgkTheme.typography.heading,
                                        fontFamily = PgkTheme.fontFamily.fontFamily,
                                        modifier = Modifier.padding(start = 20.dp)
                                    )

                                    Spacer(modifier = Modifier.height(25.dp))
                                }
                            }

                            items(students){ student ->
                                student?.let {
                                    StudentCard(
                                        group = groupResult.data!!,
                                        student = student,
                                        onClick = onStudentDetailsScreen
                                    )
                                }
                            }

                            item(span = { GridItemSpan(maxCurrentLineSpan) }) {
                                Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
                            }
                        }
                    }
                }
            }
        }
    )
}

@Composable
private fun ClassroomTeacherUi(classroomTeacher: Teacher) {

    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Column {
        Card(
            modifier = Modifier
                .fillMaxWidth()
                .padding(5.dp),
            backgroundColor = PgkTheme.colors.secondaryBackground,
            shape = PgkTheme.shapes.cornersStyle
        ) {
            Row(
                modifier = Modifier
                    .fillMaxWidth(),
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                if(classroomTeacher.photoUrl != null) {
                    ImageCoil(
                        url = classroomTeacher.photoUrl,
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

                Column(horizontalAlignment = Alignment.CenterHorizontally) {
                    Text(
                        text = "${classroomTeacher.lastName} ${classroomTeacher.firstName} " +
                                (classroomTeacher.middleName ?: ""),
                        color = PgkTheme.colors.primaryText,
                        style = PgkTheme.typography.body,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        modifier = Modifier.padding(5.dp),
                        textAlign = TextAlign.Center
                    )

                    Text(
                        text = stringResource(id = R.string.classroomTeacher),
                        color = PgkTheme.colors.primaryText,
                        style = PgkTheme.typography.caption,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        modifier = Modifier.padding(5.dp),
                        textAlign = TextAlign.Center
                    )

                    Box(modifier = Modifier.align(Alignment.End)){
                        IconButton(onClick = { /*TODO*/ }) {
                            Icon(
                                imageVector = Icons.Default.KeyboardArrowRight,
                                contentDescription = null,
                                modifier = Modifier.padding(5.dp),
                                tint = PgkTheme.colors.primaryText
                            )
                        }
                    }
                }
            }
        }
    }
}

@Composable
private fun DepartmentAndSpecialityUi(
    department: Department,
    speciality: Specialization
) {
    Card(
        modifier = Modifier.padding(5.dp),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        shape = PgkTheme.shapes.cornersStyle
    ) {
        Column {
            Text(
                text = speciality.name,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp)
            )

            Text(
                text = department.name,
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.caption,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp)
            )

            Box(modifier = Modifier.align(Alignment.End)){
                IconButton(onClick = { /*TODO*/ }) {
                    Icon(
                        imageVector = Icons.Default.KeyboardArrowRight,
                        contentDescription = null,
                        modifier = Modifier.padding(5.dp),
                        tint = PgkTheme.colors.primaryText
                    )
                }
            }
        }
    }
}


@OptIn(ExperimentalMaterialApi::class)
@Composable
fun StudentCard(
    group: Group,
    student: Student,
    onClick: (studentId: Int) -> Unit
) {

    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Card(
        modifier = Modifier.padding(5.dp),
        backgroundColor = PgkTheme.colors.secondaryBackground,
        shape = PgkTheme.shapes.cornersStyle,
        onClick = { onClick(student.id) }
    ) {
        Column(
            modifier = Modifier.padding(10.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            if(student.photoUrl != null) {
                ImageCoil(
                    url = student.photoUrl,
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
                text = "${student.lastName} ${student.firstName} " +
                        (student.middleName ?: ""),
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp),
                textAlign = TextAlign.Center
            )

            Text(
                text = stringResource(
                    id = if(group.deputyHeadma?.id == student.id)
                        R.string.deputyHeadma
                    else if(group.headman?.id == student.id)
                        R.string.headman
                    else
                        R.string.student
                ),
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.caption,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp),
                textAlign = TextAlign.Center
            )
        }
    }
}