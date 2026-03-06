import requests
import uuid

BASE_URL = "http://localhost:5010"
LOGIN_URL = f"{BASE_URL}/api/Auth/login"
PRODUCTS_URL = f"{BASE_URL}/api/Products"
EMAIL = "stronger_osman@hotmail.com"
PASSWORD = "ezEaE_76"
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
CATEGORY_ID = str(uuid.uuid4())
TIMEOUT = 30


def get_jwt_token():
    resp = requests.post(
        LOGIN_URL,
        json={"email": EMAIL, "password": PASSWORD},
        timeout=TIMEOUT,
    )
    resp.raise_for_status()
    json_resp = resp.json()
    token = json_resp.get("accessToken") or json_resp.get("token")
    assert token, "No token found in login response"
    return token


def create_product(auth_header):
    # Minimal valid product data for creation, adjusted with categoryId
    product_data = {
        "companyId": COMPANY_ID,
        "categoryId": CATEGORY_ID,
        "code": f"TC009-{uuid.uuid4()}",
        "name": "Test Product for TC009",
        "isActive": True,
        "price": 10.0
    }
    resp = requests.post(
        PRODUCTS_URL,
        json=product_data,
        headers=auth_header,
        timeout=TIMEOUT,
    )
    resp.raise_for_status()
    product = resp.json()
    product_id = product.get("id")
    assert product_id, "No product ID returned after creation"
    return product_id


def delete_product(product_id, auth_header):
    url = f"{PRODUCTS_URL}/{product_id}"
    resp = requests.delete(url, headers=auth_header, timeout=TIMEOUT)
    # No assertion on delete; allow 204 or 200 or 404 if already deleted.
    if resp.status_code not in [200, 204, 404]:
        resp.raise_for_status()


def test_get_current_stock_level():
    token = get_jwt_token()
    headers = {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}

    product_id = None
    try:
        product_id = create_product(headers)

        stock_level_url = f"{PRODUCTS_URL}/{product_id}/stock-level"
        resp = requests.get(stock_level_url, headers=headers, timeout=TIMEOUT)
        resp.raise_for_status()

        stock_data = resp.json()
        # Expecting stock level data — details depend on API. Assert that stock level is present and numeric (e.g., integer or float)
        assert stock_data is not None, "Response JSON is empty or null"
        assert isinstance(stock_data, dict), "Stock level response is not a JSON object"
        # Common field name assumptions: "stockLevel" or similar
        possible_fields = ["stockLevel", "currentStock", "quantity", "stock"]
        found_field = None
        for f in possible_fields:
            if f in stock_data:
                found_field = f
                break

        assert found_field is not None, f"Stock level field not found in response keys: {stock_data.keys()}"
        stock_level_value = stock_data[found_field]
        assert isinstance(stock_level_value, (int, float)), f"Stock level value is not numeric: {stock_level_value}"
        assert stock_level_value >= 0, f"Stock level should not be negative, got: {stock_level_value}"

    finally:
        if product_id:
            delete_product(product_id, headers)


test_get_current_stock_level()