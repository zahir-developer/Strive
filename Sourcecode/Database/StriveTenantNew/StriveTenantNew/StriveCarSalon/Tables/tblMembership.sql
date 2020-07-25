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
    [UpdatedDate]    DATETIMEOFFSET (7) NULL
);

