.PHONY: * #since no targets will produce files, saves us from needing to specify on all https://www.gnu.org/software/make/manual/html_node/Phony-Targets.html

sqlUsername = sa
sqlPassword = Localp@55
sqlHost = localhost
sqlPort = 1433

all: deps build run

deps: docker-compose waitForDb db

docker-compose:
	docker compose -p shared -f shared-compose.yml up -d
	docker compose -p sample-api-deps up -d --force-recreate

waitForDb:
ifeq ($(OS), Windows_NT)
	powershell ./build/wait-for-db.ps1 $(sqlHost) $(sqlPort) $(sqlUsername) $(sqlPassword)
else
	./build/wait-for-db.sh $(sqlHost) $(sqlPort) $(sqlUsername) $(sqlPassword)
endif
	@echo DB Ready!

db:
	sqlcmd -S $(sqlHost),$(sqlPort) -U $(sqlUsername) -P $(sqlPassword) -i build/create-local-dbs.sql
	dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/Api/Api.csproj
	sqlcmd -S $(sqlHost),$(sqlPort) -U $(sqlUsername) -P $(sqlPassword) -d Sales -i build/seed-database.sql

build: stop
	-docker image rm sample-api 
	@docker build . -f src/Api/Dockerfile -t sample --no-cache

stop:
	-docker rm -f sample-api
 
run: stop
	docker run -d -p 53182:443 -p 53181:80 \
		-e RabbitMq__Host=rabbitmq://host.docker.internal \
		-e ConnectionStrings__SSalesDbContext='Data Source=host.docker.internal,1433;Persist Security Info=True;Initial Catalog=Sales;User ID=$(sqlUsername);Password=$(sqlPassword);TrustServerCertificate=True' \
		--name sample-api \
		sample-api

clean: stop
	docker compose -p shared down
	docker compose -p sample-api-deps down

install:
	-dotnet tool update --global dotnet-ef

migration:
	dotnet ef migrations add $(name) --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\Api\Api.csproj

migration-remove:
	dotnet ef migrations remove --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\Api\Api.csproj

db-script:
	dotnet ef migrations script --idempotent --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\Api\Api.csproj

test:
	dotnet test SampleApi.sln