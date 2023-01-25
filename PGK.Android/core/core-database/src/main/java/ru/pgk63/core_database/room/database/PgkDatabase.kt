package ru.pgk63.core_database.room.database

import androidx.room.AutoMigration
import androidx.room.Database
import androidx.room.RoomDatabase
import androidx.room.TypeConverters
import ru.pgk63.core_database.room.database.converter.DateConverter
import ru.pgk63.core_database.room.database.history.dao.HistoryDao
import ru.pgk63.core_database.room.database.history.model.History
import ru.pgk63.core_database.room.database.historySorting.dao.HistorySortingDao
import ru.pgk63.core_database.room.database.historySorting.model.HistorySorting
import ru.pgk63.core_database.room.database.migrations.MigrationTo1From2

@Database(
    entities = [History::class, HistorySorting::class],
    version = 3,
    autoMigrations = [
        AutoMigration(
            from = 1,
            to = 2,
            spec = MigrationTo1From2::class
        ),
        AutoMigration(
            from = 2,
            to = 3
        )
    ]
)
@TypeConverters(value = [DateConverter::class])
internal abstract class PgkDatabase: RoomDatabase() {

    abstract fun historyDao(): HistoryDao

    abstract fun historySortingDao(): HistorySortingDao
}