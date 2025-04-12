using Restaurant.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Media.Animation;

namespace Restaurant.windows
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<User> users;
        public User User { get; set; }

        public DataWindow(User User, ObservableCollection<User> users)
        {
            InitializeComponent();
            this.DataContext = this;
            this.users = users;
            this.User = User;
        }



        // onPChange
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void save_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                this.DialogResult = true;
                this.Close();
            }
        }
        private bool InputCheck()
        {
            if (String.IsNullOrWhiteSpace(User.name))
            {
                MessageBox.Show("Name required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (String.IsNullOrWhiteSpace(User.email))
            {
                MessageBox.Show("Email address required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (String.IsNullOrWhiteSpace(User.phone))
            {
                MessageBox.Show("Phone nunber required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (String.IsNullOrWhiteSpace(User.address))
            {
                MessageBox.Show("Address required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void cancel_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
