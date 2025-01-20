[Back to README](../README.md)

### Sales

#### GET /sales
- Description: Retrieve a list of all sales
- Response: 
  ```json
  {
    "data": [
      {
        "id": "string",
        "saleNumber": "string",
        "saleDate": "string",
        "customer": "string",
        "branch": "string",
        "totalAmount": "number",
        "isCancelled": "boolean",
        "items": {
          "id": "string",
          "product": "string",
          "quantity": "integer",
          "unitPrice": "number",
          "discount": "number",
          "totalAmount": "number",
          "isCancelled": "boolean"
        }
      }
    ]
  }
  ```

#### POST /sales
- Description: Add a new sale
- Request Body:
  ```json
  {
    "saleNumber": "string",
    "saleDate": "string",
    "customer": "string",
    "branch": "string",
    "totalAmount": "number",
    "isCancelled": "boolean",
    "items": {
      "product": "string",
      "quantity": "integer",
      "unitPrice": "number",
    }
  }
  ```
- Response: 
  ```json
  {
    "data": [
      {
        "id": "string",
        "saleNumber": "string",
        "saleDate": "string",
        "customer": "string",
        "branch": "string",
        "totalAmount": "number",
        "isCancelled": "boolean",
        "items": {
          "id": "string",
          "product": "string",
          "quantity": "integer",
          "unitPrice": "number",
          "discount": "number",
          "totalAmount": "number",
          "isCancelled": "boolean",
          "saleId": "string"
        }
      }
    ]
  }
  ```

#### GET /sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "data": [
      {
        "id": "string",
        "saleNumber": "string",
        "saleDate": "string",
        "customer": "string",
        "branch": "string",
        "totalAmount": "number",
        "isCancelled": "boolean",
        "items": {
          "id": "string",
          "product": "string",
          "quantity": "integer",
          "unitPrice": "number",
          "discount": "number",
          "totalAmount": "number",
          "isCancelled": "boolean",
          "saleId": "string"
        }
      }
    ]
  }
  ```

#### PUT /sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID
- Request Body:
  ```json
  {
    "id": "string",
    "saleNumber": "string",
    "customer": "string",
    "branch": "string",
    "isCancelled": "boolean",
    "items": {
      "product": "string",
      "quantity": "integer",
      "unitPrice": "number",
    }
  }
  ```
- Response: 
  ```json
  {
    "data": [
      {
        "id": "string",
        "saleNumber": "string",
        "saleDate": "string",
        "customer": "string",
        "branch": "string",
        "totalAmount": "number",
        "isCancelled": "boolean",
        "items": {
          "id": "string",
          "product": "string",
          "quantity": "integer",
          "unitPrice": "number",
          "discount": "number",
          "totalAmount": "number",
          "isCancelled": "boolean",
          "saleId": "string"
        }
      }
    ]
  }
  ```

#### DELETE /sales/{id}
- Description: Delete a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

#### PATCH /sales/cancel/{id}
- Description: Cancel a sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

#### PATCH /sales/cancel-item/{saleId}/{itemId}
- Description: Cancel a specific item from a sale
- Path Parameters:
  - `saleId`: Sale ID
  - `itemId`: Item ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./project-structure.md">Previous: Project Structure</a>
  <a href="./teck-stack.md">Next: Tech Stack</a>
</div>