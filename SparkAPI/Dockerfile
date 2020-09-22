#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["SparkAPI.csproj", "SparkAPI/"]
RUN dotnet restore "SparkAPI/SparkAPI.csproj"
WORKDIR "/src/SparkAPI"
COPY . .
RUN dotnet build "SparkAPI.csproj" -c Release -o /app/publish

FROM build AS publish
RUN dotnet publish "SparkAPI.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app/publish
ENTRYPOINT ["dotnet", "SparkAPI.dll"]