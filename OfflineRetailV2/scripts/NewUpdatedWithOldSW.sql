
 
--
-- Create column [PeriodFlag] on table [dbo].[Trans]
--
ALTER TABLE [dbo].[Trans]
  ADD [PeriodFlag] [char](1) NULL CONSTRAINT [DF_Trans_PerioFlag] DEFAULT ('N')
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

--
-- Set transaction isolation level
--
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

--
-- Start Transaction
--
BEGIN TRANSACTION
GO

--
-- Create table [dbo].[UserCustomization]
--
CREATE TABLE [dbo].[UserCustomization] (
  [ID] [int] IDENTITY,
  [UserID] [int] NULL,
  [POSFunctionButtonShowHideState] [char](1) NULL,
  CONSTRAINT [PK_UserCustomization] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
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
-- Create table [dbo].[CentralExportSalesByPeriodReport]
--
CREATE TABLE [dbo].[CentralExportSalesByPeriodReport] (
  [id] [int] IDENTITY,
  [syncId] [int] NOT NULL,
  [PT] [char](5) NULL,
  [Price] [decimal](18, 3) NULL,
  [price1] [decimal](18, 3) NULL,
  [Cost] [decimal](18, 3) NULL,
  [TransDate] [datetime] NULL,
  [CategoryID] [nvarchar](10) NULL,
  [DepartmentID] [nvarchar](10) NULL,
  CONSTRAINT [PK__CentralE__3213E83F70C9844A] PRIMARY KEY CLUSTERED ([id])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportPOHeader]
--
CREATE TABLE [dbo].[CentralExportPOHeader] (
  [ID] [int] NULL,
  [OrderNo] [varchar](16) NULL,
  [OrderDate] [datetime] NULL,
  [VendorName] [varchar](50) NULL,
  [ExpectedDeliveryDate] [datetime] NULL,
  [Priority] [varchar](10) NULL,
  [GrossAmount] [numeric](15, 4) NULL,
  [Freight] [numeric](15, 4) NULL,
  [Tax] [numeric](15, 4) NULL,
  [NetAmount] [numeric](15, 4) NULL,
  [SupplierInstructions] [varchar](250) NULL,
  [GeneralNotes] [varchar](250) NULL,
  [VendorMinOrderAmount] [numeric](15, 3) NULL,
  [VendorID] [varchar](10) NULL
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
-- Create table [dbo].[CentralExportNotes]
--
CREATE TABLE [dbo].[CentralExportNotes] (
  [ID] [int] IDENTITY,
  [NotesID] [int] NULL,
  [RefID] [int] NULL,
  [RefType] [varchar](10) NULL,
  [Note] [varchar](150) NULL,
  [DateTime] [datetime] NULL,
  [SpecialEvent] [char](1) NULL,
  [ScanFile] [varchar](50) NULL,
  [DocumentFile] [varchar](50) NULL,
  [CustomerID] [nvarchar](20) NULL
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
-- Create table [dbo].[CentralExportEmployeeOtherReports]
--
CREATE TABLE [dbo].[CentralExportEmployeeOtherReports] (
  [ID] [int] IDENTITY,
  [TransType] [smallint] NULL DEFAULT (0),
  [Status] [int] NULL,
  [TransDate] [datetime] NULL,
  [CID] [int] NULL DEFAULT (0),
  [TS] [numeric](15, 3) NULL DEFAULT (0),
  [CustID] [nvarchar](20) NULL,
  [Company] [nvarchar](30) NULL,
  [Customer] [nvarchar](40) NULL,
  [Address1] [nvarchar](60) NULL,
  [City] [nvarchar](20) NULL,
  [State] [nvarchar](2) NULL,
  [Zip] [nvarchar](12) NULL,
  [WorkPhone] [nvarchar](14) NULL,
  [EMail] [nvarchar](60) NULL,
  [DateLastPurchase] [datetime] NULL,
  [AmountLastPurchase] [numeric](15, 3) NULL DEFAULT (0),
  [CustGroup] [nvarchar](30) NULL,
  [CustClass] [nvarchar](30) NULL,
  [ExpFlag] [char](1) NULL DEFAULT ('N'),
  [StoreID] [int] NULL DEFAULT (0),
  CONSTRAINT [PK_Customer_Other_Reports] PRIMARY KEY CLUSTERED ([ID])
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
-- Create table [dbo].[CentralExportCloseOutSalesHour]
--
CREATE TABLE [dbo].[CentralExportCloseOutSalesHour] (
  [ID] [int] IDENTITY,
  [Timeinterval] [nvarchar](25) NULL,
  [SalesAmount] [numeric](18, 3) NULL DEFAULT (0),
  [CloseoutID] [int] NULL DEFAULT (0),
  [StartDateTime] [datetime] NULL,
  [EndDateTime] [datetime] NULL,
  [CloseoutType] [char](11) NULL,
  [Notes] [varchar](100) NULL,
  [NoOfSales] [int] NULL DEFAULT (0),
  [EmpID] [nvarchar](12) NULL,
  [TerminalName] [nvarchar](50) NULL,
  [ReportTerminalName] [nvarchar](50) NULL,
  CONSTRAINT [PK_CentralExportCloseOutSalesHour] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportCloseOutSalesDept]
--
CREATE TABLE [dbo].[CentralExportCloseOutSalesDept] (
  [ID] [int] IDENTITY,
  [DeptID] [nvarchar](10) NULL,
  [DeptDesc] [nvarchar](30) NULL,
  [SalesAmount] [numeric](18, 3) NULL DEFAULT (0),
  [CloseoutID] [int] NULL DEFAULT (0),
  [StartDateTime] [datetime] NULL,
  [EndDateTime] [datetime] NULL,
  [CloseoutType] [char](11) NULL,
  [Notes] [nvarchar](100) NULL,
  [NoOfSales] [int] NULL DEFAULT (0),
  [EmpID] [nvarchar](12) NULL,
  [TerminalName] [nvarchar](50) NULL,
  [ReportTerminalName] [nvarchar](50) NULL,
  CONSTRAINT [PK_CentralExportCloseOutSalesDept] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportCloseOutReturn]
--
CREATE TABLE [dbo].[CentralExportCloseOutReturn] (
  [ReturnSKU] [nvarchar](16) NULL,
  [ReturnInvoiceNo] [int] NULL,
  [ReturnAmount] [numeric](18, 3) NULL DEFAULT (0),
  [ReportTerminalName] [nvarchar](50) NULL,
  [CloseoutID] [int] NULL DEFAULT (0)
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportCloseOutReportTender]
--
CREATE TABLE [dbo].[CentralExportCloseOutReportTender] (
  [ID] [int] IDENTITY,
  [RecordType] [char](1) NULL,
  [CloseOutID] [int] NULL,
  [TenderID] [int] NULL,
  [TenderName] [nvarchar](40) NULL,
  [TenderAmount] [numeric](18, 3) NULL DEFAULT (0),
  [TenderCount] [int] NULL DEFAULT (0),
  [ReportTerminalName] [nvarchar](50) NULL,
  CONSTRAINT [PK_CentralExportCloseOutReportTender] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportCloseOutReportMain]
--
CREATE TABLE [dbo].[CentralExportCloseOutReportMain] (
  [TaxedSales] [numeric](18, 3) NULL DEFAULT (0),
  [NonTaxedSales] [numeric](18, 3) NULL DEFAULT (0),
  [Tax1Exist] [char](1) NULL DEFAULT ('N'),
  [Tax1Name] [nvarchar](20) NULL,
  [Tax1Amount] [numeric](18, 3) NULL DEFAULT (0),
  [Tax2Exist] [char](1) NULL DEFAULT ('N'),
  [Tax2Name] [nvarchar](20) NULL,
  [Tax2Amount] [numeric](18, 3) NULL DEFAULT (0),
  [Tax3Exist] [char](1) NULL DEFAULT ('N'),
  [Tax3Name] [nvarchar](20) NULL,
  [Tax3Amount] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceSales] [numeric](18, 3) NULL DEFAULT (0),
  [ProductSales] [numeric](18, 3) NULL DEFAULT (0),
  [OtherSales] [numeric](18, 3) NULL DEFAULT (0),
  [DiscountItemNo] [int] NULL,
  [DiscountItemAmount] [numeric](18, 3) NULL DEFAULT (0),
  [DiscountInvoiceNo] [int] NULL,
  [DiscountInvoiceAmount] [numeric](18, 3) NULL DEFAULT (0),
  [LayawayDeposits] [numeric](18, 3) NULL DEFAULT (0),
  [LayawayRefund] [numeric](18, 3) NULL DEFAULT (0),
  [LayawayPayment] [numeric](18, 3) NULL DEFAULT (0),
  [LayawaySalesPosted] [numeric](18, 3) NULL DEFAULT (0),
  [PaidOuts] [numeric](18, 3) NULL DEFAULT (0),
  [GCsold] [numeric](18, 3) NULL DEFAULT (0),
  [SCissued] [numeric](18, 3) NULL DEFAULT (0),
  [SCredeemed] [numeric](18, 3) NULL DEFAULT (0),
  [HACharged] [numeric](18, 3) NULL DEFAULT (0),
  [HApayments] [numeric](18, 3) NULL DEFAULT (0),
  [NoOfSales] [int] NULL DEFAULT (0),
  [CloseoutID] [int] NULL DEFAULT (0),
  [StartDateTime] [datetime] NULL,
  [EndDateTime] [datetime] NULL,
  [CloseoutType] [char](11) NULL,
  [Notes] [nvarchar](100) NULL,
  [EmpID] [nvarchar](12) NULL,
  [TerminalName] [nvarchar](50) NULL,
  [ReportTerminalName] [nvarchar](50) NULL,
  [TotalSales_PreTax] [numeric](18, 3) NULL DEFAULT (0),
  [CostOfGoods] [numeric](18, 3) NULL DEFAULT (0),
  [NoSaleCount] [int] NULL DEFAULT (0),
  [RentSales] [numeric](18, 3) NULL DEFAULT (0),
  [RentDeposit] [numeric](18, 3) NULL DEFAULT (0),
  [RentDepositReturned] [numeric](18, 3) NULL DEFAULT (0),
  [RentTax1Amount] [numeric](18, 3) NULL DEFAULT (0),
  [RentTax2Amount] [numeric](18, 3) NULL DEFAULT (0),
  [RentTax3Amount] [numeric](18, 3) NULL DEFAULT (0),
  [RentDiscountInvoiceNo] [int] NULL DEFAULT (0),
  [RentDiscountInvoiceAmount] [numeric](18, 3) NULL DEFAULT (0),
  [RepairSales] [numeric](18, 3) NULL DEFAULT (0),
  [RepairTax1Amount] [numeric](18, 3) NULL DEFAULT (0),
  [RepairTax2Amount] [numeric](18, 3) NULL DEFAULT (0),
  [RepairTax3Amount] [numeric](18, 3) NULL DEFAULT (0),
  [RepairDiscountItemNo] [int] NULL DEFAULT (0),
  [RepairDiscountItemAmount] [numeric](18, 3) NULL DEFAULT (0),
  [RepairDiscountInvoiceNo] [int] NULL DEFAULT (0),
  [RepairDiscountInvoiceAmount] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceTax1Amount] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceTax2Amount] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceTax3Amount] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceDiscountItemNo] [int] NULL DEFAULT (0),
  [ServiceDiscountItemAmount] [numeric](18, 3) NULL DEFAULT (0),
  [OtherTax1Amount] [numeric](18, 3) NULL DEFAULT (0),
  [OtherTax2Amount] [numeric](18, 3) NULL DEFAULT (0),
  [OtherTax3Amount] [numeric](18, 3) NULL DEFAULT (0),
  [OtherDiscountItemNo] [int] NULL DEFAULT (0),
  [OtherDiscountItemAmount] [numeric](18, 3) NULL DEFAULT (0),
  [SalesInvoiceCount] [int] NULL DEFAULT (0),
  [RentInvoiceCount] [int] NULL DEFAULT (0),
  [RepairInvoiceCount] [int] NULL DEFAULT (0),
  [ProductTx] [numeric](18, 3) NULL DEFAULT (0),
  [ProductNTx] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceTx] [numeric](18, 3) NULL DEFAULT (0),
  [ServiceNTx] [numeric](18, 3) NULL DEFAULT (0),
  [OtherTx] [numeric](18, 3) NULL DEFAULT (0),
  [OtherNTx] [numeric](18, 3) NULL DEFAULT (0),
  [CashTip] [numeric](15, 3) NULL DEFAULT (0),
  [CCTip] [numeric](15, 3) NULL DEFAULT (0),
  [SalesFees] [numeric](15, 3) NULL DEFAULT (0),
  [SalesFeesTax] [numeric](15, 3) NULL DEFAULT (0),
  [RentFees] [numeric](15, 3) NULL DEFAULT (0),
  [RentFeesTax] [numeric](15, 3) NULL DEFAULT (0),
  [RepairFees] [numeric](15, 3) NULL DEFAULT (0),
  [RepairFeesTax] [numeric](15, 3) NULL DEFAULT (0),
  [DTax] [numeric](15, 3) NULL DEFAULT (0),
  [MGCsold] [numeric](15, 3) NULL DEFAULT (0),
  [BottleRefund] [numeric](15, 3) NULL DEFAULT (0),
  [PGCsold] [numeric](15, 3) NULL DEFAULT (0),
  [RepairDeposit] [numeric](15, 3) NULL DEFAULT (0),
  [DGCsold] [numeric](15, 3) NULL DEFAULT (0),
  [PLGCsold] [numeric](15, 3) NULL DEFAULT (0),
  [Free_Qty] [int] NULL DEFAULT (0),
  [Free_Amount] [numeric](15, 3) NULL DEFAULT (0),
  [LottoPayout] [numeric](15, 3) NULL DEFAULT (0)
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportCloseOut]
--
CREATE TABLE [dbo].[CentralExportCloseOut] (
  [ID] [int] IDENTITY,
  [CloseOutID] [int] NULL,
  [CloseoutType] [char](1) NULL,
  [ConsolidatedID] [int] NULL,
  [StartDatetime] [datetime] NULL,
  [EndDatetime] [datetime] NULL,
  [Notes] [nvarchar](100) NULL,
  [TransactionCnt] [int] NULL,
  [TerminalName] [nvarchar](50) NULL,
  [EmployeeID] [varchar](12) NULL,
  CONSTRAINT [PK_CentralExportCloseOut] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportCardAuthorisation]
--
CREATE TABLE [dbo].[CentralExportCardAuthorisation] (
  [ID] [int] IDENTITY,
  [CardAuthorisationID] [int] NULL,
  [CardType] [varchar](50) NULL,
  [SaleTranNo] [varchar](50) NULL,
  [CancelTranNo] [varchar](50) NULL,
  [InvoiceNo] [int] NULL,
  [InvoiceAmount] [numeric](15, 3) NULL DEFAULT (0),
  [SaleOn] [datetime] NULL,
  [SaleBy] [int] NULL,
  [CancelOn] [datetime] NULL,
  [CancelBy] [int] NULL,
  [BatchFlag] [char](1) NULL DEFAULT ('N'),
  [TipAmount] [numeric](15, 3) NULL DEFAULT (0),
  [AuthorisedTranNo] [varchar](50) NULL,
  [CompleteTranNo] [varchar](50) NULL,
  [AuthorisedOn] [datetime] NULL,
  [AuthorisedBy] [int] NULL,
  [CompleteOn] [datetime] NULL,
  [CompleteBy] [int] NULL,
  [EmployeeID] [varchar](12) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CentralExportAppointments]
--
CREATE TABLE [dbo].[CentralExportAppointments] (
  [ID] [int] IDENTITY,
  [ApptID] [int] NULL,
  [ScheduleTime] [datetime] NULL,
  [ServiceTime] [int] NULL,
  [ApptStatus] [varchar](10) NULL,
  [InvoiceNo] [int] NULL,
  [InvoiceDate] [datetime] NULL,
  [CreatedOn] [datetime] NULL,
  [CustomerID] [varchar](20) NULL,
  [EmployeeID] [varchar](20) NULL,
  [ApptRemarks] [nvarchar](200) NULL
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CenteralExportSuspndedOrderReport]
--
CREATE TABLE [dbo].[CenteralExportSuspndedOrderReport] (
  [ID] [int] IDENTITY,
  [SuspendedID] [int] NULL,
  [InvoiceNo] [int] NULL,
  [Amount] [numeric](15, 3) NULL,
  [DateTimeSuspended] [datetime] NULL,
  [CustomerID] [nvarchar](20) NULL,
  CONSTRAINT [PK_CenteralExportSuspndedOrderReport] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[CenteralExportSalesMatrixMainReport]
--
CREATE TABLE [dbo].[CenteralExportSalesMatrixMainReport] (
  [ID] [int] IDENTITY,
  [TransDate] [datetime] NULL,
  [ProductStatus] [char](1) NULL,
  [ProductType] [char](1) NULL,
  [SKU] [nvarchar](16) NULL,
  [Description] [nvarchar](200) NULL,
  [Qty] [numeric](15, 3) NULL,
  [Price] [numeric](15, 3) NULL,
  [Cost] [numeric](15, 3) NULL,
  [Discount] [numeric](15, 3) NOT NULL,
  [QtyOnHandVendor] [numeric](15, 3) NULL,
  [PCostVendor] [numeric](15, 3) NULL,
  [CustomerID] [nvarchar](20) NULL,
  [FilterIDSKU] [nvarchar](20) NULL,
  [FilterDescSKU] [nvarchar](30) NULL,
  [FilterIDDept] [nvarchar](20) NULL,
  [FilterDescDept] [nvarchar](30) NULL,
  [FilterIDCat] [nvarchar](10) NULL,
  [FilterDescCat] [nvarchar](30) NULL,
  [FilterIDBrand] [nvarchar](10) NULL,
  [FilterDescBrand] [nvarchar](30) NULL,
  [FilterIDEmp] [varchar](12) NULL,
  [FilterDescEmp] [nvarchar](40) NULL,
  [UPC] [varchar](20) NULL,
  [Season] [varchar](20) NULL,
  [Brand] [nvarchar](30) NULL,
  [VendorID] [varchar](10) NULL,
  [Vendor] [varchar](30) NULL,
  [PartNumber] [varchar](16) NULL,
  [SyncID] [varchar](50) NULL,
  [TransCustomerID] [int] NULL,
  [CustomerName] [nvarchar](40) NULL,
  [TaxIncludeRate] [numeric](15, 3) NULL,
  CONSTRAINT [PK_CenteralExportSalesMatrixMainReport] PRIMARY KEY CLUSTERED ([ID])
)
ON [PRIMARY]
GO

GO

--
-- Create table [dbo].[AppointmentReport]
--
CREATE TABLE [dbo].[AppointmentReport] (
  [Id] [int] IDENTITY,
  [ApptStart] [datetime] NULL,
  [ApptEnd] [datetime] NULL,
  [CustomerID] [int] NULL,
  [Client] [nvarchar](250) NULL,
  [EmployeeID] [int] NULL,
  [Staff] [nvarchar](250) NULL,
  [ShortName] [nvarchar](100) NULL,
  [ApptStatus] [nvarchar](10) NULL,
  [BookingDate] [datetime] NULL,
  [ApptRemarks] [nvarchar](400) NULL,
  [SyncID] [int] NULL,
  [ExpFlag] [varchar](1) NULL,
  PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

GO

--
-- Create column [createdon] on table [dbo].[XeConnectTransactions]
--
ALTER TABLE [dbo].[XeConnectTransactions]
  ADD [createdon] [datetime] NULL
GO

GO

--
-- Create column [TransType] on table [dbo].[XeConnectTransactions]
--
ALTER TABLE [dbo].[XeConnectTransactions]
  ADD [TransType] [varchar](10) NULL
GO

GO

--
-- Create column [LayAwayNo] on table [dbo].[XeConnectTransactions]
--
ALTER TABLE [dbo].[XeConnectTransactions]
  ADD [LayAwayNo] [int] NULL
GO

GO

--
-- Create column [LayAwayInvoiceNo] on table [dbo].[XeConnectTransactions]
--
ALTER TABLE [dbo].[XeConnectTransactions]
  ADD [LayAwayInvoiceNo] [int] NULL
GO

GO

--
-- Drop index [IX_Trans_CloseoutID] from table [dbo].[Trans]
--
DROP INDEX [IX_Trans_CloseoutID] ON [dbo].[Trans]
GO

GO

--
-- Drop index [IX_Trans_CustomerID] from table [dbo].[Trans]
--
DROP INDEX [IX_Trans_CustomerID] ON [dbo].[Trans]
GO

GO

--
-- Drop index [IX_Trans_EmployeeID] from table [dbo].[Trans]
--
DROP INDEX [IX_Trans_EmployeeID] ON [dbo].[Trans]
GO

GO

--
-- Drop index [IX_Trans_ExpFlag] from table [dbo].[Trans]
--
DROP INDEX [IX_Trans_ExpFlag] ON [dbo].[Trans]
GO

GO

--
-- Drop index [IX_Trans_TransDate] from table [dbo].[Trans]
--
DROP INDEX [IX_Trans_TransDate] ON [dbo].[Trans]
GO

GO

--
-- Drop index [IX_Trans_TransType] from table [dbo].[Trans]
--
DROP INDEX [IX_Trans_TransType] ON [dbo].[Trans]
GO

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
-- Create procedure [dbo].[sp_co_imp_updttag_sales_by_period_export]
--
GO
create procedure [dbo].[sp_co_imp_updttag_sales_by_period_export]
as

begin

  update trans set PeriodFlag='Y' where PeriodFlag = 'N';

end

GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_salebyperiod]
--
GO
create procedure [dbo].[sp_co_imp_updttag_salebyperiod]
			@ID	int
as

begin

 update Trans set PeriodFlag='Y' where PeriodFlag = 'N' and ID = @ID;

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
-- Drop index [IX_Tender_TenderAmount] from table [dbo].[Tender]
--
DROP INDEX [IX_Tender_TenderAmount] ON [dbo].[Tender]
GO

GO

--
-- Drop index [IX_Tender_TenderType] from table [dbo].[Tender]
--
DROP INDEX [IX_Tender_TenderType] ON [dbo].[Tender]
GO

GO

--
-- Drop index [IX_Tender_TransactionNo] from table [dbo].[Tender]
--
DROP INDEX [IX_Tender_TransactionNo] ON [dbo].[Tender]
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
-- Drop index [IX_TaxMapping_MappingID] from table [dbo].[TaxMapping]
--
DROP INDEX [IX_TaxMapping_MappingID] ON [dbo].[TaxMapping]
GO

GO

--
-- Drop index [IX_TaxMapping_MappingType] from table [dbo].[TaxMapping]
--
DROP INDEX [IX_TaxMapping_MappingType] ON [dbo].[TaxMapping]
GO

GO

--
-- Drop index [IX_TaxMapping_TaxID] from table [dbo].[TaxMapping]
--
DROP INDEX [IX_TaxMapping_TaxID] ON [dbo].[TaxMapping]
GO

GO

--
-- Create column [BookerID] on table [dbo].[TaxHeader]
--
ALTER TABLE [dbo].[TaxHeader]
  ADD [BookerID] [varchar](20) NULL
GO

GO

--
-- Drop index [IX_Employee_BookingExpFlag] from table [dbo].[Employee]
--
DROP INDEX [IX_Employee_BookingExpFlag] ON [dbo].[Employee]
GO

GO

--
-- Drop index [IX_Employee_ExpFlag] from table [dbo].[Employee]
--
DROP INDEX [IX_Employee_ExpFlag] ON [dbo].[Employee]
GO

GO

--
-- Create column [ForcedPasscode] on table [dbo].[Employee]
--
ALTER TABLE [dbo].[Employee]
  ADD [ForcedPasscode] [char](1) NULL DEFAULT ('Y')
GO

GO

--
-- Create column [ExpFlag] on table [dbo].[Suspnded]
--
ALTER TABLE [dbo].[Suspnded]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_suspended_order]
--
GO
create procedure [dbo].[sp_co_imp_updttag_suspended_order]
			@ID	int
