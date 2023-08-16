using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Data.Common;
using System.Data.SqlClient;


namespace Hospital
{
    class Base
    {
        private static Base instance;
        private MySqlConnection connection;
        string connectionStr;

        private Base()
        {
            string con = "Database=projectdb;Data Source=localhost;User Id=root;Password=jgklsdfjkl";
            this.connectionStr = con;
            MySqlConnection myConnection = new MySqlConnection(con);
            connection = myConnection;
        }

        public static Base getInstance()
        {
            if (instance == null)
                instance = new Base();
            return instance;
        }
        public void Act(string command)
        {
            string CommandText = command;
            MySqlCommand myCommand = new MySqlCommand(CommandText, this.connection);
            this.connection.Open();
            myCommand.ExecuteNonQuery();
            this.connection.Close();
        }
        public DataTable TakeValue(string what, string whereAndParm, Dictionary<string, string> parametrs = null)
        {
            string command = "SELECT " + what + " FROM " + whereAndParm;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command, this.connection);
            if (parametrs != null)
            {
                foreach (var item in parametrs)
                {
                    adapter.SelectCommand.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            DataSet ds = new DataSet();
            this.connection.Open();
            adapter.Fill(ds);
            this.connection.Close();
            if (ds == null)
            {
                return null;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }
        public int AddRow(string table, string value, Dictionary<string, string> parametrs = null)
        {
            string columns = "";
            switch (table)
            {
                case "Human":
                    columns = "Family, Name, middleName, Sex, Age, Position, Qualification, Type";
                    break;
                case "Contact":
                    columns = "ID_Human, Value, Type";
                    break;
                case "Reception":
                    columns = "ID_Doctor, ID_Patient, Date, Finish_Date, Recept, Anamnesis, Status";
                    break;
                case "Line":
                    columns = "ID_Patient, Date, Anamnesis";
                    break;
            }

            string CommandText = $"INSERT INTO {table} ({columns}) VALUES ({value}) RETURNING ID;";
            MySqlCommand myCommand = new MySqlCommand(CommandText, this.connection);
            if (parametrs != null)
            {
                foreach (var item in parametrs)
                {
                    myCommand.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            
            DataSet ds = new DataSet();
            this.connection.Open();
            int id = (Int32) myCommand.ExecuteScalar();
            this.connection.Close();
            return id;

        }
        public void Update(string table, string column, string value, int id)
        {
            string CommandText = $"UPDATE {table} SET {column} = {value} WHERE ID = {id.ToString()};";
            MySqlCommand myCommand = new MySqlCommand(CommandText, this.connection);
            this.connection.Open();
            myCommand.ExecuteNonQuery();
            this.connection.Close();
        }
        public DataTable NewCommand(string command)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(command, this.connectionStr);
            DataSet ds = new DataSet();
            this.connection.Open();
            adapter.Fill(ds);
            this.connection.Close();
            DataTable dt = ds.Tables[0];
            return dt;
        }
    }
}
