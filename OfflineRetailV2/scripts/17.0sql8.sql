ALTER procedure [dbo].[sp_CentralExport]
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
