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
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //SELECTED ITEM
        public classes.MenuItem SelectedItem { get; set; }
        public classes.MenuItem SelectedCartItem { get; set; }


        public ObservableCollection<classes.MenuItem> Cart { get; set; }

        //CART
        private ObservableCollection<classes.MenuItem> cartToShow;
        public ObservableCollection<classes.MenuItem> CartToShow
        {
            get { return cartToShow; }
            set { cartToShow = value; OnPropertyChanged(nameof(cartToShow)); }
        }
        //MENU ITEMS
        private ObservableCollection<classes.MenuItem> menuItems;
        public ObservableCollection<classes.MenuItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; OnPropertyChanged(nameof(MenuItems)); }
        }
        //TOTAL PRICE
        private double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            getMenuItems();
            Cart = new ObservableCollection<classes.MenuItem>();
            CartToShow = new ObservableCollection<classes.MenuItem>();
        }

        private void getMenuItems() {
            string json = File.ReadAllText("./src/menu.json");
            //MessageBox.Show(json);
            MenuItems = JsonSerializer.Deserialize<ObservableCollection<classes.MenuItem>>(json);
        }

        private void order_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (CartToShow.Count == 0)
            {
                MessageBox.Show("The cart is empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NavigationService.Navigate(new OrderPage(CartToShow, TotalPrice));            
        }

        private void clear_BTN_Click(object sender, RoutedEventArgs e)
        {
            CartToShow = new();
            Cart = new();
            TotalPrice = 0;
        }

        private void add_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                classes.MenuItem? itemInCart = CartToShow.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                if (itemInCart != null)
                {
                    int index = CartToShow.IndexOf(itemInCart);
                    //MessageBox.Show("HYADSSADasd");
                    itemInCart.count++;
                    Cart[index] = itemInCart;
                    CartToShow = new (Cart);
                    //OnPropertyChanged(nameof(Cart));
                }
                else {
                    SelectedItem.count = 1;
                    CartToShow.Add(SelectedItem);
                    Cart.Add(SelectedItem);
                }
                TotalPrice = Math.Round(Cart.Sum(x=>x.price*(int)x.count),2);
            }
        }

        private void back_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCartItem != null)
            {
                classes.MenuItem? itemInCart = CartToShow.Where(x => x.id == SelectedCartItem.id).FirstOrDefault();
                if (itemInCart != null)
                {
                    if (itemInCart.count == 1)
                    {
                        Cart.Remove(itemInCart);
                        CartToShow = new(Cart);
                    }
                    else { 
                        int index = CartToShow.IndexOf(itemInCart);
                        //MessageBox.Show("HYADSSADasd");
                        itemInCart.count--;
                        Cart[index] = itemInCart;
                        CartToShow = new(Cart);
                        //OnPropertyChanged(nameof(Cart));
                    }
                }
                TotalPrice = Cart.Sum(x => x.price * (int)x.count);
            }
        }
    }
}
