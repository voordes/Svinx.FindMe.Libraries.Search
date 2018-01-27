using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Svinx.FindMe.Libraries.Search;
using Svinx.Libraries.Queues.RabbitMQ;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class Search
    {
        [TestMethod]
        public void RunSearch()
        {
            var t1 = Client();
            var t2 = Server();
            Task.WaitAll(new Task[] { t1, t2 }, 1000);
            Assert.IsTrue(t1.IsCompleted, "RPC did not finish within 1 second");
            Assert.IsTrue(t1.Exception == null, $"Client returned exception: {t1.Exception?.ToString()}");
            Assert.IsTrue(t2.Exception == null, $"Server returned exception: {t2.Exception?.ToString()}");
        }

        private async Task Client()
        {
            var query = new Query()
            {
                Category = "Category",
                Term = "Term"
            };
            var options = Options.Create(new Svinx.Libraries.Queues.RabbitMQ.Queue()
            {
                queueUrl = "amqp://hjtqnayg:8s2sjVjplSmQ0G7gNBOWt7f-r0Df60R8@sheep.rmq.cloudamqp.com/hjtqnayg",
                queueName = "Search"
            });
            using (var client = new Client(new RPCClient(options)))
            {
                var results = await client.Search(query);
                var item1 = results.Items.FirstOrDefault();
                Assert.IsTrue(  
                    results.Term == query.Term &&
                    results.Count == 1 &&
                    item1?.Category == query.Category &&
                    item1?.Title == "Title 1" &&
                    item1.Description == "Description 1" &&
                    item1.Url == "Url 1",
                    $"Did not receive expected result");
            }
        }

        private async Task Server()
        {
            var options = Options.Create(new Svinx.Libraries.Queues.RabbitMQ.Queue()
            {
                queueUrl = "amqp://hjtqnayg:8s2sjVjplSmQ0G7gNBOWt7f-r0Df60R8@sheep.rmq.cloudamqp.com/hjtqnayg",
                queueName = "Search"
            });
            using (var server = new Server(new RPCServer(options)))
            {
                await server.Start(q => new Results() {
                    Term = q.Term,
                    Count = 1,
                    Items = new Result[]
                    { new Result {
                        Category = q.Category,
                        Title = "Title 1",
                        Description = "Description 1",
                        Url = "Url 1" } } });
            }
        }
    }
}
