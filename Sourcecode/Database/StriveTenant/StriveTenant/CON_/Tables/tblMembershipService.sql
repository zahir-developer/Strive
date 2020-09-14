CREATE TABLE [CON].[tblMembershipService] (
    [MembershipServiceId] INT                IDENTITY (1, 1) NOT NULL,
    [MembershipId]        INT                NULL,
    [ServiceId]           INT                NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblMembershipService] PRIMARY KEY CLUSTERED ([MembershipServiceId] ASC),
    CONSTRAINT [FK_tblMembershipService_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [CON].[tblService] ([ServiceId]),
    CONSTRAINT [FK_tblMembershipService_tblMembership] FOREIGN KEY ([MembershipId]) REFERENCES [CON].[tblMembership] ([MembershipId])
);

