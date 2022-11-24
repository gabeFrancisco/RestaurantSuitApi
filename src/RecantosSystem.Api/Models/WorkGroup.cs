using System.Collections.Generic;

namespace RecantosSystem.Api.Models
{
    public class WorkGroup : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public User Administrator { get; set; }
        public IEnumerable<User> Workers { get; set; }
    }
}