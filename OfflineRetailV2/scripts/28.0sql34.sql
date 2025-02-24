GO
Alter Table Customer Add XeroID varchar(100) null;
GO
Alter Table Product Add XeroID varchar(100) null;
GO
Alter Table TaxHeader Add XeroID varchar(100) null;
GO
Alter Table Invoice Add XeroID varchar(100) null;
GO
Alter Table Customer Add XeroName nvarchar(200) null;
GO
Alter Table TaxHeader Add XeroTaxType varchar(20) null;
GO
