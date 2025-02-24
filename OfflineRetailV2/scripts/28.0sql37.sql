CREATE procedure [dbo].[sp_AddXeroTax]
					@taxname		varchar(20),
					@taxrate		numeric(15,3),
					@taxtype		varchar(20),
					@ReturnID		int output
as 

begin

  set @ReturnID = 0;

  insert into TaxHeader(TaxName,TaxRate,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,XeroID, XeroTaxType)
  values(@taxname, @taxrate, 0, getdate(), 0, getdate(),'0000-0000',@taxtype)
  select @ReturnID = @@IDENTITY

end