package ru.pgk63.core_ui.view

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.KeyboardActionScope
import androidx.compose.foundation.text.KeyboardActions
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Close
import androidx.compose.material.icons.filled.Search
import androidx.compose.material.icons.filled.Visibility
import androidx.compose.material.icons.filled.VisibilityOff
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusDirection
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.text.input.VisualTransformation
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import ru.pgk63.core_common.validation.emailValidation
import ru.pgk63.core_common.validation.passwordValidation
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.theme.MainTheme
import ru.pgk63.core_ui.theme.PgkTheme

@Composable
private fun rememberTextFieldColors() = TextFieldDefaults.textFieldColors(
    textColor = PgkTheme.colors.primaryText,
    focusedIndicatorColor = PgkTheme.colors.tintColor,
    backgroundColor = PgkTheme.colors.primaryBackground,
    cursorColor = PgkTheme.colors.tintColor,
    focusedLabelColor = PgkTheme.colors.tintColor,
    unfocusedLabelColor = PgkTheme.colors.primaryText,
    errorIndicatorColor = PgkTheme.colors.errorColor
)

@Composable
fun TextFieldBase(
    text: String,
    onTextChanged: (String) -> Unit,
    modifier: Modifier = Modifier,
    label: String? = null,
    maxChar: Int? = null,
    hasError: Boolean = false,
    errorText: String? = null,
    singleLine: Boolean = true,
    keyboardOptions: KeyboardOptions = KeyboardOptions(imeAction = ImeAction.Next),
    keyboardActions: KeyboardActions = KeyboardActions(),
    visualTransformation: VisualTransformation = VisualTransformation.None,
    shape: Shape = PgkTheme.shapes.cornersStyle,
    colors: TextFieldColors = rememberTextFieldColors(),
    leadingIcon: @Composable (() -> Unit)? = null,
    trailingIcon: @Composable (() -> Unit)? = null,
) {
    val focusManager = LocalFocusManager.current

    Column(modifier = modifier) {
        OutlinedTextField(
            value = text,
            onValueChange = {
                onTextChanged(if(maxChar != null) it.take(maxChar) else it)
                if (maxChar != null && it.length > maxChar){
                    focusManager.moveFocus(FocusDirection.Down)
                }
            },
            singleLine = singleLine,
            shape = shape,
            label = { label?.let { Text(text = label, color = PgkTheme.colors.primaryText) } },
            leadingIcon = leadingIcon,
            trailingIcon = trailingIcon,
            colors = colors,
            keyboardOptions = keyboardOptions,
            visualTransformation = visualTransformation,
            keyboardActions = keyboardActions,
            isError = hasError
        )

        Spacer(modifier = Modifier.height(2.dp))

        Row {

            if(errorText != null && errorText.isNotEmpty()){
                Text(
                    text = errorText,
                    color = PgkTheme.colors.errorColor,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    style = PgkTheme.typography.caption
                )

                Spacer(modifier = Modifier.width(15.dp))
            }

            if(maxChar != null){
                Text(
                    text = "${text.length} / $maxChar",
                    color = PgkTheme.colors.primaryText,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    style = PgkTheme.typography.caption
                )
            }
        }
    }
}

@Composable
fun TextFieldSearch(
    text: String,
    onTextChanged: (String) -> Unit,
    modifier: Modifier = Modifier,
    label:String? = null,
    onClear: () -> Unit,
    onSearch: (KeyboardActionScope.() -> Unit)? = null
) {
    TextFieldBase(
        text = text,
        onTextChanged = onTextChanged,
        modifier = modifier,
        label = label,
        leadingIcon = {
            AnimatedVisibility(text.isEmpty()){
                Icon(
                    imageVector = Icons.Default.Search,
                    contentDescription = null,
                    tint = PgkTheme.colors.controlColor
                )
            }
        },
        trailingIcon = {
            AnimatedVisibility(text.isNotEmpty()){
                IconButton(onClick = {
                    onClear()
                }) {
                    Icon(
                        imageVector = Icons.Default.Close,
                        contentDescription = null,
                        tint = PgkTheme.colors.controlColor
                    )
                }
            }
        },
        keyboardOptions = KeyboardOptions(
            imeAction = ImeAction.Search
        ),
        keyboardActions = KeyboardActions(onSearch = onSearch)
    )
}

