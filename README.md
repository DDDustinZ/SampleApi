# Project Setup

## Prerequisites
- NET8
- Docker
- make: `choco install make`
- Install required packages: `make install`

## Build
- Run dependencies: `make deps`
- Run Api project
    - IDE: Run `Api` project
    - CLI: `make build` followed by `make run`
- Alternatively you can run `make` or `make all` to create dependencies and API in docker

## Local Docker Dependencies
- DB Server
    - Server: `localhost,1433` with username: `sa` and password `Localp@55`
    - Database `Sales` will have seed data from `seed-database.sql`

## Makefile Targets
To run a target, simply type `make` followed by the target. i.e. `make build` will run the `build` target
- `docker-compose` - runs the docker compose to set up base instances of dependencies
- `db` - runs the db scripts to create them, migrate schema, and seed
- `deps` - sets up dependencies by running both `docker-compose` and `db`
- `build` - builds a new API docker image
- `run` - starts a container of the latest API image
- `stop` - stops and removes the API container
- `clean` - runs the `stop` target and tears downs docker compose images
- `test` - runs all tests in the solution within the docker image as the build
- `install` - installs required tools
- `migration` - with parameter `name=<name>` will create a new EF migration with name
- `migration-remove` - removes the last migration
- `db-script` - creates an idempotent SQL script for migrations
