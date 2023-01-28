using Autofac;


namespace MCBA_Web.Tests.Base
{
    public abstract class AbstractBaseTest : IDisposable
    {
        private static readonly object ContainerLock = new();

        private IContainer _container;
        protected ContainerBuilder Builder { get; }

        protected AbstractBaseTest()
        {
            Builder = new ContainerBuilder();
        }

        protected IContainer Container
        {
            get
            {
                if (_container == null)
                    BuildContainer();

                return _container;
            }
        }

        private void BuildContainer()
        {
            lock (ContainerLock)
            {
                _container ??= Builder.Build();
            }
        }

        public virtual void Dispose() => Container.Dispose();
    }
}

