use [master];
go
create database MainDb;
go
use MainDb;
go
create table CurrencyExchange
(
  ExchangeId int not null identity(1,1),
  CurrencyCode varchar(3) not null,
  ExchangeRate decimal(10,2) not null ,
  DateExchange datetime not null ,
  constraint ck_date_exchange check(DateExchange <= sysdatetime()),
  constraint pk_exchange_id primary key(ExchangeId)
)
go




