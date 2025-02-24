GO
alter table suspnded add UOM nvarchar(15) null default '';
GO
alter table workorder add UOM nvarchar(15) null default '';
GO
update suspnded set UOM = '';
GO
update workorder set UOM = '';
GO