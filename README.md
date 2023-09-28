# Market API

The Market API is a RESTful web service that allows users to manage orders and products in a market. It provides endpoints for creating, updating, and deleting orders, as well as retrieving order information and product details.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Endpoints](#endpoints)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- Create, update, and delete orders.
- Retrieve a list of orders.
- Calculate and display the remaining quantity and price of products after ordering.

## Getting Started

To get started with the Market API, follow these steps:

1. **Clone the Repository**

  git clone https://github.com/yourusername/market-api.git

2. **Install Dependencies**

Navigate to the project directory and install the required dependencies:

  cd market-api
  dotnet restore

3. **Set Up the Database**

Configure your database connection in the `appsettings.json` file. Then, apply migrations to create the database schema:

  dotnet ef database update

4. **Run the Application**

Start the application:

  dotnet run

The API will be available at `http://localhost:5000` by default.

## Endpoints

The Market API provides the following endpoints:

- `GET /api/order`: Retrieve a list of orders.
- `POST /api/order/{Order_number}?User_ID={User_ID}&Product_ID={Product_ID}`: Create a new order.
- `PUT /api/order/{Order_number}`: Update an existing order.
- `DELETE /api/order/{Order_number}`: Delete an order.

## Usage

### Creating an Order

To create an order, send a POST request to the `/api/order/{Order_number}` endpoint with the following query parameters:

- `User_ID`: The ID of the user placing the order.
- `Product_ID`: The ID of the product being ordered.

Include a JSON body with the order details, including `Quantity`, `Price_Amount`, and `Price_Currency`. The API will calculate and display the remaining quantity and price of the product after ordering.

Example POST request:

```http
POST /api/order/123?User_ID=1&Product_ID=456
Content-Type: application/json

{
"Quantity": 5,
"Price_Amount": 10.5,
"Price_Currency": "USD"
}



Updating an Order
To update an existing order, send a PUT request to the /api/order/{Order_number} endpoint with the order details to be modified.

Example PUT request:
  PUT /api/order/123
Content-Type: application/json

{
   "Order_number": 123,
   "Quantity": 3,
   "Price_Amount": 7.0,
   "Price_Currency": "USD"
}
Deleting an Order
To delete an order, send a DELETE request to the /api/order/{Order_number} endpoint with the order number to be deleted.

Example DELETE request:
  DELETE /api/order/123
Contributing
Contributions to the Market API are welcome! If you have suggestions, enhancements, or bug reports, please open an issue or create a pull request.

License
This project is licensed under the MIT License - see the LICENSE file for details.

  Feel free to use this Markdown-formatted README as a template for your project and adjust it as needed.

