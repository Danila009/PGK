from database.models.base_model import db


def create_database():
    db.connect()
