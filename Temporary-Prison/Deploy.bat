sqlcmd -i SqlScripts\CreateDb.sql
sqlcmd -i SqlScripts\Deploy.sql
sqlcmd -i SqlScripts\TableTypes.sql
sqlcmd  -i SqlScripts\StoredProcedures.sql
sqlcmd -i SqlScripts\ScalarValuedFunctions.sql

pause

