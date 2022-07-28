FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ARG GITHUB_TOKEN
ENV GITHUB_TOKEN=$GITHUB_TOKEN
WORKDIR /src
COPY ["AssetsService.API.csproj", "AssetsService.API/"]
COPY ["nuget.config", "AssetsService.API/"]
RUN dotnet restore "AssetsService.API/AssetsService.API.csproj"

WORKDIR "/AssetsService.API"
COPY . .
RUN dotnet build "AssetsService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssetsService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AssetsService.API.dll"]