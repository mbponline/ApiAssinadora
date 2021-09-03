FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:80
    
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["*.csproj", "API/"]
RUN dotnet restore "API/ApiAssinadora.csproj"
COPY . ./API
WORKDIR "/src/API"
RUN dotnet build "ApiAssinadora.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiAssinadora.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

ENTRYPOINT ["dotnet","ApiAssinadora.dll"]