FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["SailingActors.Actors/SailingActors.Actors.csproj", "SailingActors.Actors/"]
COPY ["SailingActors.Shared/SailingActors.Shared.csproj", "SailingActors.Shared/"]

RUN dotnet restore "SailingActors.Actors/SailingActors.Actors.csproj"
COPY . .
WORKDIR "/src/SailingActors.Actors"
RUN dotnet build "SailingActors.Actors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SailingActors.Actors.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SailingActors.Actors.dll"]