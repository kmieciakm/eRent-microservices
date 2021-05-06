FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MailingService/Web/Web.csproj", "MailingService/Web/"]
COPY ["MailingService/Domain/Domain.csproj", "MailingService/Domain/"]
RUN dotnet restore "MailingService/Web/Web.csproj"
COPY . .
WORKDIR "/src/MailingService/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]