FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["OcelotApi.csproj", "./"]
RUN dotnet restore "OcelotApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "OcelotApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApi.dll"]