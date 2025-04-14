# Build a?amas?
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution dosyas?n? kopyala
COPY ["glassesRecommendation.sln", "./"]
COPY ["glassesRecommendation.API/glassesRecommendation.API.csproj", "glassesRecommendation.API/"]
COPY ["glassesRecommendation.Core/glassesRecommendation.Core.csproj", "glassesRecommendation.Core/"]
COPY ["glassesRecommendation.Data/glassesRecommendation.Data.csproj", "glassesRecommendation.Data/"]
COPY ["glassesRecommendation.Service/glassesRecommendation.Service.csproj", "glassesRecommendation.Service/"]

# Projeleri restore et
RUN dotnet restore "./glassesRecommendation.sln"

# Tüm dosyalar? kopyala
COPY . .

# Publish et
WORKDIR "/src/glassesRecommendation.API"
RUN dotnet publish "glassesRecommendation.API.csproj" -c Release -o /app/publish

# Runtime a?amas?
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "glassesRecommendation.API.dll"]
