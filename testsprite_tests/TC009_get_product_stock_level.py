import requests

BASE_URL = "http://localhost:5010"
LOGIN_URL = f"{BASE_URL}/api/Auth/login"
PRODUCTS_URL = f"{BASE_URL}/api/Products"
AUTH_PAYLOAD = {"email": "stronger_osman@hotmail.com", "password": "ezEaE_76"}
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30

def test_get_product_stock_level():
    # Authenticate and get JWT token
    try:
        auth_response = requests.post(LOGIN_URL, json=AUTH_PAYLOAD, timeout=TIMEOUT)
        auth_response.raise_for_status()
    except Exception as e:
        assert False, f"Authentication request failed: {e}"

    token = auth_response.json().get("token", {}).get("token")
    assert token, "Authentication token not found in response"

    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }

    # Create a new product to get a valid product ID
    product_payload = {
        "companyId": COMPANY_ID,
        "name": "Test Product Stock Level",
        "code": "TPSTL001",
        "categoryId": "00000000-0000-0000-0000-000000000000",
        "barcode": "1234567890123",
        "isActive": True
    }

    product_id = None
    try:
        create_response = requests.post(PRODUCTS_URL, json=product_payload, headers=headers, timeout=TIMEOUT)
        create_response.raise_for_status()
        created_product = create_response.json()
        product_id = created_product.get("id")
        assert product_id, "Created product ID not found"

        # Get stock level for the created product
        stock_level_url = f"{PRODUCTS_URL}/{product_id}/stock-level"
        stock_response = requests.get(stock_level_url, headers=headers, timeout=TIMEOUT)
        stock_response.raise_for_status()

        stock_data = stock_response.json()
        # Assert that the response contains a numeric stock level
        assert isinstance(stock_data, (int, float)), \
            "Stock level information not found or invalid in response"

    finally:
        # Clean up: delete the created product if it was created
        if product_id:
            try:
                delete_url = f"{PRODUCTS_URL}/{product_id}"
                requests.delete(delete_url, headers=headers, timeout=TIMEOUT)
            except Exception:
                pass

test_get_product_stock_level()
