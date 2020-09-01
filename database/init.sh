# Wait to be sure that SQL Server is online
sleep 30s

# Run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Test-123456* -d master -i init.sql
