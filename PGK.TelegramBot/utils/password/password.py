from api.user.auth_api import get_access_token
from api.user import user_api
from database.user.user_data_store import get_user_by_id


async def update_password(telegramId: int) -> str:
    user = await get_user_by_id(telegramId)

    if user:
        access_token = get_access_token(user.refreshToken)
        new_password = user_api.update_password(access_token)

        if new_password:
            return f'🔐 Ваш новый пароль\n{new_password}'
        else:
            return 'Произошла ошибка'
    else:
        return 'Вам необходимо авторизироваться'
