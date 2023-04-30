FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY projectfiles.tar .
RUN tar -xvf projectfiles.tar

RUN dotnet restore "src/Apis/DVG.AP.Cms.CarInfo.Api/DVG.AP.Cms.CarInfo.Api.csproj"  -s http://172.16.0.66:8083/v3/index.json || dotnet restore "src/Apis/DVG.AP.Cms.CarInfo.Api/DVG.AP.Cms.CarInfo.Api.csproj" -s https://api.nuget.org/v3/index.json
COPY . .
RUN ls
RUN ls /src
WORKDIR "/src/Apis/DVG.AP.Cms.CarInfo.Api"
RUN dotnet build "DVG.AP.Cms.CarInfo.Api.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "DVG.AP.Cms.CarInfo.Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG SITE_NAME
ENV SITE_NAME=$SITE_NAME
ENTRYPOINT ["dotnet", "DVG.AP.Cms.CarInfo.Api.dll","--environment=Testing"]
