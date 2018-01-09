
USE PRISONDB
GO
---------------------Add Phone---------------------------------
---------------------------------------------------------------
CREATE PROCEDURE [dbo].[AddPhoneNumber]
	@PhoneNumber nvarchar(50),
	@PrisonerId int
AS
	INSERT INTO PhoneNumbers (PhoneNumber,PrisonerID) VALUES (@PhoneNumber,@PrisonerId)
GO
---------------------InsertPrisoner------------------------------
---------------------------------------------------------------
CREATE PROC [dbo].[InsertPrisoner] 
@dt AS dbo.PrisonerDt READONLY,
@newId int output
AS
BEGIN
 SET NOCOUNT ON
 INSERT INTO dbo.Prisoners(
 RelationshipStatus,PlaceOfWork,BirthDate,Photo,AdditionalInformation,FirstName,Surname,LastName,Address)
 SELECT 
 RelationshipStatus,PlaceOfWork,BirthDate,Photo,AdditionalInformation,FirstName,Surname,LastName,Address FROM @dt
  SET @newId = SCOPE_IDENTITY();
  RETURN;
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
CREATE PROC [dbo].[GetPrisonersToPagedList]
@skip int,
@rowSize int
AS
SELECT PrisonerId,FirstName,LastName,Surname,BirthDate FROM Prisoners p
ORDER BY p.FirstName 
OFFSET @skip ROWS 
FETCH NEXT @rowSize ROWS ONLY;
DECLARE @TotalCount int
SET @Totalcount = (SELECT COUNT(*) FROM Prisoners p)

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
@rowSize int,
@TotalCount int output
AS
BEGIN
SELECT UserName,Email FROM web_Users u
ORDER BY u.UserName 
OFFSET @skip ROWS 
FETCH NEXT @rowSize ROWS ONLY;
SET @Totalcount = (SELECT COUNT(*) FROM web_Users)
END
GO
---------------------GETALLROLES-----------------------------------
-------------------------------------------------------------------
CREATE PROC [dbo].[GetAllRoles]
AS
BEGIN
SELECT RoleName FROM web_Roles
END
GO
---------------------insertUser--------------------------------------
-------------------------------------------------------------------
CREATE PROC [dbo].[insertUser] 
@dt AS dbo.web_UserDt READONLY
AS
BEGIN
 SET NOCOUNT ON
  INSERT INTO web_Users(
	UserName,
	Email,
	Password
	)
 SELECT UserName,Email,Password FROM @dt
END
GO
---------------------DELETE USER--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[DeleteUser]
@userName nvarchar(50)
AS
BEGIN
DELETE FROM web_Users WHERE web_Users.UserName = @userName 
END
GO
---------------------[EditUser]--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[EditUser]
@dt AS dbo.web_UserDt READONLY
AS
BEGIN
UPDATE
    web_Users
SET
    Email = u.Email,
    Password= u.Password
FROM (SELECT userName,Email,Password FROM @dt) u
WHERE
u.UserName = web_Users.UserName
END
GO
---------------------[EditPrisoner]--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[EditPrisoner]
@dt AS dbo.PrisonerDt READONLY
AS
BEGIN
UPDATE
    Prisoners
SET
    FirstName = p.FirstName,
    LastName= p.LastName,
	Surname = p.SurName,
	PlaceOfWork = p.PlaceOfWork,
	BirthDate = p.BirthDate,
	Photo = p.Photo,
	Address = p.Address,
	AdditionalInformation = p.AdditionalInformation,
	RelationshipStatus = p.RelationshipStatus
FROM (SELECT PrisonerId,FirstName,Surname,LastName,RelationshipStatus,PlaceOfWork,BirthDate,AdditionalInformation,Photo,Address FROM @dt) p
WHERE 
p.PrisonerId = Prisoners.PrisonerId

