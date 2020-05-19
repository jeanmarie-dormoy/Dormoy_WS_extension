using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/ServiceY/Service1");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(RequestY), httpUrl);

            // Multiple end points can be added to the Service using AddServiceEndpoint() method.
            // Host.Open() will run the service, so that it can be used by any client.

            // Example adding :
            // Uri tcpUrl = new Uri("net.tcp://localhost:8090/MyService/SimpleCalculator");
            // ServiceHost host = new ServiceHost(typeof(MyCalculatorService.SimpleCalculator), httpUrl, tcpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IRequestY), new WSHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            smb.HttpGetEnabled = true;
            // host.Description.Behaviors.Add(smb);

            /*
            host.Description.Endpoints
                .Select(e => e.Contract)
                .Distinct()
                .SelectMany(c => c.Operations)
                .ForEach(o => o.Behaviors.Add(operationBehavior)); */

            //Start the Service
            host.Open();

            Console.WriteLine("ServiceY is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
    }
}
