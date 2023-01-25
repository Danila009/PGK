package ru.pgk63.core_database.room.database

import androidx.room.AutoMigration
import androidx.room.Database
import androidx.room.RoomDatabase
import androidx.room.TypeConverters
import ru.pgk63.core_database.room.database.converter.DateConverter
import ru.pgk63.core_database.room.database.history.dao.HistoryDao
import ru.pgk63.core_database.room.database.history.model.History
import ru.pgk63.core_database.room.database.migrations.MigrationTo1From2

@Database(
    entities = [History::class],
    version = 2,
    autoMigrations = [
        AutoMigration(
            from = 1,
            to = 2,
            spec = MigrationTo1From2::class
        )
    ]
)
@TypeConverters(value = [DateConverter::class])
internal abstract class PgkDatabase: RoomDatabase() {

    abstract fun historyDao(): HistoryDao
}