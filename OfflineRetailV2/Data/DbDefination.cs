// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Collections;

namespace OfflineRetailV2.Data
{
    public class DbDefination
    {
        public static ArrayList arlLanguageTables = new ArrayList();
        public static ArrayList arlTables = new ArrayList();
        public static string strDbVersion = "63.000"; // DB Version

        // add script files

        public DbDefination()
        { }

        public static void SetAllArrayList(double currver)
        {
            arlTables.Clear();

            if (currver <= 2) arlTables.Add("2.0sql1.sql");
            if (currver <= 2) arlTables.Add("2.0sql2.sql");
            if (currver <= 2) arlTables.Add("2.0sql3.sql");
            if (currver <= 2) arlTables.Add("2.0sql4.sql");
            if (currver <= 2) arlTables.Add("2.0sql5.sql");
            if (currver <= 2) arlTables.Add("2.0sql6.sql");
            if (currver <= 2) arlTables.Add("2.0sql7.sql");
            if (currver <= 2) arlTables.Add("2.0sql8.sql");
            if (currver <= 2) arlTables.Add("2.0sql9.sql");

            if (currver <= 3) arlTables.Add("3.0sql1.sql");

            if (currver <= 4) arlTables.Add("4.0sql1.sql");

            if (currver <= 5) arlTables.Add("5.0sql1.sql");

            if (currver <= 6) arlTables.Add("6.0sql1.sql");
            if (currver <= 6) arlTables.Add("6.0sql2.sql");

            if (currver <= 7) arlTables.Add("7.0sql1.sql");

            // User Customization
            if (currver <= 8) arlTables.Add("8.0sql1.sql");

            // Evo Payment Parameter
            if (currver <= 9) arlTables.Add("9.0sql1.sql");
            if (currver <= 10) arlTables.Add("10.0sql1.sql");
            // WEB Report
            if (currver <= 12) arlTables.Add("12.0sql1.sql");

            //SMTP Column Type changes
            if (currver <= 13) arlTables.Add("13.0sql1.sql");

            //Log Table Column Type changes
            if (currver <= 14) arlTables.Add("14.0sql1.sql");

            // Web Office Import/Export Stored Procedures

            if (currver <= 15) arlTables.Add("15.0sql1.sql");
            if (currver <= 15) arlTables.Add("15.0sql2.sql");
            if (currver <= 15) arlTables.Add("15.0sql3.sql");
            if (currver <= 15) arlTables.Add("15.0sql4.sql");
            if (currver <= 15) arlTables.Add("15.0sql5.sql");
            if (currver <= 15) arlTables.Add("15.0sql6.sql");

            //Change Evo Invoice Picking Logic
            if (currver <= 16) arlTables.Add("16.0sql1.sql");
            if (currver <= 16) arlTables.Add("16.0sql2.sql");
            if (currver <= 16) arlTables.Add("16.0sql3.sql");

            // Web Host Closeout, PO and Receiving Sync

            if (currver <= 17) arlTables.Add("17.0sql1.sql");
            if (currver <= 17) arlTables.Add("17.0sql2.sql");
            if (currver <= 17) arlTables.Add("17.0sql3.sql");
            if (currver <= 17) arlTables.Add("17.0sql4.sql");
            if (currver <= 17) arlTables.Add("17.0sql5.sql");
            if (currver <= 17) arlTables.Add("17.0sql6.sql");
            if (currver <= 17) arlTables.Add("17.0sql7.sql");
            if (currver <= 17) arlTables.Add("17.0sql8.sql");
            if (currver <= 17) arlTables.Add("17.0sql9.sql");
            if (currver <= 17) arlTables.Add("17.0sql10.sql");
            if (currver <= 17) arlTables.Add("17.0sql11.sql");


            if (currver <= 18) arlTables.Add("18.0sql1.sql");

            // Paymentsense
            if (currver <= 19) arlTables.Add("19.0sql1.sql");
            if (currver <= 20) arlTables.Add("20.0sql1.sql");

            // Add AddToPosCategoryScreen
            if (currver <= 21) arlTables.Add("21.0sql1.sql");
            if (currver <= 22) arlTables.Add("22.0sql1.sql");
            if (currver <= 23) arlTables.Add("23.0sql1.sql");
            if (currver <= 24) arlTables.Add("24.0sql1.sql");

            // Void sp
            if (currver <= 25) arlTables.Add("25.0sql1.sql");

            // Add field to currency calc table
            if (currver <= 26) arlTables.Add("26.0sql1.sql");

            // Correct Central closeout tables and procedutes (faulty database update)
            if (currver <= 27) arlTables.Add("27.0sql1.sql");


            // Web Host Closeout, PO and Receiving Sync for correction

            /*arlTables.Add("19.0sql1.sql");
            arlTables.Add("19.0sql2.sql");
            arlTables.Add("19.0sql3.sql");
            arlTables.Add("19.0sql4.sql");
            arlTables.Add("19.0sql5.sql");
            arlTables.Add("19.0sql6.sql");
            arlTables.Add("19.0sql7.sql");
            arlTables.Add("19.0sql8.sql");
            arlTables.Add("19.0sql9.sql");
            arlTables.Add("19.0sql10.sql");
            arlTables.Add("19.0sql11.sql");
            arlTables.Add("19.0sql12.sql");
            arlTables.Add("19.0sql13.sql");
            arlTables.Add("19.0sql14.sql");
            arlTables.Add("19.0sql15.sql");
            arlTables.Add("19.0sql16.sql");
            arlTables.Add("19.0sql17.sql");*/

            //QuickBooks, XERO, WooCommerce integration

            if (currver <= 28) arlTables.Add("28.0sql1.sql");
            if (currver <= 28) arlTables.Add("28.0sql2.sql");
            if (currver <= 28) arlTables.Add("28.0sql3.sql");
            if (currver <= 28) arlTables.Add("28.0sql4.sql");
            if (currver <= 28) arlTables.Add("28.0sql5.sql");
            if (currver <= 28) arlTables.Add("28.0sql6.sql");
            if (currver <= 28) arlTables.Add("28.0sql7.sql");
            if (currver <= 28) arlTables.Add("28.0sql8.sql");
            if (currver <= 28) arlTables.Add("28.0sql9.sql");
            if (currver <= 28) arlTables.Add("28.0sql10.sql");

            if (currver <= 28) arlTables.Add("28.0sql11.sql");
            if (currver <= 28) arlTables.Add("28.0sql12.sql");
            if (currver <= 28) arlTables.Add("28.0sql13.sql");
            if (currver <= 28) arlTables.Add("28.0sql14.sql");
            if (currver <= 28) arlTables.Add("28.0sql15.sql");
            if (currver <= 28) arlTables.Add("28.0sql16.sql");
            if (currver <= 28) arlTables.Add("28.0sql17.sql");
            if (currver <= 28) arlTables.Add("28.0sql18.sql");
            if (currver <= 28) arlTables.Add("28.0sql19.sql");
            if (currver <= 28) arlTables.Add("28.0sql20.sql");

            if (currver <= 28) arlTables.Add("28.0sql21.sql");
            if (currver <= 28) arlTables.Add("28.0sql22.sql");
            if (currver <= 28) arlTables.Add("28.0sql23.sql");
            if (currver <= 28) arlTables.Add("28.0sql24.sql");
            if (currver <= 28) arlTables.Add("28.0sql25.sql");
            if (currver <= 28) arlTables.Add("28.0sql26.sql");
            if (currver <= 28) arlTables.Add("28.0sql27.sql");
            if (currver <= 28) arlTables.Add("28.0sql28.sql");
            if (currver <= 28) arlTables.Add("28.0sql29.sql");
            if (currver <= 28) arlTables.Add("28.0sql30.sql");

            if (currver <= 28) arlTables.Add("28.0sql31.sql");
            if (currver <= 28) arlTables.Add("28.0sql32.sql");
            if (currver <= 28) arlTables.Add("28.0sql33.sql");
            if (currver <= 28) arlTables.Add("28.0sql34.sql");
            if (currver <= 28) arlTables.Add("28.0sql35.sql");
            if (currver <= 28) arlTables.Add("28.0sql36.sql");
            if (currver <= 28) arlTables.Add("28.0sql37.sql");
            if (currver <= 28) arlTables.Add("28.0sql38.sql");
            if (currver <= 28) arlTables.Add("28.0sql39.sql");
            if (currver <= 28) arlTables.Add("28.0sql40.sql");

            if (currver <= 28) arlTables.Add("28.0sql41.sql");

            // DB Backup Type
            if (currver <= 29) arlTables.Add("29.0sql1.sql");
            if (currver <= 29) arlTables.Add("29.0sql2.sql");

            // Host Sync Add HostID in CentralExportImport Table
            if (currver <= 30) arlTables.Add("30.0sql1.sql");

            // Add Discounted Cost in Product table

            if (currver <= 31) arlTables.Add("31.0sql1.sql");
            if (currver <= 32) arlTables.Add("32.0sql1.sql");

            // Add Print Logo in Receipt flag
            if (currver <= 33) arlTables.Add("33.0sql1.sql");

            // Printer Template Customisation
            if (currver <= 34) arlTables.Add("34.0sql1.sql");

            if (currver <= 35) arlTables.Add("35.0sql1.sql");
            if (currver <= 35) arlTables.Add("35.0sql2.sql");
            if (currver <= 35) arlTables.Add("35.0sql3.sql");

            if (currver <= 36) arlTables.Add("36.0sql1.sql");
            if (currver <= 37) arlTables.Add("37.0sql1.sql");

            if (currver <= 38) arlTables.Add("38.0sql1.sql");

            // Discount Related stored procedures changed 
            if (currver <= 39) arlTables.Add("39.0sql1.sql");

            // Add Gift Certificate's New Feature 
            if (currver <= 40) arlTables.Add("40.0sql1.sql");

            // Option for duplate Gift Cert
            if (currver <= 41) arlTables.Add("41.0sql1.sql");

            // Data Purge
            if (currver <= 42) arlTables.Add("42.0sql1.sql");

            // 1 Up Label Template Customization
            if (currver <= 43) arlTables.Add("43.0sql1.sql");

            // Add field in log table
            if (currver <= 43) arlTables.Add("43.0sql2.sql");

            // Add Gmail Email Setup
            if (currver <= 44) arlTables.Add("44.0sql1.sql");


            // Add Default Receipt Template Design
            if (currver <= 45) arlTables.Add("45.0sql1.sql");
            if (currver <= 46) arlTables.Add("46.0sql1.sql");


            if (currver <= 47) arlTables.Add("47.0sql1.sql");

            // Gift Aid
            if (currver <= 48) arlTables.Add("48.0sql1.sql");
            if (currver <= 48) arlTables.Add("48.0sql2.sql");
            if (currver <= 48) arlTables.Add("48.0sql3.sql");
            if (currver <= 48) arlTables.Add("48.0sql4.sql");

            if (currver <= 49) arlTables.Add("49.0sql1.sql");

            if (currver <= 50) arlTables.Add("50.0sql1.sql");
            if (currver <= 50) arlTables.Add("50.0sql2.sql");


            if (currver <= 51) arlTables.Add("51.0sql1.sql");
            if (currver <= 51) arlTables.Add("51.0sql2.sql");
            if (currver <= 51) arlTables.Add("51.0sql3.sql");
            if (currver <= 51) arlTables.Add("51.0sql4.sql");
            if (currver <= 51) arlTables.Add("51.0sql5.sql");

            if (currver <= 52) arlTables.Add("52.0sql1.sql");

            if (currver <= 53) arlTables.Add("53.0sql1.sql");

            if (currver <= 54) arlTables.Add("54.0sql1.sql");

            // Reset Training
            if (currver <= 55) arlTables.Add("55.0sql1.sql");

            if (currver <= 56) arlTables.Add("56.0sql1.sql");
            if (currver <= 56) arlTables.Add("56.0sql2.sql");
            if (currver <= 56) arlTables.Add("56.0sql3.sql");

            // Shopify

            if (currver <= 57) arlTables.Add("57.0sql1.sql");
            if (currver <= 57) arlTables.Add("57.0sql2.sql");
            if (currver <= 57) arlTables.Add("57.0sql3.sql");

            if (currver <= 58) arlTables.Add("58.0sql1.sql");

            // Xero Change Customer Field langth

            if (currver <= 59) arlTables.Add("59.0sql1.sql");
            if (currver <= 59) arlTables.Add("59.0sql2.sql");

            if (currver <= 60) arlTables.Add("60.0sql1.sql");
            if (currver <= 60) arlTables.Add("60.0sql2.sql");

            // Change logic for Closeout Host Sync

            if (currver <= 61) arlTables.Add("61.0sql1.sql");
            if (currver <= 61) arlTables.Add("61.0sql2.sql");
            if (currver <= 61) arlTables.Add("61.0sql3.sql");
            if (currver <= 61) arlTables.Add("61.0sql4.sql");
            if (currver <= 61) arlTables.Add("61.0sql5.sql");


            //Product / Serialised Item Expiry Date

            if (currver <= 62) arlTables.Add("62.0sql1.sql");
            if (currver <= 62) arlTables.Add("62.0sql2.sql");

            //Category / Sub Category

            if (currver <= 63) arlTables.Add("63.0sql1.sql");
        }

