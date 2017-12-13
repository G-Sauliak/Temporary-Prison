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