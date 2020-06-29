CREATE TABLE [StriveCarSalon].[tblMembership] (
    [MembershipId]   INT           IDENTITY (1, 1) NOT NULL,
    [MembershipName] NVARCHAR (50) NULL,
    [ServiceId]      INT           NULL,
    [LocationId]     INT           NULL,
    [IsActive]       BIT           NULL,
    [DateCreated]    DATETIME      NULL,
    CONSTRAINT [PK_tblMembership] PRIMARY KEY CLUSTERED ([MembershipId] ASC),
    CONSTRAINT [FK_tblMembership_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblMembership_tblService] FOREIGN KEY ([ServiceId]) REFERENCES [StriveCarSalon].[tblService] ([ServiceId])
);

