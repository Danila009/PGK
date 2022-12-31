package ru.pgk63.core_common.api.user

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.PATCH
import retrofit2.http.Query
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.api.user.model.UserSettings
import ru.pgk63.core_common.enums.theme.ThemeCorners
import ru.pgk63.core_common.enums.theme.ThemeFontSize
import ru.pgk63.core_common.enums.theme.ThemeFontStyle
import ru.pgk63.core_common.enums.theme.ThemeStyle

interface UserApi {

    @GET("/pgk63/api/User")
    suspend fun get(): Response<User>

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
}