#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LibraryApp.API/LibraryApp.API.csproj", "LibraryApp.API/"]
COPY ["LibraryApp.Data/LibraryApp.Data.csproj", "LibraryApp.Data/"]
COPY ["LibraryApp.Domain/LibraryApp.Domain.csproj", "LibraryApp.Domain/"]
RUN dotnet restore "LibraryApp.API/LibraryApp.API.csproj"
COPY . .
WORKDIR "/src/LibraryApp.API"
RUN dotnet build "LibraryApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryApp.API.dll"]
