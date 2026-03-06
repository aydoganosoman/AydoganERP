import requests
import uuid

BASE_URL = "http://localhost:5010"
AUTH_URL = f"{BASE_URL}/api/Auth/login"
PRODUCTS_URL = f"{BASE_URL}/api/Products"
LOGIN_PAYLOAD = {"email": "stronger_osman@hotmail.com", "password": "ezEaE_76"}
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
CATEGORY_ID = "b17e4120-1b5c-4a6a-b3a4-52a967aeb94f"  # Added dummy categoryId
TIMEOUT = 30

def authenticate():
    resp = requests.post(AUTH_URL, json=LOGIN_PAYLOAD, timeout=TIMEOUT)
    resp.raise_for_status()
    token = resp.json().get("token", {}).get("token")
    assert token, "Authentication token not found in response"
    return token

def create_product(headers, barcode):
    product_payload = {
        "code": str(uuid.uuid4())[:8],
        "name": "Test Product",
        "description": "Test Product Description",
        "companyId": COMPANY_ID,
        "categoryId": CATEGORY_ID,  # Added categoryId
        "barcode": barcode,
        "isActive": True
    }
    resp = requests.post(PRODUCTS_URL, json=product_payload, headers=headers, timeout=TIMEOUT)
    resp.raise_for_status()
    product = resp.json()
    assert "id" in product, "Created product does not have an ID"
    return product

def delete_product(headers, product_id):
    url = f"{PRODUCTS_URL}/{product_id}"
    resp = requests.delete(url, headers=headers, timeout=TIMEOUT)
    # Allow 404 if already deleted, otherwise raise for status
    if resp.status_code not in (204, 404):
        resp.raise_for_status()

def test_get_product_by_barcode_search():
    token = authenticate()
    headers = {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}

    # Create a product with a unique barcode
    barcode_test = str(uuid.uuid4())[:13]  # 13 chars suitable for EAN13 string

    product = None
    try:
        product = create_product(headers, barcode_test)
        product_id = product["id"]

        # Success case: search by barcode and companyId
        params = {"barcode": barcode_test, "companyId": COMPANY_ID}
        resp = requests.get(f"{PRODUCTS_URL}/by-barcode", headers=headers, params=params, timeout=TIMEOUT)
        resp.raise_for_status()
        found_product = resp.json()
        assert found_product["id"] == product_id
        assert found_product["barcode"] == barcode_test
        assert found_product["companyId"] == COMPANY_ID

        # Failure case: search for non-existent barcode
        fake_barcode = "NONEXISTENTBARCODE1234"
        params = {"barcode": fake_barcode, "companyId": COMPANY_ID}
        resp = requests.get(f"{PRODUCTS_URL}/by-barcode", headers=headers, params=params, timeout=TIMEOUT)
        # Assuming API returns 404 or empty result (check both cases)
        if resp.status_code == 404:
            # Expected no content found
            pass
        else:
            resp.raise_for_status()
            json_resp = resp.json()
            # If response is empty or null object for no product found, validate accordingly
            # Accept if empty dict or null
            assert not json_resp or (isinstance(json_resp, dict) and not json_resp), "Expected no product found for nonexistent barcode"
    finally:
        if product:
            delete_product(headers, product["id"])

test_get_product_by_barcode_search()