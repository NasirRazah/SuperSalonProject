USE [Retail2020DB]
GO

ALTER procedure [dbo].[sp_InsertInitialPOSFunction]
		@User			int
			
as

declare @Count		int;
declare @fCount		int;

declare @Count1		int;
declare @fCount1	int;

begin

  begin transaction

  select @Count = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton'
  
  if @Count < 47
  begin


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Down'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Down',1,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Up'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Up',2,@User,getdate(),@User,getdate());
    
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Paid Out'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Paid Out',3,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'No Sale'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','No Sale',4,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Cancel'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Cancel',5,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Layaway'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Layaway',6,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Acct Pay'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Acct Pay',7,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Gift Cert'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Gift Cert',8,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Resume/Suspend'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Resume/Suspend',9,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Return Reprint'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder, CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Return Reprint',10,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Refresh Stock'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Refresh Stock',11,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Select Apps'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Select Apps',12,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Cust. Picture'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Cust. Picture',13,@User,getdate(),@User,getdate());


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Cust. Notes'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Cust. Notes',14,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Emp. Picture'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Emp. Picture',15,@User,getdate(),@User,getdate());


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Product Picture'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Product Picture',16,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Product Notes'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Product Notes',17,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'View Product Price'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','View Product Price',18,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Change Product Price'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Change Product Price',19,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Use Price Level'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Use Price Level',20,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fast Cash'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fast Cash',21,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Gift Cert Balance'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Gift Cert Balance',22,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Invoice Item Notes'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Invoice Item Notes',23,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Work Order'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Work Order',24,@User,getdate(),@User,getdate());

     select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Print Cust. Label'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Print Cust. Label',25,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Print Gift Receipt'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Print Gift Receipt',26,@User,getdate(),@User,getdate());
      
   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Discount Ticket'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Discount Ticket',27,@User,getdate(),@User,getdate());   

   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Book Appt.'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Book Appt.',28,@User,getdate(),@User,getdate());   

   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Recall Appt.'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Recall Appt.',29,@User,getdate(),@User,getdate());   
      
   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Recall Rent'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Recall Rent',30,@User,getdate(),@User,getdate()); 
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Recall Repair'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Recall Repair',31,@User,getdate(),@User,getdate()); 

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Revert CARD Tran.'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Revert CARD Tran.',32,@User,getdate(),@User,getdate());

    
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Mercury Gift Card'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Mercury Gift Card',33,@User,getdate(),@User,getdate());
 

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fast CC'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fast CC',34,@User,getdate(),@User,getdate());
      

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fees & Charges Item'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fees & Charges Item',35,@User,getdate(),@User,getdate());
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'EBT/ Mercury Gift Card Balance'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','EBT/ Mercury Gift Card Balance',36,@User,getdate(),@User,getdate());   


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Bottle Refund'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Bottle Refund',37,@User,getdate(),@User,getdate());   
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Discount Item'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Discount Item',38,@User,getdate(),@User,getdate()); 
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Qty (+)'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Qty (+)',39,@User,getdate(),@User,getdate());
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Qty (-)'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Qty (-)',40,@User,getdate(),@User,getdate());    

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Tare'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Tare',41,@User,getdate(),@User,getdate());

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fees & Charges Ticket'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fees & Charges Ticket',42,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Points to Store Credit'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Points to Store Credit',43,@User,getdate(),@User,getdate());

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Lotto Payout'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Lotto Payout',44,@User,getdate(),@User,getdate());


	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Paid In'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Paid In',45,@User,getdate(),@User,getdate());

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Safe Drop'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Safe Drop',46,@User,getdate(),@User,getdate());


	  select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Gift Aid'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,IsVisible)
      values('FunctionButton','Gift Aid',47,@User,getdate(),@User,getdate(),'N');

  end

  select @Count1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' 
  if @Count1 < 4 begin
    select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Cancel'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Cancel',1,@User,getdate(),@User,getdate());

    select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Ingredients'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Ingredients',2,@User,getdate(),@User,getdate());

    select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Recipe'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Recipe',3,@User,getdate(),@User,getdate());

	select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Host'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Host',4,@User,getdate(),@User,getdate());
  end

  commit
  return 0 

end

GO
