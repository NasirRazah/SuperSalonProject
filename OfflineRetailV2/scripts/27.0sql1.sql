GO
IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOut]') AND NAME='EmployeeName')
  ALTER TABLE [dbo].[CentralExportCloseOut] add [EmployeeName] nvarchar(50) NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOut]') AND NAME='ExpFlag')
  ALTER TABLE [dbo].[CentralExportCloseOut] add [ExpFlag] char(1) NULL default 'N';
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOutReportMain]') AND NAME='EmpName')
  ALTER TABLE [dbo].[CentralExportCloseOutReportMain] add [EmpName] nvarchar(50) NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOutReportTender]') AND NAME='CloseoutType')
  ALTER TABLE [dbo].[CentralExportCloseOutReportTender] add [CloseoutType] char(1) NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOutReturn]') AND NAME='CloseoutType')
  ALTER TABLE [dbo].[CentralExportCloseOutReturn] add [CloseoutType] char(1) NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOutSalesDept]') AND NAME='EmpName')
  ALTER TABLE [dbo].[CentralExportCloseOutSalesDept] add [EmpName] nvarchar(50) NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOutSalesHour]') AND NAME='EmpName')
  ALTER TABLE [dbo].[CentralExportCloseOutSalesHour] add [EmpName] nvarchar(50) NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportPOHeader]') AND NAME='POHeaderID')
  ALTER TABLE [dbo].[CentralExportPOHeader] add [POHeaderID] int NULL;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportPOHeader]') AND NAME='RefNo')
  ALTER TABLE [dbo].[CentralExportPOHeader] add [RefNo] varchar(16) NULL;

GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[Item]') AND NAME='UOM')
  ALTER TABLE [dbo].[Item] add [UOM] nvarchar(15) NULL default '';

GO


IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[CentralExportReceiving]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[CentralExportReceiving](
	[RecvHeaderID] [int] NULL,
	[RecvDetailID] [int] NULL,
	[BatchID] [int] NULL,
	[PurchaseOrder] [varchar](16) NULL,
	[InvoiceNo] [varchar](16) NULL,
	[InvoiceTotal] [numeric](15, 4) NULL,
	[GrossAmount] [numeric](15, 4) NULL,
	[Tax] [numeric](15, 4) NULL,
	[Freight] [numeric](15, 4) NULL,
	[DateOrdered] [datetime] NULL,
	[ReceiveDate] [datetime] NULL,
	[DateTimeStamp] [datetime] NULL,
	[Note] [varchar](250) NULL,
	[PriceA] [numeric](15, 3) NULL,
	[DQty] [numeric](15, 4) NULL,
	[DCost] [numeric](15, 4) NULL,
	[DFreight] [numeric](15, 4) NULL,
	[DTax] [numeric](15, 4) NULL,
	[ProductName] [varchar](150) NULL,
	[VendorPartNo] [varchar](16) NULL,
	[CheckClerk] [nvarchar](40) NULL,
	[RecvClerk] [nvarchar](40) NULL,
	[RecvClerkID] [nvarchar](40) NULL,
	[CheckClerkID] [nvarchar](40) NULL,
	[VendorID] [nvarchar](40) NULL,
	[VendorName] [varchar](150) NULL
) ON [PRIMARY]
END

GO


DROP PROCEDURE IF EXISTS [dbo].[sp_CloseoutReportReturnItem]
GO

CREATE procedure [dbo].[sp_CloseoutReportReturnItem]
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50),
			@Sync			char(1)
			
as

declare @LocalCloseoutType		char(1);
declare @LocalCloseoutID		int;
declare @LocalTerminal		nvarchar(50);
declare @ReturnSKU		nvarchar(16);
declare @ReturnInvoiceNo		int;
declare @ReturnAmount		numeric(15,3);
declare @countvoid			int;

begin

  set @LocalCloseoutType = @CloseoutType;
  set @LocalCloseoutID	= @CloseoutID;
  set @LocalTerminal = @Terminal;

  delete from CloseoutReturn where ReportTerminalName = @LocalTerminal
  
  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin

   
    declare sc9 cursor
    for select inv.ID,i.SKU,i.Price*i.Qty from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @LocalCloseoutID 
               and i.ReturnedItemID <> 0 and i.Qty < 0 and inv.ID not in ( select invoiceno from VoidInv) 
               and i.Tagged <> 'X' and i.ServiceType = 'Sales'
    open sc9
    fetch next from sc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount
    while @@fetch_status = 0 begin

     insert into CloseoutReturn (ReturnSKU,ReturnInvoiceNo,ReturnAmount,ReportTerminalName) 
     values(@ReturnSKU,@ReturnInvoiceNo,@ReturnAmount,@LocalTerminal)

	 if @Sync = 'Y'
	 insert into CentralExportCloseoutReturn (ReturnSKU,ReturnInvoiceNo,ReturnAmount,CloseoutID,CloseoutType) 
     values(@ReturnSKU,@ReturnInvoiceNo,@ReturnAmount,@CloseoutID,@CloseoutType)
      
     
     fetch next from sc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount

    end
    close sc9
    deallocate sc9 
  
  end

  if @LocalCloseoutType = 'C' begin

   
    declare csc9 cursor
    for select inv.ID,i.SKU,i.Price*i.Qty from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) 
               and i.ReturnedItemID <> 0 and i.Qty < 0 
               and i.Tagged <> 'X' and i.ServiceType = 'Sales'

    open csc9
    fetch next from csc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount
    while @@fetch_status = 0 begin

	  if @ReturnInvoiceNo > 0 begin

	   set @countvoid = 0;

	   select @countvoid = count(*) from VoidInv where invoiceno = @ReturnInvoiceNo;

	   if @countvoid = 0 begin
    
         insert into CloseoutReturn (ReturnSKU,ReturnInvoiceNo,ReturnAmount,ReportTerminalName) 
         values(@ReturnSKU,@ReturnInvoiceNo,@ReturnAmount,@LocalTerminal)
		 
		 if @Sync = 'Y'
	 insert into CentralExportCloseoutReturn (ReturnSKU,ReturnInvoiceNo,ReturnAmount,CloseoutID,CloseoutType) 
     values(@ReturnSKU,@ReturnInvoiceNo,@ReturnAmount,@CloseoutID,@CloseoutType)
     
	   end

	 end

      fetch next from csc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount
     
    end
    close csc9
    deallocate csc9 
  
  end

end
GO

DROP PROCEDURE IF EXISTS [dbo].[sp_CloseoutReportTender]
GO

CREATE procedure [dbo].[sp_CloseoutReportTender]
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50),
			@Sync			char(1)
			with recompile
as
  
declare @TID 			int;
declare @TTName			nvarchar(40);
declare @TName			nvarchar(40);
declare @Count 			int;
declare @Amount 			numeric(18,3);
declare @TAmount			numeric(18,3);
declare @cashbackamt		numeric(18,3);
declare @cashbackcnt		int;
declare @cardprocessingamt		numeric(18,3);
declare @cardprocessingcnt		int;
declare	@LocalCloseoutType	char(1);
declare	@LocalCloseoutID		int;
declare	@LocalTerminal			nvarchar(50);

declare @cashfloatamt			numeric(18,3);
declare @boolCashTenderingExists char(1);
declare @CashTenderID int;

declare @ConsolidatedTerminal	int;

declare @safedropamt		numeric(18,3);
declare @paidinamt			numeric(18,3);
declare @paidoutamt			numeric(18,3);
declare @cashoutamt			numeric(18,3);
declare @cashinamt			numeric(18,3);



begin

   set @LocalCloseoutType = @CloseoutType;
   set @LocalCloseoutID	= @CloseoutID;
   set @LocalTerminal = @Terminal;

   set @cashbackamt = 0;
   set @cashbackcnt = 0;

   set @cardprocessingamt = 0;
   set @cardprocessingcnt = 0;
   set @cashfloatamt = 0;

   set @boolCashTenderingExists = 'N';
   set @CashTenderID = 0;
   set @ConsolidatedTerminal = 0;

   set @cashoutamt = 0;


   set @Count = 0;
   delete from CloseoutReportTender where ReportTerminalName = @LocalTerminal
  
  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin
   
    declare sc10 cursor
    for select ID, DisplayAs,Name from TenderTypes where name <> 'Store Credit' order by PaymentOrder
               
    open sc10
    fetch next from sc10 into @TID,@TName,@TTName
    while @@fetch_status = 0 begin

     select @Count = count(*) , @Amount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
     on tr.ID = t.TransactionNo 
     where t.TenderType = @TID and tr.CloseoutID = @LocalCloseoutID and tr.TransType not in (6,66,67,91,92)
     and tr.ID not in ( select transactionno from invoice where id in ( select invoiceno from VoidInv 
     where invoiceno not in ( select id from invoice where repairparentid = 0 and servicetype = 'Repair' )))
	 and tr.ID not in (select transactionno from invoice where id in (select invoiceno from VoidInv))
	 and tr.ID not in (select transactionno from invoice where RepairParentID in (select invoiceno from VoidInv))

	 if @LocalCloseoutType = 'T' and @TTName = 'Cash' and @Count = 0 begin
	   set @CashTenderID = @TID;
	   set @boolCashTenderingExists = 'N';
	   insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName,-929292,@Count,@CloseoutType);

	   select @cashfloatamt  = isnull(cashfloat,0) from CashFloat where CloseoutID = @CloseoutID;
			    insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Float',@cashfloatamt,0,@CloseoutType);

       select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash In',@cashinamt,0,@CloseoutType);

				select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Out',@cashoutamt,0,@CloseoutType);
	  
	   select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid In',@paidinamt,0,@CloseoutType);

				select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);
	  
					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid Out',@paidoutamt,0,@CloseoutType);

	   select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID = @CloseoutID);
	   insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Safe Drop',@safedropamt,0,@CloseoutType);
	 end
     if @Count > 0 begin
	    if @TTName = 'Credit Card' or @TTName = 'Debit Card' or @TTName = 'EBT' or @TTName = 'EBT Cash' begin
		  set @cardprocessingcnt = @cardprocessingcnt + @Count;
		  set @cardprocessingamt = @cardprocessingamt + @Amount;
		end
		     if @LocalCloseoutType = 'T' begin
			   if @TTName = 'Cash' begin
			        set @CashTenderID = @TID;
					set @boolCashTenderingExists = 'Y';
			        insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName,-929292,@Count,@CloseoutType);

					select @cashfloatamt  = isnull(cashfloat,0) from CashFloat where CloseoutID = @CloseoutID;
			    insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Float',@cashfloatamt,0,@CloseoutType);

				select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash In',@cashinamt,0,@CloseoutType);

				select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Out',@cashoutamt,0,@CloseoutType);

				select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid In',@paidinamt,0,@CloseoutType);

				select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid Out',@paidoutamt,0,@CloseoutType);

				select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Safe Drop',@safedropamt,0,@CloseoutType);

				insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
               
			       if @Sync = 'Y'
				   insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@CloseoutType);

			   end
			   else begin
			     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);

				  if @Sync = 'Y'
				  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@CloseoutType);

			   end
			end
			else begin
			  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);

			    if @Sync = 'Y'
				insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@CloseoutType);
			end
            
      end
      set @Count = 0;
     fetch next from sc10 into @TID,@TName,@TTName

    end

    close sc10
    deallocate sc10

	

	
	select @cashbackcnt = count(*) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and RefCardAuthAmount - CardAmount > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @LocalCloseoutID);

	select @cashbackamt = isnull(sum(RefCardAuthAmount - CardAmount),0) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @LocalCloseoutID);
    
	if (@cashbackcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99998,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@LocalTerminal)
     
	  if @Sync = 'Y'
	  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,TenderCount,CloseoutType)
                       values('T',@LocalCloseoutID,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@CloseoutType)

	end
	/*
	select @cardprocessingcnt = count(*) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @CloseoutID);

	select @cardprocessingamt = isnull(sum(CardAmount),0) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @CloseoutID);*/
    
	if (@cardprocessingcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@LocalTerminal)

	    if @Sync = 'Y'
		insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,TenderCount,CloseoutType)
                       values('T',@LocalCloseoutID,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@CloseoutType)
	end

    
    declare sc11 cursor

    for select c.TenderID,c.TenderAmount,t.DisplayAs from CloseoutTender c left outer join tendertypes t on t.ID = c.TenderID 
    where c.CloseoutID=@LocalCloseoutID and t.name <> 'Store Credit' order by t.PaymentOrder
               
    open sc11
    fetch next from sc11 into @TID,@TAmount,@TName
    while @@fetch_status = 0 begin

     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('C',@LocalCloseoutID,@TID,@TName,@TAmount,@LocalTerminal)

							 if @Sync = 'Y'
							 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,CloseoutType)
                            values('C',@LocalCloseoutID,@TName,@TAmount,@CloseoutType)

     set @Count = 0;
     select @Amount = TenderAmount,@Count = TenderCount from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
     CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal

	 if @LocalCloseoutType = 'T' and @TID = @CashTenderID  begin
	   select @Amount = sum(TenderAmount),@Count = sum(TenderCount) from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
       CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal and TenderAmount != -929292 and TenderName not in ('  Cash Float', '  Cash In', '  Cash Out', '  Paid In', '  Paid Out')
	 end
     
     if @Count > 0 begin
     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('R',@LocalCloseoutID,@TID,@TName,@TAmount-@Amount,@LocalTerminal)

					 if @Sync = 'Y'
					  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,CloseoutType)
                            values('R',@LocalCloseoutID,@TName,@TAmount-@Amount,@CloseoutType)
     end
     fetch next from sc11 into @TID,@TAmount,@TName

    end
    close sc11
    deallocate sc11

  end

  
   
  if @LocalCloseoutType = 'C' begin
   
    select @ConsolidatedTerminal = count(*) from CloseOut where CloseoutType = 'T' and ConsolidatedID = @CloseoutID;
    declare csc10 cursor
    for select ID, DisplayAs,Name from TenderTypes where name <> 'Store Credit' order by PaymentOrder
               
    open csc10
    fetch next from csc10 into @TID,@TName,@TTName
    while @@fetch_status = 0 begin

     select @Count = count(*) , @Amount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
     on tr.ID = t.TransactionNo 
     where t.TenderType = @TID and tr.CloseoutID in 
     (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and tr.TransType not in (66,67,6,91,92)
     and tr.ID not in ( select transactionno from invoice where id in ( select invoiceno from VoidInv 
     where invoiceno not in ( select id from invoice where repairparentid = 0 and servicetype = 'Repair' )))
	 and tr.ID not in (select transactionno from invoice where id in (select invoiceno from VoidInv))
	 and tr.ID not in (select transactionno from invoice where RepairParentID in (select invoiceno from VoidInv))

	  if @ConsolidatedTerminal > 0 and @TTName = 'Cash' and @Count = 0 begin
	   set @CashTenderID = @TID;
	   set @boolCashTenderingExists = 'N';
	   insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName,-929292,@Count,@CloseoutType);

	   select @cashfloatamt  = isnull(sum(cashfloat),0) from CashFloat where CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T');
			    insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Float',@cashfloatamt,0,@CloseoutType);

	  select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash In',@cashinamt,0,@CloseoutType);

	  select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Out',@cashoutamt,0,@CloseoutType);

	   select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid In',@paidinamt,0,@CloseoutType);

	  select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid Out',@paidoutamt,0,@CloseoutType);

	   select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);
	  
				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Safe Drop',@safedropamt,0,@CloseoutType);
	 end

     if @Count > 0 begin
	         if @TTName = 'Credit Card' or @TTName = 'Debit Card' or @TTName = 'EBT' or @TTName = 'EBT Cash' begin
				set @cardprocessingcnt = @cardprocessingcnt + @Count;
				set @cardprocessingamt = @cardprocessingamt + @Amount;
		end

		if @ConsolidatedTerminal > 0 begin
		  
		   if @TTName = 'Cash' begin
			        set @CashTenderID = @TID;
					set @boolCashTenderingExists = 'Y';
			        insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName,-929292,@Count,@CloseoutType);

					select @cashfloatamt  = isnull(sum(cashfloat),0) from CashFloat where CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T');
			        insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			        TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			        TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Float',@cashfloatamt,0,@CloseoutType);

					select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);


					if @Sync = 'Y'
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash In',@cashinamt,0,@CloseoutType);

					select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Cash Out',@cashoutamt,0,@CloseoutType);

					select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid In',@paidinamt,0,@CloseoutType);

	  select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Paid Out',@paidoutamt,0,@CloseoutType);

					select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
			    TenderCount,CloseoutType)values('T',@LocalCloseoutID,'  Safe Drop',@safedropamt,0,@CloseoutType);

				insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
               
					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@CloseoutType);
			   
			   end
			   else begin

			     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);

					 if @Sync = 'Y'
					 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,
					TenderCount,CloseoutType)values('T',@LocalCloseoutID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@CloseoutType);
			   end


		end
		else begin
		  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);

				 if @Sync = 'Y'
				 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,TenderCount,CloseoutType)
                       values('T',@LocalCloseoutID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@CloseoutType);

		end

			
     end
     set @Count = 0;
     
     fetch next from csc10 into @TID,@TName,@TTName

    end

    close csc10
    deallocate csc10


	


	select @cashbackcnt = count(*) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and RefCardAuthAmount - CardAmount > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) );

	select @cashbackamt = isnull(sum(RefCardAuthAmount - CardAmount),0) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) );
    
	if (@cashbackcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99998,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@LocalTerminal)

					    if @Sync = 'Y'
						insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,TenderCount,CloseoutType)
                       values('T',@LocalCloseoutID,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@CloseoutType)

	end

	/*
	select @cardprocessingcnt = count(*) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) );

	select @cardprocessingamt = isnull(sum(CardAmount),0) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) );*/
	if (@cardprocessingcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99999,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@LocalTerminal)

					    if @Sync = 'Y'
						insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,TenderCount,CloseoutType)
                       values('T',@LocalCloseoutID,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@CloseoutType)
	end

    declare csc11 cursor

    for select c.TenderID,c.TenderAmount,t.DisplayAs from CloseoutTender c left outer join tendertypes t
    on t.ID = c.TenderID where c.CloseoutID=@LocalCloseoutID and t.name <> 'Store Credit' order by t.PaymentOrder
               
    open csc11
    fetch next from csc11 into @TID,@TAmount,@TName
    while @@fetch_status = 0 begin

      insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('C',@LocalCloseoutID,@TID,@TName,@TAmount,@LocalTerminal)

							 if @Sync = 'Y'
							 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,CloseoutType)
                            values('C',@LocalCloseoutID,@TName,@TAmount,@CloseoutType)

      set @Count = 0;
      
      select @Amount = TenderAmount,@Count=TenderCount from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
      CloseoutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and ReportTerminalName = @LocalTerminal
     
	   if @ConsolidatedTerminal > 0 and @TID = @CashTenderID  begin
	   select @Amount = sum(TenderAmount),@Count = sum(TenderCount) from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
       CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal and TenderAmount != -929292 and TenderName not in ('  Cash Float', '  Cash In', '  Cash Out', '  Paid In', '  Paid Out')
	 end

     if @Count > 0 begin
      insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('R',@LocalCloseoutID,@TID,@TName,@TAmount-@Amount,@LocalTerminal)

						 if @Sync = 'Y'
						 insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderName,TenderAmount,CloseoutType)
                            values('R',@LocalCloseoutID,@TName,@TAmount-@Amount,@CloseoutType)

	end
          
      set @Count = 0;    
      fetch next from csc11 into @TID,@TAmount,@TName

    end
    close csc11
    deallocate csc11

  end


end

GO

DROP PROCEDURE IF EXISTS [dbo].[sp_CloseoutSalesByDept]
GO

CREATE procedure [dbo].[sp_CloseoutSalesByDept]
			@tax_inclusive	char(1),
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50),
			@Sync			char(1)
			with recompile

as

declare @LocalCloseoutType	char(1);
declare @LocalCloseoutID	int;
declare @LocalTerminal	nvarchar(50);
declare @DID		int;
declare @DeptID		nvarchar(10);
declare @DeptDesc		nvarchar(40);
declare @amt		numeric(18,3);
declare @amtI		numeric(18,3);
declare @SD		datetime;
declare @ED		datetime;
declare @Notes		nvarchar(100);
declare @CT		char(1);
declare @NS		int;
declare @count1		int;
declare @count2		int;
declare @EmpID		nvarchar(12);
declare @CTeml		nvarchar(50);

declare @EmpName nvarchar(50);
declare @TransactionCnt int;

begin

  set @LocalCloseoutType = @CloseoutType;
  set @LocalCloseoutID	= @CloseoutID;
  set @LocalTerminal = @Terminal;
  
  delete from CloseOutSalesDept where ReportTerminalName = @LocalTerminal

  select @CT=c.CloseoutType, @SD=c.StartDatetime, @ED=isnull(c.EndDatetime,getdate()), @Notes=c.Notes, 
  @EmpID = isnull(e.EmployeeID,'ADMIN') , @CTeml = c.TerminalName, @TransactionCnt = c.TransactionCnt,
  @EmpName = e.FirstName + ' ' + e.LastName
  from closeout c left outer join employee e on e.ID = c.CreatedBy where c.ID = @LocalCloseoutID

  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin

    /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)
    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo > 0 and t.Transtype = 4
    and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2 
   
    declare sc38 cursor
    for select i.DepartmentID,d.DepartmentID, d.Description , sum(i.Price*i.Qty) - sum(i.Discount) as salesamt,
	sum(i.TaxIncludeRate*i.Qty) as salesamt1 from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo 
               left outer join dept d on d.ID = i.departmentID where t.CloseOutID = @LocalCloseoutID and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z' and i.ProductType <> 'H' and i.departmentID > 0 and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4)) and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv)
               group by i.DepartmentID,d.DepartmentID, d.Description order by d.DepartmentID

    open sc38
    fetch next from sc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    while @@fetch_status = 0 begin
	  if @tax_inclusive = 'N' begin
      insert into CloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amt,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);

	  if @Sync = 'Y'
	  insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) 
      values (@DeptID,@DeptDesc,@amt,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);

      end    
	  if @tax_inclusive = 'Y' begin
      insert into CloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amtI,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);

	  if @Sync = 'Y'
	  insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) 
      values (@DeptID,@DeptDesc,@amtI,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
      end  
      fetch next from sc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    end
    close sc38
    deallocate sc38

  end  


  if @LocalCloseoutType = 'C' begin

      /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)

    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in
    (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) 
    and inv.LayawayNo > 0 and t.Transtype = 4
    and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2 
   
    declare csc38 cursor
    for select i.DepartmentID,d.DepartmentID, d.Description , sum(i.Price*i.Qty) - sum(i.Discount) as salesamt,
	sum(i.TaxIncludeRate*i.Qty) as salesamt1 from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo 
               left outer join dept d on d.ID = i.departmentID where t.CloseOutID 
	       in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z' and i.ProductType <> 'H' and i.departmentID > 0 and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4)) and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv)
               group by i.DepartmentID,d.DepartmentID, d.Description order by d.DepartmentID

    open csc38
    fetch next from csc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    while @@fetch_status = 0 begin
	  if @tax_inclusive = 'N' begin
      insert into CloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amt,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);

	  if @Sync = 'Y'
	  insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) 
      values (@DeptID,@DeptDesc,@amt,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
      end  
	  if @tax_inclusive = 'Y' begin
      insert into CloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amtI,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);

	  if @Sync = 'Y'
	  insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) 
      values (@DeptID,@DeptDesc,@amtI,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
      end   
      fetch next from csc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    end
    close csc38
    deallocate csc38

  end  

end
GO

DROP PROCEDURE IF EXISTS [dbo].[sp_CloseoutSalesByHour]
GO

CREATE procedure [dbo].[sp_CloseoutSalesByHour]
			@tax_inclusive	char(1),
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50),
			@Sync			char(1)
			with recompile
as

declare	@LocalCloseoutType	char(1);
declare	@LocalCloseoutID		int;
declare	@LocalTerminal		nvarchar(50);
declare @PType			char(1);
declare @Pr					numeric(18,3);
declare @NPr				numeric(18,3);
declare @Qty				numeric(18,3);
declare @Disc				numeric(18,3);
declare @tiPr				numeric(15,3);
declare @T1					char(1);
declare @T2					char(1);
declare @T3					char(1);
declare @RetrnItm			numeric(18,3);
declare @LayNo				int;
declare @Status				int;
declare @TransType			int;
declare @InvNo				int;
declare @TranNo				int;
declare @hour		int;
declare @hour0		numeric(18,3);
declare @hour1		numeric(18,3);
declare @hour2		numeric(18,3);
declare @hour3		numeric(18,3);
declare @hour4		numeric(18,3);
declare @hour5		numeric(18,3);
declare @hour6		numeric(18,3);
declare @hour7		numeric(18,3);
declare @hour8		numeric(18,3);
declare @hour9		numeric(18,3);
declare @hour10		numeric(18,3);
declare @hour11		numeric(18,3);
declare @hour12		numeric(18,3);
declare @hour13		numeric(18,3);
declare @hour14		numeric(18,3);
declare @hour15		numeric(18,3);
declare @hour16		numeric(18,3);
declare @hour17		numeric(18,3);
declare @hour18		numeric(18,3);
declare @hour19		numeric(18,3);
declare @hour20		numeric(18,3);
declare @hour21		numeric(18,3);
declare @hour22		numeric(18,3);
declare @hour23		numeric(18,3);



declare @SD		datetime;
declare @ED		datetime;
declare @Notes		varchar(100);
declare @CT		char(1);
declare @NS		int;
declare @count1		int;
declare @count2		int;
declare @EmpID		varchar(12);
declare @CTeml		varchar(50);

declare @countvoid			int;
declare @EmpName nvarchar(50);

