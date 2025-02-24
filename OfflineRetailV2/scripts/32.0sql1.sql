ALTER procedure [dbo].[sp_store_imp_product]
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
		@PDCost			numeric(15,3),
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
                                 ScaleFontColor,ScaleIsBold,ScaleIsItalics,SplitWeight,UOM,AddToPosCategoryScreen,DiscountedCost )
                                 values(@SKU,@PDesc,@PType,@PPriceA,@PPriceB,@PPriceC,@PCost, @POnHandQty,@PReorderQty,@PNormalQty,
                                 @iDept,@iCat,@PFS,@PMinAge,@PScaleBrCd,@SKU2,@SKU3,@PLastCost,@PLayawayQty,@PAllowZeroStk,
		     					 @PDispStk,@PAddPOS,@PNoPriceLbl,@PPrompt,@PBinL,@PPrintBrCd,@PLbl,@PPrntQty,@PPoints,@PDecimal,@PActive,@iBrnd,@PUPC,@PSeason,
                                 0,getdate(),0,getdate(),@PBkGrnd,@PScrnColor,@PScrnStyle,@PFont,@PFontS,@PFontC,@PBold,@PItalics,
		     					 @PCaseQty,@PCUPC,@PLinkSKU,@PBrkRatio,@PRentalPerMinute,@PRentalPerHour,@PRentalPerHalfDay,@PRentalPerDay,@PRentalPerWeek,@PRentalPerMonth,
                                 @PRentalDeposit,@PMinSrv,@PRepairCharge,@PRentalMinHour,@PRentalMinAmount,@PRental,@PReprCrg,@PReprTag,getdate(),
                                 @PNotes,@Tare,@PAddScale,@PNonDiscountable,@PNotes2,@Tare2,@ItemDisplayOrder,@SBkGrnd,@SScrnColor,@SScrnStyle,@SFont,@SFontS,@SFontC,@SBold,@SItalics,@SplitWeight,@UOM,
								 @PAddPOSCat,@PDCost)
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
				   Tare2=@Tare2,POSDisplayOrder=@ItemDisplayOrder,SplitWeight=@SplitWeight,UOM=@UOM,
				   AddToPosCategoryScreen=@PAddPOSCat,DiscountedCost=@PDCost where ID = @PID;
                                 
					

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
