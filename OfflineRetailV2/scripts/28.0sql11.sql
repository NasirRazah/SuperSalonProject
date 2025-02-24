Create procedure [dbo].[sp_AddStoreStockForQBW]
								@SyncID		int
as

declare @productType				char(1);
declare @productID					int;	
declare @qblistID					varchar(50);
declare @qty						numeric(15,3);

begin
   declare sc1 cursor
   for select ID, ProductType, QtyOnHand, QBListID from Product where QBListID is not null  and ProductStatus = 'Y'
   open sc1
   fetch next from sc1 into @productID, @productType, @qty, @qblistID
   while @@fetch_status = 0 begin
     
	 if @productType = 'P' begin
	   insert into StoreStockQbD(SyncID,ProductID,StockQty,QBListID,InitialStockQty)
	   values (@SyncID,@productID,@qty,@qblistID, @qty);
	 end

	 if @productType = 'M' begin
	    insert into StoreStockQbD(SyncID,ProductID,StockQty,QBListID,InitialStockQty)
	   values (@SyncID,@productID,@qty,@qblistID,  @qty);
	  end
		fetch next from sc1 into @productID, @productType, @qty, @qblistID
   end
   close sc1
   deallocate sc1
end
GO