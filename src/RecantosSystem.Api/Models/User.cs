using RecantosSystem.Api.Models.Enums;

namespace RecantosSystem.Api.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}