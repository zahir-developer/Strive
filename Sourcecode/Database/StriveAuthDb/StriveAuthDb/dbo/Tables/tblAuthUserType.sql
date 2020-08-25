CREATE TABLE [dbo].[tblAuthUserType] (
    [UserTypeId]  INT          IDENTITY (1, 1) NOT NULL,
    [UserType]    VARCHAR (50) NULL,
    [TenantId]    INT          NULL,
    [IsAdmin]     BIT          NULL,
    [CreatedDate] DATETIME     NULL,
    CONSTRAINT [PK_tblAuthUserType] PRIMARY KEY CLUSTERED ([UserTypeId] ASC),
    CONSTRAINT [FK_tblAuthUserType_tblTenantDetail] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[tblTenantDetail] ([TenantId])
);

