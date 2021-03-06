CREATE TABLE [CON].[tblClientVehicleMembershipDetails] (
    [ClientMembershipId] INT                IDENTITY (1, 1) NOT NULL,
    [ClientVehicleId]    INT                NOT NULL,
    [LocationId]         INT                NOT NULL,
    [MembershipId]       INT                NOT NULL,
    [StartDate]          DATE               NULL,
    [EndDate]            DATE               NULL,
    [Status]             BIT                NULL,
    [Notes]              VARCHAR (50)       NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblClientMembershipDetails] PRIMARY KEY CLUSTERED ([ClientMembershipId] ASC),
    CONSTRAINT [FK_tblClientMembershipDetails_ClientVehicleId] FOREIGN KEY ([ClientVehicleId]) REFERENCES [CON].[tblClientVehicle] ([VehicleId]),
    CONSTRAINT [FK_tblClientMembershipDetails_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [CON].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblClientMembershipDetails_MembershipId] FOREIGN KEY ([MembershipId]) REFERENCES [CON].[tblMembership] ([MembershipId])
);

