package ru.pgk63.feature_auth.screens.auth

import android.annotation.SuppressLint
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.shape.AbsoluteRoundedCornerShape
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowRight
import androidx.compose.runtime.*
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Devices
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.auth.model.SignIn
import ru.pgk63.core_common.api.auth.model.SignInResponse
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.TextFieldBase
import ru.pgk63.feature_auth.screens.auth.viewModel.AuthViewModel
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.core_ui.view.BaseLottieAnimation
import ru.pgk63.core_ui.view.LottieAnimationType
import ru.pgk63.core_ui.view.TextFieldPassword
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.validation.nameValidation
import ru.pgk63.core_common.validation.passwordValidation

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun AuthRoute(
    viewModel: AuthViewModel = hiltViewModel()
) {
    var firstName by rememberSaveable { mutableStateOf("") }
    var lastName by rememberSaveable { mutableStateOf("") }
    var password by rememberSaveable { mutableStateOf("") }

    var firstNameValidation by rememberSaveable { mutableStateOf<Pair<Boolean, Int?>?>(null) }
    var lastNameValidation by rememberSaveable { mutableStateOf<Pair<Boolean, Int?>?>(null) }
    var passwordValidation by rememberSaveable { mutableStateOf<Pair<Boolean, Int?>?>(null) }

    var resultSignIn by remember { mutableStateOf<Result<SignInResponse>?>(null) }

    viewModel.responseSignIn.onEach { result ->
        resultSignIn = result
    }.launchWhenStarted()

    AuthScreen(
        firstName = firstName,
        lastName = lastName,
        password = password,
        firstNameValidation = firstNameValidation ,
        lastNameValidation = lastNameValidation,
        passwordValidation = passwordValidation,
        resultSignIn = resultSignIn,
        signIn = {

            firstNameValidation = nameValidation(firstName)
            lastNameValidation = nameValidation(lastName)
            passwordValidation = passwordValidation(password)

            if(firstNameValidation?.first == true &&
                lastNameValidation?.first == true &&
                passwordValidation?.first == true){
                val body = SignIn(
                    firstName = firstName,
                    lastName = lastName,
                    password = password
                )

                viewModel.signIn(body = body)
            }
        },
        onFirstNameChange = { firstName = it },
        onLastNameChange = { lastName = it },
        onPasswordChange = { password = it }
    )
}

@Composable
private fun AuthScreen(
    firstName: String,
    lastName: String,
    password: String,
    firstNameValidation: Pair<Boolean, Int?>? = nameValidation(firstName),
    lastNameValidation: Pair<Boolean, Int?>? = nameValidation(lastName),
    passwordValidation: Pair<Boolean, Int?>? = passwordValidation(password),
    resultSignIn: Result<SignInResponse>? = null,
    signIn: () -> Unit,
    onFirstNameChange: (String) -> Unit,
    onLastNameChange: (String) -> Unit,
    onPasswordChange: (String) -> Unit,
) {
    val screenHeightDp = LocalConfiguration.current.screenHeightDp
    val screenWidthDp = LocalConfiguration.current.screenWidthDp

    Surface(
        modifier = Modifier.fillMaxSize(),
        color = PgkTheme.colors.primaryBackground
    ) {
        LazyColumn {
            item {
                Column(
                    modifier = Modifier.fillMaxSize(),
                    horizontalAlignment = Alignment.CenterHorizontally,
                    verticalArrangement = Arrangement.Center
                ) {
                    BaseLottieAnimation(
                        type = LottieAnimationType.WELCOME,
                        modifier = Modifier
                            .padding(5.dp)
                            .width((screenWidthDp / 1.5).dp)
                            .height((screenHeightDp / 2).dp)
                    )

                    Text(
                        text = stringResource(id = R.string.welcome),
                        color = PgkTheme.colors.primaryText,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        style = PgkTheme.typography.heading,
                        modifier = Modifier.padding(5.dp)
                    )

                    if(resultSignIn is Result.Loading){
                        CircularProgressIndicator(
                            color = PgkTheme.colors.tintColor,
                            modifier = Modifier.padding(5.dp)
                        )
                    }else if(resultSignIn is Result.Error){
                        Text(
                            text =  resultSignIn.data?.errorMessage ?: stringResource(id = R.string.authorization_error),
                            color = PgkTheme.colors.errorColor,
                            fontFamily = PgkTheme.fontFamily.fontFamily,
                            style = PgkTheme.typography.body,
                            modifier = Modifier.padding(5.dp)
                        )
                    }
                    
                    TextFieldBase(
                        text = firstName,
                        onTextChanged = onFirstNameChange,
                        maxChar = 256,
                        label = stringResource(id = R.string.firstName),
                        modifier = Modifier.padding(5.dp),
                        errorText = if(firstNameValidation?.second != null)
                            stringResource(id = firstNameValidation.second!!) else null,
                        hasError = !(firstNameValidation?.first ?: true),
                    )

                    TextFieldBase(
                        text = lastName,
                        onTextChanged = onLastNameChange,
                        maxChar = 256,
                        label = stringResource(id = R.string.lastName),
                        modifier = Modifier.padding(5.dp),
                        errorText = if (lastNameValidation?.second != null)
                            stringResource(id = lastNameValidation.second!!) else null,
                        hasError = !(lastNameValidation?.first ?: true),
                    )

                    TextFieldPassword(
                        text = password,
                        onTextChanged = onPasswordChange,
                        modifier = Modifier.padding(5.dp),
                        validation = passwordValidation
                    )

                    Row(
                        modifier = Modifier.fillMaxWidth(),
                        horizontalArrangement = Arrangement.End
                    ) {
                        SignInButton(onClick = signIn)
                    }
                }
            }
        }
    }
}

@Composable
private fun SignInButton(
    onClick: () -> Unit
) {
    Row(
        verticalAlignment = Alignment.CenterVertically,
        modifier = Modifier
            .padding(10.dp)
            .clickable {
                onClick()
            }
    ) {
        Text(
            modifier = Modifier.padding(5.dp),
            text = stringResource(id = R.string.entrance),
            color = PgkTheme.colors.primaryText,
            fontFamily = PgkTheme.fontFamily.fontFamily,
            style = PgkTheme.typography.body
        )

        Card(
            modifier = Modifier.padding(5.dp),
            backgroundColor = PgkTheme.colors.primaryText,
            shape = AbsoluteRoundedCornerShape(90.dp)
        ) {
            Box {
                Icon(
                    modifier = Modifier
                        .align(Alignment.Center)
                        .padding(10.dp),
                    imageVector = Icons.Default.KeyboardArrowRight,
                    contentDescription = null,
                    tint = PgkTheme.colors.primaryBackground
                )
            }
        }
    }
}

@Preview(device = Devices.PIXEL, showSystemUi = true, showBackground = true)
@Composable
private fun AuthScreenDartPreview(){
    MainTheme(darkTheme = true) {
        AuthScreen(
            firstName = "",
            lastName = "",
            password = "",
            signIn = {},
            onFirstNameChange = {},
            onLastNameChange = {},
            onPasswordChange = {}
        )
    }
}


@Preview(device = Devices.PIXEL, showSystemUi = true, showBackground = true)
@Composable
private fun AuthScreenLightPreview(){
    MainTheme(darkTheme = false) {
        AuthScreen(
            firstName = "",
            lastName = "",
            password = "",
            signIn = {},
            onFirstNameChange = {},
            onLastNameChange = {},
            onPasswordChange = {}
        )
    }
}
