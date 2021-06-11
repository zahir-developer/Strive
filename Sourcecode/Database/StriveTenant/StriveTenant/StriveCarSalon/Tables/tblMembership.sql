CREATE TABLE [StriveCarSalon].[tblMembership] (
    [MembershipId]    INT                IDENTITY (1, 1) NOT NULL,
    [MembershipName]  VARCHAR (50)       NULL,
    [LocationId]      INT                NULL,
    [Price]           DECIMAL (19, 2)    NULL,
    [Notes]           NVARCHAR (500)     NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    [REFCustAccID]    INT                NULL,
    [DiscountedPrice] DECIMAL (18, 2)    NULL,
    CONSTRAINT [PK_tblMembership] PRIMARY KEY CLUSTERED ([MembershipId] ASC),
    CONSTRAINT [FK_tblMembership_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





