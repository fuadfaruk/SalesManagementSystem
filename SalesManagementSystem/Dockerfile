# Use the official .NET SDK image as the base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project files to the container
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out


# Use the official .NET runtime image as the base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SalesManagementSystem.dll"]
