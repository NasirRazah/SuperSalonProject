using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OfflineRetailV2.Data
{
    public class ConfigSettings
    {
        private ConfigSettings() { }

        #region General XML Functions

        private static string getConfigFilePath()
        {
            //return Assembly.GetExecutingAssembly().Location + ".config";
            return GeneralFunctions.GetUserConfigFile();
        }

        private static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        #endregion

        #region Handling XML configuration for GwarePOS


        // Add Theme into config file
        public static bool AddTheme(string path)
        {
            bool boolResult = false;
            XmlDocument doc = loadConfigDocument();

            XmlNode node = doc.SelectSingleNode("//appSettings");

            if (node == null)
            {
                return boolResult;
            }

            try
            {
                XmlElement elem = (XmlElement)node;
                XmlElement ProjectElement = doc.CreateElement("Theme");
                ProjectElement.InnerText = path;
                elem.AppendChild(ProjectElement);
                doc.Save(getConfigFilePath());
                boolResult = true;
                return boolResult;
            }
            catch
            {
                return boolResult;
            }
        }

        // Delete Theme from config file
        public static bool RemoveTheme()
        {
            bool boolResult = false;
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null)
                {
                    return boolResult;
                }
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("Theme");
                    XmlNode DeleteNode = null;
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        DeleteNode = XmlNode[i];
                    }
                    if (DeleteNode != null)
                    {
                        if (DeleteNode.ParentNode != null)
                        {
                            DeleteNode.ParentNode.RemoveChild(DeleteNode);
                            doc.Save(getConfigFilePath());
                            boolResult = true;
                            return boolResult;
                        }
                    }
                }
                return boolResult;
            }
            catch (NullReferenceException e)
            {
                return boolResult;
            }
        }

        // Get Theme from config file
        public static string GetTheme()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "Dark";

            try
            {
                if (node == null) return "Dark";
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("Theme");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return val;
            }
            catch (NullReferenceException e)
            {
                return "Dark";
            }
        }

        // Add DB connection info. into config file
        public static bool AddConnection(string path)
        {
            bool boolResult = false;
            XmlDocument doc = loadConfigDocument();

            XmlNode node = doc.SelectSingleNode("//appSettings");

            if (node == null)
            {
                return boolResult;
            }

            try
            {
                XmlElement elem = (XmlElement)node;
                XmlElement ProjectElement = doc.CreateElement("conn");
                ProjectElement.InnerText = path;
                elem.AppendChild(ProjectElement);
                doc.Save(getConfigFilePath());
                boolResult = true;
                return boolResult;
            }
            catch
            {
                return boolResult;
            }
        }

        // Delete DB connection info. from config file
        public static bool RemoveConnection()
        {
            bool boolResult = false;
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null)
                {
                    return boolResult;
                }
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("conn");
                    XmlNode DeleteNode = null;
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        DeleteNode = XmlNode[i];
                    }
                    if (DeleteNode != null)
                    {
                        if (DeleteNode.ParentNode != null)
                        {
                            DeleteNode.ParentNode.RemoveChild(DeleteNode);
                            doc.Save(getConfigFilePath());
                            boolResult = true;
                            return boolResult;
                        }
                    }
                }
                return boolResult;
            }
            catch (NullReferenceException e)
            {
                return boolResult;
            }
        }

        // Get DB connection info. from config file
        public static DataTable GetConnectionString()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            DataTable dtbl = new DataTable();

            try
            {
                if (node == null)
                {
                    return null; ;
                }
                else
                {
                    dtbl.Columns.Add("conn", System.Type.GetType("System.String"));

                    XmlNodeList XmlNode = doc.GetElementsByTagName("conn");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        dtbl.Rows.Add(new object[] { XmlNode[i].FirstChild.InnerText });
                    }
                }
                return dtbl;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        // Get License Agreement info. from config file
        public static DataTable GetAgreementString()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            DataTable dtbl = new DataTable();

            try
            {
                if (node == null) return null;
                else
                {
                    dtbl.Columns.Add("displayagreement", System.Type.GetType("System.String"));

                    XmlNodeList XmlNode = doc.GetElementsByTagName("displayagreement");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        dtbl.Rows.Add(new object[] { XmlNode[i].FirstChild.InnerText });
                    }
                }
                return dtbl;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        // Delete License Agreement info. from config file
        public static bool RemoveAgreement()
        {
            bool boolResult = false;
            XmlDocument doc = loadConfigDocument();

            XmlNode node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null) return boolResult;
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("displayagreement");
                    XmlNode DeleteNode = null;
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        DeleteNode = XmlNode[i];
                    }
                    if (DeleteNode != null)
                    {
                        if (DeleteNode.ParentNode != null)
                        {
                            DeleteNode.ParentNode.RemoveChild(DeleteNode);
                            doc.Save(getConfigFilePath());
                            boolResult = true;
                            return boolResult;
                        }
                    }
                }
                return boolResult;
            }
            catch (NullReferenceException e)
            {
                return boolResult;
            }
        }

        // Add License Agreement info. into config file
        public static bool AddAgreement(string path)
        {
            bool boolResult = false;
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");

            if (node == null) return boolResult;

            try
            {
                XmlElement elem = (XmlElement)node;
                XmlElement ProjectElement = doc.CreateElement("displayagreement");
                ProjectElement.InnerText = path;
                elem.AppendChild(ProjectElement);
                doc.Save(getConfigFilePath());
                boolResult = true;
                return boolResult;
            }
            catch
            {
                return boolResult;
            }
        }

        // Get Branding info. from config file
        public static string GetBrandName()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "XEPOS Retail 2024";

            try
            {
                if (node == null) return "XEPOS Retail 2024";
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("BrandName");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return val;
            }
            catch (NullReferenceException e)
            {
                return "XEPOS Retail 2024";
            }
        }

        // Get Short Brand info. from config file
        public static string GetShortBrandName()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            DataTable dtbl = new DataTable();
            string val = "XEPOS Retail 2024";
            try
            {
                if (node == null)
                {
                    return "XEPOS Retail 2024";
                }
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("ShortBrandName");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return val;
            }
            catch (NullReferenceException e)
            {
                return "XEPOS Retail 2024";
            }
        }

        //Get Multilingual Connection
        public static DataTable GetMultiLingualConnectionString()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            DataTable dtbl = new DataTable();

            try
            {
                if (node == null)
                {
                    return null; ;
                }
                else
                {
                    dtbl.Columns.Add("connM", System.Type.GetType("System.String"));

                    XmlNodeList XmlNode = doc.GetElementsByTagName("connM");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        dtbl.Rows.Add(new object[] { XmlNode[i].FirstChild.InnerText });
                    }
                }
                return dtbl;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public static DataTable GetSmartGrocerConnectionString()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            DataTable dtbl = new DataTable();

            try
            {
                if (node == null)
                {
                    return null;
                }
                else
                {
                    dtbl.Columns.Add("conn3", System.Type.GetType("System.String"));

                    XmlNodeList XmlNode = doc.GetElementsByTagName("conn3");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        dtbl.Rows.Add(new object[] { XmlNode[i].FirstChild.InnerText });
                    }
                }
                return dtbl;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        // Get Currency Symbol from config file
        public static string GetCurrencySymbol()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "$";

            try
            {
                if (node == null) return "$";
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("Currency");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return val;
            }
            catch (NullReferenceException e)
            {
                return "$";
            }
        }

        // Get Date Format from config file
        public static string GetDateFormat()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "1";

            try
            {
                if (node == null) return "1";
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("DateFormat");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return val;
            }
            catch (NullReferenceException e)
            {
                return "1";
            }
        }

        // Get Page Width Adjustment Value for continuous printing from config file
        public static int GetPrintPageAdjustment()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "0";

            try
            {
                if (node == null) return 0;
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("PrintPageAdjustment");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return GeneralFunctions.fnInt32(val);
            }
            catch (NullReferenceException e)
            {
                return 0;
            }
        }


        // Get Country from config file
        public static string GetCountry()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "USA";

            try
            {
                if (node == null) return "USA";
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("Country");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                if ((val.ToUpper() == "USA") || (val.ToUpper() == "UK") || (val.ToUpper() == "CANADA") || (val.ToUpper() == "EURO"))
                {
                    return val.ToUpper();
                }
                else
                {
                    return "USA";
                }
            }
            catch (NullReferenceException e)
            {
                return "USA";
            }
        }


        // Get Booker Directory from config file
        public static string GetBookerDirectory()
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string val = "";

            try
            {
                if (node == null) return "$";
                else
                {
                    XmlNodeList XmlNode = doc.GetElementsByTagName("BookersStartUpDirectory");
                    for (int i = 0; i < XmlNode.Count; i++)
                    {
                        val = XmlNode[i].FirstChild.InnerText;
                    }
                }
                return val;
            }
            catch (NullReferenceException e)
            {
                return "";
            }
        }


        #endregion
    }
}
