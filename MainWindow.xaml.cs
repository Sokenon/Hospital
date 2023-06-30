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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hospital;

namespace Medical
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

    }
}
