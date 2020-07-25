CREATE TABLE [StriveCarSalon].[tblJobDetail] (
    [JobDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [JobId]       INT                NULL,
    [BayId]       INT                NULL,
    [SalesRep]    INT                NULL,
    [QABy]        INT                NULL,
    [Labour]      INT                NULL,
    [Review]      INT                NULL,
    [ReviewNote]  VARCHAR (50)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL
);

