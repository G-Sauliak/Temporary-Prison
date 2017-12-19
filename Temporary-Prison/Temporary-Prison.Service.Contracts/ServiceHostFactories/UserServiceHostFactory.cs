using System;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Repository;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using Temporary_Prison.Service.Contracts.Contracts;

namespace Temporary_Prison.Service.Contracts.ServiceHostFactories
{
    public class UserServiceHostFactory : ServiceHostFactory
    {
        private readonly IUserRepository userRepository;
        public UserServiceHostFactory()
        {
            userRepository = new UserRepository();
        }
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UserServiceHost(userRepository, serviceType, baseAddresses);
        }
    }
    public class UserServiceHost : ServiceHost
    {
        public UserServiceHost(IUserRepository repository, Type serviceType, params Uri[] baseAddresses)
    : base(serviceType, baseAddresses)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("UserRepository");
            }

            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new UserInstanceProvider(repository));
            }
        }
    }
    public class UserInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly IUserRepository repository;

        public UserInstanceProvider(IUserRepository repository)
        {
            this.repository = repository;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new UserService(repository);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}


