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
    /// Логика взаимодействия для Add_Line_Patient.xaml
    /// </summary>
    public partial class Add_Line_Patient : Window
    {
        Patient User;
        int Type;
        Nurse Nurse;
        Nurse_Cabinet Cabinet;

        public Add_Line_Patient(Patient user, int type, Nurse nurse = null, Nurse_Cabinet cabinet = null)
        {
            InitializeComponent();
            this.User = user;
            this.Type = type;
            this.Nurse = nurse;
            this.Cabinet = cabinet;
        }

        private void CancelLine(object sender, RoutedEventArgs e)
        {
            {
                LK_Patint lkPatient = new LK_Patint(this.User);
                lkPatient.Show();
                this.Hide();
            }
        }

        private void AddLine(object sender, RoutedEventArgs e)
        {
            if (Anamnesis.Text != "")
            {
                try
                {
                    this.User.AddToLine(Anamnesis.Text);
                    if (this.Type == 0)
                    {
                        LK_Patint lkPatient = new LK_Patint(this.User);
                        lkPatient.Show();
                    }
                    if (this.Type == 2)
                    {
                        Page_Patient pagePatient = new Page_Patient(this.Nurse, this.Cabinet, this.User);
                        pagePatient.Show();
                    }
                    this.Hide();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MessageBox.Show("Опишите жалобы");
            }
        }
    }
}
