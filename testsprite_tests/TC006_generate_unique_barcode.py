import requests

BASE_URL = "http://localhost:5010"
LOGIN_URL = f"{BASE_URL}/api/Auth/Login"
GENERATE_BARCODE_URL = f"{BASE_URL}/api/Products/generate-barcode"
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
AUTH_PAYLOAD = {"email": "stronger_osman@hotmail.com", "password": "ezEaE_76"}
TIMEOUT = 30

def test_generate_unique_barcode():
    # Authenticate and get JWT token
    try:
        login_resp = requests.post(LOGIN_URL, json=AUTH_PAYLOAD, timeout=TIMEOUT)
        login_resp.raise_for_status()
    except Exception as e:
        assert False, f"Authentication failed: {e}"
    token = login_resp.json().get("token")
    assert token, "Failed to obtain JWT token from login response"

    headers = {"Authorization": f"Bearer {token}"}

    # Test parameters with both int and string barcodeType forms
    test_cases = [
        {"barcodeType": 0, "prefix": "TEST0"},
        {"barcodeType": 1, "prefix": "TEST1"},
        {"barcodeType": 2, "prefix": "TEST2"},
        {"barcodeType": 3, "prefix": "TEST3"},
        {"barcodeType": 4, "prefix": "TEST4"},
        {"barcodeType": "EAN13", "prefix": "EAN13P"},
        {"barcodeType": "EAN8", "prefix": "EAN8P"},
        {"barcodeType": "Code128", "prefix": "C128P"},
        {"barcodeType": "Code39", "prefix": "C39P"},
        {"barcodeType": "Internal", "prefix": "INTP"},
    ]

    generated_barcodes = set()

    for case in test_cases:
        params = {"barcodeType": case["barcodeType"], "prefix": case["prefix"]}
        try:
            resp = requests.get(GENERATE_BARCODE_URL, headers=headers, params=params, timeout=TIMEOUT)
            resp.raise_for_status()
        except Exception as e:
            assert False, f"Failed to generate barcode for barcodeType '{case['barcodeType']}' with prefix '{case['prefix']}': {e}"

        json_resp = resp.json()
        barcode = json_resp.get("barcode") or json_resp.get("data") or json_resp.get("result")
        # Fallback: If the API returns the barcode in a different key, check all keys and pick the value if string.
        if not barcode:
            if isinstance(json_resp, dict):
                for v in json_resp.values():
                    if isinstance(v, str):
                        barcode = v
                        break

        assert barcode, f"No barcode returned for barcodeType '{case['barcodeType']}' with prefix '{case['prefix']}'"
        assert barcode.startswith(case["prefix"]), f"Returned barcode '{barcode}' does not start with prefix '{case['prefix']}'"
        # Ensure barcode uniqueness in this test run
        assert barcode not in generated_barcodes, f"Duplicate barcode generated: {barcode}"
        generated_barcodes.add(barcode)

test_generate_unique_barcode()
