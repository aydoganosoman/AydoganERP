import requests

BASE_URL = "http://localhost:5010"
LOGIN_URL = f"{BASE_URL}/api/Auth/login"
PRODUCTS_URL = f"{BASE_URL}/api/Products"
AUTH_PAYLOAD = {"email": "stronger_osman@hotmail.com", "password": "ezEaE_76"}
HEADERS = {"Content-Type": "application/json"}
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30


def authenticate():
    response = requests.post(LOGIN_URL, json=AUTH_PAYLOAD, headers=HEADERS, timeout=TIMEOUT)
    response.raise_for_status()
    token = response.json().get("token")
    if not token:
        raise ValueError("Authentication token not found in login response")
    return token


def create_product(token, code):
    headers = {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}
    product_payload = {
        "companyId": COMPANY_ID,
        "code": code,
        "name": "Test Product",
        "categoryId": "00000000-0000-0000-0000-000000000000",  # Dummy categoryId for valid payload
        "isActive": True
    }
    response = requests.post(PRODUCTS_URL, json=product_payload, headers=headers, timeout=TIMEOUT)
    response.raise_for_status()
    data = response.json()
    product_id = data.get("id")
    if not product_id:
        raise ValueError("Created product ID not found in response")
    return product_id


def delete_product(token, product_id):
    headers = {"Authorization": f"Bearer {token}"}
    url = f"{PRODUCTS_URL}/{product_id}"
    requests.delete(url, headers=headers, timeout=TIMEOUT)  # Ignoring response as best effort cleanup


def check_product_code_exists(token, code, exclude_id=None):
    headers = {"Authorization": f"Bearer {token}"}
    params = {"companyId": COMPANY_ID, "code": code}
    if exclude_id:
        params["excludeId"] = exclude_id
    url = f"{PRODUCTS_URL}/check-code"
    response = requests.get(url, headers=headers, params=params, timeout=TIMEOUT)
    response.raise_for_status()
    return response.json()


def test_check_product_code_existence():
    token = authenticate()

    # Create a product to test existence
    test_code = "TC004TESTCODE123"
    product_id = None
    try:
        product_id = create_product(token, test_code)

        # Case 1: Check existence without excludeId - should return exists=True or similar
        result = check_product_code_exists(token, test_code)
        assert isinstance(result, dict), "Response should be a dict"
        # Assuming API returns something like {"exists": true/false} or similar boolean indication
        assert result.get("exists") is True, "Product code should exist when not excluding the product"

        # Case 2: Check existence with excludeId set to the created product id - should return exists=False
        result_exclude = check_product_code_exists(token, test_code, exclude_id=product_id)
        assert isinstance(result_exclude, dict), "Response should be a dict"
        assert result_exclude.get("exists") is False, "Product code should not exist when excluding the product ID itself"

        # Case 3: Check existence for non-existent code - should return exists=False
        non_existent_code = "NONEXISTENT_CODE_9999"
        result_non_exist = check_product_code_exists(token, non_existent_code)
        assert isinstance(result_non_exist, dict), "Response should be a dict"
        assert result_non_exist.get("exists") is False, "Non-existent product code should not be found"

    finally:
        if product_id:
            delete_product(token, product_id)


test_check_product_code_existence()
