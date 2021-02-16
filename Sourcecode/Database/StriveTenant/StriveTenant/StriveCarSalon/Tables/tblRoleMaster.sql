CREATE TABLE [StriveCarSalon].[tblRoleMaster] (
    [RoleMasterId] INT                IDENTITY (1, 1) NOT NULL,
    [RoleName]     VARCHAR (50)       NULL,
    [RoleAlias]    VARCHAR (4)        NULL,
    [ParentId]     INT                NULL,
    [IsActive]     BIT                NULL,
    [IsDeleted]    BIT                NULL,
    [CreatedBy]    INT                NULL,
    [CreatedDate]  DATETIMEOFFSET (7) NULL,
    [UpdatedBy]    INT                NULL,
    [UpdatedDate]  DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblRoleMaster] PRIMARY KEY CLUSTERED ([RoleMasterId] ASC)
);



