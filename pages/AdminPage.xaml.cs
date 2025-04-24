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

namespace Restaurant.pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Order> orders;

        private ObservableCollection<Order> ordersToShow;
        public ObservableCollection<Order> OrdersToShow
        {
            get { return ordersToShow; }
            set { ordersToShow = value; OnPropertyChanged(nameof(OrdersToShow)); }
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); ChangeMeals(); }
        }

        private ObservableCollection<classes.MenuItem>? meals;

        public ObservableCollection<classes.MenuItem>? Meals
        {
            get { return meals; }
            set { meals = value; OnPropertyChanged(nameof(Meals)); }
        }

        private void ChangeMeals() {
            if (SelectedOrder != null)
            {
                Meals = new(selectedOrder.Meals);
            }
            else { 
                Meals = null;
            }
        }


        public AdminPage()
        {
            InitializeComponent();
            this.DataContext = this;
            getOrders();
        }

        private void getOrders() {
            string jsonStr = File.ReadAllText("./src/orders.json");
            List<Order> prevorders = JsonSerializer.Deserialize<List<Order>>(File.ReadAllText("./src/orders.json"));
            OrdersToShow = new(prevorders);
            orders = new(prevorders);
        }

        private void Delete_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
            {
                return;
            }
            OrdersToShow.Remove(SelectedOrder);
            orders.Remove(SelectedOrder);
        }
    }
}
