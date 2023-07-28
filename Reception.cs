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
    public class Reception
    {
        private int ID;
        public int id { get { return ID; } }
        private string FNM_Doctor;
        public string fnmDoctor { get { return FNM_Doctor; } }
        private string FNM_Patient;
        public string fnmPatient { get { return FNM_Patient; } }
        private string Anamnesis;
        public string anamnesis { get { return Anamnesis; } }
        private string Recept;
        public string recept { get { return Recept; } }
        private int Status;
        public int status { get { return Status; } }
        private DateTime Start_Date;
        public DateTime startDate { get { return Start_Date; } }
        private DateTime Finish_Date;
        public DateTime finishDate { get { return Finish_Date; } }
        private int ID_Doctor;
        private int ID_Patient;

        public Reception(int id)
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Reception WHERE ID = " + this.ID.ToString());
            this.ID = id;
            this.Anamnesis = dt.Rows[0]["Anamnesis"].ToString();
            this.Recept = dt.Rows[0]["Recept"].ToString();
            this.Status = int.Parse(dt.Rows[0]["Status"].ToString());
            this.Start_Date = DateTime.Parse(dt.Rows[0]["Date"].ToString());
            this.Finish_Date = DateTime.Parse(dt.Rows[0]["Finish_Date"].ToString());
            this.ID_Doctor = int.Parse(dt.Rows[0]["ID_Doctor"].ToString());
            this.ID_Patient = int.Parse(dt.Rows[0]["ID_Patient"].ToString());
            dt = bs.TakeValue("ID, (Family + \", \" + Name + \", \" + MiddleName) AS FNM", $"Human WHERE ID = {this.ID_Doctor.ToString()}, {this.ID_Patient.ToString()}");
            foreach (DataRow row in dt.Rows)
            {
                if (int.Parse(row["ID"].ToString()) == this.ID_Doctor)
                {
                    this.FNM_Doctor = row["FNM"].ToString();
                }
                else if (int.Parse(row["ID"].ToString()) == this.ID_Patient)
                {
                    this.FNM_Patient = row["FNM"].ToString();
                }
            }
        }
        public Reception(string anamnesis, string recept, int status, DateTime startDate, DateTime finishDate, int idDoctor, int idPatient)
        {
            this.Anamnesis = anamnesis;
            this.Recept = recept;
            this.Status = status;
            this.Start_Date = startDate;
            this.Finish_Date = finishDate;
            this.ID_Doctor = idDoctor;
            this.ID_Patient = idPatient;
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("ID, (Family + \", \" + Name + \", \" + MiddleName) AS FNM", $"Human WHERE ID = {this.ID_Doctor.ToString()}, {this.ID_Patient.ToString()}");
            foreach (DataRow row in dt.Rows)
            {
                if (int.Parse(row["ID"].ToString()) == this.ID_Doctor)
                {
                    this.FNM_Doctor = row["FNM"].ToString();
                }
                else if (int.Parse(row["ID"].ToString()) == this.ID_Patient)
                {
                    this.FNM_Patient = row["FNM"].ToString();
                }
            }

        }
        private void ChangeStatus(int newStatus)
        {
            if (ValidStatus(newStatus))
            {
                Base bs = Base.getInstance();
                bs.Update("Reception", "Status", newStatus.ToString(), this.ID);
                this.Status = newStatus;
            }
            else
            {
                //ОШИБКА, НЕТ ТАКОГО СТАТУСА
            }
        }
        public void Finish(string recept)
        {
            Base bs = Base.getInstance();
            bs.Act($"UPDATE Reception SET Status = 2, Recept = {recept}, Finish_Date = {DateTime.Now.ToString()} WHERE ID = {this.ID.ToString()}; ");
            this.Status = 2;
            this.Recept = recept;
        }
        static public void Create(int idDoctor, int idPatient, string anamnesis, DateTime startDate)
        {
            Base bs = Base.getInstance();
            bs.AddRow("Reception", $"{idDoctor.ToString()}, {idPatient.ToString()}, {startDate.ToString()}, NULL, NULL, {anamnesis}, 0");
        }
        static public Reception[] TakeDoctorReceptions(int idDoctor, int status = -1)
        {
            Reception[] receptions = new Reception[0];
            Base bs = Base.getInstance();
            DataTable dt = null;
            if (status == -1)
            {
                dt = bs.TakeValue("*", "Reception WHERE ID_Doctor = " + idDoctor.ToString());
            }
            if (ValidStatus(status))
            {
                dt = bs.TakeValue("*", "Reception WHERE ID_Doctor = " + idDoctor.ToString() + "AND Status = " + status.ToString());
            }
            string patients = idDoctor.ToString();
            foreach (DataRow row in dt.Rows)
            {
                patients = patients + ", " + row["ID"].ToString();
            }
            DataTable humans = dt = bs.TakeValue("ID, (Family + \", \" + Name + \", \" + MiddleName) AS FNM", $"Human WHERE ID = {patients}");
            Reception helper = null;
            foreach (DataRow row in dt.Rows)
            {
                helper.ID = int.Parse(row["ID"].ToString());
                helper.ID_Doctor = idDoctor;
                helper.ID_Patient = int.Parse(row["ID_Patient"].ToString());
                helper.Anamnesis = row["Anamnesis"].ToString();
                helper.Recept = row["Recept"].ToString();
                helper.Status = int.Parse(row["Status"].ToString());
                helper.Start_Date = DateTime.Parse(row["Date"].ToString());
                helper.Finish_Date = DateTime.Parse(row["Finish_Date"].ToString());
                foreach (DataRow r in humans.Rows)
                {
                    if (int.Parse(r["ID"].ToString()) == helper.ID_Doctor)
                    {
                        helper.FNM_Doctor = r["FNM"].ToString();
                    }
                    else if (int.Parse(r["ID"].ToString()) == helper.ID_Patient)
                    {
                        helper.FNM_Patient = r["FNM"].ToString();
                    }
                }
                Array.Resize(ref receptions, receptions.Length + 1);
                receptions[receptions.Length - 1] = helper;
            }
            return receptions;

        }
        static bool ValidStatus(int status)
        {
            bool check = false;
            switch (status)
            {
                case 0:
                    check = true;
                    break;
                case 1:
                    check = true;
                    break;
                case 2:
                    check = true;
                    break;
            }
            return check;
        }
        static public Reception[] TakePatientReceptions(int idPatient, int status = -1)
        {
            Reception[] receptions = new Reception[0];
            Base bs = Base.getInstance();
            DataTable dt = null;
            if (status == -1)
            {
                dt = bs.TakeValue("*", "Reception WHERE ID_Patient = " + idPatient.ToString() + ";");
            }
            if (ValidStatus(status))
            {
                dt = bs.TakeValue("*", "Reception WHERE ID_Patient = " + idPatient.ToString() + " AND Status = " + status.ToString() + ";");
            }
            string doctors = idPatient.ToString();
            foreach (DataRow row in dt.Rows)
            {
                doctors = doctors + ", " + row["ID"].ToString();
            }
            DataTable humans = bs.TakeValue("ID, (Family + \", \" + Name + \", \" + MiddleName) AS FNM", $"Human WHERE ID = {doctors}");
            Reception helper = null;
            foreach (DataRow row in dt.Rows)
            {
                helper.ID = int.Parse(row["ID"].ToString());
                helper.ID_Doctor = int.Parse(row["ID_Doctor"].ToString());
                helper.ID_Patient = idPatient;
                helper.Anamnesis = row["Anamnesis"].ToString();
                helper.Recept = row["Recept"].ToString();
                helper.Status = int.Parse(row["Status"].ToString());
                helper.Start_Date = DateTime.Parse(row["Date"].ToString());
                helper.Finish_Date = DateTime.Parse(row["Finish_Date"].ToString());
                foreach (DataRow r in humans.Rows)
                {
                    if (int.Parse(r["ID"].ToString()) == helper.ID_Doctor)
                    {
                        helper.FNM_Doctor = r["FNM"].ToString();
                    }
                    else if (int.Parse(r["ID"].ToString()) == helper.ID_Patient)
                    {
                        helper.FNM_Patient = r["FNM"].ToString();
                    }
                }
                Array.Resize(ref receptions, receptions.Length + 1);
                receptions[receptions.Length - 1] = helper;
            }
            return receptions;

        }
        public void Start()
        {
            ChangeStatus(1);
        }

    }
}
