package ru.pgk63.core_database.room.database.di

import android.content.Context
import androidx.room.Room
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import ru.pgk63.core_database.room.database.PgkDatabase
import ru.pgk63.core_database.room.database.history.dao.HistoryDao
import ru.pgk63.core_database.room.database.historySorting.dao.HistorySortingDao
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
internal class PgkDatabaseModule {

    @[Provides Singleton]
    fun providerDatabase(
        @ApplicationContext context: Context
    ): PgkDatabase = Room.databaseBuilder(
        context.applicationContext,
        PgkDatabase::class.java,
        "pgk_database"
    ).build()

    @[Provides Singleton]
    fun providerHistoryDao(database: PgkDatabase): HistoryDao = database.historyDao()

    @[Provides Singleton]
    fun providerHistorySortingDao(database: PgkDatabase): HistorySortingDao = database.historySortingDao()
}