@Composable
fun TextFieldEmail(
    text: String,
    onTextChanged: (String) -> Unit,
    modifier: Modifier = Modifier,
    validation: Pair<Boolean, Int?> = emailValidation(text),
    label: String = stringResource(id = R.string.email),
    onError: (Boolean) -> Unit = {},
    onNext: (KeyboardActionScope.() -> Unit)? = null
) {
    TextFieldBase(
        text = text,
        onTextChanged = onTextChanged,
        errorText = if(validation.second != null) stringResource(id = validation.second!!) else null,
        hasError = validation.first,
        label = label,
        modifier = modifier,
        keyboardOptions = KeyboardOptions(
            keyboardType = KeyboardType.Email,
            imeAction = ImeAction.Next
        ),
        keyboardActions = KeyboardActions(onNext = {
            onError(!validation.first)
            onNext?.let { it() }
        })
    )
}

@Composable
fun TextFieldPassword(
    text: String,
    onTextChanged: (String) -> Unit,
    modifier: Modifier = Modifier,
    validation: Pair<Boolean, Int?>? = passwordValidation(text),
    label: String = stringResource(id = R.string.password),
    onNext: (KeyboardActionScope.() -> Unit)? = null
) {
    var showPassword by remember { mutableStateOf(false) }

    TextFieldBase(
        text = text,
        onTextChanged = onTextChanged,
        maxChar = 256,
        errorText = if(validation?.second != null) stringResource(id = validation.second!!) else null,
        hasError = !(validation?.first ?: true),
        modifier = modifier,
        label = label,
        visualTransformation = if (showPassword) VisualTransformation.None else PasswordVisualTransformation(),
        keyboardOptions = KeyboardOptions(
            imeAction = ImeAction.Done
        ),
        keyboardActions = KeyboardActions(onNext = onNext),
        trailingIcon = {
            AnimatedVisibility(text.isNotEmpty()) {
                IconButton(onClick = { showPassword = !showPassword }) {
                    Icon(
                        imageVector = if(showPassword) Icons.Filled.Visibility else Icons.Filled.VisibilityOff,
                        contentDescription = "Visibility",
                        tint = PgkTheme.colors.primaryText
                    )
                }
            }
        }
    )
}

@Preview(showBackground = false)
@Composable
private fun TextFieldDartPreview() {
    MainTheme(darkTheme = true) {
        TextFieldBase(
            text = "Danila",
            errorText = "Invalid username",
            hasError = false,
            onTextChanged = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldLightPreview() {
    MainTheme(darkTheme = false) {
        TextFieldBase(
            text = "Danila",
            errorText = "Invalid username",
            hasError = false,
            onTextChanged = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldSearchDartPreview() {
    MainTheme(darkTheme = true) {
        TextFieldSearch(
            text = "Danila",
            onTextChanged = {},
            onClear = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldSearchLightPreview() {
    MainTheme(darkTheme = false) {
        TextFieldSearch(
            text = "Danila",
            onTextChanged = {},
            onClear = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldEmailDartPreview() {
    MainTheme(darkTheme = true) {
        TextFieldEmail(
            text = "Danila",
            onTextChanged = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldEmailLightPreview() {
    MainTheme(darkTheme = false) {
        TextFieldEmail(
            text = "Danila",
            onTextChanged = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldPasswordDartPreview() {
    MainTheme(darkTheme = true) {
        TextFieldPassword(
            text = "Danila",
            onTextChanged = {}
        )
    }
}

@Preview(showBackground = false)
@Composable
private fun TextFieldPasswordLightPreview() {
    MainTheme(darkTheme = false) {
        TextFieldPassword(
            text = "Danila",
            onTextChanged = {}
        )
    }
}
