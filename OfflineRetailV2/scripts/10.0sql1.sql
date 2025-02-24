CREATE TABLE [dbo].[XeConnectTransactions](
	[Id] [uniqueidentifier] NOT NULL,
	[TransactionId] [nvarchar](max) NOT NULL,
	[InvoiceNumber] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[StatusCode] [nvarchar](max) NOT NULL,
	[ApprovalCode] [nvarchar](max) NOT NULL,
	[AVSResult_ActualResult] [nvarchar](max) NULL,
	[AVSResult_PostalCodeResult] [nvarchar](max) NULL,
	[ProfileId] [nvarchar](max) NULL,
	[BatchId] [nvarchar](max) NULL,
	[CVResult] [nvarchar](max) NOT NULL,
	[StatusMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_XeConnectTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
update setup set EvoApiBaseAddress='http://localhost:5050/api/'
GO
Alter Table XeConnectTransactions add createdon datetime, TransType varchar(10), LayAwayNo INT, LayAwayInvoiceNo int
update XeConnectTransactions set createdon=convert( datetime, '01-01-1900'), TransType='', LayAwayNo=0
GO
