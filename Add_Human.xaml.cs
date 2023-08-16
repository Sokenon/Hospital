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
using Hospital;
using System.Text.RegularExpressions;

namespace Med
{
    /// <summary>
    /// Логика взаимодействия для Add_Human.xaml
    /// </summary>
    public partial class Add_Human : Window
    {
        int type = 0;
        int sex = 0;
        Nurse_Cabinet Cabinet;
        Nurse User;

        public Add_Human(Nurse_Cabinet cabinet, Nurse user)
        {
            InitializeComponent();
            this.Cabinet = cabinet;
            this.User = user;
            bPatient.Background = Brushes.White;
            bPatient.Foreground = Brushes.Black;
            bDoctor.Background = Brushes.Gray;
            bDoctor.Foreground = Brushes.White;
            bStaff.Background = Brushes.Gray;
            bStaff.Foreground = Brushes.White;
            stringWorkPhone.Visibility = Visibility.Hidden;
            Work_Phone.Visibility = Visibility.Hidden;
            stringPosition.Visibility = Visibility.Hidden;
            Position.Visibility = Visibility.Hidden;
            stringQualification.Visibility = Visibility.Hidden;
            Qualification.Visibility = Visibility.Hidden;
            Woman.IsChecked = true;
        }
        private void bPatient_Click(object sender, RoutedEventArgs e)
        {
            this.type = 0;
            bPatient.Background = Brushes.White;
            bPatient.Foreground = Brushes.Black;
            bDoctor.Background = Brushes.Gray;
            bDoctor.Foreground = Brushes.White;
            bStaff.Background = Brushes.Gray;
            bStaff.Foreground = Brushes.White;
            stringWorkPhone.Visibility = Visibility.Hidden;
            Work_Phone.Visibility = Visibility.Hidden;
            stringPosition.Visibility = Visibility.Hidden;
            Position.Visibility = Visibility.Hidden;
            stringQualification.Visibility = Visibility.Hidden;
            Qualification.Visibility = Visibility.Hidden;
        }
        private void bDoctor_Click(object sender, RoutedEventArgs e)
        {
            this.type = 1;
            bPatient.Background = Brushes.Gray;
            bPatient.Foreground = Brushes.White;
            bDoctor.Background = Brushes.White;
            bDoctor.Foreground = Brushes.Black;
            bStaff.Background = Brushes.Gray;
            bStaff.Foreground = Brushes.White;
            stringWorkPhone.Visibility = Visibility.Visible;
            Work_Phone.Visibility = Visibility.Visible;
            stringPosition.Visibility = Visibility.Visible;
            Position.Visibility = Visibility.Visible;
            stringQualification.Visibility = Visibility.Visible;
            Qualification.Visibility = Visibility.Visible;
        }
        private void bStaff_Click(object sender, RoutedEventArgs e)
        {
            this.type = 2;
            bPatient.Background = Brushes.Gray;
            bPatient.Foreground = Brushes.White;
            bDoctor.Background = Brushes.Gray;
            bDoctor.Foreground = Brushes.White;
            bStaff.Background = Brushes.White;
            bStaff.Foreground = Brushes.Black;
            stringWorkPhone.Visibility = Visibility.Visible;
            Work_Phone.Visibility = Visibility.Visible;
            stringPosition.Visibility = Visibility.Visible;
            Position.Visibility = Visibility.Visible;
            stringQualification.Visibility = Visibility.Hidden;
            Qualification.Visibility = Visibility.Hidden;
        }
        private void CheckBox_Man(object sender, RoutedEventArgs e)
        {
            if (Man.IsChecked == true)
            {
                this.sex = 1;
                Woman.IsChecked = false;
            }
            else
            {
                this.sex = 0;
                Woman.IsChecked = true;
            }
        }
        private void CheckBox_Woman(object sender, RoutedEventArgs e)
        {
            if (Woman.IsChecked == true)
            {
                this.sex = 0;
                Man.IsChecked = false;
            }
            else
            {
                this.sex = 1;
                Man.IsChecked = true;
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Отменить создание нового пользователя?", "Подтвердите действие", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Cabinet.Show();
                this.Hide();
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            int checkCorrect = 0;
            if (Name.Text.Trim().ToLower() == "")
            {
                Name.ToolTip = "Ведите имя";
                Name.Background = Brushes.Coral;
            }
            else
            {
                Name.ToolTip = "";
                Name.Background = Brushes.White;
                checkCorrect += 1;
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
                checkCorrect += 1;
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
                checkCorrect += 1;
            }
            if (Age.Text.Trim().ToLower() == "")
            {
                Age.ToolTip = "Ведите возраст";
                Age.Background = Brushes.Coral;
            }
            else
            {
                Age.ToolTip = "";
                Age.Background = Brushes.White;
                checkCorrect += 1;
            }
            string newTelephone = "";
            if (Telephone.Text.Trim().ToLower() != "")
            {
                bool checkPhone = false;
                for (int i = 0; i < Telephone.Text.Length; i++)
                {
                    if (Char.IsNumber(Telephone.Text, i))
                    {
                        newTelephone = newTelephone + Telephone.Text[i];
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
                    Telephone.ToolTip = "";
                    Telephone.Background = Brushes.White;
                    checkCorrect += 1;
                }
                else
                {
                    Telephone.ToolTip = "Телефон введён не корректно";
                    Telephone.Background = Brushes.Coral;
                }
            }
            else
            {
                Telephone.ToolTip = "Введите телефон";
                Telephone.Background = Brushes.Coral;
            }
            string newMail = "";
            if (Mail.Text != "")
            {
                bool checkMail = false;
                Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                checkMail = regex.IsMatch(Mail.Text);
                newMail = "\"" + Mail.Text + "\"";
                if (checkMail)
                {
                    Mail.ToolTip = "";
                    Mail.Background = Brushes.White;
                    checkCorrect += 1;
                }
                else
                {
                    Mail.ToolTip = "Почта введена не корректно";
                    Mail.Background = Brushes.Coral;
                }
            }
            else
            {
                Mail.ToolTip = "Введите почту";
                Mail.Background = Brushes.Coral;
            }
            string newWorkPhone = "";
            if (this.type == 1 || this.type == 2)
            {
                if (Position.Text.Trim().ToLower() == "")
                {
                    Position.ToolTip = "Ведите должность";
                    Position.Background = Brushes.Coral;
                }
                else
                {
                    Position.ToolTip = "";
                    Position.Background = Brushes.White;
                    checkCorrect += 1;
                }
                if (this.type == 1)
                {
                    if (Qualification.Text.Trim().ToLower() == "")
                    {
                        Qualification.ToolTip = "Ведите квалификацию";
                        Qualification.Background = Brushes.Coral;
                    }
                    else
                    {
                        Qualification.ToolTip = "";
                        Qualification.Background = Brushes.White;
                        checkCorrect += 1;
                    }
                }
                if (Work_Phone.Text.Trim().ToLower() != "")
                {
                    bool checkPhone = false;
                    for (int i = 0; i < Work_Phone.Text.Length; i++)
                    {
                        if (Char.IsNumber(Work_Phone.Text, i))
                        {
                            newWorkPhone = newWorkPhone + Work_Phone.Text[i];
                        }
                    }
                    if (newWorkPhone.Length == 3)
                    {
                        checkPhone = true;
                    }
                    else
                    {
                        checkPhone = false;
                    }

                    if (checkPhone)
                    {
                        Work_Phone.ToolTip = "";
                        Work_Phone.Background = Brushes.White;
                        checkCorrect += 1;
                    }
                    else
                    {
                        Work_Phone.ToolTip = "Рабочий телефон введён не корректно";
                        Work_Phone.Background = Brushes.Coral;
                    }
                }
                else
                {
                    Work_Phone.ToolTip = "Введите рабочий телефон";
                    Work_Phone.Background = Brushes.Coral;
                }
            }
            if (this.type == 0 & checkCorrect == 6)
            {
                Patient human = new Patient(Name.Text.Trim().ToLower(), Family.Text.Trim().ToLower(), MiddleName.Text.Trim().ToLower(), int.Parse(Age.Text.Trim().ToLower()), this.sex);
                human.Save();
                human.AddContact(newTelephone, 1);
                human.AddContact(newMail, 2);
                Page_Patient pagePatient = new Page_Patient(this.User, this.Cabinet, human);
                pagePatient.Show();
                this.Hide();
                checkCorrect = 0;
            }
            else if (this.type == 1 & checkCorrect == 9)
            {
                Doctor human = new Doctor(Name.Text.Trim().ToLower(), Family.Text.Trim().ToLower(), MiddleName.Text.Trim().ToLower(), int.Parse(Age.Text.Trim().ToLower()), this.sex, Position.Text.Trim().ToLower(), Qualification.Text.Trim().ToLower(), this.type);
                human.Save();
                human.AddContact(newTelephone, 1);
                human.AddContact(newMail, 2);
                human.AddContact(newWorkPhone, 3);
                Page_Doctor pageDoctor = new Page_Doctor(this.User, this.Cabinet, human);
                pageDoctor.Show();
                this.Hide();
                checkCorrect = 0;
            }
            else if (this.type == 2 & checkCorrect == 8)
            {
                Nurse human = new Nurse(Name.Text.Trim().ToLower(), Family.Text.Trim().ToLower(), MiddleName.Text.Trim().ToLower(), int.Parse(Age.Text.Trim().ToLower()), this.sex, Position.Text.Trim().ToLower(), this.type);
                human.Save();
                human.AddContact(newTelephone, 1);
                human.AddContact(newMail, 2);
                human.AddContact(newWorkPhone, 3);
                MessageBox.Show("Пользователь зарегистрирован");
                this.Cabinet.Show();
                this.Hide();
                checkCorrect = 0;
            }
            else
            {
                MessageBox.Show("Заполните все поля");
                checkCorrect = 0;
            }
        }
    }
}
