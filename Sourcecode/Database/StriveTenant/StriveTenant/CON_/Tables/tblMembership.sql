CREATE TABLE [CON].[tblMembership] (
    [MembershipId]   INT                IDENTITY (1, 1) NOT NULL,
    [MembershipName] VARCHAR (50)       NULL,
    [LocationId]     INT                NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblMembership] PRIMARY KEY CLUSTERED ([MembershipId] ASC),
    CONSTRAINT [FK_tblMembership_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [CON].[tblLocation] ([LocationId])
);

