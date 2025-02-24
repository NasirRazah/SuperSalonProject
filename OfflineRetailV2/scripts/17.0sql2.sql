GO
Alter Table POHeader add ExpFlag char(1) null default 'N';
GO
update POHeader set ExpFlag = 'N';
GO