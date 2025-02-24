GO
CREATE TABLE [dbo].[QuickBooksSyncLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
 CONSTRAINT [PK_QuickBooksSyncLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO