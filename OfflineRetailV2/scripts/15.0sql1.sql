ALTER procedure [dbo].[sp_store_imp_cust_h]
			
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
