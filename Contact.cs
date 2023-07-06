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

namespace Hospital
{
    class Contact
    {
        private int ID;
        private string Value;
        private int Type;
        private int ID_Human;

        public Contact(string value, int type, int idHuman)
        {
            if (ValidValue(value))
            {
                this.Value = value;
            }
            else
            {
                //ОШИБКА НЕ ПРАВИЛЬНО ВВЕДЁН КОНТАКТ
            }
            this.Value = value;
            this.Type = type;
            this.ID_Human = idHuman;
            SaveToBase();
        }
        public Contact(int id)
        {
            this.ID = id;
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", ("Contact WHERE ID=" + id.ToString()));
            this.Value = dt.Rows[0][2].ToString();
            this.Type = int.Parse(dt.Rows[0][3].ToString());
            this.ID_Human = int.Parse(dt.Rows[0][1].ToString());
        }
        private bool ValidType()
        {
            switch (this.Type)
            {
                case 1:
                    return true;
                case 2:
                    return true;
                case 3:
                    return true;
                default:
                    return false;
            }
        }
        public void SaveToBase()
        {
            if (ValidType())
            {
                Base bs = Base.getInstance();
                this.ID = bs.AddRow("Contact", $"{this.ID_Human.ToString()}, {this.Value}, {this.Type.ToString()}");
            }
            else
            {
                //ТАКОГО ТИПА НЕ СУЩЕСТВУЕТ
            }
        }
        private bool ValidValue(string value)
        {
            string newValue = "";
            switch (this.Type)
            {
                case 1:
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (Char.IsNumber(value, i))
                        {
                            newValue = newValue + value[i];
                        }
                    }
                    if (newValue.Length == 10)
                    {
                        this.Value = newValue;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    return regex.IsMatch(value);
                case 3:
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (Char.IsNumber(value, i))
                        {
                            newValue = newValue + value[i];
                        }
                    }
                    if (newValue.Length == 3)
                    {
                        this.Value = newValue;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        public void UpdateContact(string newValue)
        {
            if (ValidValue(newValue))
            {
                Base bs = Base.getInstance();
                bs.Update("Contact", "Value", newValue, this.ID);
            }
        }
        public string ContactString()
        {
            string result = (TypeOfContact(this.Type) + ": " + this.Value);
            return result;
        }
        static string TypeOfContact(int type)
        {
            string result = "";
            if (type == 1 || type == 2 || type == 3)
            {
                switch (type)
                {
                    case 1:
                        result = "Телефон";
                        break;
                    case 2:
                        result = "E-mail";
                        break;
                    case 3:
                        result = "Внутренний номер";
                        break;
                }
            }
            else
            {
                //ОШИБКА
            }
            return result;
        }
        static public Contact[] TakeAllContacts(int idHuman)
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Contact WHERE ID_Human = " + idHuman.ToString());
            Console.WriteLine("pupa");

        }
    }
}
