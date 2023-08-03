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
            Family.Visibility = Visibility.Visible;
            Name.Visibility = Visibility.Visible;
            MiddleName.Visibility = Visibility.Visible;
            Family.Text = this.Patient.family.Substring(0, 1).ToUpper() + this.Patient.family.Substring(1, this.Patient.family.Length - 1);
            Name.Text = this.Patient.name.Substring(0, 1).ToUpper() + this.Patient.name.Substring(1, this.Patient.name.Length - 1);
            MiddleName.Text = this.Patient.middleName.Substring(0, 1).ToUpper() + this.Patient.middleName.Substring(1, this.Patient.middleName.Length - 1);
            foreach (Contact con in patient.contacts)
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
                    foreach (Contact con in this.Patient.contacts)
                    {
                        if (con.type == 1)
                        {
                            this.Patient.UpdateContact(con.id, boxTelephone.Text);
                            helper += 1;
                            checkUpdate = false;
                            break;
                        }
                    }
                    if (checkUpdate)
                    {
                        this.Patient.AddContact(boxTelephone.Text, 1);
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
                    foreach (Contact con in this.Patient.contacts)
                    {
                        if (con.type == 2)
                        {
                            this.Patient.UpdateContact(con.id, newMail);
                            helper += 2;
                            checkUpdate = false;
                            break;
                        }
                    }
                    if (checkUpdate)
                    {
                        this.Patient.AddContact(newMail, 2);
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
        private void Cancel_Change(object sender, RoutedEventArgs e)
        {
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

    }
}
