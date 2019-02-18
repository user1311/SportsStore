using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> Categories => _context.Categories.ToList();

        public void Add(Category category) => _context.Categories.Add(category);
    }
}
