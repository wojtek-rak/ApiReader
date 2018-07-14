using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Newtonsoft.Json;
using ApiReader;
using System.Windows;

namespace ApiReader.Controllers
{
    /// <summary>
    /// Actor responsible for getApi
    /// </summary>
    public class ReadApiActor : ReceiveActor
    {
        //just for testing
        public sealed class OperationResult
        { 
            public OperationResult()
            {
            }
            public bool Successful { set; get; }
        }

        public ReadApiActor()
        {
            Receive<bool>(GetApi);
        }

        public bool GetApi(bool interval)
        {
            string url = "http://localhost:10030/alfaRestAPI/api/RestAPI/GetSharesPrices";
            try
            {
                HttpWebRequest httpprequest = WebRequest.Create(url) as HttpWebRequest;

                httpprequest.UserAgent = "userAgent";

                httpprequest.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)httpprequest.GetResponse();


                String htmlString;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    htmlString = reader.ReadToEnd();

                    var result = JsonConvert.DeserializeObject<List<SalesObjects>>(htmlString);

                    //Sender just for testing
                    Sender.Tell(new OperationResult() { Successful = true });

                    //inject data to DataGrid
                    MainWindow.main.StatusApi = result;
                    //Reset warning string
                    MainWindow.main.Status = "";
                }
            }
            catch (WebException ex)
            {
                //Sender for testing
                Sender.Tell(new OperationResult());
                if (!interval)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError &&
                    ex.Response != null)
                    {
                        var resp = (HttpWebResponse)ex.Response;
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("wrong URL", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(resp.StatusCode.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unexpected error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                // try to avoid a lot of error windows in interval mode
                else
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError &&
                    ex.Response != null)
                    {
                        var resp = (HttpWebResponse)ex.Response;
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                        {
                            MainWindow.main.Status = "wrong URL";
                        }
                        else
                        {
                            MainWindow.main.Status = resp.StatusCode.ToString();
                        }
                    }
                    else
                    {
                        MainWindow.main.Status = "Unexpected error";
                    }
                }
            }
            
            return true;
        }
    }
}
