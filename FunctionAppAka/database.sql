use master
go

Create database AurzeFunctionAppTestDb
go

use AurzeFunctionAppTestDb
go

Create Table VibOrder(
  PurchaseOrderNumber nvarchar (50) primary key,
  BillingZipCode nvarchar(20) not null ,
  BuyerName nvarchar(50) not null ,
  OrderAmount decimal(8,2) not null
)

go


insert into VibOrder values(
	'test user aka 01',
	'aka01',
	'aka',
	25.5
);

go
select * from VibOrder;
