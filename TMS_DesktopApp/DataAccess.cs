using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DesktopApp
{
    internal class DataAccess
    {
        MySqlConnection connection = null;
        /*
         * Function: ConnectTmsDB()
         * Description: This function establishes connection with database
         *              For example, you can connect db as below:
         *                  MySqlConnection db = new DataAccess().ConnectTmsDB();
         * Important: You need to change connStr according to your database configuration
         *            following likely to be changed: user, password, database
         * Params: NONE
         * Return: MySqlConnection connectionTmsDB, it returns instance of connection,
         *          to interact with database you need to open() connection inside your own code.
         */
        public MySqlConnection ConnectTmsDB()
        {
                //connection string
                string connStr = "Server=localhost;user=root;password=*****;Database=tms_db;";
                connection = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                try
                {
                    //test connection
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return connection;
        }

        /*
         * Function: ConnectMarketDB()
         * Description: This function establishes connection with Contract Market Place database
         *              For example, you can connect db as below:
         *                  MySqlConnection db = new DataAccess().ConnectMarketDB();
         * Params: NONE
         * Return: MySqlConnection connection, it returns instance of connection,
         *          to interact with database you need to open() connection inside your own code.
         */
        public MySqlConnection ConnectMarketDB()
        {
            //connection string
            string connStr = "Server=159.89.117.198;Port=3306;user=DevOSHT;password=Snodgr4ss!;Database=cmp;";
            connection = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                //test connection
                connection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return connection;
        }

    }
}
