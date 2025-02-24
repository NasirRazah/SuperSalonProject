CREATE procedure [dbo].[sp_AddQBWVendor]
					@name					varchar(30),
					@accountno				varchar(16),
					@contact				varchar(30),
					@addr1					varchar(60),
					@addr2					varchar(60),
					@city					varchar(20),
					@state					varchar(2),
					@zip					varchar(12),
					@country				varchar(30),
					@phone					varchar(14),
					@fax					varchar(14),
					@email					varchar(60),
					@notes					varchar(250),
					@listid					varchar(50),
					@editseq				varchar(50),
					@ReturnID			int output	

as 
declare @vid		varchar(10);
declare @MaxID		int;
begin

  set @ReturnID = 0;
  set @vid = '';
  select @MaxID = count(*) + 1 from Vendor;

   set @vid = 'QBW-' + cast(@MaxID as varchar(10));



  
  insert into Vendor (VendorID,[Name],Contact,EMail,Phone,Fax,AccountNo,
  Address1,Address2,City,[State],Zip,Country,Notes,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,QBListID,QBEditSequenceID)
  values(@vid,@name,@contact,@email,@phone,@fax,@accountno,
  @addr1,@addr2,@city,@state,@zip,@country,@notes,
  0, getdate(), 0, getdate(),@listid,@editseq)
  select @ReturnID = @@IDENTITY

 



  

end
					 
GO