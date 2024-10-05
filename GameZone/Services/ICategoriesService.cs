using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface ICategoriesService
    {
        
        IEnumerable<SelectListItem> GetSelectList();
    }
}
