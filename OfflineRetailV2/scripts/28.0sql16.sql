CREATE procedure [dbo].[sp_UpdateQuickBooksWindowsTax]

as
declare @taxrate numeric(15,3);

begin

  
  set @taxrate = (select top(1) isnull(TaxRate,0) from TaxHeader where  Active = 'Yes');
  update TaxHeader set TaxRate = @taxrate where TaxName = 'QB-XEPOS Non Zero Tax';

end
GO