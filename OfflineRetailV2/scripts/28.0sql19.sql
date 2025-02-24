CREATE TABLE [dbo].[WooCommStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SyncID] [int] NULL,
	[ProductID] [int] NULL,
	[SubProductID] [int] NULL,
	[StockQty] [numeric](15, 3) NULL,
	[WooCommID] [int] NULL,
	[WooCommVariantID] [int] NULL,
	[InitialStockQty] [numeric](15, 3) NULL,
 CONSTRAINT [PK_WooCommStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_SyncID]  DEFAULT ((0)) FOR [SyncID]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_ProductID]  DEFAULT ((0)) FOR [ProductID]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_SubProductID]  DEFAULT ((0)) FOR [SubProductID]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_StockQty]  DEFAULT ((0)) FOR [StockQty]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_WooCommID]  DEFAULT ((0)) FOR [WooCommID]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_WooCommVariantID]  DEFAULT ((0)) FOR [WooCommVariantID]
GO

ALTER TABLE [dbo].[WooCommStock] ADD  CONSTRAINT [DF_WooCommStock_InitialStockQty]  DEFAULT ((0)) FOR [InitialStockQty]
GO