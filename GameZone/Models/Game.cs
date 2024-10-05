using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game : InitialEntity
    {
        

        
        
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; }=string.Empty;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = default!;

        //خلي بالك ان ال string بيكون required by default

        public ICollection<GameDevice> Device { get; set; } = new List<GameDevice>();
    }
}
