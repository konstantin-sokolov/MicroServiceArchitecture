FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8000

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
ENV ASPNETCORE_URLS=http://+:8000
ENTRYPOINT ["dotnet", "HealthChecker.dll"]