CREATE TABLE [StriveLimoSalon].[tblCodeValue] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [CategoryId]     INT           NOT NULL,
    [CodeValue]      VARCHAR (100) NULL,
    [Description]    VARCHAR (100) NULL,
    [CodeShortValue] VARCHAR (10)  NULL,
    [CreatedBy]      INT           NULL,
    [CreatedDate]    DATETIME      NULL,
    [UpdatedBy]      INT           NULL,
    [UpdatedDate]    DATETIME      NULL,
    [ParentId]       INT           NULL,
    [SortOrder]      INT           NULL,
    CONSTRAINT [PK_tblCodeValue] PRIMARY KEY CLUSTERED ([id] ASC)
);

