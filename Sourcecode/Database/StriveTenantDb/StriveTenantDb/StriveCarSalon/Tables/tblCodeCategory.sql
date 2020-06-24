CREATE TABLE [StriveCarSalon].[tblCodeCategory] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [Category]    VARCHAR (50) NULL,
    [CreatedBy]   INT          NULL,
    [CreatedDate] DATETIME     NULL,
    [UpdatedBy]   INT          NULL,
    [UpdatedDate] DATETIME     NULL,
    CONSTRAINT [PK_tblCodeCategory] PRIMARY KEY CLUSTERED ([id] ASC)
);

