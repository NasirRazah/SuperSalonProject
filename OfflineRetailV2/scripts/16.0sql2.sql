create procedure sp_get_XeConnectID
					@ReturnVal	int output
as
begin
  begin transaction
  set @ReturnVal = 0;
  insert into XeConnectLog(HitOn)values(getdate())
  select @ReturnVal = @@IDENTITY
  commit
  return 0
end