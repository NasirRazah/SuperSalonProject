create procedure [dbo].[sp_AddQBWEmployee]
					@empid					varchar(12),
					@firstname					nvarchar(20),
					@lastname					nvarchar(20),
					@addr1						nvarchar(60),
					@addr2					nvarchar(60),
					@city					nvarchar(20),
					@state					nvarchar(2),
					@zip					nvarchar(12),
					@phone					nvarchar(14),
					@ssn					nvarchar(11),
					@email					nvarchar(60),
					@listid					varchar(50),
					@editseq				varchar(50),
					@ReturnID			int output	

as 
declare @id		varchar(12);
declare @MaxID		int;
declare @shiftid	int;
declare @profileid  int;
begin

  set @ReturnID = 0;
  set @id = '';
  select @MaxID = count(*) + 1 from Employee;

   set @id = 'QBW' + cast(@MaxID as varchar(10));

   if @empid <> ''  set @id = @empid;

   set @shiftid = 0;
   set @profileid = 0;

   set @shiftid = (select top(1) isnull(ID,0) from shiftmaster);
   set @profileid = (select top(1) isnull(ID,0) from SecurityGroup);

  
  insert into Employee(EmployeeID,FirstName,LastName,EMail,Phone1,SSNumber,
  Address1,Address2,City,[State],Zip,ProfileID,EmpShift,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,QBListID,QBEditSequenceID)
  values(@id,@firstname,@lastname,@email,@phone,@ssn,
  @addr1,@addr2,@city,@state,@zip,@profileid,@shiftid,
  0, getdate(), 0, getdate(),@listid,@editseq)
  select @ReturnID = @@IDENTITY

 



  

end
					 
GO