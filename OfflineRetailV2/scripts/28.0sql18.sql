CREATE TABLE [dbo].[WooCommerceErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [int] NULL,
	[OperationType] [varchar](10) NULL,
	[TranType] [varchar](20) NULL,
	[TranID] [int] NULL,
	[TranDesc] [nvarchar](50) NULL,
	[OperationTime] [datetime] NULL,
 CONSTRAINT [PK_WooCommerceErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WooCommerceInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WCommConsumerKey] [nvarchar](100) NULL,
	[WCommConsumerSecret] [nvarchar](100) NULL,
	[WCommStoreAddress] [nvarchar](100) NULL,
	[LastExportOn] [datetime] NULL,
	[LastProductExportOn] [datetime] NULL,
	[LastCustomerExportOn] [datetime] NULL,
	[LastDiscountExportOn] [datetime] NULL,
	[LastTaxExportOn] [datetime] NULL,
	[LastOrderExportOn] [datetime] NULL,
	[LastProductCategoryExportOn] [datetime] NULL,
 CONSTRAINT [PK_WooCommerceInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WooCommerceSyncLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
 CONSTRAINT [PK_WooCommerceSyncLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO
