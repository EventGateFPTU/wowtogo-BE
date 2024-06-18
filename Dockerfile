FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build

WORKDIR /app
COPY *.sln . 
COPY ./src/API/*.csproj ./src/API/
COPY ./src/UseCases/*.csproj ./src/UseCases/
COPY ./src/Domain/*.csproj ./src/Domain/
COPY ./src/Infrastructure/*.csproj ./src/Infrastructure/
RUN dotnet restore ./src/API/*.csproj

COPY . .
RUN dotnet build


# Publish stage
FROM build AS publish
WORKDIR /app/src/API
RUN dotnet publish -c Release --no-restore -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]