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
using Newtonsoft.Json.Linq;

namespace ApiReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal static MainWindow main;
        private ApiReaderViewModel apiReaderViewModel;
        public ApiReaderViewModel ApiReaderViewModel { get; }
        public ActorSystem actorSystem;
        private IActorRef actorReadApi;
        private IActorRef actorUpdateTable;
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
            apiReaderViewModel.ApiTabel = apiTabel;
            actorSystem = ActorSystem.Create("ActorSystem");
            actorReadApi = actorSystem.ActorOf(Props.Create(() => new ReadApiActor(apiReaderViewModel)).WithDispatcher("akka.actor.synchronized-dispatcher"));
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
        }
        private void GithubRepositoryNameTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            apiReaderViewModel.GithubRepositoryName = githubRepositoryName.Text;
        }



        /// <summary>
        /// Scaling UI
        /// </summary>

        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, null, new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                return mainWindow.OnCoerceScaleValue((double)value);
            else
                return value;
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        public double ScaleValue
        {
            get
            {
                return (double)GetValue(ScaleValueProperty);
            }
            set
            {
                SetValue(ScaleValueProperty, value);
            }
        }

        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            CalculateScale();
        }


        private void CalculateScale()
        {
            double yScale = ActualHeight / 450;
            double xScale = ActualWidth / 600;
            double value = Math.Min(xScale, yScale);
            ScaleValue = (double)OnCoerceScaleValue(myMainWindow, value);
            Console.WriteLine(xScale);
        }


    }
}