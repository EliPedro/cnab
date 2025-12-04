# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers for caching
COPY ["src/WebSite/WebSite.csproj", "src/WebSite/"]
RUN dotnet restore "src/WebSite/WebSite.csproj"

# Copy everything and publish
COPY . .
WORKDIR "/src/src/WebSite"
RUN dotnet publish "WebSite.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_RUNNING_IN_CONTAINER=true

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "WebSite.dll"]