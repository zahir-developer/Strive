
DECLARE @LocationId INT;

DECLARE @BayId INT 
DECLARE cur_new CURSOR FOR 

 Select tblb.BayId from StriveCarSalon.tblBay tblb
JOIN StriveCarSalon.tblLocation loc on loc.LocationId = tblb.locationId
LEFT JOIN StriveCarSalon.tblBaySlot bs on bs.BayId = tblb.BayId
where loc.IsDeleted = 0 and BaySlotId is NULL

OPEN cur_new 

FETCH next FROM cur_new INTO @BayId

WHILE @@FETCH_STATUS = 0 
  BEGIN 
      
	  
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'07:00:00' AS Time), 1, 1, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'07:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'07:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'08:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'08:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'09:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'09:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'10:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'10:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'11:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'11:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'12:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'12:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'13:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'13:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'14:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'14:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'15:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'15:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'16:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'16:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'17:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'17:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'18:00:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)
INSERT [StriveCarSalon].[tblBaySlot] ([BayId], [Slot], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (@BayId, CAST(N'18:30:00' AS Time), 1, 0, NULL, NULL, NULL, NULL)

      FETCH next FROM cur_new INTO @BayId
  END 

CLOSE cur_new 

DEALLOCATE cur_new
      
	  