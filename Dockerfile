FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Toi.Backend/Toi.Backend.csproj", "."]
RUN dotnet restore "./Toi.Backend.csproj"
COPY ["./Toi.Backend/", "."]
RUN dotnet build "Toi.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Toi.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Toi.Backend.dll
