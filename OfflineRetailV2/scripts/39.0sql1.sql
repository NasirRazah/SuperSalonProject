ALTER procedure [dbo].[sp_getitemdiscount]
			@ID		int,
			@OrderTime	datetime,
			@AppDisc	varchar(500) output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage	numeric(15,3);
declare @ChkDaysAvailable	char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems	char(1);

declare @ChkLimitedPeriod	char(1);
declare @LimitedStartDate	datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);

declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR		char(1);


declare @Wkday			int;
declare @day			int;
declare @FindDate		int;

declare @countRItem		int;
declare @countRGroup		int;

declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime		datetime;
declare @edatetime		datetime;

declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);


declare @l1day			varchar(2);
declare @l1month		varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;

declare @l2day			varchar(2);
declare @l2month		varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;

declare @odate			datetime;

declare @DcntIDLen			int;

begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
    where DiscountStatus = 'Y' and ChkApplyOnItem = 'Y' and ChkApplyOnCustomer = 'N' order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid = @ID and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID = @ID)

          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end


	if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 

		  set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);
		  
          if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';
       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate
    end 
    close sc1
    deallocate sc1

end

GO

ALTER procedure [dbo].[sp_getitemdiscount_cust]
			@ID		int,
			@CUSTID	int,
			@OrderTime	datetime,
			@AppDisc	varchar(500) output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage		numeric(15,3);
declare @ChkDaysAvailable		char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems		char(1);
declare @ChkLimitedPeriod		char(1);
declare @LimitedStartDate		datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);
declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);
declare @Wkday			int;
declare @day			int;
declare @FindDate			int;
declare @countRItem		int;
declare @countRGroup		int;
declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;
declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);
declare @l1day			varchar(2);
declare @l1month			varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;
declare @l2day			varchar(2);
declare @l2month			varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;
declare @odate			datetime;

declare @DcntIDLen			int;


begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
    where DiscountStatus = 'Y' and ChkApplyOnItem = 'Y' and ChkApplyOnCustomer = 'Y' 
    and ID in ( select discountid from customer where id = @CUSTID ) order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid = @ID and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID = @ID)

          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end


	if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          
	set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate
    end 
    close sc1
    deallocate sc1

end

GO

ALTER procedure [dbo].[sp_getautoitemdiscount]
			@ID		int,
			@OrderTime	datetime,
			@AppDisc	varchar(500) output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage		numeric(15,3);
declare @ChkDaysAvailable		char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems		char(1);
declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);
declare @ChkLimitedPeriod		char(1);
declare @LimitedStartDate		datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);
declare @Wkday			int;
declare @day			int;
declare @FindDate			int;
declare @countRItem		int;
declare @countRGroup		int;
declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;
declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);
declare @l1day			varchar(2);
declare @l1month			varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;
declare @l2day			varchar(2);
declare @l2month			varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;

declare @odate			datetime;
declare @DcntIDLen			int;
begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
    where DiscountStatus = 'Y' and ChkApplyOnItem = 'Y' and ChkAutoApply = 'Y' and ChkApplyOnCustomer = 'N' order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate


    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid = @ID and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID = @ID)

          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      
       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end

       if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          /*set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';*/

	  set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = cast(@DcntID as varchar(1));
		  if @DcntIDLen = 2 set @AppDisc = cast(@DcntID as varchar(2));
		  if @DcntIDLen = 3 set @AppDisc = cast(@DcntID as varchar(3));
		  if @DcntIDLen = 4 set @AppDisc = cast(@DcntID as varchar(4));
		  if @DcntIDLen = 5 set @AppDisc = cast(@DcntID as varchar(5));
		  if @DcntIDLen = 6 set @AppDisc = cast(@DcntID as varchar(6));
		  if @DcntIDLen = 7 set @AppDisc = cast(@DcntID as varchar(7));
          	  /*set @AppDisc = cast(@DcntID as varchar(4));*/
          break;

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    end 
    close sc1
    deallocate sc1

end

GO

ALTER procedure [dbo].[sp_getautoitemdiscount_cust]
			@ID		int,
			@CUSTID	int,
			@OrderTime	datetime,
			@AppDisc	varchar(500) 	output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage		numeric(15,3);
declare @ChkDaysAvailable		char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems		char(1);

declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);

