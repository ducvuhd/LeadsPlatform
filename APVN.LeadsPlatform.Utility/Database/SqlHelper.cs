using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Database
{
    public class SqlHelper
    {
        /// <summary>
        /// Lấy tên của properties và value set vào DynamicParameters dùng cho dapper thao tác vs DB
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="ignore">Những trường mà không có trong store thì bỏ ra(VD: Insert thì Ignore trường Id)</param>
        /// <returns></returns>
        public static DynamicParameters GetParam<T>(T entity, params string[] ignore)
        {
            List<string> lstIgnore = new List<string>(ignore);
            DynamicParameters parameters = new DynamicParameters();
            var type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var item in properties)
            {
                string nameEntity = item.Name;
                if (!lstIgnore.Contains(nameEntity))
                {
                    var valueEntity = type.GetProperty(item.Name).GetValue(entity, null);
                    parameters.Add(nameEntity, valueEntity);
                }
            }
            return parameters;
        }
        /// <summary>
        /// Lấy thông tin của entity và set vào DataTable. phục vụ cho insert, update list vào DB
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="ignore">Những trường mà không có trong store thì bỏ ra(VD: Insert thì Ignore trường Id)</param>
        /// <returns></returns>
        public static DataTable GetDataTable<T>(List<T> entity, params string[] ignore)
        {
            var dataTable = new DataTable();
            var type = typeof(T);
            List<string> lstIgnore = new List<string>(ignore);
            List<string> lstPropertiesName = new List<string>();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                string name = property.Name;
                if (!lstIgnore.Contains(name))
                {
                    dataTable.Columns.Add(property.Name);
                    lstPropertiesName.Add(property.Name);
                }
            }
            foreach (var item in entity)
            {
                DataRow rows = dataTable.NewRow();
                foreach (var name in lstPropertiesName)
                {
                    rows[name] = type.GetProperty(name).GetValue(item, null);
                }
                dataTable.Rows.Add(rows);
            }
            return dataTable;
        }
    }
}
