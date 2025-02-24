USE [Retail2020DB]
GO

ALTER procedure [dbo].[sp_salessummary]
					@tax_inclusive	char(1),
					@f_date		datetime,
					@t_date		datetime,
					@terminal		nvarchar(50)
as
declare @TranID			int;
declare @TranDate		datetime;
declare @InvID			int;
declare @Inv_DTaxID		int;
declare @Inv_LayawayNo	int;
declare @Inv_Tax		numeric(15,3);
declare @Inv_Tax1		numeric(15,3);
declare @Inv_Tax2		numeric(15,3);
declare @Inv_Tax3		numeric(15,3);
declare @Inv_Discount	numeric(15,3);
declare @Inv_TotalSale	numeric(15,3);
declare @Inv_Coupon		numeric(15,3);
declare @Inv_CouponPerc	numeric(15,3);
declare @Inv_Fees		numeric(15,3);
declare @Inv_FeesTax	numeric(15,3);
declare @Inv_FeesCoupon		numeric(15,3);
declare @Inv_FeesCouponTax		numeric(15,3);
declare @Inv_DTax			numeric(15,3);
declare @Inv_RentDeposit		numeric(15,3);
declare @Inv_IsRentCalculated	char(1);
declare @Inv_Status			int;
declare @item_ProductID		int;
declare @item_SKU			nvarchar(16);
declare @item_Description	nvarchar(200);
declare @item_ProductType	char(1);
declare @item_Price			numeric(15,3);
declare @item_NormalPrice	numeric(15,3);
declare @item_Qty			numeric(15,3);
declare @item_Cost			numeric(15,3);
declare @item_Taxable1		char(1);
declare @item_Taxable2		char(1);
declare @item_Taxable3		char(1);
declare @item_TaxRate1		numeric(15,3);
declare @item_TaxRate2		numeric(15,3);
declare @item_TaxRate3		numeric(15,3);
declare @item_TaxType1		int;
declare @item_TaxType2		int;
declare @item_TaxType3		int;
declare @item_TaxTotal1		numeric(15,3);
declare @item_TaxTotal2		numeric(15,3);
declare @item_TaxTotal3		numeric(15,3);
declare @item_Discount		numeric(15,3);
declare @item_TaxIncludeRate		numeric(15,3);
declare @item_TaxIncludePrice		numeric(15,3);

declare @item_FSTender		char(1);
declare @item_RentDuration	numeric(15,3);
declare @Report_Sales			numeric(15,3);
declare @Report_SalesTx1		numeric(15,3);
declare @Report_SalesTx2		numeric(15,3);
declare @Report_SalesTx3		numeric(15,3);
declare @Report_TicketDiscount	numeric(15,3);
declare @Report_RentDiscount	numeric(15,3);
declare @Report_ItemDiscount	numeric(15,3);
declare @Report_ProductDiscount	numeric(15,3);
declare @Report_ServiceDiscount	numeric(15,3);
declare @Report_OtherDiscount	numeric(15,3);
declare @Report_RepairDiscount	numeric(15,3);
declare @Report_GCSales			numeric(15,3);
declare @Report_MercGCSales		numeric(15,3);
declare @Report_PreGCSales		numeric(15,3);
declare @Report_DatacapGCSales		numeric(15,3);
declare @Report_POSLinkGCSales		numeric(15,3);
declare @Report_ServiceSales	numeric(15,3);
declare @Report_ProductSales	numeric(15,3);
declare @Report_BlankItemSales	numeric(15,3);
declare @Report_RentSales		numeric(15,3);
declare @Report_RepairSales		numeric(15,3);
declare @Report_CostOfGoods		numeric(15,3);
declare @Report_BottleRefund	numeric(15,3);
declare	@Report_MixMatchDiscount 	numeric(15,3);
declare @Report_TaxableSales1	numeric(15,3);
declare @Report_TaxableSales2	numeric(15,3);
declare @Report_TaxableSales3	numeric(15,3);
declare @Report_NonTaxableSales 	numeric(15,3);
declare @Report_LayawaySalesPosted	numeric(15,3);
declare @Report_FoodStampTendered	numeric(15,3);
declare @Report_DTax		numeric(15,3);
declare @Report_Fees		numeric(15,3);
declare @Report_SaleFees		numeric(15,3);
declare @Report_SaleFeesTax	numeric(15,3);
declare @Report_RentFees		numeric(15,3);
declare @Report_RentFeesTax	numeric(15,3);
declare @Report_RepairFees		numeric(15,3);
declare @Report_RepairFeesTax	numeric(15,3);
declare @Report_RepairDepost	numeric(15,3);
declare @Report_RepairTicketDiscount 	numeric(15,3);
declare @Repair_Parent			int;
declare @Temp_Tax1				numeric(15,3);
declare @Temp_Tax2				numeric(15,3);
declare @Temp_Tax3				numeric(15,3);
declare @Temp_Amt				numeric(15,3);
declare @Temp_Product			numeric(15,3);
declare @t_cnt					int;
declare @v_cnt					int;
declare @i_cnt					int;
declare @cpn_perc				numeric(15,3);
declare @Tx_1					numeric(15,3);
declare @Tx_2					numeric(15,3);
declare @Tx_3					numeric(15,3);
declare @PTax1					numeric(15,3);
declare @PTax2					numeric(15,3);
declare @PTax3					numeric(15,3);
declare @SaleTax1				numeric(15,3);
declare @SaleTax2				numeric(15,3);
declare @SaleTax3				numeric(15,3);
declare @STax1					numeric(15,3);
declare @STax2					numeric(15,3);
declare @STax3					numeric(15,3);
declare @BTax1					numeric(15,3);
declare @BTax2					numeric(15,3);
declare @BTax3					numeric(15,3);
declare @TaxName1				nvarchar(50);
declare @TaxName2				nvarchar(50);
declare @TaxName3				nvarchar(50);
declare @Tax1_Text				nvarchar(50);
declare @Tax2_Text				nvarchar(50);
declare @Tax3_Text				nvarchar(50);
declare @Tx_Name				nvarchar(20);
declare @Tx_Rate				numeric(15,3);
declare @TaxNameRent1			nvarchar(50);
declare @TaxNameRent2			nvarchar(50);
declare @TaxNameRent3			nvarchar(50);
declare @TaxRent1_Text			nvarchar(50);
declare @TaxRent2_Text			nvarchar(50);
declare @TaxRent3_Text			nvarchar(50);
declare @TxRent_Name			nvarchar(20);
declare @TxRent_Rate			numeric(15,3);
declare @TxRepair_Name			nvarchar(20);
declare @TxRepair_Rate			numeric(15,3);
declare @TaxNameRepair1			nvarchar(50);
declare @TaxNameRepair2			nvarchar(50);
declare @TaxNameRepair3			nvarchar(50);
declare @Inv_TaxID1				int;
declare @Inv_TaxID2				int;
declare @Inv_TaxID3				int;
declare @RnTax1					numeric(15,3);
declare @RnTax2					numeric(15,3);
declare @RnTax3					numeric(15,3);
declare @RpTax1					numeric(15,3);
declare @RpTax2					numeric(15,3);
declare @RpTax3					numeric(15,3);
declare @RentExist				int;
declare @RepairExist			int;
declare @TempCount				int;
declare @TempCount1				int;

