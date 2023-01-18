import logging

from aiogram import Dispatcher
from aiogram.types import Update


async def errors_handler(update, exception):
    from aiogram.utils.exceptions import (Unauthorized, InvalidQueryID, TelegramAPIError,
                                          CantDemoteChatCreator, MessageNotModified, MessageToDeleteNotFound,
                                          MessageTextIsEmpty, RetryAfter,
                                          CantParseEntities, MessageCantBeDeleted, BadRequest)

    await Update.get_current().message.reply('Ошибка: {}'.format(exception))

    if isinstance(exception, CantDemoteChatCreator):
        logging.debug("Can`t demote chat creator")
        return True

    if isinstance(exception, MessageNotModified):
        logging.debug("Message is not modified")
        return True

    if isinstance(exception, MessageCantBeDeleted):
        logging.info("Message can be deleted")
        return True

    if isinstance(exception, MessageToDeleteNotFound):
        logging.info("Message to delete not found")
        return True

    if isinstance(exception, MessageTextIsEmpty):
        logging.debug("MessageTextIsEmpty")
        return True

    if isinstance(exception, Unauthorized):
        logging.info(f"Unauthorized: {exception}")
        return True

    if isinstance(exception, InvalidQueryID):
        logging.exception(f"InvalidQueryID: {exception} \nUpdate: {update}")
        return True

    if isinstance(exception, CantParseEntities):
        await Update.get_current().message.answer(
            "Попало в эррор хендлер. CantParseEntities: {}".format(exception.args))
        return True

    if isinstance(exception, RetryAfter):
        logging.exception(f"RetryAfter: {exception} \nUpdate: {update}")
        return True

    if isinstance(exception, BadRequest):
        logging.exception(f"CantParseEntities: {exception} \nUpdate {update}")
        return True

    if isinstance(exception, TelegramAPIError):
        logging.exception(f"TelegramAPIError: {exception} \nUpdate {update}")
        return True

    logging.exception(f"Update: {update} \n{exception}")


def register_errors_handler(dp: Dispatcher):
    dp.register_errors_handler(errors_handler)
