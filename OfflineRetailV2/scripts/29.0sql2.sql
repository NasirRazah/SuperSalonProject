GO
Alter table AutoProcessInfo Add CloudBackupFlag char(1) null default 'N';
GO
update AutoProcessInfo set CloudBackupFlag = 'N';
GO