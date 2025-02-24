GO

CREATE TABLE [dbo].[CentralExportCloseOut](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CloseoutID] [int] NULL,
	[CloseoutType] [char](1) NULL,
	[ConsolidatedID] [int] NULL,
	[StartDatetime] [datetime] NULL,
	[EndDatetime] [datetime] NULL,
	[Notes] [nvarchar](100) NULL,
	[TransactionCnt] [int] NULL,
	[EmployeeID] [nvarchar](12) NULL,
	[EmployeeName] [nvarchar](50) NULL,
	[TerminalName] [nvarchar](50) NULL,
	[ExpFlag] [char](1) NULL,
 CONSTRAINT [PK_CentralExportCloseOut] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CentralExportCloseOut] ADD  CONSTRAINT [DF_CentralExportCloseOut_ExpFlag]  DEFAULT ('N') FOR [ExpFlag]
GO

CREATE TABLE [dbo].[CentralExportCloseOutReportMain](
	[TaxedSales] [numeric](18, 3) NULL,
	[NonTaxedSales] [numeric](18, 3) NULL,
	[Tax1Exist] [char](1) NULL,
	[Tax1Name] [nvarchar](20) NULL,
	[Tax1Amount] [numeric](18, 3) NULL,
	[Tax2Exist] [char](1) NULL,
	[Tax2Name] [nvarchar](20) NULL,
	[Tax2Amount] [numeric](18, 3) NULL,
	[Tax3Exist] [char](1) NULL,
	[Tax3Name] [nvarchar](20) NULL,
	[Tax3Amount] [numeric](18, 3) NULL,
	[ServiceSales] [numeric](18, 3) NULL,
	[ProductSales] [numeric](18, 3) NULL,
	[OtherSales] [numeric](18, 3) NULL,
	[DiscountItemNo] [int] NULL,
	[DiscountItemAmount] [numeric](18, 3) NULL,
	[DiscountInvoiceNo] [int] NULL,
	[DiscountInvoiceAmount] [numeric](18, 3) NULL,
	[LayawayDeposits] [numeric](18, 3) NULL,
	[LayawayRefund] [numeric](18, 3) NULL,
	[LayawayPayment] [numeric](18, 3) NULL,
	[LayawaySalesPosted] [numeric](18, 3) NULL,
	[PaidOuts] [numeric](18, 3) NULL,
	[GCsold] [numeric](18, 3) NULL,
	[SCissued] [numeric](18, 3) NULL,
	[SCredeemed] [numeric](18, 3) NULL,
	[HACharged] [numeric](18, 3) NULL,
	[HApayments] [numeric](18, 3) NULL,
	[NoOfSales] [int] NULL,
	[CloseoutID] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[CloseoutType] [char](1) NULL,
	[Notes] [nvarchar](100) NULL,
	[EmpID] [nvarchar](12) NULL,
	[EmpName] [nvarchar](50) NULL,
	[TerminalName] [nvarchar](50) NULL,
	[TotalSales_PreTax] [numeric](18, 3) NULL,
	[CostOfGoods] [numeric](18, 3) NULL,
	[NoSaleCount] [int] NULL,
	[RentSales] [numeric](18, 3) NULL,
	[RentDeposit] [numeric](18, 3) NULL,
	[RentDepositReturned] [numeric](18, 3) NULL,
	[RentTax1Amount] [numeric](18, 3) NULL,
	[RentTax2Amount] [numeric](18, 3) NULL,
	[RentTax3Amount] [numeric](18, 3) NULL,
	[RentDiscountInvoiceNo] [int] NULL,
	[RentDiscountInvoiceAmount] [numeric](18, 3) NULL,
	[RepairSales] [numeric](18, 3) NULL,
	[RepairTax1Amount] [numeric](18, 3) NULL,
	[RepairTax2Amount] [numeric](18, 3) NULL,
	[RepairTax3Amount] [numeric](18, 3) NULL,
	[RepairDiscountItemNo] [int] NULL,
	[RepairDiscountItemAmount] [numeric](18, 3) NULL,
	[RepairDiscountInvoiceNo] [int] NULL,
	[RepairDiscountInvoiceAmount] [numeric](18, 3) NULL,
	[ServiceTax1Amount] [numeric](18, 3) NULL,
	[ServiceTax2Amount] [numeric](18, 3) NULL,
	[ServiceTax3Amount] [numeric](18, 3) NULL,
	[ServiceDiscountItemNo] [int] NULL,
	[ServiceDiscountItemAmount] [numeric](18, 3) NULL,
	[OtherTax1Amount] [numeric](18, 3) NULL,
	[OtherTax2Amount] [numeric](18, 3) NULL,
	[OtherTax3Amount] [numeric](18, 3) NULL,
	[OtherDiscountItemNo] [int] NULL,
	[OtherDiscountItemAmount] [numeric](18, 3) NULL,
	[SalesInvoiceCount] [int] NULL,
	[RentInvoiceCount] [int] NULL,
	[RepairInvoiceCount] [int] NULL,
	[ProductTx] [numeric](18, 3) NULL,
	[ProductNTx] [numeric](18, 3) NULL,
	[ServiceTx] [numeric](18, 3) NULL,
	[ServiceNTx] [numeric](18, 3) NULL,
	[OtherTx] [numeric](18, 3) NULL,
	[OtherNTx] [numeric](18, 3) NULL,
	[CashTip] [numeric](15, 3) NULL,
	[CCTip] [numeric](15, 3) NULL,
	[SalesFees] [numeric](15, 3) NULL,
	[SalesFeesTax] [numeric](15, 3) NULL,
	[RentFees] [numeric](15, 3) NULL,
	[RentFeesTax] [numeric](15, 3) NULL,
	[RepairFees] [numeric](15, 3) NULL,
	[RepairFeesTax] [numeric](15, 3) NULL,
	[DTax] [numeric](15, 3) NULL,
	[MGCsold] [numeric](15, 3) NULL,
	[BottleRefund] [numeric](15, 3) NULL,
	[PGCsold] [numeric](15, 3) NULL,
	[RepairDeposit] [numeric](15, 3) NULL,
	[DGCsold] [numeric](15, 3) NULL,
	[PLGCsold] [numeric](15, 3) NULL,
	[Free_Qty] [int] NULL,
	[Free_Amount] [numeric](15, 3) NULL,
	[LottoPayout] [numeric](15, 3) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [TaxedSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [NonTaxedSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ('N') FOR [Tax1Exist]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [Tax1Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ('N') FOR [Tax2Exist]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [Tax2Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ('N') FOR [Tax3Exist]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [Tax3Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ProductSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [DiscountItemNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [DiscountItemAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [DiscountInvoiceNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [DiscountInvoiceAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [LayawayDeposits]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [LayawayRefund]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [LayawayPayment]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [LayawaySalesPosted]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [PaidOuts]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [GCsold]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [SCissued]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [SCredeemed]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [HACharged]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [HApayments]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [NoOfSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [CloseoutID]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [TotalSales_PreTax]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [CostOfGoods]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentDeposit]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentDepositReturned]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentTax1Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentTax2Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentTax3Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentDiscountInvoiceNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentDiscountInvoiceAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairSales]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairTax1Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairTax2Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairTax3Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairDiscountItemNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairDiscountItemAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairDiscountInvoiceNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairDiscountInvoiceAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceTax1Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceTax2Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceTax3Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceDiscountItemNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceDiscountItemAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherTax1Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherTax2Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherTax3Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherDiscountItemNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherDiscountItemAmount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [SalesInvoiceCount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentInvoiceCount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairInvoiceCount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ProductTx]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ProductNTx]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceTx]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [ServiceNTx]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherTx]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [OtherNTx]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [CashTip]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [CCTip]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [SalesFees]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [SalesFeesTax]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentFees]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RentFeesTax]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairFees]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairFeesTax]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [DTax]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [MGCsold]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [BottleRefund]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [PGCsold]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [RepairDeposit]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [DGCsold]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [PLGCsold]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [Free_Qty]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [Free_Amount]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReportMain] ADD  DEFAULT ((0)) FOR [LottoPayout]
