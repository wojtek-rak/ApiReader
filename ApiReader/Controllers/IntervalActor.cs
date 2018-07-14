using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ApiReader.Controllers
{
    /// <summary>
    /// Actor responsible for intervals
    /// </summary>
    class IntervalActor : ReceiveActor
    {
        public sealed class OperationResult
        {
            //just for testing
            public OperationResult()
            {
            }
            public bool Successful { set; get; }
        }
        public IntervalActor()
        {
            Receive<object>(Increment);
        }

        public bool Increment(object message)
        {
            long start, end;
            int interval;
            start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (true)
            {
                //sender for testing
                Sender.Tell(new OperationResult() { Successful = true });

                //get the actual interval
                interval = MainWindow.main.Interval;
                
                if (interval != -1)
                {
                    
                    //time of interval cannot be less than 1 sec
                    if (interval < 1)
                    {
                        interval = 1;
                    }

                    end = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    if(end - start > interval * 1000)
                    {
                        start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        var readApiActor =  Context.System.ActorOf<ReadApiActor>();
                        readApiActor.Tell(true);
                    }
                    
                }
            }
            return true;
        }
    }
}
