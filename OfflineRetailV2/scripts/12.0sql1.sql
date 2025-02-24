 
-- Create table [dbo].[TransTypes]
--
CREATE TABLE [dbo].[TransTypes] (
  [ID] [int] IDENTITY,
  [TransType] [smallint] NOT NULL,
  [TransName] [varchar](40) NOT NULL,
  [CreatedBy] [int] NULL DEFAULT (0),
  [CreatedOn] [datetime] NULL,
  [LastChangedBy] [int] NULL DEFAULT (0),
  [LastChangedOn] [datetime] NULL DEFAULT (getdate()),
  CONSTRAINT [PK_TransTypes] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportTransactionJournal]
--
CREATE TABLE [dbo].[CentralExportTransactionJournal] (
  [ID] [int] IDENTITY,
  [TransID] [int] NULL,
  [TransTypeInt] [smallint] NULL,
  [TransType] [varchar](40) NULL,
  [IsLayaway] [int] NULL,
  [TransDate] [datetime] NULL,
  [Emp] [varchar](20) NULL,
  [CloseoutID] [int] NULL,
  [TerminalName] [varchar](50) NULL,
  [InvNo] [int] NULL DEFAULT (0),
  [InvAmt] [numeric](15, 3) NULL,
  [Tender] [varchar](100) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportTaxAnalysis]
--
CREATE TABLE [dbo].[CentralExportTaxAnalysis] (
  [ID] [int] IDENTITY,
  [ProductID] [int] NULL,
  [SKU] [nvarchar](16) NULL,
  [Description] [nvarchar](200) NULL,
  [Qty] [numeric](15, 3) NULL,
  [Price] [numeric](15, 3) NULL,
  [PriceI] [numeric](15, 3) NULL,
  [Cost] [numeric](15, 3) NULL,
  [Discount] [numeric](15, 3) NULL,
  [Tax] [numeric](15, 3) NULL,
  [TransDate] [datetime] NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportStockValuation]
--
CREATE TABLE [dbo].[CentralExportStockValuation] (
  [ID] [int] IDENTITY,
  [SKU] [varchar](16) NULL,
  [Description] [varchar](200) NULL,
  [Qty] [numeric](15, 3) NULL,
  [Cost] [numeric](15, 3) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportSalesMatrixMainReport]
--
CREATE TABLE [dbo].[CentralExportSalesMatrixMainReport] (
  [ID] [int] IDENTITY,
  [ItemType] [varchar](10) NULL,
  [MenuID] [varchar](16) NULL,
  [MenuName] [varchar](50) NULL,
  [Price] [numeric](15, 3) NULL DEFAULT (0),
  [Cost] [numeric](15, 3) NULL DEFAULT (0),
  [Discount] [numeric](15, 3) NULL DEFAULT (0),
  [Surcharge] [numeric](15, 3) NULL DEFAULT (0),
  [IsModifier] [char](1) NULL DEFAULT ('N'),
  [CustomizedDisplayinPOS] [char](1) NULL DEFAULT ('N'),
  [Qty] [numeric](15, 3) NULL DEFAULT (0),
  [PriceI] [numeric](15, 3) NULL DEFAULT (0),
  [Tax] [numeric](15, 3) NULL DEFAULT (0),
  [ProductTypeFilterID] [varchar](20) NULL,
  [ProductTypeFilterDesc] [varchar](20) NULL,
  [ItemGroupFilterID] [varchar](10) NULL,
  [ItemGroupFilterDesc] [varchar](30) NULL,
  [EmployeeFilterID] [varchar](12) NULL DEFAULT (0),
  [EmployeeFilterDesc] [varchar](40) NULL,
  [BusinessDate] [datetime] NULL,
  [OrderStatus] [varchar](10) NULL,
  [IsVoided] [char](1) NULL DEFAULT ('N'),
  [EmployeeID] [int] NULL,
  [ProductTypeID] [int] NULL,
  [ItemGroupID] [int] NULL,
  [TransID] [int] NULL,
  [InvoiceID] [int] NULL,
  [ItemID] [int] NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportPackingList]
--
CREATE TABLE [dbo].[CentralExportPackingList] (
  [ID] [int] IDENTITY,
  [SKU] [nvarchar](16) NULL,
  [Description] [nvarchar](200) NULL,
  [SoldQty] [numeric](15, 3) NULL,
  [IssueStore] [nvarchar](10) NULL,
  [CustomerID] [nvarchar](20) NULL,
  [TransDate] [datetime] NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportOrderReceiving]
