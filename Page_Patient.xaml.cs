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
    /// Логика взаимодействия для Page_Patient.xaml
    /// </summary>
    public partial class Page_Patient : Window
    {
        Nurse User;
        Nurse_Cabinet Cabinet;
        Patient Patient;
        public Page_Patient(Nurse user, Nurse_Cabinet cabinet, Patient patient)
        {
            InitializeComponent();
            this.User = user;
            this.Cabinet = cabinet;
            this.Patient = patient;
        }
    }
}
