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
    /// Логика взаимодействия для LK_Staff.xaml
    /// </summary>
    public partial class LK_Staff : Window
    {
        Doctor userDoctor;
        Nurse userNurse;
        Stuff user;
        int Type;
        public LK_Staff(int type, Doctor doc = null, Nurse nurse = null)
        {
            InitializeComponent();
            this.Type = type;
            if (type == 0)
            {
                this.userDoctor = doc;
                this.user = new Doctor(doc.id);
            }
            else if (type == 1)
            {
                this.userNurse = nurse;
                this.user = new Nurse(nurse.id);
            }
            Family.Visibility = Visibility.Visible;
            Name.Visibility = Visibility.Visible;
            MiddleName.Visibility = Visibility.Visible;
            Family.Text = this.user.family.Substring(0, 1).ToUpper() + this.user.family.Substring(1, this.user.family.Length - 1);
            Name.Text = this.user.name.Substring(0, 1).ToUpper() + this.user.name.Substring(1, this.user.name.Length - 1);
            MiddleName.Text = this.user.middleName.Substring(0, 1).ToUpper() + this.user.middleName.Substring(1, this.user.middleName.Length - 1);
            Position.Text = this.user.position;
            if (type == 0)
            {
                Qualification.Text = this.userDoctor.qualification;
            }
            else
            {
                Qualification.Visibility = Visibility.Hidden;
            }

            foreach (Contact con in this.user.contacts)
            {
                if (con.type == 1)
                {
                    Telephone.Text = con.value;
                }
                else if (con.type == 2)
                {
                    Mail.Text = con.value;
                }
                else if (con.type == 3)
                {
                    InnerTelephone.Text = con.value;
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
                    foreach (Contact con in this.user.contacts)
                    {
                        if (con.type == 1)
                        {
                            this.user.UpdateContact(con.id, boxTelephone.Text);
                            helper += 1;
                            checkUpdate = false;
                            break;
                        }
                    }
                    if (checkUpdate)
                    {
                        this.user.AddContact(boxTelephone.Text, 1);
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
                    foreach (Contact con in this.user.contacts)
                    {
                        if (con.type == 2)
                        {
                            this.user.UpdateContact(con.id, newMail);
                            helper += 2;
                            checkUpdate = false;
                            break;
                        }
                    }
                    if (checkUpdate)
                    {
                        this.user.AddContact(newMail, 2);
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

        private void Return(object sender, RoutedEventArgs e)
        {
            if (this.Type == 0)
            {
                LK_Doctor lkDoctor = new LK_Doctor(this.userDoctor);
                lkDoctor.Show();
                this.Hide();
            }
            else if (this.Type == 1)
            {
                Nurse_Cabinet nurseCabinet = new Nurse_Cabinet(this.userNurse);
                nurseCabinet.Show();
                this.Hide();
            }
        }
    }
}
