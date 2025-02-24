CREATE procedure [dbo].[sp_AddXEROProduct]
					@maintainstock		char(1),
					@taxtype			varchar(1),
					@title				varchar(150),
					@price				numeric(15,3),
					@cost				numeric(15,3),
					@qty				numeric(15,3),
					@itemtype			varchar(10),
					@itemgroup			varchar(30),
					@producttype		varchar(30),
					@xeroid				varchar(100),
					@TerminalName			nvarchar(50),
					@ReturnID			int output	

as 
declare @sku_txt	varchar(16);
declare	@deptID		int;
declare @CategoryID	int;
declare @VendorID	int;

declare @dCnt		int;
declare @cCnt		int;
declare @vCnt		int;

declare @MaxDisplay  int;
declare @MaxPOSDisplay  int;
declare @docno			nvarchar(50);
declare @sj int;
declare @pId			int;
declare @rout			int;
declare @dt				datetime;

declare @cqty			numeric(15,3);

declare @catidcnt		int;
declare @vendidcnt		int;

declare @catidtxt		varchar(10);
declare @vendidtxt		varchar(10);
declare @iCatDisp int;

declare @grouptype		varchar(10);

declare @menuid		varchar(20);
declare @automenu	char(1);
declare @MaxID	int;

declare @txid	int;
begin

  set @ReturnID = 0;

  set @dCnt = 0;
  set @cCnt = 0;
  set @vCnt = 0;
  set @automenu = 'N';

  set @sku_txt = '';

  select @MaxID = count(*) + 1 from product;
   set @sku_txt = 'XERO' + cast(@MaxID as nvarchar(10));
  
  

  select @dCnt = count(*) from dept where DepartmentID = 'XERODEPT';

  if @dCnt > 0 begin
   select @deptID = ID from Dept where DepartmentID = 'XERODEPT';
  end
  else begin
    insert into Dept (DepartmentID, [Description], CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
	values('XERODEPT', 'XERO DEPARTMENT', 0, getdate(), 0, getdate())
	select @deptID = @@IDENTITY
  end

  select @cCnt = count(*) from Category where CategoryID = 'XEROCAT';
     if @cCnt > 0 
     select @CategoryID = isnull(ID,0) from Category where CategoryID = 'XEROCAT';

     if @cCnt = 0 begin

        select @iCatDisp = isnull(Max(POSDisplayOrder),0) + 1 from Category;

        insert into category(CategoryID,Description,MaxItemsforPOS,POSDisplayOrder,
		POSBackground,POSFontType,POSFontSize,POSFontColor,POSItemFontColor,
		        FoodStampEligible,POSScreenColor,POSScreenStyle,IsBold,IsItalics,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) 
                                    values ( 'XEROCAT','XERO CATEGORY',10,@iCatDisp,
									'Skin','Tahoma',9,'0','0','N',
                                    '0','','N','N',0,getdate(),0,getdate()) 
        select @CategoryID = @@IDENTITY ;

     end





  
  select @MaxPOSDisplay = isnull(Max(POSDisplayOrder),0) + 1 from Product where CategoryID = @CategoryID;




  insert into Product (SKU,[Description],ProductType, DepartmentID,CategoryID,PriceA,QtyOnHand, 
  POSBackground,POSScreenColor,POSScreenStyle,POSFontType,POSFontSize,POSFontColor,POSDisplayOrder,Cost,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,XeroID)
  values(@sku_txt,@title,'P',@deptID,@CategoryID,@price,@qty,
  'Skin','0','','Tahoma',8,'0',@MaxPOSDisplay,@cost,
  0, getdate(), 0, getdate(),@xeroid)
  select @ReturnID = @@IDENTITY

  if @ReturnID > 0 begin
     set @txid = 0;
	 select @txid = id from TaxHeader where XeroTaxType = @taxtype;

	 insert into TaxMapping (TaxID,MappingID,MappingType,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
	 values(@txid,@ReturnID,'Product',0, getdate(), 0, getdate());
    
	 select  @cqty = qtyonhand from Product where ID = @ReturnID; 
	 set @sj = 0;
	 set @dt = getdate();
	 set @DocNo = 'New Product from XERO/' + @sku_txt;
	 exec @sj = sp_AddJournal @DocNo, @dt, @ReturnID, 'Stock In', 'Opening Stock',@cqty,@cost,@TerminalName,0,@dt,'New Product from XERO','N', @rout output


	 

  end

end
					 