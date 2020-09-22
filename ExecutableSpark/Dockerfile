FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ExecutableSparkApi.csproj", "ExecutableSparkApi/"]
RUN dotnet restore "ExecutableSparkApi\ExecutableSparkApi.csproj"
COPY . .
WORKDIR "/src/ExecutableSparkApi"
RUN dotnet build "ExecutableSparkApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExecutableSparkApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExecutableSparkApi.dll"]
