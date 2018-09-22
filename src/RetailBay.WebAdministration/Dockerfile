FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51468
EXPOSE 44379

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/RetailBay.WebAdministration/RetailBay.WebAdministration.csproj", "src/RetailBay.WebAdministration/"]
RUN dotnet restore "src/RetailBay.WebAdministration/RetailBay.WebAdministration.csproj"
COPY . .
WORKDIR "/src/src/RetailBay.WebAdministration"
RUN dotnet build "RetailBay.WebAdministration.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RetailBay.WebAdministration.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RetailBay.WebAdministration.dll"]