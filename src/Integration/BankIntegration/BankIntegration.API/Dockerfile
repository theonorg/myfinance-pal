FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV COMPlus_EnableDiagnostics=0

# RUN addgroup --group funappgroup --gid 2000 \
#     && adduser --uid 1000 --gid 2000 "funappuser" 
# RUN chown funappuser:funappgroup  /app
# USER funappuser:funappgroup 

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
ARG GITHUB_TOKEN
ENV GITHUB_TOKEN=$GITHUB_TOKEN
WORKDIR /src
COPY ["BankIntegration.API.csproj", "BankIntegration.API/"]
COPY ["nuget.config", "BankIntegration.API/"]
RUN dotnet restore "BankIntegration.API/BankIntegration.API.csproj"

WORKDIR "/BankIntegration.API"
COPY . .
RUN dotnet build "BankIntegration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankIntegration.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BankIntegration.API.dll"]

