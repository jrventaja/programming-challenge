#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MediaCatalog/MediaCatalog.csproj", "MediaCatalog/"]
RUN dotnet restore "MediaCatalog/MediaCatalog.csproj"
COPY . .
WORKDIR "/src/MediaCatalog"
RUN dotnet build "MediaCatalog.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MediaCatalog.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaCatalog.dll"]