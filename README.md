# Tariff Comparison Exercise

This demo project consists of a .NET Core 2.2 web application which has an API to compare the costs of different tariffs.

## Endpoints

It offers a single endpoint to retrieve two tariffs:

```
GET {base-url}/api/v1/tariffs/by-annual-consumption/{annual-consumption-in-kWh}
```

Example:

```
GET https://localhost:44316/api/v1/tariffs/by-annual-consumption/4500
```

Given the annual consumption in kWh the application returns the tariffs for example like so:

```json
[
    {
        "name": "Packaged tariff",
        "annualCosts": 950.0
    },
    {
        "name": "basic electricity tariff",
        "annualCosts": 1050.0
    }
]
```

The tariffs are sorted by costs in ascending order.

## Development

The application is developed using Visual Studio 2019. Tests are provided using xUnit and Moq.
