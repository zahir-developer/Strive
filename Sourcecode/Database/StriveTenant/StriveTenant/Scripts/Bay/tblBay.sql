
DECLARE @LocationId INT;

DECLARE cur_new CURSOR FOR 
 
Select loc.locationId from tblLocation loc
LEFT JOIN tblBay b on loc.LocationId = b.locationId
where loc.IsDeleted = 0 and b.bayId is null

OPEN cur_new 

FETCH next FROM cur_new INTO @locationId

WHILE @@FETCH_STATUS = 0 

BEGIN 
      
INSERT [StriveCarSalon].[tblBay] ([LocationId], [BayName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@LocationId, N'Wash Line 1', 1, 0, 1, GETDATE(), 1, CAST(N'2021-01-25T10:36:04.9700000+05:30' AS DateTimeOffset))
INSERT [StriveCarSalon].[tblBay] ([LocationId], [BayName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@LocationId, N'Wash Line 2', 1, 0, 1, GETDATE(), 1, CAST(N'2021-01-25T10:36:04.9700000+05:30' AS DateTimeOffset))
INSERT [StriveCarSalon].[tblBay] ([LocationId], [BayName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@LocationId, N'Wash Line 3', 1, 0, 1, GETDATE(), 1, CAST(N'2021-01-25T10:36:04.9700000+05:30' AS DateTimeOffset))
INSERT [StriveCarSalon].[tblBay] ([LocationId], [BayName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@LocationId, N'Detail Bay 1', 1, 0, 1, GETDATE(), 1, CAST(N'2021-01-25T10:36:04.9700000+05:30' AS DateTimeOffset))
INSERT [StriveCarSalon].[tblBay] ([LocationId], [BayName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@LocationId, N'Detail Bay 2', 1, 0, 1, GETDATE(), 1, CAST(N'2021-01-25T10:36:04.9700000+05:30' AS DateTimeOffset))
INSERT [StriveCarSalon].[tblBay] ([LocationId], [BayName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@LocationId, N'Detail Bay 3', 1, 0, 1, GETDATE(), 1, CAST(N'2021-01-25T10:36:04.9700000+05:30' AS DateTimeOffset))

FETCH next FROM cur_new INTO @LocationId
  END 

CLOSE cur_new 

DEALLOCATE cur_new

GO