begin
  
  set @LocalCloseoutType = @CloseoutType;
  set @LocalCloseoutID	= @CloseoutID;
  set @LocalTerminal = @Terminal;

  delete from CloseOutSalesHour where ReportTerminalName = @LocalTerminal

  set @hour0  	= 0;
  set @hour1	= 0;
  set @hour2	= 0;
  set @hour3	= 0;
  set @hour4	= 0;
  set @hour5	= 0;
  set @hour6	= 0;
  set @hour7	= 0;
  set @hour8	= 0;
  set @hour9	= 0;
  set @hour10	= 0;
  set @hour11	= 0;
  set @hour12	= 0;
  set @hour13	= 0;
  set @hour14	= 0;
  set @hour15	= 0;
  set @hour16	= 0;
  set @hour17	= 0;
  set @hour18	= 0;
  set @hour19	= 0;
  set @hour20	= 0;
  set @hour21	= 0;
  set @hour22	= 0;
  set @hour23	= 0;


  select @CT=c.CloseoutType, @SD=c.StartDatetime, @ED=isnull(c.EndDatetime,getdate()), @Notes=c.Notes, 
  @EmpID = isnull(e.EmployeeID,'ADMIN') , @CTeml = c.TerminalName, @EmpName = e.FirstName + ' ' + e.LastName
  from closeout c left outer join employee e on e.ID = c.CreatedBy where c.ID = @LocalCloseoutID

  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin

    /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)

    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo > 0 and t.Transtype = 4
    and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2  

    declare sc1 cursor
    for select inv.ID, t.ID,i.TaxIncludeRate, i.ProductType, i.Price, i.NormalPrice, i.Qty,i.Discount, i.Taxable1, i.Taxable2, 
               i.Taxable3,i.ReturnedItemCnt,inv.Status,inv.LayawayNo, t.TransType,  { fn HOUR(t.TransDate) } as thour from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @LocalCloseoutID and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z'
			   and i.ProductType <> 'H' and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4)) and i.Tagged <> 'X'
                order by thour 

    open sc1
    fetch next from sc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    while @@fetch_status = 0 begin

	  set @countvoid = 0;

	   select @countvoid = count(*) from VoidInv where invoiceno = @InvNo;

	   if @countvoid = 0 begin

	  if @tax_inclusive = 'N' begin

      if @hour = 0 set @hour0 = @hour0 + (@Pr*@Qty) - @Disc;   
      if @hour = 1 set @hour1 = @hour1 + (@Pr*@Qty) - @Disc;   
      if @hour = 2 set @hour2 = @hour2 + (@Pr*@Qty) - @Disc;   
      if @hour = 3 set @hour3 = @hour3 + (@Pr*@Qty) - @Disc;   
      if @hour = 4 set @hour4 = @hour4 + (@Pr*@Qty) - @Disc;   
      if @hour = 5 set @hour5 = @hour5 + (@Pr*@Qty) - @Disc;   
      if @hour = 6 set @hour6 = @hour6 + (@Pr*@Qty) - @Disc;   
      if @hour = 7 set @hour7 = @hour7 + (@Pr*@Qty) - @Disc;   
      if @hour = 8 set @hour8 = @hour8 + (@Pr*@Qty) - @Disc;   
      if @hour = 9 set @hour9 = @hour9 + (@Pr*@Qty) - @Disc;   
      if @hour = 10 set @hour10 = @hour10 + (@Pr*@Qty) - @Disc;   
      if @hour = 11 set @hour11 = @hour11 + (@Pr*@Qty) - @Disc;   
      if @hour = 12 set @hour12 = @hour12 + (@Pr*@Qty) - @Disc;   
      if @hour = 13 set @hour13 = @hour13 + (@Pr*@Qty) - @Disc;   
      if @hour = 14 set @hour14 = @hour14 + (@Pr*@Qty) - @Disc;   
      if @hour = 15 set @hour15 = @hour15 + (@Pr*@Qty) - @Disc;   
      if @hour = 16 set @hour16 = @hour16 + (@Pr*@Qty) - @Disc;   
      if @hour = 17 set @hour17 = @hour17 + (@Pr*@Qty) - @Disc;   
      if @hour = 18 set @hour18 = @hour18 + (@Pr*@Qty) - @Disc;   
      if @hour = 19 set @hour19 = @hour19 + (@Pr*@Qty) - @Disc;   
      if @hour = 20 set @hour20 = @hour20 + (@Pr*@Qty) - @Disc;   
      if @hour = 21 set @hour21 = @hour21 + (@Pr*@Qty) - @Disc;   
      if @hour = 22 set @hour22 = @hour22 + (@Pr*@Qty) - @Disc;   
      if @hour = 23 set @hour23 = @hour23 + (@Pr*@Qty) - @Disc;  
	  
	  end

	  if @tax_inclusive = 'Y' begin

      if @hour = 0 set @hour0 = @hour0 + (@tiPr*@Qty);   
      if @hour = 1 set @hour1 = @hour1 + (@tiPr*@Qty);   
      if @hour = 2 set @hour2 = @hour2 + (@tiPr*@Qty);   
      if @hour = 3 set @hour3 = @hour3 + (@tiPr*@Qty);   
      if @hour = 4 set @hour4 = @hour4 + (@tiPr*@Qty);   
      if @hour = 5 set @hour5 = @hour5 + (@tiPr*@Qty);   
      if @hour = 6 set @hour6 = @hour6 + (@tiPr*@Qty);   
      if @hour = 7 set @hour7 = @hour7 + (@tiPr*@Qty);   
      if @hour = 8 set @hour8 = @hour8 + (@tiPr*@Qty);   
      if @hour = 9 set @hour9 = @hour9 + (@tiPr*@Qty);   
      if @hour = 10 set @hour10 = @hour10 + (@tiPr*@Qty);   
      if @hour = 11 set @hour11 = @hour11 + (@tiPr*@Qty);   
      if @hour = 12 set @hour12 = @hour12 + (@tiPr*@Qty);   
      if @hour = 13 set @hour13 = @hour13 + (@tiPr*@Qty);   
      if @hour = 14 set @hour14 = @hour14 + (@tiPr*@Qty);   
      if @hour = 15 set @hour15 = @hour15 + (@tiPr*@Qty);   
      if @hour = 16 set @hour16 = @hour16 + (@tiPr*@Qty);   
      if @hour = 17 set @hour17 = @hour17 + (@tiPr*@Qty);   
      if @hour = 18 set @hour18 = @hour18 + (@tiPr*@Qty);   
      if @hour = 19 set @hour19 = @hour19 + (@tiPr*@Qty);   
      if @hour = 20 set @hour20 = @hour20 + (@tiPr*@Qty);   
      if @hour = 21 set @hour21 = @hour21 + (@tiPr*@Qty);   
      if @hour = 22 set @hour22 = @hour22 + (@tiPr*@Qty);   
      if @hour = 23 set @hour23 = @hour23 + (@tiPr*@Qty);  
	  
	  end
	   
           end
      fetch next from sc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    end
    close sc1
    deallocate sc1 

  end 


  if @LocalCloseoutType = 'C' begin

      /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)

    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in
    (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) 
    and inv.LayawayNo > 0 and t.Transtype = 4 and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2 
   
    declare csc1 cursor
    for select inv.ID, t.ID,i.TaxIncludeRate, i.ProductType, i.Price, i.NormalPrice,i.Qty,i.Discount, i.Taxable1, i.Taxable2, 
               i.Taxable3,i.ReturnedItemCnt,inv.Status,inv.LayawayNo, t.TransType,  { fn HOUR(t.TransDate) } as thour from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z'
			   and i.ProductType <> 'H' and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4))  and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv) order by thour 

    open csc1
    fetch next from csc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    while @@fetch_status = 0 begin

	  set @countvoid = 0;

	   select @countvoid = count(*) from VoidInv where invoiceno = @InvNo;

	   if @countvoid = 0 begin

	   if @tax_inclusive = 'N' begin
      if @hour = 0 set @hour0 = @hour0 + (@Pr*@Qty) - @Disc;   
      if @hour = 1 set @hour1 = @hour1 + (@Pr*@Qty) - @Disc;   
      if @hour = 2 set @hour2 = @hour2 + (@Pr*@Qty) - @Disc;   
      if @hour = 3 set @hour3 = @hour3 + (@Pr*@Qty) - @Disc;   
      if @hour = 4 set @hour4 = @hour4 + (@Pr*@Qty) - @Disc;   
      if @hour = 5 set @hour5 = @hour5 + (@Pr*@Qty) - @Disc;   
      if @hour = 6 set @hour6 = @hour6 + (@Pr*@Qty) - @Disc;   
      if @hour = 7 set @hour7 = @hour7 + (@Pr*@Qty) - @Disc;   
      if @hour = 8 set @hour8 = @hour8 + (@Pr*@Qty) - @Disc;   
      if @hour = 9 set @hour9 = @hour9 + (@Pr*@Qty) - @Disc;   
      if @hour = 10 set @hour10 = @hour10 + (@Pr*@Qty) - @Disc;   
      if @hour = 11 set @hour11 = @hour11 + (@Pr*@Qty) - @Disc;   
      if @hour = 12 set @hour12 = @hour12 + (@Pr*@Qty) - @Disc;   
      if @hour = 13 set @hour13 = @hour13 + (@Pr*@Qty) - @Disc;   
      if @hour = 14 set @hour14 = @hour14 + (@Pr*@Qty) - @Disc;   
      if @hour = 15 set @hour15 = @hour15 + (@Pr*@Qty) - @Disc;   
      if @hour = 16 set @hour16 = @hour16 + (@Pr*@Qty) - @Disc;   
      if @hour = 17 set @hour17 = @hour17 + (@Pr*@Qty) - @Disc;   
      if @hour = 18 set @hour18 = @hour18 + (@Pr*@Qty) - @Disc;   
      if @hour = 19 set @hour19 = @hour19 + (@Pr*@Qty) - @Disc;   
      if @hour = 20 set @hour20 = @hour20 + (@Pr*@Qty) - @Disc;   
      if @hour = 21 set @hour21 = @hour21 + (@Pr*@Qty) - @Disc;   
      if @hour = 22 set @hour22 = @hour22 + (@Pr*@Qty) - @Disc;   
      if @hour = 23 set @hour23 = @hour23 + (@Pr*@Qty) - @Disc;   

	  end

	  if @tax_inclusive = 'Y' begin
      if @hour = 0 set @hour0 = @hour0 + (@tiPr*@Qty);   
      if @hour = 1 set @hour1 = @hour1 + (@tiPr*@Qty);   
      if @hour = 2 set @hour2 = @hour2 + (@tiPr*@Qty);   
      if @hour = 3 set @hour3 = @hour3 + (@tiPr*@Qty);   
      if @hour = 4 set @hour4 = @hour4 + (@tiPr*@Qty);   
      if @hour = 5 set @hour5 = @hour5 + (@tiPr*@Qty);   
      if @hour = 6 set @hour6 = @hour6 + (@tiPr*@Qty);   
      if @hour = 7 set @hour7 = @hour7 + (@tiPr*@Qty);   
      if @hour = 8 set @hour8 = @hour8 + (@tiPr*@Qty);   
      if @hour = 9 set @hour9 = @hour9 + (@tiPr*@Qty);   
      if @hour = 10 set @hour10 = @hour10 + (@tiPr*@Qty);   
      if @hour = 11 set @hour11 = @hour11 + (@tiPr*@Qty);   
      if @hour = 12 set @hour12 = @hour12 + (@tiPr*@Qty);   
      if @hour = 13 set @hour13 = @hour13 + (@tiPr*@Qty);   
      if @hour = 14 set @hour14 = @hour14 + (@tiPr*@Qty);   
      if @hour = 15 set @hour15 = @hour15 + (@tiPr*@Qty);   
      if @hour = 16 set @hour16 = @hour16 + (@tiPr*@Qty);   
      if @hour = 17 set @hour17 = @hour17 + (@tiPr*@Qty);   
      if @hour = 18 set @hour18 = @hour18 + (@tiPr*@Qty);   
      if @hour = 19 set @hour19 = @hour19 + (@tiPr*@Qty);   
      if @hour = 20 set @hour20 = @hour20 + (@tiPr*@Qty);   
      if @hour = 21 set @hour21 = @hour21 + (@tiPr*@Qty);   
      if @hour = 22 set @hour22 = @hour22 + (@tiPr*@Qty);   
      if @hour = 23 set @hour23 = @hour23 + (@tiPr*@Qty);   

	  end

      end
	   
      fetch next from csc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    end
    close csc1
    deallocate csc1 
  end

 
    insert into CloseOutSalesHour(Timeinterval,SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values  ('12:00 M -   1:00 AM',@hour0,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 1:00 AM -  2:00 AM',@hour1,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 2:00 AM -  3:00 AM',@hour2,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 3:00 AM -  4:00 AM',@hour3,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 4:00 AM -  5:00 AM',@hour4,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 5:00 AM -  6:00 AM',@hour5,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 6:00 AM -  7:00 AM',@hour6,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 7:00 AM -  8:00 AM',@hour7,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 8:00 AM -  9:00 AM',@hour8,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 9:00 AM - 10:00 AM',@hour9,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('10:00 AM - 11:00 AM',@hour10,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('11:00 AM -  12:00 N',@hour11,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('12:00 N -   1:00 PM',@hour12,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 1:00 PM -  2:00 PM',@hour13,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 2:00 PM -  3:00 PM',@hour14,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 3:00 PM -  4:00 PM',@hour15,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 4:00 PM -  5:00 PM',@hour16,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 5:00 PM -  6:00 PM',@hour17,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 6:00 PM -  7:00 PM',@hour18,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 7:00 PM -  8:00 PM',@hour19,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 8:00 PM -  9:00 PM',@hour20,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 9:00 PM - 10:00 PM',@hour21,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('10:00 PM - 11:00 PM',@hour22,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('11:00 PM -  12:00 M',@hour23,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 

  
   if @Sync = 'Y' begin

		insert into CentralExportCloseOutSalesHour(Timeinterval,SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values  ('12:00 M -   1:00 AM',@hour0,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 1:00 AM -  2:00 AM',@hour1,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 2:00 AM -  3:00 AM',@hour2,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 3:00 AM -  4:00 AM',@hour3,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 4:00 AM -  5:00 AM',@hour4,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 5:00 AM -  6:00 AM',@hour5,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName); 
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 6:00 AM -  7:00 AM',@hour6,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 7:00 AM -  8:00 AM',@hour7,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 8:00 AM -  9:00 AM',@hour8,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 9:00 AM - 10:00 AM',@hour9,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values ('10:00 AM - 11:00 AM',@hour10,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values ('11:00 AM -  12:00 N',@hour11,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName); 
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values ('12:00 N -   1:00 PM',@hour12,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 1:00 PM -  2:00 PM',@hour13,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 2:00 PM -  3:00 PM',@hour14,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 3:00 PM -  4:00 PM',@hour15,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 4:00 PM -  5:00 PM',@hour16,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 5:00 PM -  6:00 PM',@hour17,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName); 
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 6:00 PM -  7:00 PM',@hour18,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 7:00 PM -  8:00 PM',@hour19,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 8:00 PM -  9:00 PM',@hour20,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values (' 9:00 PM - 10:00 PM',@hour21,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values ('10:00 PM - 11:00 PM',@hour22,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,EmpName) values ('11:00 PM -  12:00 M',@hour23,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@EmpName); 

   end
     


end

GO

DROP PROCEDURE IF EXISTS [dbo].[sp_CloseoutReportHeader]
GO

CREATE procedure [dbo].[sp_CloseoutReportHeader]
			@tax_inclusive	char(1),
			@CloseoutType	char(1),
			@CloseoutID		int,
			@Terminal		nvarchar(50),
			@Sync			char(1)
as

declare @TaxedSales				numeric(18,3);
declare @NonTaxedSales			numeric(18,3);
declare @ServiceSales			numeric(18,3);
declare @ProductSales			numeric(18,3);
declare @OtherSales				numeric(18,3);
declare @DiscountItemNo			int;
declare @DiscountItemAmount		numeric(18,3);
declare @BDiscountItemNo		int;
declare @BDiscountItemAmount	numeric(18,3);
declare @SDiscountItemNo		int;
declare @SDiscountItemAmount	numeric(18,3);
declare @RDiscountItemNo		int;
declare @RDiscountItemAmount	numeric(18,3);
declare @DiscountInvoiceNo		int;
declare @DiscountInvoiceAmount	numeric(18,3);
declare @RDiscountInvoiceNo		int;
declare @RDiscountInvoiceAmount	numeric(18,3);
declare @RntDiscountInvoiceNo	int;
declare @RntDiscountInvoiceAmount	numeric(18,3);

declare @LayawayDeposits		numeric(18,3);
declare @LayawayRefund			numeric(18,3);
declare @LayawayPayment			numeric(18,3);
declare @LayawaySalesPosted		numeric(18,3);
declare @PaidOuts				numeric(18,3);
declare @GCsold					numeric(18,3);
declare @SCissued				numeric(18,3);
declare @SCredeemed				numeric(18,3);
declare @HACharged				numeric(18,3);
declare @HApayments				numeric(18,3);
declare @NoOfSales				int;
declare @NoOfRents				int;
declare @NoOfRepairs			int;
declare @PType				char(1);
declare @Pr					numeric(18,3);
declare @NPr				numeric(18,3);
declare @Qty				numeric(18,3);
declare @T1					char(1);
declare @T2					char(1);
declare @T3					char(1);
declare @T1r				numeric(18,3);
declare @T2r				numeric(18,3);
declare @T3r				numeric(18,3);
declare @Tx1  				numeric(18,3);
declare @Tx2				numeric(18,3);
declare @Tx3 				numeric(18,3);
declare @Tx1ty				int;
declare @Tx2ty				int;
declare @Tx3ty				int;
declare @Tx1Tot				numeric(18,3);
declare @Tx2Tot				numeric(18,3);
declare @Tx3Tot				numeric(18,3);
declare @RetrnItm			numeric(18,3);
declare @LayNo				int;
declare @Status				int;
declare @TransType			int;
declare @InvNo				int;
declare @TranNo				int;
declare @Tax1			numeric(18,3);
declare @Tax2			numeric(18,3);
declare @Tax3			numeric(18,3);
declare @LTax1			numeric(18,3);
declare @LTax2			numeric(18,3);
declare @LTax3			numeric(18,3);
declare @TaxAmt1		numeric(18,3);
declare @TaxAmt2		numeric(18,3);
declare @TaxAmt3		numeric(18,3);
declare @BTaxAmt1		numeric(18,3);
declare @BTaxAmt2		numeric(18,3);
declare @BTaxAmt3		numeric(18,3);
declare @STaxAmt1		numeric(18,3);
declare @STaxAmt2		numeric(18,3);
declare @STaxAmt3		numeric(18,3);
declare @RTaxAmt1		numeric(18,3);
declare @RTaxAmt2		numeric(18,3);
declare @RTaxAmt3		numeric(18,3);
declare @RntTaxAmt1		numeric(18,3);
declare @RntTaxAmt2		numeric(18,3);
declare @RntTaxAmt3		numeric(18,3);
declare @CpnPerc		numeric(18,3);
declare @Discnt			numeric(18,3);
declare @LDiscnt		numeric(18,3);
declare @LayAmount		numeric(18,3);
declare @TID			int;
declare @TAmount		numeric(18,3);
declare @r1				int;
declare @r2				int;
declare @r3				int;
declare @r4				int;
declare @r5				int;
declare @r6				int;
declare @r7				int;
declare @r8				int;
declare @r9				int;
declare @r10			int;
declare @r11			int;
declare @r12			int;
declare @tc				int;
declare @Tax1Name       nvarchar(20);
declare @Tax1Exist 		char(1);
declare @Tax2Name       nvarchar(20);
declare @Tax2Exist 		char(1);
declare @Tax3Name       nvarchar(20);
declare @Tax3Exist 		char(1);
declare @SD				datetime;
declare @ED				datetime;
declare @Notes			nvarchar(100);
declare @EmpID			nvarchar(12);
declare @CT				char(1);
declare @NS				int;
declare @count1			int;
declare @count2			int;
declare @CTeml			nvarchar(50);
declare @TotalSale		numeric(18,3);
declare @TTotalSale		numeric(18,3);
declare @TotalSales_PreTax 	numeric(18,3);
declare @CostOfGoods 		numeric(18,3);
declare @NoSaleCount		int;
declare @RentDuration		numeric(18,3);
declare @RentDeposit		numeric(18,3);
declare @TRentDeposit		numeric(18,3);
declare @TRentDepositReturned   numeric(18,3);
declare @RepairSales		numeric(18,3);
declare @RentSales	    	numeric(18,3);
declare @cost				numeric(18,3);
declare @UOMCount			numeric(18,3);
declare @SalesInvoiceCount	int;
declare @RentInvoiceCount	int;
declare @RepairInvoiceCount	int;
declare @RprPr				numeric(18,3);
declare @RprNPr				numeric(18,3);
declare @RprQty				numeric(18,3);
declare @RprT1				char(1);
declare @RprT2				char(1);
declare @RprT3				char(1);
declare @RprT1r				numeric(18,3);
declare @RprT2r				numeric(18,3);
declare @RprT3r				numeric(18,3);
declare @RprTx1  			numeric(18,3);
declare @RprTx2				numeric(18,3);
declare @RprTx3 			numeric(18,3);
declare @RprDiscnt			numeric(18,3);
declare @RprParentID		numeric(18,3);
declare @RentCalc			char(1);
declare @ProductTx			numeric(18,3);
declare @ProductNTx			numeric(18,3);
declare @ServiceTx			numeric(18,3);
declare @ServiceNTx			numeric(18,3);
declare @OtherTx			numeric(18,3);
declare @OtherNTx			numeric(18,3);
declare @tcld				int;
declare @fstender			char(1);
declare @invtx1				numeric(18,3);
declare @invtx2				numeric(18,3);
declare @invtx3				numeric(18,3);
declare @tcid				int;
declare @dtTipEnd			datetime;
declare @dtTipStart			datetime;
declare @Emp				int;
declare @CashTip			numeric(15,3);
declare @CCTip				numeric(15,3);
declare @TCashTip			numeric(15,3);
declare @TCCTip				numeric(15,3);
declare @AcceptTips			char(1);
declare @EmpCoutID			int;
declare @SalesFees			numeric(15,3);
declare @SalesFeesTax		numeric(15,3);
declare @RentFees			numeric(15,3);
declare @RentFeesTax		numeric(15,3);
declare @RepairFees			numeric(15,3);
declare @RepairFeesTax		numeric(15,3);
declare @Fees				numeric(15,3);
declare @FeesTax			numeric(15,3);
declare @DTax			    numeric(15,3);	
declare @TDTax			    numeric(15,3);
declare  @i_ProductID		int;
declare  @i_SKU				nvarchar(16);
declare  @MGCSold			numeric(15,2);
declare  @PGCSold			numeric(15,2);
declare  @DGCSold			numeric(15,2);
declare  @PLGCSold			numeric(15,2);
declare @BottleRefund		numeric(15,2);

declare @FreeQty			int;
declare @FreeAmount			numeric(15,3);
declare @FreeTag			char(1);

declare @LottoPayout		numeric(15,3);

declare @item_TaxIncludeRate		numeric(15,3);
declare @item_TaxIncludePrice		numeric(15,3);

declare @EmpName nvarchar(50);

declare @ConsolidatedID int;
declare @TransactionCnt int;
begin
  
  delete from CloseoutReportMain where ReportTerminalName = @Terminal

  select @CT=c.CloseoutType, @SD=c.StartDatetime, @ED=isnull(c.EndDatetime,getdate()), @Notes=c.Notes, 
  @EmpID = isnull(e.EmployeeID,'ADMIN') , @EmpName = e.FirstName + ' ' + e.LastName, @ConsolidatedID = c.ConsolidatedID,
  @CTeml = c.TerminalName, @TransactionCnt = c.TransactionCnt
  from closeout c left outer join employee e on e.ID = c.CreatedBy where c.ID = @CloseoutID

  if @Sync = 'Y' begin

		delete from CentralExportCloseOut where CloseoutID = @CloseoutID and CloseoutType = @CloseoutType;
		delete from CentralExportCloseOutReportMain where CloseoutID = @CloseoutID and CloseoutType = @CloseoutType;
		delete from CentralExportCloseOutReportTender where CloseoutID = @CloseoutID and CloseoutType = @CloseoutType;
		delete from CentralExportCloseOutReturn where CloseoutID = @CloseoutID and CloseoutType = @CloseoutType;
		delete from CentralExportCloseOutSalesDept where CloseoutID = @CloseoutID and CloseoutType = @CloseoutType;
		delete from CentralExportCloseOutSalesHour where CloseoutID = @CloseoutID and CloseoutType = @CloseoutType;

		insert into CentralExportCloseout(CloseoutID,CloseoutType,ConsolidatedID,StartDatetime,EndDatetime,
		Notes,TransactionCnt,EmployeeID,EmployeeName,TerminalName)
		values(@CloseoutID,@CloseoutType,@ConsolidatedID,@SD,@ED,@Notes,@TransactionCnt,@EmpID,@EmpName,@CTeml);
  end
  

  set @SalesInvoiceCount = 0;
  set @RentInvoiceCount = 0;
  set @RepairInvoiceCount = 0;
  
  set @TaxedSales = 0;
  set @NonTaxedSales = 0;
  set @ServiceSales = 0;
  set @ProductSales = 0;
  set @OtherSales = 0;

  set @DiscountItemNo = 0;
  set @DiscountItemAmount = 0;
  set @SDiscountItemNo = 0;
  set @SDiscountItemAmount = 0;
  set @BDiscountItemNo = 0;
  set @BDiscountItemAmount = 0;
  set @RDiscountItemNo = 0;
  set @RDiscountItemAmount = 0;
  
  set @DiscountInvoiceNo = 0;
  set @DiscountInvoiceAmount = 0;
  set @RDiscountInvoiceNo = 0;
  set @RDiscountInvoiceAmount = 0;
  set @RntDiscountInvoiceNo = 0;
  set @RntDiscountInvoiceAmount = 0;
  
  set @LayawayDeposits = 0;
  set @LayawayRefund = 0;
  set @LayawaySalesPosted = 0;
  set @LayawayPayment = 0;

  set @PaidOuts = 0;
  set @GCsold = 0;
  set @SCissued = 0;
  set @SCredeemed = 0;
  set @HACharged = 0;
  set @HApayments = 0;

  set @Tax1 = 0;
  set @Tax2 = 0;
  set @Tax3 = 0;

  set @LTax1 = 0;
  set @LTax2 = 0;
  set @LTax3 = 0;

  set @TaxAmt1 = 0;
  set @TaxAmt2 = 0;
  set @TaxAmt3 = 0;
  
  set @BTaxAmt1 = 0;
  set @BTaxAmt2 = 0;
  set @BTaxAmt3 = 0;
  
  set @STaxAmt1 = 0;
  set @STaxAmt2 = 0;
  set @STaxAmt3 = 0;
  
  set @RTaxAmt1 = 0;
  set @RTaxAmt2 = 0;
  set @RTaxAmt3 = 0;
    
  set @RntTaxAmt1 = 0;
  set @RntTaxAmt2 = 0;
  set @RntTaxAmt3 = 0;
  
  set @LayAmount = 0;

  set @NoOfSales = 0;
  set @NoOfRepairs = 0;
  set @NoOfRents = 0;

  set @Tax1Exist = 'N';
  set @Tax2Exist = 'N';
  set @Tax3Exist = 'N';

  set @TTotalSale = 0;
  set @CostOfGoods = 0;

  set @NoSaleCount = 0;
    
  set @TRentDeposit = 0;
  set @RentSales = 0;
  set @RepairSales = 0;
  set @TRentDepositReturned = 0;
  
  set @CpnPerc = 0;
  
  set @ProductTx	= 0;
  set @ProductNTx	= 0;
  set @ServiceTx	= 0;
  set @ServiceNTx	= 0;
  set @OtherTx		= 0;
  set @OtherNTx		= 0;


  set @tcld = 0;

  set  @CashTip = 0;
  set  @CCTip = 0;
  
  set  @TCashTip = 0;
  set  @TCCTip = 0;

  set  @AcceptTips = 'N';
  
  
  set @SalesFees		= 0;
  set  @SalesFeesTax	= 0;

  set @RentFees			= 0;
  set @RentFeesTax		= 0;

  set @RepairFees		= 0;
  set @RepairFeesTax	= 0;

  set @TDTax = 0;
  
  set @MGCSold = 0;
  set @PGCSold = 0;
  set @DGCSold = 0;
  set @PLGCSold = 0;
  
  set @BottleRefund = 0;

  set @FreeQty = 0;
  set @FreeAmount = 0;

  set @LottoPayout = 0;

  select @AcceptTips = AcceptTips from Setup;

  if @CloseoutType = 'E' or @CloseoutType = 'T' begin
 
    declare sc1 cursor
    for select inv.ID, t.ID, i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,i.ReturnedItemCnt,
    inv.Status,inv.LayawayNo, t.TransType,inv.TotalSale,i.cost,i.UOMCount,i.Discount,i.RentDuration,inv.RentDeposit,
    i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.IsRentCalculated,inv.CouponPerc,i.TaxType1,i.TaxType2,i.TaxType3,
    i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,inv.Tax1,inv.Tax2,inv.Tax3,i.Fees,i.FeesTax,i.DTax,i.ProductID,i.SKU,i.BuyNGetFreeCategory,
	i.TaxIncludeRate,i.TaxIncludePrice
    
    from item i left outer join invoice inv on i.invoiceNo=inv.ID left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.ID not in 
    ( select invoiceno from VoidInv) and i.Tagged <> 'X'	  
    open sc1
    fetch next from sc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,
    @TransType,@TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,
    @CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,
    @DTax,@i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice
    
    while @@fetch_status = 0 begin
    
      if @Status = 16 and @RentCalc = 'Y' set @Qty = -@Qty;
      
      set @Tx1 = 0;
      set @Tx2 = 0;
      set @Tx3 = 0;

      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
      begin
         if @T1='Y' begin
           if @Tx1ty = 0 begin
		     if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @Discnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
			 if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
		   end
           if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
         end
         if @T2='Y' begin
            if @Tx2ty = 0 begin
			  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @Discnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
			  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
			end
            if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
         end   
         if @T3='Y' begin
            if @Tx3ty = 0 begin
			  if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @Discnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
			  if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
		    end
            if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
         end   
      end         

      if @PType = 'G' and @TransType = 1  /* gift cert sales */
      begin
        set @GCsold = @GCsold + @Pr*@Qty;
      end   
      
      if @PType = 'O' and @TransType = 1  /* bottle refund */
      begin
        set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
      end   
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
      begin
        set @MGCSold = @MGCSold + @Pr;
      end  
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
      begin
        set @PGCSold = @PGCSold + @Pr;
      end 

      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
      begin
        set @DGCSold = @DGCSold + @Pr;
      end 
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
      begin
        set @PLGCSold = @PLGCSold + @Pr;
      end
    
      if @PType = 'A' and @TransType = 1  /* house account payment */
      begin
        set @HApayments = @HApayments + @Pr*@Qty;
      end  

	  if @PType = 'H' and @TransType = 1
	  begin
	    if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
        if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 15 and @RentCalc = 'N'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 16 and @RentCalc = 'Y'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end

      if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'H' and @PType <> 'S' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
      begin

	    if @FreeTag = 'F' begin			/* Buy 'n Get Free */
		   set @FreeQty = @FreeQty + @Qty;
		   set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
		end

        if @PType = 'U'					/* cost of goods sold */
        begin
	      set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
        end

        if @PType <> 'U'
        begin
	      set @CostOfGoods = @CostOfGoods + @cost*@Qty;
        end

        if @PType = 'B'					/* other sales */
        begin
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
		    if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          
          if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @Discnt;
		  if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

		  /*if @tax_inclusive = 'Y' begin
		     set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          
          set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
          set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
          set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
          if @Discnt <> 0					/* Discount on item */
          begin
            set @BDiscountItemNo = @BDiscountItemNo + 1;
            set @BDiscountItemAmount = @BDiscountItemAmount + @Discnt;
          end
          
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
          
        end

        if @PType <> 'B'				/*  product sales */
        begin
        
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          
          if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @Discnt;
		  if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty);

		  /*if @tax_inclusive = 'Y' begin
		     set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
		  
          
          set @TaxAmt1 = @TaxAmt1 + @Tx1;
          set @TaxAmt2 = @TaxAmt2 + @Tx2;
          set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
          if @Discnt <> 0					/* Discount on item */
          begin
            set @DiscountItemNo = @DiscountItemNo + 1;
            set @DiscountItemAmount = @DiscountItemAmount + @Discnt;
          end
          
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
          
        end
        
        if @DTax <> 0 set @TDTax = @TDTax + @DTax;

      end 
    
      if @PType = 'S' and @TransType = 1		/* service sales */
      begin
        if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt;
			if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty);
			/*if @tax_inclusive = 'Y' begin
		     set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/ 
        end
        else begin
          if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty); 
		  /*if @tax_inclusive = 'Y' begin
		     set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
        end

        if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @Discnt;
		if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

		/*if @tax_inclusive = 'Y' begin
		     set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/

        set @STaxAmt1 = @STaxAmt1 + @Tx1;
        set @STaxAmt2 = @STaxAmt2 + @Tx2;
        set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
        if @Discnt <> 0					/* Discount on item */
        begin
          set @SDiscountItemNo = @SDiscountItemNo + 1;
          set @SDiscountItemAmount = @SDiscountItemAmount + @Discnt;
        end
        
        if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
          
      end
     
      if @Status = 15 begin
        
        set @TRentDeposit = @TRentDeposit + @RentDeposit;
        
        if @RentCalc = 'N' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (@item_TaxIncludePrice);

		  /*if @tax_inclusive = 'Y' begin
		     set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/

          set @RntTaxAmt1 = @RntTaxAmt1 + @Tx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @Tx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @Tx3;
          
          if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
          
        end
      end
      
      if @Status = 16 begin
        if @RentCalc = 'N' begin
		  set @TRentDepositReturned = @TRentDepositReturned + (-@TotalSale);
		end
        if @RentCalc = 'Y' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (-@item_TaxIncludeRate); 
		  /*if @tax_inclusive = 'Y' begin
		     set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/

          set @RntTaxAmt1 = @RntTaxAmt1 + @invtx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @invtx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @invtx3;
          set @TRentDepositReturned = @TRentDepositReturned + @RentDeposit;
          
          if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
          
        end
      end
     
      
     
    fetch next from sc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,
    @TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,
    @Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,@DTax,@i_ProductID,@i_SKU,@FreeTag,
	@item_TaxIncludeRate,@item_TaxIncludePrice

    end
    close sc1
    deallocate sc1 
    
    
    declare scrpr1 cursor
    for select inv.RepairParentID from invoice inv left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID 
    and inv.ID not in ( select invoiceno from VoidInv) and inv.Status = 18
			  
    open scrpr1
    fetch next from scrpr1 into @RprParentID
    
    while @@fetch_status = 0 begin
    
        
    
        declare scrpr cursor
        for select i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,i.Discount,i.TaxRate1,i.TaxRate2,i.TaxRate3,
        inv.CouponPerc,i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,
		i.TaxIncludeRate, i.TaxIncludePrice
        from item i left outer join invoice inv on i.invoiceNo=inv.ID where inv.ID not in ( select invoiceno from VoidInv) and i.Tagged <> 'X'
        and inv.ID = @RprParentID
			  
       open scrpr
       fetch next from scrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
       @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
    
       while @@fetch_status = 0 begin
       
           set @RprTx1 = 0;
		   set @RprTx2 = 0;
           set @RprTx3 = 0;

           if (@RprT1='Y' or @RprT2='Y'or @RprT3='Y') and (@fstender = 'N')
           begin
             if @RprT1='Y' begin
               if @Tx1ty = 0 set @RprTx1 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT1r/100)*(100 - @CpnPerc)/100,2);
               if @Tx1ty = 1 set @RprTx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT2='Y' begin
               if @Tx2ty = 0 set @RprTx2 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT2r/100)*(100 - @CpnPerc)/100,2);
               if @Tx2ty = 1 set @RprTx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT3='Y' begin
               if @Tx3ty = 0 set @RprTx3 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT3r/100)*(100 - @CpnPerc)/100,2);
               if @Tx3ty = 1 set @RprTx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
             end
           end         
                     
            
            if @tax_inclusive = 'N' set @RepairSales = @RepairSales + (@RprPr*@RprQty) - @RprDiscnt;
			if @tax_inclusive = 'Y' set @RepairSales = @RepairSales + (@item_TaxIncludeRate);
					
			 
            set @RTaxAmt1 = @RTaxAmt1 + @RprTx1;
            set @RTaxAmt2 = @RTaxAmt2 + @RprTx2;
            set @RTaxAmt3 = @RTaxAmt3 + @RprTx3;
        
           if @RprDiscnt <> 0					/* Discount on item */
           begin
             set @RDiscountItemNo = @RDiscountItemNo + 1;
             set @RDiscountItemAmount = @RDiscountItemAmount + @RprDiscnt;
           end
           
           if @Fees <> 0 set @RepairFees = @RepairFees + @Fees;
           if @FeesTax <> 0 set @RepairFeesTax = @RepairFeesTax + @FeesTax;
          
           fetch next from scrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
           @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
        end
        close scrpr
        deallocate scrpr 
     
     fetch next from scrpr1 into @RprParentID
     
     end
     close scrpr1
     deallocate scrpr1     
    
    
    declare sc2 cursor
    for select inv.ID,t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status,inv.LayawayNo, t.TransType, inv.Coupon,inv.IsRentCalculated
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @CloseoutID and inv.ID not in ( select invoiceno from VoidInv)

    open sc2
    fetch next from sc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc
    while @@fetch_status = 0 begin

      if @LayNo = 0  /* non layaway items */

      begin

        if @Status = 3 set @NoOfSales = @NoOfSales + 1;
        
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1;
        
        if @Status = 15 or @Status = 16 set @RentInvoiceCount = @RentInvoiceCount + 1;
        
        if @Status = 18 set @RepairInvoiceCount = @RepairInvoiceCount + 1;
        
		

        if (@Discnt > 0) begin
          if @Status = 3 begin
            set @DiscountInvoiceNo = @DiscountInvoiceNo + 1;
            set @DiscountInvoiceAmount = @DiscountInvoiceAmount + @Discnt;
          end
          if (@Status = 15 and @RentCalc = 'N') or (@Status = 16 and @RentCalc = 'Y') begin
            set @RntDiscountInvoiceNo = @RntDiscountInvoiceNo + 1;
            set @RntDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
          if @Status = 18 begin
            set @RDiscountInvoiceNo = @RDiscountInvoiceNo + 1;
            set @RDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
        end

      end  

      if @LayNo > 0 and @TransType = 4 and @Status = 3  begin  /* layaway items */ 

        set @NoOfSales = @NoOfSales + 1;     
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1; 
     
      end     
           
     fetch next from sc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc

    end

    close sc2
    deallocate sc2 

    
    declare sc3 cursor
    for select inv.ID, t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status, t.TransType, inv.Coupon
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @CloseoutID and inv.LayawayNo > 0 and inv.ID not in ( select invoiceno from VoidInv)

    open sc3
    fetch next from sc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt
    while @@fetch_status = 0 begin

    if @TransType = 2  /* layaway Deposit */
    begin    
        set @LayAmount = 0;
        select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
        set @LayawayDeposits = @LayawayDeposits + @LayAmount;
    end   

    if @TransType = 4  /* layaway Payment */

    begin
      
        if @Status = 1 or @Status = 3 

        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno <> @TranNo and invoiceno = @InvNo
		  set @LayawayDeposits = @LayawayDeposits + @LayAmount;
		     
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayPayment = @LayawayPayment + @LayAmount;
          
		  
          if @Status = 3 begin /* layaway sales */

             set @LayawaySalesPosted = @LayawaySalesPosted + @LayAmount; 

             declare sc4 cursor
             for select i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2, i.Taxable3,
             inv.Tax1, inv.Tax2, inv.Tax3, i.Discount,i.cost,i.UOMCount,i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.CouponPerc,
             i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,i.DTax,
			 i.ProductID,i.SKU,i.BuyNGetFreeCategory,i.TaxIncludeRate,i.TaxIncludePrice from item i 
             left outer join invoice inv on i.invoiceNo=inv.ID
             left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.ID=@InvNo and i.Tagged <> 'X'

             open sc4
             fetch next from sc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,
             @T1r,@T2r,@T3r,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,
			 @i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice
             while @@fetch_status = 0 begin
                 set @Tx1 = 0;
				 set @Tx2 = 0;
                 set @Tx3 = 0;


				 if @FreeTag = 'F' begin			/* Buy 'n Get Free */
					set @FreeQty = @FreeQty + @Qty;
					set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
				 end

                 if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
                 begin
                  if @T1='Y' begin
                    if @Tx1ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @LDiscnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
                  end 
				  if @T2='Y' begin
                    if @Tx2ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @LDiscnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
                  end 
                  if @T3='Y' begin
                    if @Tx3ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @LDiscnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
                  end 
                 end         
               
				 if @PType = 'G' and @TransType = 1  /* gift cert sales */
			     begin
			       set @GCsold = @GCsold + @Pr*@Qty;
		         end   
		         
		         if @PType = 'O' and @TransType = 1  /* bottle refund */
                 begin
                    set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
                 end  
		         
		        if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
				begin
					set @MGCSold = @MGCSold + @Pr;
				end 
				
				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
				begin
					set @PGCSold = @PGCSold + @Pr;
				end 
                       			 if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
				begin
					set @DGCSold = @DGCSold + @Pr;
				end 
				 if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
				begin
					set @PLGCSold = @PLGCSold + @Pr;
				end 

                 if @PType = 'A' and @TransType = 1  /* house account payment */
                 begin
                   set @HApayments = @HApayments + @Pr*@Qty;
                 end  

                if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'S'  and @PType <> 'H' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
                begin

                  if @PType = 'U'					/* cost of goods sold */
	              begin
					set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
	        	  end
	
        	      if @PType <> 'U'
	              begin
  				    set @CostOfGoods = @CostOfGoods + @cost*@Qty;
	              end
	              
	              
	              if @PType = 'B'					/* other sales */
                  begin
                  
                    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
                      if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty); 
					  /*if @tax_inclusive = 'Y' begin
		                set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    else begin
                      if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt;
					  if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty);
					  /*if @tax_inclusive = 'Y' begin
		                set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    
					if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @LDiscnt;
					if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

					/*if @tax_inclusive = 'Y' begin
						set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					end*/
          
					set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
					set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
					set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @BDiscountItemNo = @BDiscountItemNo + 1;
						set @BDiscountItemAmount = @BDiscountItemAmount + @LDiscnt;
					end
					
					if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
                    if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				  end

                  if @PType <> 'B'				/*  product sales */
				  begin
				  
				    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
                      if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
					  /*if @tax_inclusive = 'Y' begin
		                set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    else begin
                      if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
					  /*if @tax_inclusive = 'Y' begin
		                set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    
                    if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @LDiscnt; 
					if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty);

					/*if @tax_inclusive = 'Y' begin
						set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					end*/
          
					set @TaxAmt1 = @TaxAmt1 + @Tx1;
					set @TaxAmt2 = @TaxAmt2 + @Tx2;
					set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @DiscountItemNo = @DiscountItemNo + 1;
						set @DiscountItemAmount = @DiscountItemAmount + @LDiscnt;
					end
                    if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
                    if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				   end
				   
				   if @DTax <> 0 set @TDTax = @TDTax + @DTax;
				   
				end
               
                if @PType = 'S' and @TransType = 1		/* service sales */
			    begin
			    
			      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
                    if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt;
					if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty);
					/*if @tax_inclusive = 'Y' begin
		                set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/  
                  end
                  else begin
                    if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt;
					if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty);
					/*if @tax_inclusive = 'Y' begin
		                set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/  
                  end
          
                  if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @LDiscnt;
				  if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

				  /*if @tax_inclusive = 'Y' begin
						set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
				  end*/

                  set @STaxAmt1 = @STaxAmt1 + @Tx1;
				  set @STaxAmt2 = @STaxAmt2 + @Tx2;
                  set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
                  if @LDiscnt <> 0					/* Discount on item */
                  begin
                    set @SDiscountItemNo = @SDiscountItemNo + 1;
                    set @SDiscountItemAmount = @SDiscountItemAmount + @LDiscnt;
                  end
                  
                  if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
                  if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
                end

             
     
            fetch next from sc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,@T1r,@T2r,
            @T3r,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,@i_ProductID,
			@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice

          end
          close sc4
          deallocate sc4
          
          end

        end

      end 

      if @TransType = 3  /* layaway Payment */
      begin 

        if @Status = 5  /* layaway Refund */
        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayRefund = @LayawayRefund + @LayAmount;

        end

      end 
           
     fetch next from sc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt

    end
    close sc3
    deallocate sc3 
    
    
    
    select @TaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
        
    select @RntTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt1 = @RntTaxAmt1 + @STaxAmt1;
    
    select @RntTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt2 = @RntTaxAmt2 + @STaxAmt2;
    
    select @RntTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt3 = @RntTaxAmt3 + @STaxAmt3;
    
    select @RTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)

  /* No Sale */

  select @NoSaleCount = count(*) from trans where TransType = 5 and CloseOutID = @CloseoutID

      /*  Paid outs */

  select @PaidOuts = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 6 and tr.CloseOutID = @CloseoutID

  /*  Lotto Payout */

  select @LottoPayout = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 60 and tr.CloseOutID = @CloseoutID


  /*  House account charged */

  select @HACharged = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='House Account' and inv.ID not in ( select invoiceno from VoidInv);

  /*
  /*  Store Credit redeemed */

  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='Store Credit ' and t.TenderAmount > 0 and inv.ID not in ( select invoiceno from VoidInv);

  /*  Store Credit issued*/

  select @SCissued = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='Store Credit ' and t.TenderAmount < 0 and inv.ID not in ( select invoiceno from VoidInv);

  */

  /*  Store Credit issued*/

  select @SCissued = isnull(sum(StoreCreditAmount),0) from trans where CloseOutID = @CloseoutID and TransType in (50,51);
  
  
  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='Store Credit' and inv.ID not in ( select invoiceno from VoidInv);


    set @tc = 0;

    declare scT cursor
    for select isnull(tx1.TaxName,''),isnull(tx2.TaxName,''),isnull(tx3.TaxName,'')
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               left outer join TaxHeader tx1 on tx1.ID = inv.TaxID1 
               left outer join TaxHeader tx2 on tx2.ID = inv.TaxID2 
               left outer join TaxHeader tx3 on tx3.ID = inv.TaxID3 
               where t.CloseOutID = @CloseoutID and inv.ID not in (select invoiceno from VoidInv)
               and (inv.TaxID1 > 0 or inv.TaxID2 > 0 or inv.TaxID3 > 0)

    open scT
    fetch next from scT into @Tax1Name,@Tax2Name,@Tax3Name
    while @@fetch_status = 0 begin

	  if @Tax1Name <> '' or  @Tax2Name <> '' or @Tax1Name <> ''
      begin
        if @Tax1Name <> '' set  @Tax1Exist = 'Y';
        if @Tax2Name <> '' set  @Tax2Exist = 'Y';
        if @Tax3Name <> '' set  @Tax3Exist = 'Y';
     end      
     fetch next from scT into @Tax1Name,@Tax2Name,@Tax3Name

    end

    close scT
    deallocate scT


    if @AcceptTips = 'Y' begin

      if @CloseoutType = 'E' begin

        select @dtTipEnd = enddatetime, @Emp = createdby from closeout where ID = @CloseoutID;
        if @dtTipEnd is null select @dtTipEnd = max(dayend) from attendanceinfo where empid = @Emp;
        
        select @dtTipStart = max(enddatetime) from closeout where createdby = @Emp and closeouttype = 'E' and ID < @CloseoutID;
 
        if @dtTipStart is not null begin
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and dayend 
           between @dtTipStart and @dtTipEnd;	
        end

        if @dtTipStart is null begin
        
           select @dtTipStart = max(daystart) from attendanceinfo where empid = @Emp and daystart <= @dtTipEnd;
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and 
           dayend between @dtTipStart and @dtTipEnd;	
        end

      end


      if @CloseoutType = 'T' begin


        declare sth cursor
        for select distinct t.EmployeeID from trans t left outer join employee  e on t.EmployeeID = e.ID 
        where t.CloseOutID = @CloseoutID and t.EmployeeID > 0 and t.TerminalName = @Terminal
        open sth
        fetch next from sth into @Emp
   
        while @@fetch_status = 0 begin
   
          select @dtTipEnd = enddatetime from closeout where ID = @CloseoutID;

          select @dtTipStart = max(enddatetime) from closeout where closeouttype = 'T' and ID < @CloseoutID;

          if @dtTipStart is not null begin
            select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and 
            dayend between @dtTipStart and @dtTipEnd;
          end

          if @dtTipStart is null begin
            select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp 
            and dayend <= @dtTipEnd ;
          end
          

          fetch next from sth into @Emp

        end
       
        close sth 
        deallocate sth
       
       
       set  @CashTip = 0;
       set @CCTip = 0;

      end
    
    end

  


    insert into CloseoutReportMain (TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,
				ProductSales,OtherSales,DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,
				DiscountInvoiceAmount,LayawayDeposits,LayawayRefund,LayawayPayment,LayawaySalesPosted,
				GCsold,HApayments,PaidOuts,HACharged,SCissued,SCredeemed,NoOfSales,
				Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,CloseoutID,StartDateTime,EndDateTime,CloseoutType,
				Notes,EmpID,TerminalName,ReportTerminalName,TotalSales_PreTax,CostOfGoods,
				NoSaleCount,RentSales,RentDeposit,RentDepositReturned,RentTax1Amount,RentTax2Amount,RentTax3Amount,
				RentDiscountInvoiceNo,RentDiscountInvoiceAmount,RepairSales,RepairTax1Amount,RepairTax2Amount,RepairTax3Amount,
				RepairDiscountItemNo,RepairDiscountItemAmount,RepairDiscountInvoiceNo,RepairDiscountInvoiceAmount,ServiceTax1Amount,ServiceTax2Amount,
				ServiceTax3Amount,ServiceDiscountItemNo,ServiceDiscountItemAmount,OtherTax1Amount,OtherTax2Amount,OtherTax3Amount,
				OtherDiscountItemNo,OtherDiscountItemAmount,SalesInvoiceCount,RentInvoiceCount,RepairInvoiceCount,
				ProductTx,ProductNTx,ServiceTx,ServiceNTx,OtherTx,OtherNTx,CashTip,CCTip,
				SalesFees,SalesFeesTax,RentFees,RentFeesTax,RepairFees,RepairFeesTax,DTax,MGCsold,PGCsold,BottleRefund,
				DGCsold,PLGCsold,Free_Qty,Free_Amount,LottoPayout) 
				values
                (@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,
				@ProductSales,@OtherSales,@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,
				@DiscountInvoiceAmount,@LayawayDeposits,@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,
				@GCsold,@HApayments,@PaidOuts,@HACharged,@SCissued,@SCredeemed,@NoOfSales,
				@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
				@CloseoutID,@SD,@ED,@CT,@Notes,@EmpID,@CTeml,@Terminal,@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@NoSaleCount,
				@RentSales,@TRentDeposit,@TRentDepositReturned,@RntTaxAmt1,@RntTaxAmt2,@RntTaxAmt3,@RntDiscountInvoiceNo,@RntDiscountInvoiceAmount,
				@RepairSales,@RTaxAmt1,@RTaxAmt2,@RTaxAmt3,@RDiscountItemNo,@RDiscountItemAmount,@RDiscountInvoiceNo,
				@RDiscountInvoiceAmount,@STaxAmt1,@STaxAmt2,@STaxAmt3,@SDiscountItemNo,@SDiscountItemAmount,@BTaxAmt1,@BTaxAmt2,
				@BTaxAmt3,@BDiscountItemNo,@BDiscountItemAmount,@SalesInvoiceCount,@RentInvoiceCount,@RepairInvoiceCount,
				@ProductTx,@ProductNTx,@ServiceTx,@ServiceNTx,@OtherTx,@OtherNTx,@CashTip,@CCTip,
				@SalesFees,@SalesFeesTax,@RentFees,@RentFeesTax,@RepairFees,@RepairFeesTax,@TDTax,@MGCSold,@PGCSold,
				@BottleRefund,@DGCsold,@PLGCsold,@FreeQty,@FreeAmount,@LottoPayout)  


	if @Sync = 'Y'
	   insert into CentralExportCloseoutReportMain (TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,
				ProductSales,OtherSales,DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,
				DiscountInvoiceAmount,LayawayDeposits,LayawayRefund,LayawayPayment,LayawaySalesPosted,
				GCsold,HApayments,PaidOuts,HACharged,SCissued,SCredeemed,NoOfSales,
				Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,CloseoutID,StartDateTime,EndDateTime,CloseoutType,
				Notes,EmpID,TerminalName,EmpName,TotalSales_PreTax,CostOfGoods,
				NoSaleCount,RentSales,RentDeposit,RentDepositReturned,RentTax1Amount,RentTax2Amount,RentTax3Amount,
				RentDiscountInvoiceNo,RentDiscountInvoiceAmount,RepairSales,RepairTax1Amount,RepairTax2Amount,RepairTax3Amount,
				RepairDiscountItemNo,RepairDiscountItemAmount,RepairDiscountInvoiceNo,RepairDiscountInvoiceAmount,ServiceTax1Amount,ServiceTax2Amount,
				ServiceTax3Amount,ServiceDiscountItemNo,ServiceDiscountItemAmount,OtherTax1Amount,OtherTax2Amount,OtherTax3Amount,
				OtherDiscountItemNo,OtherDiscountItemAmount,SalesInvoiceCount,RentInvoiceCount,RepairInvoiceCount,
				ProductTx,ProductNTx,ServiceTx,ServiceNTx,OtherTx,OtherNTx,CashTip,CCTip,
				SalesFees,SalesFeesTax,RentFees,RentFeesTax,RepairFees,RepairFeesTax,DTax,MGCsold,PGCsold,BottleRefund,
				DGCsold,PLGCsold,Free_Qty,Free_Amount,LottoPayout) 
				values
                (@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,
				@ProductSales,@OtherSales,@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,
				@DiscountInvoiceAmount,@LayawayDeposits,@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,
				@GCsold,@HApayments,@PaidOuts,@HACharged,@SCissued,@SCredeemed,@NoOfSales,
				@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
				@CloseoutID,@SD,@ED,@CT,@Notes,@EmpID,@CTeml,@EmpName,@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@NoSaleCount,
				@RentSales,@TRentDeposit,@TRentDepositReturned,@RntTaxAmt1,@RntTaxAmt2,@RntTaxAmt3,@RntDiscountInvoiceNo,@RntDiscountInvoiceAmount,
				@RepairSales,@RTaxAmt1,@RTaxAmt2,@RTaxAmt3,@RDiscountItemNo,@RDiscountItemAmount,@RDiscountInvoiceNo,
				@RDiscountInvoiceAmount,@STaxAmt1,@STaxAmt2,@STaxAmt3,@SDiscountItemNo,@SDiscountItemAmount,@BTaxAmt1,@BTaxAmt2,
				@BTaxAmt3,@BDiscountItemNo,@BDiscountItemAmount,@SalesInvoiceCount,@RentInvoiceCount,@RepairInvoiceCount,
				@ProductTx,@ProductNTx,@ServiceTx,@ServiceNTx,@OtherTx,@OtherNTx,@CashTip,@CCTip,
				@SalesFees,@SalesFeesTax,@RentFees,@RentFeesTax,@RepairFees,@RepairFeesTax,@TDTax,@MGCSold,@PGCSold,
				@BottleRefund,@DGCsold,@PLGCsold,@FreeQty,@FreeAmount,@LottoPayout)  
  
     exec @r1 = sp_CloseoutReportReturnItem @CloseoutType , @CloseoutID,@Terminal,@Sync

     exec @r2 = sp_CloseoutReportTender @CloseoutType , @CloseoutID,@Terminal,@Sync

     exec @r3 = sp_CloseoutSalesByHour @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal,@Sync

     exec @r4 = sp_CloseoutSalesByDept @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal,@Sync 

  
  end  



  

  if @CloseoutType = 'C' begin
 
    declare csc1 cursor
    for select inv.ID, t.ID, i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,
               i.ReturnedItemCnt,inv.Status,inv.LayawayNo, t.TransType,inv.TotalSale,i.cost,i.UOMCount,i.Discount,
               i.RentDuration,inv.RentDeposit,i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.IsRentCalculated,inv.CouponPerc,
               i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,
               inv.Tax1,inv.Tax2,inv.Tax3,i.Fees,i.FeesTax,i.DTax,i.ProductID,i.SKU,i.BuyNGetFreeCategory,
			   i.TaxIncludeRate,i.TaxIncludePrice from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
			   and inv.ID not in ( select invoiceno from VoidInv) and i.Tagged <> 'X'

    open csc1
    fetch next from csc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,
    @TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,@CpnPerc,
    @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,@DTax,@i_ProductID,
	@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice

    while @@fetch_status = 0 begin

      if @Status = 16 and @RentCalc = 'Y' set @Qty = -@Qty;
      set @Tx1 = 0;
      set @Tx2 = 0;
      set @Tx3 = 0;

      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
      begin
         if @T1='Y' begin
           if @Tx1ty = 0 begin
		     if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @Discnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
			 if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
		   end
           if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
         end
         if @T2='Y' begin
            if @Tx2ty = 0 begin
			  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @Discnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
			  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
			end
            if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
         end   
         if @T3='Y' begin
            if @Tx3ty = 0 begin
			 if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @Discnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
			 if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
			end
            if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
         end 
      end 

      if @PType = 'G' and @TransType = 1  /* gift cert sales */
      begin
      
        set @GCsold = @GCsold + @Pr*@Qty;

      end   
      
      
      if @PType = 'O' and @TransType = 1  /* bottle refund */
      begin
        set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
      end  
      
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
      begin
        set @MGCSold = @MGCSold + @Pr;
      end 
      
	  if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
      begin
        set @PGCSold = @PGCSold + @Pr;
      end

      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
      begin
        set @DGCSold = @DGCSold + @Pr;
      end

      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
      begin
        set @PLGCSold = @PLGCSold + @Pr;
      end
      
      if @PType = 'A' and @TransType = 1  /* house account payment */
      begin
        set @HApayments = @HApayments + @Pr*@Qty;
      end  

      if @PType = 'H' and @TransType = 1
	  begin
	    if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
        if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 15 and @RentCalc = 'N'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 16 and @RentCalc = 'Y'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end


      if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'S' and @PType <> 'H' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
      begin

	    if @FreeTag = 'F' begin			/* Buy 'n Get Free */
		  set @FreeQty = @FreeQty + @Qty;
		  set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
		end

	    if @PType = 'U'					/* cost of goods sold */
	    begin
		  set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
        end
	
        if @PType <> 'U'
	    begin
		  set @CostOfGoods = @CostOfGoods + @cost*@Qty;
	    end

        if @PType = 'B'					/* other sales */
        begin
        
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
        
          if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @Discnt;
		  if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

		  /*if @tax_inclusive = 'Y' begin
			set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
	      end*/
          
          set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
          set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
          set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
          if @Discnt <> 0					/* Discount on item */
          begin
            set @BDiscountItemNo = @BDiscountItemNo + 1;
            set @BDiscountItemAmount = @BDiscountItemAmount + @Discnt;
          end
          
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
        end

        if @PType <> 'B'				/*  product sales */
        begin
        
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          
          if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty); 

		  /*if @tax_inclusive = 'Y' begin
			set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          
          set @TaxAmt1 = @TaxAmt1 + @Tx1;
          set @TaxAmt2 = @TaxAmt2 + @Tx2;
          set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
          if @Discnt <> 0					/* Discount on item */
          begin
            set @DiscountItemNo = @DiscountItemNo + 1;
            set @DiscountItemAmount = @DiscountItemAmount + @Discnt;
          end
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
        end
        
        if @DTax <> 0 set @TDTax = @TDTax + @DTax;

      end 


      if @PType = 'S' and @TransType = 1		/* service sales */
      begin
        
        if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
          if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty); 
		  /*if @tax_inclusive = 'Y' begin
		       set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
        end
        else begin
          if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty); 
		  /*if @tax_inclusive = 'Y' begin
		       set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
        end
          
        if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @Discnt;
		if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

		/*if @tax_inclusive = 'Y' begin
			set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		end*/

        set @STaxAmt1 = @STaxAmt1 + @Tx1;
        set @STaxAmt2 = @STaxAmt2 + @Tx2;
        set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
        if @Discnt <> 0					/* Discount on item */
        begin
          set @SDiscountItemNo = @SDiscountItemNo + 1;
          set @SDiscountItemAmount = @SDiscountItemAmount + @Discnt;
        end
        if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
      end
     
      if @Status = 15 begin
      
        set @TRentDeposit = @TRentDeposit + @RentDeposit;
        
        if @RentCalc = 'N' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (@item_TaxIncludePrice); 
		  /*if @tax_inclusive = 'Y' begin
			set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          set @RntTaxAmt1 = @RntTaxAmt1 + @Tx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @Tx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @Tx3;
          
          if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
        end
      end
      if @Status = 16 begin
        if @RentCalc = 'N' begin
		  set @TRentDepositReturned = @TRentDepositReturned + (-@TotalSale);
		end
        if @RentCalc = 'Y' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (-@item_TaxIncludePrice); 
		  /*if @tax_inclusive = 'Y' begin
			set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          set @RntTaxAmt1 = @RntTaxAmt1 + @invtx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @invtx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @invtx3;
          set @TRentDepositReturned = @TRentDepositReturned + @RentDeposit;
          
           if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
        end
      end
     
     fetch next from csc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,
     @TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,@CpnPerc,
     @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,@DTax,
	 @i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice


    end
    close csc1
    deallocate csc1 




    declare cscrpr1 cursor
    for select inv.RepairParentID from invoice inv left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
    in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)
    and inv.ID not in ( select invoiceno from VoidInv) and inv.Status = 18
			  
    open cscrpr1
    fetch next from cscrpr1 into @RprParentID
    
    while @@fetch_status = 0 begin
   
    
        declare cscrpr cursor
        for select i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,i.Discount,i.TaxRate1,i.TaxRate2,i.TaxRate3,
        inv.CouponPerc, i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,
		i.TaxIncludeRate,i.TaxIncludePrice 
        from item i left outer join invoice inv on i.invoiceNo=inv.ID where inv.ID not in ( select invoiceno from VoidInv) and i.Tagged <> 'X'
        and inv.ID = @RprParentID
			  
       open cscrpr
       fetch next from cscrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
       @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
    
       while @@fetch_status = 0 begin
       
           set @RprTx1 = 0;
		   set @RprTx2 = 0;
           set @RprTx3 = 0;

           if (@RprT1='Y' or @RprT2='Y'or @RprT3='Y') and (@fstender = 'N')
           begin
             if @RprT1='Y' begin
               if @Tx1ty = 0 set @RprTx1 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT1r/100)*(100 - @CpnPerc)/100,2);
               if @Tx1ty = 1 set @RprTx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT2='Y' begin
               if @Tx2ty = 0 set @RprTx2 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT2r/100)*(100 - @CpnPerc)/100,2);
               if @Tx2ty = 1 set @RprTx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT3='Y' begin
               if @Tx3ty = 0 set @RprTx3 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT3r/100)*(100 - @CpnPerc)/100,2);
               if @Tx3ty = 1 set @RprTx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
             end
           end
                     
            if @tax_inclusive = 'N' set @RepairSales = @RepairSales + (@RprPr*@RprQty) - @RprDiscnt; 
			if @tax_inclusive = 'Y' set @RepairSales = @RepairSales + (@item_TaxIncludePrice); 

            set @RTaxAmt1 = @RTaxAmt1 + @RprTx1;
            set @RTaxAmt2 = @RTaxAmt2 + @RprTx2;
            set @RTaxAmt3 = @RTaxAmt3 + @RprTx3;
        
           if @RprDiscnt <> 0					/* Discount on item */
           begin
             set @RDiscountItemNo = @RDiscountItemNo + 1;
             set @RDiscountItemAmount = @RDiscountItemAmount + @RprDiscnt;
           end
           
            if @Fees <> 0 set @RepairFees = @RepairFees + @Fees;
            if @FeesTax <> 0 set @RepairFeesTax = @RepairFeesTax + @RepairFeesTax;
           
           fetch next from cscrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
           @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
        end
        close cscrpr
        deallocate cscrpr 
     
     fetch next from cscrpr1 into @RprParentID
     
     end
     close cscrpr1
     deallocate cscrpr1 



    declare xx2 cursor
    for select id from closeout where consolidatedID = @CloseoutID
    open xx2
    fetch next from xx2 into @tcid
    while @@fetch_status = 0 begin    
    
    declare csc2 cursor
    for select inv.ID,t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.[Status],inv.LayawayNo, t.TransType, inv.Coupon,inv.IsRentCalculated
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               where (1 = 1) and t.CloseOutID = @tcid
               and inv.ID not in ( select invoiceno from VoidInv)

    open csc2
    fetch next from csc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc

    while @@fetch_status = 0 begin
      
      if @LayNo = 0  
      begin

        if @Status = 3 set @NoOfSales = @NoOfSales + 1;
        
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1;
        
        if @Status = 15 or @Status = 16 set @RentInvoiceCount = @RentInvoiceCount + 1;
        
        if @Status = 18 set @RepairInvoiceCount = @RepairInvoiceCount + 1;
        
        
        if (@Discnt > 0) begin
          if @Status = 3 begin
            set @DiscountInvoiceNo = @DiscountInvoiceNo + 1;
            set @DiscountInvoiceAmount = @DiscountInvoiceAmount + @Discnt;
          end
          if (@Status = 15 and @RentCalc = 'N') or (@Status = 16 and @RentCalc = 'Y') begin
            set @RntDiscountInvoiceNo = @RntDiscountInvoiceNo + 1;
            set @RntDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
          if @Status = 18 begin
            set @RDiscountInvoiceNo = @RDiscountInvoiceNo + 1;
            set @RDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
        end

      end  

      if @LayNo > 0 and @TransType = 4 and @Status = 3 begin   /* layaway items */ 

        set @NoOfSales = @NoOfSales + 1;   
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1;         
  
      end   
           
     fetch next from csc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc

    end

    close csc2
    deallocate csc2 
    
    
    
    fetch next from xx2 into @tcid

    end

    close xx2
    deallocate xx2 



    
    declare csc3 cursor
    for select inv.ID, t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status, t.TransType, inv.Coupon
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
    and inv.LayawayNo > 0 and inv.ID not in ( select invoiceno from VoidInv)

    open csc3
    fetch next from csc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt
    while @@fetch_status = 0 begin

    if @TransType = 2  /* layaway Deposit */
    begin
      
        set @LayAmount = 0;
        select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
        set @LayawayDeposits = @LayawayDeposits + @LayAmount;
     
    end   

    if @TransType = 4  /* layaway Payment */

    begin
      
        if @Status = 1 or @Status = 3 

        begin
        
          set @LayAmount = 0;
             select @LayAmount = payment from laypmts where transactionno <> @TranNo and invoiceno = @InvNo
		     set @LayawayDeposits = @LayawayDeposits + @LayAmount;
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayPayment = @LayawayPayment + @LayAmount;

          if @Status = 3 begin /* layaway sales */

             set @LayawaySalesPosted = @LayawaySalesPosted + @LayAmount; 
             
             

             declare csc4 cursor
             for select i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2, i.Taxable3,
             inv.Tax1, inv.Tax2, inv.Tax3, i.Discount,i.cost,i.UOMCount,i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.CouponPerc,
             i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,
			 i.DTax,i.ProductID,i.SKU,i.BuyNGetFreeCategory,i.TaxIncludeRate,i.TaxIncludePrice from item i 
             left outer join invoice inv on i.invoiceNo=inv.ID
             left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
             in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and inv.ID=@InvNo and i.Tagged <> 'X'

             open csc4
             fetch next from csc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,
             @T1r,@T2r,@T3r,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,
			 @i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice
             while @@fetch_status = 0 begin

			   if @FreeTag = 'F' begin			/* Buy 'n Get Free */
					set @FreeQty = @FreeQty + @Qty;
					set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
				 end

               set @Tx1 = 0;
				 set @Tx2 = 0;
                 set @Tx3 = 0;

                 if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
                 begin
				  if @T1='Y' begin
                    if @Tx1ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @LDiscnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
                  end 
				  if @T2='Y' begin
                    if @Tx2ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @LDiscnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
                  end 
                  if @T3='Y' begin
                    if @Tx3ty = 0 begin
					   if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @LDiscnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
					   if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
                  end 
                 end         
               
				 if @PType = 'G' and @TransType = 1  /* gift cert sales */
			     begin
			       set @GCsold = @GCsold + @Pr*@Qty;
		         end   
		         
		         
		         if @PType = 'O' and @TransType = 1  /* bottle refund */
                 	begin
                   		set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
                 	end  
		         
		        	if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
				begin
					set @MGCSold = @MGCSold + @Pr;
				end  
				
				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
				begin
					set @PGCSold = @PGCSold + @Pr;
				end

				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
				begin
					set @DGCSold = @DGCSold + @Pr;
				end

				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
				begin
					set @PLGCSold = @PLGCSold + @Pr;
				end

                 if @PType = 'A' and @TransType = 1  /* house account payment */
                 begin
                   set @HApayments = @HApayments + @Pr*@Qty;
                 end  

                if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'S'  and @PType <> 'H' and ( @PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
                begin

                  if @PType = 'U'					/* cost of goods sold */
	              begin
					set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
	        	  end
	
        	      if @PType <> 'U'
	              begin
  				    set @CostOfGoods = @CostOfGoods + @cost*@Qty;
	              end
	              
	              
	              if @PType = 'B'					/* other sales */
                  begin
                  
                    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
					  if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty);
					  /*if @tax_inclusive = 'Y' begin
						set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end*/
					end
					else begin
						if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt;
						if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty);
						/*if @tax_inclusive = 'Y' begin
						set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
					end
          
					if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @LDiscnt;
					if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

					/*if @tax_inclusive = 'Y' begin
			          set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/
          
					set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
					set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
					set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @BDiscountItemNo = @BDiscountItemNo + 1;
						set @BDiscountItemAmount = @BDiscountItemAmount + @LDiscnt;
					end
					
					if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
					if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				  end

                  if @PType <> 'B'				/*  product sales */
				  begin
				  
				    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
						if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
						if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
						/*if @tax_inclusive = 'Y' begin
						set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
					end
					else begin
						if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
						if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
						/*if @tax_inclusive = 'Y' begin
						set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
					end
          
                    if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @LDiscnt; 
					if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty); 

					/*if @tax_inclusive = 'Y' begin
			          set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/
          
					set @TaxAmt1 = @TaxAmt1 + @Tx1;
					set @TaxAmt2 = @TaxAmt2 + @Tx2;
					set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @DiscountItemNo = @DiscountItemNo + 1;
						set @DiscountItemAmount = @DiscountItemAmount + @LDiscnt;
					end
          
					if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
					if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				   end
				   
				   if @DTax <> 0 set @TDTax = @TDTax + @DTax;
				end
               
                if @PType = 'S' and @TransType = 1		/* service sales */
			    begin
			      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
					if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt; 
					if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty); 
					/*if @tax_inclusive = 'Y' begin
						set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
				  end
				  else begin
					if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt; 
					if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty); 
					/*if @tax_inclusive = 'Y' begin
						set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
				  end
          
                  if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @LDiscnt;
				  if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

				  /*if @tax_inclusive = 'Y' begin
			          set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		          end*/

                  set @STaxAmt1 = @STaxAmt1 + @Tx1;
				  set @STaxAmt2 = @STaxAmt2 + @Tx2;
                  set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
                  if @LDiscnt <> 0					/* Discount on item */
                  begin
                    set @SDiscountItemNo = @SDiscountItemNo + 1;
                    set @SDiscountItemAmount = @SDiscountItemAmount + @LDiscnt;
                  end
                  
                  if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
                end
     
            fetch next from csc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,@T1r,@T2r,@T3r,
            @CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,@i_ProductID,
			@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice

          end
          close csc4
          deallocate csc4
          
          end

        end

      end 

      if @TransType = 3  /* layaway Payment */
      begin 

        if @Status = 5  /* layaway Refund */
        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayRefund = @LayawayRefund + @LayAmount;

        end

      end 
           
     fetch next from csc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt

    end
    close csc3
    deallocate csc3 


    select @TaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)


    select @RntTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt1 = @RntTaxAmt1 + @STaxAmt1;
    
    select @RntTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt2 = @RntTaxAmt2 + @STaxAmt2;
    
    select @RntTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt3 = @RntTaxAmt3 + @STaxAmt3;
    
    select @RTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)


  /* No Sale */

  select @NoSaleCount = count(*) from trans where TransType = 5 and CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)


  /*  Paid outs */

  select @PaidOuts = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 6 and tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  
  /* Lotto Payout */

  select @LottoPayout = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 60 and tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 


  /*  House account charged */

  select @HACharged = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='House Account' and inv.ID not in ( select invoiceno from VoidInv);

  /*
  /*  Store Credit redeemed */

  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='Store Credit ' and t.TenderAmount > 0 and inv.ID not in ( select invoiceno from VoidInv);
 

   /*  Store Credit issued */

  select @SCissued = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='Store Credit ' and t.TenderAmount < 0 and inv.ID not in ( select invoiceno from VoidInv);

  */

  select @SCissued = isnull(sum(StoreCreditAmount),0) from trans 
  where CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and TransType in (50,51);


  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='Store Credit' and inv.ID not in ( select invoiceno from VoidInv);

    
    declare xx1 cursor
    for select id from closeout where consolidatedID = @CloseoutID
    open xx1
    fetch next from xx1 into @tcid
    while @@fetch_status = 0 begin
    
      declare cscT cursor
      for select isnull(tx1.TaxName,''),isnull(tx2.TaxName,''),isnull(tx3.TaxName,'')
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               left outer join TaxHeader tx1 on tx1.ID = inv.TaxID1 
               left outer join TaxHeader tx2 on tx2.ID = inv.TaxID2 
               left outer join TaxHeader tx3 on tx3.ID = inv.TaxID3 
               where ( 1 = 1) and t.CloseOutID = @tcid
               and inv.ID not in ( select invoiceno from VoidInv )
               and (inv.TaxID1 > 0 or inv.TaxID2 > 0 or inv.TaxID3 > 0)

      open cscT
      fetch next from cscT into @Tax1Name,@Tax2Name,@Tax3Name
      while @@fetch_status = 0 begin

        if @Tax1Name <> '' or  @Tax2Name <> '' or @Tax1Name <> ''
        begin
          if @Tax1Name <> '' set  @Tax1Exist = 'Y';
          if @Tax2Name <> '' set  @Tax2Exist = 'Y';
          if @Tax3Name <> '' set  @Tax3Exist = 'Y';
        end   
        fetch next from cscT into @Tax1Name,@Tax2Name,@Tax3Name

       end

       close cscT
       deallocate cscT


       fetch next from xx1 into @tcid

    end

    close xx1
    deallocate xx1





        
           




    if @AcceptTips = 'Y' begin


        declare sth cursor
        for select distinct ID from closeout where CloseoutType = 'E' and ConsolidatedID = @CloseoutID 
        open sth
        fetch next from sth into @EmpCoutID
   
        while @@fetch_status = 0 begin
          
          set @dtTipEnd = null;
          set @dtTipStart = null;
          select @dtTipEnd = enddatetime, @Emp = createdby from closeout where ID = @EmpCoutID;
        if @dtTipEnd is null select @dtTipEnd = max(dayend) from attendanceinfo where empid = @Emp;
        
        select @dtTipStart = max(enddatetime) from closeout where createdby = @Emp and closeouttype = 'E' and ID < @EmpCoutID;
 
        if @dtTipStart is not null begin
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and dayend 
           between @dtTipStart and @dtTipEnd;	
        end

        if @dtTipStart is null begin
        
           select @dtTipStart = max(daystart) from attendanceinfo where empid = @Emp and daystart <= @dtTipEnd;
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and 
           dayend between @dtTipStart and @dtTipEnd;	
        end

          set @TCashTip = @TCashTip + @CashTip;
          set @TCCTip = @TCCTip + @CCTip;
          
          fetch next from sth into @EmpCoutID

       end
       
       close sth 
       deallocate sth 
    
    end

    
    
    insert into CloseoutReportMain (TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,
				ProductSales,OtherSales,DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,
				DiscountInvoiceAmount,LayawayDeposits,LayawayRefund,LayawayPayment,LayawaySalesPosted,
				GCsold,HApayments,PaidOuts,HACharged,SCissued,SCredeemed,NoOfSales,
				Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,CloseoutID,StartDateTime,EndDateTime,CloseoutType,
				Notes,EmpID,TerminalName,ReportTerminalName,TotalSales_PreTax,CostOfGoods,
				NoSaleCount,RentSales,RentDeposit,RentDepositReturned,RentTax1Amount,RentTax2Amount,RentTax3Amount,
				RentDiscountInvoiceNo,RentDiscountInvoiceAmount,RepairSales,RepairTax1Amount,RepairTax2Amount,RepairTax3Amount,
				RepairDiscountItemNo,RepairDiscountItemAmount,RepairDiscountInvoiceNo,RepairDiscountInvoiceAmount,ServiceTax1Amount,ServiceTax2Amount,
				ServiceTax3Amount,ServiceDiscountItemNo,ServiceDiscountItemAmount,OtherTax1Amount,OtherTax2Amount,OtherTax3Amount,
				OtherDiscountItemNo,OtherDiscountItemAmount,SalesInvoiceCount,RentInvoiceCount,RepairInvoiceCount,
				ProductTx,ProductNTx,ServiceTx,ServiceNTx,OtherTx,OtherNTx,CashTip,CCTip,
				SalesFees,SalesFeesTax,RentFees,RentFeesTax,RepairFees,RepairFeesTax,DTax,MGCsold,PGCsold,
				BottleRefund,DGCsold,PLGCsold,Free_Qty,Free_Amount,LottoPayout) 
				values
                (@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,
				@ProductSales,@OtherSales,@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,
				@DiscountInvoiceAmount,@LayawayDeposits,@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,
				@GCsold,@HApayments,@PaidOuts,@HACharged,@SCissued,@SCredeemed,@NoOfSales,
				@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
				@CloseoutID,@SD,@ED,@CT,@Notes,@EmpID,@CTeml,@Terminal,@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@NoSaleCount,
				@RentSales,@TRentDeposit,@TRentDepositReturned,@RntTaxAmt1,@RntTaxAmt2,@RntTaxAmt3,@RntDiscountInvoiceNo,@RntDiscountInvoiceAmount,
				@RepairSales,@RTaxAmt1,@RTaxAmt2,@RTaxAmt3,@RDiscountItemNo,@RDiscountItemAmount,@RDiscountInvoiceNo,
				@RDiscountInvoiceAmount,@STaxAmt1,@STaxAmt2,@STaxAmt3,@SDiscountItemNo,@SDiscountItemAmount,@BTaxAmt1,@BTaxAmt2,
				@BTaxAmt3,@BDiscountItemNo,@BDiscountItemAmount,@SalesInvoiceCount,@RentInvoiceCount,@RepairInvoiceCount,
				@ProductTx,@ProductNTx,@ServiceTx,@ServiceNTx,@OtherTx,@OtherNTx,@TCashTip,@TCCTip,
				@SalesFees,@SalesFeesTax,@RentFees,@RentFeesTax,@RepairFees,@RepairFeesTax,@TDTax,@MGCSold,@PGCsold,
				@BottleRefund,@DGCsold,@PLGCsold,@FreeQty,@FreeAmount,@LottoPayout)  

				if @Sync = 'Y'
				  insert into CentralExportCloseoutReportMain (TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,
				ProductSales,OtherSales,DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,
				DiscountInvoiceAmount,LayawayDeposits,LayawayRefund,LayawayPayment,LayawaySalesPosted,
				GCsold,HApayments,PaidOuts,HACharged,SCissued,SCredeemed,NoOfSales,
				Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,CloseoutID,StartDateTime,EndDateTime,CloseoutType,
				Notes,EmpID,TerminalName,EmpName,TotalSales_PreTax,CostOfGoods,
				NoSaleCount,RentSales,RentDeposit,RentDepositReturned,RentTax1Amount,RentTax2Amount,RentTax3Amount,
				RentDiscountInvoiceNo,RentDiscountInvoiceAmount,RepairSales,RepairTax1Amount,RepairTax2Amount,RepairTax3Amount,
				RepairDiscountItemNo,RepairDiscountItemAmount,RepairDiscountInvoiceNo,RepairDiscountInvoiceAmount,ServiceTax1Amount,ServiceTax2Amount,
				ServiceTax3Amount,ServiceDiscountItemNo,ServiceDiscountItemAmount,OtherTax1Amount,OtherTax2Amount,OtherTax3Amount,
				OtherDiscountItemNo,OtherDiscountItemAmount,SalesInvoiceCount,RentInvoiceCount,RepairInvoiceCount,
				ProductTx,ProductNTx,ServiceTx,ServiceNTx,OtherTx,OtherNTx,CashTip,CCTip,
				SalesFees,SalesFeesTax,RentFees,RentFeesTax,RepairFees,RepairFeesTax,DTax,MGCsold,PGCsold,
				BottleRefund,DGCsold,PLGCsold,Free_Qty,Free_Amount,LottoPayout) 
				values
                (@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,
				@ProductSales,@OtherSales,@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,
				@DiscountInvoiceAmount,@LayawayDeposits,@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,
				@GCsold,@HApayments,@PaidOuts,@HACharged,@SCissued,@SCredeemed,@NoOfSales,
				@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
				@CloseoutID,@SD,@ED,@CT,@Notes,@EmpID,@CTeml,@EmpName,@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@NoSaleCount,
				@RentSales,@TRentDeposit,@TRentDepositReturned,@RntTaxAmt1,@RntTaxAmt2,@RntTaxAmt3,@RntDiscountInvoiceNo,@RntDiscountInvoiceAmount,
				@RepairSales,@RTaxAmt1,@RTaxAmt2,@RTaxAmt3,@RDiscountItemNo,@RDiscountItemAmount,@RDiscountInvoiceNo,
				@RDiscountInvoiceAmount,@STaxAmt1,@STaxAmt2,@STaxAmt3,@SDiscountItemNo,@SDiscountItemAmount,@BTaxAmt1,@BTaxAmt2,
				@BTaxAmt3,@BDiscountItemNo,@BDiscountItemAmount,@SalesInvoiceCount,@RentInvoiceCount,@RepairInvoiceCount,
				@ProductTx,@ProductNTx,@ServiceTx,@ServiceNTx,@OtherTx,@OtherNTx,@TCashTip,@TCCTip,
				@SalesFees,@SalesFeesTax,@RentFees,@RentFeesTax,@RepairFees,@RepairFeesTax,@TDTax,@MGCSold,@PGCsold,
				@BottleRefund,@DGCsold,@PLGCsold,@FreeQty,@FreeAmount,@LottoPayout)  
  
     exec @r5 = sp_CloseoutReportReturnItem @CloseoutType , @CloseoutID,@Terminal,@Sync

     exec @r6 = sp_CloseoutReportTender @CloseoutType , @CloseoutID,@Terminal,@Sync

     exec @r7 = sp_CloseoutSalesByHour @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal,@Sync

     exec @r8 = sp_CloseoutSalesByDept @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal,@Sync 


  
  end  






  

