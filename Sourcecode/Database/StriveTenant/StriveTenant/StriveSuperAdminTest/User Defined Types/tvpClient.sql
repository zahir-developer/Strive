CREATE TYPE [StriveSuperAdminTest].[tvpClient] AS TABLE (
    [ClientId]      INT            NULL,
    [FirstName]     VARCHAR (50)   NULL,
    [MiddleName]    VARCHAR (50)   NULL,
    [LastName]      VARCHAR (50)   NULL,
    [Gender]        INT            NULL,
    [MaritalStatus] INT            NULL,
    [BirthDate]     DATETIME       NULL,
    [CreatedDate]   DATETIME       NULL,
    [IsActive]      BIT            NULL,
    [Notes]         NVARCHAR (200) NULL,
    [RecNotes]      NVARCHAR (200) NULL,
    [Score]         INT            NULL,
    [NoEmail]       BIT            NULL,
    [ClientType]    INT            NULL);