--
CREATE TABLE [dbo].[CentralExportOrderReceiving] (
  [ID] [int] IDENTITY,
  [RecvDetailID] [int] NOT NULL,
  [RecvHeaderID] [int] NOT NULL,
  [BatchID] [int] NULL,
  [PurchaseOrder] [varchar](16) NULL,
  [InvoiceNo] [varchar](16) NULL,
  [InvoiceTotal] [numeric](15, 4) NULL,
  [GrossAmount] [numeric](15, 4) NULL,
  [Tax] [numeric](15, 4) NULL,
  [Freight] [numeric](15, 4) NULL,
  [DateOrdered] [datetime] NULL,
  [ReceiveDate] [datetime] NULL,
  [DateTimeStamp] [datetime] NULL,
  [Note] [varchar](250) NULL,
  [VendorID] [varchar](10) NULL,
  [PriceA] [numeric](15, 3) NULL,
  [DQty] [numeric](15, 4) NULL,
  [DCost] [numeric](15, 4) NULL,
  [DFreight] [numeric](15, 4) NULL,
  [DTax] [numeric](15, 4) NULL,
  [ProductName] [varchar](150) NULL,
  [VendorPartNo] [varchar](16) NULL,
  [CheckClerkID] [varchar](12) NULL,
  [CheckClerk] [nvarchar](40) NULL,
  [RecvClerkID] [varchar](12) NULL,
  [RecvClerk] [nvarchar](40) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportLayAwayProduct]
--
CREATE TABLE [dbo].[CentralExportLayAwayProduct] (
  [ID] [int] IDENTITY,
  [CustID] [nvarchar](20) NULL,
  [SKU] [nvarchar](16) NULL,
  [Description] [nvarchar](200) NULL,
  [Qty] [numeric](15, 3) NULL,
  [Dept] [nvarchar](30) NULL,
  [PriceA] [numeric](15, 3) NULL DEFAULT (0),
  [QtyOnHand] [numeric](15, 3) NULL DEFAULT (0),
  [QtyOnLayaway] [numeric](15, 3) NULL DEFAULT (0),
  [AdjQty] [numeric](15, 3) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportLayAwayCustomer]
--
CREATE TABLE [dbo].[CentralExportLayAwayCustomer] (
  [ID] [int] IDENTITY,
  [CustID] [nvarchar](20) NULL,
  [TransDate] [datetime] NULL,
  [InvoiceNo] [int] NULL,
  [LayawayNo] [int] NULL DEFAULT (0),
  [SKU] [nvarchar](16) NULL,
  [Description] [nvarchar](200) NULL,
  [Qty] [numeric](15, 3) NULL,
  [TotalSale] [numeric](15, 3) NULL,
  [DateDue] [datetime] NULL,
  [Status] [int] NULL,
  [TransactionNo] [int] NULL DEFAULT (0),
  [Payment] [numeric](15, 3) NULL DEFAULT (0),
  [Name] [varchar](40) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportDiscounts]