as

begin

 update Suspnded set expflag='Y' where expflag = 'N' and ID = @ID;

end



GO

GO

--
-- Drop index [IX_DiscountTime_DiscountID] from table [dbo].[DiscountTime]
--
DROP INDEX [IX_DiscountTime_DiscountID] ON [dbo].[DiscountTime]
GO

GO

--
-- Drop index [IX_RecvDetail_BatchNo] from table [dbo].[RecvDetail]
--
DROP INDEX [IX_RecvDetail_BatchNo] ON [dbo].[RecvDetail]
GO

GO

--
-- Drop index [IX_RecvDetail_ProductID] from table [dbo].[RecvDetail]
--
DROP INDEX [IX_RecvDetail_ProductID] ON [dbo].[RecvDetail]
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
-- Drop index [IX_DiscountRestrictedItems_DiscountID] from table [dbo].[DiscountRestrictedItems]
--
DROP INDEX [IX_DiscountRestrictedItems_DiscountID] ON [dbo].[DiscountRestrictedItems]
GO

GO

--
-- Drop index [IX_DiscountRestrictedItems_ItemID] from table [dbo].[DiscountRestrictedItems]
--
DROP INDEX [IX_DiscountRestrictedItems_ItemID] ON [dbo].[DiscountRestrictedItems]
GO

GO

--
-- Drop index [IX_DiscountRestrictedItems_ItemType] from table [dbo].[DiscountRestrictedItems]
--
DROP INDEX [IX_DiscountRestrictedItems_ItemType] ON [dbo].[DiscountRestrictedItems]
GO

GO

--
-- Drop index [IX_Recipients_MailID] from table [dbo].[Recipients]
--
DROP INDEX [IX_Recipients_MailID] ON [dbo].[Recipients]
GO

GO

--
-- Drop index [IX_Recipients_RecipientID] from table [dbo].[Recipients]
--
DROP INDEX [IX_Recipients_RecipientID] ON [dbo].[Recipients]
GO

GO

--
-- Drop index [IX_Recipients_SenderID] from table [dbo].[Recipients]
--
DROP INDEX [IX_Recipients_SenderID] ON [dbo].[Recipients]
GO

GO

--
-- Drop index [IX_ProductModifier_ProductID] from table [dbo].[ProductModifier]
--
DROP INDEX [IX_ProductModifier_ProductID] ON [dbo].[ProductModifier]
GO

GO

--
-- Create column [BookerDepttID] on table [dbo].[Dept]
--
ALTER TABLE [dbo].[Dept]
  ADD [BookerDepttID] [varchar](50) NULL DEFAULT ('')
GO

GO

--
-- Create column [BookerDeptID] on table [dbo].[Dept]
--
ALTER TABLE [dbo].[Dept]
  ADD [BookerDeptID] [varchar](50) NULL DEFAULT ('')
GO

GO

--
-- Drop index [IX_Customer_ActiveStatus] from table [dbo].[Customer]
--
DROP INDEX [IX_Customer_ActiveStatus] ON [dbo].[Customer]
GO

GO

--
-- Drop index [IX_Customer_BookingExpFlag] from table [dbo].[Customer]
--
DROP INDEX [IX_Customer_BookingExpFlag] ON [dbo].[Customer]
GO

GO

--
-- Drop index [IX_Customer_CustomerID] from table [dbo].[Customer]
--
DROP INDEX [IX_Customer_CustomerID] ON [dbo].[Customer]
GO

GO

--
-- Drop index [IX_Customer_ExpFlag] from table [dbo].[Customer]
--
DROP INDEX [IX_Customer_ExpFlag] ON [dbo].[Customer]
GO

GO

--
-- Drop index [IX_Customer_IssueStore] from table [dbo].[Customer]
--
DROP INDEX [IX_Customer_IssueStore] ON [dbo].[Customer]
GO

GO

--
-- Drop index [IX_Customer_OperateStore] from table [dbo].[Customer]
--
DROP INDEX [IX_Customer_OperateStore] ON [dbo].[Customer]
GO

GO

--
-- Drop index [IX_Product_BookingExpFlag] from table [dbo].[Product]
--
DROP INDEX [IX_Product_BookingExpFlag] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_CategoryID] from table [dbo].[Product]
--
DROP INDEX [IX_Product_CategoryID] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_DepartmentID] from table [dbo].[Product]
--
DROP INDEX [IX_Product_DepartmentID] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_ExpFlag] from table [dbo].[Product]
--
DROP INDEX [IX_Product_ExpFlag] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_LastChangedOn] from table [dbo].[Product]
--
DROP INDEX [IX_Product_LastChangedOn] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_LinkSKU] from table [dbo].[Product]
--
DROP INDEX [IX_Product_LinkSKU] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_ProductStatus] from table [dbo].[Product]
--
DROP INDEX [IX_Product_ProductStatus] ON [dbo].[Product]
GO

GO

--
-- Drop index [IX_Product_QtyOnHand] from table [dbo].[Product]
--
DROP INDEX [IX_Product_QtyOnHand] ON [dbo].[Product]
GO

GO

--
-- Create column [BookerProductID] on table [dbo].[Product]
--
ALTER TABLE [dbo].[Product]
  ADD [BookerProductID] [varchar](50) NULL DEFAULT ('')
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
-- Create view [dbo].[Product_View_Temp1]
--
GO
CREATE VIEW [dbo].[Product_View_Temp1] AS
select ID, SKU, SKU2, Description, BinLocation, Style, Size, Color, ProductType, PriceA, PriceB, 
PriceC, PromptForPrice, LastCost, Cost, QtyOnHand, QtyOnLayaway, ReorderQty, NormalQty, PrimaryVendorID, DepartmentID, CategoryID, ClassID, PrintBarCode, 
NoPriceOnLabel, QtyToPrint, LabelType, Selected, Aux, FoodStampEligible, MinimumAge, ProductNotes, AddtoPOSScreen, ScaleBarCode, ProductPhoto, Points, 
AllowZeroStock, DisplayStockinPOS, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn, POSBackground, POSScreenColor, POSScreenStyle, POSFontType, 
POSFontSize, POSFontColor, IsBold, IsItalics, ChangedByAdmin, ChangedOnAdmin, DecimalPlace, ProductStatus, SKU3, TaggedInInvoice, BrandID, 
CaseQty, CaseUPC, Season, UPC, LinkSKU, BreakPackRatio, BreakpackFlag, RentalPerMinute, RentalPerHour, RentalPerDay, RentalPerWeek, RentalPerMonth, 
RentalDeposit, MinimumServiceTime, RepairCharge, RentalMinHour, RentalMinAmount, RentalPrompt, RepairPromptForCharge, RepairPromptForTag, ExpFlag, 
Tare, RentalPerHalfDay, AddToScaleScreen, NonDiscountable, ScaleBackground, ScaleScreenColor, ScaleScreenStyle, 
ScaleFontType, ScaleFontSize, ScaleFontColor, ScaleIsBold, ScaleIsItalics, Notes2, Tare2, SplitWeight, UOM
--ExpStoreOption1, ExpStoreOption2, EditFlag, ClientID
from Product
GO

GO

--
-- Drop index [IX_CloseOutSalesHour_CloseoutID] from table [dbo].[CloseOutSalesHour]
--
DROP INDEX [IX_CloseOutSalesHour_CloseoutID] ON [dbo].[CloseOutSalesHour]
GO

GO

--
-- Drop index [IX_CloseOutSalesDept_CloseoutID] from table [dbo].[CloseOutSalesDept]
--
DROP INDEX [IX_CloseOutSalesDept_CloseoutID] ON [dbo].[CloseOutSalesDept]
GO

GO

--
-- Drop index [IX_CloseOutSalesDept_DeptID] from table [dbo].[CloseOutSalesDept]
--
DROP INDEX [IX_CloseOutSalesDept_DeptID] ON [dbo].[CloseOutSalesDept]
GO

GO

--
-- Drop index [IX_StockJournal_EmpID] from table [dbo].[StockJournal]
--
DROP INDEX [IX_StockJournal_EmpID] ON [dbo].[StockJournal]
GO

GO

--
-- Drop index [IX_StockJournal_ProductID] from table [dbo].[StockJournal]
--
DROP INDEX [IX_StockJournal_ProductID] ON [dbo].[StockJournal]
GO

GO

--
-- Drop index [IX_CloseOutReportTender_CloseOutID] from table [dbo].[CloseOutReportTender]
--
DROP INDEX [IX_CloseOutReportTender_CloseOutID] ON [dbo].[CloseOutReportTender]
GO

GO

--
-- Drop index [IX_CloseOutReportTender_RecordType] from table [dbo].[CloseOutReportTender]
--
DROP INDEX [IX_CloseOutReportTender_RecordType] ON [dbo].[CloseOutReportTender]
GO

GO

--
-- Drop index [IX_CloseOutReportTender_TenderID] from table [dbo].[CloseOutReportTender]
--
DROP INDEX [IX_CloseOutReportTender_TenderID] ON [dbo].[CloseOutReportTender]
GO

GO

--
-- Drop index [IX_CloseOutReportMain_CloseOutID] from table [dbo].[CloseOutReportMain]
--
DROP INDEX [IX_CloseOutReportMain_CloseOutID] ON [dbo].[CloseOutReportMain]
GO

GO

--
-- Drop index [IX_PrinterMapping_MappingID] from table [dbo].[PrinterMapping]
--
DROP INDEX [IX_PrinterMapping_MappingID] ON [dbo].[PrinterMapping]
GO

GO

--
-- Drop index [IX_PrinterMapping_PrinterID] from table [dbo].[PrinterMapping]
--
DROP INDEX [IX_PrinterMapping_PrinterID] ON [dbo].[PrinterMapping]
GO

GO

--
-- Alter procedure [dbo].[sp_InsertInitialPOSFunction]
--
GO
ALTER procedure [dbo].[sp_InsertInitialPOSFunction]
		@User			int
			
as

declare @Count		int;
declare @fCount		int;

declare @Count1		int;
declare @fCount1	int;

begin

  begin transaction

  select @Count = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton'
  
  if @Count < 46
  begin


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Down'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Down',1,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Up'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Up',2,@User,getdate(),@User,getdate());
    
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Paid Out'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Paid Out',3,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'No Sale'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','No Sale',4,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Cancel'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Cancel',5,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Layaway'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Layaway',6,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Acct Pay'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Acct Pay',7,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Gift Cert'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Gift Cert',8,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Resume/Suspend'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Resume/Suspend',9,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Return Reprint'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder, CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Return Reprint',10,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Refresh Stock'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Refresh Stock',11,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Select Apps'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Select Apps',12,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Cust. Picture'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Cust. Picture',13,@User,getdate(),@User,getdate());


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Cust. Notes'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Cust. Notes',14,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Emp. Picture'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Emp. Picture',15,@User,getdate(),@User,getdate());


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Product Picture'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Product Picture',16,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Product Notes'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Product Notes',17,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'View Product Price'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','View Product Price',18,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Change Product Price'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Change Product Price',19,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Use Price Level'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Use Price Level',20,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fast Cash'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fast Cash',21,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Gift Cert Balance'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Gift Cert Balance',22,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Invoice Item Notes'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Invoice Item Notes',23,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Work Order'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Work Order',24,@User,getdate(),@User,getdate());

     select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Print Cust. Label'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Print Cust. Label',25,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Print Gift Receipt'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Print Gift Receipt',26,@User,getdate(),@User,getdate());
      
   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Discount Ticket'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Discount Ticket',27,@User,getdate(),@User,getdate());   

   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Book Appt.'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Book Appt.',28,@User,getdate(),@User,getdate());   

   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Recall Appt.'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Recall Appt.',29,@User,getdate(),@User,getdate());   
      
   select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Recall Rent'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Recall Rent',30,@User,getdate(),@User,getdate()); 
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Recall Repair'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Recall Repair',31,@User,getdate(),@User,getdate()); 

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Revert CARD Tran.'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Revert CARD Tran.',32,@User,getdate(),@User,getdate());

    
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Mercury Gift Card'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Mercury Gift Card',33,@User,getdate(),@User,getdate());
 

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fast CC'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fast CC',34,@User,getdate(),@User,getdate());
      

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fees & Charges Item'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fees & Charges Item',35,@User,getdate(),@User,getdate());
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'EBT/ Mercury Gift Card Balance'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','EBT/ Mercury Gift Card Balance',36,@User,getdate(),@User,getdate());   


    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Bottle Refund'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Bottle Refund',37,@User,getdate(),@User,getdate());   
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Discount Item'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Discount Item',38,@User,getdate(),@User,getdate()); 
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Qty (+)'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Qty (+)',39,@User,getdate(),@User,getdate());
      
    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Qty (-)'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Qty (-)',40,@User,getdate(),@User,getdate());    

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Tare'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Tare',41,@User,getdate(),@User,getdate());

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Fees & Charges Ticket'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Fees & Charges Ticket',42,@User,getdate(),@User,getdate());

    select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Points to Store Credit'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Points to Store Credit',43,@User,getdate(),@User,getdate());

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Lotto Payout'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Lotto Payout',44,@User,getdate(),@User,getdate());


	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Paid In'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Paid In',45,@User,getdate(),@User,getdate());

	select @fCount = count(*) from POSFunctionSetup where FunctionType = 'FunctionButton' and FunctionName = 'Safe Drop'
    if @fCount = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('FunctionButton','Safe Drop',46,@User,getdate(),@User,getdate());

  end

  select @Count1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' 
  if @Count1 < 4 begin
    select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Cancel'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Cancel',1,@User,getdate(),@User,getdate());

    select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Ingredients'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Ingredients',2,@User,getdate(),@User,getdate());

    select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Recipe'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Recipe',3,@User,getdate(),@User,getdate());

	select @fCount1 = count(*) from POSFunctionSetup where FunctionType = 'ScaleFunctionButton' and FunctionName = 'Host'
    if @fCount1 = 0
      insert into POSFunctionSetup(FunctionType,FunctionName,DisplayOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
      values('ScaleFunctionButton','Host',4,@User,getdate(),@User,getdate());
  end

  commit
  return 0 

end
GO

GO

--
-- Create column [UseTouchKeyboardInAdmin] on table [dbo].[Setup]
--
ALTER TABLE [dbo].[Setup]
  ADD [UseTouchKeyboardInAdmin] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [UseTouchKeyboardInPOS] on table [dbo].[Setup]
--
ALTER TABLE [dbo].[Setup]
  ADD [UseTouchKeyboardInPOS] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create column [EvoApiBaseAddress] on table [dbo].[Setup]
--
ALTER TABLE [dbo].[Setup]
  ADD [EvoApiBaseAddress] [varchar](100) NULL DEFAULT ('')
GO

GO

--
-- Create column [ExpFlag] on table [dbo].[POHeader]
--
ALTER TABLE [dbo].[POHeader]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_po_headerinfo]
--
GO
create procedure [dbo].[sp_co_imp_updttag_po_headerinfo]
			@ID	int
as

begin

  update POHeader set expflag='Y' where ID = @ID and expflag = 'N';
  
end	


GO

GO

--
-- Drop index [IX_PODetail_ProductID] from table [dbo].[PODetail]
--
DROP INDEX [IX_PODetail_ProductID] ON [dbo].[PODetail]
GO

GO

--
-- Drop index [IX_PODetail_RefID] from table [dbo].[PODetail]
--
DROP INDEX [IX_PODetail_RefID] ON [dbo].[PODetail]
GO

GO

--
-- Drop index [IX_CloseOut_CloseoutType] from table [dbo].[CloseOut]
--
DROP INDEX [IX_CloseOut_CloseoutType] ON [dbo].[CloseOut]
GO

GO

--
-- Drop index [IX_CloseOut_ConsolidatedID] from table [dbo].[CloseOut]
--
DROP INDEX [IX_CloseOut_ConsolidatedID] ON [dbo].[CloseOut]
GO

GO

--
-- Drop index [IX_CloseOut_CreatedBy] from table [dbo].[CloseOut]
--
DROP INDEX [IX_CloseOut_CreatedBy] ON [dbo].[CloseOut]
GO

GO

--
-- Drop index [IX_CloseOut_enddatetime] from table [dbo].[CloseOut]
--
DROP INDEX [IX_CloseOut_enddatetime] ON [dbo].[CloseOut]
GO

GO

--
-- Drop index [IX_CloseOut_TransactionCnt] from table [dbo].[CloseOut]
--
DROP INDEX [IX_CloseOut_TransactionCnt] ON [dbo].[CloseOut]
GO

GO

--
-- Create column [ExpFlag] on table [dbo].[CloseOut]
--
ALTER TABLE [dbo].[CloseOut]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_CloseOut]
--
GO
create procedure [dbo].[sp_co_imp_updttag_CloseOut]
			@ID	int
as

begin

 update CloseOut set expflag='Y' where expflag = 'N' and ID = @ID;

end



GO

GO

--
-- Drop index [IX_Notes_RefID] from table [dbo].[Notes]
--
DROP INDEX [IX_Notes_RefID] ON [dbo].[Notes]
GO

GO

--
-- Drop index [IX_Notes_RefType] from table [dbo].[Notes]
--
DROP INDEX [IX_Notes_RefType] ON [dbo].[Notes]
GO

GO

--
-- Create column [ExpFlag] on table [dbo].[Notes]
--
ALTER TABLE [dbo].[Notes]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_notes]
--
GO
create procedure [dbo].[sp_co_imp_updttag_notes]
			@ID	int
as

begin

 update Notes set expflag='Y' where expflag = 'N' and ID = @ID;

end



GO

GO

--
-- Drop index [IX_MatrixValues_MatrixOptionID] from table [dbo].[MatrixValues]
--
DROP INDEX [IX_MatrixValues_MatrixOptionID] ON [dbo].[MatrixValues]
GO

GO

--
-- Drop index [IX_MatrixValues_ValueID] from table [dbo].[MatrixValues]
--
DROP INDEX [IX_MatrixValues_ValueID] ON [dbo].[MatrixValues]
GO

GO

--
-- Drop index [IX_MatrixOptions_ProductID] from table [dbo].[MatrixOptions]
--
DROP INDEX [IX_MatrixOptions_ProductID] ON [dbo].[MatrixOptions]
GO

GO

--
-- Drop index [IX_MailItems_SenderID] from table [dbo].[MailItems]
--
DROP INDEX [IX_MailItems_SenderID] ON [dbo].[MailItems]
GO

GO

--
-- Drop index [IX_Laypmts_InvoiceNo] from table [dbo].[Laypmts]
--
DROP INDEX [IX_Laypmts_InvoiceNo] ON [dbo].[Laypmts]
GO

GO

--
-- Drop index [IX_Laypmts_TransactionNo] from table [dbo].[Laypmts]
--
DROP INDEX [IX_Laypmts_TransactionNo] ON [dbo].[Laypmts]
GO

GO

--
-- Drop index [IX_ItemMatrixOptions_ItemID] from table [dbo].[ItemMatrixOptions]
--
DROP INDEX [IX_ItemMatrixOptions_ItemID] ON [dbo].[ItemMatrixOptions]
GO

GO

--
-- Drop index [IX_ItemMatrixOptions_MatrixOptionID] from table [dbo].[ItemMatrixOptions]
--
DROP INDEX [IX_ItemMatrixOptions_MatrixOptionID] ON [dbo].[ItemMatrixOptions]
GO

GO

--
-- Drop index [IX_Item_Cost] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Cost] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_DepartmentID] from table [dbo].[Item]
--
DROP INDEX [IX_Item_DepartmentID] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_DescID] from table [dbo].[Item]
--
DROP INDEX [IX_Item_DescID] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_Discount] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Discount] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_FSTender] from table [dbo].[Item]
--
DROP INDEX [IX_Item_FSTender] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_InvoiceNo] from table [dbo].[Item]
--
DROP INDEX [IX_Item_InvoiceNo] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_NormalPrice] from table [dbo].[Item]
--
DROP INDEX [IX_Item_NormalPrice] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_Price] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Price] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_ProductID] from table [dbo].[Item]
--
DROP INDEX [IX_Item_ProductID] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_ProductType] from table [dbo].[Item]
--
DROP INDEX [IX_Item_ProductType] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_Qty] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Qty] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_ReturnedItemCnt] from table [dbo].[Item]
--
DROP INDEX [IX_Item_ReturnedItemCnt] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_ReturnedItemID] from table [dbo].[Item]
--
DROP INDEX [IX_Item_ReturnedItemID] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_ServiceType] from table [dbo].[Item]
--
DROP INDEX [IX_Item_ServiceType] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_tagged] from table [dbo].[Item]
--
DROP INDEX [IX_Item_tagged] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_Taxable1] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Taxable1] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_Taxable2] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Taxable2] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_Taxable3] from table [dbo].[Item]
--
DROP INDEX [IX_Item_Taxable3] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_TaxRate1] from table [dbo].[Item]
--
DROP INDEX [IX_Item_TaxRate1] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_TaxRate2] from table [dbo].[Item]
--
DROP INDEX [IX_Item_TaxRate2] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_TaxRate3] from table [dbo].[Item]
--
DROP INDEX [IX_Item_TaxRate3] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_TaxTotal1] from table [dbo].[Item]
--
DROP INDEX [IX_Item_TaxTotal1] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_TaxTotal2] from table [dbo].[Item]
--
DROP INDEX [IX_Item_TaxTotal2] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_Item_TaxTotal3] from table [dbo].[Item]
--
DROP INDEX [IX_Item_TaxTotal3] ON [dbo].[Item]
GO

GO

--
-- Drop index [IX_ServiceMapping_BookingExpFlag] from table [dbo].[ServiceMapping]
--
DROP INDEX [IX_ServiceMapping_BookingExpFlag] ON [dbo].[ServiceMapping]
GO

GO

--
-- Drop index [IX_ServiceMapping_MappingID] from table [dbo].[ServiceMapping]
--
DROP INDEX [IX_ServiceMapping_MappingID] ON [dbo].[ServiceMapping]
GO

GO

--
-- Drop index [IX_ServiceMapping_MappingType] from table [dbo].[ServiceMapping]
--
DROP INDEX [IX_ServiceMapping_MappingType] ON [dbo].[ServiceMapping]
GO

GO

--
-- Drop index [IX_ServiceMapping_ServiceID] from table [dbo].[ServiceMapping]
--
DROP INDEX [IX_ServiceMapping_ServiceID] ON [dbo].[ServiceMapping]
GO

GO

