FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["UserService/Web/Web.csproj", "UserService/Web/"]
COPY ["UserService/Domain/Domain.csproj", "UserService/Domain/"]
COPY ["UserService/Database/Database.csproj", "UserService/Database/"]
COPY ["UserService/Mailing/Mailing.csproj", "UserService/Mailing/"]
RUN dotnet restore "UserService/Web/Web.csproj"
COPY . .
WORKDIR "/src/UserService/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]