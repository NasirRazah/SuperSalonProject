GO

CREATE TABLE [dbo].[XEROErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [int] NULL,
	[OperationType] [varchar](10) NULL,
	[TranType] [varchar](20) NULL,
	[TranID] [int] NULL,
	[TranDesc] [nvarchar](50) NULL,
	[OperationTime] [datetime] NULL,
 CONSTRAINT [PK_XEROErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]

GO