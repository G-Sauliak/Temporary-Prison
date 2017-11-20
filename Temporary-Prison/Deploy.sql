CREATE TABLE [dbo].[PrisonerProfile] (
    [UserId]              INT IDENTITY (1, 1) NOT NULL,
    [FirstName]           NVARCHAR (30)  NOT NULL,
    [LastName]            NVARCHAR (30)  NULL,
    [Patronymic]          NVARCHAR (30)  NULL,
    [RelationshipStatus]  NVARCHAR (20)  NULL,
    [PlaceOfWork]         NVARCHAR (55)  NULL,
    [PhoneNumber]         NVARCHAR (55)  NULL,
    [BirthDate]           DATE           NULL,
    [Avatar]              NVARCHAR (MAX) NULL,
    [PlaceOfResidence]    NVARCHAR (55)  NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);