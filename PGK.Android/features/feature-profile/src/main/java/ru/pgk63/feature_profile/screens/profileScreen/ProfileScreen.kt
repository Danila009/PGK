package ru.pgk63.feature_profile.screens.profileScreen

import android.annotation.SuppressLint
import android.graphics.Bitmap
import android.graphics.ImageDecoder
import android.os.Build
import android.provider.MediaStore
import androidx.activity.compose.rememberLauncherForActivityResult
import androidx.activity.result.PickVisualMediaRequest
import androidx.activity.result.contract.ActivityResultContracts
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.shape.AbsoluteRoundedCornerShape
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import kotlinx.coroutines.flow.onEach
import ru.pgk63.core_common.api.user.model.User
import ru.pgk63.core_common.common.response.Result
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.core_common.extension.launchWhenStarted
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.icon.ResIcons
import ru.pgk63.core_ui.theme.PgkTheme
import ru.pgk63.core_ui.view.ErrorUi
import ru.pgk63.core_ui.view.ImageCoil
import ru.pgk63.core_ui.view.LoadingUi
import ru.pgk63.core_ui.view.TopBarBack
import ru.pgk63.core_ui.view.collapsingToolbar.rememberToolbarScrollBehavior
import ru.pgk63.feature_profile.screens.profileScreen.viewModel.ProfileViewModel
import java.io.ByteArrayOutputStream

@SuppressLint("FlowOperatorInvokedInComposition")
@Composable
internal fun ProfileRoute(
    viewModel: ProfileViewModel = hiltViewModel(),
    onBackScreen: () -> Unit,
) {
    val context = LocalContext.current

    var userResult by remember { mutableStateOf<Result<User>>(Result.Loading()) }
    var userRole by remember { mutableStateOf<UserRole?>(null) }
    var userNewPhotoUrl by remember { mutableStateOf<String?>(null) }

    val scaffoldState = rememberScaffoldState()

    val singlePhotoPickerLauncher = rememberLauncherForActivityResult(
        contract = ActivityResultContracts.PickVisualMedia(),
        onResult = { uri ->
            uri?.let {
                val bitmap = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
                    ImageDecoder.decodeBitmap(ImageDecoder.createSource(context.contentResolver, uri))
                } else {
                    MediaStore.Images.Media.getBitmap(context.contentResolver, uri)
                }

                val outputStream = ByteArrayOutputStream()
                bitmap.compress(Bitmap.CompressFormat.JPEG, 80, outputStream)

                viewModel.uploadUserImage(outputStream.toByteArray())
            }
        }
    )

    viewModel.responseUser.onEach { result ->
        userResult = result
    }.launchWhenStarted()

    viewModel.userLocalDatabase.onEach {
        userRole = it?.userRole
    }.launchWhenStarted()

    viewModel.responseUpdateUserImageUrl.onEach { url ->
        if(url != null){

            userNewPhotoUrl = url

            scaffoldState.snackbarHostState.showSnackbar(
                message = context.getString(R.string.photo_updated)
            )
        }
    }.launchWhenStarted()

    ProfileScreen(
        scaffoldState = scaffoldState,
        userNewPhotoUrl = userNewPhotoUrl,
        userResult = userResult,
        userRole = userRole,
        onBackScreen = onBackScreen,
        updateUserPhoto = {
            singlePhotoPickerLauncher.launch(
                PickVisualMediaRequest(ActivityResultContracts.PickVisualMedia.ImageOnly)
            )
        }
    )
}

