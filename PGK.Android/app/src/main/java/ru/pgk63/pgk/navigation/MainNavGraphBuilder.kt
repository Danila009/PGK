package ru.pgk63.pgk.navigation

import androidx.navigation.NavController
import androidx.navigation.NavGraphBuilder
import com.example.feature_student.navigation.StudentDetailsDestination
import com.example.feature_student.navigation.StudentListDestination
import com.example.feature_student.navigation.studentNavigation
import ru.lfybkf19.feature_journal.navigation.JournalDetailsDestination
import ru.lfybkf19.feature_journal.navigation.JournalListDestination
import ru.lfybkf19.feature_journal.navigation.journalNavigation
import ru.pgk63.core_common.enums.user.UserRole
import ru.pgk63.feature_auth.navigation.authNavigation
import ru.pgk63.feature_department.navigation.DepartmentDetailsDestination
import ru.pgk63.feature_department.navigation.DepartmentListDestination
import ru.pgk63.feature_department.navigation.departmentNavigation
import ru.pgk63.feature_group.navigation.GroupListDestination
import ru.pgk63.feature_group.navigation.GroupDetailsDestination
import ru.pgk63.feature_group.navigation.groupNavigation
import ru.pgk63.feature_main.navigation.mainNavigation
import ru.pgk63.feature_profile.navigation.ProfileDestination
import ru.pgk63.feature_profile.navigation.profileNavigation
import ru.pgk63.feature_raportichka.navigation.RaportichkaAddRowDestination
import ru.pgk63.feature_raportichka.navigation.RaportichkaListDestination
import ru.pgk63.feature_raportichka.navigation.RaportichkaSortingDestination
import ru.pgk63.feature_raportichka.navigation.raportichkaNavigation
import ru.pgk63.feature_settings.navigation.*
import ru.pgk63.feature_specialization.navigation.SpecializationDetailsDestination
import ru.pgk63.feature_specialization.navigation.SpecializationListDestination
import ru.pgk63.feature_specialization.navigation.specializationNavigation
import ru.pgk63.feature_subject.navigation.SubjectDetailsDestination
import ru.pgk63.feature_subject.navigation.SubjectListDestination
import ru.pgk63.feature_subject.navigation.subjectNavigation
import ru.pgk63.feature_tech_support.navigation.TechSupportChatDestination
import ru.pgk63.feature_tech_support.navigation.TechSupportChatListDestination
import ru.pgk63.feature_tech_support.navigation.techSupportNavigation

