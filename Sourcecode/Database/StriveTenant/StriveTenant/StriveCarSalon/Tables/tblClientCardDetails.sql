CREATE TABLE [StriveCarSalon].[tblClientCardDetails] (
    [Id]           INT                IDENTITY (1, 1) NOT NULL,
    [CardType]     VARCHAR (50)       NULL,
    [CardNumber]   VARCHAR (50)       NULL,
    [ExpiryDate]   VARCHAR (50)       NULL,
    [ClientId]     INT                NULL,
    [VehicleId]    INT                NULL,
    [MembershipId] INT                NULL,
    [IsActive]     BIT                NULL,
    [IsDeleted]    BIT                NULL,
    [CreatedBy]    INT                NULL,
    [CreatedDate]  DATETIMEOFFSET (7) NULL,
    [UpdatedBy]    INT                NULL,
    [UpdatedDate]  DATETIMEOFFSET (7) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    FOREIGN KEY ([MembershipId]) REFERENCES [StriveCarSalon].[tblClientVehicleMembershipDetails] ([ClientMembershipId]),
    FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId])
);

