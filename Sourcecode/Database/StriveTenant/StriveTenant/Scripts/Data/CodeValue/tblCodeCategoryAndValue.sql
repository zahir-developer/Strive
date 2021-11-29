
----insert service category in category table 
insert into [StriveCarSalon].[tblCodeCategory]  (category,isDeleted) values ('ServiceCategory',0)

DECLARE @ServiceCategoryId INT =( Select top 1 id from [StriveCarSalon].tblCodeCategory where Category = 'ServiceCategory' )

----insert service category A,B,C,D,E,F(CodeValue) codevalue table 
Insert into [StriveCarSalon].[tblCodeValue] ( codeValue,Description, CategoryId) 
values
('A','Category A',@ServiceCategoryId), 
('B','Category B',@ServiceCategoryId), 
('C','Category C',@ServiceCategoryId),
('D','Category D',@ServiceCategoryId), 
('E','Category E',@ServiceCategoryId), 
('F','Category F',@ServiceCategoryId)



