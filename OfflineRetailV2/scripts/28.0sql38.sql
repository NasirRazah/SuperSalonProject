Create procedure [dbo].[sp_AddStoreStockForXero]
								@SyncID		int
as

declare @productType				char(1);
declare @productID					int;	
declare @qblistID					varchar(100);
declare @qty						numeric(15,3);

begin
   declare sc1 cursor
   for select ID, ProductType, QtyOnHand, XeroID from Product where XeroID is not null  and ProductStatus = 'Y'
   open sc1
   fetch next from sc1 into @productID, @productType, @qty, @qblistID
   while @@fetch_status = 0 begin
     
	 insert into StoreStockXero(SyncID,ProductID,StockQty,XeroID,InitialStockQty)
	   values (@SyncID,@productID,@qty,@qblistID, @qty);

		fetch next from sc1 into @productID, @productType, @qty, @qblistID
   end
   close sc1
   deallocate sc1
end
GO