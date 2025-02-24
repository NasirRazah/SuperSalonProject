GO
CREATE TABLE [dbo].[QuickBooksInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuickBooksWindowsCompanyFilePath] [nvarchar](255) NULL,
	[ItemIncomeAccount] [nvarchar](50) NULL,
	[ItemCOGSAccount] [nvarchar](50) NULL,
	[SalesTaxZero] [nvarchar](50) NULL,
	[SalesTaxNonZero] [nvarchar](50) NULL,
	[LastProductExportOn] [datetime] NULL,
	[LastCustomerExportOn] [datetime] NULL,
	[LastVendorExportOn] [datetime] NULL,
	[LastEmployeeExportOn] [datetime] NULL,
	[LastOrderExportOn] [datetime] NULL,
 CONSTRAINT [PK_QuickBooksInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO