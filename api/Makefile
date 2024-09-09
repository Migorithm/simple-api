migration:
	@echo "Creating migration"
	dotnet ef migrations add $(name)

migration-apply:
	@echo "Applying migration"
	dotnet ef database update

db-cleanup:
	docker-compose down -v
	docker-compose up -d
	dotnet ef migrations add $(name)
	dotnet ef database update

