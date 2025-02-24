GO
Alter table setup Add BackupType int null default 1;
GO
Alter table setup Add BackupStorageType int null default 0;
GO
update setup set BackupType = 1, BackupStorageType = 0;
GO