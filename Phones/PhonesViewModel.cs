using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Phones
{
    public class PhonesViewModel : INotifyPropertyChanged
    {
        private Phone selectedPhone;
        private Phone changedPhone;
        public ObservableCollection<Phone> PhonesList { get; set; }
        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                if (value != null)
                {
                    selectedPhone = value;
                    changedPhone = value.Clone();
                    OnPropertyChanged("SelectedPhone");
                    OnPropertyChanged("ChangedPhone");
                }
            }
        }

        public Phone ChangedPhone
        {
            get { return changedPhone; }
            set
            {
                changedPhone = value;
                OnPropertyChanged("ChangedPhone");
            }
        }

        public PhonesViewModel(string phonesListFilePath)
        {
            string workingDirectory = Environment.CurrentDirectory;
            FileStream fileStream = File.OpenRead(Directory.GetParent(workingDirectory).Parent.FullName +"\\" + phonesListFilePath);
            PhonesList = JsonSerializer.Deserialize<ObservableCollection<Phone>>(fileStream);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        private void SortPhonesListInternal(bool ascending)
        {
            if (PhonesList == null || !PhonesList.Any())
                return;

            List<Phone> sortedList;
            if (ascending)
            {
                sortedList = PhonesList.OrderBy(p => p.Title).ToList();
            }
            else
            {
                sortedList = PhonesList.OrderByDescending(p => p.Title).ToList();
            }

            PhonesList = new ObservableCollection<Phone>(sortedList);
            OnPropertyChanged(nameof(PhonesList));
        }

        public void SortByTitleAscending()
        {
            SortPhonesListInternal(true);
        }

        public void SortByTitleDescending()
        {
            SortPhonesListInternal(false);
        }

    }
}
