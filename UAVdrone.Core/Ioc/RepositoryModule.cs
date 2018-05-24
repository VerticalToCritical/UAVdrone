using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using UAVdrone.Core.Repository;

namespace UAVdrone.Core.Ioc
{
    public class RepositoryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DroneRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
