package ru.pgk63.core_common.di

import com.google.gson.Gson
import com.google.gson.GsonBuilder
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
        okHttpClient: OkHttpClient,
        gson: Gson
    ):Retrofit = Retrofit.Builder()
        .baseUrl(BASE_URL)
        .addConverterFactory(GsonConverterFactory.create(gson))
        .client(okHttpClient)
        .build()

    @[Provides Singleton]
    fun providerOkHttpClient(
        userDataSource: UserDataSource
    ): OkHttpClient = OkHttpClient.Builder()
        .addInterceptor {
            val token = userDataSource.getAccessToken()

            val request = it.request().newBuilder()
                .addHeader("Authorization","Bearer $token")
                .build()

            it.proceed(request)
        }.build()

    @[Provides Singleton]
    fun providerGson(): Gson = GsonBuilder()
        .setDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSZ")
        .create()
}