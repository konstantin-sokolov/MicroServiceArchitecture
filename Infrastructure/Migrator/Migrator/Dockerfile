
FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Migrator/Migrator.csproj", "Migrator/"]
RUN dotnet restore "Migrator/Migrator.csproj"
COPY . .
WORKDIR "/src/Migrator"
RUN dotnet build "Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Migrator.dll"]