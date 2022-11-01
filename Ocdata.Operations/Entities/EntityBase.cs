using System.ComponentModel.DataAnnotations;

namespace Ocdata.Operations.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? State { get; set; }

        public void Activate() => State = (int)Enums.State.Active;

        public void Passivated() => State = (int)Enums.State.Passive;

        public void Delete() => State = (int)Enums.State.Deleted;
    }
}
