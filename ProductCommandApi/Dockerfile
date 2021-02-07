FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["ProductCommandApi/ProductCommandApi.csproj", "ProductCommandApi/"]
COPY ["CoreCommand/CoreCommand.csproj", "CoreCommand/"]
COPY ["ApplicationCommand/ApplicationCommand.csproj", "ApplicationCommand/"]
COPY ["InfrastructureCommand/InfrastructureCommand.csproj", "InfrastructureCommand/"]
RUN dotnet restore "ProductCommandApi/ProductCommandApi.csproj"
COPY . .
WORKDIR "/src/ProductCommandApi"
RUN dotnet build "ProductCommandApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductCommandApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCommandApi.dll"]