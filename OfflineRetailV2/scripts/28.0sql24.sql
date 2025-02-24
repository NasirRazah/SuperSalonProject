CREATE procedure [dbo].[sp_AddWooCommProduct]
					@id				varchar(20),
					@title				varchar(150),
					@price				numeric(15,3),
					@qty				numeric(15,3),
					@sku				varchar(16),
					@product_category		varchar(20),
					@product_type			char(1),
					@TerminalName			nvarchar(50),
					@cost				numeric(15,3),
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

begin

  set @ReturnID = 0;

  set @dCnt = 0;
  set @cCnt = 0;
  set @vCnt = 0;

  set @sku_txt = '';


  if @sku != 0 set @sku_txt = @sku; else set @sku_txt = @id;

  select @dCnt = count(*) from dept where DepartmentID = 'WOOCOMM DEPARTMENT';

  if @dCnt > 0 begin
   select @deptID = ID from Dept where DepartmentID = 'WOOCOMM DEPARTMENT';
  end
  else begin
    insert into Dept (DepartmentID, [Description], CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
	values('WOOCOMM DEPARTMENT', 'WOOCOMM DEPARTMENT', 0, getdate(), 0, getdate())
	select @deptID = @@IDENTITY
  end

  select @CategoryID = ID from Category where WooCommID = @product_category;


  
  select @MaxPOSDisplay = isnull(Max(POSDisplayOrder),0) + 1 from Product where CategoryID = @CategoryID;

  insert into Product (SKU,[Description],ProductType, DepartmentID,CategoryID,PriceA,QtyOnHand, 
  POSBackground,POSScreenColor,POSScreenStyle,POSFontType,POSFontSize,POSFontColor,POSDisplayOrder,Cost,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
  values(@sku_txt,@title,@product_type,@deptID,@CategoryID,@price,@qty,
  'Skin','0','','Tahoma',8,'0',@MaxPOSDisplay,@cost,
  0, getdate(), 0, getdate())
  select @ReturnID = @@IDENTITY

  if @ReturnID > 0 begin

    
    
	update product set WooCommID = @id where ID = @ReturnID;

	
	 select  @cqty = qtyonhand from Product where ID = @ReturnID; 
	 set @sj = 0;
	 set @dt = getdate();
	 set @DocNo = 'New Product from Woo Commerce Store/' + @sku_txt;
	 exec @sj = sp_AddJournal @DocNo, @dt, @ReturnID, 'Stock In', 'Opening Stock',@cqty,@cost,@TerminalName,0,@dt,'New Product from Woo Commerce Store','N', @rout output

  end

end
					 
GO