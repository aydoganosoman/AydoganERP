import requests

BASE_URL = "http://localhost:5010"
LOGIN_URL = f"{BASE_URL}/api/Auth/Login"
NEXT_CODE_URL = f"{BASE_URL}/api/Products/next-code"
EMAIL = "stronger_osman@hotmail.com"
PASSWORD = "ezEaE_76"
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30

def test_generate_next_product_code():
    # Login to get JWT token
    login_payload = {"email": EMAIL, "password": PASSWORD}
    try:
        login_resp = requests.post(LOGIN_URL, json=login_payload, timeout=TIMEOUT)
        assert login_resp.status_code == 200, f"Login failed: {login_resp.text}"
        token = login_resp.json().get("token", {}).get("token")
        assert token, "Token not found in login response"
    except requests.RequestException as e:
        assert False, f"Login request failed: {e}"

    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }

    params = {"companyId": COMPANY_ID}
    try:
        resp = requests.get(NEXT_CODE_URL, headers=headers, params=params, timeout=TIMEOUT)
        assert resp.status_code == 200, f"Expected 200 OK but got {resp.status_code}, response: {resp.text}"
        json_resp = resp.json()
        # Assert the response contains a string next product code and is not empty or null
        assert json_resp is not None, "Response JSON is None"
        assert isinstance(json_resp, str) or isinstance(json_resp, dict), "Response should be a string or dict with next product code"
        # In case the returned json is a dict with a key containing the code, try to validate that too
        if isinstance(json_resp, dict):
            # There is no explicit schema for this response; check if any key exists and value is a string
            found_code = False
            for v in json_resp.values():
                if isinstance(v, str) and v.strip():
                    found_code = True
                    break
            assert found_code, f"Response dict does not contain a valid next product code string: {json_resp}"
        else:
            # direct string response
            assert json_resp.strip(), "Next product code string is empty"
    except requests.RequestException as e:
        assert False, f"Request to generate next product code failed: {e}"

test_generate_next_product_code()