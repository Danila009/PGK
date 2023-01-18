package ru.pgk63.feature_auth.screens.registrationUser.model

import ru.pgk63.core_common.api.headman.model.HeadmanRegistrationBody
import ru.pgk63.core_common.api.student.model.StudentRegistrationBody
import ru.pgk63.core_common.api.user.model.UserRegistrationBody
import ru.pgk63.core_common.enums.user.UserRole

internal sealed class RegistrationUserState(val userRole: UserRole) {
    class Base(val body: UserRegistrationBody, userRole: UserRole): RegistrationUserState(userRole)
    class Headman(val body: HeadmanRegistrationBody, val deputy: Boolean): RegistrationUserState(
        if(deputy)
            UserRole.DEPUTY_HEADMAN
        else
            UserRole.HEADMAN
    )
    class Student(val body: StudentRegistrationBody): RegistrationUserState(UserRole.STUDENT)
}