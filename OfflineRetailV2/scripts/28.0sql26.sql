Create procedure [dbo].[sp_AddWooCommStockForWooCommStock]
								@SyncID					int,
								@woocommID				int,
								@woocommVariantID		int,
								@qty					numeric(15,3)
as

declare @productType				char(1);
declare @productID					int;	
declare @matrixID					int;
declare @headerVariantID			bigint;
declare @headerInventoryItemID		bigint;
declare @matrixVariantID			bigint;
declare @matrixInventoryItemID		bigint;
declare @zero						int;
declare @ptype						char(1);
begin

   select @productID = ID, @ptype = ProductType from Product where WooCommID = @woocommID;
   
   if @ptype = 'M' begin

     select @matrixID = ID from Matrix where MatrixOptionID in (select ID from MatrixOptions where ProductID = @productID)
	  and WooCommVariantID = @woocommVariantID;
      
	  insert into WooCommStock(SyncID,ProductID,SubProductID,StockQty,WooCommID,WooCommVariantID,InitialStockQty)
	      values (@SyncID,@productID,@matrixID,@qty, @woocommID, @woocommVariantID,@qty);

	 

   end
   else begin
    
	insert into WooCommStock(SyncID,ProductID,SubProductID,StockQty,WooCommID,WooCommVariantID,InitialStockQty)
	   values(@SyncID,@productID,0,@qty,@woocommID, 0,@qty);
      
   end
   
end
GO