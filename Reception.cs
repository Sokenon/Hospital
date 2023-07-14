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
    class Reception
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
        public void Show()
        {

        }
        public void ChangeStatus(int newStatus)
        {

        }
        public void Finish(string recept)
        {
            
        }
    }
}
