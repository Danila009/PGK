from aiogram import Bot
from aiogram.types import BotCommand


async def set_default_commands(bot: Bot):
    await bot.set_my_commands(
        commands=[
            BotCommand('auth', 'авторизоваться'),
            BotCommand('password_reset', 'сброс пароля'),
            BotCommand('documentation', 'справка')
        ]
    )
