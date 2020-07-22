CREATE TABLE [StriveCarSalon].[tblDrawer] (
    [DrawerId]    INT          IDENTITY (1, 1) NOT NULL,
    [DrawerName]  VARCHAR (10) NULL,
    [LocationId]  INT          NULL,
    [CreatedDate] DATETIME     NULL,
    CONSTRAINT [PK_tblDrawer] PRIMARY KEY CLUSTERED ([DrawerId] ASC)
);

