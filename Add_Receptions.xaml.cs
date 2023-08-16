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
    /// Логика взаимодействия для Add_Receptions.xaml
    /// </summary>
    public partial class Add_Receptions : Window
    {
        Nurse User;
        Nurse_Cabinet Cabinet;
        LineHospital[] Lines;
        public Add_Receptions(Nurse user, Nurse_Cabinet cabinet)
        {
            InitializeComponent();
            this.User = user;
            this.Cabinet = cabinet;
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Line");
            LineHospital[] line = new LineHospital[0];
            foreach (DataRow row in dt.Rows)
            {
                LineHospital ln = new LineHospital(int.Parse(row["ID"].ToString()), int.Parse(row["ID_Patient"].ToString()), DateTime.Parse(row["Date"].ToString()), row["Anamnesis"].ToString());
                Array.Resize(ref line, (line.Length + 1));
                line[line.Length - 1] = ln;
            }
            this.Lines = line;
            string info = "";
            foreach (LineHospital item in line)
            {
                info = $"{item.date.ToString()}\n{item.fnm}\n{item.anamnesis}";
                AllLine.Items.Add(info);
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            if (AllLine.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись в очереди");
            }
            else
            {
                Create_Reception createReception = new Create_Reception(this.Cabinet, this.User, this.Lines[AllLine.SelectedIndex]);
                createReception.Show();
                this.Hide();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Cabinet.Show();
            this.Hide();
        }
    }
}
