package ru.lfybkf19.feature_journal.screens.journalTopicTableScreen.model

import ru.pgk63.core_common.api.journal.model.JournalTopic

sealed class JournalTopicTableBottomDrawerType {
    object CreateTopic: JournalTopicTableBottomDrawerType()
    data class TopicRowMenu(val topic: JournalTopic): JournalTopicTableBottomDrawerType()
}
