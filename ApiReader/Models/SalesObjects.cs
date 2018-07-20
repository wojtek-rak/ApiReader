using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReader
{

    public class SalesObjects
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public String Date { get; set; }
        public String CloneUrl { get; set; }
        public int WatchersCount { get; set; }
        public String Language { get; set; }
        public String MirrorUrl { get; set; }
        public int OpenIssuesCount { get; set; }
        public int Watchers { get; set; }
    }

}
