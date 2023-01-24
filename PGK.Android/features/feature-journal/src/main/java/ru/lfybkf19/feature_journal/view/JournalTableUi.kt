package ru.lfybkf19.feature_journal.view

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyListState
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.paging.compose.LazyPagingItems
import ru.pgk63.core_common.api.journal.model.JournalEvaluation
import ru.pgk63.core_common.api.journal.model.JournalRow
import ru.pgk63.core_common.api.student.model.Student
import ru.pgk63.core_common.extension.parseToBaseDateFormat
import ru.pgk63.core_ui.view.table.Table
import ru.pgk63.core_ui.view.table.TableCell
import ru.pgk63.core_ui.R

@Composable
internal fun BoxScope.JournalTableUi(
    modifier: Modifier = Modifier,
    verticalLazyListState: LazyListState = rememberLazyListState(),
    rows: List<JournalRow>,
    students: LazyPagingItems<Student>,
    onClickStudent: (Student) -> Unit,
    onClickEvaluation: (JournalEvaluation?, columnId: Int?) -> Unit
) {
    val columns = rows.map { it.columns }.flatten()
    val dates = columns.map { it.date }.distinct().sortedBy { it }

    Table(
        modifier = modifier.matchParentSize(),
        rowModifier = Modifier.height(IntrinsicSize.Min),
        verticalLazyListState = verticalLazyListState,
        columnCount = dates.size + 1,
        rowCount = students.itemCount + 1,
    ){ columnIndex, rowIndex ->

        if(rowIndex == 0 && columnIndex == 0){
            TableCell(
                text = stringResource(id = R.string.student),
                modifier = Modifier.fillMaxSize()
            )
        }

        if(columnIndex == 0 && rowIndex != 0){
            val student = students[rowIndex-1]

            TableCell(
                text = student?.fioAbbreviated() ?: "",
                modifier = Modifier.fillMaxSize(),
                onClick = { student?.let { onClickStudent(it) } }
            )
        }

        if(columnIndex != 0 && rowIndex == 0){
            val date = dates[columnIndex-1]

            TableCell(
                text = date.parseToBaseDateFormat(),
                modifier = Modifier.fillMaxSize()
            )
        }

        if(columnIndex != 0 && rowIndex != 0){
            val date = dates[columnIndex-1]
            val student = students[rowIndex-1]

            val row = rows.firstOrNull { it.student.id == student?.id }

            val column =  row?.columns?.firstOrNull { it.date == date }

            if(column != null){
                TableCell(
                    text = column.evaluation.text,
                    modifier = Modifier.fillMaxSize(),
                    onClick = {  onClickEvaluation(column.evaluation, column.id)}
                )
            }else {
                TableCell(
                    text = "-",
                    modifier = Modifier.fillMaxSize(),
                    onClick = {  onClickEvaluation(null, null)}
                )
            }
        }
    }
}