using System;

namespace RecantosSystem.Api.Models
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
		private DateTime? _createdAt;
		public DateTime? CreatedAt
		{
			get { return _createdAt; }
			set { _createdAt = (value == null ? DateTime.UtcNow : value); }
		}
        public DateTime? UpdatedAt { get; set; }
	}
}