package ru.pgk63.core_common.api.user.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.MultipartBody
import okhttp3.RequestBody.Companion.toRequestBody
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.user.UserApi
import ru.pgk63.core_common.api.user.model.Notification
import ru.pgk63.core_common.api.user.model.UpdateUserPhotoResponse
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.api.user.model.UserSettings
import ru.pgk63.core_common.api.user.paging.NotificationPageSourse
import ru.pgk63.core_common.common.response.ApiResponse
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.enums.theme.ThemeCorners
import ru.pgk63.core_common.enums.theme.ThemeFontSize
import ru.pgk63.core_common.enums.theme.ThemeFontStyle
import ru.pgk63.core_common.enums.theme.ThemeStyle
import ru.pgk63.core_database.user.UserDataSource
import javax.inject.Inject


class UserRepository @Inject constructor(
    private val userApi: UserApi,
    private val userDataSource: UserDataSource,
): ApiResponse() {

    suspend fun get(): Result<User> = safeApiCall { userApi.get() }

    fun getNotifications(search: String? = null): Flow<PagingData<Notification>> {
        return Pager(PagingConfig(pageSize = PAGE_SIZE)){
            NotificationPageSourse(
                userApi = userApi,
                search = search
            )
        }.flow
    }

    suspend fun updatePassword(): Result<String> = safeApiCall { userApi.updatePassword() }

    suspend fun updateDarkMode(): Result<UserSettings> {

        val result = safeApiCall { userApi.updateDarkMode() }

        if(result is Result.Success){
            val darkMode = result.data?.darkMode

            darkMode?.let { userDataSource.updateDarkModel(darkMode) }
        }

        return result
    }

    suspend fun updateThemeStyle(themeStyle: ThemeStyle) {
        val response = userApi.updateThemeStyle(themeStyle)

        if(response.isSuccessful){
            response.body()?.let { settings ->
                userDataSource.updateThemeStyle(settings.themeStyle)
            }
        }
    }

    suspend fun updateThemeFontStyle(themeFontStyle: ThemeFontStyle) {
        val response = userApi.updateThemeFontStyle(themeFontStyle)

        if(response.isSuccessful){
            response.body()?.let { settings ->
                userDataSource.updateThemeFontStyle(settings.themeFontStyle)
            }
        }
    }

    suspend fun updateThemeFontSize(themeFontSize: ThemeFontSize) {
        val response = userApi.updateThemeFontSize(themeFontSize)

        if(response.isSuccessful){
            response.body()?.let { settings ->
                userDataSource.updateThemeFontSize(settings.themeFontSize)
            }
        }
    }

    suspend fun updateThemeCorners(themeCorners: ThemeCorners) {
        val response = userApi.updateThemeCorners(themeCorners)

        if(response.isSuccessful){
            response.body()?.let { settings ->
                userDataSource.updateThemeCorners(settings.themeCorners)
            }
        }
    }

    suspend fun getSettings(): Result<UserSettings> {
        val result = safeApiCall { userApi.getSettings() }

        if(result is Result.Success && result.data != null){
            userDataSource.updateDarkModel(result.data.darkMode)
            userDataSource.updateThemeStyle(result.data.themeStyle)
            userDataSource.updateThemeFontStyle(result.data.themeFontStyle)
            userDataSource.updateThemeFontSize(result.data.themeFontSize)
            userDataSource.updateThemeCorners(result.data.themeCorners)
            userDataSource.updateLanguageCode(result.data.language?.code)
        }

        return result
    }

    suspend fun uploadImage(file: ByteArray): Result<UpdateUserPhotoResponse> {
        val reqFile = file.toRequestBody("application/octet-stream".toMediaTypeOrNull(), 0, file.size)

        return safeApiCall {
            userApi.uploadImage(
                photo = MultipartBody.Part
                    .createFormData(
                        "photo",
                        "photo",
                        reqFile
                    )
            )
        }
    }

    suspend fun passwordReset(email:String)  = safeApiCall {
        userApi.passwordReset(email)
    }
}