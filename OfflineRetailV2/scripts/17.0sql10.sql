Create procedure [dbo].[sp_co_imp_updttag_po]
as

begin

  update POHeader set ExpFlag = 'Y' where expflag = 'N';

end
GO