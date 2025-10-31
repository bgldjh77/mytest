using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.IO;

namespace 质检工具.Func.SqlFunc
{
    class SQLiteHelper
    {
        // 用于与SQLite数据库交互的连接对象
        private SQLiteConnection connection;
        // 操作的表名
        private string tableName;
        // 表的列名，以逗号分隔的字符串
        private string columnNameStr;
        //表的列名
        private string[] columnNames;
        public SQLiteHelper(string dbAddress)
        {
            // 创建SQLite连接字符串构建器，并设置数据源和版本
            var connectionStringBuilder = new SQLiteConnectionStringBuilder
            {
                DataSource = dbAddress,
                Version = 3
            };

            // 通过连接字符串构建器创建SQLite连接对象
            connection = new SQLiteConnection(connectionStringBuilder.ConnectionString);
            // 打开数据库连接
            connection.Open();
        }
        /// <summary>
        /// 关闭数据库连接。
        /// </summary>
        public void Close()
        {
            // 如果连接不为空且状态为打开，则关闭连接
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 整理数据库碎片
        /// </summary>
        public void VACUUM()
        {
            string sql = $"VACUUM;";
            ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取C#类型对应的SQLite类型字符串。
        /// </summary>
        /// <param name="type">C#中的类型。</param>
        /// <returns>对应的SQLite类型字符串。</returns>
        private string GetColumnType(Type type)
        {
            // 根据C#类型返回对应的SQLite类型字符串
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return "INTEGER";
                case TypeCode.Double:
                    return "REAL";
                case TypeCode.Single:
                    return "FLOAT";
                case TypeCode.DateTime:
                    return "DATETIME";
                case TypeCode.Boolean:
                    return "BOOLEAN";

                default:
                    return "TEXT";
            }
        }
        public List<Dictionary<string, object>> GetRowDatas(string selectQuery)
        {
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();



            try
            {
                using (var reader = ExecuteQuery(selectQuery))
                {
                    var schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                    {
                        columnNames = new string[schemaTable.Rows.Count];
                        for (int i = 0; i < schemaTable.Rows.Count; i++)
                        {
                            columnNames[i] = schemaTable.Rows[i]["ColumnName"].ToString();
                        }
                    }
                    while (reader.Read())
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        foreach (var columnName in columnNames)
                        {
                            dict.Add(columnName, reader[columnName]);
                        }
                        values.Add(dict);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }


            return values;
        }

        /// <summary>
        /// 执行非查询SQL命令（如INSERT, UPDATE, DELETE）。
        /// </summary>
        /// <param name="sql">SQL命令字符串。</param>
        /// <param name="parameters">SQL命令参数数组。</param>
        /// <returns>命令执行影响的行数。</returns>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            try
            {
                // 使用SQLiteCommand对象执行SQL命令
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // 记录异常信息到日志文件
                LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// 执行查询SQL命令（如SELECT），返回SQLiteDataReader对象。
        /// </summary>
        /// <param name="sql">SQL命令字符串。</param>
        /// <returns>SQLiteDataReader对象。</returns>
        private SQLiteDataReader ExecuteQuery(string sql)
        {
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    return command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// 执行查询SQL命令（如SELECT），返回单个结果。
        /// </summary>
        /// <param name="sql">SQL命令字符串。</param>
        /// <returns>查询结果的单个值。</returns>
        private object ExecuteScalar(string sql)
        {
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// 记录异常信息到日志文件。
        /// </summary>
        /// <param name="ex">要记录的异常对象。</param>
        private void LogException(Exception ex)
        {
            // 将异常信息追加到日志文件中
            string errorMessage = $"发生错误：{ex.Message}{Environment.NewLine}{ex.StackTrace}";
            File.AppendAllText("error.log", errorMessage);
        }



    }
}
