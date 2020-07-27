CREATE TABLE [StriveCarSalon].[tblClientMembershipDetails] (
    [ClientMembershipId] INT                IDENTITY (1, 1) NOT NULL,
    [ClientId]           INT                NOT NULL,
    [LocationId]         INT                NOT NULL,
    [MembershipId]       INT                NOT NULL,
    [StartDate]          DATETIMEOFFSET (7) NULL,
    [EndDate]            DATETIMEOFFSET (7) NULL,
    [Status]             BIT                NULL,
    [Notes]              VARCHAR (50)       NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblClientMembershipDetails] PRIMARY KEY CLUSTERED ([ClientMembershipId] ASC),
    CONSTRAINT [FK_tblClientMembershipDetails_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblClientMembershipDetails_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);



