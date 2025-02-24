create procedure [dbo].[sp_AddStoreStockForWooComm]
								@SyncID		int
as

declare @productType				char(1);
declare @productID					int;	
declare @matrixID					int;
declare @woocommID					int;
declare @woocommVariantID			int;
declare @qty						numeric(15,3);
declare @qty_mtx					numeric(15,3);
declare @woocommVariantID_mtx		int;

begin
   declare sc1 cursor
   for select ID, ProductType, QtyOnHand,WooCommID from Product where WooCommID > 0 and ProductStatus = 'Y'
   open sc1
   fetch next from sc1 into @productID, @productType, @qty, @woocommID
   while @@fetch_status = 0 begin
     
	 if @productType = 'P' begin
	   insert into StoreStockWoo(SyncID,ProductID,SubProductID,StockQty,WooCommID,WooCommVariantID,InitialStockQty)
	   values (@SyncID,@productID,0,@qty,@woocommID, 0, @qty);
	 end

	 if @productType = 'M' begin
	    declare sc2 cursor
		for select ID, QtyOnHand, WooCommVariantID from Matrix where WooCommVariantID > 0 
		and MatrixOptionID in (select ID from MatrixOptions where ProductID = @productID)
        open sc2
        fetch next from sc2 into @matrixID, @qty_mtx, @woocommVariantID
        while @@fetch_status = 0 begin

	      insert into StoreStockWoo(SyncID,ProductID,SubProductID,StockQty,WooCommID,WooCommVariantID,InitialStockQty)
	      values (@SyncID,@productID,@matrixID,@qty_mtx,@woocommID, @woocommVariantID,@qty_mtx);

		  fetch next from sc2 into @matrixID, @qty_mtx, @woocommVariantID
		end
		close sc2
		deallocate sc2
	  end
		fetch next from sc1 into @productID, @productType, @qty, @woocommID
   end
   close sc1
   deallocate sc1
end
GO