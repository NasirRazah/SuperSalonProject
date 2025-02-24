GO
Alter Table Invoice Add QuickBooksCloudFlag	char(1) null default 'N';
GO
update Invoice set QuickBooksCloudFlag = 'N';
GO