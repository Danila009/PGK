package ru.lfybkf19.feature_journal.view

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
import androidx.compose.material.Card
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import ru.pgk63.core_common.api.department.model.Department
import ru.pgk63.core_common.api.departmentHead.model.DepartmentHead
import ru.pgk63.core_common.api.group.model.Group
import ru.pgk63.core_common.api.journal.model.Journal
import ru.pgk63.core_common.api.speciality.model.Specialization
import ru.pgk63.core_common.api.teacher.model.Teacher
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.R

@Composable
internal fun JournalUi(
    modifier: Modifier = Modifier,
    journal: Journal,
    onClick: () -> Unit = {}
) {
    Box(
        modifier = modifier
            .widthIn(min = 100.dp)
            .heightIn(min = 150.dp)
            .clip(PgkTheme.shapes.cornersStyle)
            .background(PgkTheme.colors.journalColor)
            .clickable { onClick() }
    ) {
        Card(
            backgroundColor = Color.White,
            shape = PgkTheme.shapes.cornersStyle,
            modifier = Modifier
                .padding(10.dp)
                .align(Alignment.TopCenter)
        ) {
            Text(
                text = journal.group.toString(),
                color = Color.Black,
                textAlign = TextAlign.Center,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier
                    .widthIn(min = 70.dp)
                    .padding(2.dp)
            )
        }

        Card(
            backgroundColor = Color.White,
            shape = PgkTheme.shapes.cornersStyle,
            modifier = Modifier
                .padding(10.dp)
                .align(Alignment.Center)
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                modifier = Modifier.padding(3.dp)
            ) {
                Text(
                    text = "${stringResource(id = R.string.course)} ${journal.course}",
                    color = Color.Black,
                    textAlign = TextAlign.Center,
                    style = PgkTheme.typography.caption,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                )

                Text(
                    text = "${stringResource(id = R.string.semester)} ${journal.semester}",
                    color = Color.Black,
                    textAlign = TextAlign.Center,
                    style = PgkTheme.typography.caption,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                )
            }
        }
    }
}

@Preview(showSystemUi = true)
@Composable
fun JournalUiPreview() {

    val teacher = Teacher(
        id = 1,
        firstName = "Виктория",
        lastName = "Александровна",
        middleName = "С"
    )

    val department = Department(
        id = 0,
        name = "",
        departmentHead = DepartmentHead(
            id = 0,
            firstName = "",
            lastName = "",
            middleName = null,
            photoUrl = null
        )
    )

    val specialization = Specialization(
        id = 1,
        number = "13.32.31",
        name = "",
        nameAbbreviation = "ИСП",
        qualification = "",
        department = department
    )

    val group = Group(
        id = 1,
        course = 2,
        number = 39,
        speciality = specialization,
        department = department,
        classroomTeacher = teacher,
    )

    val journal = Journal(
        id = 1,
        course = 1,
        semester = 1,
        group = group,
        department = department,
        classroomTeacher = teacher
    )

    val journals = listOf(journal,journal,journal,journal)

    MainTheme(darkTheme = true) {
        Surface(
            modifier = Modifier.fillMaxSize(),
            color = PgkTheme.colors.primaryBackground
        ) {
            LazyVerticalGrid(
                columns = GridCells.Fixed(2)
            ){
                items(journals){
                    Box {
                        JournalUi(
                            modifier = Modifier
                                .padding(10.dp)
                                .align(Alignment.Center),
                            journal = it
                        )
                    }
                }
            }
        }
    }
}