        public static void SetMultilingualArrayList()
        {
            arlLanguageTables.Clear();

            // Multilingual Update
            //Table
            arlLanguageTables.Add("Msql1.sql");
            arlLanguageTables.Add("Msql2.sql");
            arlLanguageTables.Add("Msql3.sql");
            arlLanguageTables.Add("Msql4.sql");
            arlLanguageTables.Add("Msql5.sql");
            arlLanguageTables.Add("Msql6.sql");
            arlLanguageTables.Add("Msql7.sql");
            arlLanguageTables.Add("Msql8.sql");
            arlLanguageTables.Add("Msql9.sql");
            arlLanguageTables.Add("Msql10.sql");
            arlLanguageTables.Add("Msql11.sql");
            arlLanguageTables.Add("Msql12.sql");
            arlLanguageTables.Add("Msql13.sql");
            arlLanguageTables.Add("Msql14.sql");
            arlLanguageTables.Add("Msql15.sql");
            arlLanguageTables.Add("Msql16.sql");
            arlLanguageTables.Add("Msql17.sql");
            arlLanguageTables.Add("Msql18.sql");
            arlLanguageTables.Add("Msql19.sql");
            arlLanguageTables.Add("Msql20.sql");
            arlLanguageTables.Add("Msql21.sql");
            arlLanguageTables.Add("Msql22.sql");
            arlLanguageTables.Add("Msql23.sql");
            arlLanguageTables.Add("Msql24.sql");
            arlLanguageTables.Add("Msql25.sql");
            arlLanguageTables.Add("Msql26.sql");

            // Stored Procedure
            arlLanguageTables.Add("Mpsql1.sql");
            arlLanguageTables.Add("Mpsql2.sql");
            arlLanguageTables.Add("Mpsql3.sql");
            arlLanguageTables.Add("Mpsql4.sql");
            arlLanguageTables.Add("Mpsql5.sql");
            arlLanguageTables.Add("Mpsql6.sql");
            arlLanguageTables.Add("Mpsql7.sql");
            arlLanguageTables.Add("Mpsql8.sql");
            arlLanguageTables.Add("Mpsql9.sql");
            arlLanguageTables.Add("Mpsql10.sql");
            arlLanguageTables.Add("Mpsql11.sql");
            arlLanguageTables.Add("Mpsql12.sql");
            arlLanguageTables.Add("Mpsql13.sql");
            arlLanguageTables.Add("Mpsql14.sql");
            arlLanguageTables.Add("Mpsql15.sql");
            arlLanguageTables.Add("Mpsql16.sql");
            arlLanguageTables.Add("Mpsql17.sql");
            arlLanguageTables.Add("Mpsql18.sql");
            arlLanguageTables.Add("Mpsql19.sql");
            arlLanguageTables.Add("Mpsql20.sql");
            arlLanguageTables.Add("Mpsql21.sql");
            arlLanguageTables.Add("Mpsql22.sql");
            arlLanguageTables.Add("Mpsql23.sql");
            arlLanguageTables.Add("Mpsql24.sql");
            arlLanguageTables.Add("Mpsql25.sql");
            arlLanguageTables.Add("Mpsql26.sql");
            arlLanguageTables.Add("Mpsql27.sql");
            arlLanguageTables.Add("Mpsql28.sql");
            arlLanguageTables.Add("Mpsql29.sql");
            arlLanguageTables.Add("Mpsql30.sql");

            arlLanguageTables.Add("Mpsql31.sql");
            arlLanguageTables.Add("Mpsql32.sql");
            arlLanguageTables.Add("Mpsql33.sql");
            arlLanguageTables.Add("Mpsql34.sql");
            arlLanguageTables.Add("Mpsql35.sql");
            arlLanguageTables.Add("Mpsql36.sql");
            arlLanguageTables.Add("Mpsql37.sql");
            arlLanguageTables.Add("Mpsql38.sql");
            arlLanguageTables.Add("Mpsql39.sql");
            arlLanguageTables.Add("Mpsql40.sql");

            arlLanguageTables.Add("Mpsql41.sql");
            arlLanguageTables.Add("Mpsql42.sql");
            arlLanguageTables.Add("Mpsql43.sql");
            arlLanguageTables.Add("Mpsql44.sql");
            arlLanguageTables.Add("Mpsql45.sql");
            arlLanguageTables.Add("Mpsql46.sql");
            arlLanguageTables.Add("Mpsql47.sql");
            arlLanguageTables.Add("Mpsql48.sql");
            arlLanguageTables.Add("Mpsql49.sql");
            arlLanguageTables.Add("Mpsql50.sql");

            arlLanguageTables.Add("Mpsql51.sql");
            arlLanguageTables.Add("Mpsql52.sql");
            arlLanguageTables.Add("Mpsql53.sql");
            arlLanguageTables.Add("Mpsql54.sql");
            arlLanguageTables.Add("Mpsql55.sql");
            arlLanguageTables.Add("Mpsql56.sql");
            arlLanguageTables.Add("Mpsql57.sql");
            arlLanguageTables.Add("Mpsql58.sql");
            arlLanguageTables.Add("Mpsql59.sql");
            arlLanguageTables.Add("Mpsql60.sql");

            arlLanguageTables.Add("Mpsql61.sql");
            arlLanguageTables.Add("Mpsql62.sql");
            arlLanguageTables.Add("Mpsql63.sql");
            arlLanguageTables.Add("Mpsql64.sql");
            arlLanguageTables.Add("Mpsql65.sql");
            arlLanguageTables.Add("Mpsql66.sql");
            arlLanguageTables.Add("Mpsql67.sql");
            arlLanguageTables.Add("Mpsql68.sql");
            arlLanguageTables.Add("Mpsql69.sql");
            arlLanguageTables.Add("Mpsql70.sql");

            arlLanguageTables.Add("Mpsql71.sql");
            arlLanguageTables.Add("Mpsql72.sql");
            arlLanguageTables.Add("Mpsql73.sql");
            arlLanguageTables.Add("Mpsql74.sql");
            arlLanguageTables.Add("Mpsql75.sql");
            arlLanguageTables.Add("Mpsql76.sql");
            arlLanguageTables.Add("Mpsql77.sql");
            arlLanguageTables.Add("Mpsql78.sql");
            arlLanguageTables.Add("Mpsql79.sql");
            arlLanguageTables.Add("Mpsql80.sql");

            arlLanguageTables.Add("Mpsql81.sql");
            arlLanguageTables.Add("Mpsql82.sql");
            arlLanguageTables.Add("Mpsql83.sql");

            arlLanguageTables.Add("Msql100.sql");
        }
    }
}