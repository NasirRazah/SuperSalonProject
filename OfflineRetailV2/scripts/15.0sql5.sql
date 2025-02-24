create procedure [dbo].[sp_store_imp_product_serial]
		@PID			int,
		@AllowPOSNew	char(1),
		@Serial1		varchar(30),
		@Serial2		varchar(30),
		@Serial3		varchar(30)
		
as
declare @cntH int;
declare @cnt int;
declare @HID int;
begin
    set @HID = 0;
    set @cntH = 0;
    select @cntH = count(*) from SerialHeader where ProductID = @PID;

	if @cntH = 0 begin
	  insert into SerialHeader( ProductID,AllowPOSNew,CreatedBy,CreatedOn, LastChangedBy, LastChangedOn) 
      values ( @PID,@AllowPOSNew, 0,getdate(),0,getdate())
      select @HID = @@identity

	  set @cnt = 0;
    select @cnt = count(*) from SerialDetail where SerialHeaderID = @HID and Serial1=@Serial1;
    
    if @cnt = 0 begin
      insert into SerialDetail( SerialHeaderID,Serial1,Serial2,Serial3,CreatedBy,CreatedOn, LastChangedBy, LastChangedOn) 
      values ( @HID,@Serial1,@Serial2,@Serial3, 0,getdate(),0,getdate());
      
    end
    
      
    if @cnt = 1 begin
    
      
      update SerialDetail set Serial2=@Serial2,Serial3=@Serial3,
      LastChangedBy = 0, LastChangedOn = getdate() where SerialHeaderID = @HID and Serial1=@Serial1;
    
    end

	end

	if @cntH = 1 begin
	   select @HID = id from SerialHeader where productid = @PID;
	   set @cnt = 0;
    select @cnt = count(*) from SerialDetail where SerialHeaderID = @HID and Serial1=@Serial1;
    
    if @cnt = 0 begin
      insert into SerialDetail( SerialHeaderID,Serial1,Serial2,Serial3,CreatedBy,CreatedOn, LastChangedBy, LastChangedOn) 
      values ( @HID,@Serial1,@Serial2,@Serial3, 0,getdate(),0,getdate());
      
    end
    
      
    if @cnt = 1 begin
    
      
      update SerialDetail set Serial2=@Serial2,Serial3=@Serial3,
      LastChangedBy = 0, LastChangedOn = getdate() where SerialHeaderID = @HID and Serial1=@Serial1;
    
    end
	end

    
end


GO