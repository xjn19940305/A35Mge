#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["A35Mge.Api/A35Mge.Api.csproj", "A35Mge.Api/"]
COPY ["A35Mge.Dependency/A35Mge.Dependency.csproj", "A35Mge.Dependency/"]
COPY ["A35Mge.Infrastructure/A35Mge.Infrastructure.csproj", "A35Mge.Infrastructure/"]
COPY ["A35Mge.Enum/A35Mge.Enum.csproj", "A35Mge.Enum/"]
COPY ["A35Mge.Service/A35Mge.Service.csproj", "A35Mge.Service/"]
COPY ["A35Mge.Model/A35Mge.Model.csproj", "A35Mge.Model/"]
COPY ["A35Mge.Database/A35Mge.Database.csproj", "A35Mge.Database/"]
COPY ["A35Mge.ScheduleTask/A35Mge.ScheduleTask.csproj", "A35Mge.ScheduleTask/"]
COPY ["A35Mge.Redis/A35Mge.Redis.csproj", "A35Mge.Redis/"]
COPY ["A35Mge.MySqlDatabase/A35Mge.MySqlDatabase.csproj", "A35Mge.MySqlDatabase/"]
RUN dotnet restore "A35Mge.Api/A35Mge.Api.csproj"
COPY . .
WORKDIR "/src/A35Mge.Api"
RUN dotnet build "A35Mge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "A35Mge.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "A35Mge.Api.dll"]