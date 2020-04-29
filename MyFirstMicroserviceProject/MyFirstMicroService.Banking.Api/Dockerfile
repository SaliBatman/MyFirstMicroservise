#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MyFirstMicroService.Banking.Api/MyFirstMicroService.Banking.Api.csproj", "MyFirstMicroService.Banking.Api/"]
RUN dotnet restore "MyFirstMicroService.Banking.Api/MyFirstMicroService.Banking.Api.csproj"
COPY . .
WORKDIR "/src/MyFirstMicroService.Banking.Api"
RUN dotnet build "MyFirstMicroService.Banking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyFirstMicroService.Banking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyFirstMicroService.Banking.Api.dll"]