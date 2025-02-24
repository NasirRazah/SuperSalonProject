CREATE procedure [dbo].[sp_AddWooCommProductCategory]
					@id			varchar(20),
					@product_category	varchar(30),
					@ReturnID		int output	
as 

declare @MaxDisplay  		int;
declare @MaxPOSDisplay  	int;
declare @catidcnt		int;
declare @catidtxt		varchar(10);

begin

  set @ReturnID = 0;

  select @MaxDisplay = Max(POSDisplayOrder) + 1 from Category;
  select @catidcnt = count(*) + 1 from category where CategoryID like 'WooC-%';
  set @catidtxt = 'WooC-' + cast(@catidcnt as varchar(3));

  insert into Category (CategoryID, [Description], CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,
  MaxItemsforPOS,POSDisplayOrder,POSBackground,POSScreenStyle,POSFontType,POSItemFontColor,POSScreenColor,POSFontColor,WooCommID)
  values(@catidtxt, @product_category, 0, getdate(), 0, getdate(),10,@MaxDisplay,'Skin','','Tahoma','0','0','0',@id)
  select @ReturnID = @@IDENTITY


end				 
GO