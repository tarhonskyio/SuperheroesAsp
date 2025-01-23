using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperheroesAspNet.Models;
using SuperheroesAspNet.Models.Superheroes;

public class HeroesController : Controller
{
    private readonly SuperheroesContext _context;

    public HeroesController(SuperheroesContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var totalItems = await _context.Superheroes.CountAsync();
        
        var heroes = await _context.Superheroes
            .OrderBy(h => h.SuperheroName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(h => new HeroViewModel
            {
                Id = h.Id,
                SuperheroName = h.SuperheroName,
                FullName = h.FullName,
                HeightCm = h.HeightCm,
                WeightKg = h.WeightKg
            })
            .ToListAsync();
        
        var viewModel = new PagingViewModel<HeroViewModel>
        {
            Items = heroes,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };

        return View(viewModel);
    }
    public async Task<IActionResult> Powers(int id)
    {
        var powers = await _context.Superpowers
            .Where(hp => hp.Id == id) 
            .Select(hp => hp.PowerName) 
            .ToListAsync();
        
        var superhero = await _context.Superheroes
            .Where(h => h.Id == id)
            .Select(h => h.SuperheroName)
            .FirstOrDefaultAsync();
        
        if (superhero == null)
        {
            return NotFound();
        }
        
        ViewBag.SuperheroName = superhero;
        
        return View(powers);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HeroViewModel model)
    {
        if (ModelState.IsValid)
        {
            var hero = new Superhero
            {
                SuperheroName = model.SuperheroName,
                FullName = model.FullName,
                HeightCm = model.HeightCm,
                WeightKg = model.WeightKg
            };

            _context.Superheroes.Add(hero);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
    public async Task<IActionResult> Superpowers()
    {
        var superpowers = await _context.Superpowers
            .GroupBy(sp => sp.PowerName)
            .Select(g => new SuperpowerViewModel
            {
                PowerName = g.Key,
                HeroCount = g.Count()
            })
            .OrderByDescending(sp => sp.HeroCount)
            .ToListAsync();

        return View(superpowers);
    }
}