FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 64834
EXPOSE 44362

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/RetailBay.WebShop/RetailBay.WebShop.csproj", "src/RetailBay.WebShop/"]
RUN dotnet restore "src/RetailBay.WebShop/RetailBay.WebShop.csproj"
COPY . .
WORKDIR "/src/src/RetailBay.WebShop"
RUN dotnet build "RetailBay.WebShop.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RetailBay.WebShop.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RetailBay.WebShop.dll"]