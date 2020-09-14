FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SimpleApiAcceptedTests/SimpleApiAcceptedTests.csproj", "SimpleApiAcceptedTests/"]
RUN dotnet restore "SimpleApiAcceptedTests/SimpleApiAcceptedTests.csproj"
COPY . .
WORKDIR "/src/SimpleApiAcceptedTests"

FROM build AS publish
RUN dotnet publish "SimpleApiAcceptedTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleApiAcceptedTests.dll"]