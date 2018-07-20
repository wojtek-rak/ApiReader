using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Akka.Actor;
using ApiReader.Actors;
using System.Windows.Threading;
using System.ComponentModel;
using System.Reactive.Linq;

namespace ApiReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal static MainWindow main;
        private ApiReaderViewModel apiReaderViewModel;
        public ActorSystem actorSystem;
        private IActorRef actorReadApi;
        private IObservable<long> syncMailObservable;
        private IDisposable subscription = null;

        private int Interval { get; set; }

        /// <summary>
        /// Constructor which initialize also intervalActor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            main = this;
            apiReaderViewModel = new ApiReaderViewModel();
            actorSystem = ActorSystem.Create("ActorSystem");
            actorReadApi = actorSystem.ActorOf(Props.Create(() => new ReadApiActor(apiReaderViewModel)));
            Interval = -1;
            DataContext = apiReaderViewModel;
        }

        /// <summary>
        /// Initialization of actor which get  data from api and injects it to Tabel
        /// </summary>
        private void readApi_Click(object sender, RoutedEventArgs e)
        {

            actorReadApi.Tell(new object { });

        }

        /// <summary>
        /// Try Parse string from input and start Intervals
        /// </summary>
        private void IntervalTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            Interval = -1;
            int j;
            TextBox textBox = sender as TextBox;
            if (Int32.TryParse(textBox.Text, out j))
            {
                Interval = j;
            }
            else
            {
                if (!String.Equals(textBox.Text, "")) MessageBox.Show("Please use only numbers", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            StartIntervals(j);
        }
        /// <summary>
        /// Start intervals with given number
        /// </summary>
        private void StartIntervals(int timespan)
        {
            if (subscription != null)
            {
                subscription.Dispose();
            }
            if (timespan == Interval)
            {
                syncMailObservable = Observable.Interval(TimeSpan.FromSeconds(timespan));
                subscription = syncMailObservable.Subscribe(s => actorReadApi.Tell(new object { }));

            }
        }

        private void GithubUsernameTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            apiReaderViewModel.GithubUsername = githubUsername.Text;
            //apiReaderViewModel.NotifyPropertyChanged("githubUsername");
        }
        private void GithubRepositoryNameTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            apiReaderViewModel.GithubRepositoryName = githubRepositoryName.Text;
            //apiReaderViewModel.NotifyPropertyChanged("githubRepositoryName");
        }

    }
}