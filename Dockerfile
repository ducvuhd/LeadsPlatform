FROM mcr.microsoft.com/dotnet/core/sdk:3.1  AS base
WORKDIR /app
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80  
RUN sed -i "s|MinProtocol = TLSv1.2|MinProtocol = TLSv1|g" /etc/ssl/openssl.cnf && \
    sed -i 's|CipherString = DEFAULT@SECLEVEL=2|CipherString = DEFAULT@SECLEVEL=1|g' /etc/ssl/openssl.cnf
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["DVDV.ICS.API/DVDV.ICS.API.csproj", "DVDV.ICS.API/"]
COPY ["DVDV.ICS.BusinessLayer/DVDV.ICS.BusinessLayer.csproj", "DVDV.ICS.BusinessLayer/"]
COPY ["DVDV.ICS.Caching/DVDV.ICS.Caching.csproj", "DVDV.ICS.Caching/"]
COPY ["DVDV.ICS.DataAccessLayer/DVDV.ICS.DataAccessLayer.csproj", "DVDV.ICS.DataAccessLayer/"]
COPY ["DVDV.ICS.Entity/DVDV.ICS.Entity.csproj", "DVDV.ICS.Entity/"]
COPY ["DVDV.ICS.Logging/DVDV.ICS.Logging.csproj", "DVDV.ICS.Logging/"]
COPY ["DVDV.ICS.Models/DVDV.ICS.Models.csproj", "DVDV.ICS.Models/"]
COPY ["DVDV.ICS.Utility/DVDV.ICS.Utility.csproj", "DVDV.ICS.Utility/"]


RUN dotnet restore "DVDV.ICS.API/DVDV.ICS.API.csproj"
COPY . .
WORKDIR "/src/DVDV.ICS.API"
RUN dotnet build "DVDV.ICS.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DVDV.ICS.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DVDV.ICS.API.dll"]

