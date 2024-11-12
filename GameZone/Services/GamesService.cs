
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GameZone.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;
        public GamesService(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath =$"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";//هنا انا هكتب ال path في  static class هسميه file settings عشان مقعدش اغير فيه كل شوية 
            
        }

        public IEnumerable<Game> GetAll()
        {
            return _context
                .Games
                .Include(g=>g.Device)
                .ThenInclude(d=>d.Device)
                .Include(g=>g.Category)
                .AsNoTracking().ToList();
        }
        public async Task Create(CreateGameFormViewModel model)
        {


           
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            
            var path= Path.Combine(_imagePath,coverName);

            using var stream = File.Create(path);

            await model.Cover.CopyToAsync(stream);
            //stream.Dispose(); => طالما عامل using يبقا مش لازم اكتب السطر ده

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = coverName,
                CategoryId = model.CategoryId,
                Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };

            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game> GetById(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game=_context.Games.Find(id);
            if (game is null)
                return isDeleted;

            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {

            isDeleted = true;
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);


            }

            return isDeleted;
        }

        public async Task UpdateGame(EditGameFormViewModel model)
        {
            // جلب اللعبة من قاعدة البيانات
            var game = await _context.Games
                .Include(g => g.Device)
                .FirstOrDefaultAsync(g => g.Id == model.Id);

            if (game == null)
            {
                throw new Exception("Game not found.");
            }

            // تحديث خصائص اللعبة
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;

            // إذا كان هناك غلاف جديد، قم بتحديثه
            if (model.Cover != null)
            {
                // حذف الغلاف القديم إذا كان موجودًا
                var oldCoverPath = Path.Combine(_imagePath, game.Cover);
                if (File.Exists(oldCoverPath))
                {
                    File.Delete(oldCoverPath);
                }

                // حفظ الغلاف الجديد
                var newCoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
                var newCoverPath = Path.Combine(_imagePath, newCoverName);

                using (var stream = File.Create(newCoverPath))
                {
                    await model.Cover.CopyToAsync(stream);
                }

                game.Cover = newCoverName;
            }

            // تحديث الأجهزة المدعومة
            game.Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d, GameId = game.Id }).ToList();

            // حفظ التغييرات في قاعدة البيانات
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            return _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetDevices()
        {
            return _context.Devices.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
        }
    }
}
