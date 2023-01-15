package ru.pgk63.feature_auth.screens.forgotPassword

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyListScope
import androidx.compose.material.Scaffold
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import com.google.accompanist.pager.ExperimentalPagerApi
import com.google.accompanist.pager.HorizontalPager
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.BaseLottieAnimation
import ru.pgk63.core_ui.view.EmptyUi
import ru.pgk63.core_ui.view.LottieAnimationType
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior

@Composable
internal fun ForgotPasswordRoute(
    onBackScreen: () -> Unit
) {
    ForgotPasswordScreen(
        onBackScreen = onBackScreen
    )
}

@OptIn(ExperimentalPagerApi::class)
@Composable
private fun ForgotPasswordScreen(
    onBackScreen: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            TopBarBack(
                title = stringResource(id = R.string.forgot_password),
                scrollBehavior = scrollBehavior,
                onBackClick = onBackScreen
            )
        }
    ) { paddingValues ->
        HorizontalPager(
            contentPadding = paddingValues,
            userScrollEnabled = false,
            count = 4
        ) {
            LazyColumn(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center
            ) {
                when(it){
                    0 -> emailOrTelegramQuestion()
                    else -> item { EmptyUi() }
                }
            }
        }
    }
}

private fun LazyListScope.emailOrTelegramQuestion(

) {
    item {
        val screenHeightDp = LocalConfiguration.current.screenHeightDp
        val screenWidthDp = LocalConfiguration.current.screenWidthDp

        BaseLottieAnimation(
            type = LottieAnimationType.FORGOT_PASSWORD,
            modifier = Modifier
                .padding(5.dp)
                .width((screenWidthDp / 1.5).dp)
                .height((screenHeightDp / 2).dp)
        )

        Text(
            text = stringResource(id = R.string.email_or_telegram_question),
            color = PgkTheme.colors.primaryText,
            style = PgkTheme.typography.toolbar,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            textAlign = TextAlign.Center,
            modifier = Modifier.padding(horizontal = 10.dp, vertical = 0.dp)
        )
    }
}