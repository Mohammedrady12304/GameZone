
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            _imagePath =$"{_webHostEnvironment.WebRootPath}/assets/images/games";
            
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

        
            



    }
}
