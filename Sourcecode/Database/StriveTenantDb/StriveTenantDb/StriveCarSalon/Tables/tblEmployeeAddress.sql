CREATE TABLE [StriveCarSalon].[tblEmployeeAddress] (
    [AddressId]      INT           IDENTITY (1, 1) NOT NULL,
    [RelationshipId] BIGINT        NULL,
    [Address1]       NVARCHAR (50) NULL,
    [Address2]       NVARCHAR (50) NULL,
    [PhoneNumber]    VARCHAR (50)  NULL,
    [PhoneNumber2]   VARCHAR (50)  NULL,
    [Email]          NVARCHAR (50) NULL,
    [City]           INT           NULL,
    [State]          INT           NULL,
    [Zip]            NVARCHAR (50) NULL,
    [IsActive]       BIT           NULL,
    CONSTRAINT [PK_tblEmployeeAddress] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_tblEmployeeAddress_tblEmployee] FOREIGN KEY ([RelationshipId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

