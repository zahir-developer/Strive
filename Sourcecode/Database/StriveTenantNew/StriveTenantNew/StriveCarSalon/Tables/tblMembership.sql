CREATE TABLE [StriveCarSalon].[tblMembership] (
    [MembershipId]   INT                IDENTITY (1, 1) NOT NULL,
    [MembershipName] VARCHAR (50)       NULL,
    [ServiceId]      INT                NULL,
    [LocationId]     INT                NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblMembership] PRIMARY KEY CLUSTERED ([MembershipId] ASC),
    CONSTRAINT [FK_tblMembership_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblMembership_tblService] FOREIGN KEY ([ServiceId]) REFERENCES [StriveCarSalon].[tblService] ([ServiceId])
);



