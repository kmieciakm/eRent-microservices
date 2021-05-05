FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RentService/Web/Web.csproj", "RentService/Web/"]
COPY ["RentService/Domain/Domain.csproj", "RentService/Domain/"]
COPY ["RentService/Web.Setup/Web.Setup.csproj", "RentService/Web.Setup/"]
COPY ["RentService/Database/Database.csproj", "RentService/Database/"]
RUN dotnet restore "RentService/Web/Web.csproj"
COPY . .
WORKDIR "/src/RentService/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]