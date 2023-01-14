#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7083

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Spg.AutoTeileShop.API/Spg.AutoTeileShop.API.csproj", "src/Spg.AutoTeileShop.API/"]
COPY ["src/Spg.AutoTeileShop.Application/Spg.AutoTeileShop.Application.csproj", "src/Spg.AutoTeileShop.Application/"]
COPY ["src/Spg.AutoTeileShop.Domain/Spg.AutoTeileShop.Domain.csproj", "src/Spg.AutoTeileShop.Domain/"]
COPY ["src/Spg.AutoTeileShop.Infrastructure/Spg.AutoTeileShop.Infrastructure.csproj", "src/Spg.AutoTeileShop.Infrastructure/"]
COPY ["src/Spg.AutoTeileShop.Repository2/Spg.AutoTeileShop.Repository2.csproj", "src/Spg.AutoTeileShop.Repository2/"]
COPY ["src/Spg.AutoTeileShop.DbExtentions/Spg.AutoTeileShop.DbExtentions.csproj", "src/Spg.AutoTeileShop.DbExtentions/"]
COPY ["src/Spg.AutoTeileShop.ServiceExtentions/Spg.AutoTeileShop.ServiceExtentions.csproj", "src/Spg.AutoTeileShop.ServiceExtentions/"]
RUN dotnet restore "src/Spg.AutoTeileShop.API/Spg.AutoTeileShop.API.csproj"
COPY . .
WORKDIR "/src/src/Spg.AutoTeileShop.API"
RUN dotnet build "Spg.AutoTeileShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spg.AutoTeileShop.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spg.AutoTeileShop.API.dll"]