--
-- Drop index [IX_SerialDetail_ItemID] from table [dbo].[SerialDetail]
--
DROP INDEX [IX_SerialDetail_ItemID] ON [dbo].[SerialDetail]
GO

GO

--
-- Drop index [IX_SerialDetail_SerialHeaderID] from table [dbo].[SerialDetail]
--
DROP INDEX [IX_SerialDetail_SerialHeaderID] ON [dbo].[SerialDetail]
GO

GO

--
-- Create column [BookerCategoryID] on table [dbo].[Category]
--
ALTER TABLE [dbo].[Category]
  ADD [BookerCategoryID] [varchar](50) NULL DEFAULT ('')
GO

GO

--
-- Create column [BookerDeptID] on table [dbo].[Category]
--
ALTER TABLE [dbo].[Category]
  ADD [BookerDeptID] [varchar](50) NULL DEFAULT ('')
GO

GO

--
-- Drop index [IX_Invoice_Coupon] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Coupon] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_CouponPerc] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_CouponPerc] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Discount] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Discount] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_DTax] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_DTax] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_DTaxID] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_DTaxID] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Fees] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Fees] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_FeesTax] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_FeesTax] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_LayawayNo] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_LayawayNo] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Status] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Status] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Tax] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Tax] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Tax1] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Tax1] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Tax2] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Tax2] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_Tax3] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_Tax3] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_TaxID1] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_TaxID1] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_TaxID2] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_TaxID2] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_TaxID3] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_TaxID3] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_TotalSale] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_TotalSale] ON [dbo].[Invoice]
GO

GO

--
-- Drop index [IX_Invoice_TransactionNo] from table [dbo].[Invoice]
--
DROP INDEX [IX_Invoice_TransactionNo] ON [dbo].[Invoice]
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
-- Drop index [IX_Second_Label_Types_Label_ID] from table [dbo].[Second_Label_Types]
--
DROP INDEX [IX_Second_Label_Types_Label_ID] ON [dbo].[Second_Label_Types]
GO

GO

--
-- Drop index [IX_Second_Label_Types_LabelFormat] from table [dbo].[Second_Label_Types]
--
DROP INDEX [IX_Second_Label_Types_LabelFormat] ON [dbo].[Second_Label_Types]
GO

GO

--
-- Drop default constraint on table [dbo].[Scale_Product]
--
ALTER TABLE [dbo].[Scale_Product]
  DROP CONSTRAINT [DF__Scale_Pro__PL_Ju__392E6792]
GO

GO

--
-- Drop default constraint on table [dbo].[Scale_Product]
--
ALTER TABLE [dbo].[Scale_Product]
  DROP CONSTRAINT [DF__Scale_Pro__Scale__3B16B004]
GO

GO

--
-- Drop default constraint on table [dbo].[Scale_Product]
--
ALTER TABLE [dbo].[Scale_Product]
  DROP CONSTRAINT [DF__Scale_Pro__SL_Ju__3A228BCB]
GO

GO

--
-- Drop index [IX_CardTrans_transactionno] from table [dbo].[CardTrans]
--
DROP INDEX [IX_CardTrans_transactionno] ON [dbo].[CardTrans]
GO

GO

--
-- Drop index [IX_ImportInventory_ImportDate] from table [dbo].[ImportInventory]
--
DROP INDEX [IX_ImportInventory_ImportDate] ON [dbo].[ImportInventory]
GO

GO

--
-- Drop index [IX_ImportHistory_EventName] from table [dbo].[ImportHistory]
--
DROP INDEX [IX_ImportHistory_EventName] ON [dbo].[ImportHistory]
GO

GO

--
-- Drop index [IX_ImportHistory_LastImportOn] from table [dbo].[ImportHistory]
--
DROP INDEX [IX_ImportHistory_LastImportOn] ON [dbo].[ImportHistory]
GO

GO

--
-- Create column [ExpFlag] on table [dbo].[CardAuthorisation]
--
ALTER TABLE [dbo].[CardAuthorisation]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_card_authorisation]
--
GO
create procedure [dbo].[sp_co_imp_updttag_card_authorisation]
			@ID	int
as

begin

 update CardAuthorisation set expflag='Y' where expflag = 'N' and ID = @ID;

end



GO

GO

--
-- Alter procedure [dbo].[sp_LoadDefaultSecurity]
--
GO
ALTER procedure [dbo].[sp_LoadDefaultSecurity] @intUser int

as

declare @gid		int;
declare @SecurityCode	varchar(10);

declare @count int;

declare @OwnerID int;


declare @DefaultCount   int;
declare @rc		int;
declare @rsid		int;

declare @addnewtender	char(1);
declare @cnt int;
declare @tid int;

declare @bcnt	int;
 
begin

   set @addnewtender = 'N';

   select  @count = count(*) from securitygroup
   
   if @count = 0 begin 

   insert into securitygroup(GroupName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
   values('Owner',@intUser, getdate(),@intUser,getdate());
   insert into securitygroup(GroupName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
   values('Sr. Clerk',@intUser, getdate(),@intUser,getdate());
   insert into securitygroup(GroupName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
   values('Jr. Manager',@intUser, getdate(),@intUser,getdate());

   select @OwnerID = ID from securitygroup where GroupName = 'Owner'

   declare sc1 cursor
   for select id from securitygroup
   open sc1
   fetch next from sc1 into @gid
   while @@fetch_status = 0 begin

     declare sc2 cursor
     for SELECT SecurityCode FROM SecurityPermission order by GroupSlNo, SlNo
     open sc2
     fetch next from sc2 into @SecurityCode
     while @@fetch_status = 0 begin

     if @SecurityCode <> '12f'
       insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
       values(@gid,@SecurityCode,'Y',@intUser, getdate(),@intUser,getdate());
     
     if @SecurityCode = '12f' begin

       if @gid = @OwnerID 
          insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
          values(@gid,@SecurityCode,'Y',@intUser, getdate(),@intUser,getdate());

       if @gid <> @OwnerID 
          insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
          values(@gid,@SecurityCode,'N',@intUser, getdate(),@intUser,getdate());

     end

     fetch next from sc2 into @SecurityCode
     
     end
     close sc2
     deallocate sc2 

     fetch next from sc1 into @gid

   end
   close sc1
   deallocate sc1 

   end


   /*    Inserting default data in the new system */


   select @DefaultCount = count(*) from localsetup where paramname = 'Default Setup';
   
   if @DefaultCount = 0 begin
      
     insert into localsetup (paramname, paramvalue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
     values( 'Default Setup','Y',@intUser, getdate(),@intUser,getdate());

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Cash';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Cash','Y','Y','N', @intUser,getdate(),@intUser,getdate(),'Cash','Y',1,'N');

       set @addnewtender = 'Y';
     end  

    
     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Check';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Check','Y','Y','N', @intUser,getdate(),@intUser,getdate(),'Check','N',2,'N');

       set @addnewtender = 'Y';
     end     

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Credit Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Credit Card','Y','Y','N', @intUser,getdate(),@intUser,getdate(),'Credit Card','N',3,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Debit Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Debit Card','Y','Y','N', @intUser,getdate(),@intUser,getdate(),'Debit Card','N',4,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Gift Certificate';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Gift Certificate','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Gift Certificate','N',5,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Store Credit';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Store Credit','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Store Credit','N',6,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'House Account';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'House Account','Y','N','N', @intUser,getdate(),@intUser,getdate(),'House Account','N',7,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Food Stamps';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Food Stamps','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Food Stamps','N',8,'N');

       set @addnewtender = 'Y';
     end 


     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Credit Card - Voice Auth';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Credit Card - Voice Auth','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Credit Card - Voice Auth','N',9,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Credit Card (STAND-IN)';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Credit Card (STAND-IN)','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Credit Card (STAND-IN)','N',10,'N');

       set @addnewtender = 'Y';
     end 

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Credit Card - Voice Auth (STAND-IN)';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Credit Card - Voice Auth (STAND-IN)','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Credit Card - Voice Auth (STAND-IN)','N',11,'N');

       set @addnewtender = 'Y';
     end 
     
     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Mercury Gift Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Mercury Gift Card','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Mercury Gift Card','N',12,'N');

       set @addnewtender = 'Y';
     end 
     
     
     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'EBT Cash';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'EBT Cash','Y','N','N', @intUser,getdate(),@intUser,getdate(),'EBT Cash','N',13,'N');

       set @addnewtender = 'Y';
     end 
     
     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'EBT Voucher';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'EBT Voucher','Y','N','N', @intUser,getdate(),@intUser,getdate(),'EBT Voucher','N',14,'N');

       set @addnewtender = 'Y';
     end 
     
     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Groupon';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Groupon','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Groupon','N',15,'N');

       set @addnewtender = 'Y';
     end

     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Precidia Gift Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Precidia Gift Card','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Precidia Gift Card','N',16,'N');

       set @addnewtender = 'Y';
     end

     
     set @bcnt = 0;
	 select @bcnt = count(*) from tendertypes where isblank = 'Y';
	 
	 if @bcnt = 0 begin
	 
	   insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Blank','Y','N','N', @intUser,getdate(),@intUser,getdate(),'','N',17,'Y');
       
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Blank','Y','N','N', @intUser,getdate(),@intUser,getdate(),'','N',18,'Y');
       
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Blank','Y','N','N', @intUser,getdate(),@intUser,getdate(),'','N',19,'Y');
       
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Blank','Y','N','N', @intUser,getdate(),@intUser,getdate(),'','N',20,'Y');
       
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Blank','Y','N','N', @intUser,getdate(),@intUser,getdate(),'','N',21,'Y');
	 
	   set @addnewtender = 'Y';
	 end
     
     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Datacap Gift Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Datacap Gift Card','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Datacap Gift Card','N',22,'N');

       set @addnewtender = 'Y';
     end


     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'POSLink Gift Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'POSLink Gift Card','Y','N','N', @intUser,getdate(),@intUser,getdate(),'POSLink Gift Card','N',23,'N');

       set @addnewtender = 'Y';
     end     


     set @rc = 0;
     select @rc = count(*) from tendertypes where name = 'Card';
     if @rc = 0 begin
       insert into tendertypes (Name,EditCloseoutActual,Enabled,Used,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,DisplayAs,IsOpenCashdrawer,PaymentOrder,IsBlank)
       values( 'Card','Y','N','N', @intUser,getdate(),@intUser,getdate(),'Card','N',24,'N');

       set @addnewtender = 'Y';
     end     

     if @addnewtender = 'Y' begin


       set @cnt = 1;
       declare csr cursor
       for select id from tendertypes 
       open csr
       fetch next from csr into @tid

       while @@fetch_status = 0 begin
        update tendertypes set paymentorder = @cnt where id = @tid
        set @cnt = @cnt + 1;
        fetch next from csr into @tid
       end
       close csr
       deallocate csr


     end

     set @rc = 0;
     select @rc = count(*) from employee where EmployeeID = '1'
     if @rc = 0 begin
       set @rc = 0;
       select @rc = count(*) from shiftmaster;
       if @rc = 0 begin
	 INSERT INTO shiftmaster(ShiftName,StartTime,EndTime,ShiftDuration,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values('Default Import Shift','10:00 AM','10:00 PM',720,@intUser,getdate(),@intUser,getdate());
       end

       select top(1) @rsid = ID FROM shiftmaster
       select @OwnerID = ID from securitygroup where GroupName = 'Owner'
       INSERT INTO employee (EmployeeID,LastName,FirstName,Password,ProfileID,EmpShift,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
       values('1','owner','store','1111',@OwnerID,@rsid,@intUser,getdate(),@intUser,getdate());

     end
  

   end
   
   
end


GO

GO

--
-- Drop index [IX_GiftCert_COID] from table [dbo].[GiftCert]
--
DROP INDEX [IX_GiftCert_COID] ON [dbo].[GiftCert]
GO

GO

--
-- Drop index [IX_GiftCert_ExpFlag] from table [dbo].[GiftCert]
--
DROP INDEX [IX_GiftCert_ExpFlag] ON [dbo].[GiftCert]
GO

GO

--
-- Drop index [IX_GiftCert_IssueStore] from table [dbo].[GiftCert]
--
DROP INDEX [IX_GiftCert_IssueStore] ON [dbo].[GiftCert]
GO

GO

--
-- Drop index [IX_GiftCert_OperateStore] from table [dbo].[GiftCert]
--
DROP INDEX [IX_GiftCert_OperateStore] ON [dbo].[GiftCert]
GO

GO

--
-- Drop index [IX_AttendanceInfo_EmpID] from table [dbo].[AttendanceInfo]
--
DROP INDEX [IX_AttendanceInfo_EmpID] ON [dbo].[AttendanceInfo]
GO

GO

--
-- Drop index [IX_AttendanceInfo_ExpFlag] from table [dbo].[AttendanceInfo]
--
DROP INDEX [IX_AttendanceInfo_ExpFlag] ON [dbo].[AttendanceInfo]
GO

GO

--
-- Drop index [IX_AttendanceInfo_ShiftID] from table [dbo].[AttendanceInfo]
--
DROP INDEX [IX_AttendanceInfo_ShiftID] ON [dbo].[AttendanceInfo]
GO

GO

--
-- Drop index [IX_GeneralMapping_ExpFlag] from table [dbo].[GeneralMapping]
--
DROP INDEX [IX_GeneralMapping_ExpFlag] ON [dbo].[GeneralMapping]
GO

GO

--
-- Drop index [IX_GeneralMapping_MappingID] from table [dbo].[GeneralMapping]
--
DROP INDEX [IX_GeneralMapping_MappingID] ON [dbo].[GeneralMapping]
GO

GO

--
-- Drop index [IX_GeneralMapping_MappingType] from table [dbo].[GeneralMapping]
--
DROP INDEX [IX_GeneralMapping_MappingType] ON [dbo].[GeneralMapping]
GO

GO

--
-- Drop index [IX_GeneralMapping_ReferenceID] from table [dbo].[GeneralMapping]
--
DROP INDEX [IX_GeneralMapping_ReferenceID] ON [dbo].[GeneralMapping]
GO

GO

--
-- Drop index [IX_GeneralMapping_ReferenceType] from table [dbo].[GeneralMapping]
--
DROP INDEX [IX_GeneralMapping_ReferenceType] ON [dbo].[GeneralMapping]
GO

GO

--
-- Drop index [IX_Scale_Mapping_MappingID] from table [dbo].[Scale_Mapping]
--
DROP INDEX [IX_Scale_Mapping_MappingID] ON [dbo].[Scale_Mapping]
GO

GO

--
-- Drop index [IX_Scale_Mapping_ProductID] from table [dbo].[Scale_Mapping]
--
DROP INDEX [IX_Scale_Mapping_ProductID] ON [dbo].[Scale_Mapping]
GO

GO

--
-- Drop index [IX_Scale_Graphics_Graphic_ID] from table [dbo].[Scale_Graphics]
--
DROP INDEX [IX_Scale_Graphics_Graphic_ID] ON [dbo].[Scale_Graphics]
GO

GO

--
-- Drop index [IX_Scale_Graphics_GraphicArtID] from table [dbo].[Scale_Graphics]
--
DROP INDEX [IX_Scale_Graphics_GraphicArtID] ON [dbo].[Scale_Graphics]
GO

GO

--
-- Drop index [IX_Appointments_BookingExpFlag] from table [dbo].[Appointments]
--
DROP INDEX [IX_Appointments_BookingExpFlag] ON [dbo].[Appointments]
GO

GO

--
-- Drop index [IX_Appointments_CustomerID] from table [dbo].[Appointments]
--
DROP INDEX [IX_Appointments_CustomerID] ON [dbo].[Appointments]
GO

GO

--
-- Drop index [IX_Appointments_EmployeeID] from table [dbo].[Appointments]
--
DROP INDEX [IX_Appointments_EmployeeID] ON [dbo].[Appointments]
GO

GO

--
-- Create column [ExpFlag] on table [dbo].[Appointments]
--
ALTER TABLE [dbo].[Appointments]
  ADD [ExpFlag] [char](1) NULL DEFAULT ('N')
GO

GO

--
-- Create procedure [dbo].[sp_co_imp_updttag_appointment]
--
GO
create procedure [dbo].[sp_co_imp_updttag_appointment]
			@ID	int
as

begin

 update Appointments set expflag='Y' where expflag = 'N' and ID = @ID;

end



GO

GO

--
-- Create procedure [dbo].[sp_CentralExport_V1]
--
GO
CREATE procedure [dbo].[sp_CentralExport_V1]
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
				where i.LayawayNo > 0 and it.Tagged <> 'X' and i.Status in (1,3) and t.LayAwayCustomerFlag='N'
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



		insert into CentralExportTaxAnalysis (ProductID, SKU, Description, Qty, Price, PriceI, Cost, Discount, Tax, TransDate)
				select a.ProductID, a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price*a.Qty,0) as Price, isnull(a.TaxIncludePrice,0) as PriceI,
				isnull(a.Cost * a.Qty,0) as Cost,  isnull(a.Discount,0) as Discount, isnull(TaxTotal1 + TaxTotal2 + TaxTotal3,0) as Tax, t.TransDate
				from Trans t
				left outer join Invoice i on t.ID = i.TransactionNo left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID
				left outer join  Product p on a.ProductID = p.ID  left outer join  BrandMaster br on p.BrandID = br.ID
				where (1 = 1) and a.Qty is not null and a.Cost is not null  and t.TransType in (1,4,18) and i.status in (3,18)
				and a.Tagged <> 'X' and a.Producttype in ('P','K','U','M','S','W','E','F','T','B') and t.TaxAnalysisFlag='N'
				Order by a.Description 

		insert into CentralExportPackingList (SKU, Description, SoldQty, IssueStore, CustomerID, TransDate)
				 select a.SKU,a.Description,sum(a.Qty) as SoldQty, c.IssueStore , c.CustomerID, t.TransDate
				 from Trans t 
				 left outer join invoice i on t.ID = i.TransactionNo  
				 left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID and a.Producttype in  ('P','K','U','M','S','W','E','F','T') 
				 left outer join  customer as c on c.ID = t.CustomerID  
				 where  t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' and t.PackingListFlag='N'
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
-- Drop index [IX_AcctRecv_customerid] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_customerid] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_AcctRecv_Date] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_Date] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_AcctRecv_ExpFlag] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_ExpFlag] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_AcctRecv_InvoiceNo] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_InvoiceNo] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_AcctRecv_IssueStore] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_IssueStore] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_AcctRecv_OperateStore] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_OperateStore] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_AcctRecv_TranType] from table [dbo].[AcctRecv]
--
DROP INDEX [IX_AcctRecv_TranType] ON [dbo].[AcctRecv]
GO

GO

--
-- Drop index [IX_CloseOutTender_CloseOutID] from table [dbo].[CloseOutTender]
--
DROP INDEX [IX_CloseOutTender_CloseOutID] ON [dbo].[CloseOutTender]
GO

GO

--
-- Drop index [IX_CloseOutTender_TenderID] from table [dbo].[CloseOutTender]
--
DROP INDEX [IX_CloseOutTender_TenderID] ON [dbo].[CloseOutTender]
GO

GO

--
-- Drop index [IX_VoidInv_InvoiceNo] from table [dbo].[VoidInv]
--
DROP INDEX [IX_VoidInv_InvoiceNo] ON [dbo].[VoidInv]
GO

GO

--
-- Create procedure [dbo].[sp_CentralExportCloseoutSalesByHour]
--
GO
CREATE procedure [dbo].[sp_CentralExportCloseoutSalesByHour]
			@tax_inclusive	char(1),
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50)		
			with recompile
as

declare	@LocalCloseoutType	char(1);
declare	@LocalCloseoutID		int;
declare	@LocalTerminal		nvarchar(50);
declare @PType			char(1);
declare @Pr					numeric(18,3);
declare @NPr				numeric(18,3);
declare @Qty				numeric(18,3);
declare @Disc				numeric(18,3);
declare @tiPr				numeric(15,3);
declare @T1					char(1);
declare @T2					char(1);
declare @T3					char(1);
declare @RetrnItm			numeric(18,3);
declare @LayNo				int;
declare @Status				int;
declare @TransType			int;
declare @InvNo				int;
declare @TranNo				int;
declare @hour		int;
declare @hour0		numeric(18,3);
declare @hour1		numeric(18,3);
declare @hour2		numeric(18,3);
declare @hour3		numeric(18,3);
declare @hour4		numeric(18,3);
declare @hour5		numeric(18,3);
declare @hour6		numeric(18,3);
declare @hour7		numeric(18,3);
declare @hour8		numeric(18,3);
declare @hour9		numeric(18,3);
declare @hour10		numeric(18,3);
declare @hour11		numeric(18,3);
declare @hour12		numeric(18,3);
declare @hour13		numeric(18,3);
declare @hour14		numeric(18,3);
declare @hour15		numeric(18,3);
declare @hour16		numeric(18,3);
declare @hour17		numeric(18,3);
declare @hour18		numeric(18,3);
declare @hour19		numeric(18,3);
declare @hour20		numeric(18,3);
declare @hour21		numeric(18,3);
declare @hour22		numeric(18,3);
declare @hour23		numeric(18,3);



declare @SD		datetime;
declare @ED		datetime;
declare @Notes		varchar(100);
declare @CT		char(1);
declare @NS		int;
declare @count1		int;
declare @count2		int;
declare @EmpID		varchar(12);
declare @CTeml		varchar(50);

declare @countvoid			int;