--
CREATE TABLE [dbo].[CentralExportDiscounts] (
  [ID] [int] IDENTITY,
  [ProductType] [char](1) NULL,
  [SKU] [nvarchar](16) NULL,
  [Description] [nvarchar](200) NULL,
  [DiscountText] [varchar](60) NULL,
  [Discount] [numeric](15, 3) NULL,
  [TransDate] [datetime] NULL,
  [FilterDate] [datetime] NULL,
  [InvoiceID] [int] NULL,
  [TDiscountItem] [numeric](15, 3) NULL,
  [TDiscountInvoice] [numeric](15, 3) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create column [IsDeleted] on table [dbo].[Product]
--
ALTER TABLE [dbo].[Product]
  ADD [IsDeleted] [bit] NULL DEFAULT ('FALSE')
GO

GO

--
-- Create column [StockValuationFlag] on table [dbo].[Product]
--
ALTER TABLE [dbo].[Product]
  ADD [StockValuationFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Refresh view [dbo].[Product_View_Temp1]
--
EXEC sp_refreshview '[dbo].[Product_View_Temp1]'
GO

GO

--
-- Create procedure [dbo].[sp_UpdateBookerProductPrice]
--
GO
CREATE procedure [dbo].[sp_UpdateBookerProductPrice]
			@SKU		varchar(20),
			@UpdatedPrice		[numeric](15, 3),
			@LastChangedBy		int,
			@ReturnID			int output

as
declare @PCount		int;

begin
	select @PCount = count(*) from Product where SKU = @SKU and IsDeleted=0; /* Check if exists in Product */
	if @PCount > 0 begin
		update Product set PriceA=@UpdatedPrice, LastChangedBy=@LastChangedBy, LastChangedOn=getdate() where SKU = @SKU and IsDeleted=0
		set @ReturnID = 0; 
		return @ReturnID;

	end

	else begin
		set @ReturnID = 1; 
		return @ReturnID;
	end
end



GO

GO

--
-- Create procedure [dbo].[sp_DeleteBookerProduct]
--
GO
CREATE procedure [dbo].[sp_DeleteBookerProduct]
			@SKU				varchar(20),
			@ReturnID			int output

as
declare @PCount		int;

begin
	select @PCount = count(*) from Product where SKU = @SKU and IsDeleted=0; /* Check if exists in Product */
	if @PCount > 0 begin
		update Product set ProductStatus='N', IsDeleted=1, LastChangedOn=getdate() where SKU = @SKU and IsDeleted=0
		set @ReturnID = 0; 
		return @ReturnID;

	end

	else begin
		set @ReturnID = 1; 
		return @ReturnID;
	end
end



GO

GO

--
-- Create column [ExpFlag] on table [dbo].[RecvDetail]
--
ALTER TABLE [dbo].[RecvDetail]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [DiscountFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [DiscountFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [LayAwayCustomerFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [LayAwayCustomerFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [LayAwayProductFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [LayAwayProductFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [TaxAnalysisFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [TaxAnalysisFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [PackingListFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [PackingListFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [TransactionJournalFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [TransactionJournalFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_transactionjournal_export]
--
GO


create procedure [dbo].[sp_co_imp_updttag_transactionjournal_export]
as

begin

  update trans set transactionjournalFlag='Y' where transactionjournalFlag = 'N';

end


GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_taxanalysis_export]
--
GO


create procedure [dbo].[sp_co_imp_updttag_taxanalysis_export]
as

begin

  update trans set TaxAnalysisFlag='Y' where TaxAnalysisFlag = 'N';

end


GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_packinglist_export]
--
GO


create procedure [dbo].[sp_co_imp_updttag_packinglist_export]
as

begin

  update trans set packinglistFlag='Y' where packinglistFlag = 'N';

end


GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_layawayproduct_export]
--
GO
create procedure [dbo].[sp_co_imp_updttag_layawayproduct_export]
as

begin

  update trans set LayAwayProductFlag='Y' where LayAwayProductFlag = 'N';

end

GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_layawaycustomer_export]
--
GO
create procedure [dbo].[sp_co_imp_updttag_layawaycustomer_export]
as

begin

  update trans set LayAwayCustomerFlag='Y' where LayAwayCustomerFlag = 'N';

end

GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_discount_export]
--
GO
create procedure [dbo].[sp_co_imp_updttag_discount_export]
as

begin

  update trans set DiscountFlag='Y' where DiscountFlag = 'N';

end

GO

GO

--
-- Create column [VendorID] on table [dbo].[CentralExportPOHeader]
--
ALTER TABLE [dbo].[CentralExportPOHeader]
  ADD [VendorID] [varchar](10) NULL
GO

GO

--
-- Create function [dbo].[FetchTrnJournalTenderFunction]
--
GO
 CREATE function [dbo].[FetchTrnJournalTenderFunction]
 (
	@tNo int  
)
 returns varchar(100) 

 AS

 Begin
	--CREATE TABLE #temp    
	--(          
	--	RowNum  int IDENTITY (1,1) Primary key,      
	--	Name varchar (30),         
	--	Amt [numeric](15, 3)
	--) 

	DECLARE @ID INT = 1;
	DECLARE @RowCnt INT;
	declare @data varchar (100);
	declare @tempdata varchar (100);
	set @data=''

	--insert into #temp (Name, Amt)
	--	select isnull(tt.DisplayAs,'') as Name,isnull(t.TenderAmount,0) as Amt , RowNum
	--	--into #temp 
	--	from Tender t left outer join TenderTypes tt on t.TenderType = tt.ID 
	--	where t.TransactionNo=@tNo order by tt.DisplayAs

 
	 

		SELECT @RowCnt = COUNT(*) FROM (
				select top 100 percent 
				ROW_NUMBER() OVER(ORDER BY tt.DisplayAs) AS RowNum,

				isnull(tt.DisplayAs,'') as Name,isnull(t.TenderAmount,0) as Amt 
				from Tender t left outer join TenderTypes tt on t.TenderType = tt.ID 
				where t.TransactionNo=@tNo order by tt.DisplayAs
		) as T   
		WHILE @ID <= @RowCnt
		BEGIN
		   SELECT @tempdata = 
		   Name + ' ' +
		   (CASE
			WHEN Amt>=0 Then 'T'
			ELSE 'R'
		   END) +
		   ':' +
		   Cast (cast(Amt as numeric (36,2)) as varchar) + ' '
		   
		   FROM (
				select top 100 percent 
				ROW_NUMBER() OVER(ORDER BY tt.DisplayAs) AS RowNum,

				isnull(tt.DisplayAs,'') as Name,isnull(t.TenderAmount,0) as Amt 
				from Tender t left outer join TenderTypes tt on t.TenderType = tt.ID 
				where t.TransactionNo=@tNo order by tt.DisplayAs
		)as T WHERE RowNum = @ID
 
 
		   SET @data = @data + @tempData

		   SET @ID += 1
		END  

		 return  @data
	-- 	drop table #temp
	
	 
end
GO

GO

--
-- Create procedure [dbo].[FetchTrnJournalTender]
--
GO
 CREATE Procedure [dbo].[FetchTrnJournalTender]
 (
	@tNo int  ,
	 @dataItem varchar(100) OUTPUT
)
 
 AS
 Begin
	--CREATE TABLE #temp    
	--(          
	--	RowNum  int IDENTITY (1,1) Primary key,      
	--	Name varchar (30),         
	--	Amt [numeric](15, 3)
	--) 

	DECLARE @ID INT = 1;
	DECLARE @RowCnt INT;
	declare @data varchar (100);
	declare @tempdata varchar (100);
	set @data=''

	--insert into #temp (Name, Amt)
	--	select isnull(tt.DisplayAs,'') as Name,isnull(t.TenderAmount,0) as Amt , RowNum
	--	--into #temp 
	--	from Tender t left outer join TenderTypes tt on t.TenderType = tt.ID 
	--	where t.TransactionNo=@tNo order by tt.DisplayAs

 
	 

		SELECT @RowCnt = COUNT(*) FROM (
		select top 100 percent isnull(tt.DisplayAs,'') as Name,isnull(t.TenderAmount,0) as Amt ,0 RowNum
		from Tender t left outer join TenderTypes tt on t.TenderType = tt.ID 
		where t.TransactionNo=@tNo order by tt.DisplayAs
		)T   
		WHILE @ID <= @RowCnt
		BEGIN
		   SELECT @tempdata = 
		   Name + ' ' +
		   (CASE
			WHEN Amt>=0 Then 'T'
			ELSE 'R'
		   END) +
		   ':' +
		   Cast (cast(Amt as numeric (36,2)) as varchar) + ' '
		   
		   FROM #temp WHERE RowNum = @ID
 
 
		   SET @data = @data + @tempData

		   SET @ID += 1
		END  

		--select @data
		drop table #temp
	
	set  @dataItem = @data
end
GO

GO

--
-- Create procedure [dbo].[sp_CentralExportTransactionJournal]
--
GO
CREATE Procedure [dbo].[sp_CentralExportTransactionJournal]

AS
BEGIN

	delete from CentralExportTransactionJournal





	Declare @JournalTender varchar(100) =''

	select t.ID,t.TransType, tt.TransName as TransTypeName, 
	CASE
		WHEN tt.TransName = 'No Sale' THEN 0
		WHEN (select count(*) from laypmts where TransactionNo=t.ID)>0 THEN 1
		ELSE 0
	END as IsLayaway,
	t.TransDate,isnull(e.EmployeeID,'ADMIN') as Emp,t.CloseoutID,t.TerminalName
	into #temp1 from trans t  
	left outer join TransTypes tt on t.TransType=tt.TransType
	left outer join Employee e on e.ID = t.EmployeeID 
	where t.TransactionJournalFlag <> 'Y'
	--where t.TransDate between @F and @T 
	order by t.TransDate 

	select t.*,
	CASE 
		When t.IsLayaway=1 
			then (select top 1 layawayno from invoice where id in ( select top 1 invoiceno from laypmts where TransactionNo = t.ID))
		When t.IsLayaway=0 and t.TransTypeName <> 'No Sale' 
			THEN  (select top 1 ID from Invoice where TransactionNo=t.ID)
		ELSE -1
	END as InvNo
	into #temp2 from #temp1 t


	select t2.* ,
	CASE 
		When t2.IsLayaway=1 
			then ( select sum(totalsale) as tot from invoice where LayawayNo = t2.invNo )
		When t2.IsLayaway=0 and t2.TransTypeName <> 'No Sale' 
			THEN  ( select TotalSale from Invoice  where TransactionNo=t2.ID)
		ELSE 0
	END as InvAmt, 
	CASE 
		When t2.IsLayaway=0 and t2.TransTypeName <> 'No Sale' 
			THEN  ( select Status from Invoice  where TransactionNo=t2.ID)
		ELSE 0
	END as Status,
	CASE 
		When t2.IsLayaway=0 and t2.TransTypeName <> 'No Sale' 
			THEN  ( select ServiceType from Invoice  where TransactionNo=t2.ID)
		ELSE ''
	END as ServiceType,
	dbo.FetchTrnJournalTenderFunction (t2.ID) as Tender
  
	into #temp3
	from #temp2 t2
	order by t2.TransDate 





	insert into CentralExportTransactionJournal (TransID, TransTypeInt, IsLayaway, TransDate, Emp, CloseoutID, TerminalName, InvNo, InvAmt, Tender, TransType)
		select t.ID, t.transtype as TransTypeInt, t.IsLayaway, t.TransDate, t.Emp, t.CloseoutID, t.TerminalName,
		isnull (t.invNo, 0) InvNo, isnull (t.InvAmt, 0) InvAmt, Tender,
		CASE
			WHEN t.Status=4 Then 'Void'
			WHEN t.ServiceType = 'Sales' and isnull (InvAmt, 0) < 0 Then 'Return'
			ELSE t.TransTypeName
		END as TransType
	
		 from #temp3 t
		order by t.TransDate




	drop table #temp1
	drop table #temp2
	drop table #temp3

END
GO

GO

--
-- Alter procedure [dbo].[sp_CentralExport_V1]
--
GO
ALTER procedure [dbo].[sp_CentralExport_V1]
		@ExportType	varchar(3)	
as

begin
	delete from CentralExportPOHeader
	delete from CentralExportEmployeeOtherReports
	delete from CentralExportNotes
	delete from CentralExportCardAuthorisation
	delete from CenteralExportSuspndedOrderReport
	delete from CenteralExportSalesMatrixMainReport
	delete from CentralExportSalesByPeriodReport
	delete from CentralExportCloseOut
	delete from CentralExportAppointments
	delete from CentralExportDiscounts
	delete from CentralExportLayAwayCustomer
	delete from CentralExportLayAwayProduct
	delete from CentralExportTaxAnalysis
	delete from CentralExportPackingList
	delete from CentralExportStockValuation
	delete from CentralExportOrderReceiving


	if @ExportType = 'ALL' begin  
		-- inserting CentralExportPOHeader
		insert into CentralExportPOHeader (ID,OrderNo,OrderDate,ExpectedDeliveryDate,VendorName, VendorID, 
		GrossAmount, Freight, Tax, NetAmount,Priority , SupplierInstructions, GeneralNotes, VendorMinOrderAmount  ) 
				select a.ID,OrderNo,OrderDate,ExpectedDeliveryDate,b.Name as VendorName,  b.VendorID,
				GrossAmount, Freight, Tax, NetAmount,Priority , SupplierInstructions, GeneralNotes, VendorMinOrderAmount  
				From POHeader a 
				left outer join Vendor b on a.VendorID = b.ID 
				where a.ExpFlag='N'
				
		-- inserting CentralExportEmployeeOtherReports


		insert into CentralExportEmployeeOtherReports (TransType,  status, TransDate, StoreID, CID, TS ,CustID, Company, Customer,
		Address1, City, State, Zip, WorkPhone, Email, DateLastPurchase, AmountLastPurchase,  CustGroup, CustClass)
				select Distinct  t.TransType, i.status, t.TransDate, t.StoreID,
				t.CustomerID as CID,isnull(sum(distinct i.TotalSale),'0') as TS,c.CustomerID as CustID,c.Company,  c.LastName + ', '+c.FirstName as Customer,
				c.Address1, c.City, c.State, c.Zip, c.WorkPhone,c.Email,  c.DateLastPurchase, c.AmountLastPurchase,isnull(gr.Description,'') as CustGroup,
				isnull(cl.Description,'') as CustClass 
				from Trans t left outer join invoice i on t.ID = i.TransactionNo  
				left outer join customer c on c.ID = t.CustomerID   
				left outer join GeneralMapping gmG  on c.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group'  
				left outer join GeneralMapping gmC on c.ID = gmC.MappingID and gmC.MappingType = 'Customer' and  gmC.ReferenceType = 'Class'  
				left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and  gmG.ReferenceType = 'Group' 
				and c.ID = gmG.MappingID  
				left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and  gmC.ReferenceType = 'Class' and c.ID = gmC.MappingID  
				where (1 = 1) and t.CustomerID > 0 
				and t.TransType in( 1,4,18 )
				and i.status in (3,18)  
				and isnull(t.ExpFlag,'N')='N'
				--and t.TransDate between '1/6/2020 12:00:01 AM' and '9/29/2021 11:59:59 PM'  
				group by t.TransType, i.status, t.TransDate,t.StoreID,
				t.CustomerID,c.CustomerID,c.Company,c.LastName + ', '+c.FirstName,c.Address1, c.City,  c.State, c.Zip,c.WorkPhone,
				c.Email,c.DateLastPurchase, c.AmountLastPurchase,gr.Description,cl.Description order by c.CustomerID 


				-- inserting Notes
		insert into CentralExportNotes (NotesID, RefID, CustomerID, RefType, Note, DateTime, SpecialEvent, ScanFile, DocumentFile)
				select n.ID, RefID, c.CustomerID, RefType, Note, DateTime, SpecialEvent, ScanFile, DocumentFile 
				from Notes n
				join Customer c on c.id=n.RefID
				where isnull(n.ExpFlag,'N')='N' and SpecialEvent='Y'


				
				-- inserting CardAuthorisation
		insert into CentralExportCardAuthorisation (CardAuthorisationID, CardType, SaleTranNo, CancelTranNo, InvoiceNo, InvoiceAmount, SaleOn, SaleBy, 
		CancelOn, CancelBy, BatchFlag, TipAmount, AuthorisedTranNo, CompleteTranNo, AuthorisedOn, AuthorisedBy, CompleteOn, CompleteBy, EmployeeID)
				select c.ID, CardType, SaleTranNo, CancelTranNo, InvoiceNo, InvoiceAmount, SaleOn, SaleBy, CancelOn, CancelBy, BatchFlag, 
				TipAmount, AuthorisedTranNo, CompleteTranNo, AuthorisedOn, AuthorisedBy, CompleteOn, CompleteBy, e.EmployeeID
				from CardAuthorisation  c
				left outer join employee e on c.SaleBy = e.ID 
				where isnull(c.ExpFlag,'N')='N'


		insert into CenteralExportSuspndedOrderReport (SuspendedID, InvoiceNo, Amount, DateTimeSuspended, CustomerID)
				select s.ID as SuspendedID, i.ID as InvoiceNo,i.TotalSale as Amount,s.DateTimeSuspended,  isnull (c.CustomerID,0)CustomerID
				from suspnded as s 
				left outer join Invoice as i on i.ID = s.InvoiceNo 
				left outer join  Customer as c on c.ID = s.CustomerID 
				where (i.Status = 2) and isnull(s.ExpFlag,'N')='N'
				order by i.ID desc


		insert into CenteralExportSalesMatrixMainReport (SyncID, TransCustomerID, CustomerName, TaxIncludeRate, 
		TransDate, ProductStatus, ProductType,SKU,Description,
		Qty,Price,Cost,Discount,QtyOnHandVendor,PCostVendor,CustomerID,FilterIDSKU,FilterDescSKU,  
		FilterIDDept, FilterDescDept, FilterIDCat, FilterDescCat, FilterIDBrand, FilterDescBrand,  
		FilterIDEmp,FilterDescEmp, UPC, Season, Brand , VendorID, Vendor ,PartNumber)

				select distinct CONVERT(varchar(10),a.id) +','+ CONVERT(varchar(10),t.id) as SyncID, 
				t.CustomerID as TransCustomerID, cust.FirstName + ' ' + cust.LastName as CustomerName, a.TaxIncludeRate,
				t.TransDate, p.ProductStatus, a.ProductType as ProductType,a.SKU,a.Description,
				isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost,  isnull(a.Discount,0) as Discount,
				p.QtyOnHand as QtyOnHandVendor,p.Cost as PCostVendor,cust.CustomerID as CustomerID,'' as FilterIDSKU, '' as FilterDescSKU,  
				b.DepartmentID as FilterIDDept, b.Description as FilterDescDept, c.CategoryID as FilterIDCat, c.Description as FilterDescCat,  
				br.BrandID as FilterIDBrand, br.BrandDescription as FilterDescBrand, isnull(e.EmployeeID,0) as FilterIDEmp, 
				isnull(e.LastName + ' ' + e.FirstName,'SUPER USER') as FilterDescEmp, isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,
				isnull(br.BrandDescription,'') as Brand , isnull(v.VendorID,'') as VendorID,isnull(v.Name,'') as Vendor ,vp.PartNumber 
				--into ##temp1
				from Trans t left outer join Invoice i on t.ID = i.TransactionNo left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID  
				left outer join Dept AS b on a.DepartmentID = b.ID left outer join Category AS c ON a.CategoryID = c.ID left outer join Employee AS e ON e.ID = t.EmployeeID 
				left outer join Customer AS cust ON cust.ID = t.CustomerID left outer join Product p on a.ProductID = p.ID  
				LEFT OUTER JOIN VendPart AS vp ON a.ProductID = vp.ProductID LEFT OUTER JOIN Vendor AS v ON vp.VendorID = v.ID left outer join BrandMaster br on p.BrandID = br.ID  
				where (1 = 1) 
				and a.Qty is not null 
				and a.Cost is not null  
				and t.TransType in( 1,4,18) 
				and i.status in (3,18) 
				and a.Tagged <> 'X'  
				and isnull(t.ExpFlag,'N')='N'
				--and t.CustomerID in (3,6) and v.ID in ( 1,2,3 ) and t.ExpFlag='N' and p.ProductStatus = 'Y'  
				--and t.TransDate between '1-1-2020' and '10-8-2021'  
				Order by a.Description --FilterDesc asc,Qty desc, a.SKU, a.Description 


				 --	QUERY FOR SalesMatrixMonthlyReport
				-- select Distinct i.ID, DatePart(Month,t.TransDate) as Month, DatePart(Year,t.TransDate) as Year,  
				-- isnull((i.TotalSale - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale, 
				-- isnull((i.TotalSale),0) as TotSaleI  
				-- into #temp1
				-- from Trans t 
				-- LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo  
				-- left outer join item m On m.invoiceno = i.id  
				-- where (1 = 1) 
				-- and t.TransType in( 1,4,18 ) 
				-- and i.status in(3,18) 
				-- and m.Tagged <> 'X'  
				-- --and t.TransDate between @SDT and @TDT  
				-- order by DatePart(Month,t.TransDate), DatePart(Year,t.TransDate) 

				--select month, year, sum (TotSale), sum (TotSaleI) from #temp1
				--group by month, year
				--order by 2 desc

				--drop table #temp1


				--	CloseOutMainFrontReport / Header Report
		insert into CentralExportCloseOut (CloseOutID, CloseoutType, ConsolidatedID, StartDatetime	, EndDatetime, Notes, TransactionCnt, EmployeeID, TerminalName)
				select ID, CloseoutType, ConsolidatedID, StartDatetime	, EndDatetime, Notes, TransactionCnt, CreatedBy, TerminalName 
				from CloseOut where isnull( ExpFlag,'N')='N'



				 -- Sale by Period Report
		insert into CentralExportSalesByPeriodReport(transdate, CategoryID, DepartmentID, PT, Price,price1,Cost,syncId) 
				SELECT 
				t.transdate, b.CategoryID, d.DepartmentID,
				a.producttype                                AS PT,
				Isnull(Sum(a.price * a.qty - a.discount), 0) AS Price,
				Isnull(Sum(a.taxincluderate * a.qty), 0)     AS PriceI,
				Isnull(Sum(a.cost * a.qty), 0)               AS Cost,
				1
				FROM   trans t
						LEFT OUTER JOIN invoice i ON t.id = i.transactionno
						LEFT OUTER JOIN item a ON a.invoiceno = i.id OR a.invoiceno = i.repairparentid
						LEFT OUTER JOIN category AS b ON a.categoryid = b.id
						LEFT OUTER JOIN dept AS d ON a.departmentid = d.id
						LEFT OUTER JOIN product p ON a.productid = p.id
						LEFT OUTER JOIN brandmaster br ON p.brandid = br.id
				WHERE  ( 1 = 1 )
						AND a.qty IS NOT NULL AND a.cost IS NOT NULL AND t.transtype IN( 1, 4, 18 ) AND i.status IN ( 3, 18 )
						AND a.tagged <> 'X' AND p.productstatus = 'Y' AND isnull(t.PeriodFlag,'N')='N' 
				GROUP  BY t.TransDate,  b.CategoryID, d.DepartmentID, a.producttype 
				ORDER  BY 1 

		-- AppointmentReport

		insert into CentralExportAppointments(ApptID, ScheduleTime, ServiceTime, ApptRemarks, ApptStatus, InvoiceNo, InvoiceDate, CreatedOn, CustomerID, EmployeeID)
				select a.ID, ScheduleTime, ServiceTime, ApptRemarks, ApptStatus, InvoiceNo, InvoiceDate, a.CreatedOn ,c.CustomerID, e.EmployeeID
				from appointments a 
				left outer join customer c on c.ID = a.CustomerID  
				left outer join employee e on e.ID = a.EmployeeID 
				where a.ApptStatus = 'New' and isnull(a.ExpFlag,'N')='N'
			order by a.ScheduleTime

		-- Discount Report
		insert into CentralExportDiscounts(Producttype, InvoiceID, TDiscountItem, TDiscountInvoice, TransDate, FilterDate, SKU,Description, DiscountText,Discount)
				 select a.Producttype as PT,i.ID,i.Discount as TDiscountItem, i.Coupon as TDiscountInvoice, t.TransDate,
				 convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate,a.SKU,a.Description, a.DiscountText,a.Discount
				 from Trans t 
				 left outer join Invoice i on t.ID = i.TransactionNo  
				 left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID  
				 where (1 = 1) and a.Discount is not null and a.Discount <> 0 and t.TransType in( 1,4,18 ) 
				 and i.status in (3,18) and a.Tagged <> 'X' and t.DiscountFlag='N' 
				 Order by FilterDate,i.ID, a.Description 





		insert into CentralExportLayAwayCustomer (CustID,TransDate,InvoiceNo,LayawayNo,SKU,Description,Qty,TotalSale,DateDue,Status,TransactionNo,Payment,Name)
				select c.CustomerID as CustID, lp.CreatedOn as TransDate, i.ID as InvoiceNo,i.LayawayNo,it.SKU, it.Description, it.Qty, i.TotalSale, l.DateDue, i.Status,
				lp.TransactionNo, lp.Payment, tt.name 
				from trans t 
				left outer join invoice i on i.transactionno = t.ID  
				left outer join item it on it.invoiceno = i.ID  
				left outer join layaway l on i.LayawayNo = l.ID  
				left outer join customer c on i.CustomerID = c.ID  
				left outer join laypmts lp on i.ID=lp.InvoiceNo
				left outer join tender tn on lp.TransactionNo = tn.transactionno  
				left outer join tendertypes tt on tt.id = tn.tendertype 
				where i.LayawayNo > 0 and it.Tagged <> 'X' and i.Status in (1,3) and isnull(t.LayAwayCustomerFlag,'N')='N'
				order by c.CustomerID, i.LayawayNo, i.ID 


		insert into CentralExportLayAwayProduct (CustID,SKU,Description,Qty,Dept,PriceA, QtyOnHand, QtyOnLayaway, AdjQty)
				select c.CustomerID as CustID, it.SKU,it.Description,isnull(sum(it.Qty),0) as Qty,d.Description as Dept,  
				p.PriceA,p.QtyOnHand,p.QtyOnLayaway,p.QtyOnHand - p.QtyOnLayaway as AdjQty 
				--c.FirstName + ' ' + c.LastName as Customer,c.Email, 
				from trans t 
				left outer join invoice i on i.transactionno = t.ID 
				left outer join item it on it.invoiceno = i.ID  
				left outer join product p on it.ProductID = p.ID  
				left outer join customer c on c.ID = i.CustomerID  
				left outer join dept d on d.ID = p.DepartmentID 
				where i.LayawayNo > 0 and i.Status in (1) and it.Tagged <> 'X' and isnull(t.LayAwayProductFlag,'N')='N'
				group by c.CustomerID,c.FirstName,c.LastName,c.Email,it.SKU, it.Description, d.Description, p.PriceA, p.QtyOnHand, p.QtyOnLayaway 
				order by it.SKU, c.CustomerID 
				 

		insert into CentralExportTaxAnalysis (ProductID, SKU, Description, Qty, Price, PriceI, Cost, Discount, Tax, TransDate)
				select a.ProductID, a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price*a.Qty,0) as Price, isnull(a.TaxIncludePrice,0) as PriceI,
				isnull(a.Cost * a.Qty,0) as Cost,  isnull(a.Discount,0) as Discount, isnull(TaxTotal1 + TaxTotal2 + TaxTotal3,0) as Tax, t.TransDate
				from Trans t
				left outer join Invoice i on t.ID = i.TransactionNo left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID
				left outer join  Product p on a.ProductID = p.ID  left outer join  BrandMaster br on p.BrandID = br.ID
				where (1 = 1) and a.Qty is not null and a.Cost is not null  and t.TransType in (1,4,18) and i.status in (3,18)
				and a.Tagged <> 'X' and a.Producttype in ('P','K','U','M','S','W','E','F','T','B') and isnull(t.TaxAnalysisFlag,'N')='N'
				Order by a.Description 

		insert into CentralExportPackingList (SKU, Description, SoldQty, IssueStore, CustomerID, TransDate)
				 select a.SKU,a.Description,sum(a.Qty) as SoldQty, c.IssueStore , c.CustomerID, t.TransDate
				 from Trans t 
				 left outer join invoice i on t.ID = i.TransactionNo  
				 left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID and a.Producttype in  ('P','K','U','M','S','W','E','F','T') 
				 left outer join  customer as c on c.ID = t.CustomerID  
				 where  t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' and isnull(t.PackingListFlag,'N')='N'
				 group by a.SKU,a.Description,t.CustomerID, c.CustomerID, t.TransDate, c.IssueStore
				 order by a.SKU,a.Description

		--	A bit of lengthy logic, i.e. moved to separate SP
		exec sp_CentralExportTransactionJournal



		insert into CentralExportStockValuation (SKU, Description, Qty, Cost)
				select SKU, Description, isnull(QtyOnHand,0) qty, isnull(Cost,0) cost from product where StockValuationFlag<>'Y' order by SKU


		insert into CentralExportOrderReceiving (RecvDetailID, RecvHeaderID, BatchID, PurchaseOrder, InvoiceNo, InvoiceTotal, GrossAmount, Tax, Freight,
		DateOrdered, ReceiveDate, DateTimeStamp, Note, VendorID, PriceA, DQty, DCost, DFreight, DTax, ProductName, VendorPartNo, 
		CheckClerkID, CheckClerk, RecvClerkID, RecvClerk)
				select d.ID as RecvDetailID, a.ID as RecvHeaderID, a.BatchID, a.PurchaseOrder, a.InvoiceNo, a.InvoiceTotal, a.GrossAmount, a.Tax, a.Freight, 
				isnull (a.DateOrdered, '1990-01-01 00:00:00.000') DateOrdered, a.ReceiveDate, a.DateTimeStamp, a.Note, v.VendorID, isnull(p.PriceA,0) as PriceA,  
				d.Qty as DQty, d.Cost as DCost, d.Freight as DFreight, d.Tax as DTax, d.Description as ProductName, d.VendorPartNo, 
				e.EmployeeId as CheckClerkID,  e.FirstName + ' ' + e.LastName as CheckClerk, f.EmployeeId RecvClerkID,  f.FirstName + ' ' + f.LastName as RecvClerk  
				from RecvHeader a left outer join Vendor v on a.VendorID = v.ID  
				left outer join RecvDetail d on a.ID = d.BatchNo left outer join Product p on p.ID = d.ProductID  
				left outer join Employee e on a.CheckInClerk = e.ID left outer join Employee f on a.ReceivingClerk = f.ID  
				where isnull (d.ExpFlag, 'N')='N'

	end


end


GO

GO

--
-- Commit Transaction
--
IF @@TRANCOUNT>0 COMMIT TRANSACTION
GO

--
-- Set NOEXEC to off
--
SET NOEXEC OFF
GO


IF not EXISTS
(
    SELECT *  from  SecurityPermission where SecurityCode = '21f' 
)
    BEGIN
       Insert into SecurityPermission (SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc ,CreatedOn) Values(
	   (select max(SlNo)+1 from SecurityPermission) , 21,'CloseOut','21f','Access Cash InOut',Getdate()	
	   )
END;
