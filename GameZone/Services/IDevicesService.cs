using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface IDevicesService
    {
        IEnumerable<SelectListItem> GetSelecList();
    }
}
