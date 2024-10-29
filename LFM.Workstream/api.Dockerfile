FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./LFM.WorkStream.Api/*.csproj ./LFM.WorkStream.Api/
COPY ./LFM.WorkStream.Application/*.csproj ./LFM.WorkStream.Application/
COPY ./LFM.WorkStream.Core/*.csproj ./LFM.WorkStream.Core/
COPY ./LFM.WorkStream.Repository/*.csproj ./LFM.WorkStream.Repository/

ARG NUGET_PAT=""
ARG NUGET_USER=""

RUN dotnet nuget add source --username "$NUGET_USER" --password "$NUGET_PAT" --store-password-in-clear-text --name "github" "https://nuget.pkg.github.com/ThijmenBrand-LifeManager/index.json"
RUN dotnet restore LFM.WorkStream.Api/LFM.WorkStream.Api.csproj

COPY . .

WORKDIR "/app/LFM.WorkStream.Api"
RUN dotnet build "LFM.WorkStream.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LFM.WorkStream.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LFM.WorkStream.Api.dll"]
