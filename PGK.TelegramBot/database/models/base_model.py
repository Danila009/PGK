from peewee import Model, MySQLDatabase

db = MySQLDatabase('ISPr24-39_BeluakovDS_PGK', user='ISPr24-39_BeluakovDS', password='ISPr24-39_BeluakovDS',
                   host='cfif31.ru', port=3306)


class BaseModel(Model):
    class Meta:
        database = db
        order_by = 'id'