declare @ChkLimitedPeriod		char(1);
declare @LimitedStartDate		datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);

declare @Wkday			int;
declare @day			int;
declare @FindDate			int;

declare @countRItem		int;
declare @countRGroup		int;

declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;

declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);

declare @l1day			varchar(2);
declare @l1month			varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;

declare @l2day			varchar(2);
declare @l2month			varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;

declare @odate			datetime;

declare @DcntIDLen			int;

begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
    where DiscountStatus = 'Y' and ChkApplyOnItem = 'Y' and ChkAutoApply = 'Y' and ChkApplyOnCustomer = 'Y' 
    and ID in ( select discountid from customer where id = @CUSTID )  order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate


    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid = @ID and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID = @ID)

          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      
       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end

       if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          /*set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';*/
          /*set @AppDisc = cast(@DcntID as varchar(4));*/


	  set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

		if @DcntIDLen = 1 set @AppDisc = cast(@DcntID as varchar(1));
		  if @DcntIDLen = 2 set @AppDisc = cast(@DcntID as varchar(2));
		  if @DcntIDLen = 3 set @AppDisc = cast(@DcntID as varchar(3));
		  if @DcntIDLen = 4 set @AppDisc = cast(@DcntID as varchar(4));
		  if @DcntIDLen = 5 set @AppDisc = cast(@DcntID as varchar(5));
		  if @DcntIDLen = 6 set @AppDisc = cast(@DcntID as varchar(6));
		  if @DcntIDLen = 7 set @AppDisc = cast(@DcntID as varchar(7));

          break;

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    end 
    close sc1
    deallocate sc1

end

GO

ALTER procedure [dbo].[sp_getautoticketdiscount]
			@TerminalName	nvarchar(50),
			@OrderTime		datetime,
			@AppDisc		varchar(500) output

as

declare @DcntID				int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage	numeric(15,3);
declare @ChkDaysAvailable	char(1);
declare @ChkMonday			char(1);
declare @ChkTuesday			char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday			char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday			char(1);
declare @ChkAllDay			char(1);
declare @ChkAllDate			char(1);
declare @ChkRestrictedItems	char(1);
declare @proceedAllDay		char(1);
declare @proceedDay			char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);
declare @ChkLimitedPeriod	char(1);
declare @LimitedStartDate	datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);
declare @Wkday				int;
declare @day				int;
declare @FindDate			int;
declare @countRItem			int;
declare @countRGroup		int;
declare @stime				nvarchar(10);
declare @etime				nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;
declare @tday				varchar(2);
declare @tmonth				varchar(2);
declare @tyear				varchar(4);
declare @l1day				varchar(2);
declare @l1month			varchar(2);
declare @l1year				varchar(4);
declare @l1date				datetime;
declare @l2day				varchar(2);
declare @l2month			varchar(2);
declare @l2year				varchar(4);
declare @l2date				datetime;
declare @odate				datetime;
declare @DcntIDLen			int;
begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
   for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
   ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
   where DiscountStatus = 'Y' and ChkApplyOnTicket = 'Y' and ChkAutoApply = 'Y' and ChkApplyOnCustomer = 'N' order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate


    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @countRItem = 0;
       set @countRGroup	= 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid   in (select ID from TempDiscountID where TerminalName = @TerminalName) and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
	
          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 


          /*set @proceedR = 'Y';*/

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end

       if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4,val5,val6)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay,@countRItem,@countRGroup);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    end 
    close sc1
    deallocate sc1
    delete from TempDiscountID where TerminalName=@TerminalName;
end

GO

ALTER procedure [dbo].[sp_getautoticketdiscount_cust]
			@CUSTID	int,
			@TerminalName	nvarchar(50),
			@OrderTime	datetime,
			@AppDisc	varchar(500) output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage		numeric(15,3);
