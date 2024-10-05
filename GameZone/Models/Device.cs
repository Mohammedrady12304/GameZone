using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Device :InitialEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;

        //هنا معملش relation مش عارف ليه
    }
}
