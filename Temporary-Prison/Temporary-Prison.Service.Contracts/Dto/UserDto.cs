using System.Runtime.Serialization;

namespace Temporary_Prison.Service.Contracts.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string[] Roles { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