begin
  
  set @LocalCloseoutType = @CloseoutType;
  set @LocalCloseoutID	= @CloseoutID;
  set @LocalTerminal = @Terminal;

  --delete from CentralExportCloseOutSalesHour where ReportTerminalName = @LocalTerminal

  set @hour0  	= 0;
  set @hour1	= 0;
  set @hour2	= 0;
  set @hour3	= 0;
  set @hour4	= 0;
  set @hour5	= 0;
  set @hour6	= 0;
  set @hour7	= 0;
  set @hour8	= 0;
  set @hour9	= 0;
  set @hour10	= 0;
  set @hour11	= 0;
  set @hour12	= 0;
  set @hour13	= 0;
  set @hour14	= 0;
  set @hour15	= 0;
  set @hour16	= 0;
  set @hour17	= 0;
  set @hour18	= 0;
  set @hour19	= 0;
  set @hour20	= 0;
  set @hour21	= 0;
  set @hour22	= 0;
  set @hour23	= 0;


  select @CT=c.CloseoutType, @SD=c.StartDatetime, @ED=isnull(c.EndDatetime,getdate()), @Notes=c.Notes, 
  @EmpID = isnull(e.EmployeeID,'ADMIN') , @CTeml = c.TerminalName
  from closeout c left outer join employee e on e.ID = c.CreatedBy where c.ID = @LocalCloseoutID

  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin

    /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)

    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo > 0 and t.Transtype = 4
    and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2  

    declare sc1 cursor
    for select inv.ID, t.ID,i.TaxIncludeRate, i.ProductType, i.Price, i.NormalPrice, i.Qty,i.Discount, i.Taxable1, i.Taxable2, 
               i.Taxable3,i.ReturnedItemCnt,inv.Status,inv.LayawayNo, t.TransType,  { fn HOUR(t.TransDate) } as thour from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @LocalCloseoutID and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z'
			   and i.ProductType <> 'H' and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4)) and i.Tagged <> 'X'
                order by thour 

    open sc1
    fetch next from sc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    while @@fetch_status = 0 begin

	  set @countvoid = 0;

	   select @countvoid = count(*) from VoidInv where invoiceno = @InvNo;

	   if @countvoid = 0 begin

	  if @tax_inclusive = 'N' begin

      if @hour = 0 set @hour0 = @hour0 + (@Pr*@Qty) - @Disc;   
      if @hour = 1 set @hour1 = @hour1 + (@Pr*@Qty) - @Disc;   
      if @hour = 2 set @hour2 = @hour2 + (@Pr*@Qty) - @Disc;   
      if @hour = 3 set @hour3 = @hour3 + (@Pr*@Qty) - @Disc;   
      if @hour = 4 set @hour4 = @hour4 + (@Pr*@Qty) - @Disc;   
      if @hour = 5 set @hour5 = @hour5 + (@Pr*@Qty) - @Disc;   
      if @hour = 6 set @hour6 = @hour6 + (@Pr*@Qty) - @Disc;   
      if @hour = 7 set @hour7 = @hour7 + (@Pr*@Qty) - @Disc;   
      if @hour = 8 set @hour8 = @hour8 + (@Pr*@Qty) - @Disc;   
      if @hour = 9 set @hour9 = @hour9 + (@Pr*@Qty) - @Disc;   
      if @hour = 10 set @hour10 = @hour10 + (@Pr*@Qty) - @Disc;   
      if @hour = 11 set @hour11 = @hour11 + (@Pr*@Qty) - @Disc;   
      if @hour = 12 set @hour12 = @hour12 + (@Pr*@Qty) - @Disc;   
      if @hour = 13 set @hour13 = @hour13 + (@Pr*@Qty) - @Disc;   
      if @hour = 14 set @hour14 = @hour14 + (@Pr*@Qty) - @Disc;   
      if @hour = 15 set @hour15 = @hour15 + (@Pr*@Qty) - @Disc;   
      if @hour = 16 set @hour16 = @hour16 + (@Pr*@Qty) - @Disc;   
      if @hour = 17 set @hour17 = @hour17 + (@Pr*@Qty) - @Disc;   
      if @hour = 18 set @hour18 = @hour18 + (@Pr*@Qty) - @Disc;   
      if @hour = 19 set @hour19 = @hour19 + (@Pr*@Qty) - @Disc;   
      if @hour = 20 set @hour20 = @hour20 + (@Pr*@Qty) - @Disc;   
      if @hour = 21 set @hour21 = @hour21 + (@Pr*@Qty) - @Disc;   
      if @hour = 22 set @hour22 = @hour22 + (@Pr*@Qty) - @Disc;   
      if @hour = 23 set @hour23 = @hour23 + (@Pr*@Qty) - @Disc;  
	  
	  end

	  if @tax_inclusive = 'Y' begin

      if @hour = 0 set @hour0 = @hour0 + (@tiPr*@Qty);   
      if @hour = 1 set @hour1 = @hour1 + (@tiPr*@Qty);   
      if @hour = 2 set @hour2 = @hour2 + (@tiPr*@Qty);   
      if @hour = 3 set @hour3 = @hour3 + (@tiPr*@Qty);   
      if @hour = 4 set @hour4 = @hour4 + (@tiPr*@Qty);   
      if @hour = 5 set @hour5 = @hour5 + (@tiPr*@Qty);   
      if @hour = 6 set @hour6 = @hour6 + (@tiPr*@Qty);   
      if @hour = 7 set @hour7 = @hour7 + (@tiPr*@Qty);   
      if @hour = 8 set @hour8 = @hour8 + (@tiPr*@Qty);   
      if @hour = 9 set @hour9 = @hour9 + (@tiPr*@Qty);   
      if @hour = 10 set @hour10 = @hour10 + (@tiPr*@Qty);   
      if @hour = 11 set @hour11 = @hour11 + (@tiPr*@Qty);   
      if @hour = 12 set @hour12 = @hour12 + (@tiPr*@Qty);   
      if @hour = 13 set @hour13 = @hour13 + (@tiPr*@Qty);   
      if @hour = 14 set @hour14 = @hour14 + (@tiPr*@Qty);   
      if @hour = 15 set @hour15 = @hour15 + (@tiPr*@Qty);   
      if @hour = 16 set @hour16 = @hour16 + (@tiPr*@Qty);   
      if @hour = 17 set @hour17 = @hour17 + (@tiPr*@Qty);   
      if @hour = 18 set @hour18 = @hour18 + (@tiPr*@Qty);   
      if @hour = 19 set @hour19 = @hour19 + (@tiPr*@Qty);   
      if @hour = 20 set @hour20 = @hour20 + (@tiPr*@Qty);   
      if @hour = 21 set @hour21 = @hour21 + (@tiPr*@Qty);   
      if @hour = 22 set @hour22 = @hour22 + (@tiPr*@Qty);   
      if @hour = 23 set @hour23 = @hour23 + (@tiPr*@Qty);  
	  
	  end
	   
           end
      fetch next from sc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    end
    close sc1
    deallocate sc1 

  end 


  if @LocalCloseoutType = 'C' begin

      /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)

    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in
    (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) 
    and inv.LayawayNo > 0 and t.Transtype = 4 and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2 
   
    declare csc1 cursor
    for select inv.ID, t.ID,i.TaxIncludeRate, i.ProductType, i.Price, i.NormalPrice,i.Qty,i.Discount, i.Taxable1, i.Taxable2, 
               i.Taxable3,i.ReturnedItemCnt,inv.Status,inv.LayawayNo, t.TransType,  { fn HOUR(t.TransDate) } as thour from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z'
			   and i.ProductType <> 'H' and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4))  and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv) order by thour 

    open csc1
    fetch next from csc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    while @@fetch_status = 0 begin

	  set @countvoid = 0;

	   select @countvoid = count(*) from VoidInv where invoiceno = @InvNo;

	   if @countvoid = 0 begin

	   if @tax_inclusive = 'N' begin
      if @hour = 0 set @hour0 = @hour0 + (@Pr*@Qty) - @Disc;   
      if @hour = 1 set @hour1 = @hour1 + (@Pr*@Qty) - @Disc;   
      if @hour = 2 set @hour2 = @hour2 + (@Pr*@Qty) - @Disc;   
      if @hour = 3 set @hour3 = @hour3 + (@Pr*@Qty) - @Disc;   
      if @hour = 4 set @hour4 = @hour4 + (@Pr*@Qty) - @Disc;   
      if @hour = 5 set @hour5 = @hour5 + (@Pr*@Qty) - @Disc;   
      if @hour = 6 set @hour6 = @hour6 + (@Pr*@Qty) - @Disc;   
      if @hour = 7 set @hour7 = @hour7 + (@Pr*@Qty) - @Disc;   
      if @hour = 8 set @hour8 = @hour8 + (@Pr*@Qty) - @Disc;   
      if @hour = 9 set @hour9 = @hour9 + (@Pr*@Qty) - @Disc;   
      if @hour = 10 set @hour10 = @hour10 + (@Pr*@Qty) - @Disc;   
      if @hour = 11 set @hour11 = @hour11 + (@Pr*@Qty) - @Disc;   
      if @hour = 12 set @hour12 = @hour12 + (@Pr*@Qty) - @Disc;   
      if @hour = 13 set @hour13 = @hour13 + (@Pr*@Qty) - @Disc;   
      if @hour = 14 set @hour14 = @hour14 + (@Pr*@Qty) - @Disc;   
      if @hour = 15 set @hour15 = @hour15 + (@Pr*@Qty) - @Disc;   
      if @hour = 16 set @hour16 = @hour16 + (@Pr*@Qty) - @Disc;   
      if @hour = 17 set @hour17 = @hour17 + (@Pr*@Qty) - @Disc;   
      if @hour = 18 set @hour18 = @hour18 + (@Pr*@Qty) - @Disc;   
      if @hour = 19 set @hour19 = @hour19 + (@Pr*@Qty) - @Disc;   
      if @hour = 20 set @hour20 = @hour20 + (@Pr*@Qty) - @Disc;   
      if @hour = 21 set @hour21 = @hour21 + (@Pr*@Qty) - @Disc;   
      if @hour = 22 set @hour22 = @hour22 + (@Pr*@Qty) - @Disc;   
      if @hour = 23 set @hour23 = @hour23 + (@Pr*@Qty) - @Disc;   

	  end

	  if @tax_inclusive = 'Y' begin
      if @hour = 0 set @hour0 = @hour0 + (@tiPr*@Qty);   
      if @hour = 1 set @hour1 = @hour1 + (@tiPr*@Qty);   
      if @hour = 2 set @hour2 = @hour2 + (@tiPr*@Qty);   
      if @hour = 3 set @hour3 = @hour3 + (@tiPr*@Qty);   
      if @hour = 4 set @hour4 = @hour4 + (@tiPr*@Qty);   
      if @hour = 5 set @hour5 = @hour5 + (@tiPr*@Qty);   
      if @hour = 6 set @hour6 = @hour6 + (@tiPr*@Qty);   
      if @hour = 7 set @hour7 = @hour7 + (@tiPr*@Qty);   
      if @hour = 8 set @hour8 = @hour8 + (@tiPr*@Qty);   
      if @hour = 9 set @hour9 = @hour9 + (@tiPr*@Qty);   
      if @hour = 10 set @hour10 = @hour10 + (@tiPr*@Qty);   
      if @hour = 11 set @hour11 = @hour11 + (@tiPr*@Qty);   
      if @hour = 12 set @hour12 = @hour12 + (@tiPr*@Qty);   
      if @hour = 13 set @hour13 = @hour13 + (@tiPr*@Qty);   
      if @hour = 14 set @hour14 = @hour14 + (@tiPr*@Qty);   
      if @hour = 15 set @hour15 = @hour15 + (@tiPr*@Qty);   
      if @hour = 16 set @hour16 = @hour16 + (@tiPr*@Qty);   
      if @hour = 17 set @hour17 = @hour17 + (@tiPr*@Qty);   
      if @hour = 18 set @hour18 = @hour18 + (@tiPr*@Qty);   
      if @hour = 19 set @hour19 = @hour19 + (@tiPr*@Qty);   
      if @hour = 20 set @hour20 = @hour20 + (@tiPr*@Qty);   
      if @hour = 21 set @hour21 = @hour21 + (@tiPr*@Qty);   
      if @hour = 22 set @hour22 = @hour22 + (@tiPr*@Qty);   
      if @hour = 23 set @hour23 = @hour23 + (@tiPr*@Qty);   

	  end

      end
	   
      fetch next from csc1 into @InvNo,@TranNo,@tiPr,@PType,@Pr,@NPr,@Qty,@Disc,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,@hour

    end
    close csc1
    deallocate csc1 
  end

    insert into CentralExportCloseOutSalesHour(Timeinterval,SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values  ('12:00 M -   1:00 AM',@hour0,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 1:00 AM -  2:00 AM',@hour1,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 2:00 AM -  3:00 AM',@hour2,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 3:00 AM -  4:00 AM',@hour3,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 4:00 AM -  5:00 AM',@hour4,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 5:00 AM -  6:00 AM',@hour5,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 6:00 AM -  7:00 AM',@hour6,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 7:00 AM -  8:00 AM',@hour7,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 8:00 AM -  9:00 AM',@hour8,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 9:00 AM - 10:00 AM',@hour9,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('10:00 AM - 11:00 AM',@hour10,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('11:00 AM -  12:00 N',@hour11,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('12:00 N -   1:00 PM',@hour12,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 1:00 PM -  2:00 PM',@hour13,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 2:00 PM -  3:00 PM',@hour14,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 3:00 PM -  4:00 PM',@hour15,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 4:00 PM -  5:00 PM',@hour16,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 5:00 PM -  6:00 PM',@hour17,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 6:00 PM -  7:00 PM',@hour18,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 7:00 PM -  8:00 PM',@hour19,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 8:00 PM -  9:00 PM',@hour20,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values (' 9:00 PM - 10:00 PM',@hour21,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('10:00 PM - 11:00 PM',@hour22,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
    insert into CentralExportCloseOutSalesHour(Timeinterval, SalesAmount,CloseoutID,StartDateTime,EndDateTime,CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) values ('11:00 PM -  12:00 M',@hour23,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal); 

  

     


end

GO

GO

--
-- Create procedure [dbo].[sp_CentralExportCloseoutSalesByDept]
--
GO
CREATE procedure [dbo].[sp_CentralExportCloseoutSalesByDept]
			@tax_inclusive	char(1),
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50)
			with recompile

as

declare @LocalCloseoutType	char(1);
declare @LocalCloseoutID	int;
declare @LocalTerminal	nvarchar(50);
declare @DID		int;
declare @DeptID		nvarchar(10);
declare @DeptDesc		nvarchar(40);
declare @amt		numeric(18,3);
declare @amtI		numeric(18,3);
declare @SD		datetime;
declare @ED		datetime;
declare @Notes		nvarchar(100);
declare @CT		char(1);
declare @NS		int;
declare @count1		int;
declare @count2		int;
declare @EmpID		nvarchar(12);
declare @CTeml		nvarchar(50);


begin

  set @LocalCloseoutType = @CloseoutType;
  set @LocalCloseoutID	= @CloseoutID;
  set @LocalTerminal = @Terminal;
  
  --delete from CentralExportCloseOutSalesDept where ReportTerminalName = @LocalTerminal

  select @CT=c.CloseoutType, @SD=c.StartDatetime, @ED=isnull(c.EndDatetime,getdate()), @Notes=c.Notes, 
  @EmpID = isnull(e.EmployeeID,'ADMIN') , @CTeml = c.TerminalName
  from closeout c left outer join employee e on e.ID = c.CreatedBy where c.ID = @LocalCloseoutID

  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin

    /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)
    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @LocalCloseoutID  and inv.LayawayNo > 0 and t.Transtype = 4
    and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2 
   
    declare sc38 cursor
    for select i.DepartmentID,d.DepartmentID, d.Description , sum(i.Price*i.Qty) - sum(i.Discount) as salesamt,
	sum(i.TaxIncludeRate*i.Qty) as salesamt1 from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo 
               left outer join dept d on d.ID = i.departmentID where t.CloseOutID = @LocalCloseoutID and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z' and i.ProductType <> 'H' and i.departmentID > 0 and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4)) and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv)
               group by i.DepartmentID,d.DepartmentID, d.Description order by d.DepartmentID

    open sc38
    fetch next from sc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    while @@fetch_status = 0 begin
	  if @tax_inclusive = 'N' begin
      insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amt,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
      end    
	  if @tax_inclusive = 'Y' begin
      insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amtI,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
      end  
      fetch next from sc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    end
    close sc38
    deallocate sc38

  end  


  if @LocalCloseoutType = 'C' begin

      /*  No. of Sale */
    select @count1 = count(inv.ID)
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and inv.LayawayNo = 0 and inv.ID not in ( select invoiceno from VoidInv)

    select @count2 = count(inv.ID) from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in
    (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) 
    and inv.LayawayNo > 0 and t.Transtype = 4
    and inv.Status = 3 and inv.ID not in ( select invoiceno from VoidInv)

    set @NS =  @count1 + @count2 
   
    declare csc38 cursor
    for select i.DepartmentID,d.DepartmentID, d.Description , sum(i.Price*i.Qty) - sum(i.Discount) as salesamt,
	sum(i.TaxIncludeRate*i.Qty) as salesamt1 from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo 
               left outer join dept d on d.ID = i.departmentID where t.CloseOutID 
	       in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) and i.ServiceType = 'Sales'
               and i.ProductType <> 'A' and i.ProductType <> 'G' and i.ProductType <> 'C' and i.ProductType <> 'X' and i.ProductType <> 'Z' and i.ProductType <> 'H' and i.departmentID > 0 and ((inv.LayawayNo = 0) or 
               (inv.LayawayNo = 0 and inv.Status = 3 and t.TransType = 4)) and i.Tagged <> 'X' and inv.ID not in ( select invoiceno from VoidInv)
               group by i.DepartmentID,d.DepartmentID, d.Description order by d.DepartmentID

    open csc38
    fetch next from csc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    while @@fetch_status = 0 begin
	  if @tax_inclusive = 'N' begin
      insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amt,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
      end  
	  if @tax_inclusive = 'Y' begin
      insert into CentralExportCloseOutSalesDept(DeptID, DeptDesc, SalesAmount,CloseoutID,StartDateTime,EndDateTime,
      CloseoutType,Notes,NoOfSales,EmpID,TerminalName,ReportTerminalName) 
      values (@DeptID,@DeptDesc,@amtI,@LocalCloseoutID,@SD,@ED,@CT,@Notes,@NS,@EmpID,@CTeml,@LocalTerminal);
      end   
      fetch next from csc38 into @DID,@DeptID,@DeptDesc,@amt,@amtI

    end
    close csc38
    deallocate csc38

  end  

end

GO

GO

--
-- Create procedure [dbo].[sp_CentralExportCloseoutReportTender]
--
GO
CREATE procedure [dbo].[sp_CentralExportCloseoutReportTender]
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50)
			with recompile
as
  
declare @TID 			int;
declare @TTName			nvarchar(40);
declare @TName			nvarchar(40);
declare @Count 			int;
declare @Amount 			numeric(18,3);
declare @TAmount			numeric(18,3);
declare @cashbackamt		numeric(18,3);
declare @cashbackcnt		int;
declare @cardprocessingamt		numeric(18,3);
declare @cardprocessingcnt		int;
declare	@LocalCloseoutType	char(1);
declare	@LocalCloseoutID		int;
declare	@LocalTerminal			nvarchar(50);

declare @cashfloatamt			numeric(18,3);
declare @boolCashTenderingExists char(1);
declare @CashTenderID int;

declare @ConsolidatedTerminal	int;

declare @safedropamt		numeric(18,3);
declare @paidinamt			numeric(18,3);
declare @paidoutamt			numeric(18,3);
declare @cashoutamt			numeric(18,3);
declare @cashinamt			numeric(18,3);

begin

   set @LocalCloseoutType = @CloseoutType;
   set @LocalCloseoutID	= @CloseoutID;
   set @LocalTerminal = @Terminal;

   set @cashbackamt = 0;
   set @cashbackcnt = 0;

   set @cardprocessingamt = 0;
   set @cardprocessingcnt = 0;
   set @cashfloatamt = 0;

   set @boolCashTenderingExists = 'N';
   set @CashTenderID = 0;
   set @ConsolidatedTerminal = 0;

   set @cashoutamt = 0;


   set @Count = 0;
   --delete from CentralExportCloseoutReportTender where ReportTerminalName = @LocalTerminal
  
  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin
   
    declare sc10 cursor
    for select ID, DisplayAs,Name from TenderTypes where name <> 'Store Credit' order by PaymentOrder
               
    open sc10
    fetch next from sc10 into @TID,@TName,@TTName
    while @@fetch_status = 0 begin

     select @Count = count(*) , @Amount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
     on tr.ID = t.TransactionNo 
     where t.TenderType = @TID and tr.CloseoutID = @LocalCloseoutID and tr.TransType not in (6,66,67,91,92)
     and tr.ID not in ( select transactionno from invoice where id in ( select invoiceno from VoidInv 
     where invoiceno not in ( select id from invoice where repairparentid = 0 and servicetype = 'Repair' )))
	 and tr.ID not in (select transactionno from invoice where id in (select invoiceno from VoidInv))
	 and tr.ID not in (select transactionno from invoice where RepairParentID in (select invoiceno from VoidInv))

	 if @LocalCloseoutType = 'T' and @TTName = 'Cash' and @Count = 0 begin
	   set @CashTenderID = @TID;
	   set @boolCashTenderingExists = 'N';
	   insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

	   select @cashfloatamt  = isnull(cashfloat,0) from CashFloat where CloseoutID = @CloseoutID;
			    insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

       select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

	  
	   select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);
	  
	   select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID = @CloseoutID);
	   insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

	 end
     if @Count > 0 begin
	    if @TTName = 'Credit Card' or @TTName = 'Debit Card' or @TTName = 'EBT' or @TTName = 'EBT Cash' begin
		  set @cardprocessingcnt = @cardprocessingcnt + @Count;
		  set @cardprocessingamt = @cardprocessingamt + @Amount;
		end
		     if @LocalCloseoutType = 'T' begin
			   if @TTName = 'Cash' begin
			        set @CashTenderID = @TID;
					set @boolCashTenderingExists = 'Y';
			        insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					select @cashfloatamt  = isnull(cashfloat,0) from CashFloat where CloseoutID = @CloseoutID;
			    insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

				select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

				select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

				select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID = @CloseoutID);
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
               end
			   else begin
			     insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
			   end
			end
			else begin
			  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
			end
            
      end
      set @Count = 0;
     fetch next from sc10 into @TID,@TName,@TTName

    end

    close sc10
    deallocate sc10

	

	
	select @cashbackcnt = count(*) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and RefCardAuthAmount - CardAmount > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @LocalCloseoutID);

	select @cashbackamt = isnull(sum(RefCardAuthAmount - CardAmount),0) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @LocalCloseoutID);
    
	if (@cashbackcnt > 0)
	begin
	  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99998,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@LocalTerminal)
	end
	/*
	select @cardprocessingcnt = count(*) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @CloseoutID);

	select @cardprocessingamt = isnull(sum(CardAmount),0) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @CloseoutID);*/
    
	if (@cardprocessingcnt > 0)
	begin
	  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99999,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@LocalTerminal)
	end

    
    declare sc11 cursor

    for select c.TenderID,c.TenderAmount,t.DisplayAs from CloseoutTender c left outer join tendertypes t on t.ID = c.TenderID 
    where c.CloseoutID=@LocalCloseoutID and t.name <> 'Store Credit' order by t.PaymentOrder
               
    open sc11
    fetch next from sc11 into @TID,@TAmount,@TName
    while @@fetch_status = 0 begin

     insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('C',@LocalCloseoutID,@TID,@TName,@TAmount,@LocalTerminal)

     set @Count = 0;
     select @Amount = TenderAmount,@Count = TenderCount from CentralExportCloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
     CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal

	 if @LocalCloseoutType = 'T' and @TID = @CashTenderID  begin
	   select @Amount = sum(TenderAmount),@Count = sum(TenderCount) from CentralExportCloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
       CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal and TenderAmount != -929292 and TenderName not in ('  Cash Float', '  Cash In', '  Cash Out', '  Paid In', '  Paid Out')
	 end
     
     if @Count > 0
     insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('R',@LocalCloseoutID,@TID,@TName,@TAmount-@Amount,@LocalTerminal)
     
     fetch next from sc11 into @TID,@TAmount,@TName

    end
    close sc11
