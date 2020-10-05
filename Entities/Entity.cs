using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}