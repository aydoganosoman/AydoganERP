
# TestSprite AI Testing Report(MCP)

---

## 1️⃣ Document Metadata
- **Project Name:** AydoganERP
- **Date:** 2026-03-05
- **Prepared by:** TestSprite AI Team

---

## 2️⃣ Requirement Validation Summary

#### Test TC001 get_product_list_pagination_and_filtering
- **Test Code:** [TC001_get_product_list_pagination_and_filtering.py](./TC001_get_product_list_pagination_and_filtering.py)
- **Test Error:** Traceback (most recent call last):
  File "<string>", line 42, in test_get_product_list_pagination_and_filtering
  File "/var/task/requests/models.py", line 1024, in raise_for_status
    raise HTTPError(http_error_msg, response=self)
requests.exceptions.HTTPError: 500 Server Error: Internal Server Error for url: http://localhost:5010/api/Products?companyId=839bf8d5-d8a0-4bc6-a949-71b2fb582aef&categoryId=category-example-1234&searchText=testProduct&isActive=true&pageNumber=1&pageSize=10

During handling of the above exception, another exception occurred:

Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 71, in <module>
  File "<string>", line 44, in test_get_product_list_pagination_and_filtering
AssertionError: GET /api/Products request failed: 500 Server Error: Internal Server Error for url: http://localhost:5010/api/Products?companyId=839bf8d5-d8a0-4bc6-a949-71b2fb582aef&categoryId=category-example-1234&searchText=testProduct&isActive=true&pageNumber=1&pageSize=10

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/d324b6fb-1796-4da3-87c7-64e9a72c4507
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC002 get_product_by_id_valid_and_invalid
- **Test Code:** [TC002_get_product_by_id_valid_and_invalid.py](./TC002_get_product_by_id_valid_and_invalid.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 85, in <module>
  File "<string>", line 63, in test_get_product_by_id_valid_and_invalid
  File "<string>", line 38, in create_product
AssertionError: Expected 201 Created, got 401

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/c4470371-c56c-4fd9-8aa3-75d7c3f2363c
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC003 get_product_by_barcode_search
- **Test Code:** [TC003_get_product_by_barcode_search.py](./TC003_get_product_by_barcode_search.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 81, in <module>
  File "<string>", line 51, in test_get_product_by_barcode_search
  File "<string>", line 30, in create_product
  File "/var/task/requests/models.py", line 1024, in raise_for_status
    raise HTTPError(http_error_msg, response=self)
requests.exceptions.HTTPError: 400 Client Error: Bad Request for url: http://localhost:5010/api/Products

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/317cab2a-8eb4-4a74-9f63-feca71e3d95d
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC004 check_product_code_existence
- **Test Code:** [TC004_check_product_code_existence.py](./TC004_check_product_code_existence.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 87, in <module>
  File "<string>", line 63, in test_check_product_code_existence
  File "<string>", line 31, in create_product
  File "/var/task/requests/models.py", line 1024, in raise_for_status
    raise HTTPError(http_error_msg, response=self)
requests.exceptions.HTTPError: 401 Client Error: Unauthorized for url: http://localhost:5010/api/Products

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/72e88fa9-f0c2-40ae-82b2-bca85d0c12fe
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC005 get_next_product_code_generation
- **Test Code:** [TC005_get_next_product_code_generation.py](./TC005_get_next_product_code_generation.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/755bb4d7-7aea-4a69-a7f0-78fae1961be8
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC006 generate_unique_barcode
- **Test Code:** [TC006_generate_unique_barcode.py](./TC006_generate_unique_barcode.py)
- **Test Error:** Traceback (most recent call last):
  File "<string>", line 42, in test_generate_unique_barcode
  File "/var/task/requests/models.py", line 1024, in raise_for_status
    raise HTTPError(http_error_msg, response=self)
requests.exceptions.HTTPError: 401 Client Error: Unauthorized for url: http://localhost:5010/api/Products/generate-barcode?barcodeType=0&prefix=TEST0

During handling of the above exception, another exception occurred:

Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 62, in <module>
  File "<string>", line 44, in test_generate_unique_barcode
AssertionError: Failed to generate barcode for barcodeType '0' with prefix 'TEST0': 401 Client Error: Unauthorized for url: http://localhost:5010/api/Products/generate-barcode?barcodeType=0&prefix=TEST0

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/bfc5a8fb-6b11-4480-9e2f-f4a2f8ef664c
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC007 create_new_product
- **Test Code:** [TC007_create_new_product.py](./TC007_create_new_product.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 71, in <module>
  File "<string>", line 51, in test_create_new_product
AssertionError: Unexpected status code 401 on create

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/0567521b-7572-4355-a5e0-9c078f69e6e8
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC008 update_existing_product
- **Test Code:** [TC008_update_existing_product.py](./TC008_update_existing_product.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 98, in <module>
  File "<string>", line 50, in test_update_existing_product
AssertionError: Product creation failed: {"type":"https://tools.ietf.org/html/rfc7231#section-6.5.1","title":"One or more validation errors occurred.","status":400,"detail":"{\"UnitId\":[\"UnitId is required.\"]}"}

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/9b51dff0-77ba-4848-b5ce-f8ae40fa2826
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC009 get_product_stock_level
- **Test Code:** [TC009_get_product_stock_level.py](./TC009_get_product_stock_level.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 63, in <module>
  File "<string>", line 39, in test_get_product_stock_level
  File "/var/task/requests/models.py", line 1024, in raise_for_status
    raise HTTPError(http_error_msg, response=self)
requests.exceptions.HTTPError: 400 Client Error: Bad Request for url: http://localhost:5010/api/Products

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/5fc09f29-60f6-4bc7-8db4-ea089be95c4a
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC010 get_product_stock_movements
- **Test Code:** [TC010_get_product_stock_movements.py](./TC010_get_product_stock_movements.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 63, in <module>
  File "<string>", line 45, in test_get_product_stock_movements
  File "<string>", line 28, in create_product
  File "/var/task/requests/models.py", line 1024, in raise_for_status
    raise HTTPError(http_error_msg, response=self)
requests.exceptions.HTTPError: 401 Client Error: Unauthorized for url: http://localhost:5010/api/Products

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/1d7c158e-cc7a-44b1-b5f4-c3c5e15b6101/4ead9016-ba0e-4742-9469-47aec2ed1898
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---


## 3️⃣ Coverage & Matching Metrics

- **10.00** of tests passed

| Requirement        | Total Tests | ✅ Passed | ❌ Failed  |
|--------------------|-------------|-----------|------------|
| ...                | ...         | ...       | ...        |
---


## 4️⃣ Key Gaps / Risks
{AI_GNERATED_KET_GAPS_AND_RISKS}
---