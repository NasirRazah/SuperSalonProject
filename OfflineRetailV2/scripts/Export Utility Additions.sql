
alter table trans add [DiscountFlag] [char](1) NULL  DEFAULT ('N')
update trans set [DiscountFlag]='N'
GO

alter table trans add [LayAwayCustomerFlag] [char](1) NULL DEFAULT ('N');
alter table trans add [LayAwayProductFlag] [char](1) NULL DEFAULT ('N');
GO
update trans set [LayAwayCustomerFlag]='N';
update trans set [LayAwayProductFlag]='N';
GO
alter table trans add	[TaxAnalysisFlag] [char](1) NULL DEFAULT ('N');
GO
update trans set [TaxAnalysisFlag]='N';

GO

alter table trans add [PackingListFlag] [char](1) NULL DEFAULT ('N')
GO
update trans set [PackingListFlag] = 'N'

GO
alter table product add [StockValuationFlag] [char](1) NULL DEFAULT ('N')
GO
update product set [StockValuationFlag] = 'N'

GO
alter table RecvDetail add [ExpFlag]  [char](1) NULL DEFAULT ('N')
GO
update RecvDetail set [ExpFlag]='N'
