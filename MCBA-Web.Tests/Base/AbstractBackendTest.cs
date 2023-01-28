using Autofac;
using MCBA_Web.Tests.Modules;

namespace MCBA_Web.Tests.Base
{
    public abstract class AbstractBackendTest : AbstractBaseTest
    {
        protected AbstractBackendTest() => Builder.RegisterModule<BackendModule>();
    }
}

