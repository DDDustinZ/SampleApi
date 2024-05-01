﻿ARG SQL_HOST
ARG SQL_PORT
ARG SQL_USERNAME
ARG SQL_PASSWORD
ARG BASE_CONNECTION_STRING="Server=$SQL_HOST,$SQL_PORT;Persist Security Info=False;User ID=$SQL_USERNAME;Password=$SQL_PASSWORD;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;"

FROM mcr.microsoft.com/mssql-tools AS sqltools
ARG SQL_HOST
ARG SQL_PORT
ARG SQL_USERNAME
ARG SQL_PASSWORD

WORKDIR /repo
COPY . .

RUN until /opt/mssql-tools/bin/sqlcmd -S ${SQL_HOST},${SQL_PORT} -U ${SQL_USERNAME} -P ${SQL_PASSWORD} -C -Q " "; do sleep 1; done
RUN /opt/mssql-tools/bin/sqlcmd -S ${SQL_HOST},${SQL_PORT} -U ${SQL_USERNAME} -P ${SQL_PASSWORD} -C -i build/create-local-dbs.sql

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG SQL_HOST
ARG BASE_CONNECTION_STRING

RUN dotnet tool install dotnet-ef --tool-path /bin

WORKDIR /repo
COPY --from=sqltools /repo .

RUN dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/Api/Api.csproj

FROM sqltools AS seed

# Need to copy something from previous stages otherwise they'll be skipped
COPY --from=build /repo/schema.Dockerfile .
RUN /opt/mssql-tools/bin/sqlcmd -S ${SQL_HOST},${SQL_PORT} -U ${SQL_USERNAME} -P ${SQL_PASSWORD} -C -d Sales -i build/seed-database.sql
