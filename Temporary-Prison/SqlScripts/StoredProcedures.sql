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
---------------------GetPrisonerById--------------------------
---------------------------------------------------------------
GO
CREATE PROCEDURE [dbo].[GetPrisonerById]
	@ID int
AS
SELECT * FROM Prisoners p WHERE  p.PrisonerId = @ID

SELECT PhoneNumber FROM PhoneNumbers ph, Prisoners pr
WHERE ph.PrisonerID = pr.PrisonerId AND ph.PrisonerID = @ID
---------------------GetPrisoners------------------------------
---------------------------------------------------------------
GO
CREATE PROC [dbo].[GetPrisoners]
AS
SELECT * FROM Prisoners p
GO
---------------------GetRoles----------------------------------
---------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetRoles]
@userName nvarchar(50)
AS
SELECT w_R.RoleName FROM web_UserRoles w_UR, web_Roles w_R, web_Users w_U 
WHERE w_UR.RoleID = w_R.RoleID AND w_UR.UserID = w_U.UserID AND w_U.UserName = @userName
GO
---------------------GetUserByName----------------------------------
-------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetUserByName]
	@userName nvarchar(50)
AS
  SELECT * FROM  web_Users u WHERE  u.UserName = @userName