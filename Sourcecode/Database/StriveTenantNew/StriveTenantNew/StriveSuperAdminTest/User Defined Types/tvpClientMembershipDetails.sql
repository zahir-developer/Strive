CREATE TYPE [StriveSuperAdminTest].[tvpClientMembershipDetails] AS TABLE (
    [ClientMembershipId] INT            NULL,
    [ClientId]           INT            NOT NULL,
    [LocationId]         INT            NOT NULL,
    [MembershipId]       INT            NOT NULL,
    [StartDate]          DATETIME       NULL,
    [EndDate]            DATETIME       NULL,
    [Status]             BIT            NULL,
    [Notes]              NVARCHAR (200) NULL,
    [CreatedDate]        DATETIME       NULL);

