using Svinx.Libraries.Queues;

namespace Svinx.FindMe.Libraries.Search
{
    public class Client: IClient
    {
        private IRPCClient _client;

        public Client(IRPCClient client)
        {
            _client = client;
        }

        public dynamic Search(string query)
        {
            var response = _client.Call<dynamic, dynamic>(new { Method = "Search", Query = query });
            return response;
        }
        
    }
}
