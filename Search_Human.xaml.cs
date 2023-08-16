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
    /// Логика взаимодействия для Search_Human.xaml
    /// </summary>
    public partial class Search_Human : Window
    {
        Nurse User;
        Nurse_Cabinet Cabinet;
        int TypeOfHuman;
        public Search_Human(Nurse user, Nurse_Cabinet cabinet, int type)
        {
            InitializeComponent();
            this.User = user;
            this.Cabinet = cabinet;
            this.TypeOfHuman = type;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Cabinet.Show();
            this.Hide();
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Trim().ToLower() == "")
            {
                Name.ToolTip = "Ведите имя";
                Name.Background = Brushes.Coral;
            }
            else
            {
                Name.ToolTip = "";
                Name.Background = Brushes.White;
            }
            if (Family.Text.Trim().ToLower() == "")
            {
                Family.ToolTip = "Ведите фамилию";
                Family.Background = Brushes.Coral;
            }
            else
            {
                Family.ToolTip = "";
                Family.Background = Brushes.White;
            }
            if (MiddleName.Text.Trim().ToLower() == "")
            {
                MiddleName.ToolTip = "Ведите отчество";
                MiddleName.Background = Brushes.Coral;
            }
            else
            {
                MiddleName.ToolTip = "";
                MiddleName.Background = Brushes.White;
            }

            Base bs = Base.getInstance();
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("@Name", Name.Text.Trim().ToLower());
            parametrs.Add("@Family", Family.Text.Trim().ToLower());
            parametrs.Add("@MiddleName", MiddleName.Text.Trim().ToLower());
            string command = "";
            if (this.TypeOfHuman == 0)
            {
                command = "Human WHERE Name = @Name AND Family = @Family AND MiddleName = @MiddleName;";
            }
            else
            {
                command = $"Human WHERE Name = @Name AND Family = @Family AND MiddleName = @MiddleName AND Type = {this.TypeOfHuman};";
            }
            DataTable dt = bs.TakeValue("ID, Type", command, parametrs);

            if (dt.Rows.Count > 0)
            {
                switch (this.TypeOfHuman)
                {
                    case 0:
                        if (dt.Rows[0]["Type"] == DBNull.Value)
                        {
                            Patient user = new Patient(int.Parse(dt.Rows[0][0].ToString()));
                            Page_Patient pagePatient = new Page_Patient(this.User, this.Cabinet, user);
                            pagePatient.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден!");
                        }
                        break;
                    case 1:
                        if (int.Parse(dt.Rows[0][1].ToString()) == 1)
                        {
                            Doctor user = new Doctor(int.Parse(dt.Rows[0][0].ToString()));
                            Page_Doctor pageDoctor = new Page_Doctor(this.User, this.Cabinet, user);
                            pageDoctor.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден!");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }

        }
    }
}
