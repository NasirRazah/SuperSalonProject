GO
IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[Setup]') AND NAME='PrintLogoInReceipt')
  ALTER TABLE [dbo].[Setup] add [PrintLogoInReceipt] char(1) NULL default 'N';
GO