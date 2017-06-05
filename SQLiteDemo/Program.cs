using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace SQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            InitLog();
            ImportData2Db();
            Console.ReadKey();
        }

        static void InitLog()
        {
            Trace.AutoFlush = true;
            var consoleListener = new ConsoleTraceListener();
            Trace.Listeners.Add(consoleListener);
        }

        static void ImportData2Db()
        {
            var items = GetTestData();
            try
            {
                var dbPath = System.Environment.CurrentDirectory + "\\DLL\\EventLogDb.db3";
                Trace.TraceInformation("EventLogDb.db3 Path:"+ dbPath);
                using (var conn = new SQLiteConnection("Data Source="+dbPath))
                {
                    conn.Open();
                    foreach (SqlEnvent e in items)
                    {
                        SqlEnventOper.UpdateEvent2Table(conn, e);
                    }
                }
                Trace.TraceInformation("操作数据库成功！");
            }
            catch (Exception ex)
            {
                Trace.TraceError("操作数据库失败：{0}", ex.Message);
            }
        }

        static IEnumerable<SqlEnvent> GetTestData()
        {
            var items = new List<SqlEnvent>();
            items.Add(new SqlEnvent()
            {
                ID = "0001",
                LoginAccount = "张三",
                Type = "Login",
                TimeStamp = DateTime.Now
            });
            items.Add(new SqlEnvent()
            {
                ID = "0002",
                LoginAccount = "李四",
                Type = "Login",
                TimeStamp = DateTime.Now
            });
            items.Add(new SqlEnvent()
            {
                ID = "0003",
                LoginAccount = "王五",
                Type = "Login",
                TimeStamp = DateTime.Now
            });
            return items;
        }
    }
}
