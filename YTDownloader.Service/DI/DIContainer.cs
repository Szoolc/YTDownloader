using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader.Service.DI
{
    public class DIContainer : IDIContainer
    {
        private StandardKernel kernel;

        public DIContainer()
        {
            kernel = new StandardKernel();
        }

        public void Setup()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        public T GetInstance<T>()
        {
            T instance = (T)kernel.Get(typeof(T));
            return instance;
        }
    }
}