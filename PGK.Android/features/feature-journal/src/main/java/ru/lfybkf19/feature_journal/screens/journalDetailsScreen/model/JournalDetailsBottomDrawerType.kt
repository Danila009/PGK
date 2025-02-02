package ru.lfybkf19.feature_journal.screens.journalDetailsScreen.model

import ru.pgk63.core_common.api.journal.model.JournalEvaluation
import ru.pgk63.core_common.api.student.model.Student
import java.util.*

internal sealed class JournalDetailsBottomDrawerType {
    object JournalSubjectDetails: JournalDetailsBottomDrawerType()
    object JournalSubjectList: JournalDetailsBottomDrawerType()
    data class JournalColumn(
        val columnId: Int?,
        val rowId: Int?,
        val student: Student,
        val date: Date?,
        val evaluation: JournalEvaluation?
    ): JournalDetailsBottomDrawerType()
}
