package ru.lfybkf19.feature_journal.screens.journalDetailsScreen.model

internal sealed class JournalDetailsSheetType {
    object JournalSubjectSorting: JournalDetailsSheetType()
    object JournalSubjectDetails: JournalDetailsSheetType()
    object JournalSubjectList: JournalDetailsSheetType()
}
