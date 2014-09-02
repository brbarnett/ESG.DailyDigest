using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Repositories.Implementations;

using System;
using ESG.DailyDigest.Services.Implementations;
using ESG.DailyDigest.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace ESG.DailyDigest.DI
{
    public static class UnityContainerFactory
    {
        private static IUnityContainer _container;

        public static IUnityContainer GetContainer()
        {
            return _container ?? (_container = BuildContainer());
        }

        private static IUnityContainer BuildContainer()
        {
            if (_container != null)
            {
                return _container;
            }

            _container = new UnityContainer();

            RegisterTypes("Repositories", () => RegisterRepositories(_container));
            RegisterTypes("Services", () => RegisterServices(_container));

            return _container;
        }

        private static void RegisterTypes(string groupName, Action registration)
        {
            try
            {
                registration.Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Registration failed for {0}. {1}", groupName, ex.Message), ex.InnerException);
            }
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ISportsService, SportsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITransportationService, TransportationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWeatherService, WeatherService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITimeService, TimeService>(new HierarchicalLifetimeManager());
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<ISportDataRepository, EspnRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmailRepository, SendGridRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITransportationRepository, CtaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IWeatherRepository, WundergroundRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITimeRepository, TimeRepository>(new HierarchicalLifetimeManager());
        }
    }
}
