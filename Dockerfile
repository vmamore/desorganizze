#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Desorganizze/Desorganizze.csproj", "Desorganizze/"]
RUN dotnet restore "Desorganizze/Desorganizze.csproj"
COPY . .
WORKDIR "/src/Desorganizze"
RUN dotnet build "Desorganizze.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desorganizze.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desorganizze.dll"]