declare @ChkDaysAvailable		char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems		char(1);
declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);
declare @ChkLimitedPeriod		char(1);
declare @LimitedStartDate		datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);
declare @Wkday			int;
declare @day			int;
declare @FindDate			int;
declare @countRItem		int;
declare @countRGroup		int;
declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;
declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);
declare @l1day			varchar(2);
declare @l1month			varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;
declare @l2day			varchar(2);
declare @l2month			varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;
declare @odate			datetime;
declare @DcntIDLen			int;
begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
   for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
   ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
   where DiscountStatus = 'Y' and ChkApplyOnTicket = 'Y' and ChkAutoApply = 'Y' and ChkApplyOnCustomer = 'Y' 
   and ID in ( select discountid from customer where id = @CUSTID ) order by ID

   open sc1

   fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

   while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @countRItem = 0;
       set @countRGroup	= 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid   in (select ID from TempDiscountID where TerminalName = @TerminalName) and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
	
          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 


          /*set @proceedR = 'Y';*/

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end

       if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4,val5,val6)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay,@countRItem,@countRGroup);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    end 
    close sc1
    deallocate sc1
    delete from TempDiscountID where TerminalName=@TerminalName;
end


GO


ALTER procedure [dbo].[sp_getitemdiscount_cust]
			@ID		int,
			@CUSTID	int,
			@OrderTime	datetime,
			@AppDisc	varchar(500) output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage		numeric(15,3);
declare @ChkDaysAvailable		char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems		char(1);
declare @ChkLimitedPeriod		char(1);
declare @LimitedStartDate		datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);
declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);
declare @Wkday			int;
declare @day			int;
declare @FindDate			int;
declare @countRItem		int;
declare @countRGroup		int;
declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;
declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);
declare @l1day			varchar(2);
declare @l1month			varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;
declare @l2day			varchar(2);
declare @l2month			varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;
declare @odate			datetime;
declare @DcntIDLen			int;
begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
    where DiscountStatus = 'Y' and ChkApplyOnItem = 'Y' and ChkApplyOnCustomer = 'Y' 
    and ID in ( select discountid from customer where id = @CUSTID ) order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
       set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid = @ID and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID = @ID)

          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end


	if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate
    end 
    close sc1
    deallocate sc1

end


GO

ALTER procedure [dbo].[sp_getticketdiscount]
			@TerminalName	nvarchar(50),
			@OrderTime		datetime,
			@AppDisc		varchar(500) output

as

declare @DcntID				int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage	numeric(15,3);
declare @ChkDaysAvailable	char(1);
declare @ChkMonday			char(1);
declare @ChkTuesday			char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday			char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday			char(1);
declare @ChkAllDay			char(1);
declare @ChkAllDate			char(1);
declare @ChkRestrictedItems	char(1);
declare @ChkLimitedPeriod	char(1);
declare @LimitedStartDate	datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);
declare @proceedAllDay		char(1);
declare @proceedDay			char(1);
declare @proceedDate		char(1);
declare @proceedR			char(1);
declare @Wkday				int;
declare @day				int;
declare @FindDate			int;
declare @countRItem			int;
declare @countRGroup		int;
declare @stime				nvarchar(10);
declare @etime				nvarchar(10);
declare @sdatetime			datetime;
declare @edatetime			datetime;
declare @tday				varchar(2);
declare @tmonth				varchar(2);
declare @tyear				varchar(4);
declare @l1day				varchar(2);
declare @l1month			varchar(2);
declare @l1year				varchar(4);
declare @l1date				datetime;
declare @l2day				varchar(2);
declare @l2month			varchar(2);
declare @l2year				varchar(4);
declare @l2date				datetime;
declare @odate				datetime;
declare @DcntIDLen			int;
begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
               where DiscountStatus = 'Y' and ChkApplyOnTicket = 'Y' and ChkApplyOnCustomer = 'N' order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate


    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
	   set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @countRItem = 0;
       set @countRGroup	= 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid   in (select ID from TempDiscountID where TerminalName = @TerminalName) and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
	
          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 


          /*set @proceedR = 'Y';*/

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end

       if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4,val5,val6)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay,@countRItem,@countRGroup);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    end 
    close sc1
    deallocate sc1
    delete from TempDiscountID where TerminalName=@TerminalName;
end



GO

ALTER procedure [dbo].[sp_getticketdiscount_cust]
			@CUSTID			int,
			@TerminalName		nvarchar(50),
			@OrderTime		datetime,
			@AppDisc		varchar(500) output

as

declare @DcntID			int;
declare @DiscountType		char(1);
declare @DiscountAmount		numeric(15,3);
declare @DiscountPercentage	numeric(15,3);
declare @ChkDaysAvailable	char(1);
declare @ChkMonday		char(1);
declare @ChkTuesday		char(1);
declare @ChkWednesday		char(1);
declare @ChkThursday		char(1);
declare @ChkFriday		char(1);
declare @ChkSaturday		char(1);
declare @ChkSunday		char(1);
declare @ChkAllDay		char(1);
declare @ChkAllDate		char(1);
declare @ChkRestrictedItems	char(1);

