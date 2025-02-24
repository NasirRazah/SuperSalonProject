GO

CREATE TABLE [dbo].[XEROSyncLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
 CONSTRAINT [PK_XEROSyncLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO