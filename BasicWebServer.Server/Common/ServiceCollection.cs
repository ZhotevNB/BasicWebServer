

namespace BasicWebServer.Server.Common
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type,Type>services;

        public ServiceCollection()
        {
            services= new Dictionary<Type,Type>();
        }
        public IServiceCollection Add<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
           this.services.Add(typeof(TService),typeof(TImplementation));

            return this;
        }

        public IServiceCollection Add<TService>() where TService : class
        {
           return Add<TService,TService>();
        }

        public object CreateInstase(Type serviceType)
        {
            if (services.ContainsKey(serviceType))
            {
                serviceType = services[serviceType];
            }
            else if (serviceType.IsInterface)
            {
                throw new InvalidOperationException($"Service {serviceType.FullName} is not registered");
            }

            var constructors=serviceType.GetConstructors();

            if (constructors.Length>1)
            {
                throw new InvalidOperationException("Multipal constructors are not supported");
            }

            var constructor = constructors.First();
            var parameters = constructor.GetParameters();
            var parametersValues = new object[parameters.Length];

            for (int i = 0; i < parametersValues.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                var parameterValue = CreateInstase(parameterType);

                parametersValues[i] = parameterValue;
            }

            return constructor.Invoke(parametersValues);
        }

        public TService Get<TService>() where TService : class
        {
            var serviceType=typeof(TService);

            if (!services.ContainsKey(serviceType))
            {
                return null;
            }
            var service = services[serviceType];

            return (TService)CreateInstase(service);
        }
    }
}
