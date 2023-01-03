package ru.pgk63.feature_tech_support.screen.chatScreen.viewModel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import ru.pgk63.core_common.api.techSupport.model.MessageListParameters
import ru.pgk63.core_common.api.techSupport.model.SendMessageBody
import ru.pgk63.core_common.api.techSupport.model.UpdateMessageBody
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

    val user = userDataSource.get()
        .stateIn(viewModelScope, SharingStarted.Eagerly, UserLocalDatabase())

    val responseMessages = messagesWebSocket.messageResponse.asStateFlow()

    fun webSocketConnect() {
        messagesParameters()

        messagesWebSocket.connect()
    }

    fun messagesParameters(parameters: MessageListParameters = MessageListParameters()) {
        messagesWebSocket.setMessagesParameters(parameters)
    }

    fun sendMessage(body: SendMessageBody) {
        viewModelScope.launch {
            techSupportRepository.sendMessage(body)
        }
    }

    fun pinMessage(messageId: Int) {
        viewModelScope.launch {
            try {
                techSupportRepository.pinMessage(messageId)
            }catch (_:Exception){

            }
        }
    }

    fun deleteMessage(messageId: Int){
        viewModelScope.launch {
            try {
                techSupportRepository.deleteMessage(messageId)
            }catch (_:Exception){

            }
        }
    }

    fun updateMessage(messageId: Int, body: UpdateMessageBody){
        viewModelScope.launch {
            try {
                techSupportRepository.updateMessage(messageId, body)
            }catch (_:Exception){

            }
        }
    }

    override fun onCleared() {
        super.onCleared()
        messagesWebSocket.clear()
    }
}