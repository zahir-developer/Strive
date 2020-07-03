using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Strive.Common
{
    public static class Utility
    {

        public static Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, Task> action)
        {
            return Task.WhenAll(sequence.Select(action));
        }



        public static void WriteLog(string logpath, string logfileName, string msg)
        {
            if (!Directory.Exists(logpath))
            {
                Directory.CreateDirectory(logpath);
            }
            File.AppendAllLines(logpath + logfileName + DateTime.UtcNow.Date.ToString("ddMMyyy") + ".txt", contents: new[] { $"\t {msg} at: " + DateTime.Now.ToString(CultureInfo.InvariantCulture) });

        }


        public static int toInt(this object value)
        {

            int result = 0;
            try
            {
                if (value is null)
                {
                    return result;
                }
                string strValue = Convert.ToString(value);
                result = string.IsNullOrEmpty(strValue) ? result : Convert.ToInt32(strValue);

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return result;
        }

        public static bool toBool(this object value)
        {
            bool result = false;
            try
            {
                string strValue = Convert.ToString(value);
                result = string.IsNullOrEmpty(strValue) ? result : Convert.ToBoolean(strValue);

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return result;
        }

        public static string Get(this JObject jdata, string key)
        {
            return Convert.ToString(jdata[key]);
        }

        public static bool GetBool(this JObject jdata, string key)
        {
            string result = Convert.ToString(jdata[key]);
            return string.IsNullOrEmpty(result) ? false : result.toBool();
        }

        public static string DateString(this DateTime date, bool leadingzero = false)
        {
            DateTime datef = Convert.ToDateTime(date);
            string curMonth = datef.extract("month", leadingzero);
            string curDate = datef.extract("day", leadingzero);
            string curYear = datef.extract("year", leadingzero);
            return $"{curMonth}/{curDate}/{curYear}";
        }

        public static string GetExactName(this string fileName, string replacedata)
        {
            string FinalName = fileName;
            FinalName = FinalName.Replace("ummary", "[!textRpl!]");

            string[] strreplace = replacedata.Split(',');
            foreach (var str in strreplace)
            {
                string[] splittext = str.Split('-');
                FinalName = FinalName.Replace(splittext[0], splittext[1]);
            }
            FinalName = FinalName.Replace("[!textRpl!]", "ummary");

            return FinalName;
        }

        public static string GetExactNameUnScore(this string fileName, string replacedata)
        {
            string FinalName = fileName;
            FinalName = FinalName.Replace("Summary", "[!textRpl!]");

            string[] strreplace = replacedata.Split(',');
            foreach (var str in strreplace)
            {
                string[] splittext = str.Split('_');
                FinalName = FinalName.Replace(splittext[0], splittext[1]);
            }
            FinalName = FinalName.Replace("[!textRpl!]", "Summary");

            return FinalName;
        }

        public static string GetAsStringToLog<T>(this List<T> lstValues)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lstValues)
            {
                var props = item.GetType().GetProperties();
                sb.Append("\n");
                foreach (var p in props)
                {
                    sb.Append(p.Name + ":" + "\t" + p.GetValue(item, null) ?? "NULL" + "\t");
                }
            }
            sb.Append("\n");
            return sb.ToString();
        }


        public static IEnumerable<(string item, int index)> WithIndex(this IEnumerable<string> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static string sliceNew(this string line, int len, int st, int end)
        {
            var slicedvalue = string.Empty;
            try
            {
                if (line.Length < st)
                {
                    return "";
                }
                end = (line.Length <= st + end) ? line.Length - st : end;
                slicedvalue = (line.TrimEnd().Length > len) ? line.Crop(st, end).TrimEnd() : string.Empty;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error:" + line + ex.Message);
                //throw ex;
            }
            return slicedvalue;
        }


        public static string slice(this string line, int len, int st, int end)
        {
            var slicedvalue = string.Empty;
            try
            {
                if (line.Length < st)
                {
                    return "";
                }
                end = (line.Length <= st + end) ? line.Length - st : end;
                slicedvalue = (line.TrimEnd().Length > len) ? line.Crop(st, end).TrimEnd() : string.Empty;

                //end = (line.Length <= end) ? line.Length - 1 : end;
                //slicedvalue = (line.TrimEnd().Length > len) ? line.Crop(st, end).TrimEnd() : string.Empty;
            }
            catch (Exception ex)
            {

            }
            return slicedvalue;
        }

        public static string extract(this DateTime curDate, string datePart, bool leadingzero = false)
        {
            int datesection;
            string dp = string.Empty;
            switch (datePart.ToUpper())
            {
                case "DAY":
                    datesection = curDate.Day;
                    dp = (leadingzero && datesection < 10) ? $"0{datesection.ToString()}" : datesection.ToString();
                    break;
                case "MONTH":
                    datesection = curDate.Month;
                    dp = (leadingzero && datesection < 10) ? $"0{datesection.ToString()}" : datesection.ToString();
                    break;
                case "YEAR":
                    datesection = curDate.Year;
                    dp = datesection.ToString();
                    break;
            }

            return dp;

        }

        public static decimal toDecimal(this object value)
        {
            var strValue = Convert.ToString(value);
            var dcResult = 0M;
            try
            {
                dcResult = string.IsNullOrEmpty(strValue) ? dcResult : Convert.ToDecimal(strValue);
            }
            catch (Exception ex)
            {

            }
            return dcResult;
        }

        public static DataTable ToDataTableNew<T>(this List<T> list)
        {
            var entityType = typeof(T);

            // Lists of type System.String and System.Enum (which includes enumerations and structs) must be handled differently 
            // than primitives and custom objects (e.g. an object that is not type System.Object).
            if (entityType == typeof(String))
            {
                var dataTable = new DataTable(entityType.Name);
                dataTable.Columns.Add(entityType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();
                    row[0] = item;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
            else if (entityType.BaseType == typeof(Enum))
            {
                var dataTable = new DataTable(entityType.Name);
                dataTable.Columns.Add(entityType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (string namedConstant in Enum.GetNames(entityType))
                {
                    var row = dataTable.NewRow();
                    row[0] = namedConstant;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }

            // Check if the type of the list is a primitive type or not. Note that if the type of the list is a custom 
            // object (e.g. an object that is not type System.Object), the underlying type will be null.
            var underlyingType = Nullable.GetUnderlyingType(entityType);
            var primitiveTypes = new List<Type>
    {
        typeof (Byte),
        typeof (Char),
        typeof (Decimal),
        typeof (Double),
        typeof (Int16),
        typeof (Int32),
        typeof (Int64),
        typeof (SByte),
        typeof (Single),
        typeof (UInt16),
        typeof (UInt32),
        typeof (UInt64),
    };

            var typeIsPrimitive = primitiveTypes.Contains(underlyingType);

            // If the type of the list is a primitive, perform a simple conversion.
            // Otherwise, map the object's properties to columns and fill the cells with the properties' values.
            if (typeIsPrimitive)
            {
                var dataTable = new DataTable(underlyingType.Name);
                dataTable.Columns.Add(underlyingType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();
                    row[0] = item;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
            else
            {
                // TODO:
                // 1. Convert lists of type System.Object to a data table.
                // 2. Handle objects with nested objects (make the column name the name of the object and print "system.object" as the value).

                var dataTable = new DataTable(entityType.Name);
                var propertyDescriptorCollection = TypeDescriptor.GetProperties(entityType);

                // Iterate through each property in the object and add that property name as a new column in the data table.
                foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
                {
                    // Data tables cannot have nullable columns. The cells can have null values, but the actual columns themselves cannot be nullable.
                    // Therefore, if the current property type is nullable, use the underlying type (e.g. if the type is a nullable int, use int).
                    var propertyType = Nullable.GetUnderlyingType(propertyDescriptor.PropertyType) ?? propertyDescriptor.PropertyType;
                    dataTable.Columns.Add(propertyDescriptor.Name, propertyType);
                }

                // Iterate through each object in the list adn add a new row in the data table.
                // Then iterate through each property in the object and add the property's value to the current cell.
                // Once all properties in the current object have been used, add the row to the data table.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();

                    foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
                    {
                        var value = propertyDescriptor.GetValue(item);
                        row[propertyDescriptor.Name] = value ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
        }


        public static float toFloat(this object value)
        {
            var strValue = Convert.ToString(value);
            var dcResult = "0";

            if (strValue.IsNumeric())
            {
                return string.IsNullOrEmpty(strValue) ? float.Parse(dcResult) : float.Parse(strValue);
            }
            return float.Parse(dcResult);
        }

        public static DateTime toDate(this string value)
        {
            return Convert.ToDateTime(value);
        }

        public static int MultiplyBy(this int value, int mulitiplier)
        {
            return value * mulitiplier;
        }
        public static JProperty WithName(this object value, string val2)
        {
            return new JProperty(val2, JToken.FromObject(value));
        }

        public static bool IsNumeric(this string value)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(value.Trim());
        }

        public static bool IsValidDate(this string value)
        {
            bool IsDate = true;
            try
            {
                Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                IsDate = false;
            }
            return IsDate;
        }

        public static string Crop(this string text, int st, int end)
        {
            return text.Substring(st, end).TrimEnd();
        }

        public static string TrimLength(this string text, int end)
        {
            int endlen = end > text.Length ? text.Length : end;
            return text.Substring(0, endlen).TrimEnd();
        }

        public static string Repeat(this string text, int count)
        {
            if (!String.IsNullOrEmpty(text))
            {
                return String.Concat(Enumerable.Repeat(text, count));
            }
            return "";
        }


        public static Crypt GetEncryptionStuff(string gSecretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(gSecretKey));
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("தமிழ்*"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var encryptingCreds = new EncryptingCredentials(key1, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            return new Crypt() { EnCredentials = encryptingCreds, SignCredentials = creds };
        }

        public static Crypt GetDecryptionStuff(string gSecretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(gSecretKey));
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("தமிழ்*"));
            return new Crypt() { SignKey = key, DecryptKey = key1 };
        }

    }

    public class Crypt
    {
        public SigningCredentials SignCredentials { get; set; }
        public EncryptingCredentials EnCredentials { get; set; }

        public SymmetricSecurityKey DecryptKey { get; set; }
        public SymmetricSecurityKey SignKey { get; set; }
    }

}
