package ru.pgk63.core_common.api.user

import okhttp3.MultipartBody
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Multipart
import retrofit2.http.PATCH
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Query
import ru.pgk63.core_common.Constants.PAGE_SIZE
import ru.pgk63.core_common.api.user.model.NotificationResponse
import ru.pgk63.core_common.api.user.model.UpdateUserPhotoResponse
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.api.user.model.UserSettings
import ru.pgk63.core_common.enums.theme.ThemeCorners
import ru.pgk63.core_common.enums.theme.ThemeFontSize
import ru.pgk63.core_common.enums.theme.ThemeFontStyle
import ru.pgk63.core_common.enums.theme.ThemeStyle

interface UserApi {

    @GET("/pgk63/api/User")
    suspend fun get(): Response<User>

    @GET("/pgk63/api/User/Notifications")
    suspend fun getNotifications(
        @Query("search") search:String? = null,
        @Query("pageNumber") pageNumber:Int,
        @Query("pageSize") pageSize:Int = PAGE_SIZE
    ): NotificationResponse

    @PATCH("/pgk63/api/User/Password")
    suspend fun updatePassword(): Response<String>

    @PATCH("/pgk63/api/User/Settings/DrarkMode")
    suspend fun updateDarkMode(): Response<UserSettings>

    @PATCH("/pgk63/api/User/Settings/ThemeStyle")
    suspend fun updateThemeStyle(@Query("themeStyle") themeStyle: ThemeStyle): Response<UserSettings>

    @PATCH("/pgk63/api/User/Settings/ThemeFontStyle")
    suspend fun updateThemeFontStyle(
        @Query("themeFontStyle") themeFontStyle: ThemeFontStyle
    ): Response<UserSettings>

    @PATCH("/pgk63/api/User/Settings/ThemeFontSize")
    suspend fun updateThemeFontSize(
        @Query("themeFontSize") themeFontSize: ThemeFontSize
    ): Response<UserSettings>

    @PATCH("/pgk63/api/User/Settings/ThemeCorners")
    suspend fun updateThemeCorners(
        @Query("themeCorners") themeCorners: ThemeCorners
    ): Response<UserSettings>

    @GET("/pgk63/api/User/Settings")
    suspend fun getSettings(): Response<UserSettings>

    @POST("/pgk63/api/User/Photo")
    @Multipart
    suspend fun uploadImage(
        @Part photo: MultipartBody.Part
    ): Response<UpdateUserPhotoResponse>
}