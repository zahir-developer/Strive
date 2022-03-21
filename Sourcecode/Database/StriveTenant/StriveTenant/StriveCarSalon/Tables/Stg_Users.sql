﻿CREATE TABLE [StriveCarSalon].[Stg_Users] (
    [LocationID] INT           NOT NULL,
    [UserID]     SMALLINT      NOT NULL,
    [LoginID]    VARCHAR (16)  NOT NULL,
    [Password]   VARCHAR (16)  NULL,
    [Initials]   VARCHAR (5)   NULL,
    [LastName]   VARCHAR (25)  NULL,
    [FirstName]  VARCHAR (25)  NULL,
    [Phone]      VARCHAR (25)  NULL,
    [Fax]        VARCHAR (25)  NULL,
    [Email]      VARCHAR (50)  NULL,
    [Active]     BIT           NOT NULL,
    [CurrentIP]  VARCHAR (15)  NULL,
    [RoleID]     INT           NULL,
    [Address]    VARCHAR (80)  NULL,
    [City]       VARCHAR (50)  NULL,
    [State]      CHAR (2)      NULL,
    [Zip]        CHAR (5)      NULL,
    [PayRate]    NCHAR (10)    NULL,
    [SickRate]   NCHAR (10)    NULL,
    [VacRate]    NCHAR (10)    NULL,
    [ComRate]    NCHAR (10)    NULL,
    [Hire]       DATETIME      NULL,
    [birth]      DATETIME      NULL,
    [Gender]     CHAR (10)     NULL,
    [SSNo]       CHAR (11)     NULL,
    [MStat]      CHAR (1)      NULL,
    [Exempt]     SMALLINT      NULL,
    [Salery]     NCHAR (10)    NULL,
    [LRT]        DATETIME      NULL,
    [TIP]        BIT           NULL,
    [citizen]    TINYINT       NULL,
    [AlienNo]    VARCHAR (50)  NULL,
    [AuthDate]   SMALLDATETIME NULL,
    [Alt_UserID] INT           NULL
);

