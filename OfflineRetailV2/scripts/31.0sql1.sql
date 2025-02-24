GO
IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[Product]') AND NAME='DiscountedCost')
  ALTER TABLE [dbo].[Product] add [DiscountedCost] numeric(15,3) NULL default 0;
GO