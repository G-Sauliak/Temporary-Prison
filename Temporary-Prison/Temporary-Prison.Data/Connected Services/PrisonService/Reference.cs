﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Temporary_Prison.Data.PrisonService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PrisonService.IPrisonService")]
    public interface IPrisonService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPrisonService/GetPrisoners", ReplyAction="http://tempuri.org/IPrisonService/GetPrisonersResponse")]
        Temporary_Prison.Service.Contracts.Dto.PrisonerDto[] GetPrisoners();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPrisonService/GetPrisoners", ReplyAction="http://tempuri.org/IPrisonService/GetPrisonersResponse")]
        System.Threading.Tasks.Task<Temporary_Prison.Service.Contracts.Dto.PrisonerDto[]> GetPrisonersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPrisonService/GetPrisonerById", ReplyAction="http://tempuri.org/IPrisonService/GetPrisonerByIdResponse")]
        Temporary_Prison.Service.Contracts.Dto.PrisonerDto[] GetPrisonerById();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPrisonService/GetPrisonerById", ReplyAction="http://tempuri.org/IPrisonService/GetPrisonerByIdResponse")]
        System.Threading.Tasks.Task<Temporary_Prison.Service.Contracts.Dto.PrisonerDto[]> GetPrisonerByIdAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPrisonServiceChannel : Temporary_Prison.Data.PrisonService.IPrisonService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PrisonServiceClient : System.ServiceModel.ClientBase<Temporary_Prison.Data.PrisonService.IPrisonService>, Temporary_Prison.Data.PrisonService.IPrisonService {
        
        public PrisonServiceClient() {
        }
        
        public PrisonServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PrisonServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PrisonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PrisonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Temporary_Prison.Service.Contracts.Dto.PrisonerDto[] GetPrisoners() {
            return base.Channel.GetPrisoners();
        }
        
        public System.Threading.Tasks.Task<Temporary_Prison.Service.Contracts.Dto.PrisonerDto[]> GetPrisonersAsync() {
            return base.Channel.GetPrisonersAsync();
        }
        
        public Temporary_Prison.Service.Contracts.Dto.PrisonerDto[] GetPrisonerById() {
            return base.Channel.GetPrisonerById();
        }
        
        public System.Threading.Tasks.Task<Temporary_Prison.Service.Contracts.Dto.PrisonerDto[]> GetPrisonerByIdAsync() {
            return base.Channel.GetPrisonerByIdAsync();
        }
    }
}