deallocate sc11

  end

  
   
  if @LocalCloseoutType = 'C' begin
   
    select @ConsolidatedTerminal = count(*) from CloseOut where CloseoutType = 'T' and ConsolidatedID = @CloseoutID;
    declare csc10 cursor
    for select ID, DisplayAs,Name from TenderTypes where name <> 'Store Credit' order by PaymentOrder
               
    open csc10
    fetch next from csc10 into @TID,@TName,@TTName
    while @@fetch_status = 0 begin

     select @Count = count(*) , @Amount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
     on tr.ID = t.TransactionNo 
     where t.TenderType = @TID and tr.CloseoutID in 
     (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and tr.TransType not in (66,67,6,91,92)
     and tr.ID not in ( select transactionno from invoice where id in ( select invoiceno from VoidInv 
     where invoiceno not in ( select id from invoice where repairparentid = 0 and servicetype = 'Repair' )))
	 and tr.ID not in (select transactionno from invoice where id in (select invoiceno from VoidInv))
	 and tr.ID not in (select transactionno from invoice where RepairParentID in (select invoiceno from VoidInv))

	  if @ConsolidatedTerminal > 0 and @TTName = 'Cash' and @Count = 0 begin
	   set @CashTenderID = @TID;
	   set @boolCashTenderingExists = 'N';
	   insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

	   select @cashfloatamt  = isnull(sum(cashfloat),0) from CashFloat where CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T');
			    insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

	  select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

	  select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

	   select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

	  select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

	   select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);
	  
	 end

     if @Count > 0 begin
	         if @TTName = 'Credit Card' or @TTName = 'Debit Card' or @TTName = 'EBT' or @TTName = 'EBT Cash' begin
				set @cardprocessingcnt = @cardprocessingcnt + @Count;
				set @cardprocessingamt = @cardprocessingamt + @Amount;
		end

		if @ConsolidatedTerminal > 0 begin
		  
		   if @TTName = 'Cash' begin
			        set @CashTenderID = @TID;
					set @boolCashTenderingExists = 'Y';
			        insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					select @cashfloatamt  = isnull(sum(cashfloat),0) from CashFloat where CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T');
			        insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			        TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);


					select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

					select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);


					select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

	  select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

					select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
               end
			   else begin
			     insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
			   end


		end
		else begin
		  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
		end

			
     end
     set @Count = 0;
     
     fetch next from csc10 into @TID,@TName,@TTName

    end

    close csc10
    deallocate csc10


	


	select @cashbackcnt = count(*) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and RefCardAuthAmount - CardAmount > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) );

	select @cashbackamt = isnull(sum(RefCardAuthAmount - CardAmount),0) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) );
    
	if (@cashbackcnt > 0)
	begin
	  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99998,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@LocalTerminal)
	end

	/*
	select @cardprocessingcnt = count(*) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) );

	select @cardprocessingamt = isnull(sum(CardAmount),0) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) );*/
	if (@cardprocessingcnt > 0)
	begin
	  insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99999,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@LocalTerminal)
	end

    declare csc11 cursor

    for select c.TenderID,c.TenderAmount,t.DisplayAs from CloseoutTender c left outer join tendertypes t
    on t.ID = c.TenderID where c.CloseoutID=@LocalCloseoutID and t.name <> 'Store Credit' order by t.PaymentOrder
               
    open csc11
    fetch next from csc11 into @TID,@TAmount,@TName
    while @@fetch_status = 0 begin

      insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('C',@LocalCloseoutID,@TID,@TName,@TAmount,@LocalTerminal)

      set @Count = 0;
      
      select @Amount = TenderAmount,@Count=TenderCount from CentralExportCloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
      CloseoutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and ReportTerminalName = @LocalTerminal
     
	   if @ConsolidatedTerminal > 0 and @TID = @CashTenderID  begin
	   select @Amount = sum(TenderAmount),@Count = sum(TenderCount) from CentralExportCloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
       CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal and TenderAmount != -929292 and TenderName not in ('  Cash Float', '  Cash In', '  Cash Out', '  Paid In', '  Paid Out')
	 end

     if @Count > 0
      insert into CentralExportCloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('R',@LocalCloseoutID,@TID,@TName,@TAmount-@Amount,@LocalTerminal)
          
      set @Count = 0;    
   fetch next from csc11 into @TID,@TAmount,@TName

    end
    close csc11
    deallocate csc11

  end


end
GO

GO

--
-- Create procedure [dbo].[sp_CentralExportCloseoutReportReturnItem]
--
GO
CREATE procedure [dbo].[sp_CentralExportCloseoutReportReturnItem]
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50)
			
as

declare @LocalCloseoutType		char(1);
declare @LocalCloseoutID		int;
declare @LocalTerminal		nvarchar(50);
declare @ReturnSKU		nvarchar(16);
declare @ReturnInvoiceNo		int;
declare @ReturnAmount		numeric(15,3);
declare @countvoid			int;

begin

  set @LocalCloseoutType = @CloseoutType;
  set @LocalCloseoutID	= @CloseoutID;
  set @LocalTerminal = @Terminal;

  --delete from CentralExportCloseoutReturn where ReportTerminalName = @LocalTerminal
  
  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin

   
    declare sc9 cursor
    for select inv.ID,i.SKU,i.Price*i.Qty from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @LocalCloseoutID 
               and i.ReturnedItemID <> 0 and i.Qty < 0 and inv.ID not in ( select invoiceno from VoidInv) 
               and i.Tagged <> 'X' and i.ServiceType = 'Sales'
    open sc9
    fetch next from sc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount
    while @@fetch_status = 0 begin

     insert into CentralExportCloseoutReturn (ReturnSKU,ReturnInvoiceNo,ReturnAmount,ReportTerminalName, CloseoutID) 
     values(@ReturnSKU,@ReturnInvoiceNo,@ReturnAmount,@LocalTerminal, @CloseoutID)
      
     
     fetch next from sc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount

    end
    close sc9
    deallocate sc9 
  
  end

  if @LocalCloseoutType = 'C' begin

   
    declare csc9 cursor
    for select inv.ID,i.SKU,i.Price*i.Qty from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) 
               and i.ReturnedItemID <> 0 and i.Qty < 0 
               and i.Tagged <> 'X' and i.ServiceType = 'Sales'

    open csc9
    fetch next from csc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount
    while @@fetch_status = 0 begin

	  if @ReturnInvoiceNo > 0 begin

	   set @countvoid = 0;

	   select @countvoid = count(*) from VoidInv where invoiceno = @ReturnInvoiceNo;

	   if @countvoid = 0 begin
    
         insert into CentralExportCloseoutReturn (ReturnSKU,ReturnInvoiceNo,ReturnAmount,ReportTerminalName, CloseoutID) 
         values(@ReturnSKU,@ReturnInvoiceNo,@ReturnAmount,@LocalTerminal, @CloseoutID) 
     
	   end

	 end

      fetch next from csc9 into @ReturnInvoiceNo,@ReturnSKU,@ReturnAmount
     
    end
    close csc9
    deallocate csc9 

	
  
  end


  



end


GO

GO

--
-- Create procedure [dbo].[sp_CentralExportCloseoutReportHeader]
--
GO
CREATE procedure [dbo].[sp_CentralExportCloseoutReportHeader]
			@tax_inclusive	char(1),
			@CloseoutType	char(1),
			@CloseoutID		int,
			@Terminal		nvarchar(50)
as

declare @TaxedSales				numeric(18,3);
declare @NonTaxedSales			numeric(18,3);
declare @ServiceSales			numeric(18,3);
declare @ProductSales			numeric(18,3);
declare @OtherSales				numeric(18,3);
declare @DiscountItemNo			int;
declare @DiscountItemAmount		numeric(18,3);
declare @BDiscountItemNo		int;
declare @BDiscountItemAmount	numeric(18,3);
declare @SDiscountItemNo		int;
declare @SDiscountItemAmount	numeric(18,3);
declare @RDiscountItemNo		int;
declare @RDiscountItemAmount	numeric(18,3);
declare @DiscountInvoiceNo		int;
declare @DiscountInvoiceAmount	numeric(18,3);
declare @RDiscountInvoiceNo		int;
declare @RDiscountInvoiceAmount	numeric(18,3);
declare @RntDiscountInvoiceNo	int;
declare @RntDiscountInvoiceAmount	numeric(18,3);

declare @LayawayDeposits		numeric(18,3);
declare @LayawayRefund			numeric(18,3);
declare @LayawayPayment			numeric(18,3);
declare @LayawaySalesPosted		numeric(18,3);
declare @PaidOuts				numeric(18,3);
declare @GCsold					numeric(18,3);
declare @SCissued				numeric(18,3);
declare @SCredeemed				numeric(18,3);
declare @HACharged				numeric(18,3);
declare @HApayments				numeric(18,3);
declare @NoOfSales				int;
declare @NoOfRents				int;
declare @NoOfRepairs			int;
declare @PType				char(1);
declare @Pr					numeric(18,3);
declare @NPr				numeric(18,3);
declare @Qty				numeric(18,3);
declare @T1					char(1);
declare @T2					char(1);
declare @T3					char(1);
declare @T1r				numeric(18,3);
declare @T2r				numeric(18,3);
declare @T3r				numeric(18,3);
declare @Tx1  				numeric(18,3);
declare @Tx2				numeric(18,3);
declare @Tx3 				numeric(18,3);
declare @Tx1ty				int;
declare @Tx2ty				int;
declare @Tx3ty				int;
declare @Tx1Tot				numeric(18,3);
declare @Tx2Tot				numeric(18,3);
declare @Tx3Tot				numeric(18,3);
declare @RetrnItm			numeric(18,3);
declare @LayNo				int;
declare @Status				int;
declare @TransType			int;
declare @InvNo				int;
declare @TranNo				int;
declare @Tax1			numeric(18,3);
declare @Tax2			numeric(18,3);
declare @Tax3			numeric(18,3);
declare @LTax1			numeric(18,3);
declare @LTax2			numeric(18,3);
declare @LTax3			numeric(18,3);
declare @TaxAmt1		numeric(18,3);
declare @TaxAmt2		numeric(18,3);
declare @TaxAmt3		numeric(18,3);
declare @BTaxAmt1		numeric(18,3);
declare @BTaxAmt2		numeric(18,3);
declare @BTaxAmt3		numeric(18,3);
declare @STaxAmt1		numeric(18,3);
declare @STaxAmt2		numeric(18,3);
declare @STaxAmt3		numeric(18,3);
declare @RTaxAmt1		numeric(18,3);
declare @RTaxAmt2		numeric(18,3);
declare @RTaxAmt3		numeric(18,3);
declare @RntTaxAmt1		numeric(18,3);
declare @RntTaxAmt2		numeric(18,3);
declare @RntTaxAmt3		numeric(18,3);
declare @CpnPerc		numeric(18,3);
declare @Discnt			numeric(18,3);
declare @LDiscnt		numeric(18,3);
declare @LayAmount		numeric(18,3);
declare @TID			int;
declare @TAmount		numeric(18,3);
declare @r1				int;
declare @r2				int;
declare @r3				int;
declare @r4				int;
declare @r5				int;
declare @r6				int;
declare @r7				int;
declare @r8				int;
declare @r9				int;
declare @r10			int;
declare @r11			int;
declare @r12			int;
declare @tc				int;
declare @Tax1Name       nvarchar(20);
declare @Tax1Exist 		char(1);
declare @Tax2Name       nvarchar(20);
declare @Tax2Exist 		char(1);
declare @Tax3Name       nvarchar(20);
declare @Tax3Exist 		char(1);
declare @SD				datetime;
declare @ED				datetime;
declare @Notes			nvarchar(100);
declare @EmpID			nvarchar(12);
declare @CT				char(1);
declare @NS				int;
declare @count1			int;
declare @count2			int;
declare @CTeml			nvarchar(50);
declare @TotalSale		numeric(18,3);
declare @TTotalSale		numeric(18,3);
declare @TotalSales_PreTax 	numeric(18,3);
declare @CostOfGoods 		numeric(18,3);
declare @NoSaleCount		int;
declare @RentDuration		numeric(18,3);
declare @RentDeposit		numeric(18,3);
declare @TRentDeposit		numeric(18,3);
declare @TRentDepositReturned   numeric(18,3);
declare @RepairSales		numeric(18,3);
declare @RentSales	    	numeric(18,3);
declare @cost				numeric(18,3);
declare @UOMCount			numeric(18,3);
declare @SalesInvoiceCount	int;
declare @RentInvoiceCount	int;
declare @RepairInvoiceCount	int;
declare @RprPr				numeric(18,3);
declare @RprNPr				numeric(18,3);
declare @RprQty				numeric(18,3);
declare @RprT1				char(1);
declare @RprT2				char(1);
declare @RprT3				char(1);
declare @RprT1r				numeric(18,3);
declare @RprT2r				numeric(18,3);
declare @RprT3r				numeric(18,3);
declare @RprTx1  			numeric(18,3);
declare @RprTx2				numeric(18,3);
declare @RprTx3 			numeric(18,3);
declare @RprDiscnt			numeric(18,3);
declare @RprParentID		numeric(18,3);
declare @RentCalc			char(1);
declare @ProductTx			numeric(18,3);
declare @ProductNTx			numeric(18,3);
declare @ServiceTx			numeric(18,3);
declare @ServiceNTx			numeric(18,3);
declare @OtherTx			numeric(18,3);
declare @OtherNTx			numeric(18,3);
declare @tcld				int;
declare @fstender			char(1);
declare @invtx1				numeric(18,3);
declare @invtx2				numeric(18,3);
declare @invtx3				numeric(18,3);
declare @tcid				int;
declare @dtTipEnd			datetime;
declare @dtTipStart			datetime;
declare @Emp				int;
declare @CashTip			numeric(15,3);
declare @CCTip				numeric(15,3);
declare @TCashTip			numeric(15,3);
declare @TCCTip				numeric(15,3);
declare @AcceptTips			char(1);
declare @EmpCoutID			int;
declare @SalesFees			numeric(15,3);
declare @SalesFeesTax		numeric(15,3);
declare @RentFees			numeric(15,3);
declare @RentFeesTax		numeric(15,3);
declare @RepairFees			numeric(15,3);
declare @RepairFeesTax		numeric(15,3);
declare @Fees				numeric(15,3);
declare @FeesTax			numeric(15,3);
declare @DTax			    numeric(15,3);	
declare @TDTax			    numeric(15,3);
declare  @i_ProductID		int;
declare  @i_SKU				nvarchar(16);
declare  @MGCSold			numeric(15,2);
declare  @PGCSold			numeric(15,2);
declare  @DGCSold			numeric(15,2);
declare  @PLGCSold			numeric(15,2);
declare @BottleRefund		numeric(15,2);

declare @FreeQty			int;
declare @FreeAmount			numeric(15,3);
declare @FreeTag			char(1);

declare @LottoPayout		numeric(15,3);

declare @item_TaxIncludeRate		numeric(15,3);
declare @item_TaxIncludePrice		numeric(15,3);

