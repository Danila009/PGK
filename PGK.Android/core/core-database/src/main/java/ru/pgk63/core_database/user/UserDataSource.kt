package ru.pgk63.core_database.user

import android.content.Context
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import dagger.hilt.android.qualifiers.ApplicationContext
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import ru.pgk63.core_common.extension.decodeFromString
import ru.pgk63.core_common.extension.encodeToString
import ru.pgk63.core_database.user.model.User
import javax.inject.Inject

class UserDataSource @Inject constructor(
   @ApplicationContext private val context: Context
) {
    suspend fun save(user: User){
        context.userDataStore.edit { preferences ->
            preferences[USER] = user.encodeToString()
        }
    }

    fun get(): Flow<User> {
        return context.userDataStore.data
            .map { preferences ->
                preferences[USER]?.decodeFromString() ?: User()
            }
    }

    private companion object{
        private val Context.userDataStore by preferencesDataStore(name = "user_data_store")
        val USER = stringPreferencesKey("user_key")
    }

}