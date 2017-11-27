CREATE TABLE [dbo].[Prisoners] (
    [PrisonerId]             INT IDENTITY (1, 1) NOT NULL,
    [FirstName]              NVARCHAR (30)  NOT NULL,
    [LastName]               NVARCHAR (30)  NULL,
    [Patronymic]             NVARCHAR (30)  NULL,
    [RelationshipStatus]     NVARCHAR (20)  NULL,
    [PlaceOfWork]            NVARCHAR (55)  NULL,
    [PhoneNumberID]          INT  NULL,
    [BirthDate]              DATE           NULL,
    [Photo]                  NVARCHAR (MAX) NULL,
    [ListOfDetentionsID]     INT  NOT NULL,
    [PlaceOfResidence]       NVARCHAR (55)  NULL,
    [AdditionalInformation]  NVARCHAR (Max) NULL,
    PRIMARY KEY CLUSTERED ([PrisonerId] ASC)
);