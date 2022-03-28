CREATE TABLE [CON].[tblCodeCategory] (
    [id]          INT                IDENTITY (1, 1) NOT NULL,
    [Category]    VARCHAR (50)       NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblCodeCategory] PRIMARY KEY CLUSTERED ([id] ASC)
);

