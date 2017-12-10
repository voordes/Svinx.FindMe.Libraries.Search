using Svinx.Libraries.Queues;

namespace Svinx.FindMe.Libraries.Search
{
    public interface IClient
    {
        dynamic Search(string query);
    }
}
