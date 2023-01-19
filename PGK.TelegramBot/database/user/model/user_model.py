from peewee import PrimaryKeyField, IntegerField, TextField

from database.models.base_model import BaseModel


class User(BaseModel):
    id = PrimaryKeyField(column_name='id')
    telegramId = IntegerField(column_name='TelegramId')
    password = TextField(column_name='Password')
    refreshToken = TextField(column_name='RefreshToken')

    class Meta:
        table_name = 'Users'
