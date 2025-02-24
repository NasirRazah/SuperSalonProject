Create procedure [dbo].[sp_co_imp_updttag_recv]
as

begin

  update RecvHeader set ExpFlag = 'Y' where expflag = 'N';

end
GO