GO


CREATE TABLE [dbo].[CentralExportCloseOutReportTender](
	[RecordType] [char](1) NULL,
	[CloseOutID] [int] NULL,
	[CloseoutType] [char](1) NULL,
	[TenderName] [nvarchar](40) NULL,
	[TenderAmount] [numeric](18, 3) NULL,
	[TenderCount] [int] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CentralExportCloseOutReturn](
	[CloseOutID] [int] NULL,
	[CloseoutType] [char](1) NULL,
	[ReturnSKU] [nvarchar](16) NULL,
	[ReturnInvoiceNo] [int] NULL,
	[ReturnAmount] [numeric](18, 3) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReturn] ADD  DEFAULT ((0)) FOR [ReturnInvoiceNo]
GO

ALTER TABLE [dbo].[CentralExportCloseOutReturn] ADD  DEFAULT ((0)) FOR [ReturnAmount]
GO

CREATE TABLE [dbo].[CentralExportCloseOutSalesDept](
	[DeptID] [nvarchar](10) NULL,
	[DeptDesc] [nvarchar](30) NULL,
	[SalesAmount] [numeric](18, 3) NULL,
	[CloseoutID] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[CloseoutType] [char](1) NULL,
	[Notes] [nvarchar](100) NULL,
	[NoOfSales] [int] NULL,
	[EmpID] [nvarchar](12) NULL,
	[EmpName] [nvarchar](50) NULL,
	[TerminalName] [nvarchar](50) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CentralExportCloseOutSalesHour](
	[Timeinterval] [nvarchar](25) NULL,
	[SalesAmount] [numeric](18, 3) NULL,
	[CloseoutID] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[CloseoutType] [char](1) NULL,
	[Notes] [varchar](100) NULL,
	[NoOfSales] [int] NULL,
	[EmpID] [nvarchar](12) NULL,
	[EmpName] [nvarchar](50) NULL,
	[TerminalName] [nvarchar](50) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CentralExportCloseOutSalesHour] ADD  DEFAULT ((0)) FOR [SalesAmount]
