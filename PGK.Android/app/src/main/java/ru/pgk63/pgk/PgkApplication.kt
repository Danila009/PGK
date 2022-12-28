package ru.pgk63.pgk

import android.app.Application
import com.google.firebase.FirebaseOptions
import com.google.firebase.ktx.Firebase
import com.google.firebase.ktx.initialize
import dagger.hilt.android.HiltAndroidApp
import ru.pgk63.pgk.utils.FirebaseConstants

@HiltAndroidApp
class PgkApplication: Application() {

    override fun onCreate() {
        super.onCreate()

        val options = FirebaseOptions.Builder()
            .setProjectId(FirebaseConstants.PROJECT_ID)
            .setApplicationId(FirebaseConstants.APPLICATION_ID)
            .build()

        Firebase.initialize(this, options, FirebaseConstants.PROJECT_ID)
    }
}