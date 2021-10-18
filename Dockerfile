FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UserService/UserServiceApi/UserServiceApi.csproj", "UserService/UserServiceApi/"]
RUN dotnet restore "UserService\UserServiceApi\UserServiceApi.csproj"
COPY . .
WORKDIR "/src/UserService/UserServiceApi"
RUN dotnet build "UserServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserServiceApi.dll"]
