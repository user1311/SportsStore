using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public NavigationMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories;
            return View(categories);
        }
    }
}
