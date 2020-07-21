CREATE TABLE [StriveCarSalon].[tblClientMembershipDetails] (
    [ClientMembershipId] INT            IDENTITY (1, 1) NOT NULL,
    [ClientId]           INT            NOT NULL,
    [LocationId]         INT            NOT NULL,
    [MembershipId]       INT            NOT NULL,
    [StartDate]          DATETIME       NULL,
    [EndDate]            DATETIME       NULL,
    [Status]             BIT            NULL,
    [Notes]              NVARCHAR (100) NULL,
    [CreatedDate]        DATETIME       NULL,
    CONSTRAINT [PK_tblClientMembershipDetails] PRIMARY KEY CLUSTERED ([ClientMembershipId] ASC),
    CONSTRAINT [FK_tblClientMembershipDetails_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblClientMembershipDetails_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

