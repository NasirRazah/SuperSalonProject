create procedure [dbo].[sp_store_imp_product_uom]
		@PID			int,
		@Description	varchar(50),
		@PackageCount	int,
		@UnitPrice		numeric(15,3),
		@IsDefault		char(1)
as
declare @cnt int;
begin

    set @cnt = 0;
    select @cnt = count(*) from UOM where productid = @PID and PackageCount = @PackageCount;
    
    if @cnt = 0 begin
      insert into UOM( ProductID,Description,PackageCount,UnitPrice,IsDefault,CreatedBy,CreatedOn, LastChangedBy, LastChangedOn) 
      values ( @PID,@Description,@PackageCount,@UnitPrice,@IsDefault, 0,getdate(),0,getdate());
    end
    
      
    if @cnt = 1 begin
    
      
      update UOM set Description=@Description,UnitPrice=@UnitPrice,IsDefault=@IsDefault,
      LastChangedBy = 0, LastChangedOn = getdate() where productid = @PID and PackageCount = @PackageCount;
    
    end
end


GO