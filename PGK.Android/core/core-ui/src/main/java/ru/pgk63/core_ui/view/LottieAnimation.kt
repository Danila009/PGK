package ru.pgk63.core_ui.view

import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import com.airbnb.lottie.compose.*
import ru.pgk63.core_ui.R

enum class LottieAnimationType(val resId: Int) {
    WELCOME(R.raw.welcome),
    LOADING(R.raw.loading),
    ERROR(R.raw.error),
    EMPTY(R.raw.empty),
    PASSWORD_SECURITY(R.raw.password_security),
    FORGOT_PASSWORD(R.raw.forgot_password)
}

@Composable
fun BaseLottieAnimation(
    modifier: Modifier = Modifier,
    type:LottieAnimationType,
    iterations:Int = LottieConstants.IterateForever
){
    val compositionResult =
        rememberLottieComposition(spec = LottieCompositionSpec.RawRes(type.resId))

    val progress = animateLottieCompositionAsState(
        composition = compositionResult.value,
        iterations = iterations,
    )

    LottieAnimation(
        composition = compositionResult.value,
        progress = progress.progress,
        modifier = modifier
    )
}