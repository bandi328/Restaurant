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

        // onPChange
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderPage(ObservableCollection<classes.MenuItem> cart)
        {
            InitializeComponent();
            this.DataContext = this;
            CartToShow = cart;
            getUsers();
        }
        private void getUsers()
        {
            string json = File.ReadAllText("./src/users.json");
            MessageBox.Show(json);
            users = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
            UsersToShow = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        }
        private void clear_BTN_Click(object sender, RoutedEventArgs e)
        {
            ParentFrame.Navigate(new MainPage() { ParentFrame = this.ParentFrame });
        }

        private void addUser_BTN_Click(object sender, RoutedEventArgs e)
        {
            //DataWindow dataWindow = new DataWindow(new User(), Users);
            //dataWindow.ShowDialog();
            //if (dataWindow.DialogResult == true)
            //{
            //    Teams.Add(dataWindow.Team);
            //    search_BTN_Click(sender, e);
            //}
        }
    }
}