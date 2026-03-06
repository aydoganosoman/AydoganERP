import requests

BASE_URL = "http://localhost:5010"
LOGIN_ENDPOINT = "/api/Auth/Login"
PRODUCTS_ENDPOINT = "/api/Products"
AUTH_PAYLOAD = {
    "email": "stronger_osman@hotmail.com",
    "password": "ezEaE_76"
}
TIMEOUT = 30
COMPANY_ID = "839bf8d5-d8a0-4bc6-a949-71b2fb582aef"

def test_get_product_list_pagination_and_filtering():
    # Authenticate and get JWT token
    login_url = BASE_URL + LOGIN_ENDPOINT
    try:
        login_resp = requests.post(login_url, json=AUTH_PAYLOAD, timeout=TIMEOUT)
        login_resp.raise_for_status()
    except Exception as e:
        assert False, f"Authentication failed: {str(e)}"
    login_data = login_resp.json()
    assert "token" in login_data and "token" in login_data["token"], "Token missing in login response"
    token = login_data["token"]["token"]
    headers = {
        "Authorization": f"Bearer {token}"
    }

    # Define query parameters for filtering and pagination
    query_params = {
        "companyId": COMPANY_ID,
        "categoryId": "category-example-1234",
        "searchText": "testProduct",
        "isActive": "true",
        "pageNumber": "1",
        "pageSize": "10"
    }

    # Send GET request to /api/Products with query params and auth header
    products_url = BASE_URL + PRODUCTS_ENDPOINT
    try:
        resp = requests.get(products_url, headers=headers, params=query_params, timeout=TIMEOUT)
        resp.raise_for_status()
    except Exception as e:
        assert False, f"GET /api/Products request failed: {str(e)}"

    data = resp.json()

    # Validate response structure and filtering logic
    # Expecting list structure or paginated structure - assumed paginated with items list
    assert isinstance(data, dict), "Response is not a JSON object"
    assert "items" in data, "Response missing 'items' key for product list"
    assert isinstance(data["items"], list), "'items' is not a list"

    # Check pagination keys if exist
    assert "pageNumber" in data and data["pageNumber"] == int(query_params["pageNumber"]), "Incorrect pageNumber in response"
    assert "pageSize" in data and data["pageSize"] == int(query_params["pageSize"]), "Incorrect pageSize in response"
    # Validate each product item has companyId and isActive if present
    for product in data["items"]:
        assert "companyId" in product, "Product item missing companyId"
        assert product["companyId"] == COMPANY_ID, f"Product companyId mismatch: expected {COMPANY_ID}, got {product['companyId']}"
        # isActive filtering check
        if "isActive" in product:
            assert str(product["isActive"]).lower() == query_params["isActive"], f"Product isActive mismatch filter"
        # categoryId filtering if categoryId is available in product
        if "categoryId" in product:
            assert str(product["categoryId"]) == query_params["categoryId"], "Product categoryId mismatch filter"
        # searchText check in some product field like 'name' or 'code' (best effort)
        name = product.get("name", "") or product.get("code", "")
        assert query_params["searchText"].lower() in str(name).lower(), "searchText not found in product fields"

test_get_product_list_pagination_and_filtering()