package ru.pgk63.core_database.room.database.migrations

import androidx.room.RenameColumn
import androidx.room.migration.AutoMigrationSpec

@RenameColumn(
    tableName = "user_history",
    fromColumnName = "id",
    toColumnName = "contentId"
)
internal class MigrationTo1From2 : AutoMigrationSpec