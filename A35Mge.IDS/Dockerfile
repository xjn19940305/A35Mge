#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["A35Mge.IDS/A35Mge.IDS.csproj", "A35Mge.IDS/"]
RUN dotnet restore "A35Mge.IDS/A35Mge.IDS.csproj"
COPY . .
WORKDIR "/src/A35Mge.IDS"
RUN dotnet build "A35Mge.IDS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "A35Mge.IDS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "A35Mge.IDS.dll"]