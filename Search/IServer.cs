using System;
using System.Threading.Tasks;

namespace Svinx.FindMe.Libraries.Search
{
    public interface IServer
    {
        Task Start(Func<Query, Results> callback);
    }
}
