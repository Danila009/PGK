import requests


def add_telegram_id(telegramId: int, telegramToken: str) -> bool:
    url = f'https://api.cfif31.ru/pgk63/api/User/Telegram/{telegramId}'

    response = requests.patch(url, headers={'telegramToken': telegramToken})

    return response.status_code == 200