begin
  
  --delete from CentralExportCloseoutReportMain where ReportTerminalName = @Terminal

  select @CT=c.CloseoutType, @SD=c.StartDatetime, @ED=isnull(c.EndDatetime,getdate()), @Notes=c.Notes, 
  @EmpID = isnull(e.EmployeeID,'ADMIN') , @CTeml = c.TerminalName
  from closeout c left outer join employee e on e.ID = c.CreatedBy where c.ID = @CloseoutID

  set @SalesInvoiceCount = 0;
  set @RentInvoiceCount = 0;
  set @RepairInvoiceCount = 0;
  
  set @TaxedSales = 0;
  set @NonTaxedSales = 0;
  set @ServiceSales = 0;
  set @ProductSales = 0;
  set @OtherSales = 0;

  set @DiscountItemNo = 0;
  set @DiscountItemAmount = 0;
  set @SDiscountItemNo = 0;
  set @SDiscountItemAmount = 0;
  set @BDiscountItemNo = 0;
  set @BDiscountItemAmount = 0;
  set @RDiscountItemNo = 0;
  set @RDiscountItemAmount = 0;
  
  set @DiscountInvoiceNo = 0;
  set @DiscountInvoiceAmount = 0;
  set @RDiscountInvoiceNo = 0;
  set @RDiscountInvoiceAmount = 0;
  set @RntDiscountInvoiceNo = 0;
  set @RntDiscountInvoiceAmount = 0;
  
  set @LayawayDeposits = 0;
  set @LayawayRefund = 0;
  set @LayawaySalesPosted = 0;
  set @LayawayPayment = 0;

  set @PaidOuts = 0;
  set @GCsold = 0;
  set @SCissued = 0;
  set @SCredeemed = 0;
  set @HACharged = 0;
  set @HApayments = 0;

  set @Tax1 = 0;
  set @Tax2 = 0;
  set @Tax3 = 0;

  set @LTax1 = 0;
  set @LTax2 = 0;
  set @LTax3 = 0;

  set @TaxAmt1 = 0;
  set @TaxAmt2 = 0;
  set @TaxAmt3 = 0;
  
  set @BTaxAmt1 = 0;
  set @BTaxAmt2 = 0;
  set @BTaxAmt3 = 0;
  
  set @STaxAmt1 = 0;
  set @STaxAmt2 = 0;
 set @STaxAmt3 = 0;
  
  set @RTaxAmt1 = 0;
  set @RTaxAmt2 = 0;
  set @RTaxAmt3 = 0;
    
  set @RntTaxAmt1 = 0;
  set @RntTaxAmt2 = 0;
  set @RntTaxAmt3 = 0;
  
  set @LayAmount = 0;

  set @NoOfSales = 0;
  set @NoOfRepairs = 0;
  set @NoOfRents = 0;

  set @Tax1Exist = 'N';
  set @Tax2Exist = 'N';
  set @Tax3Exist = 'N';

  set @TTotalSale = 0;
  set @CostOfGoods = 0;

  set @NoSaleCount = 0;
    
  set @TRentDeposit = 0;
  set @RentSales = 0;
  set @RepairSales = 0;
  set @TRentDepositReturned = 0;
  
  set @CpnPerc = 0;
  
  set @ProductTx	= 0;
  set @ProductNTx	= 0;
  set @ServiceTx	= 0;
  set @ServiceNTx	= 0;
  set @OtherTx		= 0;
  set @OtherNTx		= 0;


  set @tcld = 0;

  set  @CashTip = 0;
  set  @CCTip = 0;
  
  set  @TCashTip = 0;
  set  @TCCTip = 0;

  set  @AcceptTips = 'N';
  
  
  set @SalesFees		= 0;
  set  @SalesFeesTax	= 0;

  set @RentFees			= 0;
  set @RentFeesTax		= 0;

  set @RepairFees		= 0;
  set @RepairFeesTax	= 0;

  set @TDTax = 0;
  
  set @MGCSold = 0;
  set @PGCSold = 0;
  set @DGCSold = 0;
  set @PLGCSold = 0;
  
  set @BottleRefund = 0;

  set @FreeQty = 0;
  set @FreeAmount = 0;

  set @LottoPayout = 0;

  select @AcceptTips = AcceptTips from Setup;

  if @CloseoutType = 'E' or @CloseoutType = 'T' begin
 
    declare sc1 cursor
    for select inv.ID, t.ID, i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,i.ReturnedItemCnt,
    inv.Status,inv.LayawayNo, t.TransType,inv.TotalSale,i.cost,i.UOMCount,i.Discount,i.RentDuration,inv.RentDeposit,
    i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.IsRentCalculated,inv.CouponPerc,i.TaxType1,i.TaxType2,i.TaxType3,
    i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,inv.Tax1,inv.Tax2,inv.Tax3,i.Fees,i.FeesTax,i.DTax,i.ProductID,i.SKU,i.BuyNGetFreeCategory,
	i.TaxIncludeRate,i.TaxIncludePrice
    
    from item i left outer join invoice inv on i.invoiceNo=inv.ID left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.ID not in 
    ( select invoiceno from VoidInv) and i.Tagged <> 'X'	  
    open sc1
    fetch next from sc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,
    @TransType,@TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,
    @CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,
    @DTax,@i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice
    
    while @@fetch_status = 0 begin
    
      if @Status = 16 and @RentCalc = 'Y' set @Qty = -@Qty;
      
      set @Tx1 = 0;
      set @Tx2 = 0;
      set @Tx3 = 0;

      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
      begin
         if @T1='Y' begin
           if @Tx1ty = 0 begin
		     if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @Discnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
			 if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
		   end
           if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
         end
         if @T2='Y' begin
            if @Tx2ty = 0 begin
			  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @Discnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
			  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
			end
            if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
         end   
         if @T3='Y' begin
            if @Tx3ty = 0 begin
			  if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @Discnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
			  if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
		    end
            if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
         end   
      end         

      if @PType = 'G' and @TransType = 1  /* gift cert sales */
      begin
      set @GCsold = @GCsold + @Pr*@Qty;
      end   
      
      if @PType = 'O' and @TransType = 1  /* bottle refund */
      begin
        set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
      end   
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
      begin
        set @MGCSold = @MGCSold + @Pr;
      end  
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
      begin
        set @PGCSold = @PGCSold + @Pr;
      end 

      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
      begin
        set @DGCSold = @DGCSold + @Pr;
      end 
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
      begin
        set @PLGCSold = @PLGCSold + @Pr;
      end
    
      if @PType = 'A' and @TransType = 1  /* house account payment */
      begin
        set @HApayments = @HApayments + @Pr*@Qty;
      end  

	  if @PType = 'H' and @TransType = 1
	  begin
	    if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
        if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 15 and @RentCalc = 'N'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 16 and @RentCalc = 'Y'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end

      if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'H' and @PType <> 'S' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
      begin

	    if @FreeTag = 'F' begin			/* Buy 'n Get Free */
		   set @FreeQty = @FreeQty + @Qty;
		   set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
		end

        if @PType = 'U'					/* cost of goods sold */
        begin
	      set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
        end

        if @PType <> 'U'
        begin
	      set @CostOfGoods = @CostOfGoods + @cost*@Qty;
        end

        if @PType = 'B'					/* other sales */
        begin
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
		    if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          
          if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @Discnt;
		  if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

		  /*if @tax_inclusive = 'Y' begin
		     set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          
          set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
          set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
          set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
          if @Discnt <> 0					/* Discount on item */
          begin
            set @BDiscountItemNo = @BDiscountItemNo + 1;
            set @BDiscountItemAmount = @BDiscountItemAmount + @Discnt;
          end
          
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
          
  end

        if @PType <> 'B'				/*  product sales */
        begin
        
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		     set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          
          if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @Discnt;
		  if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty);

		  /*if @tax_inclusive = 'Y' begin
		     set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
		  
          
          set @TaxAmt1 = @TaxAmt1 + @Tx1;
          set @TaxAmt2 = @TaxAmt2 + @Tx2;
          set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
          if @Discnt <> 0					/* Discount on item */
          begin
            set @DiscountItemNo = @DiscountItemNo + 1;
            set @DiscountItemAmount = @DiscountItemAmount + @Discnt;
          end
          
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
          
        end
        
        if @DTax <> 0 set @TDTax = @TDTax + @DTax;

      end 
    
      if @PType = 'S' and @TransType = 1		/* service sales */
      begin
        if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt;
			if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty);
			/*if @tax_inclusive = 'Y' begin
		     set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/ 
        end
        else begin
          if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty); 
		  /*if @tax_inclusive = 'Y' begin
		     set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
        end

        if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @Discnt;
		if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

		/*if @tax_inclusive = 'Y' begin
		     set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/

        set @STaxAmt1 = @STaxAmt1 + @Tx1;
        set @STaxAmt2 = @STaxAmt2 + @Tx2;
        set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
        if @Discnt <> 0					/* Discount on item */
        begin
          set @SDiscountItemNo = @SDiscountItemNo + 1;
          set @SDiscountItemAmount = @SDiscountItemAmount + @Discnt;
        end
        
        if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
          
      end
     
      if @Status = 15 begin
        
        set @TRentDeposit = @TRentDeposit + @RentDeposit;
        
        if @RentCalc = 'N' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (@item_TaxIncludePrice);

		  /*if @tax_inclusive = 'Y' begin
		     set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/

          set @RntTaxAmt1 = @RntTaxAmt1 + @Tx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @Tx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @Tx3;
          
          if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
          
        end
      end
      
      if @Status = 16 begin
        if @RentCalc = 'N' begin
		  set @TRentDepositReturned = @TRentDepositReturned + (-@TotalSale);
		end
        if @RentCalc = 'Y' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (-@item_TaxIncludeRate); 
		  /*if @tax_inclusive = 'Y' begin
		     set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/

          set @RntTaxAmt1 = @RntTaxAmt1 + @invtx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @invtx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @invtx3;
          set @TRentDepositReturned = @TRentDepositReturned + @RentDeposit;
          
          if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
          
        end
      end
     
      
     
    fetch next from sc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,
    @TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,
    @Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,@DTax,@i_ProductID,@i_SKU,@FreeTag,
	@item_TaxIncludeRate,@item_TaxIncludePrice

    end
    close sc1
    deallocate sc1 
    
    
    declare scrpr1 cursor
    for select inv.RepairParentID from invoice inv left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID 
    and inv.ID not in ( select invoiceno from VoidInv) and inv.Status = 18
			  
    open scrpr1
    fetch next from scrpr1 into @RprParentID
    
    while @@fetch_status = 0 begin
    
        
    
        declare scrpr cursor
        for select i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,i.Discount,i.TaxRate1,i.TaxRate2,i.TaxRate3,
        inv.CouponPerc,i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,
		i.TaxIncludeRate, i.TaxIncludePrice
        from item i left outer join invoice inv on i.invoiceNo=inv.ID where inv.ID not in ( select invoiceno from VoidInv) and i.Tagged <> 'X'
        and inv.ID = @RprParentID
			  
       open scrpr
       fetch next from scrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
       @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
    
       while @@fetch_status = 0 begin
       
           set @RprTx1 = 0;
		   set @RprTx2 = 0;
           set @RprTx3 = 0;

           if (@RprT1='Y' or @RprT2='Y'or @RprT3='Y') and (@fstender = 'N')
           begin
             if @RprT1='Y' begin
               if @Tx1ty = 0 set @RprTx1 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT1r/100)*(100 - @CpnPerc)/100,2);
               if @Tx1ty = 1 set @RprTx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT2='Y' begin
               if @Tx2ty = 0 set @RprTx2 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT2r/100)*(100 - @CpnPerc)/100,2);
               if @Tx2ty = 1 set @RprTx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT3='Y' begin
               if @Tx3ty = 0 set @RprTx3 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT3r/100)*(100 - @CpnPerc)/100,2);
               if @Tx3ty = 1 set @RprTx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
             end
           end         
                     
            
            if @tax_inclusive = 'N' set @RepairSales = @RepairSales + (@RprPr*@RprQty) - @RprDiscnt;
			if @tax_inclusive = 'Y' set @RepairSales = @RepairSales + (@item_TaxIncludeRate);
					
			 
            set @RTaxAmt1 = @RTaxAmt1 + @RprTx1;
            set @RTaxAmt2 = @RTaxAmt2 + @RprTx2;
            set @RTaxAmt3 = @RTaxAmt3 + @RprTx3;
        
           if @RprDiscnt <> 0					/* Discount on item */
           begin
             set @RDiscountItemNo = @RDiscountItemNo + 1;
             set @RDiscountItemAmount = @RDiscountItemAmount + @RprDiscnt;
           end
           
           if @Fees <> 0 set @RepairFees = @RepairFees + @Fees;
           if @FeesTax <> 0 set @RepairFeesTax = @RepairFeesTax + @FeesTax;
          
           fetch next from scrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
           @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
        end
        close scrpr
        deallocate scrpr 
     
     fetch next from scrpr1 into @RprParentID
     
     end
     close scrpr1
     deallocate scrpr1     
    
    
    declare sc2 cursor
    for select inv.ID,t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status,inv.LayawayNo, t.TransType, inv.Coupon,inv.IsRentCalculated
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @CloseoutID and inv.ID not in ( select invoiceno from VoidInv)

    open sc2
    fetch next from sc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc
    while @@fetch_status = 0 begin

      if @LayNo = 0  /* non layaway items */

      begin

        if @Status = 3 set @NoOfSales = @NoOfSales + 1;
        
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1;
        
        if @Status = 15 or @Status = 16 set @RentInvoiceCount = @RentInvoiceCount + 1;
        
        if @Status = 18 set @RepairInvoiceCount = @RepairInvoiceCount + 1;
        
		

        if (@Discnt > 0) begin
          if @Status = 3 begin
            set @DiscountInvoiceNo = @DiscountInvoiceNo + 1;
            set @DiscountInvoiceAmount = @DiscountInvoiceAmount + @Discnt;
          end
          if (@Status = 15 and @RentCalc = 'N') or (@Status = 16 and @RentCalc = 'Y') begin
            set @RntDiscountInvoiceNo = @RntDiscountInvoiceNo + 1;
            set @RntDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
          if @Status = 18 begin
            set @RDiscountInvoiceNo = @RDiscountInvoiceNo + 1;
            set @RDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
        end

      end  

      if @LayNo > 0 and @TransType = 4 and @Status = 3  begin  /* layaway items */ 

        set @NoOfSales = @NoOfSales + 1;     
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1; 
     
      end     
           
     fetch next from sc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc

    end

    close sc2
    deallocate sc2 

    
    declare sc3 cursor
    for select inv.ID, t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status, t.TransType, inv.Coupon
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID = @CloseoutID and inv.LayawayNo > 0 and inv.ID not in ( select invoiceno from VoidInv)

    open sc3
    fetch next from sc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt
    while @@fetch_status = 0 begin

    if @TransType = 2  /* layaway Deposit */
    begin    
        set @LayAmount = 0;
        select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
        set @LayawayDeposits = @LayawayDeposits + @LayAmount;
    end   

    if @TransType = 4  /* layaway Payment */

    begin
      
        if @Status = 1 or @Status = 3 

        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno <> @TranNo and invoiceno = @InvNo
		  set @LayawayDeposits = @LayawayDeposits + @LayAmount;
		     
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayPayment = @LayawayPayment + @LayAmount;
          
		  
          if @Status = 3 begin /* layaway sales */

             set @LayawaySalesPosted = @LayawaySalesPosted + @LayAmount; 

             declare sc4 cursor
             for select i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2, i.Taxable3,
             inv.Tax1, inv.Tax2, inv.Tax3, i.Discount,i.cost,i.UOMCount,i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.CouponPerc,
             i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,i.DTax,
			 i.ProductID,i.SKU,i.BuyNGetFreeCategory,i.TaxIncludeRate,i.TaxIncludePrice from item i 
             left outer join invoice inv on i.invoiceNo=inv.ID
             left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.ID=@InvNo and i.Tagged <> 'X'

             open sc4
             fetch next from sc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,
             @T1r,@T2r,@T3r,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,
			 @i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice
             while @@fetch_status = 0 begin
                 set @Tx1 = 0;
				 set @Tx2 = 0;
                 set @Tx3 = 0;


				 if @FreeTag = 'F' begin			/* Buy 'n Get Free */
					set @FreeQty = @FreeQty + @Qty;
					set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
				 end

                 if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
                 begin
                  if @T1='Y' begin
                    if @Tx1ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @LDiscnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
                  end 
				  if @T2='Y' begin
                    if @Tx2ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @LDiscnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
                  end 
                  if @T3='Y' begin
                    if @Tx3ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @LDiscnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
                  end 
                 end         
               
				 if @PType = 'G' and @TransType = 1  /* gift cert sales */
			     begin
			       set @GCsold = @GCsold + @Pr*@Qty;
		         end   
		         
		         if @PType = 'O' and @TransType = 1  /* bottle refund */
                 begin
                    set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
                 end  
		         
		        if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
				begin
					set @MGCSold = @MGCSold + @Pr;
				end 
				
				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
				begin
					set @PGCSold = @PGCSold + @Pr;
				end 
                       			 if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
				begin
					set @DGCSold = @DGCSold + @Pr;
				end 
				 if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
				begin
					set @PLGCSold = @PLGCSold + @Pr;
				end 

                 if @PType = 'A' and @TransType = 1  /* house account payment */
                 begin
                   set @HApayments = @HApayments + @Pr*@Qty;
                 end  

                if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'S'  and @PType <> 'H' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
                begin

                  if @PType = 'U'					/* cost of goods sold */
	              begin
					set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
	        	  end
	
        	      if @PType <> 'U'
	              begin
  				    set @CostOfGoods = @CostOfGoods + @cost*@Qty;
	              end
	              
	              
	              if @PType = 'B'					/* other sales */
                  begin
                  
                    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
                      if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty); 
					  /*if @tax_inclusive = 'Y' begin
		                set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    else begin
                      if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt;
					  if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty);
					  /*if @tax_inclusive = 'Y' begin
		                set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    
					if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @LDiscnt;
					if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

					/*if @tax_inclusive = 'Y' begin
						set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					end*/
          
					set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
					set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
					set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @BDiscountItemNo = @BDiscountItemNo + 1;
						set @BDiscountItemAmount = @BDiscountItemAmount + @LDiscnt;
					end
					
					if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
                    if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				  end

                  if @PType <> 'B'				/*  product sales */
				  begin
				  
				    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
                      if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
					  /*if @tax_inclusive = 'Y' begin
		                set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    else begin
                      if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
					  /*if @tax_inclusive = 'Y' begin
		                set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		              end*/
                    end
                    
                    if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @LDiscnt; 
					if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty);

					/*if @tax_inclusive = 'Y' begin
						set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					end*/
          
					set @TaxAmt1 = @TaxAmt1 + @Tx1;
					set @TaxAmt2 = @TaxAmt2 + @Tx2;
					set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @DiscountItemNo = @DiscountItemNo + 1;
						set @DiscountItemAmount = @DiscountItemAmount + @LDiscnt;
					end
                    if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
                    if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				   end
				   
				   if @DTax <> 0 set @TDTax = @TDTax + @DTax;
				   
				end
               
                if @PType = 'S' and @TransType = 1		/* service sales */
			    begin
			    
			      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
                    if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt;
					if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty);
					/*if @tax_inclusive = 'Y' begin
		                set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/  
                  end
                  else begin
                    if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt;
					if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty);
					/*if @tax_inclusive = 'Y' begin
		                set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/  
                  end
          
                  if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @LDiscnt;
				  if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

				  /*if @tax_inclusive = 'Y' begin
						set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
				  end*/

                  set @STaxAmt1 = @STaxAmt1 + @Tx1;
				  set @STaxAmt2 = @STaxAmt2 + @Tx2;
                  set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
                  if @LDiscnt <> 0					/* Discount on item */
                  begin
                    set @SDiscountItemNo = @SDiscountItemNo + 1;
                    set @SDiscountItemAmount = @SDiscountItemAmount + @LDiscnt;
                  end
                  
                  if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
                  if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
                end

             
     
            fetch next from sc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,@T1r,@T2r,
            @T3r,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,@i_ProductID,
			@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice

          end
          close sc4
          deallocate sc4
          
          end

        end

      end 

      if @TransType = 3  /* layaway Payment */
      begin 

        if @Status = 5  /* layaway Refund */
        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayRefund = @LayawayRefund + @LayAmount;

        end

      end 
           
     fetch next from sc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt

    end
    close sc3
    deallocate sc3 
    
    
    
    select @TaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
        
    select @RntTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt1 = @RntTaxAmt1 + @STaxAmt1;
    
    select @RntTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt2 = @RntTaxAmt2 + @STaxAmt2;
    
    select @RntTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt3 = @RntTaxAmt3 + @STaxAmt3;
    
    select @RTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID = @CloseoutID and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)

  /* No Sale */

  select @NoSaleCount = count(*) from trans where TransType = 5 and CloseOutID = @CloseoutID

      /*  Paid outs */

  select @PaidOuts = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 6 and tr.CloseOutID = @CloseoutID

  /*  Lotto Payout */

  select @LottoPayout = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 60 and tr.CloseOutID = @CloseoutID


  /*  House account charged */

  select @HACharged = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='House Account' and inv.ID not in ( select invoiceno from VoidInv);

  /*
  /*  Store Credit redeemed */

  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='Store Credit ' and t.TenderAmount > 0 and inv.ID not in ( select invoiceno from VoidInv);

  /*  Store Credit issued*/

  select @SCissued = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='Store Credit ' and t.TenderAmount < 0 and inv.ID not in ( select invoiceno from VoidInv);

  */

  /*  Store Credit issued*/

  select @SCissued = isnull(sum(StoreCreditAmount),0) from trans where CloseOutID = @CloseoutID and TransType in (50,51);
  
  
  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID = @CloseoutID and tt.name='Store Credit' and inv.ID not in ( select invoiceno from VoidInv);


    set @tc = 0;

    declare scT cursor
    for select isnull(tx1.TaxName,''),isnull(tx2.TaxName,''),isnull(tx3.TaxName,'')
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               left outer join TaxHeader tx1 on tx1.ID = inv.TaxID1 
               left outer join TaxHeader tx2 on tx2.ID = inv.TaxID2 
               left outer join TaxHeader tx3 on tx3.ID = inv.TaxID3 
               where t.CloseOutID = @CloseoutID and inv.ID not in (select invoiceno from VoidInv)
               and (inv.TaxID1 > 0 or inv.TaxID2 > 0 or inv.TaxID3 > 0)

    open scT
    fetch next from scT into @Tax1Name,@Tax2Name,@Tax3Name
    while @@fetch_status = 0 begin

	  if @Tax1Name <> '' or  @Tax2Name <> '' or @Tax1Name <> ''
      begin
        if @Tax1Name <> '' set  @Tax1Exist = 'Y';
        if @Tax2Name <> '' set  @Tax2Exist = 'Y';
        if @Tax3Name <> '' set  @Tax3Exist = 'Y';
     end      
     fetch next from scT into @Tax1Name,@Tax2Name,@Tax3Name

    end

    close scT
    deallocate scT


    if @AcceptTips = 'Y' begin

      if @CloseoutType = 'E' begin

        select @dtTipEnd = enddatetime, @Emp = createdby from closeout where ID = @CloseoutID;
        if @dtTipEnd is null select @dtTipEnd = max(dayend) from attendanceinfo where empid = @Emp;
        
        select @dtTipStart = max(enddatetime) from closeout where createdby = @Emp and closeouttype = 'E' and ID < @CloseoutID;
 
        if @dtTipStart is not null begin
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and dayend 
           between @dtTipStart and @dtTipEnd;	
        end

        if @dtTipStart is null begin
        
           select @dtTipStart = max(daystart) from attendanceinfo where empid = @Emp and daystart <= @dtTipEnd;
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and 
           dayend between @dtTipStart and @dtTipEnd;	
        end

      end


      if @CloseoutType = 'T' begin


        declare sth cursor
        for select distinct t.EmployeeID from trans t left outer join employee  e on t.EmployeeID = e.ID 
        where t.CloseOutID = @CloseoutID and t.EmployeeID > 0 and t.TerminalName = @Terminal
        open sth
        fetch next from sth into @Emp
   
        while @@fetch_status = 0 begin
   
          select @dtTipEnd = enddatetime from closeout where ID = @CloseoutID;

          select @dtTipStart = max(enddatetime) from closeout where closeouttype = 'T' and ID < @CloseoutID;

          if @dtTipStart is not null begin
            select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and 
            dayend between @dtTipStart and @dtTipEnd;
          end

          if @dtTipStart is null begin
            select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp 
            and dayend <= @dtTipEnd ;
    end
          

          fetch next from sth into @Emp

        end
       
        close sth 
        deallocate sth
       
       
       set  @CashTip = 0;
       set @CCTip = 0;

      end
    
    end

  


    insert into CentralExportCloseoutReportMain (TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,
				ProductSales,OtherSales,DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,
				DiscountInvoiceAmount,LayawayDeposits,LayawayRefund,LayawayPayment,LayawaySalesPosted,
				GCsold,HApayments,PaidOuts,HACharged,SCissued,SCredeemed,NoOfSales,
				Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,CloseoutID,StartDateTime,EndDateTime,CloseoutType,
				Notes,EmpID,TerminalName,ReportTerminalName,TotalSales_PreTax,CostOfGoods,
				NoSaleCount,RentSales,RentDeposit,RentDepositReturned,RentTax1Amount,RentTax2Amount,RentTax3Amount,
				RentDiscountInvoiceNo,RentDiscountInvoiceAmount,RepairSales,RepairTax1Amount,RepairTax2Amount,RepairTax3Amount,
				RepairDiscountItemNo,RepairDiscountItemAmount,RepairDiscountInvoiceNo,RepairDiscountInvoiceAmount,ServiceTax1Amount,ServiceTax2Amount,
				ServiceTax3Amount,ServiceDiscountItemNo,ServiceDiscountItemAmount,OtherTax1Amount,OtherTax2Amount,OtherTax3Amount,
				OtherDiscountItemNo,OtherDiscountItemAmount,SalesInvoiceCount,RentInvoiceCount,RepairInvoiceCount,
				ProductTx,ProductNTx,ServiceTx,ServiceNTx,OtherTx,OtherNTx,CashTip,CCTip,
				SalesFees,SalesFeesTax,RentFees,RentFeesTax,RepairFees,RepairFeesTax,DTax,MGCsold,PGCsold,BottleRefund,
				DGCsold,PLGCsold,Free_Qty,Free_Amount,LottoPayout) 
				values
                (@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,
				@ProductSales,@OtherSales,@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,
				@DiscountInvoiceAmount,@LayawayDeposits,@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,
				@GCsold,@HApayments,@PaidOuts,@HACharged,@SCissued,@SCredeemed,@NoOfSales,
				@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
				@CloseoutID,@SD,@ED,@CT,@Notes,@EmpID,@CTeml,@Terminal,@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@NoSaleCount,
				@RentSales,@TRentDeposit,@TRentDepositReturned,@RntTaxAmt1,@RntTaxAmt2,@RntTaxAmt3,@RntDiscountInvoiceNo,@RntDiscountInvoiceAmount,
				@RepairSales,@RTaxAmt1,@RTaxAmt2,@RTaxAmt3,@RDiscountItemNo,@RDiscountItemAmount,@RDiscountInvoiceNo,
				@RDiscountInvoiceAmount,@STaxAmt1,@STaxAmt2,@STaxAmt3,@SDiscountItemNo,@SDiscountItemAmount,@BTaxAmt1,@BTaxAmt2,
				@BTaxAmt3,@BDiscountItemNo,@BDiscountItemAmount,@SalesInvoiceCount,@RentInvoiceCount,@RepairInvoiceCount,
				@ProductTx,@ProductNTx,@ServiceTx,@ServiceNTx,@OtherTx,@OtherNTx,@CashTip,@CCTip,
				@SalesFees,@SalesFeesTax,@RentFees,@RentFeesTax,@RepairFees,@RepairFeesTax,@TDTax,@MGCSold,@PGCSold,
				@BottleRefund,@DGCsold,@PLGCsold,@FreeQty,@FreeAmount,@LottoPayout)  

  
     exec @r1 = sp_CentralExportCloseoutReportReturnItem @CloseoutType , @CloseoutID,@Terminal

     exec @r2 = sp_CentralExportCloseoutReportTender @CloseoutType , @CloseoutID,@Terminal

     exec @r3 = sp_CentralExportCloseoutSalesByHour @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal

     exec @r4 = sp_CentralExportCloseoutSalesByDept @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal 

  
  end  



  

  if @CloseoutType = 'C' begin
 
    declare csc1 cursor
    for select inv.ID, t.ID, i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,
               i.ReturnedItemCnt,inv.Status,inv.LayawayNo, t.TransType,inv.TotalSale,i.cost,i.UOMCount,i.Discount,
               i.RentDuration,inv.RentDeposit,i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.IsRentCalculated,inv.CouponPerc,
               i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,
               inv.Tax1,inv.Tax2,inv.Tax3,i.Fees,i.FeesTax,i.DTax,i.ProductID,i.SKU,i.BuyNGetFreeCategory,
			   i.TaxIncludeRate,i.TaxIncludePrice from item i 
               left outer join invoice inv on i.invoiceNo=inv.ID
               left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
			   and inv.ID not in ( select invoiceno from VoidInv) and i.Tagged <> 'X'

    open csc1
    fetch next from csc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,
    @TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,@CpnPerc,
    @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,@DTax,@i_ProductID,
	@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice

    while @@fetch_status = 0 begin

      if @Status = 16 and @RentCalc = 'Y' set @Qty = -@Qty;
      set @Tx1 = 0;
      set @Tx2 = 0;
      set @Tx3 = 0;

      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
      begin
         if @T1='Y' begin
           if @Tx1ty = 0 begin
		     if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @Discnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
			 if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
		   end
           if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
         end
         if @T2='Y' begin
            if @Tx2ty = 0 begin
			  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @Discnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
			  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
			end
            if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
         end   
         if @T3='Y' begin
            if @Tx3ty = 0 begin
			 if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @Discnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
			 if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
			end
            if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
         end 
      end 

      if @PType = 'G' and @TransType = 1  /* gift cert sales */
      begin
      
        set @GCsold = @GCsold + @Pr*@Qty;

      end   
      
      
      if @PType = 'O' and @TransType = 1  /* bottle refund */
      begin
        set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
      end  
      
      
      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
      begin
        set @MGCSold = @MGCSold + @Pr;
      end 
      
	  if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
      begin
        set @PGCSold = @PGCSold + @Pr;
      end

      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
      begin
        set @DGCSold = @DGCSold + @Pr;
      end

      if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
      begin
        set @PLGCSold = @PLGCSold + @Pr;
      end
      
      if @PType = 'A' and @TransType = 1  /* house account payment */
      begin
        set @HApayments = @HApayments + @Pr*@Qty;
      end  

      if @PType = 'H' and @TransType = 1
	  begin
	    if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
        if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 15 and @RentCalc = 'N'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end

	  if @PType = 'H' and @Status = 16 and @RentCalc = 'Y'
	  begin
	    if @Fees <> 0 set @RentFees = @RentFees + @Fees;
        if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
	  end


      if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'S' and @PType <> 'H' and (@PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
      begin

	    if @FreeTag = 'F' begin			/* Buy 'n Get Free */
		  set @FreeQty = @FreeQty + @Qty;
		  set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
		end

	    if @PType = 'U'					/* cost of goods sold */
	    begin
		  set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
        end
	
        if @PType <> 'U'
	    begin
		  set @CostOfGoods = @CostOfGoods + @cost*@Qty;
	    end

        if @PType = 'B'					/* other sales */
        begin
        
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
        
          if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @Discnt;
		  if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

		  /*if @tax_inclusive = 'Y' begin
			set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
	      end*/
          
          set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
          set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
          set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
          if @Discnt <> 0					/* Discount on item */
          begin
            set @BDiscountItemNo = @BDiscountItemNo + 1;
            set @BDiscountItemAmount = @BDiscountItemAmount + @Discnt;
          end
          
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
        end

        if @PType <> 'B'				/*  product sales */
        begin
        
          if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
            if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          else begin
            if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
			if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
			/*if @tax_inclusive = 'Y' begin
		       set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
          end
          
          if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty); 

		  /*if @tax_inclusive = 'Y' begin
			set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          
          set @TaxAmt1 = @TaxAmt1 + @Tx1;
          set @TaxAmt2 = @TaxAmt2 + @Tx2;
          set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
          if @Discnt <> 0					/* Discount on item */
          begin
            set @DiscountItemNo = @DiscountItemNo + 1;
            set @DiscountItemAmount = @DiscountItemAmount + @Discnt;
          end
          if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
        end
        
        if @DTax <> 0 set @TDTax = @TDTax + @DTax;

      end 


      if @PType = 'S' and @TransType = 1		/* service sales */
      begin
        
        if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
          if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty); 
		  /*if @tax_inclusive = 'Y' begin
		       set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
        end
        else begin
          if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt; 
		  if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty); 
		  /*if @tax_inclusive = 'Y' begin
		       set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		    end*/
        end
          
        if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @Discnt;
		if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

		/*if @tax_inclusive = 'Y' begin
			set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		end*/

        set @STaxAmt1 = @STaxAmt1 + @Tx1;
        set @STaxAmt2 = @STaxAmt2 + @Tx2;
        set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
        if @Discnt <> 0					/* Discount on item */
        begin
          set @SDiscountItemNo = @SDiscountItemNo + 1;
          set @SDiscountItemAmount = @SDiscountItemAmount + @Discnt;
        end
        if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
      end
     
      if @Status = 15 begin
      
        set @TRentDeposit = @TRentDeposit + @RentDeposit;
        
        if @RentCalc = 'N' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (@item_TaxIncludePrice); 
		  /*if @tax_inclusive = 'Y' begin
			set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          set @RntTaxAmt1 = @RntTaxAmt1 + @Tx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @Tx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @Tx3;
          
          if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
        end
      end
      if @Status = 16 begin
        if @RentCalc = 'N' begin
		  set @TRentDepositReturned = @TRentDepositReturned + (-@TotalSale);
		end
        if @RentCalc = 'Y' begin
          if @tax_inclusive = 'N' set @RentSales = @RentSales + (@Qty*@Pr*@RentDuration) - @Discnt; 
		  if @tax_inclusive = 'Y' set @RentSales = @RentSales + (-@item_TaxIncludePrice); 
		  /*if @tax_inclusive = 'Y' begin
			set @RentSales = @RentSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		  end*/
          set @RntTaxAmt1 = @RntTaxAmt1 + @invtx1;
          set @RntTaxAmt2 = @RntTaxAmt2 + @invtx2;
          set @RntTaxAmt3 = @RntTaxAmt3 + @invtx3;
          set @TRentDepositReturned = @TRentDepositReturned + @RentDeposit;
          
           if @Fees <> 0 set @RentFees = @RentFees + @Fees;
          if @FeesTax <> 0 set @RentFeesTax = @RentFeesTax + @FeesTax;
        end
      end
     
     fetch next from csc1 into @InvNo,@TranNo,@PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@RetrnItm,@Status,@LayNo,@TransType,
     @TotalSale,@cost,@UOMCount,@Discnt,@RentDuration,@RentDeposit,@T1r,@T2r,@T3r,@RentCalc,@CpnPerc,
     @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@invtx1,@invtx2,@invtx3,@Fees,@FeesTax,@DTax,
	 @i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice


    end
    close csc1
    deallocate csc1 




    declare cscrpr1 cursor
    for select inv.RepairParentID from invoice inv left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
    in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)
    and inv.ID not in ( select invoiceno from VoidInv) and inv.Status = 18
			  
    open cscrpr1
    fetch next from cscrpr1 into @RprParentID
    
    while @@fetch_status = 0 begin
   
    
        declare cscrpr cursor
        for select i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2,i.Taxable3,i.Discount,i.TaxRate1,i.TaxRate2,i.TaxRate3,
        inv.CouponPerc, i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,
		i.TaxIncludeRate,i.TaxIncludePrice 
        from item i left outer join invoice inv on i.invoiceNo=inv.ID where inv.ID not in ( select invoiceno from VoidInv) and i.Tagged <> 'X'
        and inv.ID = @RprParentID
			  
       open cscrpr
       fetch next from cscrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
       @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
    
       while @@fetch_status = 0 begin
       
           set @RprTx1 = 0;
		   set @RprTx2 = 0;
           set @RprTx3 = 0;

           if (@RprT1='Y' or @RprT2='Y'or @RprT3='Y') and (@fstender = 'N')
           begin
             if @RprT1='Y' begin
               if @Tx1ty = 0 set @RprTx1 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT1r/100)*(100 - @CpnPerc)/100,2);
               if @Tx1ty = 1 set @RprTx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT2='Y' begin
               if @Tx2ty = 0 set @RprTx2 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT2r/100)*(100 - @CpnPerc)/100,2);
               if @Tx2ty = 1 set @RprTx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
             end
             if @RprT3='Y' begin
               if @Tx3ty = 0 set @RprTx3 = round(((@RprPr*@RprQty - @RprDiscnt)*@RprT3r/100)*(100 - @CpnPerc)/100,2);
               if @Tx3ty = 1 set @RprTx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
             end
           end
                     
            if @tax_inclusive = 'N' set @RepairSales = @RepairSales + (@RprPr*@RprQty) - @RprDiscnt; 
			if @tax_inclusive = 'Y' set @RepairSales = @RepairSales + (@item_TaxIncludePrice); 

            set @RTaxAmt1 = @RTaxAmt1 + @RprTx1;
            set @RTaxAmt2 = @RTaxAmt2 + @RprTx2;
            set @RTaxAmt3 = @RTaxAmt3 + @RprTx3;
        
           if @RprDiscnt <> 0					/* Discount on item */
           begin
             set @RDiscountItemNo = @RDiscountItemNo + 1;
             set @RDiscountItemAmount = @RDiscountItemAmount + @RprDiscnt;
           end
           
            if @Fees <> 0 set @RepairFees = @RepairFees + @Fees;
            if @FeesTax <> 0 set @RepairFeesTax = @RepairFeesTax + @RepairFeesTax;
           
           fetch next from cscrpr into @RprPr,@RprNPr,@RprQty,@RprT1,@RprT2,@RprT3,@RprDiscnt,@RprT1r,@RprT2r,@RprT3r,@CpnPerc,
           @Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@item_TaxIncludeRate,@item_TaxIncludePrice
        end
        close cscrpr
        deallocate cscrpr 
     
     fetch next from cscrpr1 into @RprParentID
     
     end
     close cscrpr1
     deallocate cscrpr1 



    declare xx2 cursor
    for select id from closeout where consolidatedID = @CloseoutID
    open xx2
    fetch next from xx2 into @tcid
    while @@fetch_status = 0 begin    
    
    declare csc2 cursor
    for select inv.ID,t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.[Status],inv.LayawayNo, t.TransType, inv.Coupon,inv.IsRentCalculated
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               where (1 = 1) and t.CloseOutID = @tcid
               and inv.ID not in ( select invoiceno from VoidInv)

    open csc2
    fetch next from csc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc

    while @@fetch_status = 0 begin
      
      if @LayNo = 0  
      begin

        if @Status = 3 set @NoOfSales = @NoOfSales + 1;
        
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1;
        
        if @Status = 15 or @Status = 16 set @RentInvoiceCount = @RentInvoiceCount + 1;
        
        if @Status = 18 set @RepairInvoiceCount = @RepairInvoiceCount + 1;
        
        
        if (@Discnt > 0) begin
          if @Status = 3 begin
            set @DiscountInvoiceNo = @DiscountInvoiceNo + 1;
            set @DiscountInvoiceAmount = @DiscountInvoiceAmount + @Discnt;
          end
          if (@Status = 15 and @RentCalc = 'N') or (@Status = 16 and @RentCalc = 'Y') begin
            set @RntDiscountInvoiceNo = @RntDiscountInvoiceNo + 1;
            set @RntDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
          if @Status = 18 begin
            set @RDiscountInvoiceNo = @RDiscountInvoiceNo + 1;
            set @RDiscountInvoiceAmount = @RDiscountInvoiceAmount + @Discnt;
          end
        end

      end  

      if @LayNo > 0 and @TransType = 4 and @Status = 3 begin   /* layaway items */ 

        set @NoOfSales = @NoOfSales + 1;   
        if @Status = 3 set @SalesInvoiceCount = @SalesInvoiceCount + 1;         
  
      end   
           
     fetch next from csc2 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @LayNo, @TransType, @Discnt,@RentCalc

    end

    close csc2
    deallocate csc2 
    
    
    
    fetch next from xx2 into @tcid

    end

    close xx2
    deallocate xx2 



    
    declare csc3 cursor
    for select inv.ID, t.ID, inv.Tax1, inv.Tax2, inv.Tax3,inv.Status, t.TransType, inv.Coupon
    from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
    where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
    and inv.LayawayNo > 0 and inv.ID not in ( select invoiceno from VoidInv)

    open csc3
    fetch next from csc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt
    while @@fetch_status = 0 begin

    if @TransType = 2  /* layaway Deposit */
    begin
      
        set @LayAmount = 0;
        select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
        set @LayawayDeposits = @LayawayDeposits + @LayAmount;
     
    end   

    if @TransType = 4  /* layaway Payment */

    begin
      
        if @Status = 1 or @Status = 3 

        begin
        
          set @LayAmount = 0;
             select @LayAmount = payment from laypmts where transactionno <> @TranNo and invoiceno = @InvNo
		     set @LayawayDeposits = @LayawayDeposits + @LayAmount;
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayPayment = @LayawayPayment + @LayAmount;

          if @Status = 3 begin /* layaway sales */

             set @LayawaySalesPosted = @LayawaySalesPosted + @LayAmount; 
             
             

             declare csc4 cursor
             for select i.ProductType, i.Price, i.NormalPrice, i.Qty, i.Taxable1, i.Taxable2, i.Taxable3,
             inv.Tax1, inv.Tax2, inv.Tax3, i.Discount,i.cost,i.UOMCount,i.TaxRate1,i.TaxRate2,i.TaxRate3,inv.CouponPerc,
             i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,i.Fees,i.FeesTax,
			 i.DTax,i.ProductID,i.SKU,i.BuyNGetFreeCategory,i.TaxIncludeRate,i.TaxIncludePrice from item i 
             left outer join invoice inv on i.invoiceNo=inv.ID
             left outer join trans t on t.ID=inv.TransactionNo where t.CloseOutID 
             in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and inv.ID=@InvNo and i.Tagged <> 'X'

             open csc4
             fetch next from csc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,
             @T1r,@T2r,@T3r,@CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,
			 @i_ProductID,@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice
             while @@fetch_status = 0 begin

			   if @FreeTag = 'F' begin			/* Buy 'n Get Free */
					set @FreeQty = @FreeQty + @Qty;
					set @FreeAmount = @FreeAmount + round((@NPr*@Qty),2);
				 end

               set @Tx1 = 0;
				 set @Tx2 = 0;
                 set @Tx3 = 0;

                 if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N')
                 begin
				  if @T1='Y' begin
                    if @Tx1ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx1 = round(((@Pr*@Qty - @LDiscnt)*@T1r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx1 = round(((@Pr*@Qty)*@T1r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx1ty = 1 set @Tx1 = round((@Tx1Tot)*(100 - @CpnPerc)/100,2);
                  end 
				  if @T2='Y' begin
                    if @Tx2ty = 0 begin
					  if @tax_inclusive = 'N' set @Tx2 = round(((@Pr*@Qty - @LDiscnt)*@T2r/100)*(100 - @CpnPerc)/100,2);
					  if @tax_inclusive = 'Y' set @Tx2 = round(((@Pr*@Qty)*@T2r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx2ty = 1 set @Tx2 = round((@Tx2Tot)*(100 - @CpnPerc)/100,2);
                  end 
                  if @T3='Y' begin
                    if @Tx3ty = 0 begin
					   if @tax_inclusive = 'N' set @Tx3 = round(((@Pr*@Qty - @LDiscnt)*@T3r/100)*(100 - @CpnPerc)/100,2);
					   if @tax_inclusive = 'Y' set @Tx3 = round(((@Pr*@Qty)*@T3r/100)*(100 - @CpnPerc)/100,2);
					end
                    if @Tx3ty = 1 set @Tx3 = round((@Tx3Tot)*(100 - @CpnPerc)/100,2);
                  end 
                 end         
               
				 if @PType = 'G' and @TransType = 1  /* gift cert sales */
			     begin
			       set @GCsold = @GCsold + @Pr*@Qty;
		         end   
		         
		         
		         if @PType = 'O' and @TransType = 1  /* bottle refund */
                 	begin
                   		set @BottleRefund = @BottleRefund + (-(@Pr*@Qty));
                 	end  
		         
		        	if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'MGC' and @TransType = 1  /* mercury gift cert sales */
				begin
					set @MGCSold = @MGCSold + @Pr;
				end  
				
				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PGC' and @TransType = 1  /* precidia gift cert sales */
				begin
					set @PGCSold = @PGCSold + @Pr;
				end

				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'DGC' and @TransType = 1  /* datacap gift cert sales */
				begin
					set @DGCSold = @DGCSold + @Pr;
				end

				if @PType = 'X' and @i_ProductID = 111 and @i_SKU = 'PLGC' and @TransType = 1  /* poslink gift cert sales */
				begin
					set @PLGCSold = @PLGCSold + @Pr;
				end

                 if @PType = 'A' and @TransType = 1  /* house account payment */
                 begin
                   set @HApayments = @HApayments + @Pr*@Qty;
                 end  

                if @PType <> 'A' and @PType <> 'G' and @PType <> 'O' and @PType <> 'C' and @PType <> 'Z' and @PType <> 'S'  and @PType <> 'H' and ( @PType <> 'X' and @i_ProductID <> 111 and @i_SKU <> 'MGC') and @TransType = 1  /* product sales */
                begin

                  if @PType = 'U'					/* cost of goods sold */
	              begin
					set @CostOfGoods = @CostOfGoods + @cost*@UOMCount; 
	        	  end
	
        	      if @PType <> 'U'
	              begin
  				    set @CostOfGoods = @CostOfGoods + @cost*@Qty;
	              end
	              
	              
	              if @PType = 'B'					/* other sales */
                  begin
                  
                    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
					  if @tax_inclusive = 'N' set @OtherTx = @OtherTx + (@Pr*@Qty) - @Discnt; 
					  if @tax_inclusive = 'Y' set @OtherTx = @OtherTx + (@item_TaxIncludeRate*@Qty);
					  /*if @tax_inclusive = 'Y' begin
						set @OtherTx = @OtherTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end*/
					end
					else begin
						if @tax_inclusive = 'N' set @OtherNTx = @OtherNTx + (@Pr*@Qty) - @Discnt;
						if @tax_inclusive = 'Y' set @OtherNTx = @OtherNTx + (@item_TaxIncludeRate*@Qty);
						/*if @tax_inclusive = 'Y' begin
						set @OtherNTx = @OtherNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
					end
          
					if @tax_inclusive = 'N' set @OtherSales = @OtherSales + (@Pr*@Qty) - @LDiscnt;
					if @tax_inclusive = 'Y' set @OtherSales = @OtherSales + (@item_TaxIncludeRate*@Qty);

					/*if @tax_inclusive = 'Y' begin
			          set @OtherSales = @OtherSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/
          
					set @BTaxAmt1 = @BTaxAmt1 + @Tx1;
					set @BTaxAmt2 = @BTaxAmt2 + @Tx2;
					set @BTaxAmt3 = @BTaxAmt3 + @Tx3;
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @BDiscountItemNo = @BDiscountItemNo + 1;
						set @BDiscountItemAmount = @BDiscountItemAmount + @LDiscnt;
					end
					
					if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
					if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				  end

                  if @PType <> 'B'				/*  product sales */
				  begin
				  
				    if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
						if @tax_inclusive = 'N' set @ProductTx = @ProductTx + (@Pr*@Qty) - @Discnt; 
						if @tax_inclusive = 'Y' set @ProductTx = @ProductTx + (@item_TaxIncludeRate*@Qty); 
						/*if @tax_inclusive = 'Y' begin
						set @ProductTx = @ProductTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
					end
					else begin
						if @tax_inclusive = 'N' set @ProductNTx = @ProductNTx + (@Pr*@Qty) - @Discnt; 
						if @tax_inclusive = 'Y' set @ProductNTx = @ProductNTx + (@item_TaxIncludeRate*@Qty); 
						/*if @tax_inclusive = 'Y' begin
						set @ProductNTx = @ProductNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
					end
          
                    if @tax_inclusive = 'N' set @ProductSales = @ProductSales + (@Pr*@Qty) - @LDiscnt; 
					if @tax_inclusive = 'Y' set @ProductSales = @ProductSales + (@item_TaxIncludeRate*@Qty); 

					/*if @tax_inclusive = 'Y' begin
			          set @ProductSales = @ProductSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		            end*/
          
					set @TaxAmt1 = @TaxAmt1 + @Tx1;
					set @TaxAmt2 = @TaxAmt2 + @Tx2;
					set @TaxAmt3 = @TaxAmt3 + @Tx3;
          
					if @LDiscnt <> 0					/* Discount on item */
					begin
						set @DiscountItemNo = @DiscountItemNo + 1;
						set @DiscountItemAmount = @DiscountItemAmount + @LDiscnt;
					end
          
					if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
					if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
				   end
				   
				   if @DTax <> 0 set @TDTax = @TDTax + @DTax;
				end
               
                if @PType = 'S' and @TransType = 1		/* service sales */
			    begin
			      if (@T1='Y' or @T2='Y'or @T3='Y') and (@fstender = 'N') begin
					if @tax_inclusive = 'N' set @ServiceTx = @ServiceTx + (@Pr*@Qty) - @Discnt; 
					if @tax_inclusive = 'Y' set @ServiceTx = @ServiceTx + (@item_TaxIncludeRate*@Qty); 
					/*if @tax_inclusive = 'Y' begin
						set @ServiceTx = @ServiceTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
				  end
				  else begin
					if @tax_inclusive = 'N' set @ServiceNTx = @ServiceNTx + (@Pr*@Qty) - @Discnt; 
					if @tax_inclusive = 'Y' set @ServiceNTx = @ServiceNTx + (@item_TaxIncludeRate*@Qty); 
					/*if @tax_inclusive = 'Y' begin
						set @ServiceNTx = @ServiceNTx - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
					  end */
				  end
          
                  if @tax_inclusive = 'N' set @ServiceSales = @ServiceSales + (@Pr*@Qty) - @LDiscnt;
				  if @tax_inclusive = 'Y' set @ServiceSales = @ServiceSales + (@item_TaxIncludeRate*@Qty);

				  /*if @tax_inclusive = 'Y' begin
			          set @ServiceSales = @ServiceSales - round((@Tx1Tot + @Tx2Tot + @Tx3Tot),2);
		          end*/

                  set @STaxAmt1 = @STaxAmt1 + @Tx1;
				  set @STaxAmt2 = @STaxAmt2 + @Tx2;
                  set @STaxAmt3 = @STaxAmt3 + @Tx3;
        
                  if @LDiscnt <> 0					/* Discount on item */
                  begin
                    set @SDiscountItemNo = @SDiscountItemNo + 1;
                    set @SDiscountItemAmount = @SDiscountItemAmount + @LDiscnt;
                  end
                  
                  if @Fees <> 0 set @SalesFees = @SalesFees + @Fees;
          if @FeesTax <> 0 set @SalesFeesTax = @SalesFeesTax + @FeesTax;
                end
     
            fetch next from csc4 into @PType,@Pr,@NPr,@Qty,@T1,@T2,@T3,@LTax1,@LTax2,@LTax3,@LDiscnt,@cost,@UOMCount,@T1r,@T2r,@T3r,
            @CpnPerc,@Tx1ty,@Tx2ty,@Tx3ty,@Tx1Tot,@Tx2Tot,@Tx3Tot,@fstender,@Fees,@FeesTax,@DTax,@i_ProductID,
			@i_SKU,@FreeTag,@item_TaxIncludeRate,@item_TaxIncludePrice

          end
          close csc4
          deallocate csc4
          
          end

        end

      end 

      if @TransType = 3  /* layaway Payment */
      begin 

        if @Status = 5  /* layaway Refund */
        begin
        
          set @LayAmount = 0;
          select @LayAmount = payment from laypmts where transactionno = @TranNo and invoiceno = @InvNo
          set @LayawayRefund = @LayawayRefund + @LayAmount;

        end

      end 
           
     fetch next from csc3 into @InvNo, @TranNo, @Tax1, @Tax2, @Tax3, @Status, @TransType, @Discnt

    end
    close csc3
    deallocate csc3 


    select @TaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @TaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)  and t.transtype = 1 
    and inv.ID not in ( select invoiceno from VoidInv)


    select @RntTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt1 = @RntTaxAmt1 + @STaxAmt1;
    
    select @RntTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt2 = @RntTaxAmt2 + @STaxAmt2;
    
    select @RntTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 15 and  inv.IsRentCalculated = 'N'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @STaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 16 and  inv.IsRentCalculated = 'Y'
    and inv.ID not in ( select invoiceno from VoidInv)
    
    set @RntTaxAmt3 = @RntTaxAmt3 + @STaxAmt3;
    
    select @RTaxAmt1 = isnull(sum(inv.Tax1),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt2 = isnull(sum(inv.Tax2),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)
    
    select @RTaxAmt3 = isnull(sum(inv.Tax3),0) from invoice inv left outer join trans t 
    on t.ID=inv.TransactionNo where t.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) and inv.status = 18
    and inv.ID not in ( select invoiceno from VoidInv)


  /* No Sale */

  select @NoSaleCount = count(*) from trans where TransType = 5 and CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID)


  /*  Paid outs */

  select @PaidOuts = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 6 and tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  
  /* Lotto Payout */

  select @LottoPayout = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  where tr.TransType = 60 and tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 


  /*  House account charged */

  select @HACharged = isnull(sum(TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='House Account' and inv.ID not in ( select invoiceno from VoidInv);

  /*
  /*  Store Credit redeemed */

  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='Store Credit ' and t.TenderAmount > 0 and inv.ID not in ( select invoiceno from VoidInv);
 

   /*  Store Credit issued */

  select @SCissued = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
  left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='Store Credit ' and t.TenderAmount < 0 and inv.ID not in ( select invoiceno from VoidInv);

  */

  select @SCissued = isnull(sum(StoreCreditAmount),0) from trans 
  where CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and TransType in (50,51);


  select @SCredeemed = isnull(sum(t.TenderAmount),0) from tender t left outer join trans tr on t.TransactionNo = tr.ID
 left outer join tendertypes tt on t.tendertype=tt.ID
  left outer join invoice inv on inv.transactionNo = tr.ID
  where tr.CloseOutID in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) 
  and tt.name='Store Credit' and inv.ID not in ( select invoiceno from VoidInv);

    
    declare xx1 cursor
    for select id from closeout where consolidatedID = @CloseoutID
    open xx1
    fetch next from xx1 into @tcid
    while @@fetch_status = 0 begin
    
      declare cscT cursor
      for select isnull(tx1.TaxName,''),isnull(tx2.TaxName,''),isnull(tx3.TaxName,'')
               from invoice inv left outer join trans t on t.ID=inv.TransactionNo 
               left outer join TaxHeader tx1 on tx1.ID = inv.TaxID1 
               left outer join TaxHeader tx2 on tx2.ID = inv.TaxID2 
               left outer join TaxHeader tx3 on tx3.ID = inv.TaxID3 
               where ( 1 = 1) and t.CloseOutID = @tcid
               and inv.ID not in ( select invoiceno from VoidInv )
               and (inv.TaxID1 > 0 or inv.TaxID2 > 0 or inv.TaxID3 > 0)

      open cscT
      fetch next from cscT into @Tax1Name,@Tax2Name,@Tax3Name
      while @@fetch_status = 0 begin

        if @Tax1Name <> '' or  @Tax2Name <> '' or @Tax1Name <> ''
        begin
          if @Tax1Name <> '' set  @Tax1Exist = 'Y';
          if @Tax2Name <> '' set  @Tax2Exist = 'Y';
          if @Tax3Name <> '' set  @Tax3Exist = 'Y';
        end   
        fetch next from cscT into @Tax1Name,@Tax2Name,@Tax3Name

       end

       close cscT
       deallocate cscT


       fetch next from xx1 into @tcid

    end

    close xx1
    deallocate xx1





        
           




    if @AcceptTips = 'Y' begin


        declare sth cursor
        for select distinct ID from closeout where CloseoutType = 'E' and ConsolidatedID = @CloseoutID 
        open sth
        fetch next from sth into @EmpCoutID
   
        while @@fetch_status = 0 begin
          
          set @dtTipEnd = null;
          set @dtTipStart = null;
          select @dtTipEnd = enddatetime, @Emp = createdby from closeout where ID = @EmpCoutID;
        if @dtTipEnd is null select @dtTipEnd = max(dayend) from attendanceinfo where empid = @Emp;
        
        select @dtTipStart = max(enddatetime) from closeout where createdby = @Emp and closeouttype = 'E' and ID < @EmpCoutID;
 
        if @dtTipStart is not null begin
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and dayend 
           between @dtTipStart and @dtTipEnd;	
        end

        if @dtTipStart is null begin
        
           select @dtTipStart = max(daystart) from attendanceinfo where empid = @Emp and daystart <= @dtTipEnd;
           select @CashTip = isnull(sum(cashtip),0) , @CCTip = isnull(sum(cctip),0) from attendanceinfo where empid = @Emp and 
           dayend between @dtTipStart and @dtTipEnd;	
        end

          set @TCashTip = @TCashTip + @CashTip;
          set @TCCTip = @TCCTip + @CCTip;
          
          fetch next from sth into @EmpCoutID

       end
       
       close sth 
       deallocate sth 
    
    end

    
    
    insert into CentralExportCloseoutReportMain (TaxedSales,NonTaxedSales,Tax1Amount,Tax2Amount,Tax3Amount,ServiceSales,
				ProductSales,OtherSales,DiscountItemNo,DiscountItemAmount,DiscountInvoiceNo,
				DiscountInvoiceAmount,LayawayDeposits,LayawayRefund,LayawayPayment,LayawaySalesPosted,
				GCsold,HApayments,PaidOuts,HACharged,SCissued,SCredeemed,NoOfSales,
				Tax1Name,Tax1Exist,Tax2Name,Tax2Exist,Tax3Name,Tax3Exist,CloseoutID,StartDateTime,EndDateTime,CloseoutType,
				Notes,EmpID,TerminalName,ReportTerminalName,TotalSales_PreTax,CostOfGoods,
				NoSaleCount,RentSales,RentDeposit,RentDepositReturned,RentTax1Amount,RentTax2Amount,RentTax3Amount,
				RentDiscountInvoiceNo,RentDiscountInvoiceAmount,RepairSales,RepairTax1Amount,RepairTax2Amount,RepairTax3Amount,
				RepairDiscountItemNo,RepairDiscountItemAmount,RepairDiscountInvoiceNo,RepairDiscountInvoiceAmount,ServiceTax1Amount,ServiceTax2Amount,
				ServiceTax3Amount,ServiceDiscountItemNo,ServiceDiscountItemAmount,OtherTax1Amount,OtherTax2Amount,OtherTax3Amount,
				OtherDiscountItemNo,OtherDiscountItemAmount,SalesInvoiceCount,RentInvoiceCount,RepairInvoiceCount,
				ProductTx,ProductNTx,ServiceTx,ServiceNTx,OtherTx,OtherNTx,CashTip,CCTip,
				SalesFees,SalesFeesTax,RentFees,RentFeesTax,RepairFees,RepairFeesTax,DTax,MGCsold,PGCsold,
				BottleRefund,DGCsold,PLGCsold,Free_Qty,Free_Amount,LottoPayout) 
				values
                (@TaxedSales,@NonTaxedSales,@TaxAmt1,@TaxAmt2,@TaxAmt3,@ServiceSales,
				@ProductSales,@OtherSales,@DiscountItemNo,@DiscountItemAmount,@DiscountInvoiceNo,
				@DiscountInvoiceAmount,@LayawayDeposits,@LayawayRefund,@LayawayPayment,@LayawaySalesPosted,
				@GCsold,@HApayments,@PaidOuts,@HACharged,@SCissued,@SCredeemed,@NoOfSales,
				@Tax1Name,@Tax1Exist,@Tax2Name,@Tax2Exist,@Tax3Name,@Tax3Exist,
				@CloseoutID,@SD,@ED,@CT,@Notes,@EmpID,@CTeml,@Terminal,@TTotalSale -@TaxAmt1-@TaxAmt2-@TaxAmt3,@CostOfGoods,@NoSaleCount,
				@RentSales,@TRentDeposit,@TRentDepositReturned,@RntTaxAmt1,@RntTaxAmt2,@RntTaxAmt3,@RntDiscountInvoiceNo,@RntDiscountInvoiceAmount,
				@RepairSales,@RTaxAmt1,@RTaxAmt2,@RTaxAmt3,@RDiscountItemNo,@RDiscountItemAmount,@RDiscountInvoiceNo,
				@RDiscountInvoiceAmount,@STaxAmt1,@STaxAmt2,@STaxAmt3,@SDiscountItemNo,@SDiscountItemAmount,@BTaxAmt1,@BTaxAmt2,
				@BTaxAmt3,@BDiscountItemNo,@BDiscountItemAmount,@SalesInvoiceCount,@RentInvoiceCount,@RepairInvoiceCount,
				@ProductTx,@ProductNTx,@ServiceTx,@ServiceNTx,@OtherTx,@OtherNTx,@TCashTip,@TCCTip,
				@SalesFees,@SalesFeesTax,@RentFees,@RentFeesTax,@RepairFees,@RepairFeesTax,@TDTax,@MGCSold,@PGCsold,
				@BottleRefund,@DGCsold,@PLGCsold,@FreeQty,@FreeAmount,@LottoPayout)  

  
     exec @r5 = sp_CentralExportCloseoutReportReturnItem @CloseoutType , @CloseoutID,@Terminal

     exec @r6 = sp_CentralExportCloseoutReportTender @CloseoutType , @CloseoutID,@Terminal

     exec @r7 = sp_CentralExportCloseoutSalesByHour @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal

     exec @r8 = sp_CentralExportCloseoutSalesByDept @tax_inclusive, @CloseoutType , @CloseoutID,@Terminal 


  
  end  






  

end

GO

GO

--
-- Create procedure [dbo].[sp_CentralExport_V2]
--
GO
--	For Specifically CloseOut Report


CREATE procedure [dbo].[sp_CentralExport_V2]
		@ExportType	varchar(3),
		@tax_inclusive	char(1),
		@Terminal		nvarchar(50)
as
--DECLARE @tax_inclusive char(1) ='Y';
--DECLARE @Terminal char(1) ='USAMA-GDELL';
begin
	DECLARE @cnt INT = 0;
	DECLARE @Counter INT = 1;
	DECLARE @CloseoutID INT;
	DECLARE @CloseoutType char(1);

	delete CentralExportCloseoutReportMain
	delete CentralExportCloseoutReturn
	delete CentralExportCloseoutReportTender
	delete CentralExportCloseOutSalesHour
	delete CentralExportCloseOutSalesDept




	select ROW_NUMBER() OVER(ORDER BY ID ASC) RowNum, CloseoutType , ID into #temp from CloseOut --where id>40
	
	select @cnt = count (*) from #temp
	--select @cnt
	--select * from #temp  order by id desc


	WHILE ( @Counter <= @cnt)
	BEGIN
		select @CloseoutID = ID, @CloseoutType=CloseoutType from #temp where RowNum=@Counter
		--select @Counter, @CloseoutID, @CloseoutType

		exec sp_CentralExportCloseoutReportHeader @tax_inclusive, @CloseoutType , @CloseoutID, @Terminal

		SET @Counter  = @Counter  + 1
	END



	drop table #temp
end


GO

GO

--
-- Alter procedure [dbo].[sp_CloseoutReportTender]
--
GO
ALTER procedure [dbo].[sp_CloseoutReportTender]
			@CloseoutType	char(1),
			@CloseoutID	int,
			@Terminal	nvarchar(50)
			with recompile
as
  
declare @TID 			int;
declare @TTName			nvarchar(40);
declare @TName			nvarchar(40);
declare @Count 			int;
declare @Amount 			numeric(18,3);
declare @TAmount			numeric(18,3);
declare @cashbackamt		numeric(18,3);
declare @cashbackcnt		int;
declare @cardprocessingamt		numeric(18,3);
declare @cardprocessingcnt		int;
declare	@LocalCloseoutType	char(1);
declare	@LocalCloseoutID		int;
declare	@LocalTerminal			nvarchar(50);

declare @cashfloatamt			numeric(18,3);
declare @boolCashTenderingExists char(1);
declare @CashTenderID int;

declare @ConsolidatedTerminal	int;

declare @safedropamt		numeric(18,3);
declare @paidinamt			numeric(18,3);
declare @paidoutamt			numeric(18,3);
declare @cashoutamt			numeric(18,3);
declare @cashinamt			numeric(18,3);

begin

   set @LocalCloseoutType = @CloseoutType;
   set @LocalCloseoutID	= @CloseoutID;
   set @LocalTerminal = @Terminal;

   set @cashbackamt = 0;
   set @cashbackcnt = 0;

   set @cardprocessingamt = 0;
   set @cardprocessingcnt = 0;
   set @cashfloatamt = 0;

   set @boolCashTenderingExists = 'N';
   set @CashTenderID = 0;
   set @ConsolidatedTerminal = 0;

   set @cashoutamt = 0;


   set @Count = 0;
   delete from CloseoutReportTender where ReportTerminalName = @LocalTerminal
  
  if @LocalCloseoutType = 'E' or @LocalCloseoutType = 'T' begin
   
    declare sc10 cursor
    for select ID, DisplayAs,Name from TenderTypes where name <> 'Store Credit' order by PaymentOrder
               
    open sc10
    fetch next from sc10 into @TID,@TName,@TTName
    while @@fetch_status = 0 begin

     select @Count = count(*) , @Amount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
     on tr.ID = t.TransactionNo 
     where t.TenderType = @TID and tr.CloseoutID = @LocalCloseoutID and tr.TransType not in (6,66,67,91,92)
     and tr.ID not in ( select transactionno from invoice where id in ( select invoiceno from VoidInv 
     where invoiceno not in ( select id from invoice where repairparentid = 0 and servicetype = 'Repair' )))
	 and tr.ID not in (select transactionno from invoice where id in (select invoiceno from VoidInv))
	 and tr.ID not in (select transactionno from invoice where RepairParentID in (select invoiceno from VoidInv))

	 if @LocalCloseoutType = 'T' and @TTName = 'Cash' and @Count = 0 begin
	   set @CashTenderID = @TID;
	   set @boolCashTenderingExists = 'N';
	   insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

	   select @cashfloatamt  = isnull(cashfloat,0) from CashFloat where CloseoutID = @CloseoutID;
			    insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

       select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

	  
	   select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);
	  
	   select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID = @CloseoutID);
	   insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

	 end
     if @Count > 0 begin
	    if @TTName = 'Credit Card' or @TTName = 'Debit Card' or @TTName = 'EBT' or @TTName = 'EBT Cash' begin
		  set @cardprocessingcnt = @cardprocessingcnt + @Count;
		  set @cardprocessingamt = @cardprocessingamt + @Amount;
		end
		     if @LocalCloseoutType = 'T' begin
			   if @TTName = 'Cash' begin
			        set @CashTenderID = @TID;
					set @boolCashTenderingExists = 'Y';
			        insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					select @cashfloatamt  = isnull(cashfloat,0) from CashFloat where CloseoutID = @CloseoutID;
			    insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

				select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

				select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

				select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

				select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

				select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID = @CloseoutID);
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
               end
			   else begin
			     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
			   end
			end
			else begin
			  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
			end
            
      end
      set @Count = 0;
     fetch next from sc10 into @TID,@TName,@TTName

    end

    close sc10
    deallocate sc10

	

	
	select @cashbackcnt = count(*) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and RefCardAuthAmount - CardAmount > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @LocalCloseoutID);

	select @cashbackamt = isnull(sum(RefCardAuthAmount - CardAmount),0) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @LocalCloseoutID);
    
	if (@cashbackcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99998,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@LocalTerminal)
	end
	/*
	select @cardprocessingcnt = count(*) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @CloseoutID);

	select @cardprocessingamt = isnull(sum(CardAmount),0) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID  = @CloseoutID);*/
    
	if (@cardprocessingcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99999,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@LocalTerminal)
	end

    
    declare sc11 cursor

    for select c.TenderID,c.TenderAmount,t.DisplayAs from CloseoutTender c left outer join tendertypes t on t.ID = c.TenderID 
    where c.CloseoutID=@LocalCloseoutID and t.name <> 'Store Credit' order by t.PaymentOrder
               
    open sc11
    fetch next from sc11 into @TID,@TAmount,@TName
    while @@fetch_status = 0 begin

     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('C',@LocalCloseoutID,@TID,@TName,@TAmount,@LocalTerminal)

     set @Count = 0;
     select @Amount = TenderAmount,@Count = TenderCount from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
     CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal

	 if @LocalCloseoutType = 'T' and @TID = @CashTenderID  begin
	   select @Amount = sum(TenderAmount),@Count = sum(TenderCount) from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
       CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal and TenderAmount != -929292 and TenderName not in ('  Cash Float', '  Cash In', '  Cash Out', '  Paid In', '  Paid Out')
	 end
     
     if @Count > 0
     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('R',@LocalCloseoutID,@TID,@TName,@TAmount-@Amount,@LocalTerminal)
     
     fetch next from sc11 into @TID,@TAmount,@TName

    end
    close sc11
    deallocate sc11

  end

  
   
  if @LocalCloseoutType = 'C' begin
   
    select @ConsolidatedTerminal = count(*) from CloseOut where CloseoutType = 'T' and ConsolidatedID = @CloseoutID;
    declare csc10 cursor
    for select ID, DisplayAs,Name from TenderTypes where name <> 'Store Credit' order by PaymentOrder
               
    open csc10
    fetch next from csc10 into @TID,@TName,@TTName
    while @@fetch_status = 0 begin

     select @Count = count(*) , @Amount = isnull(sum(t.tenderamount),0) from tender t left outer join trans tr
     on tr.ID = t.TransactionNo 
     where t.TenderType = @TID and tr.CloseoutID in 
     (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and tr.TransType not in (66,67,6,91,92)
     and tr.ID not in ( select transactionno from invoice where id in ( select invoiceno from VoidInv 
     where invoiceno not in ( select id from invoice where repairparentid = 0 and servicetype = 'Repair' )))
	 and tr.ID not in (select transactionno from invoice where id in (select invoiceno from VoidInv))
	 and tr.ID not in (select transactionno from invoice where RepairParentID in (select invoiceno from VoidInv))

	  if @ConsolidatedTerminal > 0 and @TTName = 'Cash' and @Count = 0 begin
	   set @CashTenderID = @TID;
	   set @boolCashTenderingExists = 'N';
	   insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

	   select @cashfloatamt  = isnull(sum(cashfloat),0) from CashFloat where CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T');
			    insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);

	  select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

	  select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);

	   select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

	  select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

	   select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);
	  
	 end

     if @Count > 0 begin
	         if @TTName = 'Credit Card' or @TTName = 'Debit Card' or @TTName = 'EBT' or @TTName = 'EBT Cash' begin
				set @cardprocessingcnt = @cardprocessingcnt + @Count;
				set @cardprocessingamt = @cardprocessingamt + @Amount;
		end

		if @ConsolidatedTerminal > 0 begin
		  
		   if @TTName = 'Cash' begin
			        set @CashTenderID = @TID;
					set @boolCashTenderingExists = 'Y';
			        insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName,-929292,@Count,@LocalTerminal);

					select @cashfloatamt  = isnull(sum(cashfloat),0) from CashFloat where CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T');
			        insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			        TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Float',@cashfloatamt,0,@LocalTerminal);


					select @cashinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 91 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash In',@cashinamt,0,@LocalTerminal);

					select @cashoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 92 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Cash Out',@cashoutamt,0,@LocalTerminal);


					select @paidinamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 66 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid In',@paidinamt,0,@LocalTerminal);

	  select @paidoutamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 6 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Paid Out',@paidoutamt,0,@LocalTerminal);

					select @safedropamt = isnull(sum(TenderAmount),0) from Tender where TransactionNo in (select id from Trans where TransType = 67 and CloseoutID in (Select ID from CloseOut where ConsolidatedID = @CloseoutID and CloseoutType = 'T'));
					insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
			    TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'  Safe Drop',@safedropamt,0,@LocalTerminal);

				insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,'   Tender' + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
               end
			   else begin
			     insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,
					TenderCount,ReportTerminalName)values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
			   end


		end
		else begin
		  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,@TID,@TName + '  (' + cast(@Count as varchar(5)) + ')',@Amount,@Count,@LocalTerminal);
		end

			
     end
     set @Count = 0;
     
     fetch next from csc10 into @TID,@TName,@TTName

    end

    close csc10
    deallocate csc10


	


	select @cashbackcnt = count(*) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and RefCardAuthAmount - CardAmount > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) );

	select @cashbackamt = isnull(sum(RefCardAuthAmount - CardAmount),0) from CardTrans where CardType = 'Debit Card' and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @LocalCloseoutID) );
    
	if (@cashbackcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99998,'Debit Card Cash Back',@cashbackamt,@cashbackcnt,@LocalTerminal)
	end

	/*
	select @cardprocessingcnt = count(*) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) );

	select @cardprocessingamt = isnull(sum(CardAmount),0) from CardTrans where CardType in ('Credit Card','Debit Card','EBT','EBT Cash') and TransactionType = 'Sale'
	and TransactionNo > 0 and TransactionNo in ( select id from Trans where CloseOutID 
               in (select cls.ID from closeout cls where cls.consolidatedID = @CloseoutID) );*/
	if (@cardprocessingcnt > 0)
	begin
	  insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,TenderCount,ReportTerminalName)
                       values('T',@LocalCloseoutID,99999,'Card Processing Total',@cardprocessingamt,@cardprocessingcnt,@LocalTerminal)
	end

    declare csc11 cursor

    for select c.TenderID,c.TenderAmount,t.DisplayAs from CloseoutTender c left outer join tendertypes t
    on t.ID = c.TenderID where c.CloseoutID=@LocalCloseoutID and t.name <> 'Store Credit' order by t.PaymentOrder
               
    open csc11
    fetch next from csc11 into @TID,@TAmount,@TName
    while @@fetch_status = 0 begin

      insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('C',@LocalCloseoutID,@TID,@TName,@TAmount,@LocalTerminal)

      set @Count = 0;
      
      select @Amount = TenderAmount,@Count=TenderCount from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
      CloseoutID in (select c.ID from closeout c where c.consolidatedID = @LocalCloseoutID) and ReportTerminalName = @LocalTerminal
     
	   if @ConsolidatedTerminal > 0 and @TID = @CashTenderID  begin
	   select @Amount = sum(TenderAmount),@Count = sum(TenderCount) from CloseoutReportTender where RecordType = 'T' and TenderID = @TID and 
       CloseoutID=@LocalCloseoutID and ReportTerminalName = @LocalTerminal and TenderAmount != -929292 and TenderName not in ('  Cash Float', '  Cash In', '  Cash Out', '  Paid In', '  Paid Out')
	 end

     if @Count > 0
      insert into CloseoutReportTender(RecordType,CloseOutID,TenderID,TenderName,TenderAmount,ReportTerminalName)
                            values('R',@LocalCloseoutID,@TID,@TName,@TAmount-@Amount,@LocalTerminal)
          
      set @Count = 0;    
      fetch next from csc11 into @TID,@TAmount,@TName

    end
    close csc11
    deallocate csc11

  end


