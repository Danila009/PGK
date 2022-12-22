package ru.pgk63.core_common.api.student.repository

import ru.pgk63.core_common.api.student.StudentApi
import ru.pgk63.core_common.common.response.ApiResponse
import javax.inject.Inject

class StudentRepsitory @Inject constructor(
    private val studentApi: StudentApi
): ApiResponse() {
    
}