declare @ChkLimitedPeriod	char(1);
declare @LimitedStartDate	datetime;
declare @LimitedEndDate		datetime;
declare @proceedLimiled		char(1);

declare @proceedAllDay		char(1);
declare @proceedDay		char(1);
declare @proceedDate		char(1);
declare @proceedR		char(1);

declare @Wkday			int;
declare @day			int;
declare @FindDate		int;

declare @countRItem		int;
declare @countRGroup		int;

declare @stime			nvarchar(10);
declare @etime			nvarchar(10);
declare @sdatetime		datetime;
declare @edatetime		datetime;

declare @tday			varchar(2);
declare @tmonth			varchar(2);
declare @tyear			varchar(4);

declare @l1day			varchar(2);
declare @l1month		varchar(2);
declare @l1year			varchar(4);
declare @l1date			datetime;

declare @l2day			varchar(2);
declare @l2month		varchar(2);
declare @l2year			varchar(4);
declare @l2date			datetime;

declare @odate			datetime;
declare @DcntIDLen			int;

begin

   set @OrderTime = getdate();

   set @AppDisc = '';

   declare sc1 cursor
    for select ID,DiscountType,DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,
    ChkFriday,ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate from discountmaster
    where DiscountStatus = 'Y' and ChkApplyOnTicket = 'Y' and ChkApplyOnCustomer = 'Y' 
    and ID in (select discountid from customer where id = @CUSTID) order by ID

    open sc1

    fetch next from sc1 into @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate


    while @@fetch_status = 0 begin

       set @proceedAllDay = 'N'; 
       set @proceedDay = 'N'; 
       set @proceedDate = 'N';
       set @proceedR = 'N';
	   set @proceedLimiled = 'N';
       set @Wkday = 0;
       set @day = 0;
       set @FindDate = 0;

       set @countRItem = 0;
       set @countRGroup	= 0;

       set @tday = cast( datepart(day,@OrderTime) as varchar(2));
       set @tmonth = cast( datepart(month,@OrderTime) as varchar(2));
       set @tyear = cast( datepart(year,@OrderTime) as varchar(4));

       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from DiscountRestrictedItems where itemtype = 'I' and itemid   in (select ID from TempDiscountID where TerminalName = @TerminalName) and discountid = @DcntID
          select @countRGroup = count(*) from DiscountRestrictedItems where itemtype = 'G' and discountid = @DcntID and itemid in ( select categoryid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
	
          if (@countRItem > 0) or (@countRGroup > 0) set @proceedR = 'Y'; 


          /*set @proceedR = 'Y';*/

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if  @ChkDaysAvailable = 'Y' set @proceedAllDay = 'Y';


       
       if  @ChkDaysAvailable = 'N' begin
         
	     set @Wkday = datepart(weekday,@OrderTime);
         
         if @Wkday = 1 begin

            if @ChkSunday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 2 begin

            if @ChkMonday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 3 begin

            if @ChkTuesday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 4 begin

            if @ChkWednesday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 5 begin

            if @ChkThursday = 'Y' set @proceedAllDay = 'Y'

         end

         if @Wkday = 6 begin

            if @ChkFriday = 'Y' set @proceedAllDay = 'Y'

         end


         if @Wkday = 7 begin

            if @ChkSaturday = 'Y' set @proceedAllDay = 'Y'

         end
       

       end
       

       if  @ChkAllDate = 'Y' set @proceedDate = 'Y';

       if  @ChkAllDate = 'N' begin

	  set @day = datepart(day,@OrderTime);

          select @FindDate = count(*) from discountdate where discountid = @DcntID and dateofmonth = @day

          if @FindDate = 1 set @proceedDate = 'Y';

       end
      


       if  @ChkAllDay = 'Y' set @proceedDay = 'Y';

       if  @ChkAllDay = 'N' begin


           declare sc2 cursor
    		for select starttime,endtime from discounttime where DiscountID = @DcntID order by ID

    	   open sc2

           fetch next from sc2 into @stime,@etime

           while @@fetch_status = 0 begin

             set @sdatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@stime,101)
             set @edatetime = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear +' '+@etime,101)

             if @OrderTime between @sdatetime and @edatetime set @proceedDay = 'Y';
             

             fetch next from sc2 into @stime,@etime
	   end
           close sc2
           deallocate sc2

       end

       if @ChkLimitedPeriod = 'N' set @proceedLimiled = 'Y';
        if @ChkLimitedPeriod = 'Y' begin

          set @l1day = cast( datepart(day,@LimitedStartDate) as varchar(2));
          set @l1month = cast( datepart(month,@LimitedStartDate) as varchar(2));
          set @l1year = cast( datepart(year,@LimitedStartDate) as varchar(4));
          set @l1date = convert(datetime,@l1month+'/' + @l1day +'/'+ @l1year,101);

          set @l2day = cast( datepart(day,@LimitedEndDate) as varchar(2));
          set @l2month = cast( datepart(month,@LimitedEndDate) as varchar(2));
          set @l2year = cast( datepart(year,@LimitedEndDate) as varchar(4));
          set @l2date = convert(datetime,@l2month+'/' + @l2day +'/'+ @l2year,101);

          set @odate = convert(datetime,@tmonth+'/' + @tday +'/'+ @tyear,101);

          if @odate between @l1date and @l2date set @proceedLimiled = 'Y';

        end

       /*insert into table1(val1,val2,val3,val4,val5,val6)values(@proceedR,@proceedDay,@proceedDate,@proceedAllDay,@countRItem,@countRGroup);*/

       if (@proceedR = 'Y') and (@proceedDay = 'Y') and (@proceedDate = 'Y') and (@proceedAllDay = 'Y') and (@proceedLimiled = 'Y')
       begin
	
          /*if @DiscountType = 'P' set @DiscP = @DiscP + @DiscountPercentage;*/
          /*if @DiscountType = 'A' set @DiscA = @DiscA + @DiscountAmount;*/ 
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@DcntID);