end
GO

GO

--
-- Drop index [IX_PrintLabel_ProductID] from table [dbo].[PrintLabel]
--
DROP INDEX [IX_PrintLabel_ProductID] ON [dbo].[PrintLabel]
GO

GO

--
-- Drop index [IX_Kit_ItemID] from table [dbo].[Kit]
--
DROP INDEX [IX_Kit_ItemID] ON [dbo].[Kit]
GO

GO

--
-- Drop index [IX_Kit_KitID] from table [dbo].[Kit]
--
DROP INDEX [IX_Kit_KitID] ON [dbo].[Kit]
GO

GO

--
-- Drop index [IX_AttachTag_ItemID] from table [dbo].[AttachTag]
--
DROP INDEX [IX_AttachTag_ItemID] ON [dbo].[AttachTag]
GO

GO

--
-- Drop index [IX_AttachTag_TagID] from table [dbo].[AttachTag]
--
DROP INDEX [IX_AttachTag_TagID] ON [dbo].[AttachTag]
GO

GO

--
-- Drop index [IX_DiscountDate_DiscountID] from table [dbo].[DiscountDate]
--
DROP INDEX [IX_DiscountDate_DiscountID] ON [dbo].[DiscountDate]
GO

GO

--
-- Drop index [IX_TaxDetail_RefID] from table [dbo].[TaxDetail]
--
DROP INDEX [IX_TaxDetail_RefID] ON [dbo].[TaxDetail]
GO

GO

--
-- Alter procedure [dbo].[LoggedOnServer]
--
GO
ALTER procedure [dbo].[LoggedOnServer]
			@Count	int output
as
begin

  set nocount on
  create table #sp_who (
  spid		smallint,
  ecid		smallint,
  status	nchar(30),
  loginame	nchar(128),
  hostname	nchar(128),
  blk		char(5),
  dbname	nchar(128),
  cmd		nchar(100),
  request_id smallint)
  insert into #sp_who execute sp_who
  select @Count = count(*) from #sp_who where upper(dbname)='RETAIL2020DB'
  
end

go

update DbVersion set CurrentVersion ='10.000'

GO

GO

--
-- Commit Transaction
--
IF @@TRANCOUNT>0 COMMIT TRANSACTION
GO
 

