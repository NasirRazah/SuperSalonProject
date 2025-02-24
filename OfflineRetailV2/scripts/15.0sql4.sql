create procedure [dbo].[sp_store_imp_product_kit]
		@PID			int,
		@ItemSKU	varchar(16),
		@ItemCount	int
as
declare @cnt int;
declare @itemid int;
begin
    set @itemid = 0;
	select @itemid = ID from product where SKU = @ItemSKU;
    set @cnt = 0;
    select @cnt = count(*) from Kit where KitID = @PID and ItemID = @itemid;
    
    if @cnt = 0 begin
      insert into Kit( KitID,ItemID,ItemCount,CreatedBy,CreatedOn, LastChangedBy, LastChangedOn) 
      values ( @PID,@itemid,@ItemCount, 0,getdate(),0,getdate());
    end
    
      
    if @cnt = 1 begin
    
      
      update Kit set ItemCount=@ItemCount,
      LastChangedBy = 0, LastChangedOn = getdate() where KitID = @PID and ItemID = @itemid;
    
    end
end


GO