
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GameZone.Settings;
using Microsoft.EntityFrameworkCore;

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

       
    }
}
