using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Phones
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PhonesViewModel phonesViewModel;
        public MainWindow()
        {
            InitializeComponent();
            phonesViewModel = new PhonesViewModel("C:\\Users\\User\\source\\repos\\Phones\\Phones\\phonesList.json");
            DataContext = phonesViewModel;
        }

        private void Price_Edit(object sender, RoutedEventArgs e)
        {
            int newPrice = int.Parse(PhonePrice.Text);
            phonesViewModel.SelectedPhone.Price = newPrice;
        }
    }
}
