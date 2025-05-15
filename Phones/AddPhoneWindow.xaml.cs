using Phones;
using System;
using System.Windows;


namespace Phones
{
    public partial class AddPhoneWindow : Window
    {
        public Phone NewPhone { get; private set; } // Свойство для передачи созданного объекта Phone

        public AddPhoneWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NewPhone = new Phone
            {
                Title = txtModel.Text,
                Company = txtManufacturer.Text,
                Price = Convert.ToInt32(txtPrice.Text)
            };

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

}