---------------------Add Phone---------------------------------
---------------------------------------------------------------
CREATE PROCEDURE [dbo].[AddPhoneNumber]
	@PhoneNumber nvarchar(50),
	@PrisonerId int
AS
	INSERT INTO PhoneNumbers (PhoneNumber,PrisonerID) VALUES (@PhoneNumber,@PrisonerId)
GO
---------------------Add Prisoner------------------------------
---------------------------------------------------------------
CREATE PROC [dbo].[AddPriosner]
@FirstName nvarchar(50),
@SurName nvarchar(50),
@LastName nvarchar(50),
@PlaceOfWork nvarchar(50),
@BirthDate date,
@Photo nvarchar(max),
@Address nvarchar(max),
@AdditionalInformation nvarchar(max),
@RelationshipStatus nvarchar(50)

AS
BEGIN
    INSERT INTO Prisoners(
	FirstName,
	LastName,
	Surname,
	PlaceOfWork,
	Photo,
    BirthDate,
	RelationshipStatus,
	[Address],
	AdditionalInformation
	)
	VALUES(
	@FirstName,
	@LastName,
	@SurName,
	@PlaceOfWork,
	@Photo,
	@BirthDate,
	@RelationshipStatus,
	@Address,
	@AdditionalInformation
	)

	DECLARE @LastId int = SCOPE_IDENTITY()
RETURN @lastID
END
---------------------GetPrisonerById---------------------------
---------------------------------------------------------------
GO
CREATE PROCEDURE [dbo].[GetPrisonerById]
	@ID int
AS
SELECT * FROM Prisoners p WHERE  p.PrisonerId = @ID

SELECT PhoneNumber FROM PhoneNumbers ph, Prisoners pr
WHERE ph.PrisonerID = pr.PrisonerId AND ph.PrisonerID = @ID
---------------------GetPrisonersForPageList--------------------
---------------------------------------------------------------
GO
CREATE PROC [dbo].[GetPrisonersToPageList]
@skip int,
@rowSize int
AS
SELECT * FROM Prisoners p
ORDER BY p.FirstName 
OFFSET @skip ROWS 
FETCH NEXT @rowSize ROWS ONLY;
DECLARE @TotalCount int
SET @Totalcount = (SELECT COUNT(*) FROM Prisoners)

RETURN @Totalcount
GO
---------------------GetRoles----------------------------------
---------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetRolesByUserName]
@userName nvarchar(50)
AS
SELECT w_R.RoleName FROM web_UserRoles w_UR, web_Roles w_R, web_Users w_U 
WHERE w_UR.RoleID = w_R.RoleID AND w_UR.UserID = w_U.UserID AND w_U.UserName = @userName
GO
---------------------GetUserByName-----------------------------
---------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetUserByName]
	@userName nvarchar(50)
AS
  SELECT * FROM  web_Users u WHERE  u.UserName = @userName
GO
---------------------GetUsersForPagedList--------------------------
-------------------------------------------------------------------
CREATE PROC [dbo].[GetUsersForPagedList]
@skip int,
@rowSize int
AS
SELECT UserName,Email FROM web_Users u
ORDER BY u.UserName 
OFFSET @skip ROWS 
FETCH NEXT @rowSize ROWS ONLY;
DECLARE @TotalCount int
SET @Totalcount = (SELECT COUNT(*) FROM web_Users)

RETURN @Totalcount
GO
---------------------GETALLROLES-----------------------------------
-------------------------------------------------------------------
CREATE PROC [dbo].[GetAllRoles]
AS
BEGIN
SELECT RoleName FROM web_Roles
END
GO
---------------------ADD USER--------------------------------------
-------------------------------------------------------------------
CREATE PROC [dbo].[AddUser]
@UserName nvarchar(50),
@Email nvarchar(max),
@Password nvarchar(max)
 
AS
BEGIN
    INSERT INTO web_Users(
	UserName,
	Email,
	Password
	)
	VALUES(
	@UserName,
	@Email,
	@Password
	)
END
	DECLARE @LastId int = SCOPE_IDENTITY()
RETURN @lastID
GO
---------------------ADD TO ROLES---------------------------------------
------------------------------------------------------------------------
GO
CREATE PROCEDURE [dbo].[AddToRoles]
	@RoleName nvarchar(50),
	@UserId int
AS
DECLARE @RoleId int
set @RoleId = (SELECT RoleID FROM web_Roles w_R WHERE w_R.RoleName = @RoleName)
	INSERT INTO web_UserRoles(UserID,RoleID) VALUES (@UserId,@RoleId)
GO
---------------------DELETE USER--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[DeleteUser]
@userName nvarchar(50)
AS
BEGIN
DELETE FROM web_UserRoles WHERE web_UserRoles.UserID IN 
(SELECT UserID FROM web_Users w WHERE w.UserName = @userName )

DELETE FROM web_Users WHERE web_Users.UserName = @userName 
END
GO
---------------------UPDATE USER--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[UpdateUser]
@userName nvarchar(50),
@email nvarchar(max),
@password nvarchar(max)
AS
BEGIN
UPDATE
    web_Users
SET
    Email = @email,
    Password= @password
FROM web_Users w
WHERE w.UserName = @userName
END
GO
---------------------UPDATE PRISONER--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[UpdatePrisoner]
@Prisoner_ID int,
@FirstName nvarchar(50),
@SurName nvarchar(50),
@LastName nvarchar(50),
@PlaceOfWork nvarchar(50),
@BirthDate date,
@Photo nvarchar(max),
@Address nvarchar(max),
@AdditionalInformation nvarchar(max),
@RelationshipStatus nvarchar(50)
AS
BEGIN
UPDATE
    Prisoners
SET
    FirstName = @FirstName,
    LastName= @LastName,
	Surname = @SurName,
	PlaceOfWork = @PlaceOfWork,
	BirthDate = @BirthDate,
	Photo = @Photo,
	Address = @Address,
	AdditionalInformation = @AdditionalInformation,
	RelationshipStatus = @RelationshipStatus
FROM Prisoners p
WHERE p.PrisonerId = @Prisoner_ID
END
---------------------DELETE PRISONER--------------------------------------
----------------------------------------------------------------------
GO
CREATE PROC [dbo].[DeletePrisoner]
@Priosner_ID int
AS
BEGIN
DELETE FROM PhoneNumbers WHERE PrisonerID = @Priosner_ID
DELETE FROM Prisoners WHERE PrisonerId = @Priosner_ID 
END