end
GO


DROP PROCEDURE IF EXISTS [dbo].[sp_CentralExport]
GO

CREATE procedure [dbo].[sp_CentralExport]
		@ExportType	varchar(3)	
as

declare @TaxedSales				numeric(15,3);
declare @NonTaxedSales				numeric(15,3);
declare @ServiceSales				numeric(15,3);
declare @ProductSales				numeric(15,3);
declare @OtherSales				numeric(15,3);
declare @DiscountItemNo				int;
declare @DiscountItemAmount				numeric(15,3);
declare @DiscountInvoiceNo				int;
declare @DiscountInvoiceAmount			numeric(15,3);
declare @LayawayDeposits				numeric(15,3);
declare @LayawayRefund				numeric(15,3);
declare @LayawayPayment				numeric(15,3);
declare @LayawaySalesPosted			numeric(15,3);
declare @PaidOuts					numeric(15,3);
declare @GCsold					numeric(15,3);
declare @SCissued					numeric(15,3);
declare @SCredeemed				numeric(15,3);
declare @HACharged				numeric(15,3);
declare @HApayments				numeric(15,3);
declare @NoOfSales				int;
declare @PType					char(1);
declare @Pr					numeric(15,3);
declare @NPr					numeric(15,3);
declare @Qty					numeric(15,3);
declare @Disc					numeric(15,3);
declare @T1					char(1);
declare @T2					char(1);
declare @T3					char(1);
declare @RetrnItm					numeric(15,3);
declare @LayNo					int;
declare @Status					int;
declare @TransType					int;
declare @InvNo					int;
declare @TranNo					int;
declare @TransDate				datetime;
declare @Tax1					numeric(15,3);
declare @Tax2					numeric(15,3);
declare @Tax3					numeric(15,3);
declare @LTax1					numeric(15,3);
declare @LTax2					numeric(15,3);
declare @LTax3					numeric(15,3);
declare @TaxAmt1					numeric(15,3);
declare @TaxAmt2					numeric(15,3);
declare @TaxAmt3					numeric(15,3);
declare @Discnt					numeric(15,3);
declare @LDiscnt					numeric(15,3);
declare @LayAmount				numeric(15,3);
declare @ExpTID					int;
declare @ExpTName				nvarchar(40);
declare @ExpTAmount				numeric(15,3);
declare @ExpCount 				int;
declare @ExpAmount 				numeric(15,3);
declare @Fees 				    	numeric(15,3);
declare @FeesTax 					numeric(15,3);
declare @FeesCpn 					numeric(15,3);
declare @FeesCpnTax 				numeric(15,3);
declare @r1					int;
declare @r2					int;
declare @r3					int;
declare @r4					int;
declare @r5					int;
declare @r6					int;
declare @r7					int;
declare @r8					int;
declare @r9					int;
declare @r10					int;
declare @r11					int;
declare @r12					int;
declare @tc					int;
declare @Tax1Name				nvarchar(20);
declare @Tax1Exist 					char(1);
declare @Tax2Name				nvarchar(20);
declare @Tax2Exist 					char(1);
declare @Tax3Name				nvarchar(20);
declare @Tax3Exist 					char(1);
declare @SD					datetime;
declare @ED					datetime;
declare @Notes					nvarchar(100);
declare @EmpID					nvarchar(12);
declare @CT					char(1);
declare @NS					int;
declare @count1					int;
declare @count2					int;
declare @CTeml					nvarchar(50);
declare @TotalSale					numeric(15,3);
declare @TTotalSale				numeric(15,3);

