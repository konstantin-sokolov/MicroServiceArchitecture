FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8001
EXPOSE 8002
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HealthChecker/HealthChecker.csproj", "HealthChecker/"]
RUN dotnet restore "HealthChecker/HealthChecker.csproj"
COPY . .
WORKDIR "/src/HealthChecker"
RUN dotnet build "HealthChecker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HealthChecker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HealthChecker.dll"]