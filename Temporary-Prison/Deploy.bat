sqlcmd -S ADMINISTRATOR\SQLEXPRESS -i SqlScripts\CreateDb.sql
sqlcmd -S ADMINISTRATOR\SQLEXPRESS -i SqlScripts\Deploy.sql
sqlcmd -S ADMINISTRATOR\SQLEXPRESS -i SqlScripts\TableTypes.sql
sqlcmd -S ADMINISTRATOR\SQLEXPRESS -i SqlScripts\StoredProcedures.sql
sqlcmd -S ADMINISTRATOR\SQLEXPRESS -i SqlScripts\ScalarValuedFunctions.sql

pause

pause
