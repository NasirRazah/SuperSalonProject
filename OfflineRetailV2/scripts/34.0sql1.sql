IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[ReceiptTemplateDefaultData]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[ReceiptTemplateDefaultData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NULL,
	[SlNo] [int] NULL,
	[TemplateType] [varchar](20) NULL,
 CONSTRAINT [PK_ReceiptTemplateDefaultData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
END

GO

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[ReceiptTemplateMaster]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[ReceiptTemplateMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [nvarchar](30) NULL,
	[TemplateType] [varchar](20) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL,
	[LastChangedOn] [datetime] NULL,
	[TemplateSize] [varchar](10) NULL,
 CONSTRAINT [PK_ReceiptTemplateMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
END

GO

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[ReceiptTemplateLinkData]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[ReceiptTemplateLinkData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NULL,
	[GroupSL] [int] NULL,
	[GroupSubSL] [int] NULL,
	[GroupData] [nvarchar](200) NULL,
	[TextAlign] [varchar](6) NULL,
	[TextStyle] [varchar](6) NULL,
	[FontSize] [int] NULL,
	[CtrlWidth] [int] NULL,
	[CtrlHeight] [int] NULL,
	[CustomImage] [image] NULL,
	[ShowHeader1] [char](1) NULL,
	[ShowHeader2] [char](1) NULL,
	[ShowHeader3] [char](1) NULL,
	[ShowHeader4] [char](1) NULL,
	[Header1Caption] [nvarchar](20) NULL,
	[Header2Caption] [nvarchar](20) NULL,
	[Header3Caption] [nvarchar](20) NULL,
	[Header4Caption] [nvarchar](20) NULL,
	[TemplateRefID] [int] NULL,
	[SL] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

GO

IF NOT EXISTS(SELECT 1 FROM ReceiptTemplateDefaultData)
BEGIN
  
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Reprint/Void Caption', 3, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 4, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 5, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 6, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Work Order Number', 7, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Work Order Date', 8, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent - Issue', 9, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent - Return', 10, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Number', 11, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 12, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 13, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 14, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 15, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Date of Birth', 16, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Item/Price Header', 17, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Item/Price Line', 18, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 19, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 20, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 21, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount', 22, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Ticket Discount Amount', 23, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Security Deposit Amount', 24, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 25, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 26, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Change Due Amount', 27, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Certificate Balance', 28, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'EBT Balance', 29, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Balance', 30, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Balance', 31, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Payment Reference', 32, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 33, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 34, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 35, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 36, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Holder Copy', 37, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Merchant Copy', 38, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 39, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 40, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 41, N'Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 42, N'Receipt')





INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 2, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Cancellation Caption', 3, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Reprinted Caption', 6, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt/Layaway Number', 7, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 8, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 9, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 10, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Item/Price Line', 11, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 12, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 13, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 14, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 15, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Payment Details', 16, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Balance Due Amount', 17, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Due Date', 18, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 19, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Balance', 20, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Signature Line', 21, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Payment Reference', 22, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 23, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 24, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 25, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Policy', 26, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Holder Copy', 27, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Merchant Copy', 28, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 29, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 30, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 31, N'Layaway')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 32, N'Layaway')





INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 2, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 3, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 4, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Work Order Number', 5, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 6, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 7, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 8, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Item/Price Line', 9, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 10, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 11, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 12, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount', 13, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Ticket Discount Amount', 14, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 15, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 16, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 17, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 18, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 19, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 20, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 21, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 22, N'WorkOrder')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 23, N'WorkOrder')





INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 2, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 3, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 4, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Suspended Number', 5, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 6, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 7, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 8, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Date of Birth', 9, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Item/Price Line', 10, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 11, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 12, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 13, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount', 14, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Ticket Discount Amount', 15, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 16, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 17, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 18, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 19, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 20, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 21, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 22, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 23, N'Suspend Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 24, N'Suspend Receipt')




INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Receipt Caption', 2, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Number', 6, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 7, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 8, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 9, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 10, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Details', 11, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 12, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 13, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 14, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 15, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 16, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 17, N'Gift Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 18, N'Gift Receipt')




INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent Issue Caption', 2, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Number', 6, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 7, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 8, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 9, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 10, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Details', 11, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 12, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 13, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 14, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount', 15, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Ticket Discount Amount', 16, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Security Deposit Amount', 17, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 18, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 19, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Change Due Amount', 20, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Certificate Balance', 21, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'EBT Balance', 22, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Balance', 23, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Balance', 24, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Payment Reference', 25, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 26, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 27, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 28, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 29, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Holder Copy', 30, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Merchant Copy', 31, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 32, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 33, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 34, N'Rent Issue')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 35, N'Rent Issue')







INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent Return Caption', 2, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Number', 6, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 7, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 8, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 9, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 10, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Details', 11, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 12, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Security Deposit Amount', 13, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Return Deposit Amount', 14, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Due Amount', 15, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 16, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 17, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Change Due Amount', 18, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 19, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 20, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 21, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 22, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 23, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 24, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 25, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 26, N'Return Rent Item')





INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Issue Caption', 2, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Number', 6, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Date In', 7, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Delivery Date', 8, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Notified Date', 9, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 10, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 11, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 12, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 13, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Phone', 14, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Item Name', 15, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Item Serial No', 16, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Item Problem', 17, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Details', 18, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 19, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 20, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 21, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount', 22, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Security Deposit Amount', 23, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Due Amount', 24, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 25, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 26, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Change Due Amount', 27, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Certificate Balance', 28, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'EBT Balance', 29, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Balance', 30, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Balance', 31, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Payment Reference', 32, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Agree Line', 33, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Signature Line', 34, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 35, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 36, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 37, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer/Repair Disclaimer', 38, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Holder Copy', 39, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Merchant Copy', 40, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 41, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 42, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 43, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 44, N'Repair In')







INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Deliver Caption', 2, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Number', 6, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Reference Invoice Number', 7, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 8, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 9, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Code', 10, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Address', 11, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Phone', 12, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Item Name', 13, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Item Serial No', 14, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Details', 15, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Subtotal Amount', 16, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Amount', 17, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Amount', 18, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount', 19, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Security Deposit Amount', 20, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Due Amount', 21, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 22, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 23, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Change Due Amount', 24, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Certificate Balance', 25, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'EBT Balance', 26, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Balance', 27, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Balance', 28, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Payment Reference', 29, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Problem', 30, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Notes', 31, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Signature Line', 32, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 33, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 34, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 35, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer/Repair Disclaimer', 36, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Holder Copy', 37, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Merchant Copy', 38, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 39, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 40, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 41, N'Repair Deliver')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 42, N'Repair Deliver')





INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'No Sale Caption', 2, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 6, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 7, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 8, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 9, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 10, N'No Sale')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 11, N'No Sale')



INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid Out Caption', 2, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid Out Explanation', 6, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid Out Amount', 7, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 8, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 9, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 10, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 11, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 12, N'Paid Out')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 13, N'Paid Out')



INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid In Caption', 2, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid In Explanation', 6, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid In Amount', 7, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 8, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 9, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 10, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 11, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 12, N'Paid In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 13, N'Paid In')



INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Safe Drop Caption', 2, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Safe Drop Amount', 6, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 7, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 8, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 9, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 10, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 11, N'Safe Drop')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 12, N'Safe Drop')




INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Lotto Payout Caption', 2, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 3, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 4, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 5, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Lotto Payout Explanation', 6, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Lotto Payout Amount', 7, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Date', 8, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 9, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 10, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 11, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 12, N'Lotto Payout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 13, N'Lotto Payout')



INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Company', 1, N'Customer Label')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Name', 2, N'Customer Label')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Customer Mail Address', 3, N'Customer Label')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 4, N'Customer Label')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 5, N'Customer Label')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 6, N'Customer Label')








INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 2, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 3, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 4, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Report Caption', 5, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Type', 6, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Transaction Summary', 7, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Begin Date/Time', 8, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout End Date/Time', 9, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Number', 10, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Notes', 11, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales Caption', 12, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'No of Invoices (Sales)', 13, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Product Sales Amount', 14, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Product Sales Amount(Taxed)', 15, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Product Sales Amount(Non Taxed)', 16, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Service Sales Amount', 17, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Service Sales Amount(Taxed)', 18, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Service Sales Amount(Non Taxed)', 19, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Other Sales Amount', 20, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Other Sales Amount(Taxed)', 21, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Other Sales Amount(Non Taxed)', 22, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Details (Sales)', 23, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Caption (Sales)', 24, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Details (Sales)', 25, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent Caption', 26, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'No of Invoices (Rent)', 27, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Net Issued (Rent)', 28, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Details (Rent)', 29, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent Deposit Amount', 30, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Rent Deposit Return Amount', 31, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Caption (Rent)', 32, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Details (Rent)', 33, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount (Rent)', 34, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Caption', 35, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'No of Invoices (Repair)', 36, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales Amount (Repair)', 37, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tax Details (Repair)', 38, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Repair Deposit Amount', 39, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Caption (Repair)', 40, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Discount Details (Repair)', 41, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount (Repair)', 42, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Non Sales Caption', 43, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Deposit', 44, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Refund', 45, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Payment', 46, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Layaway Sales Posted', 47, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Number of No Sale', 48, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Paid Out', 49, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Lotto Payout', 50, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Certificate Sold', 51, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Payment', 52, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Mercury/Datacap/PosLink Gift Card Sold', 53, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Bottle Refund', 54, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Caption', 55, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Issued', 56, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Redeemed', 57, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Caption', 58, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Charged', 59, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Payment', 60, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tips Details', 61, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Return Items', 62, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'No Return Caption', 63, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Total Amount', 64, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Details', 65, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Count Details', 66, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Over/Short Details', 67, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales by Hour Caption', 68, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Type (Sales by Hour)', 69, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Begin Date/Time (Sales by Hour)', 70, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout End Date/Time (Sales by Hour)', 71, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Number (Sales by Hour)', 72, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Notes (Sales by Hour)', 73, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales by Hour Details', 74, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales by Hour Total Amount', 75, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales by Department Caption', 76, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Type (Sales by Department)', 77, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Begin Date/Time (Sales by Department)', 78, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout End Date/Time (Sales by Department)', 79, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Number (Sales by Department)', 80, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Closeout Notes (Sales by Department)', 81, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales by Department Details', 82, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Sales by Department Total Amount', 83, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 84, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 85, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 86, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Reference Invoice Number', 27, N'Return Rent Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Transaction/ Advance Amount', 45, N'Repair In')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Free Items', 87, N'Closeout')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Fees Amount (Sales)', 88, N'Closeout')

END
GO