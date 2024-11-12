using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll();
        Task Create(CreateGameFormViewModel model);
        Task<Game> GetById(int id);
        Task UpdateGame(EditGameFormViewModel model); // دالة التعديل
        bool Delete(int id);

        IEnumerable<SelectListItem> GetCategories();

        // دالة لجلب الأجهزة المتاحة
        IEnumerable<SelectListItem> GetDevices();
    }
}
