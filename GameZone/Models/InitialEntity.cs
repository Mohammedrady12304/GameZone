using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class InitialEntity
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
    }
}
