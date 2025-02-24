IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[PrinterTypes]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[PrinterTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PrinterType] [varchar](20) NULL,
	[CreatedBy] [int] NULL default 0,
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] default 0,
	[LastChangedOn] [datetime] NULL,
 CONSTRAINT [PK_PrinterTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[Printers]') AND NAME='PrinterTypeID')
  ALTER TABLE [dbo].[Printers] add [PrinterTypeID] [int] NULL default 0;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[ReceiptTemplateMaster]') AND NAME='PrinterTypeID')
  ALTER TABLE [dbo].[ReceiptTemplateMaster] add [PrinterTypeID] [int] NULL default 0;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[ReceiptTemplateMaster]') AND NAME='AttachedPrinterID')
  ALTER TABLE [dbo].[ReceiptTemplateMaster] add [AttachedPrinterID] [int] NULL default 0;
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[ReceiptTemplateMaster]') AND NAME='PrintCopy')
  ALTER TABLE [dbo].[ReceiptTemplateMaster] add [PrintCopy] [int] NULL default 1;
GO