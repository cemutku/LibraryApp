#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=development

WORKDIR /app
COPY ["LibraryApp.API/LibraryApp.API.csproj", "LibraryApp.API/"]
COPY ["LibraryApp.Data/LibraryApp.Data.csproj", "LibraryApp.Data/"]
COPY ["LibraryApp.Domain/LibraryApp.Domain.csproj", "LibraryApp.Domain/"]

RUN dotnet restore "LibraryApp.API/LibraryApp.API.csproj"
COPY . .

WORKDIR "/app/LibraryApp.API"
EXPOSE 5000

ENTRYPOINT ["/bin/bash", "-c", "dotnet watch run --urls http://0.0.0.0:5000"]

#ENTRYPOINT [ "/bin/bash", "-c", "dotnet", "watch", "run", "--no-restore", "--urls", "https://0.0.0.0:5000"]