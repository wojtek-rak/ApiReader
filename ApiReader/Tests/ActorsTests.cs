using Akka.Actor;
using Akka.TestKit.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiReader.Actors;
using System.Threading;
using System.Diagnostics;

namespace ApiReader.Tests
{
    [TestFixture]
    class ActorsTests : TestKit
    {
        //[Test]
        //public void ReadApiActorTest()
        //{
        //    var identity = Sys.ActorOf(Props.Create(() => new ReadApiActor()));
        //    identity.Tell(false);
        //    var result = ExpectMsg<ReadApiActor.OperationResult>().Successful;
        //    Assert.True(result);
        //}
        //[Test]
        //public void IntervalActorTestCreation()
        //{
        //    var identity = Sys.ActorOf(Props.Create(() => new IntervalActor()));
        //    identity.Tell(new object { });
        //    var result = ExpectMsg<IntervalActor.OperationResult>().Successful;

        //    Assert.True(result);
        //}

        
        


    }
}
