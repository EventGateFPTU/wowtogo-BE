# WOWToGo WebAPI

Welcome to WOWToGo WebAPI! This project serves as the backend for WOWToGo, a web application designed to provide users with information and services related to World of Warcraft.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Setup](#setup)
- [Usage](#usage)
- [Contributing](#contributing)
- [Convention](#Convention)
- [License](#license)

## Introduction

WOWToGo WebAPI is built on .NET 8 and provides a RESTful API for accessing and managing data related to World of Warcraft. Whether you're fetching character information, querying item data, or managing guilds, WOWToGo WebAPI has you covered.

## Features

- **Character Information**: Retrieve detailed information about characters, including stats, equipment, achievements, and more.
- **Item Database**: Access a comprehensive database of items available in World of Warcraft.
- **Guild Management**: Query and manage guild information, including membership, ranks, and activities.
- **Authentication and Authorization**: Secure endpoints using JWT tokens for authentication and authorization.

## Convention

- Github commit conventions : https://gist.github.com/qoomon/5dfcdf8eec66a051ecd85625518cfd13#types
- .NET conventions : https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
- Endpoints HTTP naming conventions : https://restfulapi.net/resource-naming/

## Setup

To run WOWToGo WebAPI locally, follow these steps:

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/wowtogo/wowtogo-BE.git
   ```
2. **Change the Connection Strings in**
   `appsettings.json`
3. **Direct the terminal to API Project**
   `cd ./src/API`
4. **Run the project**
   ```
   dotnet run : run the project
   dotnet watch : run the project with hot reload
   ```
