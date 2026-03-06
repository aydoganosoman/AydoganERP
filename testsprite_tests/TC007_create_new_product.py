import requests
import uuid

BASE_URL = "http://localhost:5010"
LOGIN_URL = f"{BASE_URL}/api/Auth/login"
PRODUCTS_URL = f"{BASE_URL}/api/Products"
AUTH_PAYLOAD = {
    "email": "stronger_osman@hotmail.com",
    "password": "ezEaE_76"
}
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30


def authenticate():
    response = requests.post(LOGIN_URL, json=AUTH_PAYLOAD, timeout=TIMEOUT)
    response.raise_for_status()
    token = response.json().get("token")
    if not token:
        raise Exception("Authentication failed: No token returned")
    return token


def test_create_new_product():
    token = authenticate()
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }

    product_code = f"TESTCODE-{uuid.uuid4().hex[:8]}"
    product_name = f"Test Product {uuid.uuid4().hex[:6]}"
    # Use a valid 13-digit barcode for EAN13
    barcode = "1234567890123"

    product_payload = {
        "companyId": COMPANY_ID,
        "code": product_code,
        "name": product_name,
        "barcodeType": "EAN13",
        "barcode": barcode,
        "description": "Test product description",
        "price": 10.5,
        "isActive": True
    }

    product_id = None
    try:
        # Create product
        create_resp = requests.post(PRODUCTS_URL, json=product_payload, headers=headers, timeout=TIMEOUT)
        assert create_resp.status_code == 201 or create_resp.status_code == 200, f"Unexpected status code {create_resp.status_code} on create"
        created_product = create_resp.json()
        product_id = created_product.get("id")
        assert product_id is not None, "Created product has no ID"
        assert created_product.get("companyId") == COMPANY_ID
        assert created_product.get("code") == product_code
        assert created_product.get("name") == product_name
        assert created_product.get("barcodeType") == product_payload["barcodeType"]
        assert created_product.get("barcode") == product_payload["barcode"]
        assert created_product.get("description") == product_payload["description"]
        assert isinstance(created_product.get("price"), (int, float))
        assert created_product.get("isActive") is True

    finally:
        # Cleanup: delete created product if exists
        if product_id:
            delete_resp = requests.delete(f"{PRODUCTS_URL}/{product_id}", headers=headers, timeout=TIMEOUT)
            assert delete_resp.status_code == 200 or delete_resp.status_code == 204, f"Failed to delete product {product_id}, status: {delete_resp.status_code}"


test_create_new_product()