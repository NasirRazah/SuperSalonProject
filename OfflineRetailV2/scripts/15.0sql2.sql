ALTER procedure [dbo].[sp_store_imp_emp_h]
			@EmployeeID			nvarchar(12),
			@LastName			nvarchar(20),
			@FirstName			nvarchar(20),
			@Address1			nvarchar(60),
			@Address2			nvarchar(60),
			@City				nvarchar(20),
			@State				nvarchar(2),
			@Zip				nvarchar(12),
			@Phone1				nvarchar(14),
			@Phone2				nvarchar(14),
			@EmergencyPhone			nvarchar(14),
			@EmergencyContact		nvarchar(30),
			@EMail				nvarchar(60),
			@SSNumber			nvarchar(11),
			@EmpRate			numeric(15, 3),
			@IssueStore			nvarchar(10),
			@OperateStore			nvarchar(10)
as

declare @empid	int; 	
declare @zcnt1 	int;
declare @zcnt2 	int;

begin

  set @empid = 0;

  select @empid = isnull(id,0) from Employee where issuestore = @IssueStore and employeeid=@EmployeeID

  if @empid = 0 
  insert into Employee ( EmployeeID,LastName,FirstName,Address1,Address2,City,State,Zip,
						 Phone1,Phone2,EmergencyPhone,EmergencyContact,EMail,SSNumber,EmpRate,IssueStore,OperateStore,ExpFlag )
                values(  @EmployeeID,@LastName,@FirstName,@Address1,@Address2,@City,@State,@Zip,
						 @Phone1,@Phone2,@EmergencyPhone,@EmergencyContact,@EMail,@SSNumber,@EmpRate,
					     @IssueStore,@OperateStore, 'Y' );
  if @empid > 0 
  update Employee set LastName=@LastName,FirstName=@FirstName,Address1=@Address1,Address2=@Address2,
            City=@City,State=@State,Zip=@Zip,Phone1=@Phone1,Phone2=@Phone2,EmergencyPhone=@EmergencyPhone,
            EmergencyContact=@EmergencyContact,EMail=@EMail,SSNumber=@SSNumber,EmpRate=@EmpRate,
            OperateStore=@OperateStore,ExpFlag='Y' where id = @empid
			
			
			
  if @Zip <> '' begin
    set @zcnt1 = 0;
    select @zcnt1 = count(*) from zipcode where zip = @Zip;
    if @zcnt1 = 0 
      insert into zipcode(City,State,Zip,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values(@City,@State,@Zip,0,getdate(),0,getdate());
  end			
	  	
	  	
	
	  	
	  		
			
			
end