fun NavGraphBuilder.mainNavGraphBuilder(
    navController: NavController
){
    mainNavigation(
        onTechSupportChatScreen = { userRole ->
            navController.navigate(
                if(userRole != UserRole.ADMIN)
                    TechSupportChatDestination.route
                else
                    TechSupportChatListDestination.route
            )
        },
        onGroupScreen = {
            navController.navigate(GroupListDestination.route)
        },
        onSpecializationListScreen = {
            navController.navigate(SpecializationListDestination.route)
        },
        onSubjectListScreen = {
            navController.navigate(SubjectListDestination.route)
        },
        onStudentListScreen = {
            navController.navigate(StudentListDestination.route)
        },
        onProfileScreen = {
            navController.navigate(ProfileDestination.route)
        },
        onDepartmentListScreen = {
            navController.navigate(DepartmentListDestination.route)
        },
        onRaportichkaScreen = { userRole, userId ->
            if(userRole != UserRole.STUDENT && userRole != UserRole.HEADMAN &&userRole != UserRole.DEPUTY_HEADMAN){
                navController.navigate(RaportichkaSortingDestination.route)
            }else {
                navController.navigate(
                    RaportichkaListDestination.route +
                            "?${RaportichkaListDestination.studentIds}=${listOf(userId)}"
                )
            }
        },
        onJournalScreen = { userRole, userId ->
            navController.navigate(JournalListDestination.route)
        },
        onSettingsScreen = {
            navController.navigate(SettingsDestination.route)
        }
    )

    authNavigation()

    groupNavigation(
        onGroupDetailsScreen = { id ->
            navController.navigate("${GroupDetailsDestination.route}/$id")
        },
        onStudentDetailsScreen = { id ->
            navController.navigate("${StudentDetailsDestination.route}/$id")
        },
        onDepartmentDetailsScreen = { id ->
            navController.navigate("${DepartmentDetailsDestination.route}/$id")
        },
        onSpecializationDetailsScreen = { id ->
            navController.navigate("${SpecializationDetailsDestination.route}/$id")
        },
        onBackScreen = { navController.navigateUp() }
    )

    techSupportNavigation(
        onBackScreen = { navController.navigateUp() },
        onChatScreen = { chatId ->
            navController.navigate("${TechSupportChatDestination.route}?" +
                    "${TechSupportChatDestination.chatId}=$chatId")
        }
    )

    specializationNavigation(
        onBackScreen = { navController.navigateUp() },
        onSpecializationDetailsScreen = { id ->
            navController.navigate("${SpecializationDetailsDestination.route}/$id")
        },
        onGroupDetailsScreen = { id ->
            navController.navigate("${GroupDetailsDestination.route}/$id")
        },
        onDepartmentDetailsScreen = { id ->
            navController.navigate("${DepartmentDetailsDestination.route}/$id")
        }
    )

    subjectNavigation(
        onBackScreen = { navController.navigateUp() },
        onSubjectDetailsScreen = { id ->
            navController.navigate("${SubjectDetailsDestination.route}/$id")
        }
    )

    studentNavigation(
        onBackScreen = { navController.navigateUp() },
        onStudentDetailsScreen = { id ->
            navController.navigate("${StudentDetailsDestination.route}/$id")
        }
    )

    profileNavigation(
        onBackScreen = { navController.navigateUp() },
    )

    departmentNavigation(
        onBackScreen = { navController.navigateUp() },
        onDepartmentDetailsScreen = { id ->
            navController.navigate("${DepartmentDetailsDestination.route}/$id")
        },
        onSpecializationDetailsScreen = { id ->
            navController.navigate("${SpecializationDetailsDestination.route}/$id")
        },
        onGroupDetailsScreen = { id ->
            navController.navigate("${GroupDetailsDestination.route}/$id")
        }
    )

    raportichkaNavigation(
        onBackScreen = { navController.navigateUp() },
        onStudentDetailsScreen = { studentId ->
            navController.navigate("${StudentDetailsDestination.route}/$studentId")
        },
        onTeacherDetailsScreen = { teacherId ->

        },
        onSubjectDetailsScreen = { subjectId ->
            navController.navigate("${SubjectDetailsDestination.route}/$subjectId")
        },
        onRaportichkaScreen = { studentId, groupsId, subjectsId, teacherId, startDate, endDate, onlyDate ->
            navController.navigate(
                RaportichkaListDestination.route +
                        "?${RaportichkaListDestination.studentIds}=$studentId" +
                        "&${RaportichkaListDestination.groupIds}=$groupsId" +
                        "&${RaportichkaListDestination.subjectIds}=$subjectsId" +
                        "&${RaportichkaListDestination.teacherIds}=$teacherId" +
                        "&${RaportichkaListDestination.startDate}=$startDate" +
                        "&${RaportichkaListDestination.endDate}=$endDate" +
                        "&${RaportichkaListDestination.onlyDate}=$onlyDate"
            )
        },
        onRaportichkaAddRowScreen = { raportichkaId, groupId ->
            navController.navigate(
                "${RaportichkaAddRowDestination.route}/$raportichkaId?" +
                        "${RaportichkaAddRowDestination.groupId}=$groupId"
            )
        },

        onRaportichkaUpdateRowScreen = { raportichkaId, groupId, rowId, updateTeacherId,
                                         updateNumberLesson, updateCountHours, updateStudentId, updateSubjectId ->
            navController.navigate(
                "${RaportichkaAddRowDestination.route}/$raportichkaId?" +
                        "${RaportichkaAddRowDestination.groupId}=$groupId" +
                        "&${RaportichkaAddRowDestination.raportichkaRowId}=$rowId" +
                        "&${RaportichkaAddRowDestination.updateTeacherId}=$updateTeacherId" +
                        "&${RaportichkaAddRowDestination.updateNumberLesson}=$updateNumberLesson" +
                        "&${RaportichkaAddRowDestination.updateCountHours}=$updateCountHours" +
                        "&${RaportichkaAddRowDestination.updateStudentId}=$updateStudentId" +
                        "&${RaportichkaAddRowDestination.updateSubjectId}=$updateSubjectId"
            )
        }
    )

    journalNavigation(
        onBackScreen = {
            navController.navigateUp()
        },
        onJournalDetailsScreen = { journalId ->
            navController.navigate("${JournalDetailsDestination.route}/$journalId")
        }
    )

    settingsNavigation(
        onBackScreen = { navController.navigateUp() },
        onSettingsSecurityScreen = {
            navController.navigate(SettingsSecurityDestination.route)
        },
        onSettingsNotificationsScreen = {
            navController.navigate(SettingsNotificationsDestination.route)
        },
        onSettingsLanguageScreen = {
            navController.navigate(SettingsLanguageDestination.route)
        },
        onSettingsAppearanceScreen = {
            navController.navigate(SettingsAppearanceDestination.route)
        },
        onChangePasswordScreen = {
            navController.navigate(ChangePasswordDestination.route)
        }
    )
}