declare @TotalSales_PreTax 				numeric(15,3);
declare @CostOfGoods 				numeric(15,3);

declare @ReturnItemNo				int;
declare @ReturnItemAmount				numeric(15,3);
declare @ReturnAmount				numeric(15,3);

declare @ExpEmpID				int;
declare @ExpShiftID				int;
declare @ExpShiftDuration				int;
declare @ExpEmployeeID				nvarchar(12);
declare @ExpLastName				nvarchar(20);
declare @ExpFirstName				nvarchar(20);
declare @ExpShiftName				nvarchar(50);
declare @ExpStartTime				nvarchar(15);
declare @ExpEndTime				nvarchar(15);
declare @ExpDayStart				datetime;
declare @ExpDayEnd				datetime;
declare @ExpShiftStartDate				datetime;
declare @ExpShiftEndDate				datetime;
declare @ExpSKU					nvarchar(16);
declare @ExpDescription				nvarchar(150);
declare @ExpProductType				char(1);
declare @ExpQtyOnHand				numeric(15,3);
declare @ExpQtyOnLayaway				numeric(15,3);
declare @ExpReorderQty				numeric(15,3);
declare @ExpNormalQty				numeric(15,3);
declare @gcNO					nvarchar(20);
declare @gcAmount					numeric(15, 3);
declare @gcItemID					int;
declare @gcTenderNo				int;
declare @gcRegisterID				smallint;
declare @gcCreatedBy				int;
declare @gcCreatedOn				datetime;
declare @gcLastChangedBy				int;
declare @gcLastChangedOn				datetime;
declare @gcCustomerID				int;
declare @gcIssueStore				nvarchar(10);
declare @gcOperateStore				nvarchar(10);
declare @trnexportcnt				int;
declare @cost					numeric(15,3);
declare @UOMCount				numeric(15,3);
declare @pSKU					nvarchar(16);
declare @pProdName				nvarchar(150);
declare @pDeptName				nvarchar(30);
declare @pCatName				nvarchar(30);
declare @pQty					Numeric(15,3);
declare	@pTransDate				datetime;
declare @prevtranid					int;
declare @chCustomerID				nvarchar(10);
declare @chAccountNo				nvarchar(7);
declare @chLastName				nvarchar(20);
declare @chFirstName				nvarchar(20);
declare @chSpouse					nvarchar(20);
declare @chCompany				nvarchar(30);
declare @chSalutation				nvarchar(4);
declare @chAddress1				nvarchar(30);
declare @chAddress2				nvarchar(30);
declare @chCity					nvarchar(20);
declare @chState					nvarchar(2);
declare @chCountry					nvarchar(20);
declare @chZip					nvarchar(12);
declare @chShipAddress1				nvarchar(30);
declare @chShipAddress2				nvarchar(30);
declare @chShipCity				nvarchar(20);
declare @chShipState				nvarchar(2);			  
declare @chShipCountry				nvarchar(20);
declare @chShipZip					nvarchar(12);
declare @chWorkPhone				nvarchar(14);
declare @chHomePhone				nvarchar(14);
declare @chFax					nvarchar(14);
declare @chMobilePhone				nvarchar(14);
declare @chEMail					nvarchar(30);
declare @chDiscount				nvarchar(20);			  
declare @chTaxExempt				char(1);
declare @chTaxID					nvarchar(12);
declare @chDiscountLevel				char(1);
declare @chStoreCredit				numeric(15,3);
declare @chDateLastPurchase			datetime;
declare @chAmountLastPurchase			numeric(15,3);          
declare @chTotalPurchases				numeric(15,3);
declare @chSelected				char(1);
declare @chARCreditLimit				numeric(15,3);
declare @chCreatedBy				int;
declare @chCreatedOn				datetime;
declare @chLastChangedBy				int;
declare @chLastChangedOn				datetime;			  
declare @chDateOfBirth				datetime;
declare @chDateOfMarriage				datetime;
declare @chClosingBalance				numeric(15,3);	
declare @chPoints					int;
declare @chStoreCreditCard				nvarchar(10);
declare @chParamValue1				nvarchar(30);
declare @chParamValue2				nvarchar(30);		          
declare @chParamValue3				nvarchar(30);
declare @chParamValue4				nvarchar(30);
declare @chParamValue5				nvarchar(30);
declare @chPOSNotes				nvarchar(100);
declare @chIssueStore				nvarchar(10);
declare @chOperateStore				nvarchar(10);
declare @chActiveStatus		char(1);
declare @chDiscountID		int;
declare @chRefDiscount		nvarchar(50);

declare @caCustomerID		int;
declare @caInvoiceNo		int;
declare @caAmount			numeric(15,3);
declare @caTranType			int;
declare @caDate				datetime;
declare @caCreatedBy		int;		
declare @caCreatedOn		datetime;
declare @caLastChangedBy	int;			  
declare @caLastChangedOn	datetime;
declare @caIssueStore		nvarchar(10);
declare @caOperateStore		nvarchar(10);

declare @cgCustomerID		nvarchar(10);
declare @cgGroupID			nvarchar(10);
declare @cgDescription		nvarchar(10);
declare @cgIssueStore		nvarchar(10);

declare @ccCustomerID		nvarchar(10);
declare @ccClassID			nvarchar(10);
declare @ccDescription		nvarchar(10);
declare @ccIssueStore		nvarchar(10);
declare @thisstorecode		nvarchar(10);
declare @ercdID				int;
declare @gcrcdID			int;
declare @chrcdID			int;
declare @carcdID			int;
declare @cgrcdID			int;
declare @ccrcdID			int;

declare @fstender			char(1);
declare @psDate				datetime;	
declare @psDate_s			varchar(12);		




declare @eEID				int;
declare @eEmployeeID		nvarchar(12);
declare @eLastName			nvarchar(20);
declare @eFirstName			nvarchar(20);
declare @eAddress1			nvarchar(30);
declare @eAddress2			nvarchar(30);
declare @eCity				nvarchar(20);
declare @eState				nvarchar(2);
declare @eZip				nvarchar(12);
declare @ePhone1			nvarchar(14);
declare @ePhone2			nvarchar(14);
declare @eEmergencyPhone	nvarchar(14);
declare @eEmergencyContact	nvarchar(30);
declare @eEMail				nvarchar(25);
declare @eSSNumber			nvarchar(11);			  
declare @eEmpRate			numeric(15,3);

declare @eProfileID			int;
declare @eProfileName		varchar(50);

declare @eShiftID			int;
declare @eShiftDuration		int;
declare @eShiftName			nvarchar(50);
declare @eStartTime			nvarchar(15);
declare @eEndTime			nvarchar(15);

declare @eIssueStore		nvarchar(10);
declare @eOperateStore		nvarchar(10);


declare @fe 				numeric(15,3);
declare @fetx				numeric(15,3);

declare @fecpn 				numeric(15,3);
declare @fecpntx			numeric(15,3);

declare @i_ProductID		int;
declare @i_SKU				nvarchar(16);
declare  @MGCSold			numeric(15,2);

declare @ProductID			int;
declare @MatrixOptionID		int;
declare @MatrixValue1		nvarchar(30);
declare @MatrixValue2		nvarchar(30);
declare @MatrixValue3		nvarchar(30);

declare @MatrixQtyOnHand	numeric(15,3);

declare @Option1Name		nvarchar(30);
declare @Option2Name		nvarchar(30);
declare @Option3Name		nvarchar(30);

declare @ValueID			smallint;
declare @OptionValue		nvarchar(30);
declare @OptionDefault		char(1);

declare @TrnHeaderID		int;
declare @TransferNo			nvarchar(16);
declare @TransferDate		datetime;
declare @FromStore			nvarchar(10);
declare @ToStore			varchar(10);
declare @TotalQty			numeric(15,4);
declare @TotalCost			numeric(15,4);
declare @GeneralNotes		nvarchar(250);

declare @TrnRefID			int;
declare @ItemSKU			nvarchar(16);
declare @ItemDescription	nvarchar(150);
declare @TransferCost		numeric(15,4);
declare @TransferQty		numeric(15,4);
declare @TransferNotes		nvarchar(100);



declare @tranID				int;
declare @tranDate			datetime;
declare @tranDate_s			varchar(12);
declare @invID				int;
declare @invTax				numeric(15,3);
declare @invTax1			numeric(15,3);
declare @invTax2			numeric(15,3);
declare @invTax3			numeric(15,3);
declare @invDiscount		numeric(15,3);
declare	@invCoupon			numeric(15,3);
declare @invFees			numeric(15,3);
declare @invFeesTax			numeric(15,3);
declare @invFeesCoupon		numeric(15,3);
declare @invFeesCouponTax	numeric(15,3);
declare @invTotalSale		numeric(15,3);
declare @invServiceType		varchar(10);
declare @hTax1Name			nvarchar(20);
declare @hTax2Name			nvarchar(20);
declare @hTax3Name			nvarchar(20);
declare @iSKU				nvarchar(16);
declare @iDescription		nvarchar(200);
declare @iProductType		char(1);
declare @deptname			nvarchar(30);
declare @catname			nvarchar(30);
declare @iQty				numeric(15,3);				
declare @tirate				numeric(15,3);
declare @tiprice			numeric(15,3);
declare @iCost				numeric(15,3);
declare @iPrice				numeric(15,3);
declare @iNormalPrice		numeric(15,3);
declare @iTaxTotal1			numeric(15,3);
declare @iTaxTotal2			numeric(15,3);
declare @iTaxTotal3			numeric(15,3);
declare @iDiscount			numeric(15,3);
declare @iFees				numeric(15,3);
declare @iFeesTax			numeric(15,3);
declare	@iUOMCount			numeric(15,3);
declare @iUOMPrice			numeric(15,3);
declare @iTaxable1			char(1);
declare @iTaxable2			char(1);
declare @iTaxable3			char(1);
declare @dTax1Name			nvarchar(20);
declare @dTax2Name			nvarchar(20);
declare @dTax3Name			nvarchar(20);

declare @iTaxRate1			numeric(15,3);
declare @iTaxRate2			numeric(15,3);
declare @iTaxRate3			numeric(15,3);

declare @i_TaxIncludeRate	numeric(15,3);
declare @i_TaxIncludePrice  numeric(15,3);




declare @tenderName			nvarchar(40);
declare @tenderDisplayName	nvarchar(40);
declare @TenderAmount		numeric(15,3);

declare @TaxFlag			char(1);
declare @countHeader		int;



declare @RecvHeaderID_r		int;
declare @RecvDetailID_r		int;
declare @BatchID_r		int;

declare @PurchaseOrder_r	varchar(16);
declare @InvoiceNo_r	varchar(16);
declare @Note_r	varchar(16);

declare @InvoiceTotal_r	numeric(15,4);
declare @GrossAmount_r	numeric(15,4);
declare @Tax_r	numeric(15,4);
declare @Freight_r	numeric(15,4);


declare @PriceA_r	numeric(15,3);
declare @DQty_r	numeric(15,4);
declare @DCost_r	numeric(15,4);
declare @DFreight_r	numeric(15,4);
declare @DTax_r	numeric(15,4);
declare @ProductName_r	varchar(150);
declare @VendorPartNo_r	varchar(16);
declare @CheckClerk_r	nvarchar(40);
declare @RecvClerk_r	nvarchar(40);
declare @RecvClerkID_r	nvarchar(40);
declare @CheckClerkID_r	nvarchar(40);
declare @VendorID_r	nvarchar(40);

declare @DateOrdered_r datetime;
declare @ReceiveDate_r datetime;
declare @DateTimeStamp_r datetime;

declare @VendorName_r	varchar(100);

declare @POHeaderID_poh				int;
declare @OrderNo_poh				varchar(16);
declare @OrderDate_poh				datetime;
declare @VendorID_poh				varchar(10);
declare @RefNo_poh					varchar(16);
declare @ExpectedDeliveryDate_poh	datetime;
declare @Priority_poh				varchar(10);
declare @GrossAmount_poh			numeric(15, 4);
declare @Freight_poh				numeric(15, 4);
declare @Tax_poh					numeric(15, 4);
declare @NetAmount_poh				numeric(15, 4);
declare @SupplierInstructions_poh	varchar(250);
declare @GeneralNotes_poh			varchar(250);
declare @VendorMinOrderAmount_poh	numeric(15, 3);
declare @VendorName_poh				varchar(30);

begin
  
  delete from CentralExportSalesHeader
  delete from CentralExportSalesTender
  delete from CentralExportEmployee
  delete from CentralExportEmp
  delete from CentralExportInventory
  delete from CentralExportGiftCert
  delete from CentralExportProductSales
  delete from CentralExportCustomer
  delete from CentralExportAcctRecv
  delete from CentralExportGeneralMapping
  delete from CentralExportTransferHeader
  delete from CentralExportTransferDetail
  delete from CentralExportInvoice;
  delete from CentralExportItem;
  delete from CentralExportTender;
  delete from CentralExportReceiving;
  delete from CentralExportPOHeader;
  
  set @ReturnItemNo = 0;
  set @ReturnItemAmount = 0;
  set @TaxedSales = 0;
  set @NonTaxedSales = 0;
  set @ServiceSales = 0;
  set @ProductSales = 0;
  set @OtherSales = 0;
  set @DiscountItemNo = 0;
  set @DiscountItemAmount = 0;
  set @DiscountInvoiceNo = 0;
  set @DiscountInvoiceAmount = 0;
  set @LayawayDeposits = 0;
  set @LayawayRefund = 0;
  set @LayawaySalesPosted = 0;
  set @LayawayPayment = 0;

  set @PaidOuts = 0;
  set @GCsold = 0;
  set @SCissued = 0;
  set @SCredeemed = 0;
  set @HACharged = 0;
  set @HApayments = 0;

  set @Tax1 = 0;
  set @Tax2 = 0;
  set @Tax3 = 0;

  set @LTax1 = 0;
  set @LTax2 = 0;
  set @LTax3 = 0;

  set @TaxAmt1 = 0;
  set @TaxAmt2 = 0;
  set @TaxAmt3 = 0;
    
  set @LayAmount = 0;

  set @NoOfSales = 0;

  set @Tax1Exist = 'N';
  set @Tax2Exist = 'N';
  set @Tax3Exist = 'N';

  set @TTotalSale = 0;
  set @CostOfGoods = 0;
  set @ReturnAmount = 0;
  
	set @trnexportcnt = 0;

	set @prevtranid = 0;
  
	set @Fees = 0;
	set @FeesTax = 0;

	set @FeesCpn = 0;
	set @FeesCpnTax = 0;
  
	set @MGCSold = 0;



	set @TrnHeaderID = 0;
	set @TransferNo	= '';
	set @TransferDate	= getdate();
	set @FromStore	= '';
	set @ToStore		= '';
	set @TotalQty			 = 0;
	set @TotalCost			 = 0;
	set @GeneralNotes	= '';

	set @TrnRefID			 = 0;
	set @ItemSKU			= '';
	set @ItemDescription	= '';
	set @TransferCost		 = 0;
	set @TransferQty		 = 0;
	set @TransferNotes		= '';

