CREATE TABLE [StriveCarSalon].[tblEmployeeAddress] (
    [EmployeeAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]        INT                NULL,
    [Address1]          VARCHAR (50)       NULL,
    [Address2]          VARCHAR (50)       NULL,
    [PhoneNumber]       VARCHAR (50)       NULL,
    [PhoneNumber2]      VARCHAR (50)       NULL,
    [Email]             VARCHAR (50)       NULL,
    [City]              INT                NULL,
    [State]             INT                NULL,
    [Zip]               VARCHAR (10)       NULL,
    [Country]           INT                NULL,
    [IsActive]          BIT                NULL,
    [IsDeleted]         BIT                NULL,
    [CreatedBy]         INT                NULL,
    [CreatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         INT                NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL,
    [IsNotified]        BIT                NULL,
    CONSTRAINT [PK_tblEmployeeAddress] PRIMARY KEY CLUSTERED ([EmployeeAddressId] ASC),
    CONSTRAINT [FK_tblEmployeeAddress_City] FOREIGN KEY ([City]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeAddress_Country] FOREIGN KEY ([Country]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeAddress_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeAddress_State] FOREIGN KEY ([State]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);




GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeAddress_EmployeeId]
    ON [StriveCarSalon].[tblEmployeeAddress]([EmployeeId] ASC);

