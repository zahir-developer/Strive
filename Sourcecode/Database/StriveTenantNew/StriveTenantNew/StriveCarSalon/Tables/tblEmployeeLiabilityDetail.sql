﻿CREATE TABLE [StriveCarSalon].[tblEmployeeLiabilityDetail] (
    [LiabilityDetailId]   INT                IDENTITY (1, 1) NOT NULL,
    [LiabilityId]         INT                NULL,
    [LiabilityDetailType] INT                NOT NULL,
    [Amount]              FLOAT (53)         NULL,
    [PaymentType]         INT                NULL,
    [DocumentPath]        VARCHAR (200)      NULL,
    [Description]         VARCHAR (100)      NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeLiabilityDetail] PRIMARY KEY CLUSTERED ([LiabilityDetailId] ASC),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_tblCodeValue] FOREIGN KEY ([LiabilityDetailType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_tblCodeValue1] FOREIGN KEY ([PaymentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_tblEmployeeLiability] FOREIGN KEY ([LiabilityId]) REFERENCES [StriveCarSalon].[tblEmployeeLiability] ([LiabilityId])
);





