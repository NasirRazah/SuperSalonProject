GO
alter table product add dummyupc varchar(20) null;
GO
declare @rid		int;
declare @upc		varchar(20);
declare @sku		varchar(16);
declare @dummyval	varchar(20);
declare @dummylen	int;

declare sc cursor
for select ID, trim(UPC) from product  
open sc
fetch next from sc into @rid, @upc
while @@fetch_status = 0 begin
  update product set dummyupc = @upc where id = @rid;
  fetch next from sc into @rid, @upc
end
close sc
deallocate sc

declare sc1 cursor
for select ID, trim(SKU) from product  
open sc1
fetch next from sc1 into @rid, @sku
while @@fetch_status = 0 begin
  update product set UPC = @sku where id = @rid;
  fetch next from sc1 into @rid, @sku
end
close sc1
deallocate sc1

declare sc2 cursor
for select ID, dummyupc from product  
open sc2
fetch next from sc2 into @rid, @dummyval
while @@fetch_status = 0 begin
  set @dummylen = 0;
  set @dummylen = len(@dummyval);
  if @dummylen > 0 begin
    update product set SKU = SUBSTRING(@dummyval,1,16) where id = @rid;
  end
  fetch next from sc2 into @rid, @dummyval
end
close sc2
deallocate sc2
GO
alter table product drop column dummyupc;
GO