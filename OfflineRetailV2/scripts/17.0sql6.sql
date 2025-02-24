ALTER procedure [dbo].[sp_CloseoutSalesByDept]
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

