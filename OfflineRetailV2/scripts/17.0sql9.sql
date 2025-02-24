create procedure [dbo].[sp_co_imp_updttag_closeout]
as

begin

  update CentralExportCloseOut set ExpFlag = 'Y' where expflag = 'N';

  delete from CentralExportCloseOutReturn;
  delete from CentralExportCloseOutReportMain;
  delete from CentralExportCloseOutReportTender;
  delete from CentralExportCloseOutSalesDept;
  delete from CentralExportCloseOutSalesHour;


end
GO