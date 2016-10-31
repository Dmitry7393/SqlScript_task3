using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SqlScript_task3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool CheckDatabaseExists(SqlConnection tmpConn, string databaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;
            try
            {
                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);
                using (tmpConn)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                    {
                        tmpConn.Open();
                        object resultObj = sqlCmd.ExecuteScalar();
                        int databaseID = 0;
                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }
                        tmpConn.Close();
                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string pathProject = System.IO.Directory.GetCurrentDirectory();
            string DataPathName = pathProject + "\\WorkplaceDatabaseData.mdf";
            string LogPathName = pathProject + "\\WorkplaceDatabaseLog.ldf";

            string connectionString = "Server=localhost;Integrated security=SSPI";

            bool databaseExist = CheckDatabaseExists(new SqlConnection(connectionString), "WorkplaceDatabase");

            SqlConnection mConnection = new SqlConnection(connectionString);

            //IF database does not exist
            if (databaseExist == false)
            {
                //Create database
               string strCreateDB = "CREATE DATABASE WorkplaceDatabase ON PRIMARY " +
                           "(NAME = WorkPlaceDatabase_Data, " +
                           "FILENAME = '" + DataPathName + "', " +
                           "SIZE = 6MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                           "LOG ON (NAME = WorkPlaceDatabase_Log, " +
                           "FILENAME = '" + LogPathName + "', " +
                           "SIZE = 1MB, " +
                           "MAXSIZE = 5MB, " +
                           "FILEGROWTH = 10%)";
                SqlCommand mCommand = new SqlCommand(strCreateDB, mConnection);
                try
                {
                    mConnection.Open();
                    mCommand.ExecuteNonQuery();
                    label_state.Text = "DataBase was Created Successfully";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "SqlScript_task3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (mConnection.State == ConnectionState.Open)
                    {
                        mConnection.Close();
                    }
                }
            }
            else  //Database already exists
            {
                label_state.Text = "Database already exists";
            }
        }
        private void CreateTable(SqlConnection connection, string strRequest)
        {
            SqlCommand sqlCommand = new SqlCommand(strRequest, connection);
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                label_state_table.Text = "Tables has been created";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SqlScript_task3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void btnAddTables_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Initial Catalog=WorkplaceDatabase;Integrated security=SSPI;database=WorkplaceDatabase";
            string[] strCreateRequest = new string[5];
            strCreateRequest[0] = "IF  NOT EXISTS (SELECT * FROM sys.objects " +
                                    "WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U')) CREATE TABLE Person" +
                                 "(PersonID INTEGER PRIMARY KEY," +
                                 "First_name CHAR(50) NOT NULL, Last_name CHAR(50) NOT NULL, Birth_date CHAR(255) NOT NULL, " +
                                 "Address CHAR(255))";
            strCreateRequest[1] = "IF  NOT EXISTS (SELECT * FROM sys.objects " +
                                   "WHERE object_id = OBJECT_ID(N'[dbo].[PhoneNumber]') AND type in (N'U')) CREATE TABLE PhoneNumber" +
                                "(NumberID INTEGER PRIMARY KEY," +
                                "PersonID INTEGER FOREIGN KEY REFERENCES Person(PersonID) NOT NULL,  Phone_number CHAR(255) NOT NULL)";
            strCreateRequest[2] = "IF  NOT EXISTS (SELECT * FROM sys.objects " +
                                "WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U')) CREATE TABLE Company" +
                                 "(CompanyID INTEGER PRIMARY KEY," +
                                 "Name CHAR(255) NOT NULL)";
            strCreateRequest[3] = "IF  NOT EXISTS (SELECT * FROM sys.objects " +
                               "WHERE object_id = OBJECT_ID(N'[dbo].[BusinessAddress]') AND type in (N'U')) CREATE TABLE BusinessAddress" +
                                "(AddressID INTEGER PRIMARY KEY," +
                                "CompanyID INTEGER FOREIGN KEY REFERENCES Company(CompanyID), Address CHAR(255))";
            strCreateRequest[4] = "IF  NOT EXISTS (SELECT * FROM sys.objects " +
                               "WHERE object_id = OBJECT_ID(N'[dbo].[PlaceOfEmployment]') AND type in (N'U')) CREATE TABLE PlaceOfEmployment" +
                               "(PlaceID INTEGER PRIMARY KEY," +
                               "PersonID INTEGER FOREIGN KEY REFERENCES Person(PersonID), " +
                               "AddressID INTEGER FOREIGN KEY REFERENCES BusinessAddress(AddressID))";
            for(int i = 0; i < 5; i++)
            {
                CreateTable(new SqlConnection(connectionString), strCreateRequest[i]);
            }
        }
    }
}
