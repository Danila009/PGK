package com.example.feature_student.screens.studentListScreen

import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Card
import androidx.compose.material.ExperimentalMaterialApi
import androidx.compose.material.Scaffold
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
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
import androidx.paging.compose.items
import com.example.feature_student.screens.studentListScreen.viewModel.StudentListViewModel
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.ImageCoil

@Composable
internal fun StudentListRoute(
    viewModel: StudentListViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
    onStudentDetailsScreen: (studentId: Int) -> Unit
) {

    val students = viewModel.getStudents().collectAsLazyPagingItems()

    StudentListScreen(
        students = students,
        onBackScreen = onBackScreen,
        onStudentDetailsScreen = onStudentDetailsScreen
    )
}

@Composable
private fun StudentListScreen(
    students: LazyPagingItems<Student>,
    onBackScreen: () -> Unit,
    onStudentDetailsScreen: (studentId: Int) -> Unit
) {
    Scaffold(
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.students),
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        LazyColumn {

            items(students){ student ->
                student?.let { StudentCard(student,onStudentDetailsScreen) }
            }

            item {
                Spacer(modifier = Modifier.height(paddingValues.calculateBottomPadding()))
            }
        }
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun StudentCard(student: Student, onStudentDetailsScreen: (studentId: Int) -> Unit) {

    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Card(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = PgkTheme.shapes.cornersStyle,
        modifier = Modifier.padding(PgkTheme.shapes.padding),
        onClick = { onStudentDetailsScreen(student.id) }
    ) {
        Row(verticalAlignment = Alignment.CenterVertically) {

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

            Column(
                modifier = Modifier.padding(5.dp),
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center
            ) {
                Text(
                    text = "${student.lastName} ${student.firstName} " +
                            "${student.middleName}",
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.body,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier.padding(5.dp),
                    textAlign = TextAlign.Center
                )

                Text(
                    text = "${student.group.speciality.nameAbbreviation}-${student.group.course}${student.group.number}",
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.body,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier.padding(5.dp),
                    textAlign = TextAlign.Center
                )
            }
        }
    }
}