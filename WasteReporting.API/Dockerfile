# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WasteReporting.API/WasteReporting.API.csproj", "WasteReporting.API/"]
RUN dotnet restore "WasteReporting.API/WasteReporting.API.csproj"
COPY . .
WORKDIR "/src/WasteReporting.API"
RUN dotnet build "WasteReporting.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WasteReporting.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WasteReporting.API.dll"]
