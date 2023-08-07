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
    /// Логика взаимодействия для Nurse_Cabinet.xaml
    /// </summary>
    public partial class Nurse_Cabinet : Window
    {
        Nurse User;
        public Nurse_Cabinet(Nurse user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Choose_Doctor(object sender, RoutedEventArgs e)
        {
            Search_Human searchHuman = new Search_Human(this.User, this, 1);
            searchHuman.Show();
            this.Hide();
        }

        private void Choose_Patient(object sender, RoutedEventArgs e)
        {
            Search_Human searchHuman = new Search_Human(this.User, this, 0);
            searchHuman.Show();
            this.Hide();
        }

        private void LK(object sender, RoutedEventArgs e)
        {
            LK_Nurse lkNurse = new LK_Nurse(this.User, this);
            lkNurse.Show();
            this.Hide();
        }

        private void Add_New_User(object sender, RoutedEventArgs e)
        {
            Add_Human addHuman = new Add_Human(this, this.User);
            addHuman.Show();
            this.Hide();
        }
    }
}
