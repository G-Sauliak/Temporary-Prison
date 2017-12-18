CREATE FUNCTION IsValidUser(@name nvarchar(max),@Password nvarchar(max))
RETURNS bit
AS
BEGIN
DECLARE @RESULT bit;
DECLARE @PASS nvarchar(max);
SET @pass = (SELECT Password FROM web_Users WHERE UserName = @name AND Password = @Password)
 SELECT @RESULT = CASE WHEN @pass IS NULL 
            THEN 0 
            ELSE 1 
        END
RETURN @RESULT
END
GO
Create FUNCTION [dbo].[IsExistsLogin](@userName nvarchar(max))
RETURNS bit
AS
BEGIN
DECLARE @IsExist bit;
DECLARE @Exist  nvarchar(max);
SET @Exist = (SELECT UserName FROM web_Users WHERE UserName = @userName )
 SELECT @IsExist = CASE WHEN @Exist  IS NULL 
            THEN 0 
            ELSE 1 
        END
RETURN @IsExist
END
GO
CREATE FUNCTION [dbo].[IsExistsEmail](@email nvarchar(max))
RETURNS bit
AS
BEGIN
DECLARE @IsExists bit;
DECLARE @Exists  nvarchar(max);
SET @Exists = (SELECT UserName FROM web_Users WHERE Email = @email )
 SELECT @IsExists = CASE WHEN @Exists  IS NULL 
            THEN 0 
            ELSE 1 
        END
RETURN @IsExists
END