if @DcntIDLen = 1 set @AppDisc = @AppDisc + cast(@DcntID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppDisc = @AppDisc + cast(@DcntID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppDisc = @AppDisc + cast(@DcntID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppDisc = @AppDisc + cast(@DcntID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppDisc = @AppDisc + cast(@DcntID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppDisc = @AppDisc + cast(@DcntID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppDisc = @AppDisc + cast(@DcntID as varchar(7)) + ',';

       end
      

      fetch next from sc1 into  @DcntID,@DiscountType,@DiscountAmount,@DiscountPercentage,@ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,
    				@ChkFriday,@ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,@ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate

    end 
    close sc1
    deallocate sc1
    delete from TempDiscountID where TerminalName=@TerminalName;
end


GO


ALTER procedure [dbo].[sp_getfeesncharges]
			@ID		int,
			@AppFee	varchar(500) output

as

declare @FeesID				int;
declare @FeesType			char(1);
declare @FeesAmount			numeric(15,3);
declare @FeesPercentage		numeric(15,3);
declare @ChkRestrictedItems	char(1);
declare @proceedR		char(1);
declare @countRItem		int;
declare @countRGroup	int;
declare @countRFamily	int;
declare @countRDept	int;
declare @DcntIDLen			int;
begin

   set @AppFee = '';

   declare sc1 cursor
    for select ID,FeesType,FeesAmount,FeesPercentage,ChkRestrictedItems from feesmaster where FeesStatus = 'Y' and ChkInclude = 'N' and ChkApplyItemTicket = 'I' order by ID

    open sc1

    fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems

    while @@fetch_status = 0 begin

       set @proceedR = 'N';
       
       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from FeesRestrictedItems where itemtype = 'I' and itemid = @ID and feesid = @FeesID
          select @countRGroup = count(*) from FeesRestrictedItems where itemtype = 'G' and feesid = @FeesID and itemid in ( select categoryid from product where ID = @ID)
          select @countRFamily = count(*) from FeesRestrictedItems where itemtype = 'F' and feesid = @FeesID and itemid in ( select brandid from product where ID = @ID)
          select @countRDept = count(*) from FeesRestrictedItems where itemtype = 'D' and feesid = @FeesID and itemid in ( select departmentid from product where ID = @ID)
          
          
          
          
          if (@countRItem > 0) or (@countRGroup > 0) or (@countRFamily > 0) or (@countRDept > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if (@proceedR = 'Y')
       begin
	
          

	  set @DcntIDLen = 0;
		  set @DcntIDLen = len(@FeesID);

if @DcntIDLen = 1 set @AppFee = @AppFee + cast(@FeesID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppFee = @AppFee + cast(@FeesID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppFee = @AppFee + cast(@FeesID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppFee = @AppFee + cast(@FeesID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppFee = @AppFee + cast(@FeesID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppFee = @AppFee + cast(@FeesID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppFee = @AppFee + cast(@FeesID as varchar(7)) + ',';

       end

      fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems
    end 
    close sc1
    deallocate sc1

end

GO


ALTER procedure [dbo].[sp_getfeesncharges_auto]
			@ID		int,
			@AppFee	varchar(500) output

as

declare @FeesID				int;
declare @FeesType			char(1);
declare @FeesAmount			numeric(15,3);
declare @FeesPercentage		numeric(15,3);
declare @ChkRestrictedItems	char(1);
declare @proceedR		char(1);
declare @countRItem		int;
declare @countRGroup	int;
declare @countRFamily	int;
declare @countRDept	int;
declare @DcntIDLen			int;
begin

   set @AppFee = '';

   declare sc1 cursor
    for select ID,FeesType,FeesAmount,FeesPercentage,ChkRestrictedItems from feesmaster where FeesStatus = 'Y' and ChkInclude = 'N' and
    ChkAutoApply = 'Y' and ChkApplyItemTicket = 'I' order by ID

    open sc1

    fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems

    while @@fetch_status = 0 begin

       set @proceedR = 'N';
       
       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from FeesRestrictedItems where itemtype = 'I' and itemid = @ID and feesid = @FeesID
          select @countRGroup = count(*) from FeesRestrictedItems where itemtype = 'G' and feesid = @FeesID and itemid in ( select categoryid from product where ID = @ID)
          select @countRFamily = count(*) from FeesRestrictedItems where itemtype = 'F' and feesid = @FeesID and itemid in ( select brandid from product where ID = @ID)
          select @countRDept = count(*) from FeesRestrictedItems where itemtype = 'D' and feesid = @FeesID and itemid in ( select departmentid from product where ID = @ID)
          
          
          
          
          if (@countRItem > 0) or (@countRGroup > 0) or (@countRFamily > 0) or (@countRDept > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if (@proceedR = 'Y')
       begin
	
          /*set @AppFee = @AppFee + cast(@FeesID as varchar(4));*/

set @DcntIDLen = 0;
		  set @DcntIDLen = len(@FeesID);

if @DcntIDLen = 1 set @AppFee = @AppFee + cast(@FeesID as varchar(1));
		  if @DcntIDLen = 2 set @AppFee = @AppFee + cast(@FeesID as varchar(2));
		  if @DcntIDLen = 3 set @AppFee = @AppFee + cast(@FeesID as varchar(3));
		  if @DcntIDLen = 4 set @AppFee = @AppFee + cast(@FeesID as varchar(4));
		  if @DcntIDLen = 5 set @AppFee = @AppFee + cast(@FeesID as varchar(5));
		  if @DcntIDLen = 6 set @AppFee = @AppFee + cast(@FeesID as varchar(6));
		  if @DcntIDLen = 7 set @AppFee = @AppFee + cast(@FeesID as varchar(7));
		  break;
       end

      fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems
    end 
    close sc1
    deallocate sc1

end



GO

ALTER procedure [dbo].[sp_getfeesncharges_ticket]
		    @TerminalName	varchar(50),
			@AppFee	varchar(500) output

as

declare @FeesID				int;
declare @FeesType			char(1);
declare @FeesAmount			numeric(15,3);
declare @FeesPercentage		numeric(15,3);
declare @ChkRestrictedItems	char(1);
declare @proceedR		char(1);
declare @countRItem		int;
declare @countRGroup	int;
declare @countRFamily	int;
declare @countRDept	int;
declare @DcntIDLen			int;
begin

   set @AppFee = '';

   declare sc1 cursor
    for select ID,FeesType,FeesAmount,FeesPercentage,ChkRestrictedItems from feesmaster where FeesStatus = 'Y' and ChkInclude = 'N' and ChkApplyItemTicket = 'T' order by ID

    open sc1

    fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems

    while @@fetch_status = 0 begin

       set @proceedR = 'N';
       
       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from FeesRestrictedItems where itemtype = 'I' and itemid in (select ID from TempDiscountID where TerminalName = @TerminalName) and feesid = @FeesID
          select @countRGroup = count(*) from FeesRestrictedItems where itemtype = 'G' and feesid = @FeesID and itemid in ( select categoryid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
          select @countRFamily = count(*) from FeesRestrictedItems where itemtype = 'F' and feesid = @FeesID and itemid in ( select brandid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
          select @countRDept = count(*) from FeesRestrictedItems where itemtype = 'D' and feesid = @FeesID and itemid in ( select departmentid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
          
          
          
          
          if (@countRItem > 0) or (@countRGroup > 0) or (@countRFamily > 0) or (@countRDept > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if (@proceedR = 'Y')
       begin
	
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@FeesID);

if @DcntIDLen = 1 set @AppFee = @AppFee + cast(@FeesID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppFee = @AppFee + cast(@FeesID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppFee = @AppFee + cast(@FeesID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppFee = @AppFee + cast(@FeesID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppFee = @AppFee + cast(@FeesID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppFee = @AppFee + cast(@FeesID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppFee = @AppFee + cast(@FeesID as varchar(7)) + ',';

       end

      fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems
    end 
    close sc1
    deallocate sc1

end


GO


ALTER procedure [dbo].[sp_getfeesncharges_ticket_auto]
		    @TerminalName	varchar(50),
			@AppFee	varchar(500) output

as

declare @FeesID				int;
declare @FeesType			char(1);
declare @FeesAmount			numeric(15,3);
declare @FeesPercentage		numeric(15,3);
declare @ChkRestrictedItems	char(1);
declare @proceedR		char(1);
declare @countRItem		int;
declare @countRGroup	int;
declare @countRFamily	int;
declare @countRDept	int;
declare @DcntIDLen			int;

begin

   set @AppFee = '';

   declare sc1 cursor
    for select ID,FeesType,FeesAmount,FeesPercentage,ChkRestrictedItems from feesmaster where FeesStatus = 'Y' and ChkInclude = 'N' and ChkAutoApply = 'Y' 
	and ChkApplyItemTicket = 'T' order by ID

    open sc1

    fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems

    while @@fetch_status = 0 begin

       set @proceedR = 'N';
       
       if @ChkRestrictedItems = 'Y'
       begin

	      select @countRItem = count(*) from FeesRestrictedItems where itemtype = 'I' and itemid in (select ID from TempDiscountID where TerminalName = @TerminalName) and feesid = @FeesID
          select @countRGroup = count(*) from FeesRestrictedItems where itemtype = 'G' and feesid = @FeesID and itemid in ( select categoryid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
          select @countRFamily = count(*) from FeesRestrictedItems where itemtype = 'F' and feesid = @FeesID and itemid in ( select brandid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
          select @countRDept = count(*) from FeesRestrictedItems where itemtype = 'D' and feesid = @FeesID and itemid in ( select departmentid from product where ID in (select ID from TempDiscountID where TerminalName = @TerminalName))
          
          
          
          
          if (@countRItem > 0) or (@countRGroup > 0) or (@countRFamily > 0) or (@countRDept > 0) set @proceedR = 'Y'; 

       end 

       if @ChkRestrictedItems = 'N' set @proceedR = 'Y'; 

       if (@proceedR = 'Y')
       begin
	
          set @DcntIDLen = 0;
		  set @DcntIDLen = len(@FeesID);

if @DcntIDLen = 1 set @AppFee = @AppFee + cast(@FeesID as varchar(1)) + ',';
		  if @DcntIDLen = 2 set @AppFee = @AppFee + cast(@FeesID as varchar(2)) + ',';
		  if @DcntIDLen = 3 set @AppFee = @AppFee + cast(@FeesID as varchar(3)) + ',';
		  if @DcntIDLen = 4 set @AppFee = @AppFee + cast(@FeesID as varchar(4)) + ',';
		  if @DcntIDLen = 5 set @AppFee = @AppFee + cast(@FeesID as varchar(5)) + ',';
		  if @DcntIDLen = 6 set @AppFee = @AppFee + cast(@FeesID as varchar(6)) + ',';
		  if @DcntIDLen = 7 set @AppFee = @AppFee + cast(@FeesID as varchar(7)) + ',';

       end

      fetch next from sc1 into @FeesID,@FeesType,@FeesAmount,@FeesPercentage,@ChkRestrictedItems
    end 
    close sc1
    deallocate sc1

end


GO


