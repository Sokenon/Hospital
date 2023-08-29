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
    /// Логика взаимодействия для LK_Doctor.xaml
    /// </summary>
    public partial class LK_Doctor : Window
    {
        Doctor User;
        Reception[] Receptions;
        public LK_Doctor(Doctor user)
        {
            InitializeComponent();
            this.User = user;
            Base bs = Base.getInstance();
            this.Receptions = Reception.TakeDoctorReceptions(this.User.id, 0);
            foreach (Reception rec in this.Receptions)
            {
                string info = $"Начало: {rec.startDate.ToString()}\nПациент: {rec.fnmPatient}";
                receptionsList.Items.Add(info);
            }
        }

        private void Start_Work(object sender, RoutedEventArgs e)
        {
            if (receptionsList.SelectedItem == null)
            {
                MessageBox.Show("Выберете приём");
            }
            else
            {
                Reception_Work receptionWork = new Reception_Work(this.User, this.Receptions[receptionsList.SelectedIndex]);
                receptionWork.Show();
                this.Hide();
            }
        }

        private void LK(object sender, RoutedEventArgs e)
        {
            LK_Staff lkStaff = new LK_Staff(0, this.User);
            lkStaff.Show();
            this.Hide();
        }
    }
}
