migration:
	@echo "Creating migration"
	dotnet ef migrations add $(name)

migration-apply:
	@echo "Applying migration"
	dotnet ef database update