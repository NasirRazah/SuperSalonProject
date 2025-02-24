
GO
Alter table product add AddToPosCategoryScreen char(1) null default 'N';
GO
update product set AddToPosCategoryScreen = 'N';
GO
