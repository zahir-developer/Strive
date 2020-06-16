CREATE TABLE [Cus].[tblServiceType] (
    [ServiceTypeId]          INT          IDENTITY (1, 1) NOT NULL,
    [ServiceTypeDescription] VARCHAR (32) NOT NULL,
    [IsActive]               BIT          NOT NULL,
    [CreatedDate]            DATETIME     CONSTRAINT [DF_tblServiceType_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblServiceType_ServiceTypeId] PRIMARY KEY CLUSTERED ([ServiceTypeId] ASC)
);

