GO
CREATE TABLE [dbo].[StoreStockXero](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SyncID] [int] NULL,
	[ProductID] [int] NULL,
	[StockQty] [numeric](15, 3) NULL,
	[InitialStockQty] [numeric](15, 3) NULL,
	[XeroID] [varchar](100) NULL,
 CONSTRAINT [PK_StoreStockXero] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StoreStockXero] ADD  CONSTRAINT [DF_StoreStockXero_SyncID]  DEFAULT ((0)) FOR [SyncID]
GO

ALTER TABLE [dbo].[StoreStockXero] ADD  CONSTRAINT [DF_StoreStockXero_ProductID]  DEFAULT ((0)) FOR [ProductID]
GO

ALTER TABLE [dbo].[StoreStockXero] ADD  CONSTRAINT [DF_StoreStockXero_StockQty]  DEFAULT ((0)) FOR [StockQty]
GO

ALTER TABLE [dbo].[StoreStockXero] ADD  CONSTRAINT [DF_StoreStockXero_InitialStockQty]  DEFAULT ((0)) FOR [InitialStockQty]
GO
