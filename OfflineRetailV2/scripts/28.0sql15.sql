CREATE procedure [dbo].[sp_AddAutoDataForQuickBooksWindows]

as
declare @taxrate numeric(15,3);
declare @vid		varchar(10);
declare @MaxID		int;
begin

  insert into TaxHeader(TaxType,TaxName,TaxRate,Active) values(0,'QB-XEPOS Zero Tax',0.00,'No');

  set @taxrate = (select top(1) isnull(TaxRate,0) from TaxHeader where Active = 'Yes');

  

  insert into TaxHeader(TaxType,TaxName,TaxRate,Active) values(0,'QB-XEPOS Non Zero Tax',@taxrate,'No');

  set @vid = '';
  select @MaxID = count(*) + 1 from Vendor;
  set @vid = 'QBW-' + cast(@MaxID as varchar(10));

  insert into Vendor (VendorID,[Name],
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
  values(@vid,'QB-XEPOS Vendor',
  0, getdate(), 0, getdate())

  update QuickBooksInfo set DefaultVendorName = 'QB-XEPOS Vendor', SalesTaxZero = 'QB-XEPOS Zero Tax',
  SalesTaxNonZero = 'QB-XEPOS Non Zero Tax';

end
GO