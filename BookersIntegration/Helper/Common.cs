using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookersIntegration.Helper
{
    public static class CommonMethod
    {
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }

        public static string Right(this string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }
        public static string GetString(this string str, int length, int TotalSize)
        {
            try
            {
                return str.Substring(TotalSize - length, length);

            }
            catch (Exception ex)
            {

                return "";
            }
        }
    }  
}
