package ru.pgk63.feature_profile.screens.profileScreen.model

import androidx.annotation.DrawableRes
import androidx.annotation.StringRes
import ru.pgk63.core_ui.icon.ResIcons
import ru.pgk63.core_ui.R

internal enum class SecurityEmailState(@StringRes val nameId: Int, @DrawableRes val iconId: Int) {
    SECURITY(R.string.email_security, ResIcons.securitySecurity),
    EMAIL_VERIFICATION(R.string.email_verification, ResIcons.emailBlocker),
    INSECURITY(R.string.email_insecurity, ResIcons.insecuritySecurity)
}

internal fun getSecurityEmailState(
    email: String?,
    emailVerification: Boolean,
): SecurityEmailState {
    return if(email == null){
       SecurityEmailState.INSECURITY
    } else if (!emailVerification){
        SecurityEmailState.EMAIL_VERIFICATION
    }else {
        SecurityEmailState.SECURITY
    }
}