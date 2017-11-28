using System.Runtime.Serialization;

namespace Temporary_Prison.Service.Contracts.Dto
{
    [DataContract]
    public class DataErrorDto
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorDetails { get; set; }
    }
}
