create procedure [dbo].[sp_deleteprintertemplate]
			@ID		int,
			@ReturnID	int output
as

begin


  delete from ReceiptTemplateMaster where ID = @ID
  delete from ReceiptTemplateLinkData where TemplateRefID = @ID
  
    set @ReturnID = 0; 
    return 0;

end