if @ExportType = 'ALL' begin  

    select @trnexportcnt = count(*) from trans where ExpFlag = 'N';
    
	if @trnexportcnt > 0 begin

	select @TaxFlag = TaxInclusive from setup;

	declare scTdate0 cursor
	for select distinct convert(date,TransDate) from Trans where ExpFlag = 'N' 
	open scTdate0
	fetch next from scTdate0 into @TransDate
	while @@fetch_status = 0 begin


	set @TaxedSales = 0;
	set @NonTaxedSales = 0;
	set @TaxAmt1 = 0;
	set @TaxAmt2 = 0;
	set @TaxAmt3 = 0;
	set @ServiceSales = 0;
	set @ProductSales = 0;
	set @OtherSales = 0;
	set @DiscountItemNo = 0;
	set @DiscountItemAmount = 0;
	set @DiscountInvoiceNo = 0;
	set @DiscountInvoiceAmount = 0;
	set @LayawayDeposits  = 0;
	set @LayawayRefund = 0;
	set @LayawayPayment = 0;
	set @LayawaySalesPosted = 0;
	set @GCsold = 0;
	set @HApayments = 0;
	set @PaidOuts = 0;
	set @HACharged = 0;
	set @SCissued = 0;
	set @SCredeemed = 0;
	set @NoOfSales = 0;
	set @Tax1Name = '';
	set @Tax1Exist = 'N';
	set @Tax2Name = '';
	set @Tax2Exist = 'N';
	set @Tax3Name = '';
	set @Tax3Exist = 'N';
	set @TTotalSale  = 0;
	set @CostOfGoods  = 0;
	set @ReturnItemNo  = 0;
	set @ReturnItemAmount = 0;
	set @Fees = 0;
	set @FeesTax = 0;
	set @MGCSold = 0;
	set @FeesCpn = 0;
	set @FeesCpnTax  = 0;

    declare sc1 cursor
    for select inv.ID,t.ID,i.ProductType,i.Price,i.NormalPrice,i.Qty,i.Taxable1,i.Taxable2,i.Taxable3,i.ReturnedItemCnt,inv.Status,
    inv.LayawayNo,t.TransType,inv.TotalSale,i.cost,i.UOMCount,i.Discount,isnull(d.Description,'') as ProdDept,isnull(c.Description,'') as ProdCat,
    isnull(i.SKU,'') as ProdSKU,isnull(i.Description,'') as ProdName,t.TransDate,i.FSTender,i.ProductID,i.SKU,
	i.TaxIncludeRate, i.TaxIncludePrice  from item i 
    left outer join invoice inv on i.invoiceNo=inv.ID left outer join trans t on t.ID=inv.TransactionNo 
    left outer join dept d on d.ID = i.DepartmentID left outer join Category c on c.ID = i.CategoryID
    left outer join product p on p.ID = i.ProductID
    where t.ExpFlag = 'N' and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv) and convert(date,t.TransDate) = @TransDate

    open sc1
    fetch next from sc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@TotalSale,@cost,@UOMCount,@Disc,
    @pDeptName,@pCatName,@pSKU,@pProdName,@pTransDate,@fstender,@i_ProductID,@i_SKU,@i_TaxIncludeRate,@i_TaxIncludePrice

    while @@fetch_status = 0 begin

      if @PType = 'G' and @TransType = 1  /* gift cert sales */
      begin
      
        set @GCsold = @GCsold + @Pr*@Qty;

      end   
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
      begin
        set @MGCSold = @MGCSold + @Pr;
      end  

      if @PType = 'A' and @TransType = 1  /* house account payment */
      begin
      
        set @HApayments = @HApayments + @Pr*@Qty;

      end  

      
      if @TransType = 1 begin
      
        if @prevtranid = 0 or @prevtranid != @InvNo
          set @TTotalSale = @TTotalSale + @TotalSale;	/* sales (pretax) */

      end

      if @PType <> 'A' and @PType <> 'G' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'H' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
      begin

        if @PType = 'U'					/* cost of goods sold */
        begin

	      set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 

        end

        if @PType <> 'U'
        begin

	      set @CostOfGoods = @CostOfGoods + @cost*@Qty;

        end


        if @PType = 'S'		/* service sales */
        begin

		  if @TaxFlag = 'N' begin
			set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @Disc;
		  end

		  if @TaxFlag = 'Y' begin
			set @ServiceSales = @ServiceSales + (@i_TaxIncludePrice);
		  end

        end

        if @PType = 'B'		/* other sales */
        begin

		  if @TaxFlag = 'N' begin
			set @OtherSales = @OtherSales + (@Pr*@Qty) - @Disc;
		  end

		  if @TaxFlag = 'Y' begin
			set @OtherSales = @OtherSales + (@i_TaxIncludePrice);
		  end

        end

        if @PType <> 'S' and @PType <> 'B'   /*  product sales */
        begin

		  if @TaxFlag = 'N' begin
			set @ProductSales = @ProductSales + (@Pr*@Qty) - @Disc;
		  end

		  if @TaxFlag = 'Y' begin
			set @ProductSales = @ProductSales + (@i_TaxIncludePrice);
		  end
        
        end

        if @Disc <> 0     /* Discount on item */
        begin

          set @DiscountItemNo = @DiscountItemNo + 1;

          set @DiscountItemAmount = @DiscountItemAmount + @Disc;
        
        end

        if (@T1='N' and @T2='N'and @T3='N') or (@fstender = 'Y') /* non taxed sales */
        begin

		   if @TaxFlag = 'N' begin
			set @NonTaxedSales = @NonTaxedSales + (@Pr*@Qty) - @Disc;
		  end

		  if @TaxFlag = 'Y' begin
			set @NonTaxedSales = @NonTaxedSales + (@Pr*@Qty);
		  end

        end 
       
        if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') /* taxed sales */
        begin

		   if @TaxFlag = 'N' begin
			set @TaxedSales = @TaxedSales + (@Pr*@Qty) - @Disc;
		  end

		  if @TaxFlag = 'Y' begin
			set @TaxedSales = @TaxedSales + (@Pr*@Qty);
		  end

        end         

      end 
      
      set @prevtranid  = @InvNo;
     
     fetch next from sc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@TotalSale,@cost,@UOMCount,@Disc,
     @pDeptName,@pCatName,@pSKU,@pProdName,@pTransDate,@fstender,@i_ProductID,@i_SKU,@i_TaxIncludeRate,@i_TaxIncludePrice

    end
    close sc1
    deallocate sc1 

    declare sc2 cursor
    for select inv.ID,t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status,inv.LayawayNo, t.TransType, inv.Coupon,inv.Fees,inv.FeesTax,
	inv.FeesCoupon,inv.FeesCouponTax
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo where t.ExpFlag = 'N' 
               and inv.ID not in ( select invoiceno from VoidInv) and convert(date,t.TransDate) = @TransDate

    open sc2
    fetch next from sc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@fe,@fetx,@fecpn,@fecpntx

    while @@fetch_status = 0 begin


      if @LayNo = 0  /* non layaway items */

      begin

        set @NoOfSales = @NoOfSales + 1;
      
        set @TaxAmt1 = @TaxAmt1 + @Tax1;
        set @TaxAmt2 = @TaxAmt2 + @Tax2;
        set @TaxAmt3 = @TaxAmt3 + @Tax3;
        
        if (@Discnt > 0) begin
          set @DiscountInvoiceNo = @DiscountInvoiceNo + 1;
          set @DiscountInvoiceAmount = @DiscountInvoiceAmount + @Discnt;
        end
        
        set @Fees = @Fees + @fe;
        set @FeesTax = @FeesTax + @fetx;

		set @FeesCpn = @FeesCpn + @fecpn;
        set @FeesCpnTax = @FeesCpnTax + @fecpntx;

      end  

      if @LayNo > 0 and @TransType = 4 and @Status = 3 begin   /* layaway items */ 

        set @NoOfSales = @NoOfSales + 1;     
        
        set @Fees = @Fees + @fe;
        set @FeesTax = @FeesTax + @fetx;

		set @FeesCpn = @FeesCpn + @fecpn;
        set @FeesCpnTax = @FeesCpnTax + @fecpntx;
      
      end   
           
     fetch next from sc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@fe,@fetx,@fecpn,@fecpntx

    end

    close sc2
    deallocate sc2 

    
    declare sc3 cursor
    for select inv.ID, t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status, t.TransType, inv.Coupon
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.ExpFlag = 'N' and inv.LayawayNo > 0 and inv.ID not in ( select invoiceno from VoidInv) and convert(date,t.TransDate) = @TransDate

    open sc3
    fetch next from sc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt
    while @@fetch_status = 0 begin

    if @TransType = 2  /* layaway Deposit */
    begin
      
        set @LayAmount = 0;
        select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
        set @LayawayDeposits = @LayawayDeposits + @LayAmount;
     
    end   

    if @TransType = 4  /* layaway Payment */

    begin
      
        if @Status = 1 or @Status = 3 

        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayPayment = @LayawayPayment + @LayAmount;

          if @Status = 3 begin /* layaway sales */

             set @LayawaySalesPosted = @LayawaySalesPosted + @LayAmount; 

             declare sc4 cursor
             for select i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2, i.Taxable3,
             inv.Tax1, inv.Tax2, inv.Tax3,i.Discount,i.cost,i.UOMCount,i.FSTender,inv.Fees,inv.FeesTax,i.ProductID,i.SKU,
			 inv.FeesCoupon,inv.FeesCouponTax,i.TaxIncludeRate, i.TaxIncludePrice from item i 
             left outer join invoice inv on i.invoiceNo=inv.ID
             left outer join trans t on t.ID=inv.TransactionNo where t.ExpFlag = 'N' and inv.ID=@InvNo and i.Tagged <> 'X'
             and inv.ID not in ( select invoiceno from VoidInv) and convert(date,t.TransDate) = @TransDate

             open sc4
             fetch next from sc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,@fstender,@fe,@fetx,
             @i_ProductID,@i_SKU,@fecpn,@fecpntx,@i_TaxIncludeRate,@i_TaxIncludePrice
             while @@fetch_status = 0 begin

               set @TaxAmt1 = @TaxAmt1 + @LTax1;
               set @TaxAmt2 = @TaxAmt2 + @LTax2;
			   set @TaxAmt3 = @TaxAmt3 + @LTax3;
               
               if (@Discnt > 0) begin
                 set @DiscountInvoiceNo = @DiscountInvoiceNo + 1;
                 set @DiscountInvoiceAmount = @DiscountInvoiceAmount + @LDiscnt;
               end
               
               
               set @Fees = @Fees + @fe;
               set @FeesTax = @FeesTax + @fetx;

			   set @FeesCpn = @FeesCpn + @fecpn;
               set @FeesCpnTax = @FeesCpnTax + @fecpntx;

               
	           set @TTotalSale = @TTotalSale + @TotalSale; 	/* sales (pretax) */

	       if @PType = 'U'					/* cost of goods sold */
	        begin

		      set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
	
        	end
	
        	if @PType <> 'U'
	        begin

		      set @CostOfGoods = @CostOfGoods + @cost*@Qty;

	        end

               if @PType = 'G' and @TransType = 1  /* gift cert sales */
               begin
      
                 set @GCsold = @GCsold + (@Pr*@Qty);
   
               end   
               
               if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
			   begin
                 set @MGCSold = @MGCSold + @Pr;
               end

               if @PType = 'A' and @TransType = 1  /* house account payment */
               begin
       
                 set @HApayments = @HApayments + (@Pr*@Qty) ;

               end  


               if @PType <> 'A' and @PType <> 'G' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
               begin

      
               if @PType = 'S'		/* service sales */
               begin

				 if @TaxFlag = 'N' begin
					set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @LDiscnt;
				 end

				 if @TaxFlag = 'Y' begin
					set @ServiceSales = @ServiceSales + (@i_TaxIncludePrice);
			     end


               end

               if @PType = 'B'		/* other sales */
               begin

				 if @TaxFlag = 'N' begin
					set @OtherSales = @OtherSales + (@Pr*@Qty) - @LDiscnt;
				 end

				 if @TaxFlag = 'Y' begin
					set @OtherSales = @OtherSales + (@i_TaxIncludePrice);
			     end

               end

               if @PType <> 'S' and @PType <> 'B'   /*  product sales */
               begin

				 if @TaxFlag = 'N' begin
					set @ProductSales = @ProductSales + (@Pr*@Qty) - @LDiscnt;
				 end

				 if @TaxFlag = 'Y' begin
					set @ProductSales = @ProductSales + (@i_TaxIncludePrice);
			     end
        
               end

               if @LDiscnt <> 0     /* Discount on item */
               begin

                 set @DiscountItemNo = @DiscountItemNo + 1;

                 set @DiscountItemAmount = @DiscountItemAmount + @LDiscnt;
        
               end

               if (@T1='N' and @T2='N'and @T3='N') or (@fstender = 'Y') /* non taxed sales */
               begin
 
				 if @TaxFlag = 'N' begin
					set @NonTaxedSales = @NonTaxedSales + (@Pr*@Qty) - @LDiscnt;
				 end

				 if @TaxFlag = 'Y' begin
					set @NonTaxedSales = @NonTaxedSales + (@Pr*@Qty);
			     end

               end 
       
               if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') /* taxed sales */
               begin

				 if @TaxFlag = 'N' begin
					set @TaxedSales = @TaxedSales + (@Pr*@Qty) - @LDiscnt;
				 end

				 if @TaxFlag = 'Y' begin
					set @TaxedSales = @TaxedSales + (@Pr*@Qty);
			     end

               end         


            end 
     
            fetch next from sc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,@fstender,@fe,@fetx,
            @i_ProductID,@i_SKU,@fecpn,@fecpntx,@i_TaxIncludeRate,@i_TaxIncludePrice

          end
          close sc4
          deallocate sc4
          
          end

        end

      end 

      if @TransType = 3  /* layaway Payment */
      begin 

        if @Status = 5  /* layaway Refund */
        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayRefund = @LayawayRefund + @LayAmount;

        end

      end 
           
     fetch next from sc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt

    end
    close sc3
    deallocate sc3 


      /*  Paid outs */

  select @PaidOuts = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 6 and tr.ExpFlag = 'N' and convert(date,tr.TransDate) = @TransDate


  /*  House account charged */

  select @HACharged = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.ExpFlag = 'N'and tt.name='House Account' and inv.ID not in ( select invoiceno from VoidInv) and convert(date,tr.TransDate) = @TransDate;

  /*  Store Credit redeemed */

  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.ExpFlag = 'N' and tt.name='Store Credit ' and t.TenderAmount > 0 and inv.ID not in ( select invoiceno from VoidInv) 
  and convert(date,tr.TransDate) = @TransDate;

  /*  Store Credit issued*/

  select @SCissued = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.ExpFlag = 'N' and tt.name='Store Credit ' and t.TenderAmount < 0 and inv.ID not in ( select invoiceno from VoidInv) 
  and convert(date,tr.TransDate) = @TransDate;


    set @tc = 0;

    declare scT cursor
    for select isnull(tx1.TaxName,''),isnull(tx2.TaxName,''),isnull(tx3.TaxName,'')
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               left outer join TaxHeader tx1 on tx1.ID = inv.TaxID1 
               left outer join TaxHeader tx2 on tx2.ID = inv.TaxID2 
               left outer join TaxHeader tx3 on tx3.ID = inv.TaxID3 
               where t.ExpFlag = 'N' and inv.ID not in ( select invoiceno from VoidInv) and convert(date,t.TransDate) = @TransDate

    open scT
    fetch next from scT into @Tax1Name,@Tax2Name,@Tax3Name
    while @@fetch_status = 0 begin

      if  @tc = 1 break;
      
      if @Tax1Name <> '' set  @Tax1Exist = 'Y';
      if @Tax2Name <> '' set  @Tax2Exist = 'Y';
      if @Tax3Name <> '' set  @Tax3Exist = 'Y';
           
     fetch next from scT into @Tax1Name,@Tax2Name,@Tax3Name

    end

    close scT
    deallocate scT



    declare sc9 cursor
    for select inv.Totalsale from item i left outer join invoice inv on i.invoiceNo=inv.ID
    left outer join trans t on t.ID=inv.TransactionNo where t.ExpFlag = 'N'
    and i.ReturnedItemID <> 0 and i.Qty < 0 and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv) 
	and convert(date,t.TransDate) = @TransDate
    open sc9
    fetch next from sc9 into @ReturnAmount
    while @@fetch_status = 0 begin

      set @ReturnItemNo = @ReturnItemNo + 1;
      set @ReturnItemAmount = @ReturnItemAmount + @ReturnAmount;
     
     fetch next from sc9 into @ReturnAmount

    end
    close sc9
    deallocate sc9 


    

		insert into CentralExportSalesHeader (	TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,ProductSales,OtherSales,
												DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,DiscountInvoiceAmount,LayawayDeposits,
												LayawayRefund,LayawayPayment,LayawaySalesPosted,GCsold,HApayments,PaidOuts,HACharged,SCissued,
												SCredeemed,NoOfSales,Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,
												TotalSales_PreTax,CostOfGoods,ReturnItemNo,ReturnItemAmount,Fees,FeesTax,MGCsold,FeesCoupon,FeesCouponTax,TransDate) 
									values	 (	@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,@ProductSales,@OtherSales,
												@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,@DiscountInvoiceAmount,@LayawayDeposits,
												@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,@GCsold,@HApayments,@PaidOuts,@HACharged,
												@SCissued,@SCredeemed,@NoOfSales,@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
												@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@ReturnItemNo,@ReturnItemAmount,
												@Fees,@FeesTax,@MGCSold,@FeesCpn,@FeesCpnTax,@TransDate)
     

	 fetch next from scTdate0 into @TransDate
	 end
	 close scTdate0
	 deallocate scTdate0

  end
  
     














	if @trnexportcnt > 0 begin

	  declare scTdate1 cursor
	  for select distinct convert(date,TransDate) from Trans where ExpFlag = 'N' 
	  open scTdate1
	  fetch next from scTdate1 into @TransDate
	  while @@fetch_status = 0 begin

         set @ExpCount = 0;
         
		declare sc10 cursor
		for select ID, Name from TenderTypes where name <> 'Store Credit' order by ID
         
		open sc10
		fetch next from sc10 into @ExpTID,@ExpTName
		while @@fetch_status = 0 begin

	      select @ExpCount = count(*) , @ExpAmount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
		  on tr.ID = t.TransactionNo left outer join invoice inv on inv.transactionNo = tr.ID 
		  where t.TenderType = @ExpTID and tr.ExpFlag = 'N' and inv.ID not in ( select invoiceno from VoidInv) and 
		  convert(date,tr.TransDate) = @TransDate
		  
	      if @ExpCount > 0 insert into CentralExportSalesTender(TenderName,TenderAmount,TenderCount,TransDate)values(@ExpTName,@ExpAmount,@ExpCount,@TransDate)
      
          set @ExpCount = 0;
		  fetch next from sc10 into @ExpTID,@ExpTName

       end

	   close sc10
       deallocate sc10

	   fetch next from scTdate1 into @TransDate
	   end
	   close scTdate1
	   deallocate scTdate1
    
    end
    
    

   declare sc19 cursor
    for select e.ID,e.EmployeeID,e.LastName,e.FirstName,e.Address1,e.Address2,e.City,e.State,e.Zip,
    e.Phone1,e.Phone2,e.EmergencyPhone,e.EmergencyContact,e.EMail,e.SSNumber,e.EmpRate,e.IssueStore,e.OperateStore,
    e.ProfileID,g.GroupName ,
    s.ID,s.ShiftDuration,s.ShiftName,s.StartTime,s.EndTime 
    from employee e left outer join ShiftMaster s on e.EmpShift = s.ID 
    left outer join SecurityGroup g on e.ProfileID = g.ID 
    where e.ExpFlag = 'N' order by e.EmployeeID
               
    open sc19
    fetch next from sc19 into @eEID,@eEmployeeID,@eLastName,@eFirstName,@eAddress1,@eAddress2,@eCity,@eState,@eZip,
    @ePhone1,@ePhone2,@eEmergencyPhone,@eEmergencyContact,@eEMail,@eSSNumber,@eEmpRate,@eIssueStore,@eOperateStore,
    @eProfileID,@eProfileName,
    @eShiftID,@eShiftDuration,@eShiftName,@eStartTime,@eEndTime
    while @@fetch_status = 0 begin

		

		insert into CentralExportEmployee(RecordID,EmpID,EmployeeID,LastName,FirstName,
		Address1,Address2,City,State,Zip,Phone1,Phone2,EmergencyPhone,EmergencyContact,
		SSNumber,EMail,EmpRate,IssueStore,OperateStore,ProfileID,ProfileName,
		ShiftID,ShiftDuration,ShiftName,StartTime,EndTime)
                           values(@eEID,@eEID,@eEmployeeID,@eLastName,@eFirstName,@eAddress1,@eAddress2,@eCity,@eState,@eZip,
    @ePhone1,@ePhone2,@eEmergencyPhone,@eEmergencyContact,@eSSNumber,@eEMail,@eEmpRate,@eIssueStore,@eOperateStore,
    @eProfileID,@eProfileName,
    @eShiftID,@eShiftDuration,@eShiftName,@eStartTime,@eEndTime)
      
		fetch next from sc19 into @eEID,@eEmployeeID,@eLastName,@eFirstName,@eAddress1,@eAddress2,@eCity,@eState,@eZip,
    @ePhone1,@ePhone2,@eEmergencyPhone,@eEmergencyContact,@eEMail,@eSSNumber,@eEmpRate,@eIssueStore,@eOperateStore,
    @eProfileID,@eProfileName,
    @eShiftID,@eShiftDuration,@eShiftName,@eStartTime,@eEndTime

    end

    close sc19
    deallocate sc19
    


    declare sc11 cursor
    for select a.ID,e.ID,s.ID,s.ShiftDuration,e.EmployeeID,e.LastName,e.FirstName,s.ShiftName,s.StartTime,s.EndTime,a.DayStart,a.DayEnd,
    a.ShiftStartDate,a.ShiftEndDate from AttendanceInfo a left outer join employee e on a.EmpID = e.ID  
    left outer join ShiftMaster s on a.ShiftID = s.ID where a.ExpFlag = 'N' order by e.EmployeeID
               
    open sc11
    fetch next from sc11 into @ercdID, @ExpEmpID,@ExpShiftID,@ExpShiftDuration,@ExpEmployeeID,@ExpLastName,@ExpFirstName,@ExpShiftName,
    @ExpStartTime,@ExpEndTime,@ExpDayStart,@ExpDayEnd,@ExpShiftStartDate,@ExpShiftEndDate
    while @@fetch_status = 0 begin

		insert into CentralExportEmp(RecordID,EmpID,ShiftID,ShiftDuration,EmployeeID,LastName,FirstName,ShiftName,StartTime,EndTime,DayStart,DayEnd,
						   ShiftStartDate,ShiftEndDate)
                           values(@ercdID,@ExpEmpID,@ExpShiftID,@ExpShiftDuration,@ExpEmployeeID,@ExpLastName,@ExpFirstName,@ExpShiftName,@ExpStartTime,
                           @ExpEndTime,@ExpDayStart,@ExpDayEnd,@ExpShiftStartDate,@ExpShiftEndDate)
      
		fetch next from sc11 into @ercdID, @ExpEmpID,@ExpShiftID,@ExpShiftDuration,@ExpEmployeeID,@ExpLastName,@ExpFirstName,@ExpShiftName,@ExpStartTime,
		@ExpEndTime,@ExpDayStart,@ExpDayEnd,@ExpShiftStartDate,@ExpShiftEndDate

    end

    close sc11
    deallocate sc11

  

    declare sc12 cursor
     for select ID,SKU,Description,ProductType,QtyOnHand,QtyOnLayaway,ReorderQty,NormalQty
      from product where productstatus = 'Y' and expflag = 'N' order by SKU
               
    open sc12
    fetch next from sc12 into @ProductID,@ExpSKU,@ExpDescription,@ExpProductType,@ExpQtyOnHand,@ExpQtyOnLayaway,@ExpReorderQty,@ExpNormalQty
    while @@fetch_status = 0 begin

     insert into CentralExportInventory(SKU,Description,ProductType,QtyOnHand,QtyOnLayaway,ReorderQty,NormalQty)
           values(@ExpSKU,@ExpDescription,@ExpProductType,@ExpQtyOnHand,@ExpQtyOnLayaway,@ExpReorderQty,@ExpNormalQty)
     
     if @ExpProductType = 'M' begin
       
       set @MatrixValue1 = '';
       set @MatrixValue2 = '';
       set @MatrixValue3 = '';
       
       set @Option1Name		= '';
	   set @Option2Name		= '';
	   set @Option3Name		= '';

	   set @ValueID			= 0;
	   set @OptionValue		= '';
	   set @OptionDefault	= 'N';
       
       select @MatrixOptionID = ID from MatrixOptions where ProductID = @ProductID;  
       
       declare scmx1 cursor 
       for select OptionValue1,OptionValue2,OptionValue3,QtyOnHand from Matrix where MatrixOptionID = @MatrixOptionID
       open scmx1
	   fetch next from scmx1 into @MatrixValue1,@MatrixValue2,@MatrixValue3,@MatrixQtyOnHand
       while @@fetch_status = 0 begin
         insert into CentralExportMatrix(SKU,OptionValue1,OptionValue2,OptionValue3,QtyOnHand)
         values(@ExpSKU,@MatrixValue1,@MatrixValue2,@MatrixValue3,@MatrixQtyOnHand);
         fetch next from scmx1 into @MatrixValue1,@MatrixValue2,@MatrixValue3,@MatrixQtyOnHand
       end
       close scmx1
	   deallocate scmx1
	   
	   
	   declare scmx2 cursor 
       for select Option1Name,Option2Name,Option3Name from MatrixOptions where ProductID = @ProductID
       open scmx2
	   fetch next from scmx2 into @Option1Name,@Option2Name,@Option3Name
       while @@fetch_status = 0 begin
         insert into CentralExportMatrixOptions(SKU,Option1Name,Option2Name,Option3Name)
         values(@ExpSKU,@Option1Name,@Option2Name,@Option3Name);
         fetch next from scmx2 into @Option1Name,@Option2Name,@Option3Name
       end
       close scmx2
	   deallocate scmx2
	   
	   declare scmx3 cursor 
       for select ValueID,OptionValue,OptionDefault from MatrixValues where MatrixOptionID = @MatrixOptionID
       open scmx3
	   fetch next from scmx3 into @ValueID,@OptionValue,@OptionDefault
       while @@fetch_status = 0 begin
         insert into CentralExportMatrixValues(SKU,ValueID,OptionValue,OptionDefault)
         values(@ExpSKU,@ValueID,@OptionValue,@OptionDefault);
         fetch next from scmx3 into @ValueID,@OptionValue,@OptionDefault
       end
       close scmx3
	   deallocate scmx3
     
     end
                           
     fetch next from sc12 into @ProductID,@ExpSKU,@ExpDescription,@ExpProductType,@ExpQtyOnHand,@ExpQtyOnLayaway,@ExpReorderQty,@ExpNormalQty
    end

    close sc12
    deallocate sc12
    
    
    declare scps cursor
    for select i.SKU,i.Description,i.ProductType,d.Description,c.Description,convert(varchar, t.TransDate, 101) as tDate,
    isnull(sum(i.Qty),0) as TQty from item i 
    left outer join invoice inv on i.invoiceNo=inv.ID left outer join trans t on t.ID=inv.TransactionNo 
    left outer join dept d on d.ID = i.DepartmentID left outer join Category c on c.ID = i.CategoryID
    left outer join product p on p.ID = i.ProductID
    where t.ExpFlag = 'N' and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv)
    and i.ProductType <> 'A' and i.ProductType <> 'C' and i.ProductType <>'G' and i.ProductType <>'Z' and i.ProductType <>'H'
    and t.TransType = 1
    group by i.SKU,i.Description,i.ProductType,d.Description,c.Description,convert(varchar, t.TransDate, 101)
    order by tDate, i.SKU
    
    open scps
    fetch next from scps into @pSKU,@pProdName,@PType,@pDeptName,@pCatName,@psDate_s,@Qty
    while @@fetch_status = 0 begin
    
      set @psDate = CONVERT(datetime, @psDate_s, 101);
    
      insert into CentralExportProductSales(SKU,ProductName,ProductType,DeptName,CategoryName,Qty,TranDate)
      values(@pSKU,@pProdName,@PType,@pDeptName,@pCatName,@Qty,@psDate);
    
      fetch next from scps into  @pSKU,@pProdName,@PType,@pDeptName,@pCatName,@psDate_s,@Qty
    end

    close scps
    deallocate scps








	declare ctrn1 cursor
    for select ID,TransferNo,TransferDate, FromStore, ToStore, TotalQty, TotalCost, GeneralNotes from TransferHeader 
    where ExpFlag = 'N' and Status = 'Ready' order by ID
    open ctrn1
    fetch next from ctrn1 into @TrnHeaderID,@TransferNo,@TransferDate,@FromStore,@ToStore,@TotalQty,@TotalCost,@GeneralNotes
    while @@fetch_status = 0 begin
    
      insert into CentralExportTransferHeader(HeaderID,TransferNo,TransferDate,FromStore,ToStore,TotalQty,TotalCost,GeneralNotes)
      values(@TrnHeaderID,@TransferNo,@TransferDate,@FromStore,@ToStore,@TotalQty,@TotalCost,@GeneralNotes);

	  declare ctrn2 cursor
	  for select p.SKU,t.Description, t.Qty, t.Cost, t.Notes from TransferDetail t left outer join product p on p.ID = t.ProductID
      where t.RefID = @TrnHeaderID
      open ctrn2
      fetch next from ctrn2 into @ItemSKU,@ItemDescription,@TransferQty,@TransferCost,@TransferNotes
      while @@fetch_status = 0 begin
        insert into CentralExportTransferDetail(RefID,SKU,Description,Cost,Qty,Notes)
        values(@TrnHeaderID,@ItemSKU,@ItemDescription,@TransferCost,@TransferQty,@TransferNotes);
		fetch next from ctrn2 into @ItemSKU,@ItemDescription,@TransferQty,@TransferCost,@TransferNotes
	  end
	  close ctrn2
	  deallocate ctrn2
      fetch next from ctrn1 into @TrnHeaderID,@TransferNo,@TransferDate,@FromStore,@ToStore,@TotalQty,@TotalCost,@GeneralNotes
    end

    close ctrn1
    deallocate ctrn1





	select @TaxFlag = TaxInclusive from setup;


	declare ctrlinv cursor
    for select t.ID, convert(varchar, t.TransDate, 101) as tDate, 
	inv.ID, inv.Tax, inv.Tax1,inv.Tax2, inv.Tax3, inv.Discount, inv.Coupon, inv.Fees, inv.FeesTax, inv.FeesCoupon,
	inv.FeesCouponTax, inv.TotalSale,inv.ServiceType,
	isnull(txh1.TaxName,'') hTax1Name,isnull(txh2.TaxName,'') hTax2Name,isnull(txh3.TaxName,'') hTax3Name,
	i.SKU,i.Description,i.ProductType,isnull(d.Description,'') deptname, isnull(c.Description,'') catname,
	i.Qty, isnull(i.TaxIncludeRate,0) tirate, isnull(i.TaxIncludeprice,0) tiprice,
	i.Cost, i.Price, i.NormalPrice, i.TaxTotal1, i.TaxTotal2, i.TaxTotal3, i.Discount, i.Fees, i.FeesTax,
	i.UOMCount, i.UOMPrice, i.Taxable1, i.Taxable2, i.Taxable3,
	isnull(txd1.TaxName,'') dTax1Name,isnull(txd2.TaxName,'') dTax2Name,isnull(txd3.TaxName,'') dTax3Name,
	i.TaxRate1,i.TaxRate2,i.TaxRate3
	from item i 
    left outer join invoice inv on i.invoiceNo=inv.ID left outer join trans t on t.ID=inv.TransactionNo 
    left outer join dept d on d.ID = i.DepartmentID left outer join Category c on c.ID = i.CategoryID
    left outer join product p on p.ID = i.ProductID
	left outer join TaxHeader txh1 on txh1.ID = inv.TaxID1
	left outer join TaxHeader txh2 on txh2.ID = inv.TaxID2
	left outer join TaxHeader txh3 on txh3.ID = inv.TaxID3
	left outer join TaxHeader txd1 on txd1.ID = i.TaxID1
	left outer join TaxHeader txd2 on txd2.ID = i.TaxID2
	left outer join TaxHeader txd3 on txd3.ID = i.TaxID3

    where t.ExpFlag = 'N' and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv)
    /*and i.ProductType <> 'A' and i.ProductType <> 'C' and i.ProductType <>'G' and i.ProductType <>'Z' and i.ProductType <>'H'*/
    and t.TransType = 1
   
    order by tDate, i.SKU

	open ctrlinv
	fetch next from ctrlinv into @tranID, @tranDate_s, @invID, @invTax, @invTax1,@invTax2, @invTax3,@invDiscount,
	@invCoupon, @invFees, @invFeesTax, @invFeesCoupon, @invFeesCouponTax, @invTotalSale, @invServiceType,
	@hTax1Name,@hTax2Name, @hTax3Name,
	@iSKU,@iDescription,@iProductType,@deptname,@catname,
	@iQty, @tirate, @tiprice,
	@iCost, @iPrice, @iNormalPrice, @iTaxTotal1, @iTaxTotal2, @iTaxTotal3, @iDiscount, @iFees, @iFeesTax,
	@iUOMCount, @iUOMPrice, @iTaxable1, @iTaxable2, @iTaxable3,
	@dTax1Name,@dTax2Name,@dTax3Name,@iTaxRate1,@iTaxRate2,@iTaxRate3

	while @@fetch_status = 0 begin

	  set @tranDate = CONVERT(datetime, @tranDate_s, 101);

	  select @countHeader = count(*) from CentralExportInvoice where InvoiceID = @invID;

	  if @countHeader = 0 begin
			insert into CentralExportInvoice(TaxIncludeFlag,InvoiceID,TransactionType,TransactionDate,
			TotalTaxAmt,Tax1Name,Tax2Name,Tax3Name,Tax1Amt,Tax2Amt,Tax3Amt,DiscountAmt,CouponAmt,Fees,
			FeesTax,FeesCoupon,FeesCouponTax,TotalAmt)
			values(@TaxFlag,@invID,@invServiceType,@tranDate,
			@invTax, @hTax1Name,@hTax2Name, @hTax3Name, @invTax1,@invTax2, @invTax3,@invDiscount,
			@invCoupon, @invFees, @invFeesTax, @invFeesCoupon, @invFeesCouponTax, @invTotalSale);

	  end

	  insert into CentralExportItem(InvoiceID,ProductType,SKU,ItemName,DeptName,CategoryName,TaxIncludeRate,
	  TaxIncludePrice,Cost,Price,NormalPrice,Tax1Name,Tax2Name,Tax3Name,Taxable1,Taxable2,Taxable3,
	  Tax1Total,Tax2Total,Tax3Total,UOMCount,UOMPrice,Discount,Fees,FeesTax,Qty,Tax1Rate,Tax2Rate,Tax3Rate)
	  values(@invID,@iProductType,@iSKU,@iDescription,@deptname,@catname, @tirate, @tiprice,
	  @iCost, @iPrice, @iNormalPrice,@dTax1Name,@dTax2Name,@dTax3Name,@iTaxable1, @iTaxable2, @iTaxable3,
	  @iTaxTotal1, @iTaxTotal2, @iTaxTotal3,@iUOMCount, @iUOMPrice,@iDiscount, @iFees, @iFeesTax,@iQty,
	  @iTaxRate1,@iTaxRate2,@iTaxRate3);




	  fetch next from ctrlinv into @tranID, @tranDate_s, @invID, @invTax, @invTax1,@invTax2, @invTax3,@invDiscount,
	@invCoupon, @invFees, @invFeesTax, @invFeesCoupon, @invFeesCouponTax, @invTotalSale, @invServiceType,
	@hTax1Name,@hTax2Name, @hTax3Name,
	@iSKU,@iDescription,@iProductType,@deptname,@catname,
	@iQty, @tirate, @tiprice,
	@iCost, @iPrice, @iNormalPrice, @iTaxTotal1, @iTaxTotal2, @iTaxTotal3, @iDiscount, @iFees, @iFeesTax,
	@iUOMCount, @iUOMPrice, @iTaxable1, @iTaxable2, @iTaxable3,
	@dTax1Name,@dTax2Name,@dTax3Name,@iTaxRate1,@iTaxRate2,@iTaxRate3


	end
	close ctrlinv
	deallocate ctrlinv



	declare ctrltender cursor
	for select t.ID, inv.ID, convert(varchar, t.TransDate, 101) as tDate, 
	tm.Name, tm.DisplayAs, tnd.TenderAmount
	from tender tnd left outer join trans t on t.ID=tnd.TransactionNo 
    left outer join invoice inv on inv.TransactionNo=t.ID 
	left outer join TenderTypes tm on tm.ID = tnd.TenderType
    where t.ExpFlag = 'N' and inv.ID not in ( select invoiceno from VoidInv)
    and t.TransType = 1
	order by tDate, t.ID
	open ctrltender
	fetch next from ctrltender into @tranID, @invID, @tranDate_s, @tenderName, @tenderDisplayName, @TenderAmount
	while @@fetch_status = 0 begin

	  set @tranDate = CONVERT(datetime, @tranDate_s, 101);
	  insert into CentralExportTender(InvoiceID,TransactionDate,TenderName,TenderAmt)
	  values(@invID,@tranDate,@tenderName,@TenderAmount);

	  fetch next from ctrltender into @tranID, @invID, @tranDate_s, @tenderName, @tenderDisplayName, @TenderAmount

	end
	close ctrltender
	deallocate ctrltender





	declare ctrltender1 cursor
	for select h.ID as RecvHeaderID, h.BatchID, h.DateTimeStamp, h.ReceiveDate, h.PurchaseOrder, h.InvoiceNo,
	h.InvoiceTotal, h.Freight, h.GrossAmount, h.Tax, h.DateOrdered, h.Note,
	v.VendorID,v.Name as VendorName, 
	isnull (e1.EmployeeID,'ADMIN') as CheckInClerkID,  isnull(e1.FirstName,'') + ' ' + isnull(e1.LastName,'') as CheckInClerkName,
	isnull (e2.EmployeeID,'ADMIN') as ReceivingClerkID, isnull(e2.FirstName,'') + ' ' + isnull(e2.LastName,'') as ReceivingClerkName,
	d.ID as RecvDetailID, d.VendorPartNo,  d.Cost as dcost, d.qty as dqty, d.Freight as dfreight, d.Tax as dtax, 
	p.Description as pname, p.PriceA as pr
from RecvHeader h
left outer join Vendor v on v.Id = h.VendorID
left outer join Employee e1 on e1.Id = h.CheckInClerk
left outer join Employee e2 on e2.Id = h.ReceivingClerk
left outer join RecvDetail d on d.BatchNo = h.BatchID
left outer join Product p on p.ID = d.ProductID
    where h.ExpFlag = 'N'
	order by h.BatchID
	open ctrltender1
	fetch next from ctrltender1 into @RecvHeaderID_r, @BatchID_r, @DateTimeStamp_r, 
	@ReceiveDate_r, @PurchaseOrder_r, @InvoiceNo_r, @InvoiceTotal_r,@Freight_r,@GrossAmount_r,
	@Tax_r, @DateOrdered_r, @Note_r, @VendorID_r, @VendorName_r,@CheckClerk_r,@CheckClerkID_r,
	@RecvClerk_r,@RecvClerkID_r,@RecvDetailID_r,@VendorPartNo_r,@DCost_r,@DQty_r,@DFreight_r,
	@DTax_r,@ProductName_r,@PriceA_r

	while @@fetch_status = 0 begin

	  
	  insert into CentralExportReceiving(RecvHeaderID,BatchID,DateTimeStamp,ReceiveDate,
	  PurchaseOrder,InvoiceNo,InvoiceTotal,Freight,GrossAmount,Tax,DateOrdered,Note,VendorID,
	  VendorName,CheckClerk,CheckClerkID,RecvClerk,RecvClerkID,RecvDetailID,VendorPartNo,
	  DCost,DQty,DFreight,DTax,ProductName,PriceA)
	  values(@RecvHeaderID_r, @BatchID_r, @DateTimeStamp_r, 
	@ReceiveDate_r, @PurchaseOrder_r, @InvoiceNo_r, @InvoiceTotal_r,@Freight_r,@GrossAmount_r,
	@Tax_r, @DateOrdered_r, @Note_r, @VendorID_r, @VendorName_r,@CheckClerk_r,@CheckClerkID_r,
	@RecvClerk_r,@RecvClerkID_r,@RecvDetailID_r,@VendorPartNo_r,@DCost_r,@DQty_r,@DFreight_r,
	@DTax_r,@ProductName_r,@PriceA_r);

	  fetch next from ctrltender1 into @RecvHeaderID_r, @BatchID_r, @DateTimeStamp_r, 
	@ReceiveDate_r, @PurchaseOrder_r, @InvoiceNo_r, @InvoiceTotal_r,@Freight_r,@GrossAmount_r,
	@Tax_r, @DateOrdered_r, @Note_r, @VendorID_r, @VendorName_r,@CheckClerk_r,@CheckClerkID_r,
	@RecvClerk_r,@RecvClerkID_r,@RecvDetailID_r,@VendorPartNo_r,@DCost_r,@DQty_r,@DFreight_r,
	@DTax_r,@ProductName_r,@PriceA_r

	end
	close ctrltender1
	deallocate ctrltender1




	declare ctrltender2 cursor
	for select h.ID as POHeaderId, h.OrderNo, h.OrderDate, h.RefNo, h.ExpectedDeliveryDate, h.Priority,
h.GrossAmount, h.Freight, h.Tax, h.NetAmount, h.SupplierInstructions, h.GeneralNotes, h.VendorMinOrderAmount
, v.VendorID, v.Name as vname from POHeader h left outer join Vendor v on h.VendorID = v.ID
where h.ExpFlag = 'N'
	open ctrltender2
	fetch next from ctrltender2 into @POHeaderID_poh, @OrderNo_poh, @OrderDate_poh, 
	@RefNo_poh, @ExpectedDeliveryDate_poh, @Priority_poh, @GrossAmount_poh,@Freight_poh,@Tax_poh,
	@NetAmount_poh, @SupplierInstructions_poh, @GeneralNotes_poh, @VendorMinOrderAmount_poh, @VendorID_poh,@VendorName_poh
	

	while @@fetch_status = 0 begin

	  
	  insert into CentralExportPOHeader(POHeaderID,OrderNo,OrderDate,RefNo,
	  ExpectedDeliveryDate,Priority,GrossAmount,Freight,Tax,NetAmount,SupplierInstructions,GeneralNotes,
	  VendorMinOrderAmount,VendorID,VendorName)
	  values(@POHeaderID_poh, @OrderNo_poh, @OrderDate_poh, 
	@RefNo_poh, @ExpectedDeliveryDate_poh, @Priority_poh, @GrossAmount_poh,@Freight_poh,@Tax_poh,
	@NetAmount_poh, @SupplierInstructions_poh, @GeneralNotes_poh, @VendorMinOrderAmount_poh, @VendorID_poh,@VendorName_poh);

	  fetch next from ctrltender2 into @POHeaderID_poh, @OrderNo_poh, @OrderDate_poh, 
	@RefNo_poh, @ExpectedDeliveryDate_poh, @Priority_poh, @GrossAmount_poh,@Freight_poh,@Tax_poh,
	@NetAmount_poh, @SupplierInstructions_poh, @GeneralNotes_poh, @VendorMinOrderAmount_poh, @VendorID_poh,@VendorName_poh
	

	end
	close ctrltender2
	deallocate ctrltender2




end

if @ExportType = 'GF' begin
  
     declare scgc1 cursor
     for select ID,GiftCertID,Amount,ItemID,TenderNo,RegisterID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,
	 CustomerID,IssueStore,OperateStore from GiftCert where ExpFlag = 'N'
               
     open scgc1
     fetch next from scgc1 into @gcrcdID, @gcNO,@gcAmount,@gcItemID,@gcTenderNo,@gcRegisterID,@gcCreatedBy,@gcCreatedOn,
     @gcLastChangedBy,@gcLastChangedOn,@gcCustomerID,@gcIssueStore,@gcOperateStore
     while @@fetch_status = 0 begin

     insert into CentralExportGiftCert(RecordID,GiftCertID,Amount,ItemID,TenderNo,RegisterID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,
	 CustomerID,IssueStore,OperateStore)values(@gcrcdID,@gcNO,@gcAmount,@gcItemID,@gcTenderNo,@gcRegisterID,@gcCreatedBy,@gcCreatedOn,
     @gcLastChangedBy,@gcLastChangedOn,@gcCustomerID,@gcIssueStore,@gcOperateStore)
      
     fetch next from scgc1 into @gcrcdID,@gcNO,@gcAmount,@gcItemID,@gcTenderNo,@gcRegisterID,@gcCreatedBy,@gcCreatedOn,
     @gcLastChangedBy,@gcLastChangedOn,@gcCustomerID,@gcIssueStore,@gcOperateStore
    end

    close scgc1
    deallocate scgc1
    
    
    


    
    
    declare sccust1 cursor
     for select ID,CustomerID,AccountNo,LastName,FirstName,Spouse,Company,Salutation,Address1,Address2,City,State,Country,Zip,
		ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,WorkPhone,HomePhone,Fax,MobilePhone,EMail,
		Discount,TaxExempt,TaxID,DiscountLevel,StoreCredit,DateLastPurchase,AmountLastPurchase,TotalPurchases,
		Selected,ARCreditLimit,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DateOfBirth,DateOfMarriage,ClosingBalance,
		Points,StoreCreditCard,ParamValue1,ParamValue2,ParamValue3,ParamValue4,ParamValue5,POSNotes,IssueStore,OperateStore,
		ActiveStatus,DiscountID
                from Customer where ExpFlag = 'N'
               
     open sccust1
     fetch next from sccust1 into @chrcdID,@chCustomerID,@chAccountNo,@chLastName,@chFirstName,@chSpouse,@chCompany,@chSalutation,@chAddress1,
				  @chAddress2,@chCity,@chState,@chCountry,@chZip,@chShipAddress1,@chShipAddress2,@chShipCity,@chShipState,
				  @chShipCountry,@chShipZip,@chWorkPhone,@chHomePhone,@chFax,@chMobilePhone,@chEMail,@chDiscount,
				  @chTaxExempt,@chTaxID,@chDiscountLevel,@chStoreCredit,@chDateLastPurchase,@chAmountLastPurchase,
			          @chTotalPurchases,@chSelected,@chARCreditLimit,@chCreatedBy,@chCreatedOn,@chLastChangedBy,@chLastChangedOn,
				  @chDateOfBirth,@chDateOfMarriage,@chClosingBalance,@chPoints,@chStoreCreditCard,@chParamValue1,@chParamValue2,
			          @chParamValue3,@chParamValue4,@chParamValue5,@chPOSNotes,@chIssueStore,@chOperateStore,@chActiveStatus,@chDiscountID
     
     while @@fetch_status = 0 begin

	 set @chRefDiscount = '';

	 if @chDiscountID > 0
	   select @chRefDiscount = isnull(DiscountName,'') from DiscountMaster where ID = @chDiscountID;

     insert into CentralExportCustomer(
			RecordID,CustomerID,AccountNo,LastName,FirstName,Spouse,Company,Salutation,Address1,Address2,City,State,Country,Zip,
			ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipCountry,ShipZip,WorkPhone,HomePhone,Fax,MobilePhone,EMail,
			Discount,TaxExempt,TaxID,DiscountLevel,StoreCredit,DateLastPurchase,AmountLastPurchase,TotalPurchases,
			Selected,ARCreditLimit,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DateOfBirth,DateOfMarriage,ClosingBalance,
			Points,StoreCreditCard,ParamValue1,ParamValue2,ParamValue3,ParamValue4,ParamValue5,POSNotes,IssueStore,OperateStore,
			ActiveStatus,RefDiscount)
			values(@chrcdID,@chCustomerID,@chAccountNo,@chLastName,@chFirstName,@chSpouse,@chCompany,@chSalutation,@chAddress1,
				  @chAddress2,@chCity,@chState,@chCountry,@chZip,@chShipAddress1,@chShipAddress2,@chShipCity,@chShipState,
				  @chShipCountry,@chShipZip,@chWorkPhone,@chHomePhone,@chFax,@chMobilePhone,@chEMail,@chDiscount,
				  @chTaxExempt,@chTaxID,@chDiscountLevel,@chStoreCredit,@chDateLastPurchase,@chAmountLastPurchase,
			          @chTotalPurchases,@chSelected,@chARCreditLimit,@chCreatedBy,@chCreatedOn,@chLastChangedBy,@chLastChangedOn,
				  @chDateOfBirth,@chDateOfMarriage,@chClosingBalance,@chPoints,@chStoreCreditCard,@chParamValue1,@chParamValue2,
			          @chParamValue3,@chParamValue4,@chParamValue5,@chPOSNotes,@chIssueStore,@chOperateStore,@chActiveStatus,@chRefDiscount)
      
     fetch next from sccust1 into @chrcdID,@chCustomerID,@chAccountNo,@chLastName,@chFirstName,@chSpouse,@chCompany,@chSalutation,@chAddress1,
				  @chAddress2,@chCity,@chState,@chCountry,@chZip,@chShipAddress1,@chShipAddress2,@chShipCity,@chShipState,
				  @chShipCountry,@chShipZip,@chWorkPhone,@chHomePhone,@chFax,@chMobilePhone,@chEMail,@chDiscount,
				  @chTaxExempt,@chTaxID,@chDiscountLevel,@chStoreCredit,@chDateLastPurchase,@chAmountLastPurchase,
			          @chTotalPurchases,@chSelected,@chARCreditLimit,@chCreatedBy,@chCreatedOn,@chLastChangedBy,@chLastChangedOn,
				  @chDateOfBirth,@chDateOfMarriage,@chClosingBalance,@chPoints,@chStoreCreditCard,@chParamValue1,@chParamValue2,
			          @chParamValue3,@chParamValue4,@chParamValue5,@chPOSNotes,@chIssueStore,@chOperateStore,@chActiveStatus,@chDiscountID
    end

    close sccust1
    deallocate sccust1
    
    declare sccust2 cursor
     for select a.ID,c.CustomerID,a.InvoiceNo,a.Amount,a.TranType,a.Date,a.CreatedBy,a.CreatedOn,a.LastChangedBy,a.LastChangedOn,a.IssueStore,a.OperateStore 
		from customer c left outer join AcctRecv a on a.CustomerID = c.ID where a.ExpFlag = 'N'
               
     open sccust2
     fetch next from sccust2 into @carcdID,@chCustomerID,@caInvoiceNo,@caAmount,@caTranType,@caDate,@caCreatedBy,@caCreatedOn,@caLastChangedBy,
				  @caLastChangedOn,@caIssueStore,@caOperateStore
     while @@fetch_status = 0 begin

     insert into CentralExportAcctRecv(	RecordID,CustomerID,InvoiceNo,Amount,TranType,Date,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,
					IssueStore,OperateStore) values(@carcdID,@chCustomerID,@caInvoiceNo,@caAmount,@caTranType,@caDate,@caCreatedBy,
					@caCreatedOn,@caLastChangedBy,@caLastChangedOn,@caIssueStore,@caOperateStore)
      
     fetch next from sccust2 into @carcdID,@chCustomerID,@caInvoiceNo,@caAmount,@caTranType,@caDate,@caCreatedBy,@caCreatedOn,@caLastChangedBy,
				  @caLastChangedOn,@caIssueStore,@caOperateStore
    end

    close sccust2
    deallocate sccust2

    
    select @thisstorecode = isnull(storecode,'') from centralexportimport;
     
    declare sccust3 cursor
     for select gmp.ID,c.CustomerID,g.GroupID,g.Description,c.IssueStore from generalmapping gmp left outer join groupmaster g on gmp.referenceID = g.ID 
		left outer join customer c on gmp.MappingID = c.ID where gmp.referencetype = 'Group' and gmp.mappingtype = 'Customer'
                and c.IssueStore = gmp.IssueStore and c.IssueStore = @thisstorecode and gmp.expflag = 'N' order by c.CustomerID
               
     open sccust3

     fetch next from sccust3 into @cgrcdID,@chCustomerID,@cgGroupID,@cgDescription,@cgIssueStore

     while @@fetch_status = 0 begin

       		insert into CentralExportGeneralMapping(RecordID,CustomerID,ReferenceID,Description,ReferenceType,MappingType,IssueStore)
		 values(@cgrcdID,@chCustomerID,@cgGroupID,@cgDescription,'Group','Customer',@cgIssueStore)
      
       fetch next from sccust3 into @cgrcdID,@chCustomerID,@cgGroupID,@cgDescription,@cgIssueStore
     end
     
    close sccust3
    deallocate sccust3

    declare sccust4 cursor
     for select gmp.ID,c.CustomerID,g.ClassID,g.Description,c.IssueStore from generalmapping gmp left outer join classmaster g on gmp.referenceID = g.ID 
		left outer join customer c on gmp.MappingID = c.ID where gmp.referencetype = 'Class' and gmp.mappingtype = 'Customer'
                and c.IssueStore = gmp.IssueStore and c.IssueStore = @thisstorecode and gmp.expflag = 'N' order by c.CustomerID
               
     open sccust4

     fetch next from sccust4 into @ccrcdID,@chCustomerID,@ccClassID,@ccDescription,@ccIssueStore

     while @@fetch_status = 0 begin

     insert into CentralExportGeneralMapping(RecordID,CustomerID,ReferenceID,Description,ReferenceType,MappingType,IssueStore)
		 values(@ccrcdID,@chCustomerID,@ccClassID,@ccDescription,'Class','Customer',@ccIssueStore)
      
       fetch next from sccust4 into @ccrcdID,@chCustomerID,@ccClassID,@ccDescription,@ccIssueStore
     end

    close sccust4
    deallocate sccust4
    
    
  end

