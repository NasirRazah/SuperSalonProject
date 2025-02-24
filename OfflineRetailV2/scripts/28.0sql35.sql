CREATE procedure [dbo].[sp_AddXEROCustomer]
					@id					varchar(20),
					@firstname				nvarchar(20),
					@lastname				nvarchar(20),
					@email					nvarchar(60),
					@phone					nvarchar(14),
					@mobile					nvarchar(14),
					@addr1					nvarchar(60),
					@addr2					nvarchar(60),
					@city					nvarchar(20),
					@state					nvarchar(20),
					@zip					nvarchar(12),
					@country				nvarchar(30),
					@saddr1					nvarchar(60),
					@saddr2					nvarchar(60),
					@scity					nvarchar(20),
					@sstate					nvarchar(20),
					@szip					nvarchar(12),
					@scountry				nvarchar(30),
					@xeroid					varchar(100),
					@xeroname				varchar(200),
					@ReturnID			int output	

as 
declare @cid		nvarchar(20);
declare	@autoFlag	char(1);
declare @MaxID	int;
declare @store		nvarchar(10);
begin

  set @ReturnID = 0;
  set @cid = '';
  set @store = '';

  select @autoFlag = AutoCustomer from Setup;
  select @MaxID = count(*) + 1 from Customer;
  if @autoFlag = 'Y' begin
   
	set @cid = cast(@MaxID as nvarchar(20));
  end 

  if @autoFlag = 'N' begin
    set @cid = 'XERO-' + cast(@MaxID as nvarchar(20));
  end

  
  
  insert into Customer (CustomerID,FirstName,LastName,EMail,WorkPhone,MobilePhone,
  Address1,Address2,City,[State],Zip,Country,ShipAddress1,ShipAddress2,ShipCity,ShipState,ShipZip,ShipCountry,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,DiscountLevel,XEROID,XEROName)
  values(@cid,@firstname,@lastname,@email,@phone,@mobile,
  @addr1,@addr2,@city,@state,@zip,@country,@saddr1,@saddr2,@scity,@sstate,@szip,@scountry,
  0, getdate(), 0, getdate(),'A',@xeroid,@xeroname)
  select @ReturnID = @@IDENTITY

 



  

end