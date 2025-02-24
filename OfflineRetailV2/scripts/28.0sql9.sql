CREATE procedure [dbo].[sp_AddQBWProduct]
					@title				varchar(150),
					@price				numeric(15,3),
					@cost				numeric(15,3),
					@qty				numeric(15,3),
					@rqty				numeric(15,3),
					@vendorref			varchar(50),
					@vendorpart			varchar(16),
					@listid					varchar(50),
					@editseq				varchar(50),
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


begin

  set @ReturnID = 0;

  set @dCnt = 0;
  set @cCnt = 0;
  set @vCnt = 0;

  set @sku_txt = '';


  set @sku_txt = @editseq;

  select @dCnt = count(*) from dept where DepartmentID = 'QB DEPARTMENT';

  if @dCnt > 0 begin
   select @deptID = ID from Dept where DepartmentID = 'QB DEPARTMENT';
  end
  else begin
    insert into Dept (DepartmentID, [Description], CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
	values('QB DEPARTMENT', 'QB DEPARTMENT', 0, getdate(), 0, getdate())
	select @deptID = @@IDENTITY
  end

  select @cCnt = count(*) from Category where CategoryID = 'QBCATEGORY';
     if @cCnt > 0 
     select @CategoryID = isnull(ID,0) from Category where CategoryID = 'QBCATEGORY';

     if @cCnt = 0 begin

        select @iCatDisp = isnull(Max(POSDisplayOrder),0) + 1 from Category;

        insert into category(CategoryID,Description,MaxItemsforPOS,POSDisplayOrder,
		POSBackground,POSFontType,POSFontSize,POSFontColor,POSItemFontColor,
		        FoodStampEligible,POSScreenColor,POSScreenStyle,IsBold,IsItalics,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) 
                                    values ( 'QBCATEGORY','QB CATEGORY',10,@iCatDisp,
									'Skin','Tahoma',9,'0','0','N',
                                    '0','','N','N',0,getdate(),0,getdate()) 
        select @CategoryID = @@IDENTITY ;

     end





  
  select @MaxPOSDisplay = isnull(Max(POSDisplayOrder),0) + 1 from Product where CategoryID = @CategoryID;

  insert into Product (SKU,[Description],ProductType, DepartmentID,CategoryID,PriceA,QtyOnHand, 
  POSBackground,POSScreenColor,POSScreenStyle,POSFontType,POSFontSize,POSFontColor,POSDisplayOrder,Cost,ReorderQty,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,QBListID,QBEditSequenceID)
  values(@sku_txt,@title,'P',@deptID,@CategoryID,@price,@qty,
  'Skin','0','','Tahoma',8,'0',@MaxPOSDisplay,@cost,@rqty,
  0, getdate(), 0, getdate(),@listid,@editseq)
  select @ReturnID = @@IDENTITY

  if @ReturnID > 0 begin

    
	 select  @cqty = qtyonhand from Product where ID = @ReturnID; 
	 set @sj = 0;
	 set @dt = getdate();
	 set @DocNo = 'New Product from QuickBooks/' + @sku_txt;
	 exec @sj = sp_AddJournal @DocNo, @dt, @ReturnID, 'Stock In', 'Opening Stock',@cqty,@cost,@TerminalName,0,@dt,'New Product from QuickBooks','N', @rout output


	 if @vendorref != '' begin
	   select @VendorID = ID from Vendor where QBListID = @vendorref;
	   insert into VendPart(VendorID,ProductID,PartNumber,IsPrimary,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
	   values(@VendorID,@ReturnID,@vendorpart,'Y',0, getdate(), 0, getdate());
	 end

  end

end
					 
GO