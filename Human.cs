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

namespace Hospital
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
            return this.TypeOfContact; ;
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


    }
    class Patient : Human
    {
        public Patient(string name, string family, string middleName, int age, int sex) : base(name, family, middleName, age, sex)
        { }
        public Patient(int id) : base(id)
        { }
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
        public Reception[] Receptions()
        {
            Reception[] receptions = Reception.TakePatientReceptions(this.ID, 0);
            return receptions;
        }
        public void CancelReception(int idReception)
        {
            Reception reception = new Reception(idReception);
            if (reception.status == 0)
            {
                reception.Finish("");
            }
            else
            {
                //ОШИБКА, ПРИЁМ НЕВОЗМОЖНО ОТМЕНИТЬ
            }
        }
        public void AddToLine(string anamnesis)
        {
            Line.AddLine(this.ID, anamnesis);
        }
        public void LeftLine()
        {
            Line.DeleteLine(this.ID);
        }
        public Reception[] AllReceptions()
        {
            Reception[] receptions = Reception.TakePatientReceptions(this.ID);
            return receptions;
        }
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

    }
    class Nurse : Stuff
    {
        public Nurse(string name, string family, string middleName, int age, int sex, string position, int type) : base(name, family, middleName, age, sex, position, 2)
        { }
        public Nurse(int id) : base(id)
        { }
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
        public void CreateReception(int idDoctor, DateTime date)
        {
            Line.TakeLast().CreateReception(idDoctor, date);
        }
    }
    class Doctor : Stuff
    {
        private string Qualification;
        public string qualification { get { return Qualification; } }
        private Reception[] Receptions;
        public Reception[] receptions { get { return Receptions; } }
        private Reception ActualReception = null;
        public Reception actualReception { get { return ActualReception; } }

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
        private void TakeReceptions()
        {
            this.Receptions = Reception.TakeDoctorReceptions(this.ID, 0);
        }
        public Reception[] ShowReceptions()
        {
            TakeReceptions();
            return this.Receptions;
        }
        public void StartReception(int idReception)
        {
            if (this.ActualReception == null)
            {
                foreach (Reception rec in Receptions)
                {
                    if (rec.id == idReception)
                    {
                        rec.Start();
                        this.ActualReception = rec;
                    }
                }
            }
            else
            {
                //ОШИБКА, ПРИЁМ УЖЕ ИДЁТ
            }
        }
        public void FinishReception(string recept)
        {
            if (this.ActualReception == null)
            {
                //ОШИБКА НЕТ АКТУАЛЬНОГО ПРИЁМА
            }
            else
            {
                this.ActualReception.Finish(recept);
                this.ActualReception = null;
            }
        }
        static Doctor[] TakeDoctors(string qualification)
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Human WHERE Qualification = " + qualification);
            Doctor helper = null;
            Doctor[] doctors = new Doctor[0];
            foreach (DataRow row in dt.Rows)
            {
                helper.ID = int.Parse(row["ID"].ToString());
                helper.Family = row["Family"].ToString();
                helper.Name = row["Name"].ToString();
                helper.MiddleName = row["MiddleName"].ToString();
                helper.Sex = int.Parse(row["Sex"].ToString());
                helper.Age = int.Parse(row["Age"].ToString());
                helper.Contacts = Contact.TakeAllContacts(helper.ID);
                helper.Position = row["Position"].ToString();
                helper.Type = int.Parse(row["Type"].ToString());
                helper.Qualification = row["Qualification"].ToString();
                Array.Resize(ref doctors, doctors.Length + 1);
                doctors[doctors.Length - 1] = helper;
            }
            return doctors;
        }
    }
}
