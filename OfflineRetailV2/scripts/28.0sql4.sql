GO
CREATE TABLE [dbo].[qbDStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SyncID] [int] NULL,
	[ProductID] [int] NULL,
	[StockQty] [numeric](15, 3) NULL,
	[InitialStockQty] [numeric](15, 3) NULL,
	[QBListID] [varchar](50) NULL,
 CONSTRAINT [PK_qbDStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[qbDStock] ADD  CONSTRAINT [DF_qbDStock_SyncID]  DEFAULT ((0)) FOR [SyncID]
GO

ALTER TABLE [dbo].[qbDStock] ADD  CONSTRAINT [DF_qbDStock_ProductID]  DEFAULT ((0)) FOR [ProductID]
GO

ALTER TABLE [dbo].[qbDStock] ADD  CONSTRAINT [DF_qbDStock_StockQty]  DEFAULT ((0)) FOR [StockQty]
GO

ALTER TABLE [dbo].[qbDStock] ADD  CONSTRAINT [DF_qbDStock_InitialStockQty]  DEFAULT ((0)) FOR [InitialStockQty]
GO