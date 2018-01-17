CREATE DATABASE PrisonDb
ON (
  NAME = dbName_dat,
  FILENAME = 'd:\PrisonDb.mdf'
)
LOG ON (
  NAME = dbName_log,
  FILENAME = 'd:\PrisonDb.ldf'
)
GO
USE PrisonDb
GO
CREATE LOGIN fon WITH PASSWORD = 'asd1301!'
GO
sp_addsrvrolemember 'fon', 'sysadmin'
GO
EXEC sp_changedbowner 'fon';  
GO