GO

CREATE TABLE [dbo].[CentralExportPOHeader](
	[POHeaderID] [int] NULL,
	[OrderNo] [varchar](16) NULL,
	[OrderDate] [datetime] NULL,
	[VendorID] [varchar](10) NULL,
	[RefNo] [varchar](16) NULL,
	[ExpectedDeliveryDate] [datetime] NULL,
	[Priority] [varchar](10) NULL,
	[GrossAmount] [numeric](15, 4) NULL,
	[Freight] [numeric](15, 4) NULL,
	[Tax] [numeric](15, 4) NULL,
	[NetAmount] [numeric](15, 4) NULL,
	[SupplierInstructions] [varchar](250) NULL,
	[GeneralNotes] [varchar](250) NULL,
	[VendorMinOrderAmount] [numeric](15, 3) NULL,
	[VendorName] [varchar](30) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CentralExportReceiving](
	[RecvHeaderID] [int] NULL,
	[RecvDetailID] [int] NULL,
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
	[PriceA] [numeric](15, 3) NULL,
	[DQty] [numeric](15, 4) NULL,
	[DCost] [numeric](15, 4) NULL,
	[DFreight] [numeric](15, 4) NULL,
	[DTax] [numeric](15, 4) NULL,
	[ProductName] [varchar](150) NULL,
	[VendorPartNo] [varchar](16) NULL,
	[CheckClerk] [nvarchar](40) NULL,
	[RecvClerk] [nvarchar](40) NULL,
	[RecvClerkID] [nvarchar](40) NULL,
	[CheckClerkID] [nvarchar](40) NULL,
	[VendorID] [nvarchar](40) NULL,
	[VendorName] [varchar](150) NULL
) ON [PRIMARY]
GO