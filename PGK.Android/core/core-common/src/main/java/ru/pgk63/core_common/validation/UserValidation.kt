package ru.pgk63.core_common.validation

import ru.pgk63.core_common.R

fun emailValidation(email:String): Pair<Boolean, Int?> {
    if(email.isEmpty())
        return false to R.string.field_required

    if(!email.any("."::contains) || !email.any("@"::contains))
        return false to R.string.incorrect_email

    return true to null
}

fun passwordValidation(password:String): Pair<Boolean, Int?> {
    if(password.isEmpty())
        return false to R.string.field_required

    if(password.length < 4)
        return false to R.string.incorrect_password

    return true to null
}

fun nameValidation(name:String): Pair<Boolean, Int?> {
    if(name.isEmpty())
        return false to R.string.field_required

    return true to null
}