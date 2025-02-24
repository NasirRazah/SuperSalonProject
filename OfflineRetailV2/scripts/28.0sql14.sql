GO
Alter Table TaxHeader alter column TaxName varchar(30) null;
GO
Alter Table QuickBooksInfo Add DefaultVendorName nvarchar(30) null;
GO
Alter Table QuickBooksInfo Add DefaultVendorQBListID varchar(50) null;
GO
Alter Table QuickBooksInfo Add DefaultVendorQBEditSequenceID varchar(50) null;
GO