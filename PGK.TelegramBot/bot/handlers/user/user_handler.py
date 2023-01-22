from aiogram import Dispatcher, types

from database.user import user_data_store
from database.user.model.user_model import get_user_fio


async def get_user_handler(message: types.Message):
    telegram_id = message.from_user.id
    user = await user_data_store.get_user_by_id(telegramId=telegram_id)

    if user:
        if user.photoUrl:
            await message.answer_photo(photo=user.photoUrl, caption=get_user_fio(user))
        else:
            await message.answer(get_user_fio(user))
    else:
        await message.answer('необходимо авторизоваться 🔒')


def register_user(dp: Dispatcher):
    dp.register_message_handler(get_user_handler, commands=['user'])
