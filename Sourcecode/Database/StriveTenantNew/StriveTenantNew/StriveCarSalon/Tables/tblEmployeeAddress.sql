CREATE TABLE [StriveCarSalon].[tblEmployeeAddress] (
    [EmployeeAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]        INT                NULL,
    [Address1]          NVARCHAR (50)      NULL,
    [Address2]          NVARCHAR (50)      NULL,
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
    CONSTRAINT [PK_tblEmployeeAddress] PRIMARY KEY CLUSTERED ([EmployeeAddressId] ASC),
    CONSTRAINT [FK_tblEmployeeAddress_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);


GO
CREATE NONCLUSTERED INDEX [tblemployeeaddress_idx_employeeid]
    ON [StriveCarSalon].[tblEmployeeAddress]([EmployeeId] ASC);