DELETE FROM PhoneNumbers WHERE PhoneNumbers.PrisonerID = p.PrisonerId
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
---------------------search prisoners by name--------------------------------------
----------------------------------------------------------------------
GO
CREATE PROC [dbo].[FindPrisonersByName]
@search nvarchar(100)
AS
BEGIN
SELECT PrisonerId,FirstName,LastName,Surname,BirthDate FROM Prisoners p
WHERE p.FirstName LIKE  CONCAT('%',@search) OR p.LastName LIKE  CONCAT('%',@search)
END
GO
---------------------ADD TO ROLE--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[AddToRole]
@UserName nvarchar(50),
@RoleName nvarchar(50)
AS
BEGIN
DECLARE @userID int;
DECLARE @RoleID int;
SET @userID = (SELECT UserID FROM web_Users WHERE UserName = @UserName)
SET @RoleID = (SELECT RoleID FROM web_Roles WHERE RoleName = @RoleName )
    INSERT INTO web_UserRoles(
	RoleID,
	UserID
	)
	VALUES(
	@RoleID,
	@UserID
	)
END
GO
---------------------DELETE FROM ROLES--------------------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[DeleteFromRoles]
@userName nvarchar(50),
@roleName nvarchar(50)
AS
BEGIN
declare @userid int;
declare @roleid int;
SET @userid = (SELECT UserID FROM web_Users WHERE UserName = @UserName)
SET @roleid = (SELECT RoleID FROM web_Roles WHERE RoleName = @RoleName )
DELETE FROM web_UserRoles WHERE web_UserRoles.UserID = @userid and web_UserRoles.RoleID = @roleid
END
go
---------------------GET PHONE NUMBERS--------------------------------------
----------------------------------------------------------------------
CREATE PROC GetPhoneNumbers
@prisonerId int
AS
BEGIN
SELECT PhoneNumber FROM PhoneNumbers ph, Prisoners pr
WHERE ph.PrisonerID = pr.PrisonerId AND ph.PrisonerID = @prisonerId
END
---------------------INSERT PRISONER----------------------------------
----------------------------------------------------------------------
GO
CREATE PROC [dbo].[InsertPrisoner] 
@dt AS dbo.PrisonerDt READONLY,
@newId int output
AS
BEGIN
 SET NOCOUNT ON
 INSERT INTO dbo.Prisoners(
 RelationshipStatus,PlaceOfWork,BirthDate,Photo,AdditionalInformation,FirstName,Surname,LastName,Address)
 SELECT 
 RelationshipStatus,PlaceOfWork,BirthDate,Photo,AdditionalInformation,FirstName,Surname,LastName,Address FROM @dt
  SET @newId = SCOPE_IDENTITY();
  RETURN;
END
-------------------- [InsertPhoneNumbers]-----------------------------
----------------------------------------------------------------------
GO
CREATE PROC [dbo].[InsertPhoneNumbers] 
@dt AS dbo.PhoneNumbersDt READONLY
AS
BEGIN
 SET NOCOUNT ON
 INSERT dbo.PhoneNumbers(PrisonerID,PhoneNumber)
 SELECT *
 FROM @dt
 RETURN;
END
-------------------- [EditPrisoner]-----------------------------
----------------------------------------------------------------------
GO
CREATE PROC [dbo].[EditPrisoner]
@dt AS dbo.PrisonerDt READONLY
AS
BEGIN
UPDATE
    Prisoners
SET
    FirstName = p.FirstName,
    LastName= p.LastName,
	Surname = p.SurName,
	PlaceOfWork = p.PlaceOfWork,
	BirthDate = p.BirthDate,
	Photo = p.Photo,
	Address = p.Address,
	AdditionalInformation = p.AdditionalInformation,
	RelationshipStatus = p.RelationshipStatus
