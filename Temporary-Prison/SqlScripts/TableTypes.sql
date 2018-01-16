USE [PrisonDb]
GO
CREATE TYPE [dbo].[PhoneNumbersDt] AS TABLE(
	[PrisonerId] [int] NULL,
	[PhoneNumber] [nvarchar](100) NULL
)
GO
CREATE TYPE [dbo].[EmployeeDt] AS TABLE(
	[EmployeeID] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Position] [nvarchar](50) NULL
)
GO
CREATE TYPE [dbo].[PrisonerDt] AS TABLE(
	[PrisonerId] [int] NULL,
	[RelationshipStatus] [nvarchar](20) NULL,
	[PlaceOfWork] [nvarchar](55) NULL,
	[BirthDate] [date] NULL,
	[Photo] [nvarchar](max) NULL,
	[AdditionalInformation] [nvarchar](max) NULL,
	[FirstName] [nvarchar](30) NULL,
	[Surname] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[Address] [nvarchar](max) NULL
)
GO

CREATE TYPE [dbo].[RegistrationOfDetentionDt] AS TABLE(
	[PrisonerID] [int] NULL,
	[DateOfDetention] [date] NULL,
	[DateOfArrival] [date] NULL,
	[PlaceofDetention] [nvarchar](max) NULL,
	[DetentionProceduresID] [int] NULL,
	[DeliveredProceduresID] [int] NULL
)
GO
CREATE TYPE [dbo].[web_UserDt] AS TABLE(
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL
)
GO
CREATE TYPE [dbo].[EmployeeDt] AS TABLE(
	[EmployeeID] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Position] [nvarchar](50) NULL
)
GO
CREATE TYPE [dbo].[DetentionDt_] AS TABLE(
	[DetentionID] [int] NOT NULL,
	[PrisonerID] [int] NOT NULL,
	[DateOfDetention] [date] NULL,
	[DateOfArrival] [date] NULL,
	[DateOfRelease] [date] NULL,
	[AccruedAmount] [money] NULL,
	[PaidAmount] [money] NULL,
	[PlaceofDetention] [nvarchar](50) NULL,
	[ReleaseProceduresID] [int] NULL,
	[DetentionProceduresID] [int] NULL,
	[DeliveredProceduresID] [int] NULL
)
GO
USE [PrisonDb]
GO

CREATE TYPE [dbo].[ReleaseOfPrisoner] AS TABLE(
	[DetentionID] [int] NULL,
	[DateOfRelease] [date] NULL,
	[AccruedAmount] [money] NULL,
	[PaidAmount] [money] NULL,
	[ReleaseProceduresID] [int] NULL
)
GO
