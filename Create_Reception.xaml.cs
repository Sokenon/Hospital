using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MySqlConnector;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Hospital;

namespace Med
{
    /// <summary>
    /// Логика взаимодействия для Create_Reception.xaml
    /// </summary>
    public partial class Create_Reception : Window
    {
        Nurse_Cabinet Cabinet;
        Nurse User;
        LineHospital Line;
        Doctor[] Doctors;
        int id = -1;

        public Create_Reception(Nurse_Cabinet cabinet, Nurse user, LineHospital line)
        {
            InitializeComponent();
            this.User = user;
            this.Cabinet = cabinet;
            this.Line = line;
            Name.Text = this.Line.fnm;
            Anamnesis.Text = this.Line.anamnesis;
            string[] times = new string[] { "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30" };
            Time.ItemsSource = times;
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Human WHERE Type = 1");
            //Doctor[] doctors = new Doctor[0]; ПЕРЕПИСАТЬ БЭ У ПРИЁМА, ЧТОБЫ БРЫЛ ПО АЙДИ ВСЕ ПРИЁМЫ
            foreach (DataRow row in dt.Rows)
            {
                Doctor doc = new Doctor(row["Name"].ToString(), row["Family"].ToString(), row["MiddleName"].ToString(), int.Parse(row["Age"].ToString()), int.Parse(row["Sex"].ToString()), row["Position"].ToString(), row["Qualification"].ToString(), int.Parse(row["Type"].ToString()));
                Array.Resize(ref doctors, doctors.Length + 1);
                doctors[doctors.Length - 1] = doc;
            }
            this.Doctors = doctors;
            string[] items = new string[0];
            foreach (Doctor doc in this.Doctors)
            {
                string info = doc.qualification + "\n" + doc.family + " " + doc.name + " " + doc.middleName;
                Array.Resize(ref items, items.Length + 1);
                items[items.Length - 1] = info;
            }
            Doctor.ItemsSource = items;
        }

        private void Search_Receptions(object sender, RoutedEventArgs e)
        {
            //ВСЁ ПЕРЕПИСАТЬ
            if (Doctor.SelectedIndex == -1)
            {
                MessageBox.Show("Выберете доктора");
            }
            else
            {
                this.id = Doctor.SelectedIndex;
                Receptions.Items.Clear();
                Reception[] receptions = this.Doctors[Doctor.SelectedIndex].ShowReceptions();
                foreach (Reception reception in receptions)
                {
                    string info = reception.startDate.ToString() + "\n" + reception.fnmPatient;
                    Receptions.Items.Add(info);
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Add_Receptions addReceptions = new Add_Receptions(this.User, this.Cabinet);
            addReceptions.Show();
            this.Hide();
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            string strDate = "";
            if (this.id == -1)
            {
                MessageBox.Show("Выберете доктора");
            }
            else
            {
                if (Date.SelectedDate != null)
                {
                    MessageBox.Show(Date.SelectedDate.ToString());
                }
                else
                {
                    MessageBox.Show("Выберете дату");
                }
                //this.Line.CreateReception(this.Doctors[this.id], date);
            }
        }
    }
}
