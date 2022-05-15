#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CinemaApplicationProject.API/CinemaApplicationProject.API.csproj", "CinemaApplicationProject.API/"]
COPY ["CinemaApplicationProject.Model/CinemaApplicationProject.Model.csproj", "CinemaApplicationProject.Model/"]
RUN dotnet restore "CinemaApplicationProject.API/CinemaApplicationProject.API.csproj"
COPY . .
WORKDIR "/src/CinemaApplicationProject.API"
RUN dotnet build "CinemaApplicationProject.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaApplicationProject.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CinemaApplicationProject.API.dll"]