from peewee import PrimaryKeyField, IntegerField, TextField

from database.models.base_model import BaseModel


class User(BaseModel):
    id = PrimaryKeyField(column_name='id')
    telegramId = IntegerField(column_name='TelegramId')
    firstName = TextField(column_name='FirstName')
    lastName = TextField(column_name='LastName')
    middleName = TextField(column_name='MiddleName')
    email = TextField(column_name='Email')
    photoUrl = TextField(column_name='PhotoUrl')
    password = TextField(column_name='Password')
    refreshToken = TextField(column_name='RefreshToken')

    class Meta:
        table_name = 'Users'


def get_user_fio(user: User) -> str:

    email = ''

    if user.email:
        email = f'✉️ Электронная почта {user.email}'

    if user.middleName:
        return f'🧓 {user.firstName} {user.lastName} {user.middleName}\n{email}'
    else:
        return f'🧓 {user.firstName} {user.lastName}\n{email}'
