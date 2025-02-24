GO
CREATE TABLE [dbo].[XeroStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SyncID] [int] NULL,
	[ProductID] [int] NULL,
	[StockQty] [numeric](15, 3) NULL,
	[InitialStockQty] [numeric](15, 3) NULL,
	[XeroID] [varchar](100) NULL,
 CONSTRAINT [PK_XeroStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[XeroStock] ADD  CONSTRAINT [DF_XeroStock_SyncID]  DEFAULT ((0)) FOR [SyncID]
GO

ALTER TABLE [dbo].[XeroStock] ADD  CONSTRAINT [DF_XeroStock_ProductID]  DEFAULT ((0)) FOR [ProductID]
GO

ALTER TABLE [dbo].[XeroStock] ADD  CONSTRAINT [DF_XeroStock_StockQty]  DEFAULT ((0)) FOR [StockQty]
GO

ALTER TABLE [dbo].[XeroStock] ADD  CONSTRAINT [DF_XeroStock_InitialStockQty]  DEFAULT ((0)) FOR [InitialStockQty]
GO