CREATE TABLE [StriveCarSalon].[tblClientVehicleMembershipDetails] (
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
    [TotalPrice]         DECIMAL (19, 2)    NULL,
    [REFCustAccID]       INT                NULL,
    [IsDiscount]         BIT                NULL,
    [CardNumber]         VARCHAR (30)       NULL,
    [ExpiryDate]         VARCHAR (6)        NULL,
    [ProfileId]          VARCHAR (30)       NULL,
    [AccountId]          VARCHAR (30)       NULL,
    [FailedAttempts]     INT                NULL,
    [IsNotified]         BIT                NULL,
    [DocumentId]         INT                NULL,
    [LastPaymentDate]    DATE               NULL,
    CONSTRAINT [PK_tblClientMembershipDetails] PRIMARY KEY CLUSTERED ([ClientMembershipId] ASC),
    CONSTRAINT [FK_tblClientMembershipDetails_ClientVehicleId] FOREIGN KEY ([ClientVehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId]),
    CONSTRAINT [FK_tblClientMembershipDetails_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblClientMembershipDetails_MembershipId] FOREIGN KEY ([MembershipId]) REFERENCES [StriveCarSalon].[tblMembership] ([MembershipId])
);









