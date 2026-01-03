# CAPTCHA API – ASP.NET Core

A simple **CAPTCHA generation and validation API** built with **ASP.NET Core Web API**.  
The API generates a CAPTCHA image, stores the CAPTCHA value in the server session, and validates user input against it.

-----------------------------

## Features

- Generate random CAPTCHA text
- Render CAPTCHA as a JPEG image
- Store CAPTCHA securely using server-side session
- Validate user input against stored CAPTCHA
- Case-insensitive validation

-----------------------------

## Technologies Used

- Session State
- In-Memory Distributed Cache
- `System.Drawing`

-----------------------------

## Configuration

### Enable Session and Distributed Cache
Add the following configuration in `Program.cs`:

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

---------------------------------------------------------------------
Make sure to register the session middleware before mapping controllers:
app.UseSession();

--------------------------
 ### API Endpoints
--------------------------

- Generate CAPTCHA

Endpoint :- GET /api/captcha/getCaptcha

--------------------------
### Description
--------------------------

1- Generates a random CAPTCHA string
2- Stores the CAPTCHA in session
3- Returns a CAPTCHA image as image/jpeg

--------------------------
### Response
--------------------------

Content-Type: image/jpeg
Validate CAPTCHA
Endpoint POST /api/captcha/validateCaptcha


--------------------------
### Success Response
--------------------------

{
  "success": true,
  "message": "CAPTCHA validation succeeded."
}

--------------------------
### Failure Response
--------------------------

{
  "success": false,
  "message": "CAPTCHA validation failed."
}

## How It Works

- A random CAPTCHA string is generated.
- The string is rendered into an image using System.Drawing.
- The CAPTCHA value is stored in the server session.
- The client submits the CAPTCHA value for validation.
- The API compares the input with the stored value (case-insensitive).


--------------------------
## Important Notes
--------------------------
1- This CAPTCHA implementation is stateful.
2- Suitable for:
  - Learning purposes
  - Small projects
  - Internal tools
3- Not recommended for large-scale or distributed systems without a shared session store (e.g., Redis).

--------------------------
## Possible Improvements
--------------------------

1. Add noise lines and text distortion
2. Implement CAPTCHA expiration
3. Use Redis instead of in-memory cache
4. Replace session-based CAPTCHA with a stateless token-based approach

--------------------
## License
--------------------
This project is intended for educational purposes and can be freely modified or extended.
