from aiogram import Dispatcher, types

from utils.password.password import update_password


async def password_reset_handler(message: types.Message):
    telegramId = message.from_user.id
    result = await update_password(telegramId=telegramId)
    await message.answer(str(result))


def register_password_reset(dp: Dispatcher):
    dp.register_message_handler(password_reset_handler, commands=['password_reset'])
