GO
Alter Table RecvHeader add ExpFlag char(1) null default 'N';
GO
update RecvHeader set ExpFlag = 'N';
GO