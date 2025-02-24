ALTER procedure [dbo].[sp_CloseoutReportHeader]
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