FROM (SELECT PrisonerId,FirstName,Surname,LastName,RelationshipStatus,PlaceOfWork,BirthDate,AdditionalInformation,Photo,Address FROM @dt) p
WHERE 
p.PrisonerId = Prisoners.PrisonerId
END
-------------------- [insertEmployee] --------------------------------
----------------------------------------------------------------------
GO
CREATE PROC [dbo].[insertEmployee] 
@dt AS dbo.EmployeeDt READONLY,
@employeeID int output
AS
BEGIN
DECLARE @id int;

SET @id = (SELECT EmployeeID FROM Employees e WHERE EXISTS 
(SELECT * 
FROM @dt d
WHERE d.FirstName = e.FirstName AND 
d.Surname = e.Surname AND 
d.LastName = e.LastName))

 if @id > 0
  BEGIN
     SET @employeeID = @id
  END
 ELSE
 BEGIN
   INSERT INTO Employees(
	 FirstName,
	 Surname,
	 LastName,
 	 Position)
    SELECT d.FirstName,d.Surname,d.LastName,d.Position 
   FROM @dt d
   SET @employeeID = SCOPE_IDENTITY();
 END
END
GO
-------------------- [InsertDetentionProcedures]----------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[InsertDetentionProcedures] 
@employeeID int,
@DetentionProceduresID int output
AS
BEGIN
    BEGIN 
     INSERT INTO DetentionProcedures(EmployeeID)
      VALUES(@employeeID);
      set @DetentionProceduresID = SCOPE_IDENTITY();
    END
END
GO
-------------------- [InsertDeliveryProcedures]----------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[InsertDeliveryProcedures] 
@employeeID int,
@DeliveryProceduresID int output
AS
BEGIN
     INSERT INTO DeliveryProcedures(EmployeeID)
     VALUES(@employeeID);
     set @DeliveryProceduresID = SCOPE_IDENTITY();
END
--------------------[RegistrationOfDetention]-------------------------
----------------------------------------------------------------------
CREATE PROC [dbo].[RegistrationOfDetention] 
@dt AS dbo.RegistrationOfDetentionDt READONLY
AS
BEGIN
 SET NOCOUNT ON
  INSERT INTO ListOfDetentions(
	PrisonerID,
	DateOfDetention,
	DateOfArrival,
	PlaceofDetention,
	DetentionProceduresID,
	DeliveredProceduresID
	)
 SELECT * FROM @dt
END
--------------------[GetDetentionById]-------------------------
---------------------------------------------------------------
GO
CREATE PROC [dbo].[GetDetentionById]
@DetentionID int
AS
BEGIN
SELECT * FROM ListOfDetentions ld WHERE ld.DetentionID = @DetentionID
END
GO
--------------------[GetEmployeeById]-------------------------
---------------------------------------------------------------
CREATE PROC [dbo].[GetEmployeeById]
@EmployeeID int
AS
BEGIN
SELECT * FROM Employees em WHERE em.EmployeeID = @EmployeeID
END
GO
--------------------[GetEmployeeByExecutionProcedureID]--------------------
---------------------------------------------------------------------------
CREATE PROC GetEmployeeByExecutionProcedureID
@ExecuProcName nvarchar(50),
@ExecProcedureID int
AS
BEGIN
   declare @EmployeeID int;
   set @EmployeeID = 
   CASE 
            WHEN @ExecuProcName = 'Release' 
			    THEN (SELECT rp.EmployeesID FROM ReleaseProcedures rp 
				             WHERE rp.ReleaseProceduresID = @ExecProcedureID)
            WHEN @ExecuProcName = 'Delivery' 
			    THEN (SELECT rp.EmployeeID FROM DeliveryProcedures rp 
				             WHERE rp.DeliveredProceduresID = @ExecProcedureID)
		    WHEN @ExecuProcName = 'Detention' 
			    THEN (SELECT rp.EmployeeID FROM DetentionProcedures rp 
				             WHERE rp.DetentionProceduresID = @ExecProcedureID)
	END  
