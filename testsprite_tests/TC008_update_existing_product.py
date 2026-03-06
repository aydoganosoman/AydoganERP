import requests

BASE_URL = "http://localhost:5010"
LOGIN_ENDPOINT = "/api/Auth/login"
PRODUCTS_ENDPOINT = "/api/Products"
AUTH_PAYLOAD = {
    "email": "stronger_osman@hotmail.com",
    "password": "ezEaE_76"
}
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30

def test_update_existing_product():
    # Authenticate and get JWT token
    auth_response = requests.post(
        BASE_URL + LOGIN_ENDPOINT,
        json=AUTH_PAYLOAD,
        timeout=TIMEOUT
    )
    assert auth_response.status_code == 200, f"Authentication failed: {auth_response.text}"
    auth_data = auth_response.json()
    token = auth_data.get("token", {}).get("token")
    assert token, "JWT token not found in authentication response"

    headers = {
        "Authorization": f"Bearer {token}"
    }

    # Create a product to update
    product_create_payload = {
        "companyId": COMPANY_ID,
        "code": "UPD-001",
        "name": "Test Product for Update",
        "categoryId": None,
        "unitId": None,
        "barcode": "1234567890123",
        "barcodeType": "EAN13",
        "isActive": True,
        "description": "Initial description"
    }

    created_product = None
    try:
        create_response = requests.post(
            BASE_URL + PRODUCTS_ENDPOINT,
            json=product_create_payload,
            headers=headers,
            timeout=TIMEOUT
        )
        assert create_response.status_code == 201, f"Product creation failed: {create_response.text}"
        created_product = create_response.json()
        product_id = created_product.get("id")
        assert product_id, "Created product does not have an 'id'"

        # Prepare update payload with valid data
        update_payload = {
            "companyId": COMPANY_ID,
            "code": "UPD-001-UPDATED",
            "name": "Updated Test Product",
            "categoryId": None,
            "unitId": None,
            "barcode": "9876543210987",
            "barcodeType": "1",
            "isActive": False,
            "description": "Updated description"
        }

        # Update existing product using PUT
        update_response = requests.put(
            f"{BASE_URL}{PRODUCTS_ENDPOINT}/{product_id}",
            json=update_payload,
            headers=headers,
            timeout=TIMEOUT
        )
        assert update_response.status_code == 200, f"Product update failed: {update_response.text}"
        updated_product = update_response.json()

        # Validate response details
        assert updated_product.get("id") == product_id, "Updated product ID mismatch"
        assert updated_product.get("code") == update_payload["code"], "Product code not updated correctly"
        assert updated_product.get("name") == update_payload["name"], "Product name not updated correctly"
        assert updated_product.get("barcode") == update_payload["barcode"], "Product barcode not updated correctly"
        # barcodeType can be int or str, accept either form in response
        assert updated_product.get("barcodeType") in [update_payload["barcodeType"], str(update_payload["barcodeType"])], "Product barcodeType not updated correctly"
        assert updated_product.get("isActive") == update_payload["isActive"], "Product isActive not updated correctly"
        assert updated_product.get("description") == update_payload["description"], "Product description not updated correctly"
        assert updated_product.get("companyId") == COMPANY_ID, "Product companyId changed unexpectedly"

    finally:
        # Cleanup - delete created product
        if created_product and "id" in created_product:
            requests.delete(
                f"{BASE_URL}{PRODUCTS_ENDPOINT}/{created_product['id']}",
                headers=headers,
                timeout=TIMEOUT
            )

test_update_existing_product()
