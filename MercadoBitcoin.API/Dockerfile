#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MercadoBitcoin.API/MercadoBitcoin.API.csproj", "MercadoBitcoin.API/"]
RUN dotnet restore "MercadoBitcoin.API/MercadoBitcoin.API.csproj"
COPY . .
WORKDIR "/src/MercadoBitcoin.API"
RUN dotnet build "MercadoBitcoin.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MercadoBitcoin.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MercadoBitcoin.API.dll"]