SELECT e.EmployeeID FROM Employees e WHERE e.EmployeeID = @EmployeeID
END
--------------------[GetEmployeeByExecutionProcedureID]--------------------
---------------------------------------------------------------------------
GO
CREATE PROC [dbo].[GetDetentionsByIdForPagedList]
@PrisonerID int,
@skip int,
@rowSize int,
@TotalCount int output
AS
BEGIN
SELECT DetentionID,DateOfDetention,PlaceofDetention,DateOfRelease FROM [ListOfDetentions] l
WHERE l.PrisonerID = @PrisonerID
ORDER BY l.DetentionID
OFFSET @skip ROWS 
FETCH NEXT @rowSize ROWS ONLY;
SET @Totalcount = (SELECT COUNT(*) FROM [ListOfDetentions] l WHERE l.PrisonerID = @PrisonerID)
END
--------------------[ReleaseOfPrisoner]-----------------------------------
---------------------------------------------------------------------------
GO
CREATE PROC [dbo].[ReleaseOfPrisoner]
@dt AS dbo.ReleaseOfPrisoner READONLY
AS
BEGIN
 SET NOCOUNT ON
  UPDATE
   ListOfDetentions 
	SET
	[DateOfRelease] = l.DateOfRelease,
	[AccruedAmount] = l.AccruedAmount,
	[PaidAmount] = l.PaidAmount,
	[ReleaseProceduresID] = l.ReleaseProceduresID
	FROM (SELECT [DetentionID],[DateOfRelease],[AccruedAmount],[PaidAmount],[ReleaseProceduresID] FROM @dt) l
 WHERE
  ListOfDetentions.DetentionID = l.DetentionID
END
GO
--------------------[RegistrationOfDetention]------------------------------
---------------------------------------------------------------------------
CREATE PROC [dbo].[RegistrationOfDetention] 
@dt AS dbo.RegistrationOfDetentionDt READONLY
AS
BEGIN
 SET NOCOUNT ON
  INSERT INTO ListOfDetentions(
	PrisonerID,
	DateOfDetention,
	DateOfArrival,
	PlaceofDetention,
	DetentionProceduresID,
	DeliveredProceduresID
	)
 SELECT * FROM @dt
END
GO
--------------------[EditDetention]----------------------------------------
---------------------------------------------------------------------------
CREATE PROC [dbo].[EditDetention]
@dt AS dbo.DetentionDt_ READONLY
AS
BEGIN
UPDATE
    ListOfDetentions
SET
    DateOfDetention = p.DateOfDetention,
    DateOfArrival= p.DateOfArrival,
	DateOfRelease = p.DateOfRelease,
	PlaceofDetention = p.PlaceofDetention,
	AccruedAmount = p.AccruedAmount,
	PaidAmount = p.PaidAmount
FROM (SELECT 
         [DetentionID],
		 DateOfDetention,
		 DateOfArrival,
		 DateOfRelease,
		 PlaceofDetention,
		 AccruedAmount,
		 PaidAmount 
		 FROM @dt) p
WHERE 
p.[DetentionID] = ListOfDetentions.[DetentionID]
END
--------------------[EditEmployee]----------------------------------------
---------------------------------------------------------------------------
CREATE PROC [dbo].[EditEmployee]
@dt AS dbo.EmployeeDt READONLY
AS
BEGIN
UPDATE
    Employees
SET
    FirstName = p.FirstName,
    Surname= p.Surname,
	LastName = p.LastName,
	Position = p.Position

FROM (SELECT 
         EmployeeID,
         FirstName,
		 Surname,
		 LastName,
		 Position
		 FROM @dt) p
WHERE 
p.EmployeeID = Employees.EmployeeID
END
GO
--------------------[DeleteDetention]-------------------------------------
---------------------------------------------------------------------------
CREATE PROC [dbo].[DeleteDetention]
@id int
AS
BEGIN
DELETE FROM ListOfDetentions WHERE ListOfDetentions.DetentionID = @id 
END
GO


