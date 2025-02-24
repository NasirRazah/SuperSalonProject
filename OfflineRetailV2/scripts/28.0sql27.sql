CREATE procedure [dbo].[sp_AddWooCommTax]
					@id			varchar(20),
					@taxname		varchar(20),
					@taxrate		numeric(15,3),
					@ReturnID		int output
as 

begin

  set @ReturnID = 0;

  insert into TaxHeader(TaxName,TaxRate,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,WooCommID)
  values(@taxname, @taxrate, 0, getdate(), 0, getdate(),@id)
  select @ReturnID = @@IDENTITY

end
			 
GO