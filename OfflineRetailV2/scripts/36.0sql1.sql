IF NOT EXISTS(SELECT 1 FROM ReceiptTemplateDefaultData where TemplateType = 'Item')
BEGIN
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Logo', 4, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Name', 5, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Business Address', 6, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Invoice Number', 11, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Date', 11, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Printer Name', 11, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Item and Qty', 11, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Text', 40, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Image', 41, N'Item')
INSERT [dbo].[ReceiptTemplateDefaultData] ( [GroupName], [SlNo], [TemplateType]) VALUES ( N'Separator', 42, N'Item')
END
GO