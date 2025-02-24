create procedure sp_set_XeConnectInvoice
					@xeid	int,
					@invno	int
as
begin

  begin transaction
 
  update XeConnectLog set InvoiceNo = @invno where ID = @xeid;

  commit
  return 0

end