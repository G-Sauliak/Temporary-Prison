BEGIN TRAN
USE PRISONDB
CREATE TABLE [dbo].[Prisoners] (
    [PrisonerId]            INT            IDENTITY (1, 1) NOT NULL,
    [RelationshipStatus]    NVARCHAR (20)  NOT NULL,
    [PlaceOfWork]           NVARCHAR (55)  NOT NULL,
    [BirthDate]             DATE           NULL,
    [Photo]                 NVARCHAR (MAX) NULL,
    [AdditionalInformation] NVARCHAR (MAX) NOT NULL,
    [FirstName]             NVARCHAR (30)  NOT NULL,
    [Surname]               NVARCHAR (30)  NOT NULL,
    [LastName]              NVARCHAR (30)  NOT NULL,
    [Address]               NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([PrisonerId] ASC)
);

CREATE TABLE [dbo].[PhoneNumbers] (
    [PhoneNumberID] INT           IDENTITY (1, 1) NOT NULL,
    [PrisonerID]    INT           NOT NULL,
    [PhoneNumber]   NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([PhoneNumberID] ASC),
    CONSTRAINT [FK_Phones_Prisoners] FOREIGN KEY ([PrisonerID]) REFERENCES [dbo].[Prisoners] ([PrisonerId])
);

CREATE TABLE [dbo].[Employees] (
    [EmployeeID] INT           IDENTITY (1, 1) NOT NULL,
    [Position]   NVARCHAR (50) NULL,
    [FirstName]  NVARCHAR (30) NULL,
    [Surname]    NVARCHAR (30) NULL,
    [LastName]   NVARCHAR (30) NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);


CREATE TABLE [dbo].[DetentionProcedures] (
    [DetentionID] INT  IDENTITY (1, 1) NOT NULL,
    [EmployeeID]  INT NOT NULL,
    CONSTRAINT [PK_Detention] PRIMARY KEY CLUSTERED ([DetentionID] ASC),
    CONSTRAINT [FK_DetentionProcedures_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

CREATE TABLE [dbo].[DeliveryProcedures] (
    [DeliveredID] INT  IDENTITY (1, 1) NOT NULL,
    [EmployeeID]  INT NOT NULL,
    CONSTRAINT [PK_Delivered] PRIMARY KEY CLUSTERED ([DeliveredID] ASC),
    CONSTRAINT [FK_DeliveryProcedures_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

CREATE TABLE [dbo].[ReleaseProcedures] (
    [ReleaseProceduresID] INT  IDENTITY (1, 1) NOT NULL,
    [EmployeesID]         INT NOT NULL,
    CONSTRAINT [PK_ReleaseProcedures] PRIMARY KEY CLUSTERED ([ReleaseProceduresID] ASC),
    CONSTRAINT [FK_ReleaseProcedures_Employees] FOREIGN KEY ([EmployeesID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

CREATE TABLE [dbo].[ExecutionProcedures] (
    [ExecutionProceduresID] INT  IDENTITY (1, 1) NOT NULL,
    [DeliveryProcedureID]   INT NULL,
    [DetentionProcedureID]  INT NULL,
    [ReleaseProceduresID]   INT NULL,
    CONSTRAINT [PK_ExecutionProcedures] PRIMARY KEY CLUSTERED ([ExecutionProceduresID] ASC),
    CONSTRAINT [FK_ExecutionProcedures_DeliveryProcedures] FOREIGN KEY ([DeliveryProcedureID]) REFERENCES [dbo].[DeliveryProcedures] ([DeliveredID]),
    CONSTRAINT [FK_ExecutionProcedures_DetentionProcedures] FOREIGN KEY ([DetentionProcedureID]) REFERENCES [dbo].[DetentionProcedures] ([DetentionID]),
    CONSTRAINT [FK_ExecutionProcedures_ReleaseProcedures] FOREIGN KEY ([ReleaseProceduresID]) REFERENCES [dbo].[ReleaseProcedures] ([ReleaseProceduresID])
);

CREATE TABLE [dbo].[ListOfDetentions] (
    [DetentionID]           INT           IDENTITY (1, 1) NOT NULL,
    [PrisonerID]            INT           NOT NULL,
    [DateOfDetention]       DATE          NOT NULL,
    [DateOfArrival]         DATE          NULL,
    [DateOfRelease]         DATE          NULL,
    [AccruedAmount]         MONEY         NULL,
    [PaidAmount]            MONEY         NULL,
    [PlaceofDetention]      NVARCHAR (50) NULL,
    [ExecutionProceduresID] INT           NULL,
    PRIMARY KEY CLUSTERED ([DetentionID] ASC),
    CONSTRAINT [FK_ListOfDetentions_ExecutionProcedures] FOREIGN KEY ([ExecutionProceduresID]) REFERENCES [dbo].[ExecutionProcedures] ([ExecutionProceduresID]),
    CONSTRAINT [FK_ListOfDetentions_Prisoners] FOREIGN KEY ([PrisonerID]) REFERENCES [dbo].[Prisoners] ([PrisonerId])
);


CREATE TABLE [dbo].[web_Users] (
    [UserID]   INT  IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (50)  NULL,
    [Password] NVARCHAR (MAX) NULL,
    [Email]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_web_Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

CREATE TABLE [dbo].[web_Roles] (
    [RoleID]   INT  IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_web_Roles] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

CREATE TABLE [dbo].[web_UserRoles] (
    [UserRolesID] INT  IDENTITY (1, 1) NOT NULL,
    [RoleID]      INT NULL,
    [UserID]      INT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserRolesID] ASC),
    CONSTRAINT [FK_web_UserRoles_web_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[web_Users] ([UserID]),
    CONSTRAINT [FK_web_UserRoles_web_Roles] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[web_Roles] ([RoleID])
);

INSERT INTO web_Roles(RoleName)
	VALUES(
	'Admin'
	)
INSERT INTO web_Roles(RoleName)
	VALUES(
	'Editor'
	)
INSERT INTO web_Roles(RoleName)
	VALUES(
	'User'
	)
INSERT INTO web_Users(UserName,Password,Email)
	VALUES(
	'admin','123','ss@qq.com'
	)
INSERT INTO web_UserRoles(UserID,RoleID)
	VALUES(
	1,1
	)
INSERT INTO web_UserRoles(UserID,RoleID)
	VALUES(
	1,2
	)

IF @@ERROR != 0
BEGIN
	ROLLBACK
END
ELSE
BEGIN
	COMMIT
END