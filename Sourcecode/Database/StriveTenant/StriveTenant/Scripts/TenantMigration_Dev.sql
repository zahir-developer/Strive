
    --state
	update StriveCarSalon.tblCodeValue set IsDeleted=1 where description ='__'
	--ClientyType

	select * from tblCodeValue WHERE  CodeValue like'monthly'
	declare @CategoryId int =(select id from StriveCarSalon.tblCodeCategory where Category='clienttype' )
	update StriveCarSalon.tblCodeValue
	set IsDeleted=1 
	where CodeValue='Monthly' AND CategoryId =@CategoryId


	--Disable unwanted Service Types

	DECLARE @ServiceType_CategroyId INT =(Select top 1 id from StriveCarSalon.tblCodeCategory where Category = 'ServiceType')
	
	Update StriveCarSalon.tblCodeValue SET IsDeleted = 1 where 
	(codevalue like 'Wash Upcharges (%)' OR
	codevalue like 'Detail Upcharges (%)' OR
	codevalue IN ('V.I.P. Cards','In-House Coupons', 'Free Washes') OR
	codevalue IN ('Deli Items')
	)

	and CategoryId = @ServiceType_CategroyId 

	Select * from StriveCarSalon.tblcodevalue where codevalue like 'Detail Upcharges (%)'

	Select * from StriveCarSalon.tblRoleMaster

	Select * from StriveCarSalon.tblEmployee e 
	JOIN StriveCarSalon.tblEmployeeRole er on e.EmployeeId = er.EmployeeId
	where er.RoleId = 5

	delete from StriveCarSalon.tblTimeClock where TimeClockId = 0
	delete from StriveCarSalon.tblGiftCard where GiftCardId = 0


--Sprint 10
--Change Code Value - Details to Detail Package
DECLARE @CategoryId INT = (Select top 1 id from StriveCarSalon.tblCodeCategory where Category = 'ServiceType')
DECLARE @DetailPackage VARCHAR(15) = 'Detail Package';
DECLARE @Detail VARCHAR(10) = 'Details';

UPDATE StrivecarSalon.tblCodeValue SET CodeValue = @DetailPackage, [Description] = @DetailPackage where CodeValue = @Detail and CategoryId = @CategoryId



