using System;
using System.Collections.Generic;
using System.Text;

namespace Svinx.FindMe.Libraries.Search
{
    interface IServer
    {
        T GetRepository<T>() where T : IRepository;
    }
}
