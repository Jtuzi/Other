using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using WebApplication4.Controllers;

namespace JtuziWeb.Tool
{
    public class JtuziMySql
    {
        public class MySqlData
        {
            public MySqlDataReader mySqlDataReader;
            public ulong Rows;
        }
        public static MySqlConnection mySqlC;

        public static void InitMySql()
        {
            MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            mySqlC.Open();
        }
        //===========================通用方法==============================
        public static DataTable Query(string cmd)
        {
            mySqlC = DataBasePool.getInstance().getConnection();

            MySqlDataReader sdr = new MySqlCommand(cmd, mySqlC).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            mySqlC.Close();
            return dt;
        }


        public static bool NonQuery(string cmd)
        {
            return (new MySqlCommand(cmd, mySqlC).ExecuteNonQuery() == 1) ? true : false;
        }

        //===========================具体方法=============================
        //不返回结果的更新
        public static void NoBoolUpdate(string cmd)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand(cmd, mySqlC);
            //mySqlC.Open();
            msqlCmd.ExecuteNonQuery();
            //mySqlC.Close();
        }
        //返回结果的更新
        public static bool BoolUpdate(string cmd)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand(cmd, mySqlC);
            //mySqlC.Open();
            int res = msqlCmd.ExecuteNonQuery();
            //mySqlC.Close();
            return (res == 1) ? true : false;
        }
        //使用值为String类型的值来查询某个表里的符合条件的行数
        public static int GetRowsUseString(string Table, string key, string val)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand("select count(*) as value from " + Table + " where " + key + "='" + val + "'", mySqlC);
            //mySqlC.Open();
            MySqlDataReader reader = msqlCmd.ExecuteReader();
            int R = 1;
            while (reader.Read())
            {
                R = reader.GetInt32("value");
            }
            //mySqlC.Close();
            return R;
        }
        //使用值为Int32类型的值来查询某个表里的符合条件的行数
        public static int GetRowsUseInt32(string Table, string key, int val)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand("select count(*) as value from " + Table + " where " + key + "=" + val, mySqlC);
            //mySqlC.Open();
            MySqlDataReader reader = msqlCmd.ExecuteReader();
            int R = 1;
            while (reader.Read())
            {
                R = reader.GetInt32("value");
            }
            //mySqlC.Close();
            return R;
        }
        //用String类型的字段值来查询某个表里的某个字段String类型的值
        public static string GetStringValUseString(string Table, string key1, string val1, string key2)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand("select * from " + Table + " where " + key1 + "='" + val1 + "'", mySqlC);
            //mySqlC.Open();
            MySqlDataReader reader = msqlCmd.ExecuteReader();
            string R = "";
            while (reader.Read())
            {
                R = reader.GetString(key2);
            }
            //mySqlC.Close();
            return R;
        }
        //用Int32类型的字段值来查询某个表里的某个字段String类型的值
        public static int GetInt32ValUseString(string Table, string key1, int val1, string key2)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand("select * from " + Table + " where " + key1 + "='" + val1 + "'", mySqlC);
            //mySqlC.Open();
            MySqlDataReader reader = msqlCmd.ExecuteReader();
            int R = 0;
            while (reader.Read())
            {
                R = reader.GetInt32(key2);
            }
            //mySqlC.Close();
            return R;
        }
        //用Int32类型的字段值来查询某个表里的某个字段String类型的值
        public static int GetInt32ValUseInt32(string Table, string key1, string val1, string key2)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand("select * from " + Table + " where " + key1 + "=" + val1, mySqlC);
            //mySqlC.Open();
            MySqlDataReader reader = msqlCmd.ExecuteReader();
            int R = 0;
            while (reader.Read())
            {
                R = reader.GetInt32(key2);
            }
            //mySqlC.Close();
            return R;
        }
        //----------------更新===============================
        //根据Int32类型的字段值来更改某个表里的某个字段Int32类型的值
        public static bool UpdataInt32ValUseInt32(string Table, string key1, string val1, string key2, int val2)
        {
            //MySqlConnection mySqlC = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["MySqlConnection"].ToString());
            MySqlCommand msqlCmd;
            msqlCmd = new MySqlCommand("update " + Table + " set " + key2 + "=" + val2 + " where " + key1 + "=" + val1, mySqlC);
            //mySqlC.Open();
            int res = msqlCmd.ExecuteNonQuery();
            //mySqlC.Close();
            return (res == 1 ? true : false);
        }
    }
}