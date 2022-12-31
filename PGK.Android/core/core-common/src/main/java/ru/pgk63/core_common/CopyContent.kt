package ru.pgk63.core_common

import android.content.ClipData
import android.content.Context

fun Context.setClipboard(text: String) {
    val clipboard =
        this.getSystemService(Context.CLIPBOARD_SERVICE) as android.content.ClipboardManager
    val clip = ClipData.newPlainText("Copied Text", text)
    clipboard.setPrimaryClip(clip)
}