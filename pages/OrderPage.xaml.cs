using Restaurant.classes;
using Restaurant.windows;
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

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page, INotifyPropertyChanged
    {
        public Frame ParentFrame { get; set; }

        // cart
        private ObservableCollection<classes.MenuItem> cart;
        private ObservableCollection<classes.MenuItem> cartToShow;
        public ObservableCollection<classes.MenuItem> CartToShow
        {
            get { return cartToShow; }
            set { cartToShow = value; OnPropertyChanged(nameof(CartToShow)); }
        }

        // user
        private ObservableCollection<User> users;

        private ObservableCollection<User> usersToShow;
        public ObservableCollection<User> UsersToShow
        {
            get { return usersToShow; }
            set { usersToShow = value; OnPropertyChanged(nameof(UsersToShow)); }
        }

        // CURRENT USER
        public User SelectedUser { get; set; }


        // onPChange
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //TOTAL PRICE
        private double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public OrderPage(ObservableCollection<classes.MenuItem> cart, double totalPrice)
        {
            InitializeComponent();
            this.DataContext = this;
            CartToShow = cart;
            TotalPrice = totalPrice;
            getUsers();
        }
        private void getUsers()
        {
            string json = File.ReadAllText("./src/users.json");
            users = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
            UsersToShow = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        }
        private void cancel_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack) NavigationService.GoBack();
        }

        private void addUser_BTN_Click(object sender, RoutedEventArgs e)
        {
            DataWindow dataWindow = new DataWindow(new User(), UsersToShow);
            dataWindow.ShowDialog();
            if (dataWindow.DialogResult == true)
            {
                users.Add(dataWindow.User);
                UsersToShow.Add(dataWindow.User);
                string jsonStr = JsonSerializer.Serialize(users);
                File.WriteAllText("./src./users.json", jsonStr);
            }
        }

        private void confirm_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Choose an user!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NavigationService.Navigate(new DonePage());
        }

        private void modify_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Choose an user!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int index = users.IndexOf(users.FirstOrDefault(x => x.name == SelectedUser.name));
            User tmpTeam = new User()
            {
                name = SelectedUser.name,
                email = SelectedUser.email,
                phone = SelectedUser.phone,
                address = SelectedUser.address
            };
            DataWindow dataWindow = new DataWindow(tmpTeam, users);
            dataWindow.ShowDialog();
            if (dataWindow.DialogResult == true)
            {
                users[index] = dataWindow.User;
                UsersToShow[index] = dataWindow.User;
                string jsonStr = JsonSerializer.Serialize(users);
                File.WriteAllText("./src./users.json", jsonStr);
            }
        }

        private void deleteUser_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Choose an user!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int index = users.IndexOf(users.FirstOrDefault(x => x.name == SelectedUser.name));
            users.RemoveAt(index);
            UsersToShow.RemoveAt(index);
            string jsonStr = JsonSerializer.Serialize(users);
            File.WriteAllText("./src./users.json", jsonStr);
        }
    }
}