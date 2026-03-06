import requests

BASE_URL = "http://localhost:5010"
LOGIN_ENDPOINT = "/api/Auth/login"
PRODUCTS_ENDPOINT = "/api/Products"
USERNAME = "info@testfirma.com"
PASSWORD = "Q7xdG@4t"
TIMEOUT = 30

def authenticate():
    url = BASE_URL + LOGIN_ENDPOINT
    payload = {"username": USERNAME, "password": PASSWORD}
    response = requests.post(url, json=payload, timeout=TIMEOUT)
    response.raise_for_status()
    token = response.json().get("token")
    assert token, "Authentication failed: token not found in response"
    return token

def create_product(headers):
    url = BASE_URL + PRODUCTS_ENDPOINT
    product_data = {
        "name": "Test Product TC002",
        "code": "TC002CODE",
        "barcode": "1234567890123",
        "description": "Product created for test case TC002",
        "companyId": 1,
        "isActive": True,
        "price": 10.0
    }
    response = requests.post(url, json=product_data, headers=headers, timeout=TIMEOUT)
    response.raise_for_status()
    product = response.json()
    product_id = product.get("id")
    assert product_id is not None, "Failed to create product: id not returned"
    return product_id, product_data, product

def test_get_product_by_id():
    token = authenticate()
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }
    product_id = None
    created_product_data = None
    try:
        # Create a product to ensure a valid product ID
        product_id, created_product_data, created_product_response = create_product(headers)
        # Get product by ID
        url = f"{BASE_URL}{PRODUCTS_ENDPOINT}/{product_id}"
        response = requests.get(url, headers=headers, timeout=TIMEOUT)
        response.raise_for_status()
        product = response.json()
        # Validate the product details match what we created
        assert product.get("id") == product_id
        assert product.get("name") == created_product_data["name"]
        assert product.get("code") == created_product_data["code"]
        assert product.get("barcode") == created_product_data["barcode"]
        assert product.get("description") == created_product_data["description"]
        assert product.get("companyId") == created_product_data["companyId"]
        assert product.get("isActive") == created_product_data["isActive"]
        assert abs(float(product.get("price", 0)) - created_product_data["price"]) < 0.001
    finally:
        pass


test_get_product_by_id()