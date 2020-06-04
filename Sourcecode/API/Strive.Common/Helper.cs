﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Text;

namespace Strive.Common
{
    public static class Helper
    {
        public static Result BindFailedResult(Exception ex, HttpStatusCode scode)
        {

            return new Result()
            {
                Exception = ex.Message + "\n" + ex.StackTrace,
                Status = GlobalEnum.Fail.ToString(),
                StatusCode = scode
            };
        }

        public static Result BindSuccessResult(JObject resultContent)
        {
            return new Result()
            {
                ResultData = JsonConvert.SerializeObject(resultContent)
            };
        }

        public static Result BindFailedResultWithContent(JObject resultContent, Exception ex, HttpStatusCode scode)
        {
            return new Result()
            {
                Exception = ex.Message,
                Status = GlobalEnum.Fail.ToString(),
                StatusCode = scode,
                ResultData = JsonConvert.SerializeObject(resultContent)
            };
        }

        public static Result Bind404SuccessResult(Exception ex, JObject resultContent)
        {
            return new Result()
            {
                ResultData = JsonConvert.SerializeObject(resultContent),
                StatusCode = HttpStatusCode.NotFound,
                Exception = ex.Message
            };
        }


        public static DataTable ToDataTable<T>(this List<T> list)
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
    }
}
