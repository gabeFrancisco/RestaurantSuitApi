namespace RecantosSystem.Api.Models
{
    public class UserWorkGroup : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual WorkGroup WorkGroup { get; set; }
        public int WorkGroupId { get; set; }
    }
}