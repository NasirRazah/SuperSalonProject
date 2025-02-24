GO
CREATE TABLE [dbo].[XeroInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[XeroCompanyName] [varchar](50) NULL,
	[XeroAppName] [varchar](50) NULL,
	[XeroClientId] [nvarchar](100) NULL,
	[XeroCallbackUrl] [nvarchar](100) NULL,
	[XeroAccessToken] [ntext] NULL,
	[XeroRefreshToken] [nvarchar](200) NULL,
	[XeroTenantId] [nvarchar](100) NULL,
	[LastProductExportOn] [datetime] NULL,
	[LastCustomerExportOn] [datetime] NULL,
	[LastTaxExportOn] [datetime] NULL,
	[LastOrderExportOn] [datetime] NULL,
	[XeroInventoryAssetAccountCode] [varchar](10) NULL,
	[XeroCOGSAccountCodePurchase] [varchar](10) NULL,
	[XeroCOGSAccountCodeSale] [varchar](10) NULL,
	[XeroAccountCodeSale] [varchar](10) NULL,
	[XeroAccountCodePurchase] [varchar](10) NULL,
	[XeroInventoryAdjustmentAccountCode] [varchar](10) NULL,
 CONSTRAINT [PK_XeroInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO