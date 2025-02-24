Create procedure [dbo].[sp_AdjustQBWStock]
								@SyncID					int,
								@PrevSyncID				int,
								@ProductID				int,
								@UpdatedQty				numeric(15,3),
								@TerminalName			nvarchar(50),
								@ReturnQBWStock			numeric(15,3) output
								
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
   
     set @ReturnQBWStock = -1;

	 select @qty = StockQty from StoreStockQbD where ProductID = @ProductID  and SyncID = @PrevSyncID;
	 update StoreStockQbD set StockQty = @qty + @UpdatedQty where ProductID = @ProductID and SyncID = @SyncID;

	 update Product set QtyOnHand = @qty + @UpdatedQty where ID = @ProductID;


	 select @cost = cost, @cqty = qtyonhand from Product where ID = @ProductID;
	 select @pqty = InitialStockQty from StoreStockQbD where ProductID = @ProductID and SyncID = @SyncID;

	 set @sj = 0;
	 set @dt = getdate();
	 set @jqty = 0;
	 set @DocNo = 'Adjust from QuickBooks/' + cast(@SyncID as varchar(10));
	 if @cqty <> @pqty begin
	     if @cqty > @pqty begin
		    set @jqty = @cqty - @pqty;
			if @jqty < 1 set @jqty = -@jqty;
			exec @sj = sp_AddJournal @DocNo, @dt, @ProductID, 'Stock In', 'Adjust-QB',@jqty,@cost,@TerminalName,0,@dt,'Adjust from QuickBooks','N', @rout output
		 end
		 if @cqty < @pqty begin
		    set @jqty = @pqty - @cqty;
			if @jqty < 1 set @jqty = -@jqty;
			exec @sj = sp_AddJournal @DocNo, @dt, @ProductID, 'Stock Out', 'Adjust-QB',@jqty,@cost,@TerminalName,0,@dt,'Sale from QuickBooks','N', @rout output
		 end
	 end



	 select @qty_shp = StockQty from qbDStock where ProductID = @ProductID  and SyncID = @PrevSyncID;

	 update qbDStock set StockQty = @qty_shp + @UpdatedQty where ProductID = @ProductID  and SyncID = @SyncID;

	 select @psqty = InitialStockQty from qbDStock where ProductID = @ProductID  and SyncID = @SyncID;
	 select @csqty = StockQty from qbDStock where ProductID = @ProductID and SyncID = @SyncID;

	 if @psqty = @csqty begin
	   set @ReturnQBWStock = -1;
	 end
     else begin
	   set @ReturnQBWStock = @csqty;
	 end
   
end
GO