end
GO

DROP PROCEDURE IF EXISTS [dbo].[sp_co_imp_updttag_closeout]
GO

CREATE procedure [dbo].[sp_co_imp_updttag_closeout]
as

begin

  update CentralExportCloseOut set ExpFlag = 'Y' where expflag = 'N';

  delete from CentralExportCloseOutReturn;
  delete from CentralExportCloseOutReportMain;
  delete from CentralExportCloseOutReportTender;
  delete from CentralExportCloseOutSalesDept;
  delete from CentralExportCloseOutSalesHour;


end
GO

DROP PROCEDURE IF EXISTS [dbo].[sp_store_imp_product]
GO

CREATE procedure [dbo].[sp_store_imp_product]
		@SKU			nvarchar(16),
		@SKU2			nvarchar(16),
		@SKU3			nvarchar(16),
		@PDesc			nvarchar(150),
		@PBinL			nvarchar(10),
		@PNotes			nvarchar(250),
		@PNotes2			nvarchar(250),
		@PScrnColor		varchar(20),
		@PScrnStyle		varchar(50),
		@PBkGrnd		varchar(5),
		@PFont			varchar(50),
		@PFontS			int,
		@PFontC			varchar(20),
		@PBold			char(1),
		@PItalics		char(1),
		@SScrnColor		varchar(20),
		@SScrnStyle		varchar(50),
		@SBkGrnd		varchar(5),
		@SFont			varchar(50),
		@SFontS			int,
		@SFontC			varchar(20),
		@SBold			char(1),
		@SItalics		char(1),
		@PCUPC			nvarchar(20),
		@PUPC			nvarchar(20),
		@PSeason		nvarchar(20),
		@PType			char(1),
		@PPrompt		char(1),
		@PPrintBrCd		char(1),
		@PNoPriceLbl	char(1),
		@PFS			char(1),
		@PAddPOS		char(1),
		@PAddPOSCat		char(1),
		@PAddScale		char(1),
		@PNonDiscountable		char(1),
		@PScaleBrCd		char(1),
		@PAllowZeroStk	char(1),	
		@PDispStk		char(1),
		@PActive		char(1),
		@PRental		char(1),
		@PReprCrg		char(1),
		@PReprTag		char(1),
		@PBrkFlag		char(1),
		@PPriceA		numeric(15,3),
		@PPriceB		numeric(15,3),
		@PPriceC		numeric(15,3),
		@PLastCost		numeric(15,3),
		@PCost			numeric(15,3),
		@POnHandQty		numeric(15,3),
		@PLayawayQty	numeric(15,3),
		@PReorderQty	numeric(15,3),
		@PNormalQty		numeric(15,3),
		@PBrkRatio		numeric(15,3),
		@PRentalPerMinute	numeric(15,3),
		@PRentalPerHour		numeric(15,3),
		@PRentalPerHalfDay  numeric(15,3),
		@PRentalPerDay		numeric(15,3),
		@PRentalPerWeek		numeric(15,3),
		@PRentalPerMonth	numeric(15,3),
		@PRentalDeposit		numeric(15,3),
		@PRentalMinHour		numeric(15,3),
		@PRentalMinAmount	numeric(15,3),
		@PRepairCharge		numeric(15,3),
		@PPrntQty		int,
		@PLbl			int,
		@PPoints		int,
		@PMinAge		int,
		@PDecimal		int,
		@PCaseQty		int,
		@PLinkSKU		int,
		@PMinSrv		int,
		@DeptID			nvarchar(10),
        @DeptDesc		nvarchar(30),
		@DeptFS			char(1),
		@DeptSF			char(1),
		@CatID			nvarchar(10),
		@CatDesc		nvarchar(30),
		@CatFS			char(1),
		@CatScrnColor	varchar(20),
		@CatScrnStyle	varchar(50),
		@CatBkGrnd		varchar(5),
		@CatFont		varchar(50),
		@CatFontS		int,
		@CatFontC		varchar(20),
		@CatItemFontC	varchar(20),
		@CatBold		char(1),
		@CatItalics		char(1),
		@CatProdCheck1	char(1),
		@CatProdCheck2	char(1),
		@CatProdCheck3	char(1),
		@CatProdCheck4	char(1),
		@CatProdCheck5	char(1),
		@CatProdCheck6	char(1),
		@CatProdCheck7	char(1),
		@CatProdCheck8	char(1),
		@CatTx1nm		nvarchar(20),
		@CatTx1rt		numeric(15,3),
		@CatTx2nm		nvarchar(20),
		@CatTx2rt		numeric(15,3),
		@CatTx3nm		nvarchar(20),
		@CatTx3rt		numeric(15,3),

		@Tx1nm			nvarchar(20),
		@Tx1rt			numeric(15,3),
		@Tx2nm			nvarchar(20),
		@Tx2rt			numeric(15,3),
		@Tx3nm			nvarchar(20),
		@Tx3rt			numeric(15,3),
		@rntTx1nm		nvarchar(20),
		@rntTx1rt		numeric(15,3),
		@rntTx2nm		nvarchar(20),
		@rntTx2rt		numeric(15,3),
		@rntTx3nm		nvarchar(20),
		@rntTx3rt		numeric(15,3),
		@BrndID			nvarchar(10),
		@BrndDesc		nvarchar(30),
		@Terminal		nvarchar(50),
		@Tare			numeric(15,3),
		@Tare2			numeric(15,3),
		@SplitWeight	numeric(15,3),
		@UOM			nvarchar(15),
		@ID 			int output		
as

declare @isku		int;
declare @ialtsku	int;
declare @ialtsku2	int;

declare @iDept		int;
declare @iCat		int;
declare @iCatDisp	int;


declare @icTx1		int;
declare @icTx2		int;
declare @icTx3		int;

declare @icTc1    int;
declare @icTc2    int;
declare @icTc3    int;

declare @iTx1		int;
declare @iTx2		int;
declare @iTx3		int;

declare @irTx1		int;
declare @irTx2		int;
declare @irTx3		int;

declare @GoTxAdd  char(1);
declare @tcnt int;

declare @iBrnd 	int;

declare @iDpc	int;
declare @iCtc	int;
declare @iBrc    int;

declare @iTc1    int;
declare @iTc2    int;
declare @iTc3    int;

declare @irTc1    int;
declare @irTc2    int;
declare @irTc3    int;

declare @Vdc    int;

declare @ItemDisplayOrder	int;
declare @PID		int;
declare @prevCat	int;
declare @prevOnHandQty	int;
declare @IsExistInStockJournal	int;

declare @ExistsTax1		int;
declare @ExistsTax2		int;
declare @ExistsTax3		int;

declare @ExistsRentalTax1	int;
declare @ExistsRentalTax2	int;
declare @ExistsRentalTax3	int;


declare @ExistsCatTax1		int;
declare @ExistsCatTax2		int;
declare @ExistsCatTax3		int;

declare @rexec1 int;

declare @ScaleDisplayOrder_Dept	int;

begin


   set @ID = 0;
   set @iTx1 = 0;
   set @iTx2 = 0;
   set @iTx3 = 0;	
   set @isku = 0;
   set @ialtsku = 0;
   set @ialtsku2 = 0;
   set @iBrnd = 0;
   set @iDept = 0;
   set @iCat = 0;
   set @iDpc = 0;
   set @iCtc = 0;
   set @iBrc = 0;
   set @iTc1 = 0;
   set @iTc2 = 0;
   set @iTc3 = 0;
   
   set @irTx1 = 0;
   set @irTx2 = 0;
   set @irTx3 = 0;	
   set @irTc1 = 0;
   set @irTc2 = 0;
   set @irTc3 = 0;

   set @icTx1 = 0;
   set @icTx2 = 0;
   set @icTx3 = 0;
   set @icTc1 = 0;
   set @icTc2 = 0;
   set @icTc3 = 0;

   set @ItemDisplayOrder = 0;

   select @isku = count(SKU) from product where SKU = @SKU;

   select @ialtsku = count(SKU2) from Product where SKU2 = @SKU2 and SKU2 <> '';

   select @ialtsku2 = count(SKU3) from Product where SKU3 = @SKU3 and SKU3 <> '';

   if ( @isku = 0 ) and ( @ialtsku = 0 ) and ( @ialtsku2 = 0 ) 

  begin

    select @iDpc = count(*) from Dept where DepartmentID = @DeptID;
    
    if @iDpc > 0 begin
      select @iDept = isnull(ID,0) from Dept where DepartmentID = @DeptID;
    end
    
    if @iDpc = 0 begin
	   set @ScaleDisplayOrder_Dept = 0;
	   if @DeptSF = 'Y' select @ScaleDisplayOrder_Dept = isnull(max(scaledisplayorder),0) + 1 from dept where scaleflag = 'Y';
       insert into dept(DepartmentID,Description,Selected,FoodStampEligible,ScaleFlag,ScaleDisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) 
                  	values ( @DeptID,@DeptDesc,'N',@DeptFS,@DeptSF,@ScaleDisplayOrder_Dept,0,getdate(),0,getdate()) 
       select @iDept = @@IDENTITY ;
    end

     select @iCtc = count(*) from Category where CategoryID = @CatID;
     if @iCtc > 0 
     select @iCat = isnull(ID,0) from Category where CategoryID = @CatID;

     if @iCtc = 0 begin

        select @iCatDisp = isnull(Max(POSDisplayOrder),0) + 1 from Category;

        insert into category(CategoryID,Description,MaxItemsforPOS,POSDisplayOrder,POSBackground,POSFontType,POSFontSize,POSFontColor,POSItemFontColor,
		        FoodStampEligibleForProduct,POSScreenColor,POSScreenStyle,IsBold,IsItalics,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,
				AddToPOSScreen,NoPriceOnLabel,PrintBarCode,NonDiscountable,ScaleBarcode,DisplayStockinPOS,AllowZeroStock,RepairPromptForTag) 
                                    values ( @CatID,@CatDesc,10,@iCatDisp,@CatBkGrnd,@CatFont,@CatFontS,@CatFontC,@CatItemFontC,@CatFS,
                                    @CatScrnColor,@CatScrnStyle,@CatBold,@CatItalics,0,getdate(),0,getdate(),
									@CatProdCheck1,@CatProdCheck2,@CatProdCheck3,@CatProdCheck4,@CatProdCheck5,@CatProdCheck6,@CatProdCheck7,@CatProdCheck8) 
        select @iCat = @@IDENTITY ;

     end

     if @Tx1nm <> ''  begin

        select @iTc1 = count(*) from taxheader where upper(taxname) = upper(@Tx1nm);
        if @iTc1 > 0 
        select @iTx1 = isnull(ID,0) from taxheader where upper(taxname) = upper(@Tx1nm);

         if @iTc1 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @Tx1nm,@Tx1rt,0,getdate(),0,getdate()) 
                select @iTx1 = @@IDENTITY;

            end     
         end
     end


     if @Tx2nm <> ''  begin

        select @iTc2 = count(*) from taxheader where upper(taxname) = upper(@Tx2nm);
        if @iTc2 > 0  
        select @iTx2 = isnull(ID,0) from taxheader where upper(taxname) = upper(@Tx2nm);

         if @iTc2 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @Tx2nm,@Tx2rt,0,getdate(),0,getdate()) 
                select @iTx2 = @@IDENTITY;

            end     
            
         end
        
     end


     if @Tx3nm <> ''  begin

        select @iTc3 = count(*) from taxheader where upper(taxname) = upper(@Tx3nm);
        if @iTc3 > 0 
        select @iTx3 = isnull(ID,0) from taxheader where upper(taxname) = upper(@Tx3nm);

         if @iTc3 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @Tx3nm,@Tx3rt,0,getdate(),0,getdate()) 
                select @iTx3 = @@IDENTITY;

            end     
            
         end
        
     end
     
     
     
     if @rntTx1nm <> ''  begin

        select @irTc1 = count(*) from taxheader where upper(taxname) = upper(@rntTx1nm);
        if @irTc1 > 0 
        select @irTx1 = isnull(ID,0) from taxheader where upper(taxname) = upper(@rntTx1nm);

         if @irTc1 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @rntTx1nm,@rntTx1rt,0,getdate(),0,getdate()) 
                select @irTx1 = @@IDENTITY;

            end     
         end
     end
     
     if @rntTx2nm <> ''  begin

        select @irTc2 = count(*) from taxheader where upper(taxname) = upper(@rntTx2nm);
        if @irTc2 > 0 
        select @irTx2 = isnull(ID,0) from taxheader where upper(taxname) = upper(@rntTx2nm);

         if @irTc2 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @rntTx2nm,@rntTx2rt,0,getdate(),0,getdate()) 
                select @irTx2 = @@IDENTITY;

            end     
         end
     end
     
     if @rntTx3nm <> ''  begin

        select @irTc3 = count(*) from taxheader where upper(taxname) = upper(@rntTx3nm);
        if @irTc3 > 0 
        select @irTx3 = isnull(ID,0) from taxheader where upper(taxname) = upper(@rntTx3nm);

         if @irTc3 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @rntTx3nm,@rntTx3rt,0,getdate(),0,getdate()) 
                select @irTx3 = @@IDENTITY;

            end     
         end
     end
     
     

	 if @CatTx1nm <> ''  begin

        select @icTc1 = count(*) from taxheader where upper(taxname) = upper(@CatTx1nm);
        if @icTc1 > 0 
        select @icTx1 = isnull(ID,0) from taxheader where upper(taxname) = upper(@CatTx1nm);

         if @icTc1 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @CatTx1nm,@CatTx1rt,0,getdate(),0,getdate()) 
                select @icTx1 = @@IDENTITY;

            end     
         end
     end


     if @CatTx2nm <> ''  begin

        select @icTc2 = count(*) from taxheader where upper(taxname) = upper(@CatTx2nm);
        if @icTc2 > 0  
        select @icTx2 = isnull(ID,0) from taxheader where upper(taxname) = upper(@CatTx2nm);

         if @icTc2 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @CatTx2nm,@CatTx2rt,0,getdate(),0,getdate()) 
                select @icTx2 = @@IDENTITY;

            end     
            
         end
        
     end


     if @CatTx3nm <> ''  begin

        select @icTc3 = count(*) from taxheader where upper(taxname) = upper(@CatTx3nm);
        if @icTc3 > 0 
        select @icTx3 = isnull(ID,0) from taxheader where upper(taxname) = upper(@CatTx3nm);

         if @icTc3 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @CatTx3nm,@CatTx3rt,0,getdate(),0,getdate()) 
                select @icTx3 = @@IDENTITY;

            end     
            
         end
        
     end
     
     if @iCat > 0 begin
       if @icTx1 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @icTx1,@iCat,'Category',0,getdate(),0,getdate() ) ;

		if @icTx2 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @icTx2,@iCat,'Category',0,getdate(),0,getdate() ) ;

		if @icTx3 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @icTx3,@iCat,'Category',0,getdate(),0,getdate() ) ;

	 end

     if @BrndID <> '' and @BrndDesc <> '' begin

       select @iBrc = count(*) from brandmaster where BrandID = @BrndID;
       if @iBrc > 0
       select @iBrnd = isnull(ID,0) from brandmaster where BrandID = @BrndID;

       if @iBrc = 0 begin
         insert into brandmaster(BrandID,BrandDescription,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) 
                  	values ( @BrndID,@BrndDesc,0,getdate(),0,getdate()) 
         select @iBrnd = @@IDENTITY ;
        end  
     end  
	

	 select @ItemDisplayOrder = isnull(Max(POSDisplayOrder),0) + 1 from product where categoryid = @iCat;

     insert into Product( SKU,Description,ProductType,PriceA,PriceB,PriceC,Cost,QtyOnHand,ReorderQty,NormalQty,DepartmentID,CategoryID,FoodStampEligible,
                                 MinimumAge,ScaleBarCode,SKU2,SKU3,LastCost,QtyOnLayaway,AllowZeroStock,DisplayStockinPOS,AddtoPOSScreen,NoPriceOnLabel,
                                 PromptForPrice,BinLocation,PrintBarCode,LabelType,QtyToPrint,Points,DecimalPlace,ProductStatus,BrandID,UPC,Season,
                                 CreatedBy,CreatedOn,LastChangedBy,LastChangedOn, POSBackground,POSScreenColor,POSScreenStyle,POSFontType,POSFontSize,
                                 POSFontColor,IsBold,IsItalics,CaseQty,CaseUPC,LinkSKU,BreakPackRatio,RentalPerMinute,RentalPerHour,RentalPerHalfDay,RentalPerDay,RentalPerWeek,
                                 RentalPerMonth,RentalDeposit,MinimumServiceTime,RepairCharge,RentalMinHour,RentalMinAmount,
                                 RentalPrompt,RepairPromptForCharge,RepairPromptForTag,ImportDate,ProductNotes,Tare, 
								 AddToScaleScreen,NonDiscountable,Notes2,Tare2,POSDisplayOrder,ScaleBackground,ScaleScreenColor,ScaleScreenStyle,ScaleFontType,ScaleFontSize,
                                 ScaleFontColor,ScaleIsBold,ScaleIsItalics,SplitWeight,UOM,AddToPosCategoryScreen )
                                 values(@SKU,@PDesc,@PType,@PPriceA,@PPriceB,@PPriceC,@PCost, @POnHandQty,@PReorderQty,@PNormalQty,
                                 @iDept,@iCat,@PFS,@PMinAge,@PScaleBrCd,@SKU2,@SKU3,@PLastCost,@PLayawayQty,@PAllowZeroStk,
		     					 @PDispStk,@PAddPOS,@PNoPriceLbl,@PPrompt,@PBinL,@PPrintBrCd,@PLbl,@PPrntQty,@PPoints,@PDecimal,@PActive,@iBrnd,@PUPC,@PSeason,
                                 0,getdate(),0,getdate(),@PBkGrnd,@PScrnColor,@PScrnStyle,@PFont,@PFontS,@PFontC,@PBold,@PItalics,
		     					 @PCaseQty,@PCUPC,@PLinkSKU,@PBrkRatio,@PRentalPerMinute,@PRentalPerHour,@PRentalPerHalfDay,@PRentalPerDay,@PRentalPerWeek,@PRentalPerMonth,
                                 @PRentalDeposit,@PMinSrv,@PRepairCharge,@PRentalMinHour,@PRentalMinAmount,@PRental,@PReprCrg,@PReprTag,getdate(),
                                 @PNotes,@Tare,@PAddScale,@PNonDiscountable,@PNotes2,@Tare2,@ItemDisplayOrder,@SBkGrnd,@SScrnColor,@SScrnStyle,@SFont,@SFontS,@SFontC,@SBold,@SItalics,@SplitWeight,@UOM,
								 @PAddPOSCat)
    select @ID = @@IDENTITY


    if @ID > 0 begin

       if @iTx1 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @iTx1,@ID,'Product',0,getdate(),0,getdate() ) ;

       if @iTx2 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @iTx2,@ID,'Product',0,getdate(),0,getdate() ) ;

        if @iTx3 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @iTx3,@ID,'Product',0,getdate(),0,getdate() ) ;
           
           
        if @irTx1 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @irTx1,@ID,'Rent',0,getdate(),0,getdate() ) ;

       if @irTx2 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @irTx2,@ID,'Rent',0,getdate(),0,getdate() ) ;

        if @irTx3 > 0
    
           insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
           values ( @irTx3,@ID,'Rent',0,getdate(),0,getdate() ) ;    
      
        if @POnHandQty <> 0 

          insert into StockJournal(DocNo,DocDate,ProductID,TranType,TranSubType,Qty,Cost,StockOnHand,TerminalName,EmpID,TranDate)
          values (cast(@ID as varchar(10)),getdate(),@ID,'Stock In','Opening Stock',@POnHandQty,@PCost,@POnHandQty,@Terminal,0, getdate())

     end
  
  end
  else begin

  select @iDpc = count(*) from Dept where DepartmentID = @DeptID;
    
    if @iDpc > 0 begin
      select @iDept = isnull(ID,0) from Dept where DepartmentID = @DeptID;
    end
    
    if @iDpc = 0 begin
	   set @ScaleDisplayOrder_Dept = 0;
	   if @DeptSF = 'Y' select @ScaleDisplayOrder_Dept = isnull(max(scaledisplayorder),0) + 1 from dept where scaleflag = 'Y';
       insert into dept(DepartmentID,Description,Selected,FoodStampEligible,ScaleFlag,ScaleDisplayOrder, CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) 
                  	values ( @DeptID,@DeptDesc,'N',@DeptFS,@DeptSF,@ScaleDisplayOrder_Dept,0,getdate(),0,getdate()) 
       select @iDept = @@IDENTITY ;
    end

     select @iCtc = count(*) from Category where CategoryID = @CatID;
     if @iCtc > 0 
     select @iCat = isnull(ID,0) from Category where CategoryID = @CatID;

     if @iCtc = 0 begin

        select @iCatDisp = isnull(Max(POSDisplayOrder),0) + 1 from Category;

        insert into category(CategoryID,Description,MaxItemsforPOS,POSDisplayOrder,POSBackground,POSFontType,POSFontSize,POSFontColor,POSItemFontColor,
		        FoodStampEligibleForProduct,POSScreenColor,POSScreenStyle,IsBold,IsItalics,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,
				AddToPOSScreen,NoPriceOnLabel,PrintBarCode,NonDiscountable,ScaleBarcode,DisplayStockinPOS,AllowZeroStock,RepairPromptForTag) 
                                    values ( @CatID,@CatDesc,10,@iCatDisp,@CatBkGrnd,@CatFont,@CatFontS,@CatFontC,@CatItemFontC,@CatFS,
                                    @CatScrnColor,@CatScrnStyle,@CatBold,@CatItalics,0,getdate(),0,getdate(),
									@CatProdCheck1,@CatProdCheck2,@CatProdCheck3,@CatProdCheck4,@CatProdCheck5,@CatProdCheck6,@CatProdCheck7,@CatProdCheck8) 
        select @iCat = @@IDENTITY ;

     end

	 if @CatTx1nm <> ''  begin

        select @icTc1 = count(*) from taxheader where upper(taxname) = upper(@CatTx1nm);
        if @icTc1 > 0 
        select @icTx1 = isnull(ID,0) from taxheader where upper(taxname) = upper(@CatTx1nm);

         if @icTc1 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @CatTx1nm,@CatTx1rt,0,getdate(),0,getdate()) 
                select @icTx1 = @@IDENTITY;

            end     
         end
     end


     if @CatTx2nm <> ''  begin

        select @icTc2 = count(*) from taxheader where upper(taxname) = upper(@CatTx2nm);
        if @icTc2 > 0  
        select @icTx2 = isnull(ID,0) from taxheader where upper(taxname) = upper(@CatTx2nm);

         if @icTc2 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @CatTx2nm,@CatTx2rt,0,getdate(),0,getdate()) 
                select @icTx2 = @@IDENTITY;

            end     
            
         end
        
     end


     if @CatTx3nm <> ''  begin

        select @icTc3 = count(*) from taxheader where upper(taxname) = upper(@CatTx3nm);
        if @icTc3 > 0 
        select @icTx3 = isnull(ID,0) from taxheader where upper(taxname) = upper(@CatTx3nm);

         if @icTc3 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @CatTx3nm,@CatTx3rt,0,getdate(),0,getdate()) 
                select @icTx3 = @@IDENTITY;

            end     
            
         end
        
     end

     if @Tx1nm <> ''  begin

        select @iTc1 = count(*) from taxheader where upper(taxname) = upper(@Tx1nm);
        if @iTc1 > 0 
        select @iTx1 = isnull(ID,0) from taxheader where upper(taxname) = upper(@Tx1nm);

         if @iTc1 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @Tx1nm,@Tx1rt,0,getdate(),0,getdate()) 
                select @iTx1 = @@IDENTITY;

            end     
         end
     end


     if @Tx2nm <> ''  begin

        select @iTc2 = count(*) from taxheader where upper(taxname) = upper(@Tx2nm);
        if @iTc2 > 0  
        select @iTx2 = isnull(ID,0) from taxheader where upper(taxname) = upper(@Tx2nm);

         if @iTc2 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @Tx2nm,@Tx2rt,0,getdate(),0,getdate()) 
                select @iTx2 = @@IDENTITY;

            end     
            
         end
        
     end


     if @Tx3nm <> ''  begin

        select @iTc3 = count(*) from taxheader where upper(taxname) = upper(@Tx3nm);
        if @iTc3 > 0 
        select @iTx3 = isnull(ID,0) from taxheader where upper(taxname) = upper(@Tx3nm);

         if @iTc3 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @Tx3nm,@Tx3rt,0,getdate(),0,getdate()) 
                select @iTx3 = @@IDENTITY;

            end     
            
         end
        
     end
     
     
     
     if @rntTx1nm <> ''  begin

        select @irTc1 = count(*) from taxheader where upper(taxname) = upper(@rntTx1nm);
        if @irTc1 > 0 
        select @irTx1 = isnull(ID,0) from taxheader where upper(taxname) = upper(@rntTx1nm);

         if @irTc1 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @rntTx1nm,@rntTx1rt,0,getdate(),0,getdate()) 
                select @irTx1 = @@IDENTITY;

            end     
         end
     end
     
     if @rntTx2nm <> ''  begin

        select @irTc2 = count(*) from taxheader where upper(taxname) = upper(@rntTx2nm);
        if @irTc2 > 0 
        select @irTx2 = isnull(ID,0) from taxheader where upper(taxname) = upper(@rntTx2nm);

         if @irTc2 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @rntTx2nm,@rntTx2rt,0,getdate(),0,getdate()) 
                select @irTx2 = @@IDENTITY;

            end     
         end
     end
     
     if @rntTx3nm <> ''  begin

        select @irTc3 = count(*) from taxheader where upper(taxname) = upper(@rntTx3nm);
        if @irTc3 > 0 
        select @irTx3 = isnull(ID,0) from taxheader where upper(taxname) = upper(@rntTx3nm);

         if @irTc3 = 0 begin

            set @GoTxAdd = 'N';
            set @tcnt = 0;

            select @tcnt = count(*) from taxheader where active = 'Yes';

            if  @tcnt < 3 set  @GoTxAdd = 'Y';

            if  @GoTxAdd = 'Y' begin

                insert into taxheader(taxname,taxrate,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)  values ( @rntTx3nm,@rntTx3rt,0,getdate(),0,getdate()) 
                select @irTx3 = @@IDENTITY;

            end     
         end
     end
     
     
     
     
     

     if @BrndID <> '' and @BrndDesc <> '' begin

       select @iBrc = count(*) from brandmaster where BrandID = @BrndID;
       if @iBrc > 0
       select @iBrnd = isnull(ID,0) from brandmaster where BrandID = @BrndID;

       if @iBrc = 0 begin
         insert into brandmaster(BrandID,BrandDescription,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) 
                  	values ( @BrndID,@BrndDesc,0,getdate(),0,getdate()) 
         select @iBrnd = @@IDENTITY ;
        end  
     end  
	

	 select @ItemDisplayOrder = isnull(Max(POSDisplayOrder),0) + 1 from product where categoryid = @iCat;

	 select @PID = ID from product where SKU = @SKU and SKU <> '';

     select @PID = ID from Product where SKU2 = @SKU2 and SKU2 <> '';

     select @PID = ID from Product where SKU3 = @SKU3 and SKU3 <> '';

	 select @prevCat = CategoryID from Product where ID = @PID;

	 select @prevOnHandQty = QtyOnHand from Product where ID = @PID;

	 if @iCat <> @prevCat begin

	   exec @rexec1 = sp_UpdateItemDisplayOrder @PID, @iCat;

	 end

     update Product set  SKU = @SKU,Description = @PDesc,ProductType = @PType,PriceA = @PPriceA,PriceB = @PPriceB,
	               PriceC=@PPriceC,Cost=@PCost,QtyOnHand = @POnHandQty,ReorderQty=@PReorderQty,NormalQty=@PNormalQty,
				   DepartmentID = @iDept,CategoryID = @iCat,FoodStampEligible = @PFS,
                   MinimumAge = @PMinAge,ScaleBarCode = @PScaleBrCd,SKU2 = @SKU2,SKU3 = @SKU3,
				   LastCost=@PLastCost,QtyOnLayaway=@PLayawayQty,AllowZeroStock=@PAllowZeroStk,
				   DisplayStockinPOS=@PDispStk,AddtoPOSScreen=@PAddPOS,NoPriceOnLabel=@PNoPriceLbl,
                   PromptForPrice=@PPrompt,BinLocation=@PBinL,PrintBarCode=@PPrintBrCd,LabelType=@PLbl,
				   QtyToPrint=@PPrntQty,Points=@PPoints,DecimalPlace=@PDecimal,
				   ProductStatus=@PActive,BrandID=@iBrnd,UPC=@PUPC,Season=@PSeason,
                   LastChangedBy=0,LastChangedOn=getdate(), 
				   POSBackground=@PBkGrnd,POSScreenColor=@PScrnColor,POSScreenStyle=@PScrnStyle,POSFontType=@PFont,POSFontSize=@PFontS,
                   POSFontColor=@PFontC,IsBold=@PBold,IsItalics=@PItalics,
				   ScaleBackground=@SBkGrnd,ScaleScreenColor=@SScrnColor,ScaleScreenStyle=@SScrnStyle,ScaleFontType=@SFont,ScaleFontSize=@SFontS,
                   ScaleFontColor=@SFontC,ScaleIsBold=@SBold,ScaleIsItalics=@SItalics,
				   CaseQty=@PCaseQty,CaseUPC=@PCUPC,
				   LinkSKU=@PLinkSKU,BreakPackRatio=@PBrkRatio,RentalPerMinute=@PRentalPerMinute,
				   RentalPerHour=@PRentalPerHour,RentalPerHalfDay=@PRentalPerHalfDay,RentalPerDay=@PRentalPerDay,RentalPerWeek=@PRentalPerWeek,
                   RentalPerMonth=@PRentalPerMonth,RentalDeposit=@PRentalDeposit,MinimumServiceTime=@PMinSrv,
				   RepairCharge=@PRepairCharge,RentalMinHour=@PRentalMinHour,RentalMinAmount=@PRentalMinAmount,
                   RentalPrompt=@PRental,RepairPromptForCharge=@PReprCrg,RepairPromptForTag=@PReprTag,
	               ImportDate = getdate(),ProductNotes=@PNotes,Tare=@Tare, 
				   AddToScaleScreen=@PAddScale,NonDiscountable=@PNonDiscountable,Notes2=@PNotes2,
				   Tare2=@Tare2,POSDisplayOrder=@ItemDisplayOrder,SplitWeight=@SplitWeight,UOM=@UOM,AddToPosCategoryScreen=@PAddPOSCat where ID = @PID;
                                 
					

	if @icTx1 > 0 begin
	  set @ExistsCatTax1 = 0;
	  select @ExistsCatTax1 = count(taxid) from taxmapping where mappingtype = 'Category' and mappingid = @iCat and taxid = @icTx1;
	  if @ExistsCatTax1 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @icTx1,@iCat,'Category',0,getdate(),0,getdate() ) ;
	end
    
	if @icTx2 > 0 begin
	  set @ExistsCatTax2 = 0;
	  select @ExistsCatTax2 = count(taxid) from taxmapping where mappingtype = 'Category' and mappingid = @iCat and taxid = @icTx2;
	  if @ExistsCatTax2 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @icTx2,@iCat,'Category',0,getdate(),0,getdate() ) ;
	end

	if @icTx3 > 0 begin
	  set @ExistsCatTax3 = 0;
	  select @ExistsCatTax3 = count(taxid) from taxmapping where mappingtype = 'Category' and mappingid = @iCat and taxid = @icTx3;
	  if @ExistsCatTax3 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @icTx3,@iCat,'Category',0,getdate(),0,getdate() ) ;
	end

    if @iTx1 > 0 begin
	  set @ExistsTax1 = 0;
	  select @ExistsTax1 = count(taxid) from taxmapping where mappingtype = 'Product' and mappingid = @PID and taxid = @iTx1;
	  if @ExistsTax1 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @iTx1,@PID,'Product',0,getdate(),0,getdate() ) ;
	end
    
	if @iTx2 > 0 begin
	  set @ExistsTax2 = 0;
	  select @ExistsTax2 = count(taxid) from taxmapping where mappingtype = 'Product' and mappingid = @PID and taxid = @iTx2;
	  if @ExistsTax2 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @iTx2,@PID,'Product',0,getdate(),0,getdate() ) ;
	end
          
    if @iTx3 > 0 begin
	  set @ExistsTax3 = 0;
	  select @ExistsTax3 = count(taxid) from taxmapping where mappingtype = 'Product' and mappingid = @PID and taxid = @iTx3;
	  if @ExistsTax3 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @iTx3,@PID,'Product',0,getdate(),0,getdate() ) ;
	end

	if @irTx1 > 0 begin
	  set @ExistsRentalTax1 = 0;
	  select @ExistsRentalTax1 = count(taxid) from taxmapping where mappingtype = 'Rent' and mappingid = @PID and taxid = @irTx1;
	  if @ExistsRentalTax1 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @irTx1,@PID,'Rent',0,getdate(),0,getdate() ) ;
	end

	if @irTx2 > 0 begin
	  set @ExistsRentalTax2 = 0;
	  select @ExistsRentalTax2 = count(taxid) from taxmapping where mappingtype = 'Rent' and mappingid = @PID and taxid = @irTx2;
	  if @ExistsRentalTax2 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @irTx2,@PID,'Rent',0,getdate(),0,getdate() ) ;
	end

	if @irTx3 > 0 begin
	  set @ExistsRentalTax3 = 0;
	  select @ExistsRentalTax3 = count(taxid) from taxmapping where mappingtype = 'Rent' and mappingid = @PID and taxid = @irTx3;
	  if @ExistsRentalTax3 = 0 
	    insert into taxmapping ( taxid,mappingid,mappingtype,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) 
        values ( @irTx3,@PID,'Rent',0,getdate(),0,getdate() ) ;
	end
      
    if @POnHandQty <> @prevOnHandQty begin
	
	  set @IsExistInStockJournal = 0;
	  select @IsExistInStockJournal = count(ProductID) from stockjournal where ProductID = @PID;
	  if (@POnHandQty > @prevOnHandQty)
      begin

	     if @IsExistInStockJournal = 0
		    insert into StockJournal(DocNo,DocDate,ProductID,TranType,TranSubType,Qty,Cost,StockOnHand,TerminalName,EmpID,TranDate)
            values (cast(@PID as varchar(10)),getdate(),@PID,'Stock In','Opening Stock',@POnHandQty - @prevOnHandQty,@PCost,@POnHandQty,@Terminal,0, getdate())
          else 
		   insert into StockJournal(DocNo,DocDate,ProductID,TranType,TranSubType,Qty,Cost,StockOnHand,TerminalName,EmpID,TranDate)
            values (cast(@PID as varchar(10)),getdate(),@PID,'Stock In','Manual aAdjustment',@POnHandQty - @prevOnHandQty,@PCost,@POnHandQty,@Terminal,0, getdate())
       end
       else begin

	       if @IsExistInStockJournal = 0
		    insert into StockJournal(DocNo,DocDate,ProductID,TranType,TranSubType,Qty,Cost,StockOnHand,TerminalName,EmpID,TranDate)
            values (cast(@PID as varchar(10)),getdate(),@PID,'Stock In','Opening Stock',@POnHandQty - @prevOnHandQty,@PCost,@POnHandQty,@Terminal,0, getdate())
          else 
		   insert into StockJournal(DocNo,DocDate,ProductID,TranType,TranSubType,Qty,Cost,StockOnHand,TerminalName,EmpID,TranDate)
            values (cast(@PID as varchar(10)),getdate(),@PID,'Stock Out','Manual aAdjustment',@prevOnHandQty - @POnHandQty,@PCost,@POnHandQty,@Terminal,0, getdate())

	   end
	
	end 

    set @ID = @PID;      

  end


  
