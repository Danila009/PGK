package ru.pgk63.core_common.di

import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import ru.pgk63.core_common.common.Constants.BASE_URL
import ru.pgk63.core_database.user.UserDataSource
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
internal class RetrofitModule {

    @[Provides Singleton]
    fun providerRetrofit(
        okHttpClient: OkHttpClient
    ):Retrofit = Retrofit.Builder()
        .baseUrl(BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .client(okHttpClient)
        .build()

    @[Provides Singleton]
    fun providerOkHttpClient(
        userDataSource: UserDataSource
    ): OkHttpClient = OkHttpClient.Builder()
        .addInterceptor {
            val token = userDataSource.getAccessToken()

            val request = it.request().newBuilder()
                .header("Authorization","Bearer $token")
                .build()

            it.proceed(request)
        }.build()
}