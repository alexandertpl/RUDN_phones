using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Phones
{
    public partial class MainWindow : Window
    {
        private PhonesViewModel phonesViewModel;
        public MainWindow()
        {
            InitializeComponent();
            phonesViewModel = new PhonesViewModel("./phonesList.json");
            DataContext = phonesViewModel;
        }

        private void ChangeModel(object sender, RoutedEventArgs e)
        {
            if (PhonePrice.Text != "") {
                int newPrice = int.Parse(PhonePrice.Text);
                int index = phonesViewModel.PhonesList.IndexOf(phonesViewModel.SelectedPhone);
                phonesViewModel.PhonesList.Insert(index, phonesViewModel.ChangedPhone);
                phonesViewModel.PhonesList.Remove(phonesViewModel.SelectedPhone);
            }
            
        }
        private void AddPhoneButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddPhoneWindow();

       
            if (addWindow.ShowDialog() == true)
            {
                if (addWindow.NewPhone != null)
                {
                    phonesViewModel.PhonesList.Add(addWindow.NewPhone);
                }
            }
        }

        private void DeletePhoneButton_Click(object sender, RoutedEventArgs e)
        {
            Phone selectedPhone = phonesViewModel.SelectedPhone as Phone; 
            if (selectedPhone == null)
            {
                MessageBox.Show("Пожалуйста, выберите телефон для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

             var result = MessageBox.Show($"Вы уверены, что хотите удалить телефон \"{selectedPhone.Title}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                 phonesViewModel.PhonesList.Remove(selectedPhone);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SavePhonesToJson("phonesList.json");
        }

         private void SavePhonesToJson(string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(phonesViewModel.PhonesList, options);
                string workingDirectory = Environment.CurrentDirectory;
                File.WriteAllText(Directory.GetParent(workingDirectory).Parent.FullName + "\\" + filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortNameAscending_Click(object sender, RoutedEventArgs e)
        {
            if (phonesViewModel != null)
            {
                phonesViewModel.SortByTitleAscending();
            }
            else
            {
                MessageBox.Show("Представление коллекции недоступно для сортировки.", "Ошибка сортировки", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SortNameDescending_Click(object sender, RoutedEventArgs e)
        {
            if (phonesViewModel != null)
            {
                phonesViewModel.SortByTitleDescending();    
            }
            else
            {
                MessageBox.Show("Представление коллекции недоступно для сортировки.", "Ошибка сортировки", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
