ALTER procedure [dbo].[sp_CloseoutReportTender]
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