 

--
-- Start Transaction
--
BEGIN TRANSACTION
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
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
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
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
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
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Create column [DiscountFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [DiscountFlag] [char](1) NULL DEFAULT ('N')
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Create column [LayAwayCustomerFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [LayAwayCustomerFlag] [char](1) NULL DEFAULT ('N')
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Create column [LayAwayProductFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [LayAwayProductFlag] [char](1) NULL DEFAULT ('N')
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
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
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
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
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
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
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Alter procedure [dbo].[sp_CentralExport_V1]
--
GO
--  [sp_CentralExport_V1] 'all'
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


	if @ExportType = 'ALL' begin  
		-- inserting CentralExportPOHeader
		insert into CentralExportPOHeader (ID,OrderNo,OrderDate,ExpectedDeliveryDate,VendorName,  
		GrossAmount, Freight, Tax, NetAmount,Priority , SupplierInstructions, GeneralNotes, VendorMinOrderAmount  ) 
 
				select a.ID,OrderNo,OrderDate,ExpectedDeliveryDate,b.Name as VendorName,  
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
				and t.ExpFlag='N'
				--and t.TransDate between '1/6/2020 12:00:01 AM' and '9/29/2021 11:59:59 PM'  
				group by t.TransType, i.status, t.TransDate,t.StoreID,
				t.CustomerID,c.CustomerID,c.Company,c.LastName + ', '+c.FirstName,c.Address1, c.City,  c.State, c.Zip,c.WorkPhone,
				c.Email,c.DateLastPurchase, c.AmountLastPurchase,gr.Description,cl.Description order by c.CustomerID 


				-- inserting Notes
				insert into CentralExportNotes (NotesID, RefID, CustomerID, RefType, Note, DateTime, SpecialEvent, ScanFile, DocumentFile)
				select n.ID, RefID, c.CustomerID, RefType, Note, DateTime, SpecialEvent, ScanFile, DocumentFile 
				from Notes n
				join Customer c on c.id=n.RefID
				where n.ExpFlag='N' and SpecialEvent='Y'


				
				-- inserting CardAuthorisation
				insert into CentralExportCardAuthorisation (CardAuthorisationID, CardType, SaleTranNo, CancelTranNo, InvoiceNo, InvoiceAmount, SaleOn, SaleBy, 
				CancelOn, CancelBy, BatchFlag, TipAmount, AuthorisedTranNo, CompleteTranNo, AuthorisedOn, AuthorisedBy, CompleteOn, CompleteBy, EmployeeID)

				
				 select c.ID, CardType, SaleTranNo, CancelTranNo, InvoiceNo, InvoiceAmount, SaleOn, SaleBy, CancelOn, CancelBy, BatchFlag, 
				 TipAmount, AuthorisedTranNo, CompleteTranNo, AuthorisedOn, AuthorisedBy, CompleteOn, CompleteBy, e.EmployeeID
				 from CardAuthorisation  c
				 left outer join employee e on c.SaleBy = e.ID 
				 where c.expflag='N'


				 insert into CenteralExportSuspndedOrderReport (SuspendedID, InvoiceNo, Amount, DateTimeSuspended, CustomerID)
					select s.ID as SuspendedID, i.ID as InvoiceNo,i.TotalSale as Amount,s.DateTimeSuspended,  isnull (c.CustomerID,0)CustomerID
					from suspnded as s 
					left outer join Invoice as i on i.ID = s.InvoiceNo 
					left outer join  Customer as c on c.ID = s.CustomerID 
					where (i.Status = 2) and s.expflag='N'
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
				 and t.ExpFlag='N'
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
				from CloseOut where ExpFlag='N'



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
							AND a.tagged <> 'X' AND p.productstatus = 'Y' AND t.PeriodFlag='N' 
					GROUP  BY t.TransDate,  b.CategoryID, d.DepartmentID, a.producttype 
					ORDER  BY 1 

		-- AppointmentReport

		insert into CentralExportAppointments(ApptID, ScheduleTime, ServiceTime, ApptRemarks, ApptStatus, InvoiceNo, InvoiceDate, CreatedOn, CustomerID, EmployeeID)
				select a.ID, ScheduleTime, ServiceTime, ApptRemarks, ApptStatus, InvoiceNo, InvoiceDate, a.CreatedOn ,c.CustomerID, e.EmployeeID
				from appointments a 
				left outer join customer c on c.ID = a.CustomerID  
				left outer join employee e on e.ID = a.EmployeeID 
				where a.ApptStatus = 'New' and a.ExpFlag='N'
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
				select c.CustomerID as CustID,t.TransDate, i.ID as InvoiceNo,i.LayawayNo,it.SKU, it.Description, it.Qty, i.TotalSale, l.DateDue, i.Status,
				lp.TransactionNo, lp.Payment, tt.name 
				from trans t 
				left outer join invoice i on i.transactionno = t.ID  
				left outer join item it on it.invoiceno = i.ID  
				left outer join layaway l on i.LayawayNo = l.ID  
				left outer join customer c on i.CustomerID = c.ID  
				left outer join laypmts lp on i.ID=lp.InvoiceNo
				left outer join tender tn on lp.TransactionNo = tn.transactionno  
				left outer join tendertypes tt on tt.id = tn.tendertype 
				where i.LayawayNo > 0 and it.Tagged <> 'X' and t.LayAwayCustomerFlag='N' and i.Status in (1,3)
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
				where i.LayawayNo > 0 and i.Status in (1) and it.Tagged <> 'X' and t.LayAwayProductFlag='N'
				group by c.CustomerID,c.FirstName,c.LastName,c.Email,it.SKU, it.Description, d.Description, p.PriceA, p.QtyOnHand, p.QtyOnLayaway 
				order by it.SKU, c.CustomerID 


	end


end


GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
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