using Svinx.Libraries.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Svinx.FindMe.Libraries.SearchClient
{
    public class Client
    {
        public Client()
        {

        }

        public dynamic Search(string query)
        {
            var client = new RPCClient(ConfigurationManager.AppSettings["QueueUrl"]);
            client.Start(ConfigurationManager.AppSettings["QueueName"]);
            var response = client.Call<dynamic, dynamic>(new { Method = "Search", Query = query});
            return response;
        }
    }
}
