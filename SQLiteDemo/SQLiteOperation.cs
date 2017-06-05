using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteDemo
{
    internal class SqlEnvent
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string LoginAccount { get; set; }
    }
    internal class SqlEnventOper
    {
        public static bool AddEvent2Table(SQLiteConnection conn,SqlEnvent lEvent)
        {
            try
            {
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into Events(ID,Type,TimeStamp,LoginAccount) values(@ID,@Type,@TimeStamp,@LoginAccount);";
                    cmd.Parameters.Add(new SQLiteParameter("ID", lEvent.ID));
                    cmd.Parameters.Add(new SQLiteParameter("Type", lEvent.Type));
                    cmd.Parameters.Add(new SQLiteParameter("TimeStamp", lEvent.TimeStamp));
                    cmd.Parameters.Add(new SQLiteParameter("LoginAccount", lEvent.LoginAccount));
                    int i = cmd.ExecuteNonQuery();
                    return i == 1;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return false;
            }
        }
        public static bool DeleteEventFromTable(SQLiteConnection conn, DateTime timeStamp)
        {
            try
            {         
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "delete from Events where TimeStamp<@TimeStamp;";
                    cmd.Parameters.Add(new SQLiteParameter("TimeStamp", timeStamp));
                    int i = cmd.ExecuteNonQuery();
                    return i == 1;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return false;
            }
        }
        public static List<SqlEnvent> GetEventByTime(SQLiteConnection conn, DateTime timeStamp)
        {
            try
            {
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from Events where TimeStamp>=@TimeStamp;";
                    cmd.Parameters.Add(new SQLiteParameter("TimeStamp", timeStamp));
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {
                        var eventList = new List<SqlEnvent>();
                        while (dr.Read())
                        {
                            var lEvent = new SqlEnvent();
                            lEvent.ID = dr.GetString(1);
                            lEvent.Type = dr.GetString(2);
                            lEvent.TimeStamp = dr.GetDateTime(3);
                            lEvent.LoginAccount = dr.GetString(0);
                            eventList.Add(lEvent);                           
                        }
                        return eventList;
                    }  
                    return null;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return null;
            }
        }
        public static bool UpdateEvent2Table(SQLiteConnection conn,SqlEnvent lEvent)
        {
            try
            {
                SQLiteCommand cmdU = conn.CreateCommand();
                cmdU.CommandText = "update Events set ID=@ID,Type=@Type,TimeStamp=@TimeStamp where LoginAccount=@LoginAccount;";
                cmdU.Parameters.Add(new SQLiteParameter("ID", lEvent.ID));
                cmdU.Parameters.Add(new SQLiteParameter("Type", lEvent.Type));
                cmdU.Parameters.Add(new SQLiteParameter("TimeStamp", lEvent.TimeStamp));
                cmdU.Parameters.Add(new SQLiteParameter("LoginAccount", lEvent.LoginAccount));
                int i = cmdU.ExecuteNonQuery();
                if (i == 0)
                {
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into Events(ID,Type,TimeStamp,LoginAccount) values(@ID,@Type,@TimeStamp,@LoginAccount);";
                    cmd.Parameters.Add(new SQLiteParameter("ID", lEvent.ID));
                    cmd.Parameters.Add(new SQLiteParameter("Type", lEvent.Type));
                    cmd.Parameters.Add(new SQLiteParameter("TimeStamp", lEvent.TimeStamp));
                    cmd.Parameters.Add(new SQLiteParameter("LoginAccount", lEvent.LoginAccount));
                    int j = cmd.ExecuteNonQuery();
                    if(j==1)
                    {
                        //Trace.WriteLine("--新增事件记录：“登录账户:" + lEvent.LoginAccount + "，最近事件日志ID:" + lEvent.ID
                        //              + "，事件:" + lEvent.Type + "，事件时间:" + lEvent.TimeStamp.ToString() + "”");
                    }
                    return j == 1;
                }
                //Trace.WriteLine("--更新事件记录：“登录账户:" + lEvent.LoginAccount + "，最近事件日志ID:" + lEvent.ID
                //                + "，事件:" + lEvent.Type + "，事件时间:" + lEvent.TimeStamp.ToString() + "”");
                return true;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return false;
            }
        }
    }
}
