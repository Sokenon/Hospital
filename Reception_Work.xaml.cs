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
    /// Логика взаимодействия для Reception_Work.xaml
    /// </summary>
    public partial class Reception_Work : Window
    {
        Doctor User;
        Reception Reception;
        Patient Patient;
        public Reception_Work(Doctor user, Reception reception)
        {
            InitializeComponent();
            this.User = user;
            this.Reception = reception;
            this.Reception.Start();
            Base bs = Base.getInstance();
            this.Patient = new Patient(this.Reception.idPatient);
            Family.Text = this.Patient.family;
            Name.Text = this.Patient.name;
            MiddleName.Text = this.Patient.middleName;
            Age.Text = this.Patient.age.ToString();
            if (this.Patient.sex == 0)
            {
                Sex.Text = "Женщина";
            }
            else if (this.Patient.sex == 1)
            {
                Sex.Text = "Мужчина";
            }
            foreach (Contact con in this.Patient.contacts)
            {
                if (con.type == 1)
                {
                    Telephone.Text = "+7" + con.value;
                }
                else if (con.type == 2)
                {
                    Mail.Text = con.value;
                }
            }
            Anamnesis.Text = this.Reception.anamnesis;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Reception.ChangeStatus(0);
            Back();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            if (Recept.Text != "")
            {
                Dictionary<string, string> parametrs = new Dictionary<string, string>();
                parametrs.Add("@Recept", Recept.Text);
                parametrs.Add("@Date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                parametrs.Add("@ID", this.Reception.id.ToString());
                this.Reception.Finish(Recept.Text, parametrs);
                MessageBox.Show("Рецепт выписан");
                Back();
            }
            else
            {
                MessageBox.Show("Выпишите рецепт");
            }
        }
        private void Back()
        {
            LK_Doctor lkDoctor = new LK_Doctor(this.User);
            lkDoctor.Show();
            this.Hide();
        }
    }
}
