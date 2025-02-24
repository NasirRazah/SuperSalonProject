GO
CREATE TABLE [dbo].[QuickBooksErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [int] NULL,
	[OperationType] [varchar](10) NULL,
	[TranType] [varchar](20) NULL,
	[TranID] [int] NULL,
	[TranDesc] [nvarchar](50) NULL,
	[OperationTime] [datetime] NULL,
 CONSTRAINT [PK_QuickBooksErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO