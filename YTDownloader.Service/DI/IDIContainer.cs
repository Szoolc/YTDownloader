using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader.Service.DI
{
    public interface IDIContainer
    {
        void Setup();

        T GetInstance<T>();
    }
}