using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Restaurant.classes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Frame ParentFrame { get; set; }


        private ObservableCollection<classes.MenuItem> menuItems;
        public ObservableCollection<classes.MenuItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; OnPropertyChanged(nameof(MenuItems)); }
        }

        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            getMenuItems();
        }

        private void getMenuItems() {
            string json = File.ReadAllText("./src/menu.json");
            MessageBox.Show(json);
            MenuItems = JsonSerializer.Deserialize<ObservableCollection<classes.MenuItem>>(json);
        }

        private void order_BTN_Click(object sender, RoutedEventArgs e)
        {
            ParentFrame.Navigate(new OrderPage() { ParentFrame = this.ParentFrame});
        }

        private void clear_BTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
