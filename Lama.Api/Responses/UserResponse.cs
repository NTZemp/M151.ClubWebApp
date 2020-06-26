using System;
namespace Lama.Api.Responses
{
    public class UserResponse
    {
        public UserResponse()
        {
            
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
