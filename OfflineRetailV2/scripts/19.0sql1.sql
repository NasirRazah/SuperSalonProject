GO
alter table setup add Paymentsense_AccountName nvarchar(100) null default '';
GO
alter table setup add Paymentsense_ApiKey nvarchar(100) null default '';
GO
alter table setup add Paymentsense_SoftwareHouseId nvarchar(50) null default '';
GO
alter table setup add Paymentsense_InstallerId nvarchar(50) null default '';
GO
update setup set Paymentsense_AccountName = '',Paymentsense_ApiKey = '', Paymentsense_SoftwareHouseId = '', Paymentsense_InstallerId = '';
GO