declare @Report_FreeQty			int;
declare @Report_FreeAmount		numeric(15,3);
declare @BuyNGetFree_Tag		char(1);

declare @Paidout		numeric(15,3);
declare @LottoPayout		numeric(15,3);

declare @Report_GiftAid			numeric(15,3);

begin

  delete from SummaryData where TerminalName = @terminal;

  set @SaleTax1 = 0;
  set @SaleTax2 = 0;
  set @SaleTax3 = 0;

  set @Report_GiftAid = 0;
  set @Report_DTax = 0;
  set @TaxNameRepair1 = '';
  set @TaxNameRepair2 = '';
  set @TaxNameRepair3 = '';

  set @RentExist = 0;
  set @RepairExist = 0;
  set @TempCount = 0;
  set @TempCount1 = 0;

  set @RpTax1 = 0;
  set @RpTax2 = 0;
  set @RpTax3 = 0;

  set @RnTax1 = 0;
  set @RnTax2 = 0;
  set @RnTax3 = 0;

  set @TaxRent1_Text = '';
  set @TaxRent2_Text = '';
  set @TaxRent3_Text = '';

  set @Tax1_Text = '';
  set @Tax2_Text = '';
  set @Tax3_Text = '';

  set @PTax1 = 0;
  set @PTax2 = 0;
  set @PTax3 = 0;

  set @STax1 = 0;
  set @STax2 = 0;
  set @STax3 = 0;

  set @BTax1 = 0;
  set @BTax2 = 0;
  set @BTax3 = 0;

  set @t_cnt = 0;  
  set @v_cnt = 0; 
  set @i_cnt = 0; 

  set @cpn_perc = 0;

  set @Report_Sales = 0;
  set @Report_SalesTx1 = 0;
  set @Report_SalesTx2 = 0;
  set @Report_SalesTx3 = 0;
  set @Report_TicketDiscount = 0;
  set @Report_RentDiscount = 0;
  set @Report_RepairTicketDiscount = 0;
  set @Report_ItemDiscount = 0;
  set @Report_GCSales = 0;
  set @Report_MercGCSales = 0;
  set @Report_PreGCSales = 0;
  set @Report_DatacapGCSales = 0;
  set @Report_POSLinkGCSales = 0;
  set @Report_ServiceSales = 0;
  set @Report_ProductSales = 0;
  set @Report_BlankItemSales = 0;
  set @Report_RentSales = 0;
  set @Report_RepairSales = 0;
  set @Report_CostOfGoods = 0;
  set @Report_BottleRefund = 0;
  set @Report_TaxableSales1 = 0;
  set @Report_TaxableSales2 = 0;
  set @Report_TaxableSales3 = 0;
  set @Report_NonTaxableSales = 0;
  set @Report_LayawaySalesPosted = 0;
  set @Report_FoodStampTendered = 0;
  set @Report_Fees = 0;
  set @Report_SaleFees = 0;
  set @Report_SaleFeesTax = 0;
  set @Report_RentFees = 0;
  set @Report_RentFeesTax = 0;
  set @Report_RepairFees = 0;
  set @Report_RepairFeesTax = 0;

  set @Report_ProductDiscount = 0;
  set @Report_ServiceDiscount = 0;
  set @Report_OtherDiscount	= 0;
  set @Report_RepairDiscount = 0;

  set @Report_RepairDepost = 0;
  set @Report_MixMatchDiscount = 0;
  set @Temp_Tax1 = 0;
  set @Temp_Tax2 = 0;
  set @Temp_Tax3 = 0;

  set @Temp_Product = 0;

  set @TaxName1 = '';
  set @TaxName2 = '';
  set @TaxName3 = '';

  set @TaxNameRent1 = '';
  set @TaxNameRent2 = '';
  set @TaxNameRent3 = '';

  set @Report_RentSales = 0;
  set @Report_RepairSales = 0;

  set @Report_FreeQty = 0;
  set @Report_FreeAmount = 0;

  set @Paidout = 0;
  set @LottoPayout = 0;


  declare sc_s0 cursor
  for select ID, TransDate from trans where TransDate between @f_date and @t_date
  open sc_s0
  fetch next from sc_s0 into @TranID, @TranDate
  while @@FETCH_STATUS = 0 begin



    declare sc_s1 cursor
    for select ID, Tax,Tax1,Tax2,Tax3,Discount,TotalSale,LayawayNo,Coupon,CouponPerc,Fees,FeesTax,
	DTaxID,DTax,TaxID1,TaxID2,TaxID3,FeesCoupon,FeesCouponTax from invoice
	where TransactionNo = @TranID and Status = 3
    open sc_s1
    fetch next from sc_s1 into @InvID, @Inv_Tax, @Inv_Tax1, @Inv_Tax2, @Inv_Tax3, @Inv_Discount, @Inv_TotalSale, 
	@Inv_LayawayNo,@Inv_Coupon, @Inv_CouponPerc, @Inv_Fees, @Inv_FeesTax, @Inv_DTaxID, @Inv_DTax,@Inv_TaxID1,
	@Inv_TaxID2,@Inv_TaxID3,@Inv_FeesCoupon, @Inv_FeesCouponTax
    while @@FETCH_STATUS = 0 begin

	  

	  if @Inv_TaxID1 > 0 begin
	    set @Tx_Name = '';
	    set @Tx_Rate = 0;
	    select @Tx_Name = isnull(TaxName,''), @Tx_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID1;
	    if @TaxName1 = '' and @Tx_Name <> ''
	      set @TaxName1 = @Tx_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@Tx_Rate)) + ' %';
      end;

	  if @Inv_TaxID2 > 0 begin
        set @Tx_Name = '';
	    set @Tx_Rate = 0;

	    select @Tx_Name = isnull(TaxName,''), @Tx_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID2;
	    if @TaxName2 = '' and @Tx_Name <> ''
	      set @TaxName2 = @Tx_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@Tx_Rate)) + ' %';
       end

	   if @Inv_TaxID3 > 0 begin
	    set @Tx_Name = '';
	    set @Tx_Rate = 0;

	    select @Tx_Name = isnull(TaxName,''), @Tx_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID3;
	    if @TaxName3 = '' and @Tx_Name <> ''
	      set @TaxName3 = @Tx_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@Tx_Rate)) + ' %';
	   end

	  set @Temp_Tax1 = @Temp_Tax1 + @Inv_Tax1;
	  set @Temp_Tax2 = @Temp_Tax2 + @Inv_Tax2;
	  set @Temp_Tax3 = @Temp_Tax3 + @Inv_Tax3;

	  set @Report_TicketDiscount = @Report_TicketDiscount + @Inv_Coupon;

	  set @Report_SaleFees = @Report_SaleFees + @Inv_Fees + @Inv_FeesCoupon;
	  set @Report_SaleFeesTax = @Report_SaleFeesTax + @Inv_FeesTax + @Inv_FeesCouponTax;

	  set @Temp_Amt = 0;
	  select @Temp_Amt = isnull(sum(TenderAmount),0) from tender where TransactionNo = @TranID 
      and tendertype in (select id from tendertypes where name = 'Food Stamps') ;

	  if @Temp_Amt > 0
	    set @Report_FoodStampTendered = @Report_FoodStampTendered + @Temp_Amt;

	  if @Inv_LayawayNo > 0 begin
	    set @Temp_Amt = 0;
        select @Temp_Amt = payment from laypmts where TransactionNo = @TranID and InvoiceNo = @InvID;
		set @Report_LayawaySalesPosted = @Report_LayawaySalesPosted + @Temp_Amt;
      end

	  if @Inv_DTaxID > 0 set @Report_DTax = @Report_DTax + @Inv_DTax;

	  

	  declare sc_s2 cursor
      for select ProductID, SKU, [Description], ProductType, Price, NormalPrice, Qty, Cost, Taxable1, Taxable2,
	  Taxable3, TaxRate1, TaxRate2, TaxRate3, TaxType1, TaxType2, TaxType3, TaxTotal1, TaxTotal2, TaxTotal3, Discount, FSTender, BuyNGetFreeCategory,
	  TaxIncludeRate, TaxIncludePrice from item
	  where InvoiceNo = @InvID and Tagged <> 'X' and ProductType not in  ('C','Z','H','A')
      open sc_s2
      fetch next from sc_s2 into @item_ProductID, @item_SKU, @item_Description, @item_ProductType, @item_Price, @item_NormalPrice,
	  @item_Qty, @item_Cost, @item_Taxable1, @item_Taxable2,@item_Taxable3, @item_TaxRate1, @item_TaxRate2, @item_TaxRate3, 
	  @item_TaxType1, @item_TaxType2, @item_TaxType3, @item_TaxTotal1, @item_TaxTotal2, @item_TaxTotal3, @item_Discount, @item_FSTender, @BuyNGetFree_Tag,
	  @item_TaxIncludeRate,@item_TaxIncludePrice

      while @@FETCH_STATUS = 0 begin
	    
		set @Tx_1 = 0;
		set @Tx_2 = 0;
		set @Tx_3 = 0;

		set @cpn_perc = (100 - @Inv_CouponPerc)/100;

		if ((@item_Taxable1 = 'Y' or @item_Taxable2 = 'Y' or @item_Taxable3 = 'Y') and (@item_FSTender = 'N')) begin

		  if @item_Taxable1 = 'Y' begin
		    if @item_TaxType1 = 0 begin
			  set @Tx_1 = round(((@item_Price * @item_Qty * @item_TaxRate1 / 100) * @cpn_perc),2);
			end
			if @item_TaxType1 = 1 
			  set @Tx_1 = round((@item_TaxTotal1 * @cpn_perc),2);
		  end

		  if @item_Taxable2 = 'Y' begin
		    if @item_TaxType2 = 0 
			  set @Tx_2 = round(((@item_Price * @item_Qty * @item_TaxRate2 / 100) * @cpn_perc),2);
			if @item_TaxType2 = 1 
			  set @Tx_2 = round((@item_TaxTotal2 * @cpn_perc),2);
		  end

		  if @item_Taxable3 = 'Y' begin
		    if @item_TaxType3 = 0 
			  set @Tx_3 = round(((@item_Price * @item_Qty * @item_TaxRate3 / 100) * @cpn_perc),2);
			if @item_TaxType3 = 1 
			  set @Tx_3 = round((@item_TaxTotal3 * @cpn_perc),2);
		  end

		end

		if @item_ProductType = 'G' begin
		  set @Report_GCSales = @Report_GCSales + @item_Price;
		end
		else if @item_ProductType = 'I' begin
		  set @Report_GiftAid = @Report_GiftAid + @item_Price;
		end

	    else if @item_ProductType = 'O' begin
		  if @tax_inclusive = 'N' begin
		    set @Report_BottleRefund = @Report_BottleRefund - round((@item_Qty * @item_Price),2);
		  end
		  if @tax_inclusive = 'Y' begin
		    set @Report_BottleRefund = @Report_BottleRefund - round((@item_Qty * @item_TaxIncludeRate),2);
		  end
		end
		else if @item_ProductType = 'X' begin
		  if @item_ProductID = 111 and @item_SKU = 'MGC' 
		    set @Report_MercGCSales = @Report_MercGCSales + @item_Price;
		  if @item_ProductID = 111 and @item_SKU = 'PGC' 
		    set @Report_PreGCSales = @Report_PreGCSales + @item_Price;
                                   if @item_ProductID = 111 and @item_SKU = 'DGC' 
		    set @Report_DatacapGCSales = @Report_DatacapGCSales + @item_Price;
                                   if @item_ProductID = 111 and @item_SKU = 'PLGC' 
		    set @Report_POSLinkGCSales = @Report_POSLinkGCSales + @item_Price;
		end
		else if @item_ProductType = 'S' begin

		  if @tax_inclusive = 'N' begin
		    set @Report_ServiceSales = @Report_ServiceSales + round(((@item_Qty * @item_Price) - @item_Discount),2);
		  end

		  if @tax_inclusive = 'Y' begin
		    set @Report_ServiceSales = @Report_ServiceSales + round((@item_Qty * @item_TaxIncludeRate),2);
		  end

		  /*
		  if @tax_inclusive = 'Y' begin
		     set @Report_ServiceSales = @Report_ServiceSales - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		  end */

		  set @STax1 = @STax1 + @Tx_1;
		  set @STax2 = @STax2 + @Tx_2;
		  set @STax3 = @STax3 + @Tx_3;

		  set @Report_ServiceDiscount = @Report_ServiceDiscount + @item_Discount;


		end
		else if @item_ProductType = 'B' begin
		  if @tax_inclusive = 'N' begin
		    set @Report_BlankItemSales = @Report_BlankItemSales + round(((@item_Qty * @item_Price) - @item_Discount),2);
		  end

		  if @tax_inclusive = 'Y' begin
		    set @Report_BlankItemSales = @Report_BlankItemSales + round((@item_Qty * @item_TaxIncludeRate),2);
		  end
		  

		  set @BTax1 = @BTax1 + @Tx_1;
		  set @BTax2 = @BTax2 + @Tx_2;
		  set @BTax3 = @BTax3 + @Tx_3;

		  set @Report_OtherDiscount = @Report_OtherDiscount + @item_Discount;

		end
		else if @item_ProductType = 'Z' begin
		  set @Report_MixMatchDiscount = @Report_MixMatchDiscount + @item_Discount;
		end
		else begin
		  if @tax_inclusive = 'N' begin
		   set @Report_ProductSales = @Report_ProductSales + round(((@item_Qty * @item_Price) - @item_Discount),2);
		  end
		  if @tax_inclusive = 'Y' begin
		   set @Report_ProductSales = @Report_ProductSales + round((@item_Qty * @item_TaxIncludeRate),2);
		  end

		  /*
		  if @tax_inclusive = 'Y' begin
		     set @Report_ProductSales = @Report_ProductSales - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		  end  */

		  set @PTax1 = @PTax1 + @Tx_1;
		  set @PTax2 = @PTax2 + @Tx_2;
		  set @PTax3 = @PTax3 + @Tx_3;
		  set @Report_ProductDiscount = @Report_ProductDiscount + @item_Discount;
		  set @Report_CostOfGoods = @Report_CostOfGoods + @item_Cost;
		end

		if @item_ProductType <> 'G' and @item_ProductType <> 'X' and @item_ProductType <> 'O' and @item_ProductType <> 'Z' and @item_ProductType <> 'H' and @item_ProductType <> 'A' begin
		  set @Report_ItemDiscount = @Report_ItemDiscount + @item_Discount;
		end

		if ((@item_Taxable1 = 'N' and @item_Taxable2 = 'N' and @item_Taxable3 = 'N') or (@item_FSTender = 'Y')) begin
		  set @Report_NonTaxableSales = @Report_NonTaxableSales + round(((@item_Qty * @item_Price) - @item_Discount),2);
		end 
		else begin
		  
		   

		  if @item_Taxable1 = 'Y' begin
		    if @tax_inclusive = 'N' begin
		      set @Report_TaxableSales1 = @Report_TaxableSales1 + round(((@item_Price * @item_Qty) - @item_Discount),2);
			end
			if @tax_inclusive = 'Y' begin
		      set @Report_TaxableSales1 = @Report_TaxableSales1 + round((@item_Price * @item_Qty),2);
			end
			/*
			if @tax_inclusive = 'Y' begin
		     set @Report_TaxableSales1 = @Report_TaxableSales1 - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		    end */
		  end
		  if @item_Taxable2 = 'Y' begin
		    if @tax_inclusive = 'N' begin
		      set @Report_TaxableSales2 = @Report_TaxableSales2 + round(((@item_Price * @item_Qty) - @item_Discount),2);
			end
			if @tax_inclusive = 'Y' begin
		      set @Report_TaxableSales2 = @Report_TaxableSales2 + round((@item_Price * @item_Qty),2);
			end
			/*if @tax_inclusive = 'Y' begin
		     set @Report_TaxableSales2 = @Report_TaxableSales2 - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		    end*/
		  end
		  if @item_Taxable3 = 'Y' begin
		    if @tax_inclusive = 'N' begin
		       set @Report_TaxableSales3 = @Report_TaxableSales3 + round(((@item_Price * @item_Qty) - @item_Discount),2);
			end
			if @tax_inclusive = 'Y' begin
		       set @Report_TaxableSales3 = @Report_TaxableSales3 + round((@item_Price * @item_Qty),2);
			end
			/*if @tax_inclusive = 'Y' begin
		     set @Report_TaxableSales3 = @Report_TaxableSales3 - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		    end*/
		  end
		end

		if @BuyNGetFree_Tag = 'F' begin
		  set @Report_FreeQty = @Report_FreeQty + @item_Qty;
		  set @Report_FreeAmount = @Report_FreeAmount + round((@item_Qty * @item_NormalPrice),2);
		end

        fetch next from sc_s2 into @item_ProductID, @item_SKU, @item_Description, @item_ProductType, @item_Price, @item_NormalPrice,
	    @item_Qty, @item_Cost, @item_Taxable1, @item_Taxable2,@item_Taxable3, @item_TaxRate1, @item_TaxRate2, @item_TaxRate3, 
	    @item_TaxType1, @item_TaxType2, @item_TaxType3, @item_TaxTotal1, @item_TaxTotal2, @item_TaxTotal3, @item_Discount, @item_FSTender, @BuyNGetFree_Tag,
		@item_TaxIncludeRate,@item_TaxIncludePrice
      end
      close sc_s2
      deallocate sc_s2


      fetch next from sc_s1 into @InvID, @Inv_Tax, @Inv_Tax1, @Inv_Tax2, @Inv_Tax3, @Inv_Discount, @Inv_TotalSale, 
	  @Inv_LayawayNo,@Inv_Coupon, @Inv_CouponPerc, @Inv_Fees, @Inv_FeesTax, @Inv_DTaxID, @Inv_DTax,@Inv_TaxID1,
	  @Inv_TaxID2,@Inv_TaxID3,@Inv_FeesCoupon, @Inv_FeesCouponTax
    end
    close sc_s1
    deallocate sc_s1
    
    fetch next from sc_s0 into @TranID, @TranDate
  end
  close sc_s0
  deallocate sc_s0

  if @TaxName1 <> '' set @Tax1_Text = 'Taxable Sales - ' + @TaxName1;
  if @TaxName1 <> '' set @Tax2_Text = 'Taxable Sales - ' + @TaxName2;
  if @TaxName1 <> '' set @Tax3_Text = 'Taxable Sales - ' + @TaxName3;

  set @SaleTax1 = @Temp_Tax1;
  set @SaleTax2 = @Temp_Tax2;
  set @SaleTax3 = @Temp_Tax3;




  select @TempCount = count(inv.ID) from trans t left outer join invoice inv on inv.TransactionNo = t.ID where 
  t.TransDate between @f_date and @t_date and (inv.Status = 15 or inv.Status = 16);

  set @RentExist = @TempCount; 
  



  if @RentExist > 0 begin

  set @Temp_Tax1 = 0;
  set @Temp_Tax2 = 0;
  set @Temp_Tax3 = 0;

  declare sc_r0 cursor
  for select ID, TransDate from trans where TransDate between @f_date and @t_date
  open sc_r0 
  fetch next from sc_r0 into @TranID, @TranDate
  while @@FETCH_STATUS = 0 begin



    declare sc_r1 cursor
    for select ID, Tax,Tax1,Tax2,Tax3,Discount,TotalSale,LayawayNo,Coupon,CouponPerc,Fees,FeesTax,
	DTaxID,DTax,TaxID1,TaxID2,TaxID3,RentDeposit,Status,IsRentCalculated,FeesCoupon,FeesCouponTax from invoice
	where TransactionNo = @TranID and ((Status = 15 and IsRentCalculated = 'N') or (Status = 16 and IsRentCalculated = 'Y'))
    open sc_r1 
    fetch next from sc_r1 into @InvID, @Inv_Tax, @Inv_Tax1, @Inv_Tax2, @Inv_Tax3, @Inv_Discount, @Inv_TotalSale, 
	@Inv_LayawayNo,@Inv_Coupon, @Inv_CouponPerc, @Inv_Fees, @Inv_FeesTax, @Inv_DTaxID, @Inv_DTax,@Inv_TaxID1,
	@Inv_TaxID2,@Inv_TaxID3,@Inv_RentDeposit,@Inv_Status,@Inv_IsRentCalculated,@Inv_FeesCoupon, @Inv_FeesCouponTax
    while @@FETCH_STATUS = 0 begin

	  

	  if @Inv_TaxID1 > 0 begin
	    set @TxRent_Name = '';
	    set @TxRent_Rate = 0;
	    select @TxRent_Name = isnull(TaxName,''), @TxRent_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID1;
	    if @TaxNameRent1 = '' and @TxRent_Name <> ''
	      set @TaxNameRent1 = @TxRent_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@TxRent_Rate)) + ' %';
          end;

	  if @Inv_TaxID2 > 0 begin
            set @TxRent_Name = '';
	    set @TxRent_Rate = 0;

	    select @TxRent_Name = isnull(TaxName,''), @TxRent_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID2;
	    if @TaxNameRent2 = '' and @TxRent_Name <> ''
	      set @TaxNameRent2 = @TxRent_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@TxRent_Rate)) + ' %';
          end

	  if @Inv_TaxID3 > 0 begin
	    set @TxRent_Name = '';
	    set @TxRent_Rate = 0;

	    select @TxRent_Name = isnull(TaxName,''), @TxRent_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID3;
	    if @TaxNameRent3 = '' and @TxRent_Name <> ''
	      set @TaxNameRent3 = @TxRent_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@TxRent_Rate)) + ' %';
	  end

          if (@Inv_Status = 15 and @Inv_IsRentCalculated = 'N') or (@Inv_Status = 16 and @Inv_IsRentCalculated = 'Y')
          begin 
  	     set @Temp_Tax1 = @Temp_Tax1 + @Inv_Tax1;
	     set @Temp_Tax2 = @Temp_Tax2 + @Inv_Tax2;
	     set @Temp_Tax3 = @Temp_Tax3 + @Inv_Tax3;

	     set @Report_RentDiscount = @Report_RentDiscount + @Inv_Coupon;

	     set @Report_RentFees = @Report_RentFees + @Inv_Fees + @Inv_FeesCoupon;
	     set @Report_RentFeesTax = @Report_RentFeesTax + @Inv_FeesTax + @Inv_FeesCouponTax;

	  end

	  

	  declare sc_r2 cursor
      for select ProductType, Price, NormalPrice, Qty, Cost, Taxable1, Taxable2,
	  Taxable3, TaxRate1, TaxRate2, TaxRate3, TaxType1, TaxType2, TaxType3, TaxTotal1, TaxTotal2, TaxTotal3, Discount, FSTender, RentDuration,
	  TaxIncludeRate, TaxIncludePrice from item
	  where InvoiceNo = @InvID and Tagged <> 'X' and ProductType <> 'C' and ProductType <> 'H' and ProductType <> 'A'
      open sc_r2
      fetch next from sc_r2 into @item_ProductType, @item_Price, @item_NormalPrice,
	  @item_Qty, @item_Cost, @item_Taxable1, @item_Taxable2,@item_Taxable3, @item_TaxRate1, @item_TaxRate2, @item_TaxRate3, 
	  @item_TaxType1, @item_TaxType2, @item_TaxType3, @item_TaxTotal1, @item_TaxTotal2, @item_TaxTotal3, @item_Discount,
	  @item_FSTender,@item_RentDuration,@item_TaxIncludeRate, @item_TaxIncludePrice
      while @@FETCH_STATUS = 0 begin
	    
		set @Tx_1 = 0;
		set @Tx_2 = 0;
		set @Tx_3 = 0;

		set @cpn_perc = (100 - @Inv_CouponPerc)/100;

        if (@Inv_Status = 15 and @Inv_IsRentCalculated = 'N') or (@Inv_Status = 16 and @Inv_IsRentCalculated = 'Y') begin

		  if ((@item_Taxable1 = 'Y' or @item_Taxable2 = 'Y' or @item_Taxable3 = 'Y') and (@item_FSTender = 'N')) begin

     		     if @Inv_IsRentCalculated = 'N' begin
		  	if @item_Taxable1 = 'Y' begin
		    		if @item_TaxType1 = 0 
			  		set @Tx_1 = round(((@item_Price * @item_Qty * @item_TaxRate1 / 100) * @cpn_perc),2);
				if @item_TaxType1 = 1 
			  		set @Tx_1 = round((@item_TaxTotal1 * @cpn_perc),2);
		  	end

		  	if @item_Taxable2 = 'Y' begin
		    		if @item_TaxType2 = 0 
			  		set @Tx_2 = round(((@item_Price * @item_Qty * @item_TaxRate2 / 100) * @cpn_perc),2);
				if @item_TaxType2 = 1 
			  		set @Tx_2 = round((@item_TaxTotal2 * @cpn_perc),2);
		  	end

		  	if @item_Taxable3 = 'Y' begin
		    		if @item_TaxType3 = 0 
			  		set @Tx_3 = round(((@item_Price * @item_Qty * @item_TaxRate3 / 100) * @cpn_perc),2);
				if @item_TaxType3 = 1 
			  		set @Tx_3 = round((@item_TaxTotal3 * @cpn_perc),2);
		  	end
                     end
                     if @Inv_IsRentCalculated = 'Y' begin
                        if @Inv_Tax1 > 0 set @Tx_1 = @Inv_Tax1;
                        if @Inv_Tax2 > 0 set @Tx_2 = @Inv_Tax2;
			if @Inv_Tax3 > 0 set @Tx_3 = @Inv_Tax3; 
                     end
                  end

                  if @Inv_IsRentCalculated = 'N' begin

				    if @tax_inclusive = 'N' begin
				      set @Report_RentSales = @Report_RentSales + round(((@item_Qty * @item_Price * @item_RentDuration) - @item_Discount),2);
					end

					if @tax_inclusive = 'Y' begin
				      set @Report_RentSales = @Report_RentSales + round((@item_TaxIncludePrice),2);
					end

		          end
				  if @Inv_IsRentCalculated = 'Y' begin
				    if @tax_inclusive = 'N' begin
				      set @Report_RentSales = @Report_RentSales + round(((-@item_Qty * @item_Price * @item_RentDuration) - @item_Discount),2);
					end
					if @tax_inclusive = 'Y' begin
				      set @Report_RentSales = @Report_RentSales + round((-@item_TaxIncludePrice),2);
					end
					/*if @tax_inclusive = 'Y' begin
		              set @Report_RentSales = @Report_RentSales - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		            end*/
                  end
		end

		
        fetch next from sc_r2 into @item_ProductType, @item_Price, @item_NormalPrice,
	    @item_Qty, @item_Cost, @item_Taxable1, @item_Taxable2,@item_Taxable3, @item_TaxRate1, @item_TaxRate2, @item_TaxRate3, 
	    @item_TaxType1, @item_TaxType2, @item_TaxType3, @item_TaxTotal1, @item_TaxTotal2, @item_TaxTotal3, @item_Discount,
		@item_FSTender,@item_RentDuration,@item_TaxIncludeRate, @item_TaxIncludePrice
      end
      close sc_r2
      deallocate sc_r2


      fetch next from sc_r1 into @InvID, @Inv_Tax, @Inv_Tax1, @Inv_Tax2, @Inv_Tax3, @Inv_Discount, @Inv_TotalSale, 
	  @Inv_LayawayNo,@Inv_Coupon, @Inv_CouponPerc, @Inv_Fees, @Inv_FeesTax, @Inv_DTaxID, @Inv_DTax,@Inv_TaxID1,
	  @Inv_TaxID2,@Inv_TaxID3,@Inv_RentDeposit,@Inv_Status,@Inv_IsRentCalculated,@Inv_FeesCoupon, @Inv_FeesCouponTax
    end
    close sc_r1
    deallocate sc_r1
    
    fetch next from sc_r0 into @TranID, @TranDate
  end
  close sc_r0
  deallocate sc_r0

  set @RnTax1 = @Temp_Tax1;
  set @RnTax2 = @Temp_Tax2;
  set @RnTax3 = @Temp_Tax3;

  end








  
  select @TempCount1 = count(inv.ID) from trans t left outer join invoice inv on inv.TransactionNo = t.ID where 
  t.TransDate between @f_date and @t_date and inv.Status = 18;

  set @RepairExist = @TempCount1; 


  select @Report_RepairDepost = isnull(sum(iv.RepairAdvanceAmount),0) from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo 
  and ((iv.Status = 17 and iv.RepairStatus = 'In') or (iv.Status = 4 and iv.servicetype = 'Repair' and iv.repairparentid = 0)) where (1 = 1) 
  and t.TransDate between @f_date and @t_date;

  if @RepairExist = 0 begin
    if @Report_RepairDepost > 0 set @RepairExist = 1;
  end
  
  if @RepairExist > 0 begin

  set @Temp_Tax1 = 0;
  set @Temp_Tax2 = 0;
  set @Temp_Tax3 = 0;

  declare sc_rp0 cursor
  for select ID, TransDate from trans where TransDate between @f_date and @t_date
  open sc_rp0 
  fetch next from sc_rp0 into @TranID, @TranDate
  while @@FETCH_STATUS = 0 begin



    declare sc_rp1 cursor
    for select ID,Tax1,Tax2,Tax3,Discount,Coupon,CouponPerc,Fees,FeesTax,RepairParentID,FeesCoupon,FeesCouponTax from invoice
	where TransactionNo = @TranID and Status = 18
    open sc_rp1 
    fetch next from sc_rp1 into @InvID, @Inv_Tax1, @Inv_Tax2, @Inv_Tax3, @Inv_Discount,
	@Inv_Coupon, @Inv_CouponPerc, @Inv_Fees, @Inv_FeesTax, @Repair_Parent,@Inv_FeesCoupon, @Inv_FeesCouponTax
    while @@FETCH_STATUS = 0 begin

	  

	 set @Temp_Tax1 = @Temp_Tax1 + @Inv_Tax1;
	 set @Temp_Tax2 = @Temp_Tax2 + @Inv_Tax2;
	 set @Temp_Tax3 = @Temp_Tax3 + @Inv_Tax3;

	 set @Report_RepairTicketDiscount = @Report_RepairTicketDiscount + @Inv_Coupon;
	 set @Report_RepairFees = @Report_RepairFees + @Inv_Fees + @Inv_FeesCoupon;
	 set @Report_RepairFeesTax = @Report_RepairFeesTax + @Inv_FeesTax + @Inv_FeesCouponTax;



	 if @Inv_TaxID1 > 0 begin
	    set @TxRepair_Name = '';
	    set @TxRepair_Rate = 0;
	    select @TxRepair_Name = isnull(TaxName,''), @TxRepair_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID1;
	    if @TaxNameRepair1 = '' and @TxRepair_Name <> ''
	      set @TaxNameRepair1 = @TxRepair_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@TxRepair_Rate)) + ' %';
          end;

	  if @Inv_TaxID2 > 0 begin
            set @TxRepair_Name = '';
	    set @TxRepair_Rate = 0;

	    select @TxRepair_Name = isnull(TaxName,''), @TxRepair_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID2;
	    if @TaxNameRepair2 = '' and @TxRepair_Name <> ''
	      set @TaxNameRepair2 = @TxRepair_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@TxRepair_Rate)) + ' %';
          end

	  if @Inv_TaxID3 > 0 begin
	    set @TxRepair_Name = '';
	    set @TxRepair_Rate = 0;

	    select @TxRepair_Name = isnull(TaxName,''), @TxRepair_Rate = isnull(TaxRate,0) from TaxHeader where ID = @Inv_TaxID3;
	    if @TaxNameRepair3 = '' and @TxRepair_Name <> ''
	      set @TaxNameRepair3 = @TxRepair_Name + ' ' + convert(varchar,convert(DOUBLE PRECISION,@TxRepair_Rate)) + ' %';
	  end

	  

	  declare sc_rp2 cursor
      for select ProductType, Price, NormalPrice, Qty, Cost, Taxable1, Taxable2,
	  Taxable3, TaxRate1, TaxRate2, TaxRate3, TaxType1, TaxType2, TaxType3, TaxTotal1, TaxTotal2, TaxTotal3, Discount, FSTender, RentDuration,
	  TaxIncludeRate,TaxIncludePrice from item
	  where InvoiceNo = @Repair_Parent and Tagged <> 'X' and ProductType <> 'C' and ProductType <> 'H' and ProductType <> 'A'
      open sc_rp2
      fetch next from sc_rp2 into @item_ProductType, @item_Price, @item_NormalPrice,
	  @item_Qty, @item_Cost, @item_Taxable1, @item_Taxable2,@item_Taxable3, @item_TaxRate1, @item_TaxRate2, @item_TaxRate3, 
	  @item_TaxType1, @item_TaxType2, @item_TaxType3, @item_TaxTotal1, @item_TaxTotal2, @item_TaxTotal3, @item_Discount,
	  @item_FSTender,@item_RentDuration,@item_TaxIncludeRate,@item_TaxIncludePrice
      while @@FETCH_STATUS = 0 begin
	    
		set @Tx_1 = 0;
		set @Tx_2 = 0;
		set @Tx_3 = 0;

		set @cpn_perc = (100 - @Inv_CouponPerc)/100;

              

		  if ((@item_Taxable1 = 'Y' or @item_Taxable2 = 'Y' or @item_Taxable3 = 'Y') and (@item_FSTender = 'N')) begin

     		    
		  	if @item_Taxable1 = 'Y' begin
		    		if @item_TaxType1 = 0 
			  		set @Tx_1 = round(((@item_Price * @item_Qty * @item_TaxRate1 / 100) * @cpn_perc),2);
				if @item_TaxType1 = 1 
			  		set @Tx_1 = round((@item_TaxTotal1 * @cpn_perc),2);
		  	end

		  	if @item_Taxable2 = 'Y' begin
		    		if @item_TaxType2 = 0 
			  		set @Tx_2 = round(((@item_Price * @item_Qty * @item_TaxRate2 / 100) * @cpn_perc),2);
				if @item_TaxType2 = 1 
			  		set @Tx_2 = round((@item_TaxTotal2 * @cpn_perc),2);
		  	end

		  	if @item_Taxable3 = 'Y' begin
		    		if @item_TaxType3 = 0 
			  		set @Tx_3 = round(((@item_Price * @item_Qty * @item_TaxRate3 / 100) * @cpn_perc),2);
				if @item_TaxType3 = 1 
			  		set @Tx_3 = round((@item_TaxTotal3 * @cpn_perc),2);
		  	end

          end
		     
			 if @tax_inclusive = 'N' begin       
               set @Report_RepairSales = @Report_RepairSales + round(((@item_Qty * @item_Price ) - @item_Discount),2);
			 end

			 if @tax_inclusive = 'Y' begin       
               set @Report_RepairSales = @Report_RepairSales + round((@item_TaxIncludePrice),2);
			 end

			 /*if @tax_inclusive = 'Y' begin
		        set @Report_RepairSales = @Report_RepairSales - round((@item_TaxTotal1 + @item_TaxTotal2 + @item_TaxTotal3),2);
		     end*/

		     set @Report_RepairDiscount = @Report_RepairDiscount + @item_Discount;


		
        fetch next from sc_rp2 into @item_ProductType, @item_Price, @item_NormalPrice,
	    @item_Qty, @item_Cost, @item_Taxable1, @item_Taxable2,@item_Taxable3, @item_TaxRate1, @item_TaxRate2, @item_TaxRate3, 
	    @item_TaxType1, @item_TaxType2, @item_TaxType3, @item_TaxTotal1, @item_TaxTotal2, @item_TaxTotal3, 
		@item_Discount, @item_FSTender,@item_RentDuration,@item_TaxIncludeRate,@item_TaxIncludePrice
      end
      close sc_rp2
      deallocate sc_rp2


     fetch next from sc_rp1 into @InvID, @Inv_Tax1, @Inv_Tax2, @Inv_Tax3, @Inv_Discount,@Inv_Coupon, @Inv_CouponPerc, @Inv_Fees, @Inv_FeesTax, @Repair_Parent,
	 @Inv_FeesCoupon, @Inv_FeesCouponTax
    end
    close sc_rp1
    deallocate sc_rp1
    
    fetch next from sc_rp0 into @TranID, @TranDate
  end
  close sc_rp0
  deallocate sc_rp0

  set @RpTax1 = @Temp_Tax1;
  set @RpTax2 = @Temp_Tax2;
  set @RpTax3 = @Temp_Tax3;

  end

  select @Paidout = isnull(sum(TenderAmount),0) from tender 
  where TransactionNo in (Select ID from Trans where TransType = 6 and TransDate between @f_date and @t_date);

  select @LottoPayout = isnull(sum(TenderAmount),0) from tender 
  where TransactionNo in (Select ID from Trans where TransType = 60 and TransDate between @f_date and @t_date);

  insert into SummaryData(TerminalName,ProductSales,ServiceSales,OtherSales,ProductDiscount,ServiceDiscount,OtherDiscount,
						  TicketDiscount_Sales,DestinationTax,TaxName1_Sales,TaxName2_Sales,TaxName3_Sales,
						  Tax1_Sales,Tax2_Sales,Tax3_Sales,TaxableSales_1,TaxableSales_2,TaxableSales_3,NonTaxableSales,
						  TaxableSales_Info_1,TaxableSales_Info_2,TaxableSales_Info_3,Fees_Sales,TaxOnFees_Sales,FS_Tender,
						  Layaway_Sales,GC_Sales,MGC_Sales,PGC_Sales,DGC_Sales, PLGC_Sales, CostOfGoods,BootleRefund,
						  P_Tax1,P_Tax2,P_Tax3,S_Tax1,S_Tax2,S_Tax3,B_Tax1,B_Tax2,B_Tax3,
						  RentExist,RentSales,Tax1_Rent,Tax2_Rent,Tax3_Rent,TaxName1_Rent,TaxName2_Rent,TaxName3_Rent,
						  TicketDiscount_Rent,Fees_Rent,TaxOnFees_Rent,
						  RepairExist,RepairSales,TaxName1_Repair,TaxName2_Repair,TaxName3_Repair,Tax1_Repair,Tax2_Repair,
						  Tax3_Repair,RepairDiscount,TicketDiscount_Repair,Fees_Repair,TaxOnFees_Repair,Repair_Deposit,
						  Free_Qty, Free_Amount,Paidout,LottoPayout, GiftAid)
			 
			 values(@terminal,@Report_ProductSales,@Report_ServiceSales,@Report_BlankItemSales,@Report_ProductDiscount,
					@Report_ServiceDiscount,@Report_OtherDiscount,@Report_TicketDiscount,@Report_DTax,@TaxName1,@TaxName2,
					@TaxName3,@SaleTax1,@SaleTax2,@SaleTax3,@Report_TaxableSales1,@Report_TaxableSales2,@Report_TaxableSales3,
					@Report_NonTaxableSales,@Tax1_Text,@Tax2_Text,@Tax3_Text,@Report_SaleFees, @Report_SaleFeesTax,
					@Report_FoodStampTendered,@Report_LayawaySalesPosted,@Report_GCSales,@Report_MercGCSales,@Report_PreGCSales,@Report_DatacapGCSales,@Report_POSLinkGCSales,
					@Report_CostOfGoods,@Report_BottleRefund,@PTax1,@PTax2,@PTax3,@STax1,@STax2,@STax3,@BTax1,@BTax2,@BTax3,
					@RentExist,@Report_RentSales,@RnTax1,@RnTax2,@RnTax3,@TaxNameRent1,@TaxNameRent2,@TaxNameRent3,@Report_RentDiscount,
					@Report_RentFees,@Report_RentFeesTax,
					@RepairExist,@Report_RepairSales,@TaxNameRepair1,@TaxNameRepair2,@TaxNameRepair3,@RpTax1,@RpTax2,@RpTax3,
					@Report_RepairDiscount,@Report_RepairTicketDiscount,@Report_RepairFees,@Report_RepairFeesTax,
					@Report_RepairDepost,@Report_FreeQty,@Report_FreeAmount,@Paidout,@LottoPayout, @Report_GiftAid);




end

GO