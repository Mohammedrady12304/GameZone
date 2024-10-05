using Microsoft.AspNetCore.Mvc;
using GameZone.ViewModels;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameZone.Services;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {

        private readonly ApplicationDbContext _context ;

        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService ;
        private readonly IGamesService _gamesService;
        public GamesController(ApplicationDbContext context, ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService)
        {
            _context = context;
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
        }



        public IActionResult Index()
        {
            return View();
        }

        //هنا هنحتاج اكشن نوعه get
        //ده اللي هيعرض الفورم نفسها
        //وبعد كده هيكون عندنا اكشن من نوع post عشان يستقبل الفورم دي ويتعامل معاها سواء من ناحية ال validation او يبعت ال values للداتا بيز ويعملها save



        //HttpGet عشان هيعرض بس 
        //لو عايز استقبل ال list واعدل عليها httpPost
        [HttpGet]
        public IActionResult Create()
        {
            // var categories = _context.Categories.ToList();//كده انا واخد list من النوع category (Domain model)
            var viewModel = new CreateGameFormViewModel
            {
                //انا بعمل ده عشان ابعت لل view ال model حتى لو فاضي لكن ميكونش ب null
                Categories = _categoriesService.GetSelectList()

                ,

                Devices = _devicesService.GetSelecList()
                //هنا انا حولت كل اللي جوا الكاتيجوريز الى selectListItem
            };


            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelecList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _gamesService.Create(model);//خلي بالك ان ال create دي asyncronus


            return RedirectToAction(nameof(Index));

        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CreateGameFormViewModel model)
        //{
        //    return View();
        //}
    }
}
