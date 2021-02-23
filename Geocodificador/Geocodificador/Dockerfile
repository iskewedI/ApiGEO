FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src
COPY ["Geocodificador/Geocodificador.csproj", "Geocodificador/"]
COPY ["Geocodificador.Data/Geocodificador.GeoApi.Data.csproj", "Geocodificador.Data/"]
COPY ["Geocodificador.Domain/Geocodificador.GeoApi.Domain.csproj", "Geocodificador.Domain/"]
COPY ["Geocodificador.Service/Geocodificador.Service.csproj", "Geocodificador.Service/"]
COPY ["Geocodificador.Messaging.Send/Geocodificador.Messaging.Send.csproj", "Geocodificador.Messaging.Send/"]
COPY ["Geocodificador.Messaging.Receive/Geocodificador.Messaging.Receive.csproj", "Geocodificador.Messaging.Receive/"]
# COPY ["Tests/CustomerApi.Test/CustomerApi.Test.csproj", "Tests/CustomerApi.Test/"]   TODO
# COPY ["Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj", "Tests/CustomerApi.Service.Test/"]  
# COPY ["Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj", "Tests/CustomerApi.Data.Test/"] 
COPY ["Geocodificador/nuget.config", ""]
COPY ["*.props", "./"]

ARG PAT=localhost
RUN sed -i "s|</configuration>|<packageSourceCredentials><MicroserviceDemoNugets><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></MicroserviceDemoNugets></packageSourceCredentials></configuration>|" nuget.config

RUN dotnet restore "Geocodificador/Geocodificador.csproj" --configfile "./nuget.config"
# RUN dotnet restore "Tests/CustomerApi.Test/CustomerApi.Test.csproj" --configfile "./nuget.config" TODO
# RUN dotnet restore "Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj" --configfile "./nuget.config"
# RUN dotnet restore "Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj" --configfile "./nuget.config"
COPY . .

RUN dotnet build "Geocodificador/Geocodificador.csproj" -c Release -o /app/build --no-restore
# RUN dotnet build "Tests/CustomerApi.Test/CustomerApi.Test.csproj" -c Release --no-restore TODO
# RUN dotnet build "Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj" -c Release --no-restore
# RUN dotnet build "Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj" -c Release --no-restore

FROM build AS test  
ARG BuildId=localhost
LABEL test=${BuildId}
# RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/CustomerApi.Test/CustomerApi.Test.csproj   TODO
# RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/CustomerApi.Service.Test/CustomerApi.Service.Test.csproj  
# RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results3.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/CustomerApi.Data.Test/CustomerApi.Data.Test.csproj

FROM build AS publish
RUN dotnet publish "Geocodificador/Geocodificador.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geocodificador.dll"]