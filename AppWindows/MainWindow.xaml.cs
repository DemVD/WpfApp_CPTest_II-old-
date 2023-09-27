using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace WpfApp_CPTest_II.AppWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Создаем колонки
            DataGridTextColumn column1 = new DataGridTextColumn();
            column1.Header = "ИНН";
            column1.Binding = new Binding("Column1");

            DataGridTextColumn column2 = new DataGridTextColumn();
            column2.Header = "Наименование";
            column2.Binding = new Binding("Column2");

            DataGridTextColumn column3 = new DataGridTextColumn();
            column3.Header = "Город";
            column3.Binding = new Binding("Column3");

            DataGridTextColumn column4 = new DataGridTextColumn();
            column4.Header = "Область";
            column4.Binding = new Binding("Column4");

            // Добавляем колонки и устанавливаем источник данных для DataGrid
            DataGridMainMenu.Columns.Add(column1);
            DataGridMainMenu.Columns.Add(column2);
            DataGridMainMenu.Columns.Add(column3);
            DataGridMainMenu.Columns.Add(column4);
        }

        public class DataItem
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
        }

        // Проверка DataGrid Заполнением пустыми данными
        private void ButtonTestData_Click(object sender, RoutedEventArgs e)
        {
            // Существующие колонки удалить, если они существуют
            //DataGridMainMenu.ItemsSource = null;
            //DataGridMainMenu.Columns.Clear();

            List<DataItem> dataItemList = new List<DataItem>{
                new DataItem { Column1 = "Значение1_1", Column2 = "Значение1_2", Column3 = "Значение1_3", Column4 = "Значение1_4"},
                new DataItem { Column1 = "Значение2_1", Column2 = "Значение2_2", Column3 = "Значение2_3", Column4 = "Значение2_4"},
                new DataItem { Column1 = "Значение3_1", Column2 = "Значение3_2", Column3 = "Значение3_3", Column4 = "Значение3_4"}
            };
            DataGridMainMenu.ItemsSource = dataItemList;
        }

        // clear datagrid
        private void ButtonClearData_Click(object sender, RoutedEventArgs e)
        {
            DataGridMainMenu.ItemsSource = null;
            DataGridMainMenu.DataContext = null;
        }

        private void ButtonQuitDB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOpenedCompanies_Click(object sender, RoutedEventArgs e)
        {
            FetchDataFromDB(true);
        }

        private void ButtonClosedCompanies_Click(object sender, RoutedEventArgs e)
        {
            FetchDataFromDB(false);
        }

        private void FetchDataFromDB(bool Opened)
        {
            CompanyDataAccess CDA =  new CompanyDataAccess();
            List<DataItem> dataItemList = new List<DataItem>();

            if (Opened)
            {
                dataItemList = Convert_ListOfListsStr2ListDataItem(CDA.GetOpenCompanies());
            }
            else
            {
                dataItemList = Convert_ListOfListsStr2ListDataItem(CDA.GetClosedCompanies());
            }

            DataGridMainMenu.ItemsSource = dataItemList;
        }

        private List<DataItem> Convert_ListOfListsStr2ListDataItem(List<List<string>> LL)
        {
            List<DataItem> dataItemList = new List<DataItem>();
            foreach(List<string> L in LL)
            {
                DataItem di = new DataItem { Column1 = L[0], Column2 = L[1], Column3 = L[2], Column4 = L[3] };
                dataItemList.Add(di);
            }
            return dataItemList;
        }
    }
}
