import requests

BASE_URL = "http://localhost:5010"
LOGIN_ENDPOINT = "/api/Auth/login"
PRODUCTS_NEXT_CODE_ENDPOINT = "/api/Products/next-code"
LOGIN_PAYLOAD = {
    "email": "stronger_osman@hotmail.com",
    "password": "ezEaE_76"
}
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30


def test_get_next_product_code_generation():
    # Authenticate to get JWT token
    login_url = BASE_URL + LOGIN_ENDPOINT
    try:
        login_resp = requests.post(login_url, json=LOGIN_PAYLOAD, timeout=TIMEOUT)
        login_resp.raise_for_status()
    except requests.RequestException as e:
        assert False, f"Authentication failed: {e}"

    login_data = login_resp.json()
    token = login_data.get("token", {}).get("token")
    assert token, "Authentication token missing in login response"

    headers = {
        "Authorization": f"Bearer {token}"
    }

    # Call the next-code endpoint with companyId query param
    next_code_url = BASE_URL + PRODUCTS_NEXT_CODE_ENDPOINT
    params = {
        "companyId": COMPANY_ID
    }
    try:
        resp = requests.get(next_code_url, headers=headers, params=params, timeout=TIMEOUT)
        resp.raise_for_status()
    except requests.RequestException as e:
        assert False, f"GET /api/Products/next-code request failed: {e}"

    # Validate the response content
    data = resp.json()

    # Basic validation: We expect a string or number for the next code. 
    # Common practice would be a string code.
    assert data is not None, "Response JSON is None"
    assert isinstance(data, (str, int)), f"Unexpected response type, expected str or int but got {type(data)}"
    # Additionally, string should not be empty if str
    if isinstance(data, str):
        assert data.strip() != "", "Next product code is empty string"


test_get_next_product_code_generation()