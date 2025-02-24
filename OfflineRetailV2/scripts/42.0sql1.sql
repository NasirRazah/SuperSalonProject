USE [Retail2020DB]
GO

ALTER procedure [dbo].[sp_purged]

as

declare 	@cutoffdays	int;
declare   @cutoffdate	datetime;
declare   @dateval		datetime;
declare   @clsoutid		int;
declare   @ItemRowID	int;
begin

  select @cutoffdays = purgecutoffday from setup;
  set @dateval = dateadd(day,-@cutoffdays,getdate());
  set @cutoffdate = convert (datetime,cast(datepart(month,@dateval) as varchar(2))+ '/' + cast(datepart(day,@dateval) as varchar(2))+ '/' + cast(datepart(year,@dateval) as varchar(4)) + ' 11:59:59 PM',101);
  
   delete from trans where TransDate <= @cutoffdate;
   delete from invoice where CreatedOn <= @cutoffdate;
   delete from tender where CreatedOn <= @cutoffdate;
   delete from AttendanceInfo where CreatedOn <= @cutoffdate;
   delete from CardAuthorisation where SaleOn <= @cutoffdate;
   delete from CardTrans where CreatedOn <= @cutoffdate;
   delete from CentralExportImportLog where FileCreatedDate <= @cutoffdate;
   
   delete from ItemMatrixOptions where CreatedOn <= @cutoffdate;
   delete from Layaway where CreatedOn <= @cutoffdate;
   delete from Laypmts where CreatedOn <= @cutoffdate;
   delete from MailItems where EMailTime <= @cutoffdate;
   delete from Notes where CreatedOn <= @cutoffdate;
   delete from POHeader where CreatedOn <= @cutoffdate;
   delete from PODetail where CreatedOn <= @cutoffdate;
   delete from Recipients where CreatedOn <= @cutoffdate;
   delete from RecvHeader where CreatedOn <= @cutoffdate;
   delete from RecvDetail where CreatedOn <= @cutoffdate;
   delete from StockJournal where TranDate <= @cutoffdate;
   delete from Suspnded where CreatedOn <= @cutoffdate;
   delete from VoidInv where VoidOn <= @cutoffdate;
   delete from WorkOrder where CreatedOn <= @cutoffdate;

   delete from StockTakeHeader where CreatedOn <= @cutoffdate;
   delete from StockTakeDetail where CreatedOn <= @cutoffdate;

   delete from TransferHeader where CreatedOn <= @cutoffdate;
   delete from TransferDetail where CreatedOn <= @cutoffdate;

   delete from ApplicationLogs where LogTime <= @cutoffdate;

    delete from GeneralLog where LogDateTime <= @cutoffdate;
   delete from EventLog where EventDateTime <= @cutoffdate;
   delete from AutoProcessInfo where StartTime <= @cutoffdate;   

   declare clsout cursor
   for select id from closeout where StartDatetime <= @cutoffdate
   open clsout
   fetch next from clsout into @clsoutid	
   while @@fetch_status = 0 begin
     delete from closeouttender where  closeoutid = @clsoutid
	 delete from CloseoutCurrencyCalculator where RefID = @clsoutid
     fetch next from clsout into @clsoutid	
   end
   close clsout 
   deallocate clsout 
   delete from CloseOut where StartDatetime <= @cutoffdate;


   set  @ItemRowID = 0;
   select @ItemRowID = Max(ID) from item where createdon <= @cutoffdate;
   if @ItemRowID > 0 
      delete from item where ID <= @ItemRowID;


    delete from ShopifyErrorLog where OperationTime <= @cutoffdate;
	delete from WooCommerceErrorLog where OperationTime <= @cutoffdate;
	delete from XEROErrorLog where OperationTime <= @cutoffdate;
	delete from QuickBooksErrorLog where OperationTime <= @cutoffdate;

	delete from Appointments where createdon <= @cutoffdate;

	delete from CashFloat where createdon <= @cutoffdate;
	delete from PrintLabel where createdon <= @cutoffdate;



end



GO