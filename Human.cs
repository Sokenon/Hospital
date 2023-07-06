using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Hospital;

namespace Hospitl
{
    abstract class Human
    {
        private int ID;
        private string Name;
        private string Family;
        private string MiddleName;
        private int Age;
        private int Sex;
        private Contact[] Contacts;
        private int[] Type = new int[2] { 1, 2 };

        public Human(string name, string family, string middleName, int age, int sex)
        {
            this.Name = name;
            this.Family = family;
            this.MiddleName = middleName;
            this.Age = age;
            this.Sex = sex;
        }
        public Human(int id)
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Human WHERE ID = " + id.ToString());
            this.ID = id;
            this.Family = dt.Rows[0]["Family"].ToString();
            this.Name = dt.Rows[0]["Name"].ToString();
            this.MiddleName = dt.Rows[0]["MiddleName"].ToString();
            this.Sex = int.Parse(dt.Rows[0]["Sex"].ToString());
            this.Age = int.Parse(dt.Rows[0]["Age"].ToString());
            this.Contacts = Contact.TakeAllContacts(id);
        }
    }
}
