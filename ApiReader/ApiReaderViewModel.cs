using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ApiReader
{
    public partial class ApiReaderViewModel : INotifyPropertyChanged
    {
        //private ICommand GetApi { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public string GithubUsername { get; set; }
        public string GithubRepositoryName { get; set; }

        //public int count = 0;
        public ApiReaderViewModel()
        {
            GithubUsername = "google";
            GithubRepositoryName = "gvisor";
            //GetApi = new RelayCommand(Interval);
        }


        private string _apiText;

        public string ApiText
        {
            get { return _apiText; }
            set
            {
                _apiText = value;
                //_apiText = (count + 1).ToString();
                //count += 1;
                NotifyPropertyChanged("ApiText");
            }
        }

        public void Interval(object obj)
        {

        }
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}



