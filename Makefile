.PHONY: * #since no targets will produce files, saves us from needing to specify on all https://www.gnu.org/software/make/manual/html_node/Phony-Targets.html

SQL_HOST = host.docker.internal
SQL_PORT = 1433
SQL_USERNAME = sa
SQL_PASSWORD = Localp@55

all: deps build run

deps: docker-compose db

docker-compose:
	docker compose -p shared -f shared-compose.yml up -d
	docker compose -p sample-api-deps up -d --force-recreate

db:
	docker build . -f schema.Dockerfile --no-cache --label temp  \
    		--build-arg SQL_HOST=$(SQL_HOST) \
    		--build-arg SQL_PORT=$(SQL_PORT) \
    		--build-arg SQL_USERNAME=$(SQL_USERNAME) \
    		--build-arg SQL_PASSWORD=$(SQL_PASSWORD)
	docker image prune --filter label=temp --force

build: stop
	-docker image rm sample-api 
	@docker build . -f src/Api/Dockerfile -t sample --no-cache

stop:
	-docker rm -f sample-api
 
run: stop
	docker run -d -p 53182:443 -p 53181:80 \
		-e RabbitMq__Host=rabbitmq://host.docker.internal \
		-e ConnectionStrings__SalesDbContext='Data Source=host.docker.internal,1433;Persist Security Info=True;Initial Catalog=Sales;User ID=$(sqlUsername);Password=$(sqlPassword);TrustServerCertificate=True' \
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
	docker build . -f src/Api/Dockerfile --no-cache --label temp --target test \
    		--build-arg SQL_HOST=$(SQL_HOST) \
    		--build-arg SQL_PORT=$(SQL_PORT) \
    		--build-arg SQL_USERNAME=$(SQL_USERNAME) \
    		--build-arg SQL_PASSWORD=$(SQL_PASSWORD)
	docker image prune --filter label=temp --force