FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5010
EXPOSE 5011

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AydoganERP.APi/AydoganERP.APi.csproj", "AydoganERP.APi/"]
COPY ["AydoganERP.Base.Application/AydoganERP.Base.Application.csproj", "AydoganERP.Base.Application/"]
COPY ["AydoganERP.Base.Domain/AydoganERP.Base.Domain.csproj", "AydoganERP.Base.Domain/"]
COPY ["AydoganERP.Base.Infrastructure/AydoganERP.Base.Infrastructure.csproj", "AydoganERP.Base.Infrastructure/"]
COPY ["AydoganERP.Company.Application/AydoganERP.Company.Application.csproj", "AydoganERP.Company.Application/"]
COPY ["AydoganERP.Company.Domain/AydoganERP.Company.Domain.csproj", "AydoganERP.Company.Domain/"]
COPY ["AydoganERP.Company.Infrastructure/AydoganERP.Company.Infrastructure.csproj", "AydoganERP.Company.Infrastructure/"]
COPY ["AydoganERP.Customer.Application/AydoganERP.Customer.Application.csproj", "AydoganERP.Customer.Application/"]
COPY ["AydoganERP.Customer.Infrastructure/AydoganERP.Customer.Infrastructure.csproj", "AydoganERP.Customer.Infrastructure/"]
COPY ["AydoganERP.Identity.Application/AydoganERP.Identity.Application.csproj", "AydoganERP.Identity.Application/"]
COPY ["AydoganERP.Identity.Domain/AydoganERP.Identity.Domain.csproj", "AydoganERP.Identity.Domain/"]
COPY ["AydoganERP.Identity.Infrastructure/AydoganERP.Identity.Infrastructure.csproj", "AydoganERP.Identity.Infrastructure/"]
COPY ["AydoganERP.Inventory.Application/AydoganERP.Inventory.Application.csproj", "AydoganERP.Inventory.Application/"]
COPY ["AydoganERP.Inventory.Domain/AydoganERP.Inventory.Domain.csproj", "AydoganERP.Inventory.Domain/"]
COPY ["AydoganERP.Inventory.Infrastructure/AydoganERP.Inventory.Infrastructure.csproj", "AydoganERP.Inventory.Infrastructure/"]
COPY ["AydoganERP.Finance.Application/AydoganERP.Finance.Application.csproj", "AydoganERP.Finance.Application/"]
RUN dotnet restore "./AydoganERP.APi/AydoganERP.APi.csproj"
COPY . .
WORKDIR "/src/AydoganERP.APi"
RUN dotnet build "./AydoganERP.APi.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN sh -c "dotnet dev-certs https --clean"
RUN sh -c "dotnet dev-certs https"
RUN sh -c "dotnet dev-certs https --trust"
RUN dotnet build "./AydoganERP.APi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AydoganERP.APi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_Kestrel__Certificates__Default__Password="12111211"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="aspnetapp.pfx"
ENV ASPNETCORE_ENVIRONMENT Production

ENTRYPOINT ["dotnet", "AydoganERP.APi.dll"]

USER root

RUN ln -sf /usr/share/zoneinfo/Europe/Istanbul /etc/localtime