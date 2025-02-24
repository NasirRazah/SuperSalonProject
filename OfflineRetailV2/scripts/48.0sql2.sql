USE [Retail2020DB]
GO

IF NOT EXISTS (SELECT * FROM syscolumns
  WHERE ID=OBJECT_ID('[dbo].[ReceiptTemplateActive]') AND NAME='GiftAidTemplate')
  ALTER TABLE [dbo].[ReceiptTemplateActive] add [GiftAidTemplate] [int] NULL default 0;
GO


IF EXISTS(SELECT 1 FROM ReceiptTemplateActive where GiftAidTemplate is null)
BEGIN
update ReceiptTemplateActive set GiftAidTemplate = 0 where GiftAidTemplate is null
END
GO


IF NOT EXISTS(SELECT 1 FROM ReceiptTemplateDefaultData where TemplateType = 'Gift Aid Receipt')
BEGIN
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Aid Caption', 1, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 2, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 3, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 4, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Payment Ref ID', 5, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Name', 6, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Address', 7, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Aid Amount', 8, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Tender Amount', 9, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Change Due Amount', 10, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Certificate Balance', 11, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'EBT Balance', 12, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'House Account Balance', 13, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Store Credit Balance', 14, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Payment Reference', 15, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Date', 16, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'User Name', 17, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Till Name', 18, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Receipt Footer', 19, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Card Holder Copy', 20, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Merchant Copy', 21, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Barcode', 22, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 23, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 24, N'Gift Aid Receipt')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 25, N'Gift Aid Receipt')

END
GO

IF NOT EXISTS(SELECT 1 FROM ReceiptTemplateDummyData where TemplateType = 'Gift Aid Receipt')
BEGIN
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Gift Aid Caption',1,'*** Gift Aid ***', 'center', 'normal',12, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Logo',2,'Logo', 'center', 'normal',10, 90, 90  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Business Name',3,'Business Name', 'left', 'normal',12, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Business Address',4,'Business Address', 'left', 'normal',10, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Separator',5,'---- S E P A R A T O R ----', 'center', 'normal',8, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Payment Ref ID',6,'Payment Ref ID: 6547', 'left', 'normal',12, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Separator',7,'---- S E P A R A T O R ----', 'center', 'normal',8, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Name',8,'Name', 'left', 'normal',10, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Address',9,'Address', 'left', 'normal',10, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Gift Aid Amount',10,'Amount: £100.00', 'left', 'normal',11, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Separator',11,'---- S E P A R A T O R ----', 'center', 'normal',8, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Tender Amount',12,'Tender Amount', 'left', 'normal',10, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Change Due Amount',13,'Change Due Amount', 'left', 'normal',11, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Date',14,'Date', 'left', 'normal',10, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','User Name',15,'User Name', 'left', 'normal',8, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Till Name',16,'Till Name', 'left', 'normal',8, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Receipt Footer',17,'Receipt Footer', 'left', 'italic',9, 0, 0  );
Insert into ReceiptTemplateDummyData(TemplateType,GroupName,SL,GroupData,TextAlign,TextStyle,FontSize,CtrlWidth,CtrlHeight)
values('Gift Aid Receipt','Barcode',18,'Barcode', 'center', 'normal',12, 180, 40  );
END
GO

IF NOT EXISTS(SELECT 1 FROM ReceiptTemplateDefaultData where TemplateType = 'Closeout' and GroupName = 'Gift Aid Amount')
BEGIN
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Gift Aid Amount', 89, N'Closeout')
END
GO