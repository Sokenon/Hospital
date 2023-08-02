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
    /// Логика взаимодействия для LK_Nurse.xaml
    /// </summary>
    public partial class LK_Nurse : Window
    {
        Nurse_Cabinet Cabinet;
        Nurse User;
        public LK_Nurse(Nurse user, Nurse_Cabinet cabinet)
        {
            InitializeComponent();
            this.User = user;
            this.Cabinet = cabinet;
        }
    }
}
