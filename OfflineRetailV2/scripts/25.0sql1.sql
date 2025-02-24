ALTER procedure [dbo].[sp_void]
			@InvNo	int,
			@User	int,
			@terml	nvarchar(50),
			@dt		datetime

as 
declare @ItemID			int;
declare @ProductID		int;
declare @ProductType	char(1);
declare @Qty			numeric(15,3);
declare @Cost			numeric(15,3);
declare @UOMCount		numeric(15,3)
declare @DocNo			nvarchar(50);
declare @updtqty		numeric(15,3);
declare @QtyOnHand		numeric(15,3);
declare @MatrixOptionID	int;
declare @OptionValue1	nvarchar(30);
declare @OptionValue2	nvarchar(30);
declare @OptionValue3	nvarchar(30);
declare @ID				int;
declare @sj				int;
declare @rout			int;
declare @kid			int;
declare @kcnt			int;
declare	@kcost			numeric(15,3);
declare @CustomerID		int;	
declare @pts			int;
declare @tranno			int;
declare @tenderno		int;
declare @tendername		nvarchar(40);
declare @SerializedVal	numeric(15,3);
declare @HouseAccouctTendering	int;
declare @StoreCreditTendering	int;

declare @StoreCreditTenderAmount	numeric(15,3);

begin

   set @sj = 0;
   set @HouseAccouctTendering = 0;

   update invoice set status = 4 where id = @InvNo;

   

   insert into voidinv(invoiceno,voidby,voidon)values(@InvNo,@User,getdate())
   select @ID = @@IDENTITY
   
   select @CustomerID = CustomerID,@tranno = TransactionNo from Invoice where ID = @InvNo;

   select @HouseAccouctTendering = count(*) from tender t left outer join invoice i on i.transactionno = t.transactionno
   left outer join tendertypes tt on tt.ID = t.tendertype where i.ID = @InvNo  and tt.name = 'House Account' ;

   if @HouseAccouctTendering > 0 begin /* House Account Tendering */
      delete from acctrecv where invoiceno = @InvNo and trantype = 2 and customerid = @CustomerID;
   end

   select @StoreCreditTendering = count(*) from tender t left outer join invoice i on i.transactionno = t.transactionno
   left outer join tendertypes tt on tt.ID = t.tendertype where i.ID = @InvNo  and tt.name = 'Store Credit' ;

    if @StoreCreditTendering > 0 begin /* Store Credit Tendering */
	  select @StoreCreditTenderAmount = TranAmount from StoreCreditTransaction where RefInvoice = @InvNo and RefCustomer = @CustomerID;
	  update customer set StoreCredit = StoreCredit - @StoreCreditTenderAmount where ID = @CustomerID;
      delete from StoreCreditTransaction  where RefInvoice = @InvNo and RefCustomer = @CustomerID;
   end


   declare vc cursor
   for select ID, ProductID, ProductType, Qty, Cost, UOMCount from item where invoiceno = @InvNo
   and producttype not in ('G','B','A','X','Z','C','H') and tagged <> 'X' and ReturnedItemID = 0
   open vc
   fetch next from vc into @ItemID, @ProductID, @ProductType, @Qty, @Cost, @UOMCount

   while @@fetch_status = 0 begin

    set @updtqty = 0;
    set @DocNo = '';

    set @DocNo = 'void / ' + cast(@ID as varchar(10)) + ' / ' + cast(@ProductID as varchar(10)) ;

    if @ProductType = 'U' begin

      set @updtqty = @Qty*@UOMCount;
      update product set qtyonhand = qtyonhand + @Qty*@UOMCount where ID = @ProductID;

      exec @sj =  sp_AddJournal @DocNo,  @dt, @ProductID, 'Stock In', 'Return',@updtqty,@Cost,@terml,@User,@dt,'','N', @rout output

    end
   
    if @ProductType <> 'U' begin

       update product set qtyonhand = qtyonhand + @Qty where ID = @ProductID;

       exec @sj =  sp_AddJournal @DocNo,  @dt, @ProductID, 'Stock In', 'Return',@Qty,@Cost,@terml,@User,@dt,'','N', @rout output


       if @ProductType = 'M' begin

          select @MatrixOptionID = MatrixOptionID, @OptionValue1 = OptionValue1, @OptionValue2 = OptionValue2, @OptionValue3 = OptionValue3 from itemmatrixoptions where ItemID = @ItemID

          UPDATE MATRIX SET QtyOnHand = QtyOnHand +@Qty, LastChangedBy=@User, LastChangedOn=getdate()
          Where MatrixOptionID = @MatrixOptionID and OptionValue1= @OptionValue1 and OptionValue2= @OptionValue2 and OptionValue3= @OptionValue3

       end


       if @ProductType = 'K' begin


          declare vck cursor

          for select k.ItemID, k.ItemCount, isnull(p.Cost,0) as Cost from kit k left outer join product p on k.KitID=p.ID where k.kitID = @ProductID

          open vck

          fetch next from vck into @kid, @kcnt, @kcost

          while @@fetch_status = 0 begin

	    set @DocNo = 'void / ' + cast(@ID as varchar(10)) + ' / ' + cast(@kid as varchar(10)) ;
            set @updtqty = 0;
	    set @updtqty = @Qty*@kcnt;	
            update product set qtyonhand = qtyonhand + @updtqty where ID = @kid;

            exec @sj =  sp_AddJournal @DocNo,  @dt, @kid, 'Stock In', 'Return',@updtqty,@kcost,@terml,@User,@dt,'','N', @rout output

 	    fetch next from vck into @kid, @kcnt, @kcost
          end
	  close vck
          deallocate vck

       end


       if @ProductType = 'E' begin

         select @SerializedVal = Qty - ReturnedItemCnt from item Where ID=@ItemID
	 if  @SerializedVal = 0 begin

	   Update SerialDetail set ItemID = 0 , soldtype = 'I', LastChangedBy=@User, LastChangedOn=getdate() Where ID= cast(@UOMCount as int);	   

         end

       end
      
      

    end


    if @CustomerID > 0 begin

      select @pts = points from product where ID = @ProductID;

      update customer set points = points - cast(@pts*@Qty as int) where ID = @CustomerID

    end




    fetch next from vc into @ItemID, @ProductID, @ProductType, @Qty, @Cost, @UOMCount

  end
  close vc
  deallocate vc



  declare vt cursor
  for select t.ID, n.name from tender t left outer join tendertypes n on t.tendertype = n.ID where t.transactionno = @tranno

  open vt
  fetch next from vt into @tenderno,@tendername
  while @@fetch_status = 0 begin

    if   @tendername = 'Gift Certificate' begin

      insert into giftcert (GiftCertID,Amount,ItemID,TenderNo,RegisterID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,CustomerID)
      select GiftCertID, -Amount,ItemID,TenderNo, RegisterID, @user,getdate(),@user,getdate(),CustomerID from giftcert where CustomerID=@CustomerID and TenderNo=@tenderno

    end  

    fetch next from vt into @tenderno,@tendername

  end
  close vt
  deallocate vt
  
end


