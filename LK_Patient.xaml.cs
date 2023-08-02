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
    /// Логика взаимодействия для LK_Patint.xaml
    /// </summary>
    public partial class LK_Patint : Window
    {
        Patient User;
        public LK_Patint(Patient user)
        {
            InitializeComponent();

            this.User = user;
            FNM.Text = $"{user.family.Substring(0,1).ToUpper() + user.family.Substring(1, user.family.Length - 1)} {user.name.Substring(0, 1).ToUpper() + user.name.Substring(1, user.name.Length - 1)} {user.middleName.Substring(0, 1).ToUpper() + user.middleName.Substring(1, user.middleName.Length - 1)}";
            foreach (Contact con in user.contacts)
            {
                if (con.type == 1)
                {
                    Tel.Text += con.value;
                }
                else if (con.type == 2)
                {
                    Mail.Text += con.value;
                }
            }
            Reception[] receptions = user.Receptions();
            if (receptions.Length == 0)
            {
                Actual_Receptions.Visibility = Visibility.Collapsed;
                Receptions.Text = "Назначенных приёмов нет";
                bCancel.Visibility = Visibility.Hidden;
            }
            else
            {
                string info = "";
                foreach (Reception rec in receptions)
                {
                    info = $"{rec.startDate.ToString()}\nДоктор: {rec.fnmDoctor}\nЖалобы: {rec.anamnesis}";
                    Actual_Receptions.Items.Add(info);
                }
            }
            Base bs = Base.getInstance();
            DataTable dt = bs.TakeValue("*", "Line WHERE ID_Patient = " + user.id.ToString());
            if (dt.Rows.Count <= 0)
            {
                Line.Text = "Вы не стоите в очереди на приём";
                bAddLine.Visibility = Visibility.Visible;
                InfoLine.Visibility = Visibility.Hidden;
                bLeftLine.Visibility = Visibility.Hidden;
            }
            else
            {
                InfoLine.Text = $"Дата записи: {dt.Rows[0]["Date"].ToString()}\nЖалобы:\n{dt.Rows[0]["Anamnesis"].ToString()}";
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.User.CancelReception(int.Parse(Actual_Receptions.SelectedItem.ToString()));
        }
        private void Add_Line(object sender, RoutedEventArgs e)
        {
            Add_Line_Patient addLine = new Add_Line_Patient(this.User);
            addLine.Show();
            this.Hide();
        }
        private void Left_Line(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Покинуть очередь?", "Подтвердите действие", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.User.LeftLine();
                this.UpdateLayout();
                Line.Text = "Вы не стоите в очереди на приём";
                bAddLine.Visibility = Visibility.Visible;
                InfoLine.Visibility = Visibility.Hidden;
                bLeftLine.Visibility = Visibility.Hidden;
            }
        }
    }
}
