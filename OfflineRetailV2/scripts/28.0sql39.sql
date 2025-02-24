Create procedure [dbo].[sp_AddXeroStock]
								@SyncID					int,
								@qblistID				varchar(100),
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

   select @productID = ID, @ptype = ProductType from Product where XeroID = @qblistID;
   insert into XeroStock(SyncID,ProductID,StockQty,XeroID,InitialStockQty)
	   values(@SyncID,@productID,@qty,@qblistID,@qty);
   
end
GO