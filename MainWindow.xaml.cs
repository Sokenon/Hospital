﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySqlConnector;
using System.Data.Common;
using System.Data.SqlClient;
using Hospital;


namespace Med
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Base bs = Base.getInstance();
            bs.Act("CREATE TABLE IF NOT EXISTS Human (ID INT PRIMARY KEY NOT NULL AUTO_INCREMENT, Family VARCHAR(30) NOT NULL, Name VARCHAR(30) NOT NULL, MiddleName VARCHAR(30) NOT NULL, Sex TINYINT NOT NULL, Age INT NOT NULL, Position VARCHAR(20), Qualification VARCHAR (20), Type INT, UNIQUE INDEX `fio`(`Family`, `Name`, `MiddleName`) USING HASH);");
            bs.Act("CREATE TABLE IF NOT EXISTS Contact (ID INT PRIMARY KEY NOT NULL AUTO_INCREMENT, ID_Human INT NOT NULL, Value VARCHAR(30) NOT NULL UNIQUE, Type INT NOT NULL, FOREIGN KEY (ID_Human) REFERENCES Human(ID));");
            bs.Act("CREATE TABLE IF NOT EXISTS Reception (ID INT PRIMARY KEY NOT NULL AUTO_INCREMENT, ID_Doctor INT NOT NULL, ID_Patient INT NOT NULL, Date DATETIME NOT NULL, Finish_Date DATETIME, Recept VARCHAR(50), Anamnesis VARCHAR(50), Status INT NOT NULL, FOREIGN KEY (ID_Doctor) REFERENCES Human(ID), FOREIGN KEY (ID_Patient) REFERENCES Human(ID));");
            bs.Act("CREATE TABLE IF NOT EXISTS Line (ID INT PRIMARY KEY NOT NULL AUTO_INCREMENT, ID_Patient INT NOT NULL, Date DATETIME NOT NULL, Anamnesis VARCHAR (50), FOREIGN KEY (ID_Patient) REFERENCES Human(ID));");
        }

        private void Enter(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Trim().ToLower() == "")
            {
                Name.ToolTip = "Ведите имя";
                Name.Background = Brushes.Coral;
                if (Family.Text.Trim().ToLower() == "")
                {
                    Family.ToolTip = "Ведите фамилию";
                    Family.Background = Brushes.Coral;
                    if (MiddleName.Text.Trim().ToLower() == "")
                    {
                        MiddleName.ToolTip = "Ведите отчество";
                        MiddleName.Background = Brushes.Coral;
                        if (User.SelectedItem == null)
                        {
                            User.ToolTip = "Выберете тип";
                            User.Background = Brushes.Coral;
                        }
                    }
                    else if (User.SelectedItem == null)
                    {
                        User.ToolTip = "Выберете тип";
                        User.Background = Brushes.Coral;
                    }
                }
                else if (MiddleName.Text.Trim().ToLower() == "")
                {
                    MiddleName.ToolTip = "Ведите отчество";
                    MiddleName.Background = Brushes.Coral;
                    if (User.SelectedItem == null)
                    {
                        User.ToolTip = "Выберете тип";
                        User.Background = Brushes.Coral;
                    }
                }
                else if (User.SelectedItem == null)
                {
                    User.ToolTip = "Выберете тип";
                    User.Background = Brushes.Coral;
                }
            }
            else if (Family.Text.Trim().ToLower() == "")
            {
                Family.ToolTip = "Ведите фамилию";
                Family.Background = Brushes.Coral;
                if (MiddleName.Text.Trim().ToLower() == "")
                {
                    MiddleName.ToolTip = "Ведите отчество";
                    MiddleName.Background = Brushes.Coral;
                    if (User.SelectedItem == null)
                    {
                        User.ToolTip = "Выберете тип";
                        User.Background = Brushes.Coral;
                    }
                }
                else if (User.SelectedItem == null)
                {
                    User.ToolTip = "Выберете тип";
                    User.Background = Brushes.Coral;
                }


            }
            else if (MiddleName.Text.Trim().ToLower() == "")
            {
                MiddleName.ToolTip = "Ведите отчество";
                if (User.SelectedItem == null)
                {
                    User.ToolTip = "Выберете тип";
                    User.Background = Brushes.Coral;
                }
            }
            else if (User.SelectedItem == null)
            {
                User.ToolTip = "Выберете тип";
                User.Background = Brushes.Coral;
            }
            else
            {
                Name.ToolTip = "";
                Name.Background = Brushes.White;
                Family.ToolTip = "";
                Family.Background = Brushes.White;
                MiddleName.ToolTip = "";
                MiddleName.Background = Brushes.White;
                User.ToolTip = "";
                User.Background = Brushes.White;
            }

            Human user;
            Base bs = Base.getInstance();
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("@Name", Name.Text.Trim().ToLower());
            parametrs.Add("@Family", Family.Text.Trim().ToLower());
            parametrs.Add("@MiddleName", MiddleName.Text.Trim().ToLower());
            DataTable dt = bs.TakeValue("ID", $"Human WHERE Name = @Name AND Family = @Family AND MiddleName = @MiddleName;", parametrs);
            if (dt.Rows.Count > 0)
            {
                Error.Visibility = Visibility.Hidden;
                switch (User.SelectedItem)
                {
                    case "Пациент":
                        user = new Patient(int.Parse(dt.Rows[0][0].ToString()));
                        break;
                    case "Доктор":
                        user = new Doctor(int.Parse(dt.Rows[0][0].ToString()));
                        break;
                    case "Медсестра":           
                        user = new Nurse(int.Parse(dt.Rows[0][0].ToString()));
                        break;
                }
            }
            else
            {
                Error.Visibility = Visibility.Visible;
            }
        }
    }
}
