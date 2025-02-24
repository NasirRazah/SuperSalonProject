Alter Table Category add WooCommID int null default 0;
GO
if exists (select * from Category)
begin
update Category set WooCommID = 0;
end
GO
Alter Table DiscountMaster add WooCommID int null default 0;
GO
if exists (select * from DiscountMaster)
begin
update DiscountMaster set WooCommID = 0;
end
GO
Alter Table TaxHeader add WooCommID int null default 0;
GO
if exists (select * from TaxHeader)
begin
update TaxHeader set WooCommID = 0;
end
GO
Alter Table Customer add WooCommID int null default 0;
GO
if exists (select * from Customer)
begin
update Customer set WooCommID = 0;
end
GO
Alter Table Invoice add WooCommID int null default 0;
GO
if exists (select * from Invoice)
begin
update Invoice set WooCommID = 0;
end
GO
Alter Table Product add WooCommID int null default 0;
GO
Alter Table Product add WooCommImageID int null default 0;
GO
Alter Table Product add WooCommImageExportFlag char(1) null default 'N';
GO
if exists (select * from Product)
begin
update Product set WooCommID = 0, WooCommImageID = 0, WooCommImageExportFlag = 'N';
end
GO
Alter Table Matrix add WooCommID int null default 0;
GO
Alter Table Matrix add WooCommVariantID int null default 0;
GO
if exists (select * from Matrix)
begin
update Matrix set WooCommID = 0, WooCommVariantID = 0;
end
GO