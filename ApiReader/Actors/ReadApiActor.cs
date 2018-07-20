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
using Newtonsoft.Json.Linq;

namespace ApiReader.Actors
{

    interface IReadApiActor
    {
        String GetHtmlString(string userName, string repostioryName);
        String ParseToJson(string htmlString);
        void InputToTabel(string parsedJson);


    }
    /// <summary>
    /// Actor responsible for getApi
    /// </summary>
    public class ReadApiActor : ReceiveActor, IReadApiActor
    {
        private ApiReaderViewModel apiReaderViewModel;
        //just for testing
        public sealed class OperationResult
        { 
            public OperationResult()
            {
            }
            public bool Successful { set; get; }
        }

        public ReadApiActor(ApiReaderViewModel apiReaderViewModel)
        {
            this.apiReaderViewModel = apiReaderViewModel;
            Receive<object>(GetApi);
        }

        public bool GetApi(Object message)
        {

            String htmlString = GetHtmlString(apiReaderViewModel.GithubUsername, apiReaderViewModel.GithubRepositoryName);
            String parsedJson = ParseToJson(htmlString);
            InputToTabel(parsedJson);
            //InputToTabel("empty");
            //Sender just for testing
            Sender.Tell(new OperationResult() { Successful = true });
            return true;
        }

        public string GetHtmlString(string userName, string repostioryName)
        {
            string url = "https://api.github.com/repos/" + userName + "/" + repostioryName;
            String htmlString;
            try
            {
                HttpWebRequest httpprequest = WebRequest.Create(url) as HttpWebRequest;

                httpprequest.UserAgent = "userAgent";

                httpprequest.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)httpprequest.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    htmlString = reader.ReadToEnd();
                }
            }
            catch
            {
                htmlString = "404";
                MessageBox.Show("Unexpected error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return htmlString;
        }

        public string ParseToJson(string htmlString)
        {
            if (htmlString == "404") return htmlString;
            JsonTextReader rs = new JsonTextReader(new StringReader(htmlString));
            //return ("Token: {0}, Value: {1}" + read.TokenType + read.Value);
            //read.Read();
            JObject o2 = (JObject)JToken.ReadFrom(rs);
            IList<string> keys = o2.Properties().Select(p => p.Name).ToList();
            StringBuilder output = new StringBuilder();
            output.Append("[{\n");
            JToken value = "full_name";
            List<string> mylist = new List<string>(new string[] { "full_name", "mirror_url" , "description", "clone_url","language", "open_issues_count" ,"watchers",
                                       "created_at" });
            foreach (JProperty property in o2.Properties())
            {
                if (mylist.Any(s => property.Name.Contains(s)))
                {

                    if (property.Value.Type.ToString() == "Integer")
                    {

                        output.Append("\"").Append(property.Name).Append("\": ").Append(property.Value).Append(",\n");
                    }
                    else if (property.Value.Type.ToString() == "Null")
                    {
                        output.Append("\"").Append(property.Name).Append("\": null,\n");
                    }
                    else
                    {
                        output.Append("\"").Append(property.Name).Append("\": \"").Append(property.Value).Append("\",\n");
                    }

                }


            }
            var jsonResult = output.ToString().Remove(output.Length - 2) + "\n}]";
            return jsonResult;
        }

        public void InputToTabel(string parsedJson)
        {
            if (parsedJson == "404") return;
            var result = JsonConvert.DeserializeObject<List<SalesObjects>>(parsedJson);

            apiReaderViewModel.ApiText = parsedJson;

            //apiReaderViewModel.ApiText = System.DateTime.Now.ToString();
        }



        /*
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

return true;*/


    }
}
