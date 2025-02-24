USE [Retail2020DB]
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[Invoice]') AND NAME='GiftAidFlag')
  ALTER TABLE [dbo].[Invoice] add [GiftAidFlag] [char](1) NULL default 'N';
GO


IF EXISTS(SELECT 1 FROM Invoice where GiftAidFlag is null)
BEGIN
update Invoice set GiftAidFlag = 'N' where GiftAidFlag is null
END
GO


IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CentralExportCloseOutReportMain]') AND NAME='GiftAid')
  ALTER TABLE [dbo].[CentralExportCloseOutReportMain] add [GiftAid] [numeric](15,3) NULL default 0;
GO


IF EXISTS(SELECT 1 FROM CentralExportCloseOutReportMain where GiftAid is null)
BEGIN
update CentralExportCloseOutReportMain set GiftAid = 0 where GiftAid is null
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[CloseOutReportMain]') AND NAME='GiftAid')
  ALTER TABLE [dbo].[CloseOutReportMain] add [GiftAid] [numeric](15,3) NULL default 0;
GO


IF EXISTS(SELECT 1 FROM CloseOutReportMain where GiftAid is null)
BEGIN
update CloseOutReportMain set GiftAid = 0 where GiftAid is null
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[SummaryData]') AND NAME='GiftAid')
  ALTER TABLE [dbo].[SummaryData] add [GiftAid] [numeric](15,3) NULL default 0;
GO


IF EXISTS(SELECT 1 FROM SummaryData where GiftAid is null)
BEGIN
update SummaryData set GiftAid = 0 where GiftAid is null
END
GO