end

GO

DROP PROCEDURE IF EXISTS [dbo].[sp_store_imp_cust_h]
GO

CREATE procedure [dbo].[sp_store_imp_cust_h]
			
			@CustomerID		nvarchar(20),
			@AccountNo		nvarchar(7),
			@LastName		nvarchar(20),
			@FirstName		nvarchar(20),
			@Spouse			nvarchar(20),
			@Company		nvarchar(30),
			@Salutation		nvarchar(4),
			@Address1		nvarchar(60),
			@Address2		nvarchar(60),
			@City			nvarchar(20),
			@State			nvarchar(2),
			@Zip			nvarchar(12),
			@Country		nvarchar(30),
			@ShipAddress1	nvarchar(60),
			@ShipAddress2	nvarchar(60),
			@ShipCity		nvarchar(20),
			@ShipState		nvarchar(2),
			@ShipZip		nvarchar(12),
			@ShipCountry	nvarchar(30),
			@WorkPhone		varchar(14),
			@HomePhone		varchar(14),
			@MobilePhone	nvarchar(14),
			@Fax			nvarchar(14),
			@EMail			nvarchar(60),
			@TaxExempt		char(1),
			@TaxID			nvarchar(12),
			@DiscountLevel	char(1),
			@StoreCredit	numeric(15, 3),
			@DateLastPurchase	datetime,
			@AmountLastPurchase	numeric(15, 3),
			@TotalPurchases		numeric(15, 3),
			@ARCreditLimit		numeric(15, 3),
			@CreatedBy			int,
			@CreatedOn			datetime,
			@LastChangedBy		int,
			@LastChangedOn		datetime,
			@DateOfBirth		datetime,
			@DateOfMarriage		datetime,
			@ClosingBalance		numeric(15, 3),
			@Points				int,
			@StoreCreditCard	char(1),
			@ParamValue1		nvarchar(30),
			@ParamValue2		nvarchar(30),
			@ParamValue3		nvarchar(30),
			@ParamValue4		nvarchar(30),
			@ParamValue5		nvarchar(30),
			@POSNotes			nvarchar(100),
			@IssueStore			nvarchar(10),
			@OperateStore		nvarchar(10),
			@ActiveStatus		char(1),
			@RefDiscount		nvarchar(50)
as

declare 	@custid		int; 	

declare @zcnt1 int;
declare @zcnt2 int;
declare @DiscountID	int;

begin

  set @custid = 0;

  select @custid = isnull(id,0) from Customer where issuestore = @IssueStore and customerid=@CustomerID

  set @DiscountID = 0;
  if @RefDiscount <> '' 
    select @DiscountID = isnull(ID,0) from DiscountMaster where DiscountName = @RefDiscount;

  if @custid = 0 
  insert into Customer ( CustomerID,AccountNo,LastName,FirstName,Spouse,Company,Salutation,Address1,Address2,City,State,Zip,Country,ShipAddress1,
			 ShipAddress2,ShipCity,ShipState,ShipZip,ShipCountry,WorkPhone,HomePhone,MobilePhone,Fax,EMail,TaxExempt,TaxID,DiscountLevel,StoreCredit,DateLastPurchase,AmountLastPurchase,
		               TotalPurchases,ARCreditLimit,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DateOfBirth,DateOfMarriage,ClosingBalance,Points,StoreCreditCard,ParamValue1,
			 ParamValue2,ParamValue3,ParamValue4,ParamValue5,POSNotes,IssueStore,OperateStore,ExpFlag,DiscountID, ActiveStatus )
                               values(  @CustomerID,@AccountNo,@LastName,@FirstName,@Spouse,@Company,@Salutation,@Address1,@Address2,@City,@State,@Zip,@Country,@ShipAddress1,
			 @ShipAddress2,@ShipCity,@ShipState,@ShipZip,@ShipCountry,@WorkPhone,@HomePhone,@MobilePhone,@Fax,@EMail,@TaxExempt,@TaxID,@DiscountLevel,@StoreCredit,
			 @DateLastPurchase,@AmountLastPurchase,@TotalPurchases,@ARCreditLimit,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@DateOfBirth,@DateOfMarriage,
			 @ClosingBalance,@Points,@StoreCreditCard,@ParamValue1,@ParamValue2,@ParamValue3,@ParamValue4,@ParamValue5,@POSNotes,@IssueStore,@OperateStore, 'Y',@DiscountID,@ActiveStatus );
  if @custid > 0 
  update Customer set AccountNo=@AccountNo,LastName=@LastName,FirstName=@FirstName,Spouse=@Spouse,Company=@Company,Salutation=@Salutation,Address1=@Address1,Address2=@Address2,
                                  	City=@City,State=@State,Zip=@Zip,Country=@Country,ShipAddress1=@ShipAddress1,ShipAddress2=@ShipAddress2,ShipCity=@ShipCity,ShipState=@ShipState,ShipZip=@ShipZip,
			ShipCountry=@ShipCountry,WorkPhone=@WorkPhone,HomePhone=@HomePhone,MobilePhone=@MobilePhone,Fax=@Fax,EMail=@EMail,TaxExempt=@TaxExempt,TaxID=@TaxID,
			DiscountLevel=@DiscountLevel,StoreCredit=@StoreCredit,DateLastPurchase=@DateLastPurchase,AmountLastPurchase=@AmountLastPurchase,
			TotalPurchases=@TotalPurchases,ARCreditLimit=@ARCreditLimit,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn,DateOfBirth=@DateOfBirth,
			DateOfMarriage=@DateOfMarriage,ClosingBalance=@ClosingBalance,Points=@Points,StoreCreditCard=@StoreCreditCard,
			ParamValue1=@ParamValue1,ParamValue2=@ParamValue2,ParamValue3=@ParamValue3,ParamValue4=@ParamValue4,ParamValue5=@ParamValue5,
			POSNotes=@POSNotes,OperateStore=@OperateStore,ExpFlag='Y',DiscountID=@DiscountID,ActiveStatus = @ActiveStatus where id = @custid
			
			
			
  if @Zip <> '' begin
    set @zcnt1 = 0;
    select @zcnt1 = count(*) from zipcode where zip = @Zip;
    if @zcnt1 = 0 
      insert into zipcode(City,State,Zip,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values(@City,@State,@Zip,0,getdate(),0,getdate());
  end			
	  	
	  	
	if @ShipZip <> '' begin
    set @zcnt2 = 0;
    select @zcnt2 = count(*) from zipcode where zip = @ShipZip;
    if @zcnt2 = 0 
      insert into zipcode(City,State,Zip,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values(@ShipCity,@ShipState,@ShipZip,0,getdate(),0,getdate());
  end
	  	
	  		
			
			
end	

GO

DROP PROCEDURE IF EXISTS [dbo].[sp_store_imp_emp_h]
GO

CREATE procedure [dbo].[sp_store_imp_emp_h]
			@EmployeeID			nvarchar(12),
			@LastName			nvarchar(20),
			@FirstName			nvarchar(20),
			@Address1			nvarchar(60),
			@Address2			nvarchar(60),
			@City				nvarchar(20),
			@State				nvarchar(2),
			@Zip				nvarchar(12),
			@Phone1				nvarchar(14),
			@Phone2				nvarchar(14),
			@EmergencyPhone			nvarchar(14),
			@EmergencyContact		nvarchar(30),
			@EMail				nvarchar(60),
			@SSNumber			nvarchar(11),
			@EmpRate			numeric(15, 3),
			@IssueStore			nvarchar(10),
			@OperateStore			nvarchar(10)
as

declare @empid	int; 	
declare @zcnt1 	int;
declare @zcnt2 	int;

begin

  set @empid = 0;

  select @empid = isnull(id,0) from Employee where issuestore = @IssueStore and employeeid=@EmployeeID

  if @empid = 0 
  insert into Employee ( EmployeeID,LastName,FirstName,Address1,Address2,City,State,Zip,
						 Phone1,Phone2,EmergencyPhone,EmergencyContact,EMail,SSNumber,EmpRate,IssueStore,OperateStore,ExpFlag )
                values(  @EmployeeID,@LastName,@FirstName,@Address1,@Address2,@City,@State,@Zip,
						 @Phone1,@Phone2,@EmergencyPhone,@EmergencyContact,@EMail,@SSNumber,@EmpRate,
					     @IssueStore,@OperateStore, 'Y' );
  if @empid > 0 
  update Employee set LastName=@LastName,FirstName=@FirstName,Address1=@Address1,Address2=@Address2,
            City=@City,State=@State,Zip=@Zip,Phone1=@Phone1,Phone2=@Phone2,EmergencyPhone=@EmergencyPhone,
            EmergencyContact=@EmergencyContact,EMail=@EMail,SSNumber=@SSNumber,EmpRate=@EmpRate,
            OperateStore=@OperateStore,ExpFlag='Y' where id = @empid
			
			
			
  if @Zip <> '' begin
    set @zcnt1 = 0;
    select @zcnt1 = count(*) from zipcode where zip = @Zip;
    if @zcnt1 = 0 
      insert into zipcode(City,State,Zip,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values(@City,@State,@Zip,0,getdate(),0,getdate());
  end			
	  	
	  	
	
	  	
	  		
			
			
end

GO

DROP PROCEDURE IF EXISTS [dbo].[sp_initializereceipts]
GO

CREATE procedure [dbo].[sp_initializereceipts]
  	@ReturnID	int output

as

declare @qty numeric(15,3);
declare @ptype char(1);
declare @pid int;

begin


  IF OBJECT_ID('Invoice', 'U') IS NOT NULL  DROP TABLE Invoice;
  IF OBJECT_ID('Item', 'U') IS NOT NULL  DROP TABLE Item;
  IF OBJECT_ID('Trans', 'U') IS NOT NULL  DROP TABLE Trans;
  IF OBJECT_ID('Closeout', 'U') IS NOT NULL  DROP TABLE Closeout;
  IF OBJECT_ID('CloseoutTender', 'U') IS NOT NULL  DROP TABLE CloseoutTender;
  IF OBJECT_ID('CloseoutCurrencyCalculator', 'U') IS NOT NULL  DROP TABLE CloseoutCurrencyCalculator;
  IF OBJECT_ID('Tender', 'U') IS NOT NULL  DROP TABLE Tender;
  IF OBJECT_ID('Layaway', 'U') IS NOT NULL  DROP TABLE Layaway;
  IF OBJECT_ID('LayPmts', 'U') IS NOT NULL  DROP TABLE LayPmts;
  IF OBJECT_ID('ItemMatrixOptions', 'U') IS NOT NULL  DROP TABLE ItemMatrixOptions;
  IF OBJECT_ID('SerialDetail', 'U') IS NOT NULL  DROP TABLE SerialDetail;
  IF OBJECT_ID('Suspnded', 'U') IS NOT NULL  DROP TABLE Suspnded;
  IF OBJECT_ID('Notes', 'U') IS NOT NULL  DROP TABLE Notes;
  IF OBJECT_ID('GiftCert', 'U') IS NOT NULL  DROP TABLE GiftCert;
  IF OBJECT_ID('AcctRecv', 'U') IS NOT NULL  DROP TABLE AcctRecv;
  IF OBJECT_ID('CardAuthorisation', 'U') IS NOT NULL  DROP TABLE CardAuthorisation;
  IF OBJECT_ID('CardTrans', 'U') IS NOT NULL  DROP TABLE CardTrans;
  IF OBJECT_ID('workorder', 'U') IS NOT NULL  DROP TABLE workorder;
  IF OBJECT_ID('VoidInv', 'U') IS NOT NULL  DROP TABLE VoidInv;
  IF OBJECT_ID('StoreCreditTransaction', 'U') IS NOT NULL  DROP TABLE StoreCreditTransaction;

  /*  drop table Invoice;
  drop table Item;
  drop table Trans;
  drop table Closeout;
  drop table CloseoutTender;
  drop table Tender;
  drop table Layaway;
  drop table LayPmts;
  drop table ItemMatrixOptions;
  drop table SerialDetail;
  drop table Suspnded;
  drop table Notes;

  drop table GiftCert;
  drop table AcctRecv;
  drop table CardAuthorisation;
  drop table CardTrans;
  drop table workorder;
  drop table VoidInv;*/

  CREATE TABLE [dbo].[Invoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL CONSTRAINT [DF_Invoice_CustomerID]  DEFAULT ((0)),
	[EmployeeID] [int] NULL CONSTRAINT [DF_Invoice_EmployeeID]  DEFAULT ((0)),
	[TransactionNo] [int] NULL CONSTRAINT [DF_Invoice_TransactionNo]  DEFAULT ((0)),
	[Tax] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_Tax]  DEFAULT ((0)),
	[TaxID1] [int] NULL CONSTRAINT [DF_Invoice_TaxID1]  DEFAULT ((0)),
	[TaxID2] [int] NULL CONSTRAINT [DF_Invoice_TaxID2]  DEFAULT ((0)),
	[TaxID3] [int] NULL CONSTRAINT [DF_Invoice_TaxID3]  DEFAULT ((0)),
	[Tax1] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_Tax1]  DEFAULT ((0)),
	[Tax2] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_Tax2]  DEFAULT ((0)),
	[Tax3] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_Tax3]  DEFAULT ((0)),
	[Discount] [numeric](15, 3) NOT NULL,
    [Coupon] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_Coupon]  DEFAULT ((0)),
	[TotalSale] [numeric](15, 3) NULL,
	[DiscountReason] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [int] NULL,
	[ReceiptCnt] [int] NULL CONSTRAINT [DF_Invoice_ReceiptCnt]  DEFAULT ((1)),
	[LayawayNo] [int] NULL CONSTRAINT [DF_Invoice_LayawayNo]  DEFAULT ((0)),
	[TransMSeconds] [int] NULL CONSTRAINT [DF_Invoice_TransMSeconds]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_Invoice_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Invoice_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[ChangedByAdmin] [int] NULL,
	[ChangedOnAdmin] [datetime] NULL,
	[BreakpackFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Invoice_BreakpackFlag]  DEFAULT ('Y'),
	[ServiceType] [varchar](10) NULL CONSTRAINT [DF_Invoice_ServiceType]  DEFAULT ('Sales'),
	[RentDeposit] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_RentDeposit]  DEFAULT ((0)),
	[RentReturnDeposit] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_RentReturnDeposit]  DEFAULT ((0)),
	[RentReturnFlag] [char](1) NULL CONSTRAINT [DF_Invoice_RentReturnFlag]  DEFAULT ('N'),
	[RepairDeliveryDate] [datetime] NULL,
	[RepairNotifiedDate] [datetime] NULL,
	[RepairProblem] [nvarchar](200) NULL,
	[RepairNotes] [nvarchar](200) NULL,
	[RepairRemarks] [nvarchar](200) NULL,
	[RepairAdvanceAmount] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_RepairAdvanceAmount]  DEFAULT ((0)),
	[RepairAmount] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_RepairAmount]  DEFAULT ((0)),
	[RepairDueAmount] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_RepairDueAmount]  DEFAULT ((0)),
	[RepairStatus] [varchar](20) NULL CONSTRAINT [DF_Invoice_RepairStatus]  DEFAULT ('In'),
	[RentParentID] [int] NULL CONSTRAINT [DF_Invoice_RentParentID]  DEFAULT ((0)),
	[RepairParentID] [int] NULL CONSTRAINT [DF_Invoice_RepairParentID]  DEFAULT ((0)),
        [IsRentCalculated] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Invoice_IsRentCalculated]  DEFAULT ('N'),
        [CouponPerc] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_CouponPerc]  DEFAULT ((0)),
        [Fees] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_Fees]  DEFAULT ((0)),
        [FeesTax] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_FeesTax]  DEFAULT ((0)), 
        [DTaxID] [int] NULL CONSTRAINT [DF_Invoice_DTaxID]  DEFAULT ((0)),
        [DTax] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_DTax]  DEFAULT ((0)),
	[RepairItemName] [nvarchar](50) NULL,
	[RepairItemSlNo] [nvarchar](30) NULL,
	[RepairDateIn] [datetime] NULL,
	[RepairFindUs] [nvarchar](50) NULL,
	[CustomerOrderRef] [int] NULL CONSTRAINT [DF_Invoice_CustomerOrderRef]  DEFAULT ((0)),
	[FeesCoupon] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_FeesCoupon]  DEFAULT ((0)),
    [FeesCouponTax] [numeric](15, 3) NULL CONSTRAINT [DF_Invoice_FeesTaxCoupon]  DEFAULT ((0)), 
[CustomerDOB] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_TaxID1')
DROP INDEX [IX_Invoice_TaxID1] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_TaxID1] ON [dbo].[Invoice] 
(
	[TaxID1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_TaxID2')
DROP INDEX [IX_Invoice_TaxID2] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_TaxID2] ON [dbo].[Invoice] 
(
	[TaxID2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_TaxID3')
DROP INDEX [IX_Invoice_TaxID3] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_TaxID3] ON [dbo].[Invoice] 
(
	[TaxID3] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_TransactionNo')
DROP INDEX [IX_Invoice_TransactionNo] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_TransactionNo] ON [dbo].[Invoice] 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Status')
DROP INDEX [IX_Invoice_Status] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Status] ON [dbo].[Invoice] 
(
	[Status] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Coupon')
DROP INDEX [IX_Invoice_Coupon] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Coupon] ON [dbo].[Invoice] 
(
	[Coupon] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_CouponPerc')
DROP INDEX [IX_Invoice_CouponPerc] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_CouponPerc] ON [dbo].[Invoice] 
(
	[CouponPerc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Discount')
DROP INDEX [IX_Invoice_Discount] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Discount] ON [dbo].[Invoice] 
(
	[Discount] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_DTax')
DROP INDEX [IX_Invoice_DTax] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_DTax] ON [dbo].[Invoice] 
(
	[DTax] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_DTaxID')
DROP INDEX [IX_Invoice_DTaxID] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_DTaxID] ON [dbo].[Invoice] 
(
	[DTaxID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Fees')
DROP INDEX [IX_Invoice_Fees] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Fees] ON [dbo].[Invoice] 
(
	[Fees] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_FeesTax')
DROP INDEX [IX_Invoice_FeesTax] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_FeesTax] ON [dbo].[Invoice] 
(
	[FeesTax] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_LayawayNo')
DROP INDEX [IX_Invoice_LayawayNo] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_LayawayNo] ON [dbo].[Invoice] 
(
	[LayawayNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Tax')
DROP INDEX [IX_Invoice_Tax] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Tax] ON [dbo].[Invoice] 
(
	[Tax] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Tax1')
DROP INDEX [IX_Invoice_Tax1] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Tax1] ON [dbo].[Invoice] 
(
	[Tax1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Tax2')
DROP INDEX [IX_Invoice_Tax2] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Tax2] ON [dbo].[Invoice] 
(
	[Tax2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_Tax3')
DROP INDEX [IX_Invoice_Tax3] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_Tax3] ON [dbo].[Invoice] 
(
	[Tax3] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_TotalSale')
DROP INDEX [IX_Invoice_TotalSale] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_TotalSale] ON [dbo].[Invoice] 
(
	[TotalSale] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_CustomerOrderRef')
DROP INDEX [IX_Invoice_TransactionNo] ON [dbo].[Invoice] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Invoice_CustomerOrderRef] ON [dbo].[Invoice] 
(
	[CustomerOrderRef] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

CREATE TABLE [dbo].[Item](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [int] NULL,
	[ProductID] [int] NULL,
	[DescID] [int] NULL CONSTRAINT [DF_Item_DescID]  DEFAULT ((0)),
	[Price] [numeric](15, 3) NULL,
	[NormalPrice] [numeric](15, 3) NULL,
	[Qty] [numeric](15, 3) NULL,
	[Cost] [numeric](15, 3) NULL,
	[SKU] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DepartmentID] [int] NULL CONSTRAINT [DF_Item_DepartmentID]  DEFAULT ((0)),
	[CategoryID] [int] NULL CONSTRAINT [DF_Item_CategoryID]  DEFAULT ((0)),
	[TaxID1] [int] NULL CONSTRAINT [DF_Item_Tax1ID]  DEFAULT ((0)),
	[TaxID2] [int] NULL CONSTRAINT [DF_Item_Tax2ID]  DEFAULT ((0)),
	[TaxID3] [int] NULL CONSTRAINT [DF_Item_Tax3ID]  DEFAULT ((0)),
	[Taxable1] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_Taxable1]  DEFAULT ('N'),
	[Taxable2] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_Taxable2]  DEFAULT ('N'),
	[Taxable3] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_Taxable3]  DEFAULT ('N'),
	[TaxRate1] [numeric](15, 3) NULL CONSTRAINT [DF_Item_Tax1Rate]  DEFAULT ((0)),
	[TaxRate2] [numeric](15, 3) NULL CONSTRAINT [DF_Item_Tax2Rate]  DEFAULT ((0)),
	[TaxRate3] [numeric](15, 3) NULL CONSTRAINT [DF_Item_Tax3Rate]  DEFAULT ((0)),
	[UOMCount] [numeric](15, 3) NULL CONSTRAINT [DF_Item_UOMCount]  DEFAULT ((0)),
	[UOMPrice] [numeric](15, 3) NULL CONSTRAINT [DF_Item_UOMPrice]  DEFAULT ((0)),
	[UOMDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PromoID] [int] NULL,
	[PercentFS] [numeric](15, 3) NULL CONSTRAINT [DF_Item_PercentFS]  DEFAULT ((0)),
	[ReturnedItemID] [int] NULL CONSTRAINT [DF_Item_ReturnedItemID]  DEFAULT ((0)),
	[ReturnedItemCnt] [numeric](15, 3) NULL CONSTRAINT [DF_Item_ReturnedItemCnt]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_Item_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Item_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[Notes] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tagged] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_Tagged]  DEFAULT ('N'),

	[ItemIndex] [int] NULL CONSTRAINT [DF_Item_ItemIndex]  DEFAULT ((1)),
	[DiscountID] [int] NULL CONSTRAINT [DF_Item_DiscountID]  DEFAULT ((0)),
	[Discount] [numeric](15, 3) NULL CONSTRAINT [DF_Item_Discount]  DEFAULT ((0)),
	[DiscValue] [numeric](15, 3) NULL CONSTRAINT [DF_Item_DiscValue]  DEFAULT ((0)),
	[DiscountText] [nvarchar](60) NULL CONSTRAINT [DF_Item_DiscountText]  DEFAULT (''),
	[DiscLogic] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_DiscLogic]  DEFAULT (''),
	[BreakpackFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_BreakpackFlag]  DEFAULT ('Y'),
	
	[ServiceType] [varchar](10) NULL CONSTRAINT [DF_Item_ServiceType]  DEFAULT ('Sales'),
	[RentApplicable] [char](2) NULL CONSTRAINT [DF_Item_RentApplicable]  DEFAULT ('NA'),
	[RentEffectiveFrom] [datetime] NULL,
	[RentDuration] [numeric](15, 3) NULL CONSTRAINT [DF_Item_RentDuration]  DEFAULT ((0)),
	[RentDeposit] [numeric](15, 3) NULL CONSTRAINT [DF_Item_RentDeposit]  DEFAULT ((0)),
	[RentReturnFlag] [char](1) NULL CONSTRAINT [DF_Item_RentReturnFlag]  DEFAULT ('N'),
	[RepairItemTag] [nvarchar](30) NULL,
	[RepairItemSLNO] [nvarchar](30) NULL,
	[RepairItemPurchaseDate] [datetime] NULL,
	[RepairItemDeliveryDate] [datetime] NULL,
	[TaxType1] [int] NULL CONSTRAINT [DF_Item_TaxType1]  DEFAULT ((0)),
	[TaxType2] [int] NULL CONSTRAINT [DF_Item_TaxType2]  DEFAULT ((0)),
	[TaxType3] [int] NULL CONSTRAINT [DF_Item_TaxType3]  DEFAULT ((0)),

	[TaxTotal1] [numeric](15, 3) NULL CONSTRAINT [DF_Item_TaxTotal11]  DEFAULT ((0)),
	[TaxTotal2] [numeric](15, 3) NULL CONSTRAINT [DF_Item_TaxTotal12]  DEFAULT ((0)),
	[TaxTotal3] [numeric](15, 3) NULL CONSTRAINT [DF_Item_TaxTotal13]  DEFAULT ((0)),
    	[FSTender] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Item_FSTender] DEFAULT ('N'),
    	[MixMatchID] [int] NULL CONSTRAINT [DF_Item_MixMatchID]  DEFAULT ((0)),

    	[FeesID] [int] NULL CONSTRAINT [DF_Item_FeesID]  DEFAULT ((0)),
    	[FeesLogic] [char](1) NULL CONSTRAINT [DF_Item_FeesLogic]  DEFAULT (('')),
    	[FeesValue] [numeric](15, 3) NULL CONSTRAINT [DF_Item_FeesValue]  DEFAULT ((0)),
    	[FeesTaxRate] [numeric](15, 3) NULL CONSTRAINT [DF_Item_FeesTaxRate]  DEFAULT ((0)),
    	[Fees] [numeric](15, 3) NULL CONSTRAINT [DF_Item_Fees]  DEFAULT ((0)),
    	[FeesTax] [numeric](15, 3) NULL CONSTRAINT [DF_Item_FeesTax]  DEFAULT ((0)), 
    	[FeesText] [nvarchar](60) NULL CONSTRAINT [DF_Item_FeesText]  DEFAULT (('')),
	[FeesQty] [char](1) NULL CONSTRAINT [DF_Item_FeesQty]  DEFAULT (('N')),
    	[SalePriceID] [int] NULL CONSTRAINT [DF_Item_SalePriceID]  DEFAULT ((0)),	

        [DTaxID] [int] NULL CONSTRAINT [DF_Item_DTaxID]  DEFAULT ((0)),
        [DTaxType] [int] NULL CONSTRAINT [DF_Item_DTaxType]  DEFAULT ((0)),
	[DTaxRate] [numeric](15, 3) NULL CONSTRAINT [DF_Item_DTaxRate]  DEFAULT ((0)),
        [DTax] [numeric](15, 3) NULL CONSTRAINT [DF_Item_DTax]  DEFAULT ((0)),
	[EditFlag] [char](1) NULL CONSTRAINT [DF_Item_EditFlag]  DEFAULT (('N')),
	[QtyDecimal] [int] NULL CONSTRAINT [DF_Item_QtyDecimal]  DEFAULT ((0)),
	[PromptPrice] [char](1) NULL CONSTRAINT [DF_Item_PromptPrice]  DEFAULT (('N')),

	[BuyNGetFreeHeaderID] [int] NULL CONSTRAINT [DF_Item_BuyNGetFreeHeaderID]  DEFAULT ((0)),
	[BuyNGetFreeCategory] [char](1) NULL CONSTRAINT [DF_Item_BuyNGetFreeCategory]  DEFAULT (('X')),
	[BuyNGetFreeName] [nvarchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Age] [int] NULL CONSTRAINT [DF_Item_Age]  DEFAULT ((0)),
	[TaxIncludeRate] [numeric](15, 3) NULL CONSTRAINT [DF_Item_TaxIncludeRate]  DEFAULT ((0)),
	[TaxIncludePrice] [numeric](15, 3) NULL CONSTRAINT [DF_Item_TaxIncludePrice]  DEFAULT ((0)),
	[UOM] [nvarchar](15) NULL DEFAULT (('')),
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_DescID')
DROP INDEX [IX_Item_DescID] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_DescID] ON [dbo].[Item] 
(
	[DescID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_FSTender')
DROP INDEX [IX_Item_FSTender] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_FSTender] ON [dbo].[Item] 
(
	[FSTender] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_InvoiceNo')
DROP INDEX [IX_Item_InvoiceNo] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_InvoiceNo] ON [dbo].[Item] 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_ProductID')
DROP INDEX [IX_Item_ProductID] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_ProductID] ON [dbo].[Item] 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_ProductType')
DROP INDEX [IX_Item_ProductType] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_ProductType] ON [dbo].[Item] 
(
	[ProductType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_ServiceType')
DROP INDEX [IX_Item_ServiceType] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_ServiceType] ON [dbo].[Item] 
(
	[ServiceType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_tagged')
DROP INDEX [IX_Item_Tagged] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_tagged] ON [dbo].[Item] 
(
	[Tagged] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Cost')
DROP INDEX [IX_Item_Cost] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Cost] ON [dbo].[Item] 
(
	[Cost] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_DepartmentID')
DROP INDEX [IX_Item_DepartmentID] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_DepartmentID] ON [dbo].[Item] 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Discount')
DROP INDEX [IX_Item_Discount] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Discount] ON [dbo].[Item] 
(
	[Discount] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_NormalPrice')
DROP INDEX [IX_Item_NormalPrice] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_NormalPrice] ON [dbo].[Item] 
(
	[NormalPrice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Price')
DROP INDEX [IX_Item_Price] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Price] ON [dbo].[Item] 
(
	[Price] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Qty')
DROP INDEX [IX_Item_Qty] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Qty] ON [dbo].[Item] 
(
	[Qty] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_ReturnedItemCnt')
DROP INDEX [IX_Item_ReturnedItemCnt] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_ReturnedItemCnt] ON [dbo].[Item] 
(
	[ReturnedItemCnt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_ReturnedItemID')
DROP INDEX [IX_Item_ReturnedItemID] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_ReturnedItemID] ON [dbo].[Item] 
(
	[ReturnedItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Taxable1')
DROP INDEX [IX_Item_Taxable1] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Taxable1] ON [dbo].[Item] 
(
	[Taxable1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Taxable2')
DROP INDEX [IX_Item_Taxable2] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Taxable2] ON [dbo].[Item] 
(
	[Taxable2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_Taxable3')
DROP INDEX [IX_Item_Taxable3] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_Taxable3] ON [dbo].[Item] 
(
	[Taxable3] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_TaxRate1')
DROP INDEX [IX_Item_TaxRate1] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_TaxRate1] ON [dbo].[Item] 
(
	[TaxRate1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_TaxRate2')
DROP INDEX [IX_Item_TaxRate2] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_TaxRate2] ON [dbo].[Item] 
(
	[TaxRate2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_TaxRate3')
DROP INDEX [IX_Item_TaxRate3] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_TaxRate3] ON [dbo].[Item] 
(
	[TaxRate3] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_TaxTotal1')
DROP INDEX [IX_Item_TaxTotal1] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_TaxTotal1] ON [dbo].[Item] 
(
	[TaxTotal1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_TaxTotal2')
DROP INDEX [IX_Item_TaxTotal2] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_TaxTotal2] ON [dbo].[Item] 
(
	[TaxTotal2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_TaxTotal3')
DROP INDEX [IX_Item_TaxTotal3] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_TaxTotal3] ON [dbo].[Item] 
(
	[TaxTotal3] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND name = N'IX_Item_QtyDecimal')
DROP INDEX [IX_Item_QtyDecimal] ON [dbo].[Item] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Item_QtyDecimal] ON [dbo].[Item] 
(
	[QtyDecimal] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]







CREATE TABLE [dbo].[Trans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransType] [smallint] NULL CONSTRAINT [DF_Trans_TransType]  DEFAULT ((0)),
	[EmployeeID] [int] NULL CONSTRAINT [DF_Trans_EmployeeID]  DEFAULT ((0)),
	[RegisterID] [int] NULL CONSTRAINT [DF_Trans_RegisterID]  DEFAULT ((0)),
	[StoreID] [int] NULL CONSTRAINT [DF_Trans_StoreID]  DEFAULT ((0)),
	[CustomerID] [int] NULL CONSTRAINT [DF_Trans_CustomerID]  DEFAULT ((0)),
	[TransDate] [datetime] NULL,
	[CloseoutID] [int] NULL CONSTRAINT [DF_Trans_CloseoutID]  DEFAULT ((0)),
	[TransNoteNo] [int] NULL CONSTRAINT [DF_Trans_TransNoteNo]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_Trans_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Trans_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[ChangedByAdmin] [int] NULL,
	[ChangedOnAdmin] [datetime] NULL,
	[TerminalName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Trans_ExpFlag]  DEFAULT ('N'),
	[BreakpackFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Trans_BreakpackFlag]  DEFAULT ('Y'),
	[StoreCreditAmount] [numeric](15, 3) NULL CONSTRAINT [DF_Trans_StoreCreditAmount]  DEFAULT ((0)),
 CONSTRAINT [PK_Trans] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Trans]') AND name = N'IX_Trans_CloseoutID')
DROP INDEX [IX_Trans_CloseoutID] ON [dbo].[Trans] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Trans_CloseoutID] ON [dbo].[Trans] 
(
	[CloseoutID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Trans]') AND name = N'IX_Trans_CustomerID')
DROP INDEX [IX_Trans_CustomerID] ON [dbo].[Trans] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Trans_CustomerID] ON [dbo].[Trans] 
(
	[CustomerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Trans]') AND name = N'IX_Trans_EmployeeID')
DROP INDEX [IX_Trans_EmployeeID] ON [dbo].[Trans] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Trans_EmployeeID] ON [dbo].[Trans] 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Trans]') AND name = N'IX_Trans_TransDate')
DROP INDEX [IX_Trans_TransDate] ON [dbo].[Trans] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Trans_TransDate] ON [dbo].[Trans] 
(
	[TransDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Trans]') AND name = N'IX_Trans_TransType')
DROP INDEX [IX_Trans_TransType] ON [dbo].[Trans] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Trans_TransType] ON [dbo].[Trans] 
(
	[TransType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


CREATE TABLE [dbo].[CloseOut](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CloseoutType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_CloseOut_CloseoutType]  DEFAULT ('C'),
	[ConsolidatedID] [int] NULL CONSTRAINT [DF_CloseOut_ConsolidatedID]  DEFAULT ((0)),
	[StartDatetime] [datetime] NULL,
	[EndDatetime] [datetime] NULL,
	[Notes] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TransactionCnt] [int] NULL CONSTRAINT [DF_CloseOut_TransactionCnt]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_CloseOut_CreatedBy]  DEFAULT ((0)),
	[ChangedBy] [int] NULL CONSTRAINT [DF_CloseOut_ChangedBy]  DEFAULT ((0)),
	[TerminalName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CloseOut] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOut]') AND name = N'IX_CloseOut_CloseoutType')
DROP INDEX [IX_CloseOut_CloseoutType] ON [dbo].[CloseOut] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOut_CloseoutType] ON [dbo].[CloseOut] 
(
	[CloseoutType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOut]') AND name = N'IX_CloseOut_ConsolidatedID')
DROP INDEX [IX_CloseOut_ConsolidatedID] ON [dbo].[CloseOut] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOut_ConsolidatedID] ON [dbo].[CloseOut] 
(
	[ConsolidatedID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOut]') AND name = N'IX_CloseOut_CreatedBy')
DROP INDEX [IX_CloseOut_CreatedBy] ON [dbo].[CloseOut] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOut_CreatedBy] ON [dbo].[CloseOut] 
(
	[CreatedBy] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOut]') AND name = N'IX_CloseOut_enddatetime')
DROP INDEX [IX_CloseOut_enddatetime] ON [dbo].[CloseOut] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOut_enddatetime] ON [dbo].[CloseOut] 
(
	[EndDatetime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOut]') AND name = N'IX_CloseOut_TransactionCnt')
DROP INDEX [IX_CloseOut_TransactionCnt] ON [dbo].[CloseOut] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOut_TransactionCnt] ON [dbo].[CloseOut] 
(
	[TransactionCnt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]



CREATE TABLE [dbo].[CloseOutTender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CloseOutID] [int] NULL,
	[TenderID] [int] NULL,
	[TenderAmount] [numeric](15, 3) NULL CONSTRAINT [DF_CloseOutTender_TenderAmount]  DEFAULT ((0)),
 CONSTRAINT [PK_CloseOutTender] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOutTender]') AND name = N'IX_CloseOutTender_CloseOutID')
DROP INDEX [IX_CloseOutTender_CloseOutID] ON [dbo].[CloseOutTender] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOutTender_CloseOutID] ON [dbo].[CloseOutTender] 
(
	[CloseOutID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CloseOutTender]') AND name = N'IX_CloseOutTender_TenderID')
DROP INDEX [IX_CloseOutTender_TenderID] ON [dbo].[CloseOutTender] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CloseOutTender_TenderID] ON [dbo].[CloseOutTender] 
(
	[TenderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]



CREATE TABLE [dbo].[CloseoutCurrencyCalculator](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefID] [int] NULL,
	[Penny] [nvarchar](10) NULL,
	[Nickel] [nvarchar](10) NULL,
	[Dime] [nvarchar](10) NULL,
	[Quarter] [nvarchar](10) NULL,
	[Halve] [nvarchar](10) NULL,
	[One] [nvarchar](10) NULL,
	[Five] [nvarchar](10) NULL,
	[Ten] [nvarchar](10) NULL,
	[Twenty] [nvarchar](10) NULL,
	[Fifty] [nvarchar](10) NULL,
	[Hundred] [nvarchar](10) NULL,
	[Two] [nvarchar](10) NULL,
	[TwoHundred] [nvarchar](10) NULL,
	[FiveHundred] [nvarchar](10) NULL,
	[TwoPenny] [nvarchar](10) NULL,
	[TwentyPenny] [nvarchar](10) NULL,
 CONSTRAINT [PK_CloseoutCurrencyCalculator] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[GiftCert](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GiftCertID] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [numeric](15, 3) NULL CONSTRAINT [DF_GiftCert_Amount]  DEFAULT ((0)),
	[ItemID] [int] NULL CONSTRAINT [DF_GiftCert_ItemID]  DEFAULT ((0)),
	[TenderNo] [int] NULL CONSTRAINT [DF_GiftCert_TenderNo]  DEFAULT ((0)),
	[RegisterID] [smallint] NULL CONSTRAINT [DF_GiftCert_RegisterID]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_GiftCert_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_GiftCert_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[CustomerID] [int] NULL DEFAULT ((0)),
	[IssueStore] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperateStore] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__GiftCert__ExpFla__28D80438]  DEFAULT ('N'),
        [ImportBatch] [int] NULL CONSTRAINT [DF_GiftCert_ImportBatch]  DEFAULT ((0)),
	[ImportOn] [datetime] NULL,
	[COID] [int] NULL DEFAULT ((0)),
 CONSTRAINT [PK_GiftCert] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GiftCert]') AND name = N'IX_GiftCert_COID')
