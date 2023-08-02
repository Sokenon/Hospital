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

        public Add_Line_Patient(Patient user)
        {
            InitializeComponent();
            this.User = user;
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
                    LK_Patint lkPatient = new LK_Patint(this.User);
                    lkPatient.Show();
                    this.Hide();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MessageBox.Show("Опишите ваши жалобы");
            }
        }
    }
}
