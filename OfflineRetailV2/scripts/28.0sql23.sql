CREATE procedure [dbo].[sp_AddWooCommDiscount]
					@id					varchar(20),
					@discname				nvarchar(15),
					@disctype				char(1),
					@discamt				numeric(15,3),
					@discperc				numeric(15,3),
					@lperiod				char(1),
					@datefrom				datetime,
					@dateto					datetime,
					@ReturnID				int output	

as 

begin

  insert into DiscountMaster(DiscountName,DiscountStatus,DiscountType,DiscountAmount,DiscountPercentage,ChkLimitedPeriod,
  LimitedStartDate,LimitedEndDate,ChkApplyOnItem,WooCommID,
  CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)
  values(@discname,'Y',@disctype,@discamt,@discperc,@lperiod,@datefrom,@dateto,'Y',@id,
  0, getdate(), 0, getdate())
  select @ReturnID = @@IDENTITY

end
			 
GO