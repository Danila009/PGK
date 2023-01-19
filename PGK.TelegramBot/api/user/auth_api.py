import requests


def get_access_token(refreshToken: str) -> str:
    url = f'https://api.cfif31.ru/pgk63/api/Auth/Refresh'

    response = requests.post(url, headers={'refreshToken': refreshToken})

    json = response.json()

    return json['accessToken']

