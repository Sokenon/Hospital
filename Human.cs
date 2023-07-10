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
        protected int ID;
        protected string Name;
        protected string Family;
        protected string MiddleName;
        protected int Age;
        protected int Sex;
        protected Contact[] Contacts;
        protected int[] TypeOfContact = new int[2] { 1, 2 };

        public Human(string name, string family, string middleName, int age, int sex)
        {
            this.Name = name;
            this.Family = family;
            this.MiddleName = middleName;
            this.Age = age;
            this.Sex = sex;
            Save();
        }
        public Human(int id)
        {
            TakeHuman(id);
        }
        abstract public void AddContact(string value, int type);
        public Contact[] ShowContacts()
        {
            return this.Contacts;
        }
        public void UpdateContact(int ID, string newValue)
        {
            this.Contacts[ID].UpdateContact(newValue);
        }
        public void DeliteContact(int id)
        {
            Contacts[id].DeleteContact();
        }
        virtual protected void Save()
        {
            Base bs = Base.getInstance();
            this.ID = bs.AddRow("Human", $"{this.Family}, {this.Name}, {this.MiddleName}, {this.Sex.ToString()}, {this.Age.ToString()}, NULL, NULL, NULL");
        }
        public int[] ShowAllTypes()
        {
            return this.Type;
        }
        virtual protected void TakeHuman(int id)
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

        abstract class Stuff : Human
        {
            protected string Position;
            protected int Type;
            public Stuff(string name, string family, string middleName, int age, int sex, string position, int type) : base(name, family, middleName, age, sex)
            {
                this.Position = position;
                if (ValideType(type))
                {
                    this.Type = type;
                }
                else
                {
                    //ОШИБКА, НЕТ ТАКОГО ТИПА
                }
                Array.Resize(ref this.TypeOfContact, this.TypeOfContact.Length + 1);
                this.TypeOfContact[this.TypeOfContact.Length - 1] = 3;
            }
            public Stuff(int id) : base(id)
            {
                Array.Resize(ref this.TypeOfContact, this.TypeOfContact.Length + 1);
                this.TypeOfContact[this.TypeOfContact.Length - 1] = 3;
            }
            private bool ValideType(int type)
            {
                switch (type)
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
            protected override void Save()
            {
                Base bs = Base.getInstance();
                this.ID = bs.AddRow("Human", $"{this.Family}, {this.Name}, {this.MiddleName}, {this.Sex.ToString()}, {this.Age.ToString()}, {this.Position}, NULL, {this.Type.ToString()}");
            }
            protected override void TakeHuman(int id)
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
                this.Position = dt.Rows[0]["Position"].ToString();
                this.Type = int.Parse(dt.Rows[0]["Type"].ToString());
            }
            class Doctor : Stuff
            {
                private string Qualification;
                public string qualification { get { return Qualification; } }
                private Reception[] Receptions;
                public Reception[] receptions { get {return Receptions;} }
                private Reception ActualReception;
                public Reception actualReception { get { return ActualReception} }

                public Doctor(string name, string family, string middleName, int age, int sex, string position, string qualification) : base(name, family, middleName, age, sex, position, 1)
                {
                    this.Qualification = qualification;
                }
                public Doctor(int id) : base(id)
                {

                }
                protected override void Save()
                {
                    Base bs = Base.getInstance();
                    this.ID = bs.AddRow("Human", $"{this.Family}, {this.Name}, {this.MiddleName}, {this.Sex.ToString()}, {this.Age.ToString()}, {this.Position}, {this.Qualification.ToString()}, {this.Type.ToString()}");
                }
                protected override void TakeHuman(int id)
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
                    this.Position = dt.Rows[0]["Position"].ToString();
                    this.Type = int.Parse(dt.Rows[0]["Type"].ToString());
                    this.Qualification = dt.Rows[0]["Qualification"].ToString();
                }
                public override void AddContact(string value, int type)
                {
                    bool valid = false;
                    for (int i = 0; i < this.TypeOfContact.Length; i++)
                    {
                        if (type == this.TypeOfContact[i])
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (valid)
                    {
                        Contact contact = new Contact(value, type, this.ID);
                        contact.SaveToBase();
                        Array.Resize(ref this.Contacts, this.Contacts.Length + 1);
                        this.Contacts[this.Contacts.Length - 1] = contact;
                    }
                    else
                    {
                        //ОШИБКА. ТИП КОНТАКТА НЕ СООТВЕТСТВУЕТ ДОЛЖНОСТИ
                    }
                }

            }
        }
    }
}
