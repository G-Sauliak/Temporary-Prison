﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Temporary_Prison.Data.UserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserService.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/IsValidLogin", ReplyAction="http://tempuri.org/IUserService/IsValidLoginResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Temporary_Prison.Service.Contracts.Dto.DataErrorDto), Action="http://tempuri.org/IUserService/IsValidLoginDataErrorDtoFault", Name="DataErrorDto", Namespace="http://schemas.datacontract.org/2004/07/Temporary_Prison.Service.Contracts.Dto")]
        bool IsValidLogin(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/IsValidLogin", ReplyAction="http://tempuri.org/IUserService/IsValidLoginResponse")]
        System.Threading.Tasks.Task<bool> IsValidLoginAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByName", ReplyAction="http://tempuri.org/IUserService/GetUserByNameResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Temporary_Prison.Service.Contracts.Dto.DataErrorDto), Action="http://tempuri.org/IUserService/GetUserByNameDataErrorDtoFault", Name="DataErrorDto", Namespace="http://schemas.datacontract.org/2004/07/Temporary_Prison.Service.Contracts.Dto")]
        Temporary_Prison.Service.Contracts.Dto.UserDto GetUserByName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByName", ReplyAction="http://tempuri.org/IUserService/GetUserByNameResponse")]
        System.Threading.Tasks.Task<Temporary_Prison.Service.Contracts.Dto.UserDto> GetUserByNameAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : Temporary_Prison.Data.UserService.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<Temporary_Prison.Data.UserService.IUserService>, Temporary_Prison.Data.UserService.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsValidLogin(string userName, string password) {
            return base.Channel.IsValidLogin(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> IsValidLoginAsync(string userName, string password) {
            return base.Channel.IsValidLoginAsync(userName, password);
        }
        
        public Temporary_Prison.Service.Contracts.Dto.UserDto GetUserByName(string userName) {
            return base.Channel.GetUserByName(userName);
        }
        
        public System.Threading.Tasks.Task<Temporary_Prison.Service.Contracts.Dto.UserDto> GetUserByNameAsync(string userName) {
            return base.Channel.GetUserByNameAsync(userName);
        }
    }
}
