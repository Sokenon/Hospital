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
    /// Логика взаимодействия для Page_Doctor.xaml
    /// </summary>
    public partial class Page_Doctor : Window
    {
        Nurse User;
        Nurse_Cabinet Cabinet;
        Doctor Doctor;
        public Page_Doctor(Nurse user, Nurse_Cabinet cabinet, Doctor doctor)
        {
            InitializeComponent();
            this.User = user;
            this.Cabinet = cabinet;
            this.Doctor = doctor;
            Family.Visibility = Visibility.Visible;
            Name.Visibility = Visibility.Visible;
            MiddleName.Visibility = Visibility.Visible;
            Family.Text = this.Doctor.family.Substring(0, 1).ToUpper() + this.Doctor.family.Substring(1, this.Doctor.family.Length - 1);
            Name.Text = this.Doctor.name.Substring(0, 1).ToUpper() + this.Doctor.name.Substring(1, this.Doctor.name.Length - 1);
            MiddleName.Text = this.Doctor.middleName.Substring(0, 1).ToUpper() + this.Doctor.middleName.Substring(1, this.Doctor.middleName.Length - 1);
            Line_Lenght.Text = LineHospital.LineLenght().ToString();
            foreach (Contact con in doctor.contacts)
            {
                if (con.type == 1)
                {
                    Telephone.Text = con.value;
                }
                else if (con.type == 2)
                {
                    Mail.Text = con.value;
                }
            }
            Base bs = Base.getInstance();
            Reception[] receptions = this.Doctor.receptions;
            if (receptions == null)
            {
                Actual_Receptions.Visibility = Visibility.Hidden;
                Receptions.Text = "Назначенных приёмов нет";
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


        }

        private void Change(object sender, RoutedEventArgs e)
        {
            bCancel_Change.Visibility = Visibility.Visible;
            bSave.Visibility = Visibility.Visible;
            bChange.Visibility = Visibility.Hidden;
            Telephone.Visibility = Visibility.Hidden;
            Mail.Visibility = Visibility.Hidden;
            boxTelephone.Visibility = Visibility.Visible;
            boxMail.Visibility = Visibility.Visible;
            boxTelephone.Text = Telephone.Text;
            boxMail.Text = Mail.Text;
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            string newTelephone = "";
            bool checkPhone = false;
            string message = "Контакты не были изменены";
            int helper = 0;

            if (boxTelephone.Text != Telephone.Text)
            {
                for (int i = 0; i < boxTelephone.Text.Length; i++)
                {
                    if (Char.IsNumber(boxTelephone.Text, i))
                    {
                        newTelephone = newTelephone + boxTelephone.Text[i];
                    }
                }
                if (newTelephone.Length == 10)
                {
                    checkPhone = true;
                }
                else
                {
                    checkPhone = false;
                }

                if (checkPhone)
                {
                    bool checkUpdate = true;
                    Telephone.Text = boxTelephone.Text;
                    foreach (Contact con in this.Doctor.contacts)
                    {
                        if (con.type == 1)
                        {
                            this.Doctor.UpdateContact(con.id, boxTelephone.Text);
                            helper += 1;
                            checkUpdate = false;
                            break;
                        }
                    }
                    if (checkUpdate)
                    {
                        this.Doctor.AddContact(boxTelephone.Text, 1);
                        helper += 1;
                    }
                }
            }

            bool checkMail = false;

            if (boxMail.Text != Mail.Text)
            {
                Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                checkMail = regex.IsMatch(boxMail.Text);
                string newMail = "\"" + boxMail.Text + "\"";
                if (checkMail)
                {
                    bool checkUpdate = true;
                    Mail.Text = boxMail.Text;
                    foreach (Contact con in this.Doctor.contacts)
                    {
                        if (con.type == 2)
                        {
                            this.Doctor.UpdateContact(con.id, newMail);
                            helper += 2;
                            checkUpdate = false;
                            break;
                        }
                    }
                    if (checkUpdate)
                    {
                        this.Doctor.AddContact(newMail, 2);
                        helper += 2;
                    }
                }
            }

            switch (helper)
            {
                case 1:
                    message = "Телефон изменён";
                    break;
                case 2:
                    message = "E-mail изменён";
                    break;
                case 3:
                    message = "Контакты изменены";
                    break;
            }
            MessageBox.Show(message);
            Cancel();
        }
        private void Cancel()
        {
            bCancel_Change.Visibility = Visibility.Hidden;
            bSave.Visibility = Visibility.Hidden;
            bChange.Visibility = Visibility.Visible;
            Telephone.Visibility = Visibility.Visible;
            Mail.Visibility = Visibility.Visible;
            boxTelephone.Visibility = Visibility.Hidden;
            boxMail.Visibility = Visibility.Hidden;
        }
        private void Cancel_Change(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void Add_Receptions(object sender, RoutedEventArgs e)
        {

        }
    }
}
