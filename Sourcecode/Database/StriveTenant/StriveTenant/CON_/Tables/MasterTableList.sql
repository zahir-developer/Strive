CREATE TABLE [CON].[MasterTableList] (
    [MasterTableListID]   INT           IDENTITY (1, 1) NOT NULL,
    [MasterTableListNAME] VARCHAR (250) NULL,
    [IS0thRecord]         BIT           NULL,
    [ISIdentityInsert]    BIT           NULL,
    [ISActive]            BIT           NULL,
    [CreatedDate]         DATETIME      NULL,
    CONSTRAINT [PK_MasterTableList_MasterTableListID] PRIMARY KEY CLUSTERED ([MasterTableListID] ASC)
);

