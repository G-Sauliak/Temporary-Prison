BEGIN TRAN

CREATE TABLE [dbo].[Prisoners] (
    [PrisonerId]            INT            IDENTITY (1, 1) NOT NULL,
    [RelationshipStatus]    NVARCHAR (20)  NOT NULL,
    [PlaceOfWork]           NVARCHAR (55)  NOT NULL,
    [BirthDate]             DATE           NOT NULL,
    [Photo]                 NVARCHAR (MAX) NULL,
    [AdditionalInformation] NVARCHAR (MAX) NOT NULL,
    [FirstName]             NVARCHAR (30)  NOT NULL,
    [Surname]               NVARCHAR (30)  NOT NULL,
    [LastName]              NVARCHAR (30)  NOT NULL,
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
    [EmployeeID] INT           NOT NULL,
    [Position]   NVARCHAR (50) NULL,
    [FirstName]  NVARCHAR (30) NULL,
    [Surname]    NVARCHAR (30) NULL,
    [LastName]   NVARCHAR (30) NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

CREATE TABLE [dbo].[PlaceofLiving] (
    [PrisonerID]      INT           NOT NULL,
    [County]          NVARCHAR (50) NOT NULL,
    [City]            NVARCHAR (50) NOT NULL,
    [Street]          NVARCHAR (50) NOT NULL,
    [HouseNumber]     INT           NOT NULL,
    [ApartmentNumber] INT           NULL,
    CONSTRAINT [PK_PlaceOfLiving] PRIMARY KEY CLUSTERED ([PrisonerID] ASC),
    CONSTRAINT [FK_PlaceOfLiving_Prisoners] FOREIGN KEY ([PrisonerID]) REFERENCES [dbo].[Prisoners] ([PrisonerId])
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
    [DetainedByEmployeeID]  INT           NULL,
    [DeliveredByEmployeeID] INT           NULL,
    [ReleasedByEmployeeID]  INT           NULL,
    PRIMARY KEY CLUSTERED ([DetentionID] ASC),
    CONSTRAINT [FK_ListOfDetentions_Prisoners] FOREIGN KEY ([PrisonerID]) REFERENCES [dbo].[Prisoners] ([PrisonerId]),
    CONSTRAINT [FK_ListOfDetentions_Employees] FOREIGN KEY ([DetainedByEmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID]),
    CONSTRAINT [FK_ListOfDetentions_Employees1] FOREIGN KEY ([DeliveredByEmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID]),
    CONSTRAINT [FK_ListOfDetentions_Employees2] FOREIGN KEY ([ReleasedByEmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);


IF @@ERROR != 0
BEGIN
	ROLLBACK
END
ELSE
BEGIN
	COMMIT
END