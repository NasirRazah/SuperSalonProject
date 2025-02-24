ALTER procedure [dbo].[sp_addsecuritymodule]

as

declare @slcount int;
declare @count int;
declare @gid int;
begin

     select @count = count(*) from securitypermission;
     if @count <> 148 begin

     /*   Screen Access   */
     
     select @slcount = count(*) from securitypermission where SecurityCode = '1a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 1,1,'View','1a','Customer Screen',getdate());

     select @slcount = count(*) from securitypermission where SecurityCode = '1b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 2,1,'View','1b','Product Screen',getdate());

     select @slcount = count(*) from securitypermission where SecurityCode = '1c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 4,1,'View','1c','Reports Screen',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '1d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 5,1,'View','1d','Vendors Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 6,1,'View','1e','Reorder Reports Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 7,1,'View','1f','Purchase Order Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1g'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 8,1,'View','1g','Receiving Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1h'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 9,1,'View','1h','Print Labels Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1i'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 10,1,'View','1i','Employee Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1j'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 11,1,'View','1j','Shift Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1k'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 12,1,'View','1k','Holiday Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1l'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 13,1,'View','1l','Tax Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1m'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 14,1,'View','1m','Tender Type Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1n'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 15,1,'View','1n','Security Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1o'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 16,1,'View','1o','Department Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1p'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 17,1,'View','1p','Category Screen',getdate());

   /*select @slcount = count(*) from securitypermission where SecurityCode = '1q'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 18,1,'View','1q','ZipCode Screen',getdate());*/

   select @slcount = count(*) from securitypermission where SecurityCode = '1q'
   if @slcount = 0 
     insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
     values( 18,1,'View','1q','Scales',getdate());
       
   select @slcount = count(*) from securitypermission where SecurityCode = '1r'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 19,1,'View','1r','Group Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1s'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 20,1,'View','1s','Class Screen',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '1t'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 21,1,'View','1t','Dynamic CRM Screen',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '1u'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 22,1,'View','1u','Brand Screen',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '1v'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 23,1,'View','1v','Break Pack Screen',getdate());

select @slcount = count(*) from securitypermission where SecurityCode = '1w'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 24,1,'View','1w','StockTake Screen',getdate());


select @slcount = count(*) from securitypermission where SecurityCode = '1x'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 30,1,'View','1x','Central Office',getdate());
       
       
       select @slcount = count(*) from securitypermission where SecurityCode = '1xxd'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 40,1,'View','1xxd','Discounts / Promotions',getdate());
       
     select @slcount = count(*) from securitypermission where SecurityCode = '1z1'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 41,1,'View','1z1','Dashboard',getdate());



       
   /* Customer Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '11a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 101,11,'Customer','11a','Add a New Customer Record',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '11b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 102,11,'Customer','11b','Edit an Existing Customer Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '11c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 103,11,'Customer','11c','Delete a Customer Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '11d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 104,11,'Customer','11d','Print Customer Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '11e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 105,11,'Customer','11e','Add/Edit Customer Notes',getdate());

  
   select @slcount = count(*) from securitypermission where SecurityCode = '11f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 106,11,'Customer','11f','Change Customer Price Level',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '11g'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 107,11,'Customer','11g','Change Customer Shipping Address',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '11h'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 108,11,'Customer','11h','Change Customer Tax Exempt status',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '11i'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 109,11,'Customer','11i','Issue Customer Store Credit',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '11j'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 110,11,'Customer','11j','Add, Edit, Delete Access of Groups ',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '11k'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 111,11,'Customer','11k','Add, Edit, Delete Access of Classes ',getdate());


    select @slcount = count(*) from securitypermission where SecurityCode = '11l'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 112,11,'Customer','11l','Modify Store Credit',getdate());

	 select @slcount = count(*) from securitypermission where SecurityCode = '11m'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 113,11,'Customer','11m','Adjust House Acount',getdate());



   /* Employee Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '12a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 121,12,'Employee','12a','Add a New Employee Record',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '12b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 122,12,'Employee','12b','Edit an Existing Employee Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '12c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 123,12,'Employee','12c','Delete an Employee Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '12d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 124,12,'Employee','12d','Print Employee Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '12e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 125,12,'Employee','12e','Change Security Profile/Password',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '12f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 126,12,'Employee','12f','Update Attendance Record',getdate());



   /* Product Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '13a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 131,13,'Product','13a','Add a New Product Record',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '13b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 132,13,'Product','13b','Edit an Existing Product Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '13c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 133,13,'Product','13c','Delete a Product Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '13d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 134,13,'Product','13d','Print Product Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '13e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 135,13,'Product','13e','Change Product Cost',getdate());
 
   select @slcount = count(*) from securitypermission where SecurityCode = '13f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 136,13,'Product','13f','Change Product Price',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '13g'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 137,13,'Product','13g','Change On-Hand Quantity',getdate()); 



    select @slcount = count(*) from securitypermission where SecurityCode = '13h'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 138,13,'Product','13h','Add, Edit, Delete Access of Departments ',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '13i'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 139,13,'Product','13i','Add, Edit, Delete Access of Categories ',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '13j'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 140,13,'Product','13j','Add, Edit, Delete Access of Brands',getdate());



    /* Report Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '14a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 151,14,'Reports','14a','Customer - Detail Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 152,14,'Reports','14b','Customer - General Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14r'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 153,14,'Reports','14r','Customer - Other Report(sales,special reports etc.',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 157,14,'Reports','14c','Vendor - Detail Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 158,14,'Reports','14d','Vendor - General Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 160,14,'Reports','14e','Product - Detail Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 161,14,'Reports','14f','Product - General Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14g'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 162,14,'Reports','14g','Product - Kit Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14h'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 163,14,'Reports','14h','Product - Matrix Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14i'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 170,14,'Reports','14i','Employee - Detail Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14j'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 171,14,'Reports','14j','Employee - Attendance Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14k'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 172,14,'Reports','14k','Employee - Late Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14l'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 173,14,'Reports','14l','Employee - Absent Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14m'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 176,14,'Reports','14m','Ordering - Purchase Order Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14n'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 177,14,'Reports','14n','Ordering - Receiving Report',getdate());  

   select @slcount = count(*) from securitypermission where SecurityCode = '14o'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 178,14,'Reports','14o','Ordering - Reorder Report',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '14p'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 181,14,'Reports','14p','Sales - Sales Report',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '14q'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 182,14,'Reports','14q','Sales - Sales Summary Report',getdate());

    
    select @slcount = count(*) from securitypermission where SecurityCode = '14x'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 188,14,'Reports','14x','Sales - Card Transaction Report',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '14o1'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 189,14,'Reports','14o1','Ordering -Vendor Minimum Order Report',getdate());

   /* Scale Access */
       
  select @slcount = count(*) from securitypermission where SecurityCode = '15a'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 200,15,'Scales','15a','Maintenance (Add,Edit,Delete)',getdate());     

  select @slcount = count(*) from securitypermission where SecurityCode = '15b'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2001,15,'Scales','15b','Scale Printing',getdate());  

  select @slcount = count(*) from securitypermission where SecurityCode = '15c'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2002,15,'Scales','15c','Scale Setup',getdate());  

  select @slcount = count(*) from securitypermission where SecurityCode = '15d'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2003,15,'Scales','15d','Exit Scale Screen',getdate());  


  select @slcount = count(*) from securitypermission where SecurityCode = '15s1'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2004,15,'Scales','15s1','Change Unit Price',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '15s2'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2005,15,'Scales','15s2','Change Tare',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '15s3'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2006,15,'Scales','15s3','Change By Count',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '15s4'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2007,15,'Scales','15s4','Change Product Life',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '15s5'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2008,15,'Scales','15s5','Change Shelf Life',getdate());


 select @slcount = count(*) from securitypermission where SecurityCode = '15e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 2009,15,'Scales','15e','Markdown Settings',getdate()); 

 select @slcount = count(*) from securitypermission where SecurityCode = '15s6'
  if @slcount = 0 
    insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
    values( 2011,15,'Scales','15s6','Change Net Weight for Manual Entry',getdate());

  /* Setup Access (Add, Edit, Delete) */

   select @slcount = count(*) from securitypermission where SecurityCode = '20a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 201,20,'Setup','20a','Add, Edit, Delete Access of Tax, Security etc.',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '20b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 202,20,'Setup','20b','Exit Application',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '20c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 203,20,'Setup','20c','Start Data Purging',getdate());


  select @slcount = count(*) from securitypermission where SecurityCode = '20d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 204,20,'Setup','20d','Report Scheduler Access ',getdate());

  select @slcount = count(*) from securitypermission where SecurityCode = '20e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 205,20,'Setup','20e','Gware Host',getdate()); 

  select @slcount = count(*) from securitypermission where SecurityCode = '20f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 206,20,'Setup','20f','3rd Party Integration',getdate()); 

   select @slcount = count(*) from securitypermission where SecurityCode = '20g'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 207,20,'Setup','20g','Scale Settings',getdate()); 

	 

   /* Vendor Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '16a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 170,16,'Vendor','16a','Add a New Vendor Record',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '16b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 171,16,'Vendor','16b','Edit an Existing Vendor Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '16c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 172,16,'Vendor','16c','Delete a Vendor Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '16d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 173,16,'Vendor','16d','Print Vendor Record',getdate());


   /* PO Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '17a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 180,17,'Purchase Order','17a','Add a New Purchase Order Record',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '17b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 181,17,'Purchase Order','17b','Edit an Existing Purchase Order Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '17c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 182,17,'Purchase Order','17c','Delete a Purchase Order Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '17d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 183,17,'Purchase Order','17d','Print Purchase Order Record',getdate()); 

   
   /* Receiving Access */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '18a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 190,18,'Receiving','18a','Add a New Receiving Record',getdate());

   select @slcount = count(*) from securitypermission where SecurityCode = '18b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 191,18,'Receiving','18b','Edit an Existing Receiving Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '18c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 192,18,'Receiving','18c','Delete a Receiving Record',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '18d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 193,18,'Receiving','18d','Print Receiving Record',getdate());
       
       
       
    /* Close Out */ 


   select @slcount = count(*) from securitypermission where SecurityCode = '21a'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 211,21,'CloseOut','21a','Allow Blind Drop',getdate());
       
       declare c1 cursor
       for select ID from SecurityGroup
       open c1
       fetch next from c1 into @gid
       while @@fetch_status = 0 begin
         insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values(@gid,'21a','Y',0,getdate(),0,getdate());
         fetch next from c1 into @gid
       end
       close c1
       deallocate c1
       
     end

   select @slcount = count(*) from securitypermission where SecurityCode = '21b'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 212,21,'CloseOut','21b','Allow Blind Count',getdate());
       
       declare c2 cursor
       for select ID from SecurityGroup
       open c2
       fetch next from c2 into @gid
       while @@fetch_status = 0 begin
         insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values(@gid,'21b','Y',0,getdate(),0,getdate());
         fetch next from c2 into @gid
       end
       close c2
       deallocate c2
       
     end


   select @slcount = count(*) from securitypermission where SecurityCode = '21c'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 213,21,'CloseOut','21c','Allow Reconcile Count',getdate());
       
       declare c3 cursor
       for select ID from SecurityGroup
       open c3
       fetch next from c3 into @gid
       while @@fetch_status = 0 begin
         insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values(@gid,'21c','Y',0,getdate(),0,getdate());
         fetch next from c3 into @gid
       end
       close c3
       deallocate c3
       
     end
     
     select @slcount = count(*) from securitypermission where SecurityCode = '21d'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 214,21,'CloseOut','21d','Allow Other Terminals Closeout',getdate());
       
       declare c4 cursor
       for select ID from SecurityGroup
       open c4
       fetch next from c4 into @gid
       while @@fetch_status = 0 begin
         insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values(@gid,'21d','N',0,getdate(),0,getdate());
         fetch next from c4 into @gid
       end
       close c4
       deallocate c4
       
     end


	 select @slcount = count(*) from securitypermission where SecurityCode = '21e'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 215,21,'CloseOut','21e','Allow Cash Float',getdate());
       
       declare c4 cursor
       for select ID from SecurityGroup
       open c4
       fetch next from c4 into @gid
       while @@fetch_status = 0 begin
         insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values(@gid,'21e','N',0,getdate(),0,getdate());
         fetch next from c4 into @gid
       end
       close c4
       deallocate c4
       
     end


      select @slcount = count(*) from securitypermission where SecurityCode = '21f'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 218,21,'CloseOut','21f','Allow Cash In/Out',getdate());
       
       declare c4 cursor
       for select ID from SecurityGroup
       open c4
       fetch next from c4 into @gid
       while @@fetch_status = 0 begin
         insert into GroupPermission(GroupID,SecurityCode,PermissionFlag,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn)
         values(@gid,'21f','N',0,getdate(),0,getdate());
         fetch next from c4 into @gid
       end
       close c4
       deallocate c4
       
     end


   /*   POS Access   */
     
     select @slcount = count(*) from securitypermission where SecurityCode = '31a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 301,31,'POS','31a','View - Customer Screen ',getdate());

     select @slcount = count(*) from securitypermission where SecurityCode = '31b'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 302,31,'POS','31b','View - Product Screen',getdate());

     select @slcount = count(*) from securitypermission where SecurityCode = '31c'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 303,31,'POS','31c','View - Reports Screen',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31d'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 304,31,'POS','31d','View - Setup Screen',getdate());


			/*   Transaction   */

     select @slcount = count(*) from securitypermission where SecurityCode = '31e'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 305,31,'POS','31e','Paid-outs',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31f'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 306,31,'POS','31f','Acct Pay',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31g'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 307,31,'POS','31g','Sell Gift Certificates',getdate());


    select @slcount = count(*) from securitypermission where SecurityCode = '31h'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 308,31,'POS','31h','Return of Marchandise, Void Receipt',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31i'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 309,31,'POS','31i','Reprint Receipt',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31j'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 310,31,'POS','31j','Suspend/Resume Transaction',getdate());


     /*select @slcount = count(*) from securitypermission where SecurityCode = '31k'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 311,31,'POS','31k','Cancel Current Transaction',getdate());*/


     select @slcount = count(*) from securitypermission where SecurityCode = '31l'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 312,31,'POS','31l','Edit Item Description and Price on sale',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '31m'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 313,31,'POS','31m','Open Cash Drawer/No Sale',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31n'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 314,31,'POS','31n','Delete Item',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31o'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 319,31,'POS','31o','Work Order',getdate());


          /*   Tender */

    select @slcount = count(*) from securitypermission where SecurityCode = '31s'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 315,31,'POS','31s','Tender',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31p'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 316,31,'POS','31p','Tender - Selection of Gift Cert, Store Credit, Layaway as Payment Type',getdate());



     /*select @slcount = count(*) from securitypermission where SecurityCode = '31q'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 317,31,'POS','31q','Tender - Exit Tender Screen',getdate());*/

     select @slcount = count(*) from securitypermission where SecurityCode = '31r'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 318,31,'POS','31r','Tender - Sub-total, Discount',getdate());
 
          /*   Layaway  */
    
     select @slcount = count(*) from securitypermission where SecurityCode = '31u'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 321,31,'POS','31u','Layaway - Layaway Transaction',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31v'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 322,31,'POS','31v','Layaway - Refund for Layaway',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31w'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 323,31,'POS','31w','Layaway - Accept less than Minimum Deposit',getdate());

	/* POS function Buttons */

     select @slcount = count(*) from securitypermission where SecurityCode = '31z1'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 331,31,'POS','31z1','Function Key - Select/Customized Functions',getdate());

     select @slcount = count(*) from securitypermission where SecurityCode = '31z2'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 332,31,'POS','31z2','Function Key - Use Product Price Level',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31z3'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 333,31,'POS','31z3','Function Key - Change Product Price',getdate());

     select @slcount = count(*) from securitypermission where SecurityCode = '31z4'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 334,31,'POS','31z4','Function Key - Print Cust. Label',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '31z5'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 335,31,'POS','31z5','Function Key - Print Gift Receipt',getdate());


     select @slcount = count(*) from securitypermission where SecurityCode = '31z6'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 336,31,'POS','31z6','Function Key - Revert Card Transaction',getdate());

	select @slcount = count(*) from securitypermission where SecurityCode = '31z7'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 337,31,'POS','31z7','Function Key - Points to Store Credit',getdate());

     /* Appointments */



     select @slcount = count(*) from securitypermission where SecurityCode = '31a1'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 351,31,'POS','31a1','Appointment',getdate());

    select @slcount = count(*) from securitypermission where SecurityCode = '31a2'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 352,31,'POS','31a2','Can operate on other''s appointment',getdate());


    select @slcount = count(*) from securitypermission where SecurityCode = '31xx1'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 370,31,'POS','31xx1','Open Cash Drawer',getdate());
 
   
   
   select @slcount = count(*) from securitypermission where SecurityCode = '31xx2'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 371,31,'POS','31xx2','Allow Negative Amount in Credit Card & Debit Card Transaction',getdate());


   select @slcount = count(*) from securitypermission where SecurityCode = '31xx5'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 381,31,'POS','31xx5','Paid In',getdate());

	select @slcount = count(*) from securitypermission where SecurityCode = '31xx6'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 382,31,'POS','31xx6','Safe Drop',getdate());


   /* Labels and Signs  */

     select @slcount = count(*) from securitypermission where SecurityCode = '41a'
     if @slcount = 0 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 401,40,'Labels and Signs','41a','Label Format',getdate());

   /* Tablet */
	select @slcount = count(*) from securitypermission where SecurityCode = '41b'
    if @slcount = 0 begin 
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 501,50,'Tablet','41b','Shelf Tagged Batch',getdate());
    end else begin
	   update securitypermission set SecurityGroup = 'Tablet', SlNo = 501, GroupSlNo = 50 where SecurityCode = '41b' and GroupSlNo = 40;
	end
    select @slcount = count(*) from securitypermission where SecurityCode = '41c'
     if @slcount = 0 begin
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 502,50,'Tablet','41c','Production List',getdate()); 
	end 
	else begin
	  update securitypermission set SecurityGroup = 'Tablet', SlNo = 502, GroupSlNo = 50 where SecurityCode = '41c' and GroupSlNo = 40;
	end

	select @slcount = count(*) from securitypermission where SecurityCode = '51a'
     if @slcount = 0
       insert into securitypermission(SlNo,GroupSlNo,SecurityGroup,SecurityCode,SecurityDesc,CreatedOn)
       values( 503,50,'Tablet','51a','Markdown',getdate()); 
    

  end
     
end

