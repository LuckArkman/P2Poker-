FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["P2PokerAPI.csproj", "./"]
RUN dotnet restore "P2PokerAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "P2PokerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P2PokerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P2PokerAPI.dll"]
