import requests
import uuid

BASE_URL = "http://localhost:5010"
AUTH_URL = f"{BASE_URL}/api/Auth/login"
PRODUCTS_URL = f"{BASE_URL}/api/Products"
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"
TIMEOUT = 30

def authenticate():
    payload = {"email": "stronger_osman@hotmail.com", "password": "ezEaE_76"}
    resp = requests.post(AUTH_URL, json=payload, timeout=TIMEOUT)
    resp.raise_for_status()
    token = resp.json().get("token")
    if not token:
        raise Exception("Authentication failed, no token found")
    return token

def create_product(headers):
    unique_code = str(uuid.uuid4())[:8]
    payload = {
        "companyId": COMPANY_ID,
        "code": unique_code,
        "name": f"Test Product {unique_code}",
        "isActive": True
    }
    resp = requests.post(PRODUCTS_URL, json=payload, headers=headers, timeout=TIMEOUT)
    resp.raise_for_status()
    product = resp.json()
    product_id = product.get("id")
    if not product_id:
        raise Exception("Product creation failed, no id returned")
    return product_id

# Removed delete_product function since DELETE endpoint for products is not defined in PRD

def test_get_product_stock_movements():
    token = authenticate()
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }
    product_id = None
    try:
        product_id = create_product(headers)

        url = f"{PRODUCTS_URL}/{product_id}/movements"
        resp = requests.get(url, headers=headers, timeout=TIMEOUT)
        assert resp.status_code == 200, f"Expected status 200, got {resp.status_code}"

        data = resp.json()
        assert isinstance(data, (list, dict)), "Response data should be list or dict"
        if isinstance(data, list):
            for mov in data:
                assert 'productId' in mov, "Each movement should have 'productId'"
                assert mov['productId'] == product_id, "Movement productId should match requested productId"
        else:
            pass
    finally:
        # No deletion since endpoint not defined
        pass

test_get_product_stock_movements()
