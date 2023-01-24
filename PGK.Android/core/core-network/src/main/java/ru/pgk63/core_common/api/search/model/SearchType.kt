package ru.pgk63.core_common.api.search.model

import androidx.annotation.StringRes
import ru.pgk63.core_common.R

enum class SearchType(@StringRes val nameId: Int) {
    STUDENT(R.string.student),
    HEADMAN(R.string.headman),
    DEPUTY_HEADMAN(R.string.deputy_headman),
    TEACHER(R.string.teacher),
    DEPARTMENT_HEAD(R.string.department_head),
    DEPARTMENT(R.string.department),
    GROUP(R.string.group),
    SPECIALITY(R.string.speciality),
    SUBJECT(R.string.subject)
}