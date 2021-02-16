CREATE TABLE [StriveCarSalon].[tblCodeValue_Dev] (
    [id]             INT                IDENTITY (1, 1) NOT NULL,
    [CategoryId]     INT                NOT NULL,
    [CodeValue]      VARCHAR (100)      NULL,
    [Description]    VARCHAR (100)      NULL,
    [CodeShortValue] VARCHAR (10)       NULL,
    [ParentId]       INT                NULL,
    [SortOrder]      INT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL
);

