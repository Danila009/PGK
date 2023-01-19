import asyncio
import logging

from aiogram import Bot, Dispatcher
from aiogram.contrib.fsm_storage.memory import MemoryStorage

from bot.data.config import BOT_TOKEN
from bot.handlers.auth.auth_handler import register_auth
from bot.handlers.errors.errors_handler import register_errors_handler
from bot.handlers.password_reset.password_reset_heandler import register_password_reset
from bot.services.setting_commands import set_default_commands
from database.create_database import create_database

bot = Bot(token=BOT_TOKEN, parse_mode='HTML')
dp = Dispatcher(bot, storage=MemoryStorage())


def register_all_handlers():
    register_errors_handler(dp)

    register_auth(dp)
    register_password_reset(dp)


async def on_startup():
    create_database()


async def set_all_default_commands():
    await set_default_commands(bot=bot)


async def main():
    try:
        logging.basicConfig(level=logging.INFO)

        await set_all_default_commands()
        register_all_handlers()

        await on_startup()
        await dp.start_polling()

        await bot.get_webhook_info()
    finally:
        await bot.delete_webhook()
        await dp.storage.close()
        await dp.storage.wait_closed()
        await bot.session.close()


if __name__ == '__main__':
    asyncio.run(main())
