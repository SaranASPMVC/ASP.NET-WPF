/* *********************************************************
 * Title     :   Cosmosoft Employee Self Service Portal(cESS)
 * Version   :   1.0
 * Copyright :   Copyright (c) 2007 -2008
 * Author    :   ELTPJB601
 * Email     :   info@ctlindia.com
 * URL       :   http://www.ctlindia.com
 *License    :   As expressly not mensioned here, any attempt
                 otherwise to copy,modify,sub license or distribute the  project work is void.
 ********************************************************** */
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WPFWatermarking
{
    public class DataStore_sql
    {
        private SqlConnection sqlCon;
        protected string strSysErrMsg;
        // connection string
        private string conString = null;
        bool dbCon = false;
        public String getConnectionString()
        {
            try
            {
                INIFile obj_inifile = new INIFile(AppDomain.CurrentDomain.BaseDirectory+"settings.ini");
                String ls_servername = obj_inifile.GetValue("VOT", "servername", "cosmosoft");
                String ls_DBName = obj_inifile.GetValue("VOT", "database", "watermarking");
                String ls_username = obj_inifile.GetValue("VOT", "username", "sa");
                String ls_password = obj_inifile.GetValue("VOT", "password", "ctl");
                return "Data Source=" + ls_servername + ";Initial Catalog=" + ls_DBName + ";User Id=" + ls_username + ";Password=" + ls_password+"";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        

        public DataStore_sql()
        {
           
        }
        // This Function is used to open the connection 
        public SqlConnection DBOpen()
        {
            conString = this.getConnectionString();
            sqlCon = new SqlConnection(conString);
            sqlCon.Open();
            return sqlCon;
        }
        // This Function is used to disconnect. Some forms it will be called in the unload event
        public void DBClose()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        //This Function is used to disconnect the given connection object
        public void DBClose(SqlConnection r_sqlCon)
        {
            if (r_sqlCon != null)
            {
                r_sqlCon.Close();
                r_sqlCon = null;
            }
        }
        // This function return the Dataset with the value for corresponding select statement
        public DataSet GetRecords(string selectStatement)
        {
            SqlConnection l_sqlCon = null;
            SqlDataAdapter sqlDataAdpt;
            DataSet ds = new DataSet();
            string strQuery;
            try
            {
                conString = this.getConnectionString();
                l_sqlCon = new SqlConnection(conString);
                strQuery = selectStatement;
                l_sqlCon.Open();
                sqlDataAdpt = new SqlDataAdapter(strQuery, l_sqlCon);
                sqlDataAdpt.Fill(ds);
            }
            catch (Exception e)
            {
                System.Console.Write("Xecption is "+e.Message);
                ds = null;
            }
            finally
            {
                sqlDataAdpt = null;
                if (l_sqlCon != null)
                {
                    l_sqlCon.Close();
                    l_sqlCon = null;
                }
            }
            return ds;
        }
        // This function return the Dataset with the value for corresponding select statement and table
        public DataSet GetRecords(string selectStatement, string tableName)
        {
            SqlConnection l_sqlCon = null;
            SqlDataAdapter sqlDataAdpt;
            DataSet ds = new DataSet();
            string strQuery;
            try
            {
                l_sqlCon = new SqlConnection(conString);
                strQuery = selectStatement;
                sqlDataAdpt = new SqlDataAdapter(strQuery, l_sqlCon);
                sqlDataAdpt.Fill(ds, tableName);

            }
            catch (Exception e)
            {
                System.Console.Write(e);
                ds = null;
            }
            finally
            {
                sqlDataAdpt = null;
                if (l_sqlCon != null)
                {
                    l_sqlCon.Close();
                    l_sqlCon = null;
                }
            }
            return ds;
        }

        //This function is used to insert,update or delete the record using the sqlstatement
        public int InsertUpdateDeleteRecords(string selectStatement)
        {
            SqlConnection l_sqlCon = null;
            SqlCommand sqlCmd;
            string strQuery;
            int rowsAffected;
            try
            {
                l_sqlCon = new SqlConnection(conString);
                l_sqlCon.Open();
                strQuery = selectStatement;
                sqlCmd = new SqlCommand(strQuery, l_sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                rowsAffected = -1;
                System.Console.Write(e);
            }
            finally
            {
                sqlCmd = null;
                if (l_sqlCon != null)
                {
                    l_sqlCon.Close();
                    l_sqlCon = null;
                }
            }
            return rowsAffected;
        }
        //This function is used to insert,update or delete the record using the sqlstatement and return the message in string format.
        public string InsertUpdateDeleteRecords(string selectStatement, string returnType)
        {
            SqlConnection l_sqlCon = null;
            SqlCommand sqlCmd;
            string strQuery;

            int rowsAffected;
            string returnValue = "";
            try
            {
                l_sqlCon = new SqlConnection(conString);
                l_sqlCon.Open();
                strQuery = selectStatement;
                sqlCmd = new SqlCommand(strQuery, l_sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                rowsAffected = sqlCmd.ExecuteNonQuery();
                returnValue = " Success : " + rowsAffected;

            }
            catch (Exception e)
            {
                returnValue = "Failure : " + e.Message;
            }
            finally
            {
                sqlCmd = null;
                if (l_sqlCon != null)
                {
                    l_sqlCon.Close();
                    l_sqlCon = null;
                }
            }
            return returnValue;
        }
        // This Function return the DataReader for corresponding select statement
        public SqlDataReader GetReader(string strSql)
        {
            SqlCommand sqlCmd;
            SqlDataReader sqlReader;
            try
            {
                sqlCmd = new SqlCommand(strSql, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlReader = sqlCmd.ExecuteReader();

            }
            catch (Exception e)
            {
                System.Console.Write(e);
                sqlReader = null;
                //Session["sql"]=e.Message ;
            }
            return sqlReader;
        }
        // This function is used to update the records in the Dataset using the sql statement and table name
        public int Update(DataSet ds, string selectSql)
        {
            int rowsUpdated;
            conString = this.getConnectionString();
            SqlConnection l_sqlCon;
            SqlDataAdapter sqlDataAdpt;
            SqlCommandBuilder cmdBuilder;
            SqlCommand selectCommand;// ,insertCommand,updateCommand,deleteCommand;
            try
            {
                l_sqlCon = new SqlConnection(conString);
                sqlDataAdpt = new SqlDataAdapter();
                selectCommand = new SqlCommand(selectSql, l_sqlCon);
                sqlDataAdpt.SelectCommand = selectCommand;
                cmdBuilder = new SqlCommandBuilder(sqlDataAdpt);
                rowsUpdated = sqlDataAdpt.Update(ds);
            }
            catch (Exception e)
            {
                System.Console.Write(e);
                return -1;
            }
            finally
            {
                sqlDataAdpt = null;
                l_sqlCon = null;
            }
            return rowsUpdated;
        }
        // This function is used to update the records in the Dataset using the sql statement and table name
        public String UpdateTest(DataSet ds, string selectSql, string tableName)
        {
            String rowsUpdated;
            SqlConnection l_sqlCon;
            SqlDataAdapter sqlDataAdpt;
            SqlCommandBuilder cmdBuilder;
            SqlCommand selectCommand;// ,insertCommand,updateCommand,deleteCommand;
            try
            {
                l_sqlCon = new SqlConnection(conString);
                sqlDataAdpt = new SqlDataAdapter();
                selectCommand = new SqlCommand(selectSql, l_sqlCon);
                sqlDataAdpt.SelectCommand = selectCommand;

                cmdBuilder = new SqlCommandBuilder(sqlDataAdpt);
                //rowsUpdated = sqlDataAdpt.Update(ds, tableName);
                sqlDataAdpt.Update(ds, tableName);
                rowsUpdated = "Sucess";
            }
            catch (Exception e)
            {
                System.Console.Write(e);
                return "Error " + e.Message;
            }
            finally
            {
                sqlDataAdpt = null;
                l_sqlCon = null;
            }
            return rowsUpdated;
        }

    }


}
    