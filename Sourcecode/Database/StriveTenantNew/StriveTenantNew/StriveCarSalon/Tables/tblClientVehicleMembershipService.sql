CREATE TABLE [StriveCarSalon].[tblClientVehicleMembershipService] (
    [ClientVehicleMembershipServiceId] INT                IDENTITY (1, 1) NOT NULL,
    [ClientMembershipId]               INT                NULL,
    [ServiceId]                        INT                NULL,
    [IsActive]                         BIT                NULL,
    [IsDeleted]                        BIT                NULL,
    [CreatedBy]                        INT                NULL,
    [CreatedDate]                      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]                        INT                NULL,
    [UpdatedDate]                      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblClientVehicleMembershipService] PRIMARY KEY CLUSTERED ([ClientVehicleMembershipServiceId] ASC),
    CONSTRAINT [FK_tblClientVehicleMembershipService_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [StriveCarSalon].[tblService] ([ServiceId]),
    CONSTRAINT [FK_tblClientVehicleMembershipService_tblClientMembershipDetails] FOREIGN KEY ([ClientMembershipId]) REFERENCES [StriveCarSalon].[tblClientVehicleMembershipDetails] ([ClientMembershipId])
);



