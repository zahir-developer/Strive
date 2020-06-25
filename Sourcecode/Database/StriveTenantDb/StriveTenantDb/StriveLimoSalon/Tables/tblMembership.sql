CREATE TABLE [StriveLimoSalon].[tblMembership] (
    [MembershipId]   INT           IDENTITY (1, 1) NOT NULL,
    [MembershipName] NVARCHAR (50) NULL,
    [ServiceId]      INT           NULL,
    [LocationId]     INT           NULL,
    [IsActive]       BIT           NULL,
    [DateCreated]    DATETIME      NULL,
    CONSTRAINT [PK_tblMembership] PRIMARY KEY CLUSTERED ([MembershipId] ASC),
    CONSTRAINT [FK_tblMembership_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveLimoSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblMembership_tblService] FOREIGN KEY ([ServiceId]) REFERENCES [StriveLimoSalon].[tblService] ([ServiceId])
);

