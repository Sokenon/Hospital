﻿using System;
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
    public class Line
    {
        private int ID;
        private int ID_Patient;
        private string FNM;
        public string fnm { get { return FNM; } }
        private DateTime Date;
        public DateTime date { get { return Date; } }
        private string Anamnesis;
        public string anamnesis { get { return Anamnesis; } }

        public Line(int id, int idPatient, DateTime date, string anamnesis)
        {
            this.ID = id;
            this.ID_Patient = idPatient;
            this.Date = date;
            this.Anamnesis = anamnesis;
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("(Family + \", \" + Name + \", \" + MiddleName) AS FNM", $"Human WHERE ID = {idPatient.ToString()}");
            this.FNM = dt.Rows[0]["FNM"].ToString();
        }
        static public Line TakeLast()
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Line ORDER BY Date ASC LIMIT 1");
            Line line = new Line(int.Parse(dt.Rows[0]["ID"].ToString()), int.Parse(dt.Rows[0]["ID_Patient"].ToString()), DateTime.Parse(dt.Rows[0]["Date"].ToString()), dt.Rows[0]["Anamnesis"].ToString());
            return line;
        }
        static int LineLenght()
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("COUNT(*)", "Line");
            int length = int.Parse(dt.Rows[0][0].ToString());
            return length;
        }
        static public void AddLine(int idPatient, string anamnesis)
        {
            Base bs = Base.getInstance();
            DataTable dt = bs.NewCommand($"SELECT * FROM Line WHERE ID_Patient = {idPatient.ToString()};");
            if (dt.Rows.Count == 0)
            {
                bs.AddRow("Line", $"{idPatient.ToString()}, \"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}\", \"{anamnesis}\"");
            }
            else
            {
                throw new Exception("Пациент уже стоит в очереди");
            }
        }
        static public void DeleteLine(int idPatient)
        {
            Base bs = Base.getInstance();
            bs.Act("DELETE FROM Line WHERE ID_Patient = " + idPatient.ToString());
        }
        public void CreateReception(int idDoctor, DateTime date)
        {
            Reception.Create(idDoctor, this.ID_Patient, this.Anamnesis, date);
        }
    }
}
