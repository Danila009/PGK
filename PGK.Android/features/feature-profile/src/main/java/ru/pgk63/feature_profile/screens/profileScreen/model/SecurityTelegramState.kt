package ru.pgk63.feature_profile.screens.profileScreen.model

import androidx.annotation.DrawableRes
import androidx.annotation.StringRes
import ru.pgk63.core_ui.R
import ru.pgk63.core_ui.icon.ResIcons

internal enum class SecurityTelegramState(@StringRes val nameId: Int, @DrawableRes val iconId: Int) {
    SECURITY(R.string.telegram_security, ResIcons.securitySecurity),
    INSECURITY(R.string.telegram_insecurity, ResIcons.insecuritySecurity)
}

internal fun getSecurityTelegramState(
    telegramId: Int?
): SecurityTelegramState {
    return if(telegramId == null){
        SecurityTelegramState.INSECURITY
    }else{
        SecurityTelegramState.SECURITY
    }
}