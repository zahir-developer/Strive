CREATE TABLE [StriveCarSalon].[tblClientVehicleMembershipPaymentDetails] (
    [PaymentId]          INT                IDENTITY (1, 1) NOT NULL,
    [ClientMembershipId] INT                NULL,
    [FailedAttempts]     INT                NULL,
    [LastPaymentDate]    DATETIME           NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL
);

