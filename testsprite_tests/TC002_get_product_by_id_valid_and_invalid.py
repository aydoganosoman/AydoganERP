import requests
import uuid

BASE_URL = "http://localhost:5010"
AUTH_ENDPOINT = "/api/Auth/login"
PRODUCTS_ENDPOINT = "/api/Products"
EMAIL = "stronger_osman@hotmail.com"
PASSWORD = "ezEaE_76"
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30


def authenticate():
    url = BASE_URL + AUTH_ENDPOINT
    payload = {"email": EMAIL, "password": PASSWORD}
    response = requests.post(url, json=payload, timeout=TIMEOUT)
    response.raise_for_status()
    json_data = response.json()
    token = json_data.get("token") or json_data.get("accessToken")
    assert token, "Authentication token not found in response"
    return token


def create_product(headers):
    url = BASE_URL + PRODUCTS_ENDPOINT
    product_code = "TC002-" + str(uuid.uuid4())
    category_id = str(uuid.uuid4())  # Random UUID for categoryId
    product_payload = {
        "code": product_code,
        "companyId": COMPANY_ID,
        "name": "Test Product TC002",
        "barcode": "1234567890123",
        "barcodeType": "EAN13",
        "isActive": True,
        "categoryId": category_id
    }
    response = requests.post(url, json=product_payload, headers=headers, timeout=TIMEOUT)
    assert response.status_code == 201, f"Expected 201 Created, got {response.status_code}"
    created_product = response.json()
    assert "id" in created_product, "Created product id not found in response"
    return created_product["id"]


def delete_product(product_id, headers):
    url = f"{BASE_URL}{PRODUCTS_ENDPOINT}/{product_id}"
    try:
        response = requests.delete(url, headers=headers, timeout=TIMEOUT)
        if response.status_code not in [200, 204]:
            response.raise_for_status()
    except requests.HTTPError as e:
        print(f"Failed to delete product {product_id}: {str(e)}")


def test_get_product_by_id_valid_and_invalid():
    token = authenticate()
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }

    product_id = None
    try:
        product_id = create_product(headers)

        url_valid = f"{BASE_URL}{PRODUCTS_ENDPOINT}/{product_id}"
        resp_valid = requests.get(url_valid, headers=headers, timeout=TIMEOUT)
        assert resp_valid.status_code == 200, f"Expected 200 OK for valid product ID, got {resp_valid.status_code}"
        product_data = resp_valid.json()
        assert product_data.get("id") == product_id, "Returned product ID mismatch"
        assert product_data.get("companyId") == COMPANY_ID, "Returned product companyId mismatch"
        assert "name" in product_data, "Product name not in response"
        assert "code" in product_data, "Product code not in response"

        invalid_id = str(uuid.uuid4())
        url_invalid = f"{BASE_URL}{PRODUCTS_ENDPOINT}/{invalid_id}"
        resp_invalid = requests.get(url_invalid, headers=headers, timeout=TIMEOUT)
        assert resp_invalid.status_code in [400, 404], \
            f"Expected 400 or 404 for invalid product ID, got {resp_invalid.status_code}"

    finally:
        if product_id:
            delete_product(product_id, headers)


test_get_product_by_id_valid_and_invalid()
