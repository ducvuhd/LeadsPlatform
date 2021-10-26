#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry-ap.nextgen-global.com:5000/dotnet-core/aspnet-3.1:latest AS base
WORKDIR /app
RUN sed -i "s|MinProtocol = TLSv1.2|MinProtocol = TLSv1|g" /etc/ssl/openssl.cnf && \
    sed -i 's|CipherString = DEFAULT@SECLEVEL=2|CipherString = DEFAULT@SECLEVEL=1|g' /etc/ssl/openssl.cnf
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["APVN.LeadsPlatform/APVN.LeadsPlatform.csproj", "APVN.LeadsPlatform/"]
COPY ["APVN.LeadsPlatform.Entity/APVN.LeadsPlatform.Entity.csproj", "APVN.LeadsPlatform.Entity/"]
COPY ["APVN.LeadsPlatform.Utility/APVN.LeadsPlatform.Utility.csproj", "APVN.LeadsPlatform.Utility/"]
COPY ["APVN.LeadsPlatform.BusinessLayer/APVN.LeadsPlatform.BusinessLayer.csproj", "APVN.LeadsPlatform.BusinessLayer/"]
COPY ["APVN.LeadsPlatform.DataAccessLayer/APVN.LeadsPlatform.DataAccessLayer.csproj", "APVN.LeadsPlatform.DataAccessLayer/"]
COPY ["APVN.LeadsPlatform.Models/APVN.LeadsPlatform.Models.csproj", "APVN.LeadsPlatform.Models/"]
COPY ["APVN.LeadsPlatform.Caching/APVN.LeadsPlatform.Caching.csproj", "APVN.LeadsPlatform.Caching/"]
COPY ["APVN.LeadsPlatform.Logging/APVN.LeadsPlatform.Logging.csproj", "APVN.LeadsPlatform.Logging/"]
RUN dotnet restore "APVN.LeadsPlatform/APVN.LeadsPlatform.csproj"  -s http://172.16.0.66:8083/v3/index.json || dotnet restore "APVN.LeadsPlatform/APVN.LeadsPlatform.csproj"  -s https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/APVN.LeadsPlatform"
#RUN dotnet build "APVN.LeadsPlatform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APVN.LeadsPlatform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ=Asia/Ho_Chi_Minh
ENTRYPOINT ["dotnet", "APVN.LeadsPlatform.dll","--environment=Testing"]