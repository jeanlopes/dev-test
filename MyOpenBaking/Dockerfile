#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyOpenBaking/MyOpenBaking.Api.csproj", "MyOpenBaking/"]
COPY ["MyOpenBanking.Application/MyOpenBanking.Application.csproj", "MyOpenBanking.Application/"]
COPY ["MyOpenBanking.Domain/MyOpenBanking.Domain.csproj", "MyOpenBanking.Domain/"]
COPY ["MyOpenBanking.IoC/MyOpenBanking.IoC.csproj", "MyOpenBanking.IoC/"]
COPY ["MyOpenBanking.DataAccess/MyOpenBanking.DataAccess.csproj", "MyOpenBanking.DataAccess/"]
RUN dotnet restore "MyOpenBaking/MyOpenBaking.Api.csproj"
COPY . .
WORKDIR "/src/MyOpenBaking"

RUN apt-get update && apt-get install --no-install-recommends -yy curl
# Using Debian, as root
RUN apt-get update && apt-get install -y apt-transport-https
RUN curl -fsSL https://deb.nodesource.com/setup_current.x | bash -
RUN apt-get install -y build-essential
RUN apt-get install -y nodejs

RUN dotnet build "MyOpenBaking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyOpenBaking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyOpenBaking.Api.dll"]