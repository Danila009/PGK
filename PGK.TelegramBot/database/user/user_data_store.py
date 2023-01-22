from database.models.base_model import db
from database.user.model.user_model import User


async def get_user_by_id(telegramId: int) -> User:
    db.connect()
    user = User.get_or_none(User.telegramId == telegramId)
    db.close()
    return user