@Composable
private fun ProfileScreen(
    scaffoldState: ScaffoldState,
    userResult: Result<User>,
    userRole: UserRole?,
    userNewPhotoUrl: String?,
    onBackScreen: () -> Unit,
    updateUserPhoto: () -> Unit
) {
    val scrollBehavior = rememberToolbarScrollBehavior()

    Scaffold(
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        scaffoldState = scaffoldState,
        backgroundColor = PgkTheme.colors.primaryBackground,
        topBar = {
            Column {
                TopBarBack(
                    title = stringResource(id = R.string.profile),
                    scrollBehavior = scrollBehavior,
                    onBackClick = onBackScreen
                )

                AnimatedVisibility(
                    visible = userResult.data != null,
                    modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection)
                ) {
                    TopBarUserInfo(
                        user = userResult.data!!,
                        userRole = userRole,
                        userNewPhotoUrl = userNewPhotoUrl
                    )
                }
            }
        },
        snackbarHost = { state ->
            SnackbarHost(hostState = state) { data ->
                Snackbar(
                    backgroundColor = PgkTheme.colors.secondaryBackground,
                    contentColor = PgkTheme.colors.primaryText,
                    shape = PgkTheme.shapes.cornersStyle,
                    snackbarData = data
                )
            }
        }
    ) { paddingValues ->
        when(userResult){
            is Result.Error -> ErrorUi(message = userResult.message)
            is Result.Loading -> LoadingUi()
            is Result.Success -> UserSuccess(
                bottomPadding = paddingValues.calculateBottomPadding(),
                updateUserPhoto = updateUserPhoto
            )
        }
    }
}

@Composable
private fun TopBarUserInfo(
    user: User,
    userRole: UserRole?,
    userNewPhotoUrl: String?,
) {
    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Card(
        backgroundColor = PgkTheme.colors.secondaryBackground,
        elevation = 12.dp,
        shape = AbsoluteRoundedCornerShape(
            0, 0, 5, 5
        )
    ) {
        Row(verticalAlignment = Alignment.CenterVertically) {

            if(user.photoUrl != null || userNewPhotoUrl != null) {
                ImageCoil(
                    url = userNewPhotoUrl ?: user.photoUrl,
                    modifier = Modifier
                        .width((screenWidthDp / 2).dp)
                        .height((screenHeightDp / 4.3).dp)
                )
            }else {
                Image(
                    painter = painterResource(id = R.drawable.profile_photo),
                    contentDescription = null,
                    modifier = Modifier
                        .width((screenWidthDp / 2).dp)
                        .height((screenHeightDp / 4.3).dp)
                )
            }

            Column(
                modifier = Modifier.padding(5.dp),
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center
            ) {
                Text(
                    text = "${user.lastName} ${user.firstName} " +
                            (user.middleName ?: ""),
                    color = PgkTheme.colors.primaryText,
                    style = PgkTheme.typography.body,
                    fontFamily = PgkTheme.fontFamily.fontFamily,
                    modifier = Modifier.padding(5.dp),
                    textAlign = TextAlign.Center
                )

                if(userRole != null){
                    Text(
                        text = stringResource(id = userRole.nameId),
                        color = PgkTheme.colors.primaryText,
                        style = PgkTheme.typography.body,
                        fontFamily = PgkTheme.fontFamily.fontFamily,
                        modifier = Modifier.padding(5.dp),
                        textAlign = TextAlign.Center
                    )
                }
            }
        }
    }
}

@Composable
private fun UserSuccess(
    bottomPadding: Dp,
    updateUserPhoto: () -> Unit,
) {
    LazyColumn(
        modifier = Modifier.fillMaxSize()
    ) {
        item {

            UpdateUserPhotoCard(onClick = updateUserPhoto)

            Spacer(modifier = Modifier.height(bottomPadding))
        }
    }
}

@OptIn(ExperimentalMaterialApi::class)
@Composable
private fun UpdateUserPhotoCard(
    onClick: () -> Unit,
) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(horizontal = 10.dp, vertical = 20.dp),
        shape = PgkTheme.shapes.cornersStyle,
        backgroundColor = PgkTheme.colors.secondaryBackground,
        onClick = onClick
    ) {
        Row(
            modifier = Modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.SpaceBetween,
            verticalAlignment = Alignment.CenterVertically
        ) {
            Icon(
                painter = painterResource(id = ResIcons.camera),
                contentDescription = null,
                tint = PgkTheme.colors.primaryText,
                modifier = Modifier
                    .padding(10.dp)
                    .size(25.dp)
            )

            Text(
                text = stringResource(id = R.string.set_profile_photo),
                color = PgkTheme.colors.primaryText,
                style = PgkTheme.typography.body,
                fontFamily = PgkTheme.fontFamily.fontFamily,
                modifier = Modifier.padding(5.dp),
                textAlign = TextAlign.Center
            )
        }
    }
}