using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {
        
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories = Enumerable.Empty<SelectListItem>(); //ركز هنا كده هيختار كاتجوري واحده

        [Display(Name = "supported Devices")]


        // public List<int> SelectedDevices { get; set; }= new List<int>(); دي كده مش مخليه ال field required
        //هنا دي list<int> عشان هستقبل ال ids
        public List<int> SelectedDevices { get; set; } = default!;//دي كده بقت required


        public IEnumerable<SelectListItem> Devices = Enumerable.Empty<SelectListItem>(); //ركز هنا هيختار كذا device
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
       // [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxSize(FileSettings.MaxSizeInByts) , AllowedExtensions(FileSettings.AllowedExtensions)]
        public IFormFile Cover { get; set; } = default!;
    }
}
