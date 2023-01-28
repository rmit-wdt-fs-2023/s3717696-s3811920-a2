# Documentation

## Account

**GET: api/v1/account/customer/{id}**

- Retrieves an existing account with the passed account number.
- Path Parameter: id (integer) - The Account Number of the account to be retrieved.
- Response:
    - 200 OK and the account if the account is found.
    - 404 Not Found if the account does not exist.

**GET: api/v1/account/accounts**

- Retrieves all existing accounts.
- Response:
	- 200 OK and a list of all accounts if accounts are found.
	- 404 Not Found if the accounts are empty.

## Address

**POST: api/v1/[controller]**

- Creates a new address from the passed Address model.
- Request Body: Address object.
- Response:
	- 201 Created and the created address if the address is successfully created.
	- 400 Bad Request if the passed address is not valid.

**PUT: api/v1/[controller]/{id}**

- Updates an existing address with the passed Address model and address Id.
- Request Body: Address object.
- Path Parameter: id (integer) - The ID of the address to be updated.
- Response:
	- 201 Created and the updated address if the address is successfully updated.
	- 400 Bad Request if the passed address is not valid.

**GET: api/v1/[controller]/{id}**

- Retrieves an existing address with the passed address Id.
- Path Parameter: id (integer) - The ID of the address to be retrieved.
- Response:
	- 200 OK and the address if the address is found.
	- 404 Not Found if the address does not exist.

**GET: api/v1/[controller]**

- Retrieves all existing addresses.
- Response:
	- 200 OK and a list of all addresses if addresses are found.
	- 404 Not Found if the addresses are empty.

## BillPay

**GET: api/v1/billpay/{id}**
- Retrieves a single bill pay by ID.
- Path Parameters: id (int): The ID of the bill pay to retrieve.
- Response
	- 200 OK: Returns a single bill pay matching the provided ID.
	- 404 Not Found: If no bill pay is found with the provided ID.


**GET: api/v1/billpay**

- Retrieves all bill pays.
- Response
	- 200 OK: Returns a list of all bill pays.

**PUT: api/v1/billpay/{id}**
- Updates a single bill pay by ID.
- Path Parameters: id (int): The ID of the bill pay to update.
- Body: bill (BillPay): The updated bill pay information.
- Response
	- 201 Created: The bill pay was successfully updated and the updated bill pay information is returned.
	- 400 Bad Request: If the provided bill pay information is not valid.

## Customer

**POST: api/v1/customer/**

- Creates a customer 
- Body: Customer 
- Response:
	- 201 (Created) with location header of the created resource and the customer object in the body.
	- 400 (Bad Request) if the model is invalid.

**GET: api/v1/customer/{id}**

- Response:
	- 200 (OK) with the customer object in the body.
	- 404 (Not Found) if the customer is not found.

**GET: api/v1/customer**

- Response:
	- 200 (OK) with the list of customer objects in the body.
	- 404 (Not Found) if no customers are found.

**PUT: api/v1/customer/{id}**

- Body: Customer
- Response:
	- 200 (OK) with the updated customer object in the body.
	- 400 (Bad Request) if the model is invalid.