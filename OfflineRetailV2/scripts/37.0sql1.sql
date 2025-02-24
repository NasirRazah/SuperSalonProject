IF EXISTS(SELECT 1 FROM ReceiptTemplateDefaultData)
BEGIN
Update ReceiptTemplateDefaultData set TemplateType = 'Rent Item Issue' where TemplateType = 'Rent Issue'
Update ReceiptTemplateDefaultData set TemplateType = 'Rent Item Return' where TemplateType = 'Return Rent Item'
Update ReceiptTemplateDefaultData set TemplateType = 'Repair Item Receive' where TemplateType = 'Repair In'
Update ReceiptTemplateDefaultData set TemplateType = 'Repair Item Return' where TemplateType = 'Repair Deliver'
END
GO

IF EXISTS(SELECT 1 FROM ReceiptTemplateMaster)
BEGIN
Update ReceiptTemplateMaster set TemplateType = 'Rent Item Issue' where TemplateType = 'Rent Issue'
Update ReceiptTemplateMaster set TemplateType = 'Rent Item Return' where TemplateType = 'Return Rent Item'
Update ReceiptTemplateMaster set TemplateType = 'Repair Item Receive' where TemplateType = 'Repair In'
Update ReceiptTemplateMaster set TemplateType = 'Repair Item Return' where TemplateType = 'Repair Deliver'
END
GO