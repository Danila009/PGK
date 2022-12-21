package ru.pgk63.core_common.api.techSupport.webSocket

import android.text.TextUtils
import android.util.Log
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.launch
import okhttp3.*
import ru.pgk63.core_common.api.techSupport.model.GetMessageListParameters
import ru.pgk63.core_common.api.techSupport.model.MessageResponse
import ru.pgk63.core_common.extension.decodeFromString
import ru.pgk63.core_common.extension.encodeToString
import ru.pgk63.core_database.user.UserDataSource

class MessagesWebSocket (
    private val userDataSource: UserDataSource
) : WebSocketListener() {

    private companion object {
        const val DELAY_WEB_SOCKET_CONNECT = 100L
    }

    private var postPayload = ""

    private var webSocket: WebSocket? = null

    private var messagesParameters = GetMessageListParameters()
    var messageResponse = MutableStateFlow<MessageResponse?>(null)

    fun connect() {
        val request = Request.Builder()
            .url("wss://api.cfif31.ru/pgk63/ws/Chat/Message")
            .addHeader("Authorization","Bearer ${userDataSource.getAccessToken()}")
            .build()

        val client = OkHttpClient()

        client.newWebSocket(request, this)
        client.dispatcher.executorService.shutdown()
    }

    private fun sendRepeatHeartMessage() {
        if (TextUtils.equals(postPayload, messageResponse.value.encodeToString())) {
            postPayload = System.currentTimeMillis().toString()
            sendMessageDetail(postPayload)
        } else {
            CoroutineScope(Dispatchers.IO).launch {
                delay(DELAY_WEB_SOCKET_CONNECT)
                connect()
            }
        }
    }

    private fun sendMessageDetail(postPayload: String) {
        if (webSocket != null) {
            webSocket?.send(postPayload)
        }
    }

    fun setMessagesParameters(messagesParameters: GetMessageListParameters){
        this.messagesParameters = messagesParameters
    }

    fun clear() {
        postPayload = ""
        messageResponse.value = null
        if (webSocket != null) {
            webSocket?.cancel()
            webSocket = null
        }
    }

    override fun onOpen(webSocket: WebSocket, response: Response) {
        webSocket.send(messagesParameters.encodeToString())
        this.webSocket = webSocket
        sendRepeatHeartMessage()
    }

    override fun onClosed(webSocket: WebSocket, code: Int, reason: String) {
        CoroutineScope(Dispatchers.Main).launch {
            delay(DELAY_WEB_SOCKET_CONNECT)
            connect()
        }
    }

    override fun onClosing(webSocket: WebSocket, code: Int, reason: String) {
        CoroutineScope(Dispatchers.Main).launch {
            delay(DELAY_WEB_SOCKET_CONNECT)
            connect()
        }
    }

    override fun onMessage(webSocket: WebSocket, text: String) {
        Log.e("onMessage", "message: $text")
        messageResponse.value = text.decodeFromString()
    }

    override fun onFailure(webSocket: WebSocket, t: Throwable, response: Response?) {
        Log.e("onFailure", "message: ${t.message}")
        CoroutineScope(Dispatchers.Main).launch {
            delay(DELAY_WEB_SOCKET_CONNECT)
            connect()
        }
    }
}