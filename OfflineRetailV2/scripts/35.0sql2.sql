IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[ReceiptTemplateActive]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[ReceiptTemplateActive](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptTemplate] [int] NULL,
	[LayawayTemplate] [int] NULL,
	[RentIssueTemplate] [int] NULL,
	[RentReturnTemplate] [int] NULL,
	[RepairInTemplate] [int] NULL,
	[RepairDeliverTemplate] [int] NULL,
	[WorkOrderTemplate] [int] NULL,
	[SuspendReceiptTemplate] [int] NULL,
	[CloseoutTemplate] [int] NULL,
	[NoSaleTemplate] [int] NULL,
	[PaidOutTemplate] [int] NULL,
	[PaidInTemplate] [int] NULL,
	[SafeDropTemplate] [int] NULL,
	[LottoPayoutTemplate] [int] NULL,
	[CustomerLabelTemplate] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[LastChangedBy] [int] NULL,
	[LastChangedOn] [datetime] NULL,
 CONSTRAINT [PK_ReceiptTemplateActive] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
END
GO
