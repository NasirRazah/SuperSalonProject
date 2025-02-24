CREATE procedure [dbo].[sp_AdjustWooCommStock]
								@SyncID					int,
								@PrevSyncID				int,
								@ProductID				int,
								@SubProductID			int,
								@UpdatedQty				numeric(15,3),
								@TerminalName			nvarchar(50),
								@ReturnWooCommStock		numeric(15,3) output
								
as

declare @qty					numeric(15,3);
declare @qty_shp				numeric(15,3);
declare @cost			numeric(15,3);
declare @docno			nvarchar(50);
declare @sj int;
declare @pId			int;
declare @rout			int;
declare @dt				datetime;
declare @cqty			numeric(15,3);
declare @pqty			numeric(15,3);
declare @jqty			numeric(15,3);
declare @psqty			numeric(15,3);
declare @csqty			numeric(15,3);
begin
   
     set @ReturnWooCommStock = -1;

	 select @qty = StockQty from StoreStockWoo where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @PrevSyncID;
	 update StoreStockWoo set StockQty = @qty + @UpdatedQty where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @SyncID;

	 if @SubProductID = 0 begin
	    update Product set QtyOnHand = @qty + @UpdatedQty where ID = @ProductID;
		/* Stock Journal */
	 end

	 if @SubProductID > 0 begin
	    update Matrix set QtyonHand = QtyonHand + @UpdatedQty where ID = @SubProductID;
	    update Product set QtyOnHand = QtyOnHand + @UpdatedQty where ID = @ProductID;
		/* Stock Journal */
	 end


	 select @cost = cost, @cqty = qtyonhand from Product where ID = @ProductID;
	 select @pqty = InitialStockQty from StoreStockWoo where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @SyncID;

	 set @sj = 0;
	 set @dt = getdate();
	 set @jqty = 0;
	 set @DocNo = 'Adjust from Woo Commerce Store/' + cast(@SyncID as varchar(10));
	 if @cqty <> @pqty begin
	     if @cqty > @pqty begin
		    set @jqty = @cqty - @pqty;
			if @jqty < 1 set @jqty = -@jqty;
			exec @sj = sp_AddJournal @DocNo, @dt, @ProductID, 'Stock In', 'Adjust-WooComm',@jqty,@cost,@TerminalName,0,@dt,'Adjust from Woo Commerce Store','N', @rout output
		 end
		 if @cqty < @pqty begin
		    set @jqty = @pqty - @cqty;
			if @jqty < 1 set @jqty = -@jqty;
			exec @sj = sp_AddJournal @DocNo, @dt, @ProductID, 'Stock Out', 'Adjust-WooComm',@jqty,@cost,@TerminalName,0,@dt,'Sale from Woo Commerce Store','N', @rout output
		 end
	 end



	 select @qty_shp = StockQty from WooCommStock where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @PrevSyncID;

	 update WooCommStock set StockQty = @qty_shp + @UpdatedQty where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @SyncID;

	 select @psqty = InitialStockQty from WooCommStock where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @SyncID;
	 select @csqty = StockQty from WooCommStock where ProductID = @ProductID and SubProductID = @SubProductID and SyncID = @SyncID;

	 if @psqty = @csqty begin
	   set @ReturnWooCommStock = -1;
	 end
     else begin
	   set @ReturnWooCommStock = @csqty;
	 end
   
end
GO