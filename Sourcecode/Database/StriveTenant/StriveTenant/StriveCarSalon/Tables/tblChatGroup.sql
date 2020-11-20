CREATE TABLE [StriveCarSalon].[tblChatGroup] (
    [ChatGroupId] INT                IDENTITY (1, 1) NOT NULL,
    [GroupId]     NVARCHAR (50)      NULL,
    [GroupName]   VARCHAR (20)       NULL,
    [Comments]    VARCHAR (20)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblChatGroup] PRIMARY KEY CLUSTERED ([ChatGroupId] ASC)
);



