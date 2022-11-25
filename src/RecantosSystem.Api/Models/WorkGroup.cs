using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
    public class WorkGroup : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual User Administrator { get; set; }
        [Required]
        public int AdministratorId { get; set; }
        public IEnumerable<User> Workers { get; set; }
    }
}