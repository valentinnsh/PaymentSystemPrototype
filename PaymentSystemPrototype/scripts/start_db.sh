docker-compose --project-name=PaymentSystemPrototype  --file=../Docker/docker-compose.yml up -d
dotnet ef database update --project "../" 