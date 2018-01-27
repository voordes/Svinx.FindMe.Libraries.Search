using Svinx.Libraries.Queues;
using System.Threading.Tasks;

namespace Svinx.FindMe.Libraries.Search
{
    public interface IClient
    {
        Task<Results> Search(Query query);
    }
}
