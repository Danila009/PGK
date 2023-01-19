from database.user.model.user_model import User


async def get_user_by_id(telegramId: int) -> User:
    return User.get_or_none(User.telegramId == telegramId)
