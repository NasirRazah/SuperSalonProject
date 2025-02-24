ALTER procedure [dbo].[sp_CloseoutSalesByHour]
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
