using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
