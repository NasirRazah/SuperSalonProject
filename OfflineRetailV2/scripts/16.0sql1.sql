declare @iSeed int;
declare @RecCnt int;
declare @iFinalSeed int;
Select @RecCnt = count(*) from Invoice;
select @iSeed = IDENT_CURRENT('Invoice');
set @iFinalSeed = 0;
if (@iSeed = 1) and (@RecCnt = 0) set @iFinalSeed = 1;
else set @iFinalSeed = @iSeed + 1;


CREATE TABLE [dbo].[XeConnectLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HitOn] [datetime] NULL,
	[InvoiceNo] [int] NULL,
 CONSTRAINT [PK_XeConnectLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]

DBCC CHECKIDENT ('XeConnectLog', RESEED, @iFinalSeed);
GO
