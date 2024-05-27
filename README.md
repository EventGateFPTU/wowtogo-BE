# WOWToGo WebAPI

Welcome to WOWToGo WebAPI! This project serves as the backend for WOWToGo, a web application designed to provide users with events registering features.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Setup](#setup)
- [Usage](#usage)
- [Contributing](#contributing)
- [Convention](#Convention)
- [License](#license)

## Introduction

EventGate WebAPI is built on .NET 8 and provides a RESTful API for registering tickets for events. 
Furthermore, users can also host events for audiences to join in.

## Features

- **Event Managing**: Managing events that are hosted in FU.
- **Event Registering**: Register events as guest or user.

## Conventions

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
   dotnet build : build the project
   dotnet restore : restore the solution
   ```
