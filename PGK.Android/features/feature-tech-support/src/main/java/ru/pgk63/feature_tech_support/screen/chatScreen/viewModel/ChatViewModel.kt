package ru.pgk63.feature_tech_support.screen.chatScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.cachedIn
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.techSupport.model.GetMessageListParameters
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody
import ru.pgk63.core_common.api.techSupport.paging.MessagePagingSource
import ru.pgk63.core_common.api.techSupport.repository.TechSupportRepository
import ru.pgk63.core_common.api.techSupport.webSocket.MessagesWebSocket
import ru.pgk63.core_database.user.UserDataSource
import ru.pgk63.core_database.user.model.UserLocalDatabase
import javax.inject.Inject

@HiltViewModel
internal class ChatViewModel @Inject constructor(
    private val techSupportRepository: TechSupportRepository,
    userDataSource: UserDataSource
): ViewModel() {

    private val messagesWebSocket = MessagesWebSocket(userDataSource)

    val messages = Pager(PagingConfig(pageSize = 20)){
        MessagePagingSource(messagesWebSocket)
    }.flow.cachedIn(viewModelScope)

    val user = userDataSource.get()
        .stateIn(viewModelScope, SharingStarted.Eagerly, UserLocalDatabase())

    fun connectMessagesWebSocket(messagesParameters: GetMessageListParameters = GetMessageListParameters()){
        messagesWebSocket.setMessagesParameters(messagesParameters)
        messagesWebSocket.connect()
    }

    fun sendMessage(body: SendMessageBody) {
        viewModelScope.launch {
            techSupportRepository.sendMessage(body)
        }
    }

    override fun onCleared() {
        super.onCleared()
        messagesWebSocket.clear()
    }
}