CREATE TYPE [CON].[tvpDocument] AS TABLE (
    [DocumentId]   BIGINT        NOT NULL,
    [EmployeeId]   BIGINT        NULL,
    [Filename]     VARCHAR (MAX) NOT NULL,
    [Filepath]     VARCHAR (MAX) NOT NULL,
    [Password]     VARCHAR (MAX) NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [ModifiedDate] DATETIME      NOT NULL,
    [IsActive]     BIT           NOT NULL);