DROP INDEX [IX_GiftCert_COID] ON [dbo].[GiftCert] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_GiftCert_COID] ON [dbo].[GiftCert] 
(
	[COID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GiftCert]') AND name = N'IX_GiftCert_IssueStore')
DROP INDEX [IX_GiftCert_IssueStore] ON [dbo].[GiftCert] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_GiftCert_IssueStore] ON [dbo].[GiftCert] 
(
	[IssueStore] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GiftCert]') AND name = N'IX_GiftCert_OperateStore')
DROP INDEX [IX_GiftCert_OperateStore] ON [dbo].[GiftCert] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_GiftCert_OperateStore] ON [dbo].[GiftCert] 
(
	[OperateStore] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


CREATE TABLE [dbo].[ItemMatrixOptions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL CONSTRAINT [DF_ItemMatrixOptions_ItemID]  DEFAULT ((0)),
	[MatrixOptionID] [int] NULL CONSTRAINT [DF_ItemMatrixOptions_MatrixOptionID]  DEFAULT ((0)),
	[OptionValue1] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue3] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [int] NULL CONSTRAINT [DF_ItemMatrixOptions_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_ItemMatrixOptions_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
 CONSTRAINT [PK_ItemMatrixOptions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ItemMatrixOptions]') AND name = N'IX_ItemMatrixOptions_ItemID')
DROP INDEX [IX_ItemMatrixOptions_ItemID] ON [dbo].[ItemMatrixOptions] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_ItemMatrixOptions_ItemID] ON [dbo].[ItemMatrixOptions] 
(
	[ItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ItemMatrixOptions]') AND name = N'IX_ItemMatrixOptions_MatrixOptionID')
DROP INDEX [IX_ItemMatrixOptions_MatrixOptionID] ON [dbo].[ItemMatrixOptions] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_ItemMatrixOptions_MatrixOptionID] ON [dbo].[ItemMatrixOptions] 
(
	[MatrixOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


CREATE TABLE [dbo].[Layaway](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateLayaway] [datetime] NULL,
	[DateDue] [datetime] NULL,
	[DateClosed] [datetime] NULL,
	[CreatedBy] [int] NULL CONSTRAINT [DF_Layaway_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Layaway_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[TerminalName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Layaway] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Laypmts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [int] NULL CONSTRAINT [DF_Laypmts_InvoiceNo]  DEFAULT ((0)),
	[TransactionNo] [int] NULL CONSTRAINT [DF_Laypmts_TransactionNo]  DEFAULT ((0)),
	[Payment] [numeric](15, 3) NULL CONSTRAINT [DF_Laypmts_Payment]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_Laypmts_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Laypmts_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[TerminalName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Laypmts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Laypmts]') AND name = N'IX_Laypmts_InvoiceNo')
DROP INDEX [IX_Laypmts_InvoiceNo] ON [dbo].[Laypmts] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Laypmts_InvoiceNo] ON [dbo].[Laypmts] 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Laypmts]') AND name = N'IX_Laypmts_TransactionNo')
DROP INDEX [IX_Laypmts_TransactionNo] ON [dbo].[Laypmts] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Laypmts_TransactionNo] ON [dbo].[Laypmts] 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


CREATE TABLE [dbo].[Notes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefID] [int] NOT NULL,
	[RefType] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [datetime] NULL,
	[SpecialEvent] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_CustNote_SpecialEvent]  DEFAULT ('N'),
	[ScanFile] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentFile] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL,
	[LastChangedOn] [datetime] NULL,
 CONSTRAINT [PK_CustNote] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Notes]') AND name = N'IX_Notes_RefID')
DROP INDEX [IX_Notes_RefID] ON [dbo].[Notes] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Notes_RefID] ON [dbo].[Notes] 
(
	[RefID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Notes]') AND name = N'IX_Notes_RefType')
DROP INDEX [IX_Notes_RefType] ON [dbo].[Notes] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Notes_RefType] ON [dbo].[Notes] 
(
	[RefType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]



CREATE TABLE [dbo].[SerialDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SerialHeaderID] [int] NOT NULL CONSTRAINT [DF_SerialDetail_SerialHeaderID]  DEFAULT ((0)),
	[Serial1] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Serial2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Serial3] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ItemID] [int] NULL CONSTRAINT [DF_SerialDetail_ItemID]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_SerialDetail_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_SerialDetail_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[SoldType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL DEFAULT ('I'),
 CONSTRAINT [PK_SerialDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SerialDetail]') AND name = N'IX_SerialDetail_ItemID')
DROP INDEX [IX_SerialDetail_ItemID] ON [dbo].[SerialDetail] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_SerialDetail_ItemID] ON [dbo].[SerialDetail] 
(
	[ItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SerialDetail]') AND name = N'IX_SerialDetail_SerialHeaderID')
DROP INDEX [IX_SerialDetail_SerialHeaderID] ON [dbo].[SerialDetail] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_SerialDetail_SerialHeaderID] ON [dbo].[SerialDetail] 
(
	[SerialHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]



CREATE TABLE [dbo].[Suspnded](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [int] NULL,
	[ProductID] [int] NULL,
	[PriceA] [numeric](15, 3) NULL,
	[PriceB] [numeric](15, 3) NULL CONSTRAINT [DF_Table_1__1]  DEFAULT ((0)),
	[PriceC] [numeric](15, 3) NULL CONSTRAINT [DF_Table_1_]  DEFAULT ((0)),
	[PriceOverride] [numeric](15, 3) NULL,
	[SalePrice] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_SalePrice]  DEFAULT ((0)),
	[NewPrice] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_NewPrice]  DEFAULT ((0)),
	[Qty] [numeric](15, 3) NULL,
	[Cost] [numeric](15, 3) NULL,
	[SKU] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DepartmentID] [int] NULL CONSTRAINT [DF_Suspnded_DepartmentID]  DEFAULT ((0)),
	[CategoryID] [int] NULL CONSTRAINT [DF_Suspnded_CategoryID]  DEFAULT ((0)),
	[TaxID1] [int] NULL CONSTRAINT [DF_Suspnded_TaxID1]  DEFAULT ((0)),
	[TaxID2] [int] NULL CONSTRAINT [DF_Suspnded_TaxID2]  DEFAULT ((0)),
	[TaxID3] [int] NULL CONSTRAINT [DF_Suspnded_TaxID3]  DEFAULT ((0)),
	[Taxable1] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_Taxable1]  DEFAULT ('N'),
	[Taxable2] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_Taxable2]  DEFAULT ('N'),
	[Taxable3] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_Taxable3]  DEFAULT ('N'),
	[TaxRate1] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxRate1]  DEFAULT ((0)),
	[TaxRate2] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxRate2]  DEFAULT ((0)),
	[TaxRate3] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxRate3]  DEFAULT ((0)),
	[UOMCount] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_UOMCount]  DEFAULT ((0)),
	[UOMPrice] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_UOMPrice]  DEFAULT ((0)),
	[UOMDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CustomerID] [int] NULL CONSTRAINT [DF_Suspnded_CustomerID]  DEFAULT ((0)),
	[DiscountLevel] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTimeSuspended] [datetime] NULL,
	[GiftCertID] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OnSale] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_OnSale]  DEFAULT ('N'),
	[FSPaid] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_FSPaid]  DEFAULT ((0)),
	[MinimumAge] [smallint] NULL CONSTRAINT [DF_Suspnded_MinimumAge]  DEFAULT ((0)),
	[ReturnedItemID] [int] NULL,
	[MatrixOptionID] [int] NULL CONSTRAINT [DF_Suspnded_MatrixOptionID]  DEFAULT ((0)),
	[OptionValue1] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue3] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [int] NULL CONSTRAINT [DF_Suspnded_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Suspnded_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[CloseoutID] [int] NULL DEFAULT ((0)),

	[ItemIndex] [int] NULL CONSTRAINT [DF_Suspnded_ItemIndex]  DEFAULT ((1)),
	[DiscountID] [int] NULL CONSTRAINT [DF_Suspnded_DiscountID]  DEFAULT ((0)),
	[Discount] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_Discount]  DEFAULT ((0)),
	[DiscValue] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_DiscValue]  DEFAULT ((0)),
	[DiscountText] [nvarchar](60) NULL CONSTRAINT [DF_Suspnded_DiscountText]  DEFAULT (''),
	[DiscLogic] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_DiscLogic]  DEFAULT (''),
	[BreakpackFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_BreakpackFlag]  DEFAULT ('Y'),

	[TaxType1] [int] NULL CONSTRAINT [DF_Suspnded_TaxType1]  DEFAULT ((0)),
	[TaxType2] [int] NULL CONSTRAINT [DF_Suspnded_TaxType2]  DEFAULT ((0)),
	[TaxType3] [int] NULL CONSTRAINT [DF_Suspnded_TaxType3]  DEFAULT ((0)),

	[TaxTotal1] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxTotal11]  DEFAULT ((0)),
	[TaxTotal2] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxTotal12]  DEFAULT ((0)),
	[TaxTotal3] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxTotal13]  DEFAULT ((0)),
	
	[MixMatchID] [int] NULL CONSTRAINT [DF_Suspnded_MixMatchID]  DEFAULT ((0)),
    	[MixMatchType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_MixMatchType]  DEFAULT (''),
    	[MixMatchFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_MixMatchFlag]  DEFAULT ('N'),
    	[MixMatchValue] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_MixMatchValue]  DEFAULT ((0)),
    	[MixMatchUnique] [int] NULL CONSTRAINT [DF_Suspnded_MixMatchUnique]  DEFAULT ((0)),
	[MixMatchQty] [int] NULL CONSTRAINT [DF_Suspnded_MixMatchQty]  DEFAULT ((0)),
	[MixMatchLast] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Suspnded_MixMatchLast]  DEFAULT ('N'),


    	[FeesID] [int] NULL CONSTRAINT [DF_Suspnded_FeesID]  DEFAULT ((0)),
    	[FeesLogic] [char](1) NULL CONSTRAINT [DF_Suspnded_FeesLogic]  DEFAULT (('')),
    	[FeesValue] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_FeesValue]  DEFAULT ((0)),
    	[FeesTaxRate] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_FeesTaxRate]  DEFAULT ((0)),
    	[Fees] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_Fees]  DEFAULT ((0)),
    	[FeesTax] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_FeesTax]  DEFAULT ((0)), 
    	[FeesText] [nvarchar](60) NULL CONSTRAINT [DF_Suspnded_FeesText]  DEFAULT (('')),
	[FeesQty] [char](1) NULL CONSTRAINT [DF_Suspnded_FeesQty]  DEFAULT (('N')),

	[SalePriceID] [int] NULL CONSTRAINT [DF_Suspnded_SalePriceID]  DEFAULT ((0)),

	[DTaxID] [int] NULL CONSTRAINT [DF_Suspnded_DTaxID]  DEFAULT ((0)),
        [DTaxType] [int] NULL CONSTRAINT [DF_Suspnded_DTaxType]  DEFAULT ((0)),
	[DTaxRate] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_DTaxRate]  DEFAULT ((0)),
        [DTax] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_DTax]  DEFAULT ((0)),
	[EditFlag] [char](1) NULL CONSTRAINT [DF_Suspnded_EditFlag]  DEFAULT (('N')),
	[Notes] [nvarchar](200) NULL CONSTRAINT [DF_Suspnded_Notes]  DEFAULT (('')),
	[QtyDecimal] [int] NULL CONSTRAINT [DF_Suspnded_QtyDecimal]  DEFAULT ((0)),
	[PromptPrice] [char](1) NULL CONSTRAINT [DF_Suspnded_PromptPrice]  DEFAULT (('N')),
	[BuyNGetFreeHeaderID] [int] NULL CONSTRAINT [DF_Suspnded_BuyNGetFreeHeaderID]  DEFAULT ((0)),
	[BuyNGetFreeCategory] [char](1) NULL CONSTRAINT [DF_Suspnded_BuyNGetFreeCategory]  DEFAULT (('X')),
	[BuyNGetFreeName] [nvarchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Age] [int] NULL CONSTRAINT [DF_Suspnded_Age]  DEFAULT ((0)),
	[TaxIncludeRate] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxIncludeRate]  DEFAULT ((0)),
	[TaxIncludePrice] [numeric](15, 3) NULL CONSTRAINT [DF_Suspnded_TaxIncludePrice]  DEFAULT ((0)),
[UOM] [nvarchar](15) NULL DEFAULT (('')),
 CONSTRAINT [PK_Suspnded] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Tender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionNo] [int] NULL CONSTRAINT [DF_Tender_TransactionNo]  DEFAULT ((0)),
	[TenderType] [smallint] NULL CONSTRAINT [DF_Tender_TenderType]  DEFAULT ((0)),
	[TenderAmount] [numeric](15, 3) NULL CONSTRAINT [DF_Tender_TenderAmount]  DEFAULT ((0)),
	[CCNumber] [nvarchar](18) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CCApproval] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CCReference] [nvarchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [int] NULL CONSTRAINT [DF_Tender_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Tender_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
 CONSTRAINT [PK_Tender] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Tender]') AND name = N'IX_Tender_TenderAmount')
DROP INDEX [IX_Tender_TenderAmount] ON [dbo].[Tender] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Tender_TenderAmount] ON [dbo].[Tender] 
(
	[TenderAmount] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Tender]') AND name = N'IX_Tender_TenderType')
DROP INDEX [IX_Tender_TenderType] ON [dbo].[Tender] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Tender_TenderType] ON [dbo].[Tender] 
(
	[TenderType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Tender]') AND name = N'IX_Tender_TransactionNo')
DROP INDEX [IX_Tender_TransactionNo] ON [dbo].[Tender] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_Tender_TransactionNo] ON [dbo].[Tender] 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]



CREATE TABLE [dbo].[AcctRecv](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL CONSTRAINT [DF_Acct_Recv_CustomerID]  DEFAULT ((0)),
	[InvoiceNo] [int] NULL CONSTRAINT [DF_Acct_Recv_InvoiceNo]  DEFAULT ((0)),
	[Amount] [numeric](15, 3) NULL CONSTRAINT [DF_Acct_Recv_Amount]  DEFAULT ((0)),
	[TranType] [smallint] NULL CONSTRAINT [DF_Acct_Recv_TranType]  DEFAULT ((0)),
	[Date] [datetime] NULL,
	[CreatedBy] [int] NULL CONSTRAINT [DF_Acct_Recv_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_Acct_Recv_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[IssueStore] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperateStore] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpFlag] [char](1) NULL DEFAULT ('N') ,
 CONSTRAINT [PK_Acct_Recv] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AcctRecv]') AND name = N'IX_AcctRecv_customerid')
DROP INDEX [IX_AcctRecv_customerid] ON [dbo].[AcctRecv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_AcctRecv_customerid] ON [dbo].[AcctRecv] 
(
	[CustomerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AcctRecv]') AND name = N'IX_AcctRecv_Date')
DROP INDEX [IX_AcctRecv_Date] ON [dbo].[AcctRecv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_AcctRecv_Date] ON [dbo].[AcctRecv] 
(
	[Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AcctRecv]') AND name = N'IX_AcctRecv_InvoiceNo')
DROP INDEX [IX_AcctRecv_InvoiceNo] ON [dbo].[AcctRecv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_AcctRecv_InvoiceNo] ON [dbo].[AcctRecv] 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AcctRecv]') AND name = N'IX_AcctRecv_IssueStore')
DROP INDEX [IX_AcctRecv_IssueStore] ON [dbo].[AcctRecv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_AcctRecv_IssueStore] ON [dbo].[AcctRecv] 
(
	[IssueStore] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AcctRecv]') AND name = N'IX_AcctRecv_OperateStore')
DROP INDEX [IX_AcctRecv_OperateStore] ON [dbo].[AcctRecv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_AcctRecv_OperateStore] ON [dbo].[AcctRecv] 
(
	[OperateStore] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AcctRecv]') AND name = N'IX_AcctRecv_TranType')
DROP INDEX [IX_AcctRecv_TranType] ON [dbo].[AcctRecv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_AcctRecv_TranType] ON [dbo].[AcctRecv] 
(
	[TranType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


CREATE TABLE [dbo].[StoreCreditTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefCustomer] [int] NOT NULL,
	[TranType] [varchar](30) NOT NULL,
	[TranDate] [datetime] NOT NULL,
	[TranAmount] [numeric](15, 3) NOT NULL,
	[RefInvoice] [int] NULL CONSTRAINT [DF_StoreCreditTransaction_RefInvoice]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_StoreCreditTransaction_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_StoreCreditTransaction_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[IssueStore] [nvarchar](10) NULL,
	[OperateStore] [nvarchar](10) NULL,
	[ExpFlag] [char](1) NULL,
 CONSTRAINT [PK_StoreCreditLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[CardTrans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL CONSTRAINT [DF_CardTrans_CustomerID]  DEFAULT ((0)),
	[EmployeeID] [int] NULL CONSTRAINT [DF_CardTrans_EmployeeID]  DEFAULT ((0)),
	[CardType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CardAmount] [numeric](15, 3) NULL CONSTRAINT [DF_CardTrans_CardAmount]  DEFAULT ((0)),
	[TransactionNo] [int] NULL CONSTRAINT [DF_CardTrans_TransactionNo]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_CardTrans_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_CardTrans_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[AuthCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Reference] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDebitCard] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL DEFAULT ('N'),
	[PaymentGateway] [int] NULL DEFAULT ((1)),
	[MercuryAcqRef] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryToken] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryInvoiceNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsComplete] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL DEFAULT ('N'),
	[MercuryProcessData] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryTranCode] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryPurchaseAmount] [numeric](15, 3) NULL DEFAULT ((0)),
	[RefCardAct] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RefCardMerchID] [varchar](24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RefCardLogo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RefCardEntry] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RefCardAuthID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RefCardTranID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RefCardAuthAmount] [numeric](15, 3) NULL DEFAULT ((0)),
	[TransactionType] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdjustFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL DEFAULT ('N'),
	[MercuryResponseOrigin] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryRecordNo] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryResponseReturnCode] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MercuryTextResponse] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReturnAmount] [numeric](15, 3) NULL CONSTRAINT [DF_CardTrans_RReturnAmount]  DEFAULT ((0)),
	[RefCardBalance] [numeric](15, 3) NULL CONSTRAINT [DF_CardTrans_RefCardBalance]  DEFAULT ((0)),
	[TerminalName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LogFileName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,

 CONSTRAINT [PK_CardTrans] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CardTrans]') AND name = N'IX_CardTrans_transactionno')
DROP INDEX [IX_CardTrans_transactionno] ON [dbo].[CardTrans] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_CardTrans_transactionno] ON [dbo].[CardTrans] 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


/*
CREATE TABLE [dbo].[StockJournal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocDate] [datetime] NULL,
	[ProductID] [int] NULL CONSTRAINT [DF_StockJournal_ProductID]  DEFAULT ((0)),
	[TranType] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TranSubType] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qty] [numeric](15, 3) NULL CONSTRAINT [DF_StockJournal_Qty]  DEFAULT ((0)),
	[Cost] [numeric](15, 3) NULL CONSTRAINT [DF_StockJournal_Cost]  DEFAULT ((0)),
	[StockOnHand] [numeric](15, 3) NULL CONSTRAINT [DF_StockJournal_StockOnHand]  DEFAULT ((0)),
	[TerminalName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpID] [int] NULL,
	[TranDate] [datetime] NULL,
	[Notes] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]

*/

CREATE TABLE [dbo].[WorkOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [int] NULL,
	[ProductID] [int] NULL,
	[PriceA] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_PriceA]  DEFAULT ((0)),
	[PriceB] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_PriceB]  DEFAULT ((0)),
	[PriceC] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_PriceC]  DEFAULT ((0)),
	[PriceOverride] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_PriceOverride]  DEFAULT ((0)),
	[SalePrice] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_SalePrice]  DEFAULT ((0)),
	[NewPrice] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_NewPrice]  DEFAULT ((0)),
	[Qty] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_Qty]  DEFAULT ((0)),
	[Cost] [numeric](15, 3) NULL,
	[SKU] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DepartmentID] [int] NULL CONSTRAINT [DF_WorkOrder_DepartmentID]  DEFAULT ((0)),
	[CategoryID] [int] NULL CONSTRAINT [DF_WorkOrder_CategoryID]  DEFAULT ((0)),
	[TaxID1] [int] NULL CONSTRAINT [DF_WorkOrder_TaxID1]  DEFAULT ((0)),
	[TaxID2] [int] NULL CONSTRAINT [DF_WorkOrder_TaxID2]  DEFAULT ((0)),
	[TaxID3] [int] NULL CONSTRAINT [DF_WorkOrder_TaxID3]  DEFAULT ((0)),
	[Taxable1] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_Taxable1]  DEFAULT ('N'),
	[Taxable2] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_Taxable2]  DEFAULT ('N'),
	[Taxable3] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_Taxable3]  DEFAULT ('N'),
	[TaxRate1] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxRate1]  DEFAULT ((0)),
	[TaxRate2] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxRate2]  DEFAULT ((0)),
	[TaxRate3] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxRate3]  DEFAULT ((0)),
	[UOMCount] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_UOMCount]  DEFAULT ((0)),
	[UOMPrice] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_UOMPrice]  DEFAULT ((0)),
	[UOMDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MatrixOptionID] [int] NULL CONSTRAINT [DF_WorkOrder_MatrixOptionID]  DEFAULT ((0)),
	[OptionValue1] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue3] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CustomerID] [int] NULL CONSTRAINT [DF_WorkOrder_CustomerID]  DEFAULT ((0)),
	[DiscountLevel] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentDate] [datetime] NULL,
	[OnSale] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_OnSale]  DEFAULT ('N'),
	[FSPaid] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_FSPaid]  DEFAULT ((0)),
	[MinimumAge] [smallint] NULL CONSTRAINT [DF_WorkOrder_MinimumAge]  DEFAULT ((0)),
	[ReturnedItemID] [int] NULL,
	[CloseoutID] [int] NULL CONSTRAINT [DF_WorkOrder_CloseoutID]  DEFAULT ((0)),
	[CreatedBy] [int] NULL CONSTRAINT [DF_WorkOrder_CreatedBy]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL CONSTRAINT [DF_WorkOrder_LastChangedBy]  DEFAULT ((0)),
	[LastChangedOn] [datetime] NULL,
	[WorkOrderNo] [int] NULL DEFAULT ((0)),
	[TotalAmount] [numeric](15, 3) NULL DEFAULT ((0)),
	[TaxAmount] [numeric](15, 3) NULL DEFAULT ((0)),
	[ItemIndex] [int] NULL CONSTRAINT [DF_WorkOrder_ItemIndex]  DEFAULT ((1)),
	[DiscountID] [int] NULL CONSTRAINT [DF_WorkOrder_DiscountID]  DEFAULT ((0)),
	[Discount] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_Discount]  DEFAULT ((0)),
	[DiscValue] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_DiscValue]  DEFAULT ((0)),
	[DiscountText] [nvarchar](60) NULL CONSTRAINT [DF_WorkOrder_DiscountText]  DEFAULT (''),
	[DiscLogic] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_DiscLogic]  DEFAULT (''),
	[BreakpackFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_BreakpackFlag]  DEFAULT ('Y'),
              	[TaxType1] [int] NULL CONSTRAINT [DF_WorkOrder_TaxType1]  DEFAULT ((0)),
	[TaxType2] [int] NULL CONSTRAINT [DF_WorkOrder_TaxType2]  DEFAULT ((0)),
	[TaxType3] [int] NULL CONSTRAINT [DF_WorkOrder_TaxType3]  DEFAULT ((0)),

	[TaxTotal1] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxTotal11]  DEFAULT ((0)),
	[TaxTotal2] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxTotal12]  DEFAULT ((0)),
	[TaxTotal3] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxTotal13]  DEFAULT ((0)),
	[MixMatchID] [int] NULL CONSTRAINT [DF_WorkOrder_MixMatchID]  DEFAULT ((0)),
    	[MixMatchType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_MixMatchType]  DEFAULT (''),
	[MixMatchFlag] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_MixMatchFlag]  DEFAULT ('N'),
        	[MixMatchValue] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_MixMatchValue]  DEFAULT ((0)),
        	[MixMatchUnique] [int] NULL CONSTRAINT [DF_WorkOrder_MixMatchUnique]  DEFAULT ((0)),
	[MixMatchQty] [int] NULL CONSTRAINT [DF_WorkOrder_MixMatchQty]  DEFAULT ((0)),
	[MixMatchLast] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WorkOrder_MixMatchLast]  DEFAULT ('N'),


    	[FeesID] [int] NULL CONSTRAINT [DF_WorkOrder_FeesID]  DEFAULT ((0)),
	[FeesLogic] [char](1) NULL CONSTRAINT [DF_WorkOrder_FeesLogic]  DEFAULT (('')),
        	[FeesValue] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_FeesValue]  DEFAULT ((0)),
        	[FeesTaxRate] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_FeesTaxRate]  DEFAULT ((0)),
        	[Fees] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_Fees]  DEFAULT ((0)),
        	[FeesTax] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_FeesTax]  DEFAULT ((0)), 
        	[FeesText] [nvarchar](60) NULL CONSTRAINT [DF_WorkOrder_FeesText]  DEFAULT (('')),
	[FeesQty] [char](1) NULL CONSTRAINT [DF_WorkOrder_FeesQty]  DEFAULT (('N')),

	[SalePriceID] [int] NULL CONSTRAINT [DF_WorkOrder_SalePriceID]  DEFAULT ((0)),

	[DTaxID] [int] NULL CONSTRAINT [DF_WorkOrder_DTaxID]  DEFAULT ((0)),
        	[DTaxType] [int] NULL CONSTRAINT [DF_WorkOrder_DTaxType]  DEFAULT ((0)),
	[DTaxRate] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_DTaxRate]  DEFAULT ((0)),
        	[DTax] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_DTax]  DEFAULT ((0)),
	[EditFlag] [char](1) NULL CONSTRAINT [DF_WorkOrder_EditFlag]  DEFAULT (('N')),
	[PromptPrice] [char](1) NULL CONSTRAINT [DF_WorkOrder_PromptPrice]  DEFAULT (('N')),
	[BuyNGetFreeHeaderID] [int] NULL CONSTRAINT [DF_WorkOrder_BuyNGetFreeHeaderID]  DEFAULT ((0)),
	[BuyNGetFreeCategory] [char](1) NULL CONSTRAINT [DF_WorkOrder_BuyNGetFreeCategory]  DEFAULT (('X')),
	[BuyNGetFreeName] [nvarchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Age] [int] NULL CONSTRAINT [DF_WorkOrder_Age]  DEFAULT ((0)),
	[TaxIncludeRate] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxIncludeRate]  DEFAULT ((0)),
	[TaxIncludePrice] [numeric](15, 3) NULL CONSTRAINT [DF_WorkOrder_TaxIncludePrice]  DEFAULT ((0)),
[UOM] [nvarchar](15) NULL DEFAULT (('')),
 CONSTRAINT [PK_WorkOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[VoidInv](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [int] NOT NULL,
	[VoidBy] [int] NOT NULL,
	[VoidOn] [datetime] NULL,
 CONSTRAINT [PK_VoidInv] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VoidInv]') AND name = N'IX_VoidInv_InvoiceNo')
DROP INDEX [IX_VoidInv_InvoiceNo] ON [dbo].[VoidInv] WITH ( ONLINE = OFF )

CREATE NONCLUSTERED INDEX [IX_VoidInv_InvoiceNo] ON [dbo].[VoidInv] 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


 CREATE TABLE [dbo].[CardAuthorisation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [varchar](50) NULL,
	[SaleTranNo] [varchar](50) NULL,
	[CancelTranNo] [varchar](50) NULL,
	[InvoiceNo] [int] NULL,
	[InvoiceAmount] [numeric](15, 3) NULL,
	[SaleOn] [datetime] NULL,
	[SaleBy] [int] NULL,
	[CancelOn] [datetime] NULL,
	[CancelBy] [int] NULL,
	[AuthorisedOn] [datetime] NULL,
	[AuthorisedBy] [int] NULL,
	[CompleteOn] [datetime] NULL,
	[CompleteBy] [int] NULL,
	[BatchFlag] [char](1) NULL default 'N',
	[TipAmount] [numeric](15,3) default 0,
	[AuthorisedTranNo] [varchar](50) NULL,
	[CompleteTranNo] [varchar](50) NULL,
 CONSTRAINT [PK_CardAuthorisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

 Update Customer Set StoreCredit=0, TotalPurchases=0, DateLastPurchase=NULL, AmountLastPurchase=0 ;
 Update Employee Set TotalSales=0;
 Update product set QtyOnLayaway = 0;
 Update product set QtyOnHand = 0;
 Update product set expflag = 'N';
 Update matrix Set QtyOnHand=0;

 delete from stockjournal where transubtype <> 'Opening Stock';

    declare sc1 cursor
    for select p.ID, p.producttype, j.qty from stockjournal j left outer join product p on j.productid = p.ID 

    open sc1
    fetch next from sc1 into @pid,@ptype,@qty
    while @@fetch_status = 0 begin

      if @ptype = 'M' delete from stockjournal where productID = @pid
      
      if @ptype <> 'M' Update product Set QtyOnHand=@qty,expflag='N' where id =  @pid

      fetch next from sc1 into @pid,@ptype,@qty
    end
    close sc1
    deallocate sc1 	

 set @ReturnID = 0; 
 return 0;

end

GO

DROP PROCEDURE IF EXISTS [dbo].[sp_co_imp_updttag_closeout]
GO

create procedure [dbo].[sp_co_imp_updttag_closeout]
as

begin

  update CentralExportCloseOut set ExpFlag = 'Y' where expflag = 'N';

  delete from CentralExportCloseOutReturn;
  delete from CentralExportCloseOutReportMain;
  delete from CentralExportCloseOutReportTender;
  delete from CentralExportCloseOutSalesDept;
  delete from CentralExportCloseOutSalesHour;


end
GO

DROP PROCEDURE IF EXISTS [dbo].[sp_co_imp_updttag_po]
GO
Create procedure [dbo].[sp_co_imp_updttag_po]
as

begin

  update POHeader set ExpFlag = 'Y' where expflag = 'N';

end
GO

DROP PROCEDURE IF EXISTS [dbo].[sp_co_imp_updttag_recv]
GO
Create procedure [dbo].[sp_co_imp_updttag_recv]
as

begin

  update RecvHeader set ExpFlag = 'Y' where expflag = 'N';

end
GO

