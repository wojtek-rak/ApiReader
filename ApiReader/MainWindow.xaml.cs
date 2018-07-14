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
using ApiReader.Controllers;
using System.Windows.Threading;
//using AkkaTest.Messages;



namespace ApiReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow main;
        
        public ActorSystem actorSystem;
        
        internal int Interval { get; set; }
        //for printing Error messages for intervals
        internal string Status
        {
            get { return apiText.Text.ToString(); }
            set { Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { apiText.Text = value; })); }
        }
        //for inject data to DataGrid
        internal List<SalesObjects> StatusApi
        {
            set { Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { dataGridApi.ItemsSource = value; })); }
        }
        /// <summary>
        /// Constructor which initialize also intervalActor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            main = this;
            actorSystem = ActorSystem.Create("ActorSystem");
            var intervalActor = actorSystem.ActorOf<IntervalActor>();
            Interval = -1;
            intervalActor.Tell(new object { });
        }
        /// <summary>
        /// Initialization of actor which get  data from api and injects it to DataGrid
        /// </summary>
        private void readApi_Click(object sender, RoutedEventArgs e)
        {
            
            var readApiActor = actorSystem.ActorOf<ReadApiActor>();
            readApiActor.Tell(false);

        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (var column in dataGridApi.Columns)
            //{
            //    var starSize = column.ActualWidth / dataGridApi.ActualWidth;
            //    column.Width = new DataGridLength(starSize, DataGridLengthUnitType.Star);
            //}
        }
        /// <summary>
        /// Try Parse string from input and set Interval
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Interval = -1;
            int j;
            TextBox textBox = sender as TextBox;
            if (Int32.TryParse(textBox.Text, out j))
            {
                Interval = j;
            }
            else
                if(!String.Equals(textBox.Text, "")) MessageBox.Show("Please use only numbers", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
    

}
