using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ApiReader
{
    public interface ICreateTable
    {
        void CreateApiTable();
        TableCell CreateCell(string input);
    }
    //interface IApiReaderViewModel
    //{
    //    //String ApiText { get; set; }
    //    void NotifyPropertyChanged(string propertyName);
    //}
    public partial class ApiReaderViewModel : INotifyPropertyChanged, ICreateTable //, IApiReaderViewModel
    {
        //private ICreateTable _createTable;
        private Thickness myThickness = new Thickness();
        public Table ApiTabel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string GithubUsername { get; set; }
        public string GithubRepositoryName { get; set; }
        private JObject apiJObjectToTable = null;
        public JObject ApiJObjectToTable
        {
            get { return apiJObjectToTable; }
            set
            {
                apiJObjectToTable = value;
                CreateApiTable();
            }
        }
        //private string _apiText;
        //public string ApiText
        //{
        //    get { return _apiText; }
        //    set
        //    {
        //        _apiText = value;
        //        NotifyPropertyChanged("ApiText");
        //    }
        //}
        

        public ApiReaderViewModel()
        {
            GithubUsername = "google";
            GithubRepositoryName = "gvisor";
        }
        //[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
        public virtual void CreateApiTable()
        {
            var thickness = 1;
            myThickness.Bottom = thickness;
            myThickness.Left = thickness;
            myThickness.Right = thickness;
            myThickness.Top = thickness;


            var rowGroup = ApiTabel.RowGroups.FirstOrDefault();
            rowGroup.Rows.Clear();
            foreach (var value in apiJObjectToTable)
            { 
                if (rowGroup != null)
                {
                    TableRow row = new TableRow();

                    row.Cells.Add(CreateCell(value.Key));
                    row.Cells.Add(CreateCell(value.Value.ToString() != "" ? value.Value.ToString() : "empty"));

                    rowGroup.Rows.Add(row);
                }
            }
        }

        public TableCell CreateCell(string input)
        {
            TableCell cell = new TableCell();
            cell.Blocks.Add(new Paragraph(new Run(input)));
            cell.BorderBrush = Brushes.Black;
            cell.BorderThickness